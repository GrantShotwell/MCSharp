using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage;
using MCSharp.Linkage.Extensions;
using MCSharp.Linkage.Minecraft;
using MCSharp.Linkage.Predefined;
using MCSharp.Linkage.Script;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using ConstructorDefinitionContext = MCSharpParser.Constructor_definitionContext;
using MemberDefinitionContext = MCSharpParser.Member_definitionContext;
using TypeDefinitionContext = MCSharpParser.Type_definitionContext;
using StatementContext = MCSharpParser.StatementContext;
using CodeBlockContext = MCSharpParser.Code_blockContext;
using LanguageFunctionContext = MCSharpParser.Language_functionContext;
using IfStatementContext = MCSharpParser.If_statementContext;
using ForStatementContext = MCSharpParser.For_statementContext;
using ForeachStatementContext = MCSharpParser.Foreach_statementContext;
using WhileStatementContext = MCSharpParser.While_statementContext;
using DoStatementContext = MCSharpParser.Do_statementContext;
using ReturnStatementContext = MCSharpParser.Return_statementContext;
using ThrowStatementContext = MCSharpParser.Throw_statementContext;
using TryStatementContext = MCSharpParser.Try_statementContext;
using InitializationExpressionContext = MCSharpParser.Initialization_expressionContext;
using ExpressionContext = MCSharpParser.ExpressionContext;
using NonAssignmentExpressionContext = MCSharpParser.Non_assignment_expressionContext;
using ConditionalExpressionContext = MCSharpParser.Conditional_expressionContext;
using LambdaExpressionContext = MCSharpParser.Lambda_expressionContext;
using NullCoalescingExpressionContext = MCSharpParser.Null_coalescing_expressionContext;
using ConditionalOrExpressionContext = MCSharpParser.Conditional_or_expressionContext;
using ConditionalAndExpressionContext = MCSharpParser.Conditional_and_expressionContext;
using InclusiveOrExpressionContext = MCSharpParser.Inclusive_or_expressionContext;
using ExclusiveOrExpressionContext = MCSharpParser.Exclusive_or_expressionContext;
using AndExpressionContext = MCSharpParser.And_expressionContext;
using EqualityExpressionContext = MCSharpParser.Equality_expressionContext;
using RelationalExpressionContext = MCSharpParser.Relational_expressionContext;
using RelationOrTypeCheckContext = MCSharpParser.Relation_or_type_checkContext;
using ShiftExpressionContext = MCSharpParser.Shift_expressionContext;
using AdditiveExpressionContext = MCSharpParser.Additive_expressionContext;
using MultiplicativeExpressionContext = MCSharpParser.Multiplicative_expressionContext;
using WithExpressionContext = MCSharpParser.With_expressionContext;
using RangeExpressionContext = MCSharpParser.Range_expressionContext;
using PreStepExpressionContext = MCSharpParser.Pre_step_expressionContext;
using PostStepExpressionContext = MCSharpParser.Post_step_expressionContext;
using UnaryExpressionContext = MCSharpParser.Unary_expressionContext;
using CastExpressionContext = MCSharpParser.Cast_expressionContext;
using PointerIndirectionExpressionContext = MCSharpParser.Pointer_indirection_expressionContext;
using AddressofExpressionContext = MCSharpParser.Addressof_expressionContext;
using AssignmentExpressionContext = MCSharpParser.Assignment_expressionContext;
using PrimaryExpressionContext = MCSharpParser.Primary_expressionContext;
using ArrayCreationExpressionContext = MCSharpParser.Array_creation_expressionContext;
using PrimaryNoArrayCreationExpressionContext = MCSharpParser.Primary_no_array_creation_expressionContext;
using KeywordExpressionContext = MCSharpParser.Keyword_expressionContext;

namespace MCSharp.Compilation {

	public class Compiler : IDisposable {

		public Settings Settings { get; }
		public VirtualMachine VirtualMachine { get; private set; }
		public ICollection<Assembly> Assemblies { get; }

		public Compiler(Settings settings, ICollection<Assembly> assemblies) {
			Settings = settings;
			VirtualMachine = new VirtualMachine();
			Assemblies = assemblies;
		}

		#region Data

		public IDictionary<string, IType> DefinedTypes { get; } = new Dictionary<string, IType>();
		public ICollection<LinkerExtension> LinkerExtensions { get; } = new LinkedList<LinkerExtension>();

		#endregion

		#region Compilation

		public ResultInfo Compile() {


			// Find, parse, and first pass walk (types, members) all script files.
			ResultInfo? FirstPassWalk() {

				foreach(string file in Settings.Datapack.GetScriptFiles()) {

					// Use Antlr generated classes to parse the file.

					ICharStream stream = CharStreams.fromString(File.ReadAllText(file));
					var errorListener = new ScriptErrorListener(file[(Settings.Datapack.ScriptDirectory.Length + 1)..]);

					var lexer = new MCSharpLexer(stream);
					lexer.RemoveErrorListeners();
					lexer.AddErrorListener(errorListener);

					ITokenStream tokens = new CommonTokenStream(lexer);

					var parser = new MCSharpParser(tokens) { BuildParseTree = true };
					parser.RemoveErrorListeners();
					parser.AddErrorListener(errorListener);

					IParseTree tree = parser.script();
					if(errorListener.Result.Failure) {
						return errorListener.Result;
					}

					var walker = new ScriptClassWalker(this);
					ParseTreeWalker.Default.Walk(walker, tree);

				}

				return null;

			}


			// Add linker extensions.
			void AddLinkerExtensions() {
				foreach(Assembly assembly in Assemblies) {
					foreach(Type type in assembly.ExportedTypes) {

						if(!type.IsAbstract && typeof(LinkerExtension).IsAssignableFrom(type)) {

							ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(Compiler) });
							LinkerExtension extension = constructor.Invoke(new object[] { this }) as LinkerExtension;
							LinkerExtensions.Add(extension);

						}

					}
				}
			}


