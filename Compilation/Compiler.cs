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


			// Find linker extensions.
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

				// Prepare threads.
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
			Scope rootScope = new Scope(null, null, null);
			foreach(IType type in DefinedTypes.Values) {

				if(type.Identifier != "Program") continue;

				Scope typeScope = new Scope(type.Identifier, rootScope, type);
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

		public ResultInfo CompileStandaloneMethod(Scope typeScope, IMember member) {

			#region Argument Checks
			if(typeScope is null)
				throw new ArgumentNullException(nameof(typeScope));
			if(member is null)
				throw new ArgumentNullException(nameof(member));
			if(member.MemberType != MemberType.Method)
				throw new ArgumentOutOfRangeException(nameof(member), $"The given {member.MemberType} is not {nameof(MemberType.Method)}.");
			#endregion

			Scope methodScope = new Scope(member.Identifier, typeScope, member);
			IMethod method = member.Definition as IMethod;
			StandaloneStatementFunction invoker = method.Invoker as StandaloneStatementFunction;
			invoker.Compiled = true;
			return CompileStatements(invoker, methodScope, invoker.Statements);

		}

		#endregion

		#region Statement Compilation

		public ResultInfo CompileStatements(StandaloneStatementFunction function, Scope scope, ICollection<IStatement> statements) {

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

					ResultInfo result = CompileStatement(new CompileArguments(this, function, scope, false), statement.Context);
					if(result.Failure) return result;

				} else if(statement is PredefinedStatement) {

					ResultInfo result = CompileStatement(new CompileArguments(this, function, scope, true), statement.Context);
					// TODO: Send exception and say this is a bug if result is failed.
					if(result.Failure) return result;

				} else {

					// todo: Add more information.
					throw new Exception();

				}

			}

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileStatement(CompileArguments location, StatementContext context) {

			#region Argument Checks
			if(context is null)
				throw new ArgumentNullException(nameof(context));
			#endregion

			CodeBlockContext code_block = context.code_block();
			if(code_block != null) {

				StatementContext[] statements = code_block.statement();
				return CompileStatements(location.Function, location.Scope, CreateIStatements(statements, location.Predefined));

			}


			LanguageFunctionContext language_function = context.language_function();
			if(language_function != null) {

				IfStatementContext if_statement = language_function.if_statement();
				if(if_statement != null) {

					ExpressionContext condition = if_statement.expression();
					StatementContext[] if_stmts = if_statement.statement();
					StatementContext statement1 = if_stmts[0];
					StatementContext statement2 = if_stmts.Length > 1 ? if_stmts[1] : null;

					ResultInfo conditionResult = CompileExpression(location, condition, out IInstance value);
					if(conditionResult.Failure) return conditionResult;

					Scope statement1Scope = new Scope(null, location.Scope, null);
					StandaloneStatementFunction statement1Function = location.Function.CreateChildFunction(CreateIStatements(statement1, location.Predefined), Settings);
					CompileArguments statement1Location = new CompileArguments(location.Compiler, statement1Function, statement1Scope, location.Predefined);

					ResultInfo statement1Result = CompileStatement(statement1Location, statement1);
					if(statement1Result.Failure) return statement1Result;

					if(value is PrimitiveInstance.BooleanInstance valueBool) {

						location.Function.Writer.WriteCommand($"execute if score {MCSharpLinkerExtension.StorageSelector} {valueBool.Objective.Name} matches 1.. " +
							$"run function {statement1Function.Writer.GamePath}");

						if(statement2 != null) {

							Scope statement2Scope = new Scope(null, location.Scope, null);
							StandaloneStatementFunction statement2Function = location.Function.CreateChildFunction(CreateIStatements(statement2, location.Predefined), Settings);
							CompileArguments statement2Location = new CompileArguments(location.Compiler, statement2Function, statement2Scope, location.Predefined);

							ResultInfo statement2Result = CompileStatement(statement2Location, statement2);
							if(statement2Result.Failure) return statement2Result;

							location.Function.Writer.WriteCommand($"execute if score {MCSharpLinkerExtension.StorageSelector} {valueBool.Objective.Name} matches ..0 " +
								$"run function {statement2Function.Writer.GamePath}");

						}

						return ResultInfo.DefaultSuccess;

					} else {

						throw new NotImplementedException("Casting has not been implemented.");

					}

				}

				ForStatementContext for_statement = language_function.for_statement();
				if(for_statement != null) {

					InitializationExpressionContext for_initialization_expression = for_statement.initialization_expression();
					ExpressionContext[] for_expressions = for_statement.expression();
					ExpressionContext condition = for_expressions[0];
					ExpressionContext increment = for_expressions[1];
					StatementContext statement = for_statement.statement();

					Scope scopeOuter = new Scope(null, location.Scope, null);
					CompileArguments locationOuter = new CompileArguments(location.Compiler, location.Function, scopeOuter, location.Predefined);

					Scope scopeInner = new Scope(null, scopeOuter, null);
					StandaloneStatementFunction statementFunction = locationOuter.Function.CreateChildFunction(CreateIStatements(statement, locationOuter.Predefined), Settings);
					CompileArguments locationInner = new CompileArguments(locationOuter.Compiler, statementFunction, scopeInner, locationOuter.Predefined);

					ResultInfo initializationResult = CompileInitializationExpression(locationOuter, for_initialization_expression, out _);
					if(initializationResult.Failure) return initializationResult;

					ResultInfo conditionResult1 = CompileExpression(locationOuter, condition, out IInstance conditionValue1);
					if(conditionResult1.Failure) return conditionResult1;

					if(conditionValue1 is PrimitiveInstance.BooleanInstance conditionValue1Bool) {

						locationOuter.Function.Writer.WriteCommand($"execute if score {MCSharpLinkerExtension.StorageSelector} {conditionValue1Bool.Objective.Name} matches 1.. " +
							$"run function {statementFunction.Writer.GamePath}");

					} else {

						throw new NotImplementedException("Casting has not been implemented.");

					}

					ResultInfo statementResult = CompileStatement(locationInner, statement);
					if(statementResult.Failure) return statementResult;

					ResultInfo incrementResult = CompileExpression(locationInner, increment, out _);
					if(incrementResult.Failure) return incrementResult;

					ResultInfo conditionResult2 = CompileExpression(locationInner, condition, out IInstance conditionValue2);
					if(conditionResult2.Failure) return conditionResult2;

					if(conditionValue2 is PrimitiveInstance.BooleanInstance conditionValue2Bool) {

						locationInner.Function.Writer.WriteCommand($"execute if score {MCSharpLinkerExtension.StorageSelector} {conditionValue2Bool.Objective.Name} matches 1.. " +
							$"run function {statementFunction.Writer.GamePath}");

					} else {

						throw new NotImplementedException("Casting has not been implemented.");

					}

					return ResultInfo.DefaultSuccess;

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

				return CompileInitializationExpression(location, initialization_expression, out _);

			}


			ExpressionContext expression = context.expression();
			if(expression != null) {

				ResultInfo expression_result = CompileExpression(location, expression, out _);
				if(expression_result.Failure) return expression_result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Statement context could not be determined.");

		}

		public ResultInfo CompileInitializationExpression(CompileArguments location, InitializationExpressionContext initialization_expression, out IInstance value) {

			ITerminalNode[] names = initialization_expression.NAME();
			ITerminalNode typeName = names[0];
			ITerminalNode identifier = names[1];

			IType type = DefinedTypes[typeName.GetText()];
			value = type.InitializeInstance(location, identifier.GetText());

			ExpressionContext expression = initialization_expression.expression();
			if(expression != null) {

				ResultInfo expression_result = CompileExpression(location, expression, out IInstance assignment_value);
				if(expression_result.Failure) return expression_result;

				ResultInfo assign_result = CompileSimpleOperation(location, Operation.Assign, value, assignment_value, out _);
				if(assign_result.Failure) return location.GetLocation(initialization_expression.ASSIGN()) + assign_result;

				return ResultInfo.DefaultSuccess;

			}

			return ResultInfo.DefaultSuccess;

		}

		#endregion

		#region Expression Compilation

		public ResultInfo CompileSimpleOperation(CompileArguments compile, Operation op, IInstance operand1, IInstance operand2, out IInstance result) {

			IType type1 = operand1.Type;
			IType type2 = operand2.Type;

			ICollection<IOperation> matches = new List<IOperation>(2);

			// Find matches from type 1.
			if(type1.Operations.ContainsKey(op)) {
				HashSet<IOperation> operations = type1.Operations[op];
				foreach(IOperation operation in operations) {
					IReadOnlyList<IMethodParameter> parameters = operation.Function.MethodParameters;
					if(parameters.Count != 2)
						throw new InvalidOperationException(
							$"{nameof(CompileSimpleOperation)} was called on {op} expecting exactly 2 parameters, " +
							$"but found {parameters.Count} instead from {type1.Identifier}.");
					if(parameters[0].TypeIdentifier == type1.Identifier && parameters[1].TypeIdentifier == type2.Identifier) {
						matches.Add(operation);
					}
				}
			}

			// Find matches from type 2.
			if(type2 != type1 && type2.Operations.ContainsKey(op)) {
				HashSet<IOperation> operations = type2.Operations[op];
				foreach(IOperation operation in operations) {
					IReadOnlyList<IMethodParameter> parameters = operation.Function.MethodParameters;
					if(parameters.Count != 2)
						throw new InvalidOperationException(
							$"{nameof(CompileSimpleOperation)} was called on {operation} expecting exactly 2 parameters, " +
							$"but found {parameters.Count} instead from {type2.Identifier}.");
					if(parameters[0].TypeIdentifier == type1.Identifier && parameters[1].TypeIdentifier == type2.Identifier) {
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
				ResultInfo functionResult = operation.Function.Invoke(compile, new IType[] { }, new IInstance[] { operand1, operand2 }, out result);
				if(functionResult.Failure) return functionResult;

				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileExpression(CompileArguments compile, ExpressionContext expression, out IInstance value) {

			#region Argument Checks
			if(expression is null)
				throw new ArgumentNullException(nameof(expression));
			#endregion

			NonAssignmentExpressionContext non_assignment_expression = expression.non_assignment_expression();
			if(non_assignment_expression != null) {

				ResultInfo non_assignment_result = CompileNonAssignmentExpression(compile, non_assignment_expression, out value);
				if(non_assignment_result.Failure) return non_assignment_result;

				return ResultInfo.DefaultSuccess;

			}

			AssignmentExpressionContext assignment_expression = expression.assignment_expression();
			if(assignment_expression != null) {

				ResultInfo assignment_result = CompileAssignmentExpression(compile, assignment_expression, out value);
				if(assignment_result.Failure) return assignment_result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Expression context could not be determined.");

		}

		public ResultInfo CompileNonAssignmentExpression(CompileArguments compile, NonAssignmentExpressionContext non_assignment_expression, out IInstance value) {

			#region Argument Checks
			if(non_assignment_expression is null)
				throw new ArgumentNullException(nameof(non_assignment_expression));
			#endregion

			ConditionalExpressionContext conditional_expression = non_assignment_expression.conditional_expression();
			if(conditional_expression != null) {

				ResultInfo conditional_result = CompileConditionalExpression(compile, conditional_expression, out value);
				if(conditional_result.Failure) return conditional_result;

				return ResultInfo.DefaultSuccess;

			}

			LambdaExpressionContext lambda_expression = non_assignment_expression.lambda_expression();
			if(lambda_expression != null) {

				ResultInfo lambda_result = CompileLambdaExpression(compile, lambda_expression, out value);
				if(lambda_result.Failure) return lambda_result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Non-assignment expression context could not be determined.");

		}

		public ResultInfo CompileLambdaExpression(CompileArguments compile, LambdaExpressionContext lambda_expression, out IInstance value) {

			#region Argument Checks
			if(lambda_expression is null)
				throw new ArgumentNullException(nameof(lambda_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		public ResultInfo CompileConditionalExpression(CompileArguments compile, ConditionalExpressionContext conditional_expression, out IInstance value) {

			#region Argument Checks
			if(conditional_expression is null)
				throw new ArgumentNullException(nameof(conditional_expression));
			#endregion

			value = null;

			NullCoalescingExpressionContext null_coalescing_expression = conditional_expression.null_coalescing_expression();
			ResultInfo null_coalescing_result = CompileNullCoalescingExpression(compile, null_coalescing_expression, out IInstance null_coalescing_value);
			if(null_coalescing_result.Failure) return null_coalescing_result;

			ExpressionContext[] expressions = conditional_expression.expression();
			if(expressions != null && expressions.Length > 0) {

				IInstance[] expression_values = new IInstance[2];
				ResultInfo[] expression_results = new ResultInfo[2];

				expression_results[0] = CompileExpression(compile, expressions[0], out expression_values[0]);
				if(expression_results[0].Failure) return expression_results[0];

				expression_results[1] = CompileExpression(compile, expressions[1], out expression_values[1]);
				if(expression_results[1].Failure) return expression_results[1];

				//var condition = DefinedTypes[MCSharpLinkerExtension.BoolIdentifier].InitializeInstance(compile, null) as PrimitiveInstance.BooleanInstance;


				throw new NotImplementedException("Conditional expressions have not been implemented.");

			} else {

				value = null_coalescing_value;
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileNullCoalescingExpression(CompileArguments compile, NullCoalescingExpressionContext null_coalescing_expression, out IInstance value) {

			#region Argument Checks
			if(null_coalescing_expression is null)
				throw new ArgumentNullException(nameof(null_coalescing_expression));
			#endregion

			value = null;

			ConditionalOrExpressionContext conditional_or_expression = null_coalescing_expression.conditional_or_expression();
			ResultInfo conditional_or_result = CompileConditionalOrExpression(compile, conditional_or_expression, out IInstance conditional_or_value);
			if(conditional_or_result.Failure) return conditional_or_result;

			NullCoalescingExpressionContext null_coalescing_expression_second = null_coalescing_expression.null_coalescing_expression();
			if(null_coalescing_expression_second != null) {

				value = null;

				ResultInfo null_coalescing_second_result = CompileNullCoalescingExpression(compile, null_coalescing_expression_second, out IInstance null_coalescing_second_value);
				if(null_coalescing_second_result.Failure) return null_coalescing_second_result;

				throw new NotImplementedException("Null-coalescing expressions have not been implemented.");

			} else {

				value = conditional_or_value;
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileConditionalOrExpression(CompileArguments compile, ConditionalOrExpressionContext conditional_or_expression, out IInstance value) {

			#region Argument Checks
			if(conditional_or_expression is null)
				throw new ArgumentNullException(nameof(conditional_or_expression));
			#endregion

			ConditionalAndExpressionContext[] expressions = conditional_or_expression.conditional_and_expression();
			ITerminalNode[] operators = conditional_or_expression.BOOLEAN_OR();

			ResultInfo firstResult = CompileConditionalAndExpression(compile, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			// TODO: Stop when any instance can evaluate to 'true'.

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileConditionalAndExpression(compile, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				ResultInfo opResult = CompileSimpleOperation(compile, Operation.BooleanOR, value, expressionValue, out value);
				if(opResult.Failure) return compile.GetLocation(operators[i - 1]) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileConditionalAndExpression(CompileArguments compile, ConditionalAndExpressionContext conditional_and_expression, out IInstance value) {

			#region Argument Checks
			if(conditional_and_expression is null)
				throw new ArgumentNullException(nameof(conditional_and_expression));
			#endregion

			InclusiveOrExpressionContext[] expressions = conditional_and_expression.inclusive_or_expression();
			ITerminalNode[] operators = conditional_and_expression.BOOLEAN_AND();

			ResultInfo firstResult = CompileInclusiveOrExpression(compile, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			// TODO: Stop when any instance can evaluate to 'false'.

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileInclusiveOrExpression(compile, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				ResultInfo opResult = CompileSimpleOperation(compile, Operation.BooleanAND, value, expressionValue, out value);
				if(opResult.Failure) return compile.GetLocation(operators[i - 1]) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileInclusiveOrExpression(CompileArguments compile, InclusiveOrExpressionContext inclusive_or_expression, out IInstance value) {

			#region Argument Checks
			if(inclusive_or_expression is null)
				throw new ArgumentNullException(nameof(inclusive_or_expression));
			#endregion

			ExclusiveOrExpressionContext[] expressions = inclusive_or_expression.exclusive_or_expression();
			ITerminalNode[] operators = inclusive_or_expression.BITWISE_OR();

			ResultInfo firstResult = CompileExclusiveOrExpression(compile, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileExclusiveOrExpression(compile, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				ResultInfo opResult = CompileSimpleOperation(compile, Operation.BitwiseOR, value, expressionValue, out value);
				if(opResult.Failure) return compile.GetLocation(operators[i - 1]) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileExclusiveOrExpression(CompileArguments compile, ExclusiveOrExpressionContext exclusive_or_expression, out IInstance value) {

			#region Argument Checks
			if(exclusive_or_expression is null)
				throw new ArgumentNullException(nameof(exclusive_or_expression));
			#endregion

			AndExpressionContext[] expressions = exclusive_or_expression.and_expression();
			ITerminalNode[] operators = exclusive_or_expression.BITWISE_XOR();

			ResultInfo firstResult = CompileAndExpression(compile, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileAndExpression(compile, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				ResultInfo opResult = CompileSimpleOperation(compile, Operation.BitwiseXOR, value, expressionValue, out value);
				if(opResult.Failure) return compile.GetLocation(operators[i - 1]) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileAndExpression(CompileArguments compile, AndExpressionContext and_expression, out IInstance value) {

			#region Argument Checks
			if(and_expression is null)
				throw new ArgumentNullException(nameof(and_expression));
			#endregion

			EqualityExpressionContext[] expressions = and_expression.equality_expression();
			ITerminalNode[] operators = and_expression.BITWISE_AND();

			ResultInfo firstResult = CompileEqualityExpression(compile, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileEqualityExpression(compile, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				ResultInfo opResult = CompileSimpleOperation(compile, Operation.BitwiseAND, value, expressionValue, out value);
				if(opResult.Failure) return compile.GetLocation(operators[i - 1]) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileEqualityExpression(CompileArguments compile, EqualityExpressionContext equality_expression, out IInstance value) {

			#region Argument Checks
			if(equality_expression is null)
				throw new ArgumentNullException(nameof(equality_expression));
			#endregion

			RelationalExpressionContext[] expressions = equality_expression.relational_expression();
			MCSharpParser.Equality_operatorContext[] operators = equality_expression.equality_operator();

			ResultInfo firstResult = CompileRelationalExpression(compile, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileRelationalExpression(compile, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				Operation op;
				MCSharpParser.Equality_operatorContext equality_operator = operators[i - 1];
				if(equality_operator.EQUIVALENT() != null) op = Operation.Equality;
				else op = Operation.Inequality;

				ResultInfo opResult = CompileSimpleOperation(compile, op, value, expressionValue, out value);
				if(opResult.Failure) return compile.GetLocation(equality_operator) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileRelationalExpression(CompileArguments compile, RelationalExpressionContext relational_expression, out IInstance value) {

			#region Argument Checks
			if(relational_expression is null)
				throw new ArgumentNullException(nameof(relational_expression));
			#endregion

			ResultInfo shift_result = CompileShiftExpression(compile, relational_expression.shift_expression(), out value);
			if(shift_result.Failure) { value = null; return shift_result; }

			RelationOrTypeCheckContext[] relation_or_type_checks = relational_expression.relation_or_type_check();
			int count = relation_or_type_checks?.Length ?? 0;
			for(int i = 0; i < count; i++) {

				ResultInfo rotcResult = CompileRelationOrTypeCheck(compile, value, relation_or_type_checks[i], out value);
				if(rotcResult.Failure) return rotcResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileRelationOrTypeCheck(CompileArguments compile, IInstance operandLeft, RelationOrTypeCheckContext relation_or_type_check, out IInstance value) {

			#region Argument Checks
			if(relation_or_type_check is null)
				throw new ArgumentNullException(nameof(relation_or_type_check));
			#endregion

			MCSharpParser.Relation_operatorContext relation_operator = relation_or_type_check.relation_operator();
			if(relation_operator != null) {

				ShiftExpressionContext shift_expression = relation_or_type_check.shift_expression();
				ResultInfo expressionResult = CompileShiftExpression(compile, shift_expression, out IInstance operandRight);
				if(expressionResult.Failure) { value = null; return expressionResult; }

				Operation op;
				if(relation_operator.LESS_THAN() != null) op = Operation.LessThan;
				else if(relation_operator.GREATER_THAN() != null) op = Operation.GreaterThan;
				else if(relation_operator.LESS_THAN_EQUAL() != null) op = Operation.LessThanOrEqual;
				else op = Operation.GreaterThanOrEqual;

				ResultInfo relationResult = CompileSimpleOperation(compile, op, operandLeft, operandRight, out value);
				if(relationResult.Failure) return compile.GetLocation(relation_or_type_check) + relationResult;

				return ResultInfo.DefaultSuccess;

			}

			bool type_check_is = relation_or_type_check.IS() != null;
			bool type_check_as = relation_or_type_check.AS() != null;

			if(type_check_is) {

				value = null;
				return new ResultInfo(false, "'is' operations have not been implemented.");

			}

			if(type_check_as) {

				value = null;
				return new ResultInfo(false, "'as' operations have not been implemented.");

			}

			throw new Exception("Relation-or-type-check could not be determined.");

		}

		public ResultInfo CompileShiftExpression(CompileArguments compile, ShiftExpressionContext shift_expression, out IInstance value) {

			#region Argument Checks
			if(shift_expression is null)
				throw new ArgumentNullException(nameof(shift_expression));
			#endregion

			var additive_expressions = shift_expression.additive_expression();
			int count = additive_expressions.Length;
			ResultInfo[] additive_results = new ResultInfo[count];
			IInstance[] additive_values = new IInstance[count];

			value = null;

			additive_results[0] = CompileAdditiveExpression(compile, additive_expressions[0], out additive_values[0]);
			if(additive_results[0].Failure) return additive_results[0];

			if(count == 2 && additive_expressions[1] != null) {

				additive_results[1] = CompileAdditiveExpression(compile, additive_expressions[1], out additive_values[1]);
				if(additive_results[1].Failure) return additive_results[1];

				throw new NotImplementedException("Shift expressions have not been implemented.");

			} else {

				value = additive_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompileAdditiveExpression(CompileArguments compile, AdditiveExpressionContext additive_expression, out IInstance value) {

			#region Argument Checks
			if(additive_expression is null)
				throw new ArgumentNullException(nameof(additive_expression));
			#endregion

			MultiplicativeExpressionContext[] expressions = additive_expression.multiplicative_expression();
			MCSharpParser.Additive_operatorContext[] operators = additive_expression.additive_operator();

			ResultInfo firstResult = CompileMultiplicativeExpression(compile, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileMultiplicativeExpression(compile, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				Operation op;
				MCSharpParser.Additive_operatorContext additive_operator = operators[i - 1];
				if(additive_operator.PLUS() != null) op = Operation.Addition;
				else op = Operation.Subtraction;

				ResultInfo opResult = CompileSimpleOperation(compile, op, value, expressionValue, out value);
				if(opResult.Failure) return compile.GetLocation(additive_operator) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileMultiplicativeExpression(CompileArguments compile, MultiplicativeExpressionContext multiplicative_expression, out IInstance value) {

			#region Argument Checks
			if(multiplicative_expression is null)
				throw new ArgumentNullException(nameof(multiplicative_expression));
			#endregion

			WithExpressionContext[] expressions = multiplicative_expression.with_expression();
			MCSharpParser.Multiplicative_operatorContext[] operators = multiplicative_expression.multiplicative_operator();

			ResultInfo firstResult = CompileWithExpression(compile, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileWithExpression(compile, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				Operation op;
				MCSharpParser.Multiplicative_operatorContext multiplicative_operator = operators[i - 1];
				if(multiplicative_operator.MULTIPLY() != null) op = Operation.Multiplication;
				else if(multiplicative_operator.DIVIDE() != null) op = Operation.Division;
				else op = Operation.Modulo;

				ResultInfo opResult = CompileSimpleOperation(compile, op, value, expressionValue, out value);
				if(opResult.Failure) return compile.GetLocation(multiplicative_operator) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileWithExpression(CompileArguments compile, WithExpressionContext with_expression, out IInstance value) {

			#region Argument Checks
			if(with_expression is null)
				throw new ArgumentNullException(nameof(with_expression));
			#endregion

			value = null;

			RangeExpressionContext range_expression = with_expression.range_expression();
			ResultInfo range_result = CompileRangeExpression(compile, range_expression, out IInstance range_value);
			if(range_result.Failure) return range_result;

			MCSharpParser.Anonymous_element_initializerContext anonymous_element_initializer = with_expression.anonymous_element_initializer();
			if(anonymous_element_initializer != null) {

				throw new NotImplementedException("With epxressions have not been implemented.");

			}

			value = range_value;
			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileRangeExpression(CompileArguments compile, RangeExpressionContext range_expression, out IInstance value) {

			#region Argument Checks
			if(range_expression is null)
				throw new ArgumentNullException(nameof(range_expression));
			#endregion

			UnaryExpressionContext[] unary_expressions = range_expression.unary_expression();
			int count = unary_expressions.Length;
			ResultInfo[] unary_results = new ResultInfo[count];
			IInstance[] unary_values = new IInstance[count];

			value = null;

			unary_results[0] = CompileUnaryExpression(compile, unary_expressions[0], out unary_values[0]);
			if(unary_results[0].Failure) return unary_results[0];

			if(count == 2 && unary_expressions[1] != null) {

				unary_results[1] = CompileUnaryExpression(compile, unary_expressions[1], out unary_values[1]);
				if(unary_results[1].Failure) return unary_results[1];

				throw new NotImplementedException("Range expressions have not been implemented.");

			} else {

				value = unary_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		public ResultInfo CompilePreStepExpression(CompileArguments compile, PreStepExpressionContext pre_step_expression, out IInstance value) {

			#region Argument Checks
			if(pre_step_expression is null)
				throw new ArgumentNullException(nameof(pre_step_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		public ResultInfo CompilePostStepExpression(CompileArguments compile, PostStepExpressionContext post_step_expression, out IInstance value) {

			#region Argument Checks
			if(post_step_expression is null)
				throw new ArgumentNullException(nameof(post_step_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		public ResultInfo CompileUnaryExpression(CompileArguments compile, UnaryExpressionContext unary_expression, out IInstance value) {

			#region Argument Checks
			if(unary_expression is null)
				throw new ArgumentNullException(nameof(unary_expression));
			#endregion

			PrimaryExpressionContext primary_expression = unary_expression.primary_expression();
			if(primary_expression != null) {

				ResultInfo result = CompilePrimaryExpression(compile, primary_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			UnaryExpressionContext subunary_expression = unary_expression.unary_expression();
			if(subunary_expression != null) {

				value = null;

				ResultInfo result = CompileUnaryExpression(compile, subunary_expression, out IInstance unary_value);
				if(result.Failure) return result;

				throw new NotImplementedException("Unary expressions have not been implemented.");

			}

			PreStepExpressionContext pre_step_expression = unary_expression.pre_step_expression();
			if(pre_step_expression != null) {

				ResultInfo result = CompilePreStepExpression(compile, pre_step_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			CastExpressionContext cast_expression = unary_expression.cast_expression();
			if(cast_expression != null) {

				ResultInfo result = CompileCastExpression(compile, cast_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			PointerIndirectionExpressionContext pointer_indirection_expression = unary_expression.pointer_indirection_expression();
			if(pointer_indirection_expression != null) {

				ResultInfo result = CompilePointerIndirectionExpression(compile, pointer_indirection_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			AddressofExpressionContext addressof_expression = unary_expression.addressof_expression();
			if(addressof_expression != null) {

				ResultInfo result = CompileAddressofExpression(compile, addressof_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Unary expression could not be determined.");

		}

		public ResultInfo CompileCastExpression(CompileArguments compile, CastExpressionContext cast_expression, out IInstance value) {

			#region Argument Checks
			if(cast_expression is null)
				throw new ArgumentNullException(nameof(cast_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		public ResultInfo CompilePointerIndirectionExpression(CompileArguments compile, PointerIndirectionExpressionContext pointer_indirection_expression, out IInstance value) {

			#region Argument Checks
			if(pointer_indirection_expression is null)
				throw new ArgumentNullException(nameof(pointer_indirection_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		public ResultInfo CompileAddressofExpression(CompileArguments compile, AddressofExpressionContext addressof_expression, out IInstance value) {

			#region Argument Checks
			if(addressof_expression is null)
				throw new ArgumentNullException(nameof(addressof_expression));
			#endregion

			value = null;

			UnaryExpressionContext unary_expression = addressof_expression.unary_expression();
			ResultInfo unary_result = CompileUnaryExpression(compile, unary_expression, out IInstance unary_value);
			if(unary_result.Failure) return unary_result;

			throw new NotImplementedException("Addressof expressions have not been implemented.");

		}

		public ResultInfo CompileAssignmentExpression(CompileArguments compile, AssignmentExpressionContext assignment_expression, out IInstance value) {

			#region Argument Checks
			if(assignment_expression is null)
				throw new ArgumentNullException(nameof(assignment_expression));
			#endregion

			value = null;

			UnaryExpressionContext unary_expression = assignment_expression.unary_expression();
			ResultInfo unary_result = CompileUnaryExpression(compile, unary_expression, out IInstance unary_value);
			if(unary_result.Failure) return unary_result;

			ExpressionContext expression = assignment_expression.expression();
			ResultInfo expression_result = CompileExpression(compile, expression, out IInstance expression_value);
			if(expression_result.Failure) return expression_result;

			Operation op;
			var assignment_operator = assignment_expression.assignment_operator();
			if(assignment_operator.ASSIGN() != null) op = Operation.Assign;
			else if(assignment_operator.ASSIGN_PLUS() != null) op = Operation.AssignAddition;
			else if(assignment_operator.ASSIGN_MINUS() != null) op = Operation.AssignSubtraction;
			else if(assignment_operator.ASSIGN_MULTIPLY() != null) op = Operation.AssignMultiplication;
			else if(assignment_operator.ASSIGN_DIVIDE() != null) op = Operation.AssignDivision;
			else if(assignment_operator.ASSIGN_MODULUS() != null) op = Operation.AssignModulo;
			else if(assignment_operator.ASSIGN_ACCESS() != null) op = Operation.AssignAccess;
			else if(assignment_operator.ASSIGN_AND() != null) op = Operation.AssignBitwiseAND;
			else if(assignment_operator.ASSIGN_OR() != null) op = Operation.AssignBitwiseOR;
			else if(assignment_operator.ASSIGN_XOR() != null) op = Operation.AssignBitwiseXOR;
			else if(assignment_operator.ASSIGN_LEFT() != null) op = Operation.AssignShiftLeft;
			else op = Operation.AssignShiftRight;

			ResultInfo simple_operation_result = CompileSimpleOperation(compile, op, unary_value, expression_value, out value);
			if(simple_operation_result.Failure) return compile.GetLocation(assignment_expression) + simple_operation_result;

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompilePrimaryExpression(CompileArguments compile, PrimaryExpressionContext primary_expression, out IInstance value) {

			#region Argument Checks
			if(primary_expression is null)
				throw new ArgumentNullException(nameof(primary_expression));
			#endregion

			value = null;

			ArrayCreationExpressionContext array_creation_expression = primary_expression.array_creation_expression();
			if(array_creation_expression != null) {

				ResultInfo result = CompileArrayCreationExpression(compile, array_creation_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			PrimaryNoArrayCreationExpressionContext primary_no_array_creation_expression = primary_expression.primary_no_array_creation_expression();
			if(primary_no_array_creation_expression != null) {

				ResultInfo result = CompilePrimaryNoArrayCreationExpression(compile, primary_no_array_creation_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Primary expression could not be determined.");

		}

		public ResultInfo CompileArrayCreationExpression(CompileArguments compile, ArrayCreationExpressionContext array_creation_expression, out IInstance value) {

			#region Argument Checks
			if(array_creation_expression is null)
				throw new ArgumentNullException(nameof(array_creation_expression));
			#endregion

			throw new NotImplementedException("Array-creation expression evaluation has not been implented.");

		}

		public ResultInfo CompilePrimaryNoArrayCreationExpression(CompileArguments compile, PrimaryNoArrayCreationExpressionContext primary_no_array_creation_expression, out IInstance value) {

			#region Argument Checks
			if(primary_no_array_creation_expression is null)
				throw new ArgumentNullException(nameof(primary_no_array_creation_expression));
			#endregion

			MCSharpParser.LiteralContext literal = primary_no_array_creation_expression.literal();
			if(literal != null) {

				ResultInfo result = CompileLiteral(compile, literal, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			MCSharpParser.Short_identifierContext identifier = primary_no_array_creation_expression.short_identifier();
			if(identifier != null) {

				ResultInfo result = CompileShortIdentifier(compile, identifier, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			ExpressionContext expression = primary_no_array_creation_expression.expression();
			if(expression != null) {

				ResultInfo result = CompileExpression(compile, expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			MCSharpParser.Member_accessContext member_access = primary_no_array_creation_expression.member_access();
			if(member_access != null) {

				MCSharpParser.Member_access_prefixContext[] member_access_prefixes = member_access.member_access_prefix();
				MCSharpParser.Short_identifierContext member_identifier = member_access.short_identifier();
				MCSharpParser.Generic_argumentsContext generic_arguments = member_access.generic_arguments();
				MCSharpParser.Method_argumentsContext method_arguments = member_access.method_arguments();
				MCSharpParser.Indexer_argumentsContext indexer_arguments = member_access.indexer_arguments();

				IInstance holder;
				ResultInfo Access(IInstance holder, out IInstance value) {

					IMember accessedMember = null;
					foreach(IMember member in holder.Type.Members) {
						if(member.Identifier == member_identifier.GetText()) {
							accessedMember = member;
						}
					}

					// TODO: Check inherited types.

					if(accessedMember == null) {
						value = null;
						return new ResultInfo(false, $"{compile.GetLocation(member_identifier)}'{member_identifier.GetText()}' does not exist in type '{holder.Type.Identifier}'.");
					} else {

						IMemberDefinition definition = accessedMember.Definition;

						switch(definition) {

							case IField field: {

								// Struct, Class, and Predefined all have different ways of storing fields.
								switch(holder) {

									// Get IInstance value from StructInstance.FieldInstances.
									case StructInstance structHolder: {
										value = structHolder.FieldInstances[field];
										return ResultInfo.DefaultSuccess;
									}

									// TODO: ClassInstance

									// TODO: PredefinedInstance

									default: throw new Exception($"Unsupported type of {nameof(IInstance)}: '{holder.GetType().FullName}'.");
								}

							}

							case IProperty property: {

								// Get getter.
								var getter = property.Getter;
								if(getter == null) {
									value = null;
									return new ResultInfo(false, $"{compile.GetLocation(member_identifier)}This property is not get-able.");
								}

								// Invoke 'get' method.
								ResultInfo result = property.Getter.Invoke(compile, new IType[] { }, new IInstance[] { }, out value);
								if(result.Failure) return compile.GetLocation(member_identifier) + result;

								return ResultInfo.DefaultSuccess;

							}

							case IMethod method: {

								// Get generic arguments.
								IType[] generics;
								if(generic_arguments == null) {
									generics = new IType[] { };
								} else {
									throw new NotImplementedException($"{compile.GetLocation(generic_arguments)}Invoking methods with generic arguments has not been implemented.");
								}

								// Get method arguments.
								IInstance[] arguments;
								if(method_arguments == null) {
									value = null;
									return new ResultInfo(false, $"{compile.GetLocation(member_access)}Expected method arguments for accessing method.");
								} else {
									MCSharpParser.Argument_listContext argument_list = method_arguments.argument_list();
									if(argument_list == null) {
										arguments = new IInstance[] { };
									} else {
										throw new NotImplementedException("Accessing methods with more than zero parameters has not been implemented.");
									}
								}

								// Invoke method.
								ResultInfo result = method.Invoker.Invoke(compile, generics, arguments, out value);
								if(result.Failure) return compile.GetLocation(member_access) + result;

								return ResultInfo.DefaultSuccess;

							}

							default: throw new Exception($"Unsupported type of {nameof(IMember)}: '{definition.GetType().FullName}'.");
						}

					}

				}

				// Get the instance to access from.
				if(member_access_prefixes != null && member_access_prefixes.Length > 0) {

					value = null;

					// Evaluate chain.
					holder = null;
					for(int i = 0; i < member_access_prefixes.Length; i++) {

						var prefix = member_access_prefixes[i];

						if(holder == null) {

							// Start of chain (set holder).

							ArrayCreationExpressionContext prefix_array_creation_expression = prefix.array_creation_expression();
							if(prefix_array_creation_expression != null) {

								ResultInfo prefixResult = CompileArrayCreationExpression(compile, prefix_array_creation_expression, out IInstance instance);
								if(prefixResult.Failure) return compile.GetLocation(prefix_array_creation_expression) + prefixResult;

								holder = instance;
								continue;

							}

							MCSharpParser.LiteralContext prefix_literal = prefix.literal();
							if(prefix_literal != null) {

								ResultInfo prefixResult = CompileLiteral(compile, prefix_literal, out IInstance instance);
								if(prefixResult.Failure) return compile.GetLocation(prefix_literal) + prefixResult;

								holder = instance;
								continue;

							}

							MCSharpParser.Short_identifierContext prefix_short_identifier = prefix.short_identifier();
							if(prefix_short_identifier != null) {

								ResultInfo result = CompileShortIdentifier(compile, prefix_short_identifier, out IInstance instance);
								if(result.Failure) return result;

								// TODO: generic_arguments? ( method_arguments | indexer_arguments )?

								holder = instance;
								continue;

							}

							ExpressionContext prefix_expression = prefix.expression();
							if(prefix_expression != null) {

								ResultInfo prefixResult = CompileExpression(compile, prefix_expression, out IInstance instance);
								if(prefixResult.Failure) return compile.GetLocation(prefix_expression) + prefixResult;

								holder = instance;
								continue;

							}

							PostStepExpressionContext prefix_post_step_expression = prefix.post_step_expression();
							if(prefix_post_step_expression != null) {

								ResultInfo prefixResult = CompilePostStepExpression(compile, prefix_post_step_expression, out IInstance instance);
								if(prefixResult.Failure) return compile.GetLocation(prefix_post_step_expression) + prefixResult;

								holder = instance;
								continue;

							}

							KeywordExpressionContext prefix_keyword_expression = prefix.keyword_expression();
							if(prefix_keyword_expression != null) {

								ResultInfo prefixResult = CompileKeywordExpression(compile, prefix_keyword_expression, out IInstance instance);
								if(prefixResult.Failure) return prefixResult;

								holder = instance;
								continue;

							}

						} else {

							// Access from last (holder).

							ArrayCreationExpressionContext prefix_array_creation_expression = prefix.array_creation_expression();
							if(prefix_array_creation_expression != null) {

								ResultInfo prefixResult = CompileArrayCreationExpression(compile, prefix_array_creation_expression, out IInstance instance);
								if(prefixResult.Failure) return compile.GetLocation(prefix_array_creation_expression) + prefixResult;

								throw new NotImplementedException("TODO");

							}

							MCSharpParser.LiteralContext prefix_literal = prefix.literal();
							if(prefix_literal != null) {

								ResultInfo prefixResult = CompileLiteral(compile, prefix_literal, out IInstance instance);
								if(prefixResult.Failure) return compile.GetLocation(prefix_literal) + prefixResult;

								throw new NotImplementedException("TODO");

							}

							ExpressionContext prefix_expression = prefix.expression();
							if(prefix_expression != null) {

								ResultInfo prefixResult = CompileExpression(compile, prefix_expression, out IInstance instance);
								if(prefixResult.Failure) return compile.GetLocation(prefix_expression) + prefixResult;

								throw new NotImplementedException("TODO");

							}

							PostStepExpressionContext prefix_post_step_expression = prefix.post_step_expression();
							if(prefix_post_step_expression != null) {

								ResultInfo prefixResult = CompilePostStepExpression(compile, prefix_post_step_expression, out IInstance instance);
								if(prefixResult.Failure) return compile.GetLocation(prefix_post_step_expression) + prefixResult;

								throw new NotImplementedException("TODO");

							}

							KeywordExpressionContext prefix_keyword_expression = prefix.keyword_expression();
							if(prefix_keyword_expression != null) {

								ResultInfo prefixResult = CompileKeywordExpression(compile, prefix_keyword_expression, out IInstance instance);
								if(prefixResult.Failure) return prefixResult;

								throw new NotImplementedException("TODO");

							}

						}

					}

				} else {

					// Access from implicit 'this'.
					throw new NotImplementedException(compile.GetLocation(member_access) + "Implicit 'this' has not been implemented.");

				}


				return Access(holder, out value);

			}

			PostStepExpressionContext post_step_expression = primary_no_array_creation_expression.post_step_expression();
			if(post_step_expression != null) {

				throw new NotImplementedException("Post-step expression evaluation have not been implemented.");

			}

			KeywordExpressionContext keyword_expression = primary_no_array_creation_expression.keyword_expression();
			if(keyword_expression != null) {

				ResultInfo result = CompileKeywordExpression(compile, keyword_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Primary no-array-creation expression could not be determined.");

		}

		public ResultInfo CompileShortIdentifier(CompileArguments compile, MCSharpParser.Short_identifierContext identifier, out IInstance value) {

			string name = identifier.NAME().GetText();

			value = compile.Scope.FindFirstInstanceByName(name);
			if(value == null) return new ResultInfo(false, $"{compile.GetLocation(identifier)}Instance '{name}' does not exist yet in this scope.");

			return ResultInfo.DefaultSuccess;

		}

		public ResultInfo CompileLiteral(CompileArguments compile, MCSharpParser.LiteralContext literal, out IInstance value) {

			ITerminalNode integer_literal = literal.INTEGER();
			if(integer_literal != null) {

				string text = integer_literal.GetText();
				int _value = int.Parse(text);

				IType type = DefinedTypes[MCSharpLinkerExtension.IntIdentifier];
				value = new PrimitiveInstance.IntegerInstance.Constant(type, null, _value);
				ResultInfo scopeResult = compile.Scope.AddInstance(value);
				if(scopeResult.Failure) return compile.GetLocation(integer_literal) + scopeResult;

				return ResultInfo.DefaultSuccess;

			}

			ITerminalNode boolean_literal = literal.BOOLEAN();
			if(boolean_literal != null) {

				string text = boolean_literal.GetText();
				bool _value = bool.Parse(text);

				IType type = DefinedTypes[MCSharpLinkerExtension.BoolIdentifier];
				value = new PrimitiveInstance.BooleanInstance.Constant(type, null, _value);
				ResultInfo scopeResult = compile.Scope.AddInstance(value);
				if(scopeResult.Failure) return compile.GetLocation(boolean_literal) + scopeResult;

				return ResultInfo.DefaultSuccess;

			}

			ITerminalNode string_literal = literal.STRING();
			if(string_literal != null) {

				string text = string_literal.GetText();
				string _value = text[1..^1];

				IType type = DefinedTypes[MCSharpLinkerExtension.StringIdentifier];
				value = new PrimitiveInstance.StringInstance(type, null, _value);
				ResultInfo scopeResult = compile.Scope.AddInstance(value);
				if(scopeResult.Failure) return compile.GetLocation(string_literal) + scopeResult;

				return ResultInfo.DefaultSuccess;

			}

			ITerminalNode decimal_literal = literal.DECIMAL();
			if(decimal_literal != null) {

				string text = decimal_literal.GetText();
				double _value = double.Parse(text);

				// type

				throw new NotImplementedException("Decimal/double/float literals have not been implemented.");

			}

			throw new NotImplementedException("Literal context could not be determined.");

		}

		public ResultInfo CompileKeywordExpression(CompileArguments compile, KeywordExpressionContext keyword_expression, out IInstance value) {

			throw new NotImplementedException("Keyword expression evaluation have not been implemented.");

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

			Objective.ClearAnonymousNames();

		}

		#region Subtypes

		public struct CompileArguments {

			public Compiler Compiler { get; }

			public StandaloneStatementFunction Function { get; }

			public FunctionWriter Writer => Function.Writer;

			public Scope Scope { get; }

			public bool Predefined { get; }


			public CompileArguments(Compiler compiler, StandaloneStatementFunction function, Scope scope, bool predefined) {
				Compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
				Function = function ?? throw new ArgumentNullException(nameof(function));
				Scope = scope ?? throw new ArgumentNullException(nameof(scope));
				Predefined = predefined;
			}


			public string GetLocation(ParserRuleContext context) => GetLocation(context.Start);

			public string GetLocation(ITerminalNode node) => GetLocation(node.Symbol);

			public string GetLocation(IToken token) {
				if(Predefined) {
					int line = token.Line;
					int column = token.Column;
					return $"Predefined line {line}:{column} ";
				} else {
					string file = token.InputStream.SourceName;
					int line = token.Line;
					int column = token.Column;
					return $"{file} {line}:{column} ";
				}
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

		#endregion

	}

}