			// Apply linker extensions.
			void ApplyLinkerExtensions() {
				foreach(LinkerExtension extension in LinkerExtensions) {

					extension.CreatePredefinedTypes();

				}
			}

			{

				// Create threads.
				ResultInfo? failure = null;
				Thread threadFirstPassWalk = new Thread(new ThreadStart(() => {
					failure = FirstPassWalk();
				}));
				Thread threadAddLinkerExts = new Thread(new ThreadStart(() => {
					AddLinkerExtensions();
					ApplyLinkerExtensions();
				}));

				// Start threads.
				threadFirstPassWalk.Start();
				threadAddLinkerExts.Start();

				// Wait for threads to finish.
				threadFirstPassWalk.Join();
				threadAddLinkerExts.Join();

				// Stop if lexer/parser had errors.
				if(failure.HasValue) return failure.Value;

			}


			// Compile 'Program.Load()' and 'Program.Tick()'.
			Scope rootScope = new Scope(null, null);
			foreach(IType type in DefinedTypes.Values) {

				if(type.Identifier != "Program") continue;

				Scope typeScope = new Scope(type.Identifier, rootScope);
				foreach(IMember member in type.Members) {

					if(member.Identifier != "Load" && member.Identifier != "Tick") continue;
					if(member.MemberType != MemberType.Method) continue;

					ResultInfo result = CompileStandaloneMethod(typeScope, member);
					if(result.Success) continue;
					else return result;

				}

			}


			return ResultInfo.DefaultSuccess;

		}

		#region Member Compilation

		private ResultInfo CompileStandaloneMethod(Scope typeScope, IMember member) {

			#region Argument Checks
			if(typeScope is null)
				throw new ArgumentNullException(nameof(typeScope));
			if(member is null)
				throw new ArgumentNullException(nameof(member));
			if(member.MemberType != MemberType.Method)
				throw new ArgumentOutOfRangeException(nameof(member), $"The given {member.MemberType} is not {nameof(MemberType.Method)}.");
			#endregion

			Scope methodScope = new Scope(member.Identifier, typeScope);
			IMethod method = member.Definition as IMethod;
			StandaloneStatementFunction invoker = method.Invoker as StandaloneStatementFunction;

			return CompileStatements(invoker, methodScope, invoker.Statements);

		}

		#endregion

		#region Statement Compilation

		private ResultInfo CompileStatements(StandaloneStatementFunction function, Scope scope, ICollection<IStatement> statements) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(statements is null)
				throw new ArgumentNullException(nameof(statements));
			#endregion

			foreach(IStatement statement in statements) {

				if(statement is ScriptStatement) {

					ResultInfo result = CompileStatement(function, scope, statement.Context, false);
					if(result.Failure) return result;

				} else if(statement is PredefinedStatement) {

					ResultInfo result = CompileStatement(function, scope, statement.Context, true);
					// todo: Send exception and say this is a bug if result is failed.
					if(result.Failure) return result;

				} else {

					// todo: Add more information.
					throw new Exception();

				}

			}

			return ResultInfo.DefaultSuccess;

		}

		private ResultInfo CompileStatement(StandaloneStatementFunction function, Scope scope, StatementContext context, bool predefined) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(context is null)
				throw new ArgumentNullException(nameof(context));
			#endregion

			CodeBlockContext code_block = context.code_block();
			if(code_block != null) {

				StatementContext[] statements = code_block.statement();
				return CompileStatements(function, scope, CreateIStatements(statements, predefined));

			}


			LanguageFunctionContext language_function = context.language_function();
			if(language_function != null) {

				IfStatementContext if_statement = language_function.if_statement();
				if(if_statement != null) {

					ExpressionContext condition = if_statement.expression();
					StatementContext[] if_stmts = if_statement.statement();
					StatementContext statement1 = if_stmts[0];
					StatementContext statement2 = if_stmts.Length > 1 ? if_stmts[1] : null;

					ResultInfo conditionResult = CompileExpression(function, scope, condition, predefined, out IInstance value);
					if(conditionResult.Failure) return conditionResult;

					Scope statement1Scope = new Scope(null, scope);
					StandaloneStatementFunction statement1Function = function.CreateChildFunction(CreateIStatements(statement1, predefined), Settings);
					ResultInfo statement1Result = CompileStatement(statement1Function, statement1Scope, statement1, predefined);
					if(statement1Result.Failure) return statement1Result;

					if(statement2 != null) {
						Scope statement2Scope = new Scope(null, scope);
						StandaloneStatementFunction statement2Function = function.CreateChildFunction(CreateIStatements(statement2, predefined), Settings);
						ResultInfo statement2Result = CompileStatement(statement2Function, statement2Scope, statement2, predefined);
						if(statement2Result.Failure) return statement2Result;
					}

					throw new NotImplementedException("'if' statements have not been implemented.");

				}

				ForStatementContext for_statement = language_function.for_statement();
				if(for_statement != null) {

					throw new NotImplementedException("'for' statements have not been implemented.");

				}

				ForeachStatementContext foreach_statement = language_function.foreach_statement();
				if(foreach_statement != null) {

					throw new NotImplementedException("'foreach' statements have not been implemented.");

				}

				WhileStatementContext while_statement = language_function.while_statement();
				if(while_statement != null) {

					throw new NotImplementedException("'while' statements have not been implemented.");

				}

				DoStatementContext do_statement = language_function.do_statement();
				if(do_statement != null) {

					throw new NotImplementedException("'do' statements have not been implemented.");

				}

				ReturnStatementContext return_statement = language_function.return_statement();
				if(return_statement != null) {

					throw new NotImplementedException("'return' statements have not been implemented.");

				}

				ThrowStatementContext throw_statement = language_function.throw_statement();
				if(throw_statement != null) {

					throw new NotImplementedException("'throw' statements have not been implemented.");

				}

				TryStatementContext try_statement = language_function.try_statement();
				if(try_statement != null) {

					throw new NotImplementedException("'try' statements have not been implemented.");

				}

			}


			InitializationExpressionContext initialization_expression = context.initialization_expression();
			if(initialization_expression != null) {

				ITerminalNode[] names = initialization_expression.NAME();
				ITerminalNode typeName = names[0];
				ITerminalNode identifier = names[1];

				IType type = DefinedTypes[typeName.GetText()];
				IInstance instance = type.InitializeInstance(function.Writer, scope, identifier);

				ExpressionContext assignment_value = initialization_expression.expression();
				if(assignment_value != null) {

					ResultInfo assignment_result = CompileExpression(function, scope, assignment_value, predefined, out IInstance value);
					if(assignment_result.Failure) return assignment_result;

					// todo: assign value
					throw new NotImplementedException("Initialization expressions with assignment have not been implemented.");

				}

				return ResultInfo.DefaultSuccess;

			}


			ExpressionContext expression = context.expression();
			if(expression != null) {

				ResultInfo expression_result = CompileExpression(function, scope, expression, predefined, out _);
				if(expression_result.Failure) return expression_result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Statement context could not be determined.");

		}

		#endregion

		#region Expression Compilation

		private ResultInfo CompileSimpleOperation(CompileArguments compile, Operation op, IInstance operand1, IInstance operand2, out IInstance result) {

			IType type1 = operand1.Type;
			IType type2 = operand2.Type;

			ICollection<IOperation> matches = new List<IOperation>(2);

			// Find matches from type 1.
			{
				HashSet<IOperation> operations = type1.Operations[op];
				foreach(IOperation operation in operations) {
					IReadOnlyList<IMethodParameter> parameters = operation.Function.MethodParameters;
					if(parameters.Count != 2)
						throw new InvalidOperationException(
							$"{nameof(CompileSimpleOperation)} was called on {op} expecting exactly 2 parameters, " +
							$"but found {parameters.Count} instead from {type1.Identifier}.");
					if(parameters[0].Identifier == type1.Identifier && parameters[1].Identifier == type2.Identifier) {
						matches.Add(operation);
					}
				}
			}

			// Find matches from type 2.
			if(type2 != type1) {
				HashSet<IOperation> operations = type2.Operations[op];
				foreach(IOperation operation in operations) {
					IReadOnlyList<IMethodParameter> parameters = operation.Function.MethodParameters;
					if(parameters.Count != 2)
						throw new InvalidOperationException(
							$"{nameof(CompileSimpleOperation)} was called on {operation} expecting exactly 2 parameters, " +
							$"but found {parameters.Count} instead from {type2.Identifier}.");
					if(parameters[0].Identifier == type1.Identifier && parameters[1].Identifier == type2.Identifier) {
						matches.Add(operation);
					}
				}
			}

			if(matches.Count == 0) {

				result = null;
				return new ResultInfo(false, $"The {op} operation between '{type1.Identifier}' and '{type2.Identifier}' does not exist.");

			} else {

				// Find the correct operation.
				IOperation operation;
				{
					// TODO: Add deciding between operations when there is more than one match.
					operation = matches.First();
				}

				// Invoke the operation.
				IFunction function = operation.Function;
				ResultInfo functionResult = InvokeFunction(function, compile, new IType[] { }, new IInstance[] { operand1, operand2 }, out result);
				if(functionResult.Failure) return functionResult;

				return ResultInfo.DefaultSuccess;

			}

		}

		private ResultInfo InvokeFunction(IFunction function, CompileArguments compile, IType[] genericArguments, IInstance[] methodArguments, out IInstance result) {

			switch(function) {

				case IStatementFunction statement:
					throw new NotImplementedException("Invoking a statement function through this method has not been implemented.");

				case CustomFunction custom:
					result = custom.Invoke(compile, genericArguments, methodArguments);
					return ResultInfo.DefaultSuccess;

				default: throw new ArgumentOutOfRangeException(nameof(function), $"This type does not have an implemented path for invoking.");

			}

		}

		public ResultInfo CompileExpression(StandaloneStatementFunction function, Scope scope, ExpressionContext expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(expression is null)
				throw new ArgumentNullException(nameof(expression));
			#endregion

			NonAssignmentExpressionContext non_assignment_expression = expression.non_assignment_expression();
			if(non_assignment_expression != null) {

				ResultInfo non_assignment_result = CompileNonAssignmentExpression(function, scope, non_assignment_expression, predefined, out value);
				if(non_assignment_result.Failure) return non_assignment_result;

				return ResultInfo.DefaultSuccess;

			}

			AssignmentExpressionContext assignment_expression = expression.assignment_expression();
			if(assignment_expression != null) {

				ResultInfo assignment_result = CompileAssignmentExpression(function, scope, assignment_expression, predefined, out value);
				if(assignment_result.Failure) return assignment_result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Expression context could not be determined.");

		}

		public ResultInfo CompileNonAssignmentExpression(StandaloneStatementFunction function, Scope scope, NonAssignmentExpressionContext non_assignment_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(non_assignment_expression is null)
				throw new ArgumentNullException(nameof(non_assignment_expression));
			#endregion

			ConditionalExpressionContext conditional_expression = non_assignment_expression.conditional_expression();
			if(conditional_expression != null) {

				ResultInfo conditional_result = CompileConditionalExpression(function, scope, conditional_expression, predefined, out value);
				if(conditional_result.Failure) return conditional_result;

				return ResultInfo.DefaultSuccess;

			}

			LambdaExpressionContext lambda_expression = non_assignment_expression.lambda_expression();
			if(lambda_expression != null) {

				ResultInfo lambda_result = CompileLambdaExpression(function, scope, lambda_expression, predefined, out value);
				if(lambda_result.Failure) return lambda_result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Non-assignment expression context could not be determined.");

		}

		public ResultInfo CompileLambdaExpression(StandaloneStatementFunction function, Scope scope, LambdaExpressionContext lambda_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(lambda_expression is null)
				throw new ArgumentNullException(nameof(lambda_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		public ResultInfo CompileConditionalExpression(StandaloneStatementFunction function, Scope scope, ConditionalExpressionContext conditional_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(conditional_expression is null)
				throw new ArgumentNullException(nameof(conditional_expression));
			#endregion

			value = null;

			NullCoalescingExpressionContext null_coalescing_expression = conditional_expression.null_coalescing_expression();
			ResultInfo null_coalescing_result = CompileNullCoalescingExpression(function, scope, null_coalescing_expression, predefined, out IInstance null_coalescing_value);
			if(null_coalescing_result.Failure) return null_coalescing_result;

			ExpressionContext[] expressions = conditional_expression.expression();
			if(expressions != null && expressions.Length > 0) {

				IInstance[] expression_values = new IInstance[2];
				ResultInfo[] expression_results = new ResultInfo[2];

				expression_results[0] = CompileExpression(function, scope, expressions[0], predefined, out expression_values[0]);
				if(expression_results[0].Failure) return expression_results[0];

				expression_results[1] = CompileExpression(function, scope, expressions[1], predefined, out expression_values[1]);
				if(expression_results[1].Failure) return expression_results[1];

				throw new NotImplementedException("Conditional expressions have not been implemented.");

			} else {

				value = null_coalescing_value;
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileNullCoalescingExpression(StandaloneStatementFunction function, Scope scope, NullCoalescingExpressionContext null_coalescing_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(null_coalescing_expression is null)
				throw new ArgumentNullException(nameof(null_coalescing_expression));
			#endregion

			value = null;

			ConditionalOrExpressionContext conditional_or_expression = null_coalescing_expression.conditional_or_expression();
			ResultInfo conditional_or_result = CompileConditionalOrExpression(function, scope, conditional_or_expression, predefined, out IInstance conditional_or_value);
			if(conditional_or_result.Failure) return conditional_or_result;

			NullCoalescingExpressionContext null_coalescing_expression_second = null_coalescing_expression.null_coalescing_expression();
			if(null_coalescing_expression_second != null) {

				value = null;

				ResultInfo null_coalescing_second_result = CompileNullCoalescingExpression(function, scope, null_coalescing_expression_second, predefined, out IInstance null_coalescing_second_value);
				if(null_coalescing_second_result.Failure) return null_coalescing_second_result;

				throw new NotImplementedException("Null-coalescing expressions have not been implemented.");

			} else {

				value = conditional_or_value;
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileConditionalOrExpression(StandaloneStatementFunction function, Scope scope, ConditionalOrExpressionContext conditional_or_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(conditional_or_expression is null)
				throw new ArgumentNullException(nameof(conditional_or_expression));
			#endregion

			ConditionalAndExpressionContext[] conditional_and_expressions = conditional_or_expression.conditional_and_expression();
			int count = conditional_and_expressions.Length;
			ResultInfo[] conditional_and_results = new ResultInfo[count];
			IInstance[] conditional_and_values = new IInstance[count];

			value = null;

			conditional_and_results[0] = CompileConditionalAndExpression(function, scope, conditional_and_expressions[0], predefined, out conditional_and_values[0]);
			if(conditional_and_results[0].Failure) return conditional_and_results[0];

			if(count == 2 && conditional_and_expressions[1] != null) {

				conditional_and_results[1] = CompileConditionalAndExpression(function, scope, conditional_and_expressions[1], predefined, out conditional_and_values[1]);
				if(conditional_and_results[1].Failure) return conditional_and_results[1];

				throw new NotImplementedException("Boolean OR expressions have not been implemented.");

			} else {

				value = conditional_and_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileConditionalAndExpression(StandaloneStatementFunction function, Scope scope, ConditionalAndExpressionContext conditional_and_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(conditional_and_expression is null)
				throw new ArgumentNullException(nameof(conditional_and_expression));
			#endregion

			InclusiveOrExpressionContext[] inclusive_or_expressions = conditional_and_expression.inclusive_or_expression();
			int count = inclusive_or_expressions.Length;
			ResultInfo[] inclusive_or_results = new ResultInfo[count];
			IInstance[] inclusive_or_values = new IInstance[count];

			value = null;

			inclusive_or_results[0] = CompileInclusiveOrExpression(function, scope, inclusive_or_expressions[0], predefined, out inclusive_or_values[0]);
			if(inclusive_or_results[0].Failure) return inclusive_or_results[0];

			if(count == 2 && inclusive_or_expressions[1] != null) {

				inclusive_or_results[1] = CompileInclusiveOrExpression(function, scope, inclusive_or_expressions[1], predefined, out inclusive_or_values[1]);
				if(inclusive_or_results[1].Failure) return inclusive_or_results[1];

				throw new NotImplementedException("Boolean AND expressions have not been implemented.");

			} else {

				value = inclusive_or_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileInclusiveOrExpression(StandaloneStatementFunction function, Scope scope, InclusiveOrExpressionContext inclusive_or_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(inclusive_or_expression is null)
				throw new ArgumentNullException(nameof(inclusive_or_expression));
			#endregion

			ExclusiveOrExpressionContext[] exclusive_or_expressions = inclusive_or_expression.exclusive_or_expression();
			int count = exclusive_or_expressions.Length;
			ResultInfo[] exclusive_or_results = new ResultInfo[count];
			IInstance[] exclusive_or_values = new IInstance[count];

			value = null;

			exclusive_or_results[0] = CompileExclusiveOrExpression(function, scope, exclusive_or_expressions[0], predefined, out exclusive_or_values[0]);
			if(exclusive_or_results[0].Failure) return exclusive_or_results[0];

			if(count == 2 && exclusive_or_expressions[1] != null) {

				exclusive_or_results[1] = CompileExclusiveOrExpression(function, scope, exclusive_or_expressions[1], predefined, out exclusive_or_values[1]);
				if(exclusive_or_results[1].Failure) return exclusive_or_results[1];

				throw new NotImplementedException("Bitwise XOR expressions have not been implemented.");

			} else {

				value = exclusive_or_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileExclusiveOrExpression(StandaloneStatementFunction function, Scope scope, ExclusiveOrExpressionContext exclusive_or_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(exclusive_or_expression is null)
				throw new ArgumentNullException(nameof(exclusive_or_expression));
			#endregion

			AndExpressionContext[] and_expressions = exclusive_or_expression.and_expression();
			int count = and_expressions.Length;
			ResultInfo[] and_results = new ResultInfo[count];
			IInstance[] and_values = new IInstance[count];

			value = null;

			and_results[0] = CompileAndExpression(function, scope, and_expressions[0], predefined, out and_values[0]);
			if(and_results[0].Failure) return and_results[0];

			if(count == 2 && and_expressions[1] != null) {

				and_results[1] = CompileAndExpression(function, scope, and_expressions[1], predefined, out and_values[1]);
				if(and_results[1].Failure) return and_results[1];

				throw new NotImplementedException("Bitwise OR expressions have not been implemented.");

			} else {

				value = and_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileAndExpression(StandaloneStatementFunction function, Scope scope, AndExpressionContext and_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(and_expression is null)
				throw new ArgumentNullException(nameof(and_expression));
			#endregion

			EqualityExpressionContext[] equality_expressions = and_expression.equality_expression();
			int count = equality_expressions.Length;
			ResultInfo[] equality_results = new ResultInfo[count];
			IInstance[] equality_values = new IInstance[count];

			value = null;

			equality_results[0] = CompileEqualityExpression(function, scope, equality_expressions[0], predefined, out equality_values[0]);
			if(equality_results[0].Failure) return equality_results[0];

			if(count == 2 && equality_expressions[1] != null) {

				equality_results[1] = CompileEqualityExpression(function, scope, equality_expressions[1], predefined, out equality_values[1]);
				if(equality_results[1].Failure) return equality_results[1];

				throw new NotImplementedException("Bitwise AND expressions have not been implemented.");

			} else {

				value = equality_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileEqualityExpression(StandaloneStatementFunction function, Scope scope, EqualityExpressionContext equality_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(equality_expression is null)
				throw new ArgumentNullException(nameof(equality_expression));
			#endregion

			RelationalExpressionContext[] relational_expressions = equality_expression.relational_expression();
			int count = relational_expressions.Length;
			ResultInfo[] relational_results = new ResultInfo[count];
			IInstance[] relational_values = new IInstance[count];

			value = null;

			relational_results[0] = CompileRelationalExpression(function, scope, relational_expressions[0], predefined, out relational_values[0]);
			if(relational_results[0].Failure) return relational_results[0];

			if(count == 2 && relational_expressions[1] != null) {

				relational_results[1] = CompileRelationalExpression(function, scope, relational_expressions[1], predefined, out relational_values[1]);
				if(relational_results[1].Failure) return relational_results[1];

				throw new NotImplementedException("Equality expressions have not been implemented.");

			} else {

				value = relational_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileRelationalExpression(StandaloneStatementFunction function, Scope scope, RelationalExpressionContext relational_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(relational_expression is null)
				throw new ArgumentNullException(nameof(relational_expression));
			#endregion

			value = null;

			ShiftExpressionContext shift_expression = relational_expression.shift_expression();
			ResultInfo shift_result = CompileShiftExpression(function, scope, shift_expression, predefined, out IInstance shift_value);
			if(shift_result.Failure) return shift_result;

			RelationOrTypeCheckContext[] relation_or_type_check = relational_expression.relation_or_type_check();

			if(relation_or_type_check.Length > 0) {
				throw new NotImplementedException("Relation/type check has not been implemented.");
			}

			value = shift_value;
			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileRelationOrTypeCheck(StandaloneStatementFunction function, Scope scope, RelationOrTypeCheckContext relation_or_type_check, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(relation_or_type_check is null)
				throw new ArgumentNullException(nameof(relation_or_type_check));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		public ResultInfo CompileShiftExpression(StandaloneStatementFunction function, Scope scope, ShiftExpressionContext shift_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(shift_expression is null)
				throw new ArgumentNullException(nameof(shift_expression));
			#endregion

			var additive_expressions = shift_expression.additive_expression();
			int count = additive_expressions.Length;
			ResultInfo[] additive_results = new ResultInfo[count];
			IInstance[] additive_values = new IInstance[count];

			value = null;

			additive_results[0] = CompileAdditiveExpression(function, scope, additive_expressions[0], predefined, out additive_values[0]);
			if(additive_results[0].Failure) return additive_results[0];

			if(count == 2 && additive_expressions[1] != null) {

				additive_results[1] = CompileAdditiveExpression(function, scope, additive_expressions[1], predefined, out additive_values[1]);
				if(additive_results[1].Failure) return additive_results[1];

				throw new NotImplementedException("Shift expressions have not been implemented.");

			} else {

				value = additive_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileAdditiveExpression(StandaloneStatementFunction function, Scope scope, AdditiveExpressionContext additive_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(additive_expression is null)
				throw new ArgumentNullException(nameof(additive_expression));
			#endregion

			MultiplicativeExpressionContext[] multiplicative_expressions = additive_expression.multiplicative_expression();
			var count = multiplicative_expressions.Length;
			ResultInfo[] multiplicative_results = new ResultInfo[count];
			IInstance[] multiplicative_values = new IInstance[count];

			value = null;

			multiplicative_results[0] = CompileMultiplicativeExpression(function, scope, multiplicative_expressions[0], predefined, out multiplicative_values[0]);
			if(multiplicative_results[0].Failure) return multiplicative_results[0];

			if(count == 2 && multiplicative_expressions[1] != null) {

				multiplicative_results[1] = CompileMultiplicativeExpression(function, scope, multiplicative_expressions[1], predefined, out multiplicative_values[1]);
				if(multiplicative_results[1].Failure) return multiplicative_results[1];

				throw new NotImplementedException("Additive expressions have not been implemented.");

			} else {

				value = multiplicative_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileMultiplicativeExpression(StandaloneStatementFunction function, Scope scope, MultiplicativeExpressionContext multiplicative_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(multiplicative_expression is null)
				throw new ArgumentNullException(nameof(multiplicative_expression));
			#endregion

			WithExpressionContext[] with_expressions = multiplicative_expression.with_expression();
			int count = with_expressions.Length;
			ResultInfo[] with_results = new ResultInfo[count];
			IInstance[] with_values = new IInstance[count];

			value = null;

			with_results[0] = CompileWithExpression(function, scope, with_expressions[0], predefined, out with_values[0]);
			if(with_results[0].Failure) return with_results[0];

			if(count == 2 && with_expressions[1] != null) {

				with_results[1] = CompileWithExpression(function, scope, with_expressions[1], predefined, out with_values[1]);
				if(with_results[1].Failure) return with_results[1];

				throw new NotImplementedException("Multiplicative expressions have not been implemented.");

			} else {

				value = with_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileWithExpression(StandaloneStatementFunction function, Scope scope, WithExpressionContext with_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(with_expression is null)
				throw new ArgumentNullException(nameof(with_expression));
			#endregion

			value = null;

			RangeExpressionContext range_expression = with_expression.range_expression();
			ResultInfo range_result = CompileRangeExpression(function, scope, range_expression, predefined, out IInstance range_value);
			if(range_result.Failure) return range_result;

			MCSharpParser.Anonymous_element_initializerContext anonymous_element_initializer = with_expression.anonymous_element_initializer();
			if(anonymous_element_initializer != null) {

				throw new NotImplementedException("With epxressions have not been implemented.");

			}

			value = range_value;
			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileRangeExpression(StandaloneStatementFunction function, Scope scope, RangeExpressionContext range_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(range_expression is null)
				throw new ArgumentNullException(nameof(range_expression));
			#endregion

			UnaryExpressionContext[] unary_expressions = range_expression.unary_expression();
			int count = unary_expressions.Length;
			ResultInfo[] unary_results = new ResultInfo[count];
			IInstance[] unary_values = new IInstance[count];

			value = null;

			unary_results[0] = CompileUnaryExpression(function, scope, unary_expressions[0], predefined, out unary_values[0]);
			if(unary_results[0].Failure) return unary_results[0];

			if(count == 2 && unary_expressions[1] != null) {

				unary_results[1] = CompileUnaryExpression(function, scope, unary_expressions[1], predefined, out unary_values[1]);
				if(unary_results[1].Failure) return unary_results[1];

				throw new NotImplementedException("Range expressions have been been implemented.");

			} else {

				value = unary_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompilePreStepExpression(StandaloneStatementFunction function, Scope scope, PreStepExpressionContext pre_step_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(pre_step_expression is null)
				throw new ArgumentNullException(nameof(pre_step_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		public ResultInfo CompilePostStepExpression(StandaloneStatementFunction function, Scope scope, PostStepExpressionContext post_step_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(post_step_expression is null)
				throw new ArgumentNullException(nameof(post_step_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		public ResultInfo CompileUnaryExpression(StandaloneStatementFunction function, Scope scope, UnaryExpressionContext unary_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(unary_expression is null)
				throw new ArgumentNullException(nameof(unary_expression));
			#endregion

			PrimaryExpressionContext primary_expression = unary_expression.primary_expression();
			if(primary_expression != null) {

				ResultInfo result = CompilePrimaryExpression(function, scope, primary_expression, predefined, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			UnaryExpressionContext subunary_expression = unary_expression.unary_expression();
			if(subunary_expression != null) {

				value = null;

				ResultInfo result = CompileUnaryExpression(function, scope, subunary_expression, predefined, out IInstance unary_value);
				if(result.Failure) return result;

				throw new NotImplementedException("Unary expressions have not been implemented.");

			}

			PreStepExpressionContext pre_step_expression = unary_expression.pre_step_expression();
			if(pre_step_expression != null) {

				ResultInfo result = CompilePreStepExpression(function, scope, pre_step_expression, predefined, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			CastExpressionContext cast_expression = unary_expression.cast_expression();
			if(cast_expression != null) {

				ResultInfo result = CompileCastExpression(function, scope, cast_expression, predefined, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			PointerIndirectionExpressionContext pointer_indirection_expression = unary_expression.pointer_indirection_expression();
			if(pointer_indirection_expression != null) {

				ResultInfo result = CompilePointerIndirectionExpression(function, scope, pointer_indirection_expression, predefined, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			AddressofExpressionContext addressof_expression = unary_expression.addressof_expression();
			if(addressof_expression != null) {

				ResultInfo result = CompileAddressofExpression(function, scope, addressof_expression, predefined, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Unary expression could not be determined.");

		}

		public ResultInfo CompileCastExpression(StandaloneStatementFunction function, Scope scope, CastExpressionContext cast_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(cast_expression is null)
				throw new ArgumentNullException(nameof(cast_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		public ResultInfo CompilePointerIndirectionExpression(StandaloneStatementFunction function, Scope scope, PointerIndirectionExpressionContext pointer_indirection_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(pointer_indirection_expression is null)
				throw new ArgumentNullException(nameof(pointer_indirection_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		public ResultInfo CompileAddressofExpression(StandaloneStatementFunction function, Scope scope, AddressofExpressionContext addressof_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(addressof_expression is null)
				throw new ArgumentNullException(nameof(addressof_expression));
			#endregion

			value = null;

			UnaryExpressionContext unary_expression = addressof_expression.unary_expression();
			ResultInfo unary_result = CompileUnaryExpression(function, scope, unary_expression, predefined, out IInstance unary_value);
			if(unary_result.Failure) return unary_result;

			throw new NotImplementedException("Addressof expressions have not been implemented.");

		}

		public ResultInfo CompileAssignmentExpression(StandaloneStatementFunction function, Scope scope, AssignmentExpressionContext assignment_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(assignment_expression is null)
				throw new ArgumentNullException(nameof(assignment_expression));
			#endregion

			value = null;

			UnaryExpressionContext unary_expression = assignment_expression.unary_expression();
			ResultInfo unary_result = CompileUnaryExpression(function, scope, unary_expression, predefined, out IInstance unary_value);
			if(unary_result.Failure) return unary_result;

			ExpressionContext expression = assignment_expression.expression();
			ResultInfo expression_result = CompileExpression(function, scope, expression, predefined, out IInstance expression_value);
			if(expression_result.Failure) return expression_result;

			throw new NotImplementedException("Assignment expression evaluation has not been implemented.");

		}

		public ResultInfo CompilePrimaryExpression(StandaloneStatementFunction function, Scope scope, PrimaryExpressionContext primary_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(primary_expression is null)
				throw new ArgumentNullException(nameof(primary_expression));
			#endregion

			value = null;

			ArrayCreationExpressionContext array_creation_expression = primary_expression.array_creation_expression();
			if(array_creation_expression != null) {

				ResultInfo result = CompileArrayCreationExpression(function, scope, array_creation_expression, predefined, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			PrimaryNoArrayCreationExpressionContext primary_no_array_creation_expression = primary_expression.primary_no_array_creation_expression();
			if(primary_no_array_creation_expression != null) {

				ResultInfo result = CompilePrimaryNoArrayCreationExpression(function, scope, primary_no_array_creation_expression, predefined, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Primary expression could not be determined.");

		}

		public ResultInfo CompileArrayCreationExpression(StandaloneStatementFunction function, Scope scope,
		ArrayCreationExpressionContext array_creation_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(array_creation_expression is null)
				throw new ArgumentNullException(nameof(array_creation_expression));
			#endregion

			throw new NotImplementedException("Array-creation expression evaluation has not been implented.");

		}

		public ResultInfo CompilePrimaryNoArrayCreationExpression(StandaloneStatementFunction function, Scope scope,
		PrimaryNoArrayCreationExpressionContext primary_no_array_creation_expression, bool predefined, out IInstance value) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(primary_no_array_creation_expression is null)
				throw new ArgumentNullException(nameof(primary_no_array_creation_expression));
			#endregion

			MCSharpParser.LiteralContext literal = primary_no_array_creation_expression.literal();
			if(literal != null) {

				ITerminalNode integer_literal = literal.INTEGER();
				if(integer_literal != null) {

					string text = integer_literal.GetText();
					int integer_value = int.Parse(text);

					IType integer_type = DefinedTypes[MCSharpLinkerExtension.IntIdentifier];
					ITerminalNode anon_identifier = scope.MakeAnonymousInstanceName();
					value = new PrimitiveInstance.IntegerInstance.Constant(integer_type, anon_identifier, integer_value);
					scope.AddInstance(value);

					return ResultInfo.DefaultSuccess;

				}

				ITerminalNode boolean_literal = literal.BOOLEAN();
				if(boolean_literal != null) {

					string text = boolean_literal.GetText();
					bool boolean_value = bool.Parse(text);

					IType boolean_type = DefinedTypes[MCSharpLinkerExtension.BoolIdentifier];
					ITerminalNode anon_identifier = scope.MakeAnonymousInstanceName();
					value = new PrimitiveInstance.BooleanInstance.Constant(boolean_type, anon_identifier, boolean_value);
					scope.AddInstance(value);

					return ResultInfo.DefaultSuccess;

				}

				ITerminalNode string_literal = literal.STRING();
				if(string_literal != null) {

					string text = string_literal.GetText();
					string string_value = text[1..^1];

					IType string_type = DefinedTypes[MCSharpLinkerExtension.StringIdentifier];
					ITerminalNode anon_identifier = scope.MakeAnonymousInstanceName();

					throw new NotImplementedException("String literals have not been implemented.");

				}

				ITerminalNode decimal_literal = literal.DECIMAL();
				if(decimal_literal != null) {

					string text = decimal_literal.GetText();
					double decimal_value = double.Parse(text);

					// type
					ITerminalNode anon_identifier = scope.MakeAnonymousInstanceName();

					throw new NotImplementedException("Decimal/double/floats literals have not been implemented.");

				}

				throw new NotImplementedException("Literal context could not be determined.");

			}

			MCSharpParser.IdentifierContext identifier = primary_no_array_creation_expression.identifier();
			if(identifier != null) {

				throw new NotImplementedException("Identifier evaluation have not been implemented.");

			}

			ExpressionContext expression = primary_no_array_creation_expression.expression();
			if(expression != null) {

				ResultInfo result = CompileExpression(function, scope, expression, predefined, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			MCSharpParser.Member_accessContext member_access = primary_no_array_creation_expression.member_access();
			if(member_access != null) {

				throw new NotImplementedException("Member access evaluation have not been implemented.");

			}

			PostStepExpressionContext post_step_expression = primary_no_array_creation_expression.post_step_expression();
			if(post_step_expression != null) {

				throw new NotImplementedException("Post-step expression evaluation have not been implemented.");

			}

			KeywordExpressionContext keyword_expression = primary_no_array_creation_expression.keyword_expression();
			if(keyword_expression != null) {

				throw new NotImplementedException("Keyword expression evaluation have not been implemented.");

			}

			throw new Exception("Primary no-array-creation expression could not be determined.");

		}

		#endregion

		#endregion

		#region IStatement Creation

		private IStatement CreateIStatement(MCSharpParser.StatementContext statement, bool predefined) {

			#region Argument Checks
			if(statement is null)
				throw new ArgumentNullException(nameof(statement));
			#endregion

			if(predefined) {
				return new ScriptStatement(statement);
			} else {
				return new PredefinedStatement(statement);
			}

		}

		private IStatement[] CreateIStatements(MCSharpParser.StatementContext[] statements, bool predefined) {

			#region Argument Checks
			if(statements is null)
				throw new ArgumentNullException(nameof(statements));
			#endregion

			int length = statements.Length;
			IStatement[] array = new IStatement[length];

			if(predefined) {
				for(int i = 0; i < length; i++) {
					array[i] = new PredefinedStatement(statements[i]);
				}
			} else {
				for(int i = 0; i < length; i++) {
					array[i] = new PredefinedStatement(statements[i]);
				}
			}

			return array;

		}

		private IStatement[] CreateIStatements(MCSharpParser.StatementContext statement, bool predefined) {

			#region Argument Checks
			if(statement is null)
				throw new ArgumentNullException(nameof(statement));
			#endregion

			if(predefined) {
				return new IStatement[] { new ScriptStatement(statement) };
			} else {
				return new IStatement[] { new PredefinedStatement(statement) };
			}

		}

		#endregion

		public void Dispose() {

			foreach(var type in DefinedTypes.Values) {
				type.Dispose();
			}

		}


		public struct CompileArguments {

			public FunctionWriter Writer { get; }

			public Scope Scope { get; }

			public bool Predefined { get; }

			public CompileArguments(FunctionWriter writer, Scope scope, bool predefined) {
				Writer = writer ?? throw new ArgumentNullException(nameof(writer));
				Scope = scope ?? throw new ArgumentNullException(nameof(scope));
				Predefined = predefined;
			}
		}

		public class ScriptClassWalker : MCSharpBaseListener {

			Compiler Compiler { get; }

			private TypeDefinitionContext CurrentTypeContext { get; set; }
			private ICollection<MemberDefinitionContext> CurrentMemberContexts { get; set; } = new LinkedList<MemberDefinitionContext>();
			private ICollection<ConstructorDefinitionContext> CurrentConstructorContexts { get; set; } = new LinkedList<ConstructorDefinitionContext>();

			public ScriptClassWalker(Compiler compiler) {
				Compiler = compiler;
			}

			public override void EnterType_definition([NotNull] TypeDefinitionContext context) {
				CurrentMemberContexts.Clear();
				CurrentTypeContext = context;
			}

			public override void EnterMember_definition([NotNull] MemberDefinitionContext context) {
				CurrentMemberContexts.Add(context);
			}

			public override void EnterConstructor_definition([NotNull] ConstructorDefinitionContext context) {
				CurrentConstructorContexts.Add(context);
			}

			public override void ExitType_definition([NotNull] TypeDefinitionContext context) {
				if(context != CurrentTypeContext) throw new Exception($"Subtypes are currently not supported by {nameof(ScriptClassWalker)}.");
				var scriptType = new ScriptType(CurrentTypeContext, CurrentMemberContexts.ToArray(), CurrentConstructorContexts.ToArray(), Compiler.Settings, Compiler.VirtualMachine);
				Compiler.DefinedTypes.Add(scriptType.Identifier.GetText(), scriptType);
			}

		}

		public class ScriptErrorListener : IAntlrErrorListener<IToken>, IAntlrErrorListener<int> {

			public ResultInfo Result { get; private set; }
			string Source { get; }

			public ScriptErrorListener(string source) {
				Result = ResultInfo.DefaultSuccess;
				Source = source;
			}

			void IAntlrErrorListener<IToken>.SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {
				string message = $"{Source} line {line}:{charPositionInLine} {msg}";
				Result = new ResultInfo(false, message);
			}

			void IAntlrErrorListener<int>.SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {
				string message = $"{Source} line {line}:{charPositionInLine} {msg}";
				Result = new ResultInfo(false, message);
			}

		}

	}

}
