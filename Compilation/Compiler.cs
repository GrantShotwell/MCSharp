using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage;
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
using MCSharp.Linkage.Extensions;

namespace MCSharp.Compilation {

	public class Compiler : IDisposable {

		/// <summary>
		/// The <see cref="Compilation.Settings"/> used to configure the compiler.
		/// </summary>
		public Settings Settings { get; }

		public VirtualMachine VirtualMachine { get; private set; }

		/// <summary>
		/// The assemblies that are loaded into the compiler.
		/// </summary>
		public ICollection<Assembly> Assemblies { get; }


		/// <summary>
		/// Creates a new compiler.
		/// </summary>
		/// <param name="settings">The settings to use.</param>
		/// <param name="assemblies">The assemblies to use.</param>
		public Compiler(Settings settings, ICollection<Assembly> assemblies) {
			Settings = settings;
			VirtualMachine = new VirtualMachine();
			Assemblies = assemblies;
		}


		#region Data

		public IDictionary<string, IType> DefinedTypes { get; } = new Dictionary<string, IType>();
		public ICollection<LinkerExtension> LinkerExtensions { get; } = new LinkedList<LinkerExtension>();

		/// <summary>
		/// Finds an item within <see cref="LinkerExtensions"/> that is of type <typeparamref name="TLinkerExtension"/>.
		/// </summary>
		/// <typeparam name="TLinkerExtension">The type of <see cref="LinkerExtension"/> to find.</typeparam>
		/// <returns>Returns the first item found, <see langword="null"/> if nothing was found.</returns>
		public TLinkerExtension GetLinkerExtension<TLinkerExtension>() where TLinkerExtension : LinkerExtension {
			foreach(LinkerExtension extension in LinkerExtensions)
				if(extension is TLinkerExtension target) return target;
			return default;
		}

		#endregion

		#region Compilation

		/// <summary>
		/// Compiles the given <see cref="Settings"/> and returns the resulting <see cref="ResultInfo"/>.
		/// </summary>
		/// <returns>The result of the compilation.</returns>
		public ResultInfo Compile() {
			
			// Prepare a list of things to do after the first pass.
			List<Action> postFirstPass = new List<Action>();
			// Prepare a list of things to do on load.
			List<Action<CompileArguments>> onLoadActions = new List<Action<CompileArguments>>();
			// Prepare a list of things to do on tick.
			List<Action<CompileArguments>> onTickActions = new List<Action<CompileArguments>>();
			// Make the root scope.
			Scope rootScope = new Scope(null, null);

			// Find, parse, and first pass walk (types, members) all script files.
			ResultInfo FirstPassWalk() {

				foreach(string file in Settings.Datapack.EnumerateScriptFiles()) {

					// Use Antlr generated classes to parse the file.

					// Create an input stream from the file.
					ICharStream stream = CharStreams.fromPath(file);
					var errorListener = new ScriptErrorListener(file[(Settings.Datapack.ScriptDirectory.Length + 1)..]);

					// Create a lexer.
					var lexer = new MCSharpLexer(stream);
					lexer.RemoveErrorListeners();
					lexer.AddErrorListener(errorListener);

					// Create a stream of tokens from the lexer.
					ITokenStream tokens = new CommonTokenStream(lexer);

					// Create a parser.
					var parser = new MCSharpParser(tokens) { BuildParseTree = true };
					parser.RemoveErrorListeners();
					parser.AddErrorListener(errorListener);

					// Parse the file.
					IParseTree tree = parser.script();
					if(errorListener.Result.Failure) {
						return errorListener.Result;
					}

					// Walk the tree and perform the first pass.
					var walker = new ScriptClassWalker(this, rootScope);
					ParseTreeWalker.Default.Walk(walker, tree);

					// Add the post-first-pass action.
					postFirstPass.Add(() => walker.PostFirstPass());
					// Add the on-load action.
					onLoadActions.Add((location) => walker.OnLoad(location));

				}

				return ResultInfo.DefaultSuccess;

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

					extension.CreatePredefinedTypes(rootScope, out Action<CompileArguments> onLoad, out Action<CompileArguments> onTick);
					onLoadActions.Add(onLoad);
					onTickActions.Add(onTick);

				}
			}


			{

				// Prepare threads.
				ResultInfo firstpassResult = ResultInfo.DefaultSuccess;
				Thread threadFirstPassWalk = new Thread(new ThreadStart(() => {
					firstpassResult = FirstPassWalk();
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
				if(firstpassResult.Failure) return firstpassResult;

				// Run post-first-pass actions.
				foreach(Action action in postFirstPass) action();

			}


			// Compile 'Program.Load()' and 'Program.Tick()'.
			foreach(IType type in DefinedTypes.Values) {

				if(type.Identifier != Settings.Datapack.ProgramClassName) continue;

				foreach(IMember member in type.Members) {

					bool load = member.Identifier == Settings.Datapack.ProgramLoadName;
					bool tick = member.Identifier == Settings.Datapack.ProgramTickName;
					if(!load && !tick) continue;
					if(member.MemberType != MemberType.Method) continue;

					Action<CompileArguments> trigger = (location) => {
						foreach(var action in load ? onLoadActions : onTickActions)
							action.Invoke(location);
						location.Writer.AddBufferedLines(1);
					};
					ResultInfo result = CompileStandaloneMethod(type.Scope, member, trigger);
					if(result.Success) continue;
					else return result;

				}

			}


			return ResultInfo.DefaultSuccess;

		}

		#region Member Compilation

		/// <summary>
		/// Compiles the given <see cref="MemberType.Method"/> <see cref="IMember"/>.
		/// </summary>
		/// <param name="typeScope">The scope of the type that contains the member.</param>
		/// <param name="member">The member (<see cref="MemberType.Method"/>) to compile.</param>
		/// <param name="trigger">The trigger to call before the member is compiled.</param>
		/// <returns>The result of the compilation.</returns>
		public ResultInfo CompileStandaloneMethod(Scope typeScope, IMember member, Action<CompileArguments> trigger = null) {

			#region Argument Checks
			if(typeScope is null)
				throw new ArgumentNullException(nameof(typeScope));
			if(member is null)
				throw new ArgumentNullException(nameof(member));
			if(member.MemberType != MemberType.Method)
				throw new ArgumentOutOfRangeException(nameof(member), $"The given {member.MemberType} is not {nameof(MemberType.Method)}.");
			#endregion

			Scope methodScope = member.Scope;
			IMethod method = member.Definition as IMethod;
			StandaloneStatementFunction invoker = method.Invoker as StandaloneStatementFunction;
			invoker.Writer.WriteTitle(
				$"{member.Declarer.Identifier}.{member.Identifier}",
				indentAfter: true);
			invoker.Compiled = true;
			return CompileStatements(invoker, methodScope, invoker.Statements, trigger);

		}

		#endregion

		#region Statement Compilation

		/// <summary>
		/// Compiles the given collection of <see cref="IStatement"/>s.
		/// </summary>
		/// <param name="function">The function that contains the statements.</param>
		/// <param name="scope">The scope of the function.</param>
		/// <param name="statements">The statements to compile.</param>
		/// <param name="trigger">The trigger to call before the statements are compiled.</param>
		/// <returns>The result of the compilation.</returns>
		public ResultInfo CompileStatements(StandaloneStatementFunction function, Scope scope, ICollection<IStatement> statements, Action<CompileArguments> trigger = null) {

			#region Argument Checks
			if(function is null)
				throw new ArgumentNullException(nameof(function));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(statements is null)
				throw new ArgumentNullException(nameof(statements));
			#endregion

			trigger?.Invoke(new CompileArguments(this, function, scope, true));

			foreach(IStatement statement in statements) {

				if(statement is ScriptStatement) {

					ResultInfo result = CompileStatement(new CompileArguments(this, function, scope, false), statement.Context);
					if(result.Failure) return result;

				} else if(statement is PredefinedStatement) {

					ResultInfo result = CompileStatement(new CompileArguments(this, function, scope, true), statement.Context);
					// TODO: Send exception and say this is a bug if result is failed.
					if(result.Failure) return result;

				} else {

					// TODO: Add more information.
					throw new Exception();

				}

			}

			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles the given <see cref="StatementContext"/>.
		/// </summary>
		/// <param name="location">The location of the statement.</param>
		/// <param name="context">The statement to compile.</param>
		/// <returns>The result of the compilation.</returns>
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

					Scope statement1Scope = new Scope(null, location.Scope);
					StandaloneStatementFunction statement1Function = location.Function.CreateChildFunction(CreateIStatements(statement1, location.Predefined), Settings);
					CompileArguments statement1Location = new CompileArguments(location.Compiler, statement1Function, statement1Scope, location.Predefined);

					ResultInfo statement1Result = CompileStatement(statement1Location, statement1);
					if(statement1Result.Failure) return statement1Result;

					if(value is PrimitiveInstance.BooleanInstance valueBool) {

						location.Function.Writer.WriteCommand($"execute if score {MCSharpLinkerExtension.StorageSelector} {valueBool.Objective.Name} matches 1.. " +
							$"run function {statement1Function.Writer.GamePath}");

						if(statement2 != null) {

							Scope statement2Scope = new Scope(null, location.Scope);
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

					Scope scopeOuter = new Scope(null, location.Scope);
					CompileArguments locationOuter = new CompileArguments(location.Compiler, location.Function, scopeOuter, location.Predefined);

					Scope scopeInner = new Scope(null, scopeOuter);
					StandaloneStatementFunction statementFunction = locationOuter.Function.CreateChildFunction(CreateIStatements(statement, locationOuter.Predefined), Settings);
					CompileArguments locationInner = new CompileArguments(locationOuter.Compiler, statementFunction, scopeInner, locationOuter.Predefined);

					ResultInfo initializationResult = CompileInitializationExpression(locationOuter, for_initialization_expression, out _);
					if(initializationResult.Failure) return initializationResult;

					ResultInfo conditionResult1 = CompileExpression(locationOuter, condition, out IInstance conditionValue1);
					if(conditionResult1.Failure) return conditionResult1;

					if(!(conditionValue1 is PrimitiveInstance.BooleanInstance conditionValue1Bool)) {

						throw new NotImplementedException("Casting has not been implemented.");

					}

					locationOuter.Function.Writer.WriteCommand($"execute if score {MCSharpLinkerExtension.StorageSelector} {conditionValue1Bool.Objective.Name} matches 1.. " +
						$"run function {statementFunction.Writer.GamePath}");

					ResultInfo statementResult = CompileStatement(locationInner, statement);
					if(statementResult.Failure) return statementResult;

					ResultInfo incrementResult = CompileExpression(locationInner, increment, out _);
					if(incrementResult.Failure) return incrementResult;

					ResultInfo conditionResult2 = CompileExpression(locationInner, condition, out IInstance conditionValue2);
					if(conditionResult2.Failure) return conditionResult2;

					if(!(conditionValue2 is PrimitiveInstance.BooleanInstance conditionValue2Bool)) {

						throw new NotImplementedException("Casting has not been implemented.");

					}

					locationInner.Function.Writer.WriteCommand($"execute if score {MCSharpLinkerExtension.StorageSelector} {conditionValue2Bool.Objective.Name} matches 1.. " +
						$"run function {statementFunction.Writer.GamePath}");

					return ResultInfo.DefaultSuccess;

				}

				ForeachStatementContext foreach_statement = language_function.foreach_statement();
				if(foreach_statement != null) {

					throw new NotImplementedException("'foreach' statements have not been implemented.");

				}

				WhileStatementContext while_statement = language_function.while_statement();
				if(while_statement != null) {

					ExpressionContext condition = while_statement.expression();
					StatementContext statement = while_statement.statement();

					Scope scopeOuter = new Scope(null, location.Scope);
					CompileArguments locationOuter = new CompileArguments(location.Compiler, location.Function, scopeOuter, location.Predefined);

					Scope scopeInner = new Scope(null, scopeOuter);
					StandaloneStatementFunction statementFunction = locationOuter.Function.CreateChildFunction(CreateIStatements(statement, locationOuter.Predefined), Settings);
					CompileArguments locationInner = new CompileArguments(locationOuter.Compiler, statementFunction, scopeInner, locationOuter.Predefined);

					ResultInfo conditionResult1 = CompileExpression(locationOuter, condition, out IInstance conditionValue1);
					if(conditionResult1.Failure) return conditionResult1;

					if(!(conditionValue1 is PrimitiveInstance.BooleanInstance conditionValue1Bool)) {

						throw new NotImplementedException("Casting has not been implemented.");

					}

					locationOuter.Function.Writer.WriteCommand($"execute if score {MCSharpLinkerExtension.StorageSelector} {conditionValue1Bool.Objective.Name} matches 1.. " +
						$"run function {statementFunction.Writer.GamePath}");

					ResultInfo statementResult = CompileStatement(locationInner, statement);
					if(statementResult.Failure) return statementResult;

					ResultInfo conditionResult2 = CompileExpression(locationInner, condition, out IInstance conditionValue2);
					if(conditionResult2.Failure) return conditionResult2;

					if(!(conditionValue2 is PrimitiveInstance.BooleanInstance conditionValue2Bool)) {

						throw new NotImplementedException("Casting has not been implemented.");

					}

					locationInner.Function.Writer.WriteCommand($"execute if score {MCSharpLinkerExtension.StorageSelector} {conditionValue2Bool.Objective.Name} matches 1.. " +
							$"run function {statementFunction.Writer.GamePath}");

					return ResultInfo.DefaultSuccess;

				}

				DoStatementContext do_statement = language_function.do_statement();
				if(do_statement != null) {

					ExpressionContext condition = do_statement.expression();
					StatementContext statement = do_statement.statement();

					Scope scopeInner = new Scope(null, location.Scope);
					StandaloneStatementFunction statementFunction = location.Function.CreateChildFunction(CreateIStatements(statement, location.Predefined), Settings);
					CompileArguments locationInner = new CompileArguments(location.Compiler, statementFunction, scopeInner, location.Predefined);

					location.Function.Writer.WriteCommand($"function {statementFunction.Writer.GamePath}");

					ResultInfo statementResult = CompileStatement(locationInner, statement);
					if(statementResult.Failure) return statementResult;

					ResultInfo conditionResult = CompileExpression(locationInner, condition, out IInstance conditionValue2);
					if(conditionResult.Failure) return conditionResult;

					if(!(conditionValue2 is PrimitiveInstance.BooleanInstance conditionValue2Bool)) {

						throw new NotImplementedException("Casting has not been implemented.");

					}

					locationInner.Function.Writer.WriteCommand($"execute if score {MCSharpLinkerExtension.StorageSelector} {conditionValue2Bool.Objective.Name} matches 1.. " +
							$"run function {statementFunction.Writer.GamePath}");

					return ResultInfo.DefaultSuccess;

				}

				ReturnStatementContext return_statement = language_function.return_statement();
				if(return_statement != null) {

					// Find return type (if valid).
					IScopeHolder holder = location.Scope.Holder;
					if(holder is IMember member) {

						var memberType = member.MemberType;

						if(memberType == MemberType.Method) {

							// Find return type.
							var method = member.Definition as IMethod;
							string returnTypeIdentifier = method.Invoker.ReturnTypeIdentifier;
							ExpressionContext return_expression = return_statement.expression();

							if(returnTypeIdentifier == "void") {

								if(return_expression != null) return new ResultInfo(false, location.GetLocation(return_expression) + "Expected to return 'void'.");

							} else {

								ResultInfo expressionResult = CompileExpression(location, return_expression, out IInstance returnValue);
								if(expressionResult.Failure) return expressionResult;

								IType returnType = DefinedTypes[returnTypeIdentifier];
								if(returnValue.Type != returnType) {

									// Cast return value as desired type.
									var cast = returnType.Conversions[returnValue.Type];
									if(cast == null) {
										return new ResultInfo(false, location.GetLocation(return_expression) + $"Cannot cast '{returnValue.Type.Identifier}' to '{returnType.Identifier}'.");
									} else {
										var castResult = cast.Function.Invoke(location, new IType[] { }, new IInstance[] { returnValue }, out returnValue);
										if(castResult.Failure) return location.GetLocation(return_expression) + castResult;
									}

								}

								// Return value.
								var functionReturnValue = location.Function.ReturnInstance;
								if(functionReturnValue == null) {
									return new ResultInfo(false, location.GetLocation(return_expression) + "Cannot return from a void function.");
								} else {
									location.Compiler.CompileSimpleOperation(location, Operation.Assign, functionReturnValue, returnValue, out _);
								}

							}
							
							// TODO: End execution of function.
							location.Writer.WriteComments(
								$"Return statements do not end execution yet. Execution would stop here.",
								indentBefore: true, indentAfter: true);

						} else if(memberType == MemberType.Property) {

							// Find return type.
							var property = member.Definition as IProperty;
							// TODO: Unable to determine if 'return_statement' is from the 'setter' or 'getter'.
							throw new NotImplementedException("'return' statements in properties have not been implemented.");

						} else {

							// 'return' statement is invalid here.
							return new ResultInfo(false, location.GetLocation(return_statement) + "A 'return' statement is invalid here.");

						}

					} else {

						// 'return' statement is invalid here.
						return new ResultInfo(false, location.GetLocation(return_statement) + "A 'return' statement is invalid here.");

					}

					return ResultInfo.DefaultSuccess;

				}

				ThrowStatementContext throw_statement = language_function.throw_statement();
				if(throw_statement != null) {

					// TODO
					throw new NotImplementedException("'throw' statements have not been implemented.");

				}

				TryStatementContext try_statement = language_function.try_statement();
				if(try_statement != null) {

					// TODO
					throw new NotImplementedException("'try' statements have not been implemented.");

				}

			}


			InitializationExpressionContext initialization_expression = context.initialization_expression();
			if(initialization_expression != null) {

				return CompileInitializationExpression(location, initialization_expression, out _);

			}


			ExpressionContext expression = context.expression();
			if(expression != null) {

				return CompileExpression(location, expression, out _);

			}

			throw new Exception("Statement context could not be determined.");

		}

		/// <summary>
		/// Compiles an initialization expression.
		/// </summary>
		/// <param name="location">The location of the expression.</param>
		/// <param name="initialization_expressoin">The initialization expression.</param>
		/// <param name="value">The value of the initialization expression.</param>
		/// <returns>The result of the compilation.</returns>
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

		/// <summary>
		/// Compiles an operation between two <see cref="IInstance"/>s.
		/// </summary>
		/// <param name="location">The location of the operation.</param>
		/// <param name="op">The operation to perform.</param>
		/// <param name="operand1">The first operand.</param>
		/// <param name="operand2">The second operand.</param>
		/// <param name="result">The result of the operation.</param>
		/// <returns>The result of the compilation.</returns>
		public ResultInfo CompileSimpleOperation(CompileArguments location, Operation op, IInstance operand1, IInstance operand2, out IInstance result) {

			IType type1 = operand1.Type;
			IType type2 = operand2.Type;

			ICollection<IOperation> matches = new List<IOperation>(2);

			// Find all operations in the given type that match the operands.
			// Return true if the operation is found.
			bool FindMatchesInType(IType type) {

				// Look for matches within the top-level of this type.
				if(type.Operations.ContainsKey(op)) {

					HashSet<IOperation> operations = type.Operations[op];
					foreach(IOperation operation in operations) {

						// Get the parameters from the function.
						IReadOnlyList<IMethodParameter> parameters = operation.Function.MethodParameters;
						// Make sure there are exactly two parameters, continue if not.
						if(parameters.Count != 2) continue;

						// Get the type of the first parameter.
						IType parameterType1 = location.Compiler.DefinedTypes[parameters[0].TypeIdentifier];
						// Get the type of the second parameter.
						IType parameterType2 = location.Compiler.DefinedTypes[parameters[1].TypeIdentifier];

						// Create booleans for if the parameters match the operands.
						bool type1Matches = false, type2Matches = false;
						// Check if the types are assignable.
						if(parameterType1.IsAssignableFrom(type1)) type1Matches = true;
						if(parameterType2.IsAssignableFrom(type2)) type2Matches = true;
						// Check if the types have an implicit conversion.
						if(!type1Matches && type1.Conversions.ContainsKey(parameterType1) && type1.Conversions[parameterType1].Implicit) type1Matches = true;
						if(!type2Matches && type2.Conversions.ContainsKey(parameterType2) && type2.Conversions[parameterType2].Implicit) type2Matches = true;

						// If both parameters match, add the operation to the list of matches.
						if(type1Matches && type2Matches) {
							matches.Add(operation);
							return true;
						}

					}

				}

				// Look for matches within the base types of this type.
				foreach(IType baseType in type.BaseTypes) {

					if(FindMatchesInType(baseType)) return true;

				}

				return false;

			}

			// Find all operations in the given type that match the operands.
			FindMatchesInType(type1);
			// Don't search type2 if it is the same as type1.
			if(type1 != type2) FindMatchesInType(type2);

			if(matches.Count == 0) {

				result = null;
				return new ResultInfo(false, $"The {op} operation between '{type1.Identifier}' and '{type2.Identifier}' does not exist.");

			} else {

				// Find the correct operation.
				IOperation operation = matches.First();

				// Get the parameters from the function.
				var parameter1 = operation.Function.MethodParameters[0];
				var parameter2 = operation.Function.MethodParameters[1];
				
				// Get the types of the parameters.
				IType parameterType1 = location.Compiler.DefinedTypes[parameter1.TypeIdentifier];
				IType parameterType2 = location.Compiler.DefinedTypes[parameter2.TypeIdentifier];

				// Do the operands need to be converted?
				bool convert1 = !parameterType1.IsAssignableFrom(type1);
				bool convert2 = !parameterType2.IsAssignableFrom(type2);

				// Convert the operands if needed.
				if(convert1) type1.Conversions[parameterType1].Function.Invoke(location, new IType[] { }, new IInstance[] { operand1 }, out operand1);
				if(convert2) type2.Conversions[parameterType2].Function.Invoke(location, new IType[] { }, new IInstance[] { operand2 }, out operand2);

				// Invoke the operation.
				ResultInfo functionResult = operation.Function.Invoke(location, new IType[] { }, new IInstance[] { operand1, operand2 }, out result);
				if(functionResult.Failure) return functionResult;

				return ResultInfo.DefaultSuccess;

			}

		}

		/// <summary>
		/// Compiles an expression:
		/// <code>
		/// expression
		///   : non_assignment_expression
		///   | assignment_expression
		///   ;
		/// </code>
		/// </summary>
		/// <param name="location">The location of the expression.</param>
		/// <param name="expression">The expression to compile.</param>
		/// <param name="value">The value of the expression.</param>
		/// <returns>The result of the compilation.</returns>
		public ResultInfo CompileExpression(CompileArguments location, ExpressionContext expression, out IInstance value) {

			#region Argument Checks
			if(expression is null)
				throw new ArgumentNullException(nameof(expression));
			#endregion

			NonAssignmentExpressionContext non_assignment_expression = expression.non_assignment_expression();
			if(non_assignment_expression != null) {

				ResultInfo non_assignment_result = CompileNonAssignmentExpression(location, non_assignment_expression, out value);
				if(non_assignment_result.Failure) return non_assignment_result;

				return ResultInfo.DefaultSuccess;

			}

			AssignmentExpressionContext assignment_expression = expression.assignment_expression();
			if(assignment_expression != null) {

				ResultInfo assignment_result = CompileAssignmentExpression(location, assignment_expression, out value);
				if(assignment_result.Failure) return assignment_result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Expression context could not be determined.");

		}

		/// <summary>
		/// Compiles a non-assignment expression:
		/// <code>
		/// non_assignment_expression
		///   : conditional_expression
		///   | lambda_expression
		///   ;
		/// </code>
		/// </summary>
		/// <param name="non_assignment_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileNonAssignmentExpression(CompileArguments location, NonAssignmentExpressionContext non_assignment_expression, out IInstance value) {

			#region Argument Checks
			if(non_assignment_expression is null)
				throw new ArgumentNullException(nameof(non_assignment_expression));
			#endregion

			ConditionalExpressionContext conditional_expression = non_assignment_expression.conditional_expression();
			if(conditional_expression != null) {

				ResultInfo conditional_result = CompileConditionalExpression(location, conditional_expression, out value);
				if(conditional_result.Failure) return conditional_result;

				return ResultInfo.DefaultSuccess;

			}

			LambdaExpressionContext lambda_expression = non_assignment_expression.lambda_expression();
			if(lambda_expression != null) {

				ResultInfo lambda_result = CompileLambdaExpression(location, lambda_expression, out value);
				if(lambda_result.Failure) return lambda_result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Non-assignment expression context could not be determined.");

		}

		/// <summary>
		/// Compiles a lambda expression:
		/// <code>
		/// lambda_expression
		///   : method_arguments LAMBDA ( code_block )
		///   ;
		/// </code>
		/// </summary>
		/// <param name="lambda_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileLambdaExpression(CompileArguments location, LambdaExpressionContext lambda_expression, out IInstance value) {

			#region Argument Checks
			if(lambda_expression is null)
				throw new ArgumentNullException(nameof(lambda_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		/// <summary>
		/// Compiles a conditional expression:
		/// <code>
		/// conditional_expression
		///   : null_coalescing_expression ( CONDITION_IF expression CONDITION_ELSE expression )?
		///   ;
		/// </summary>
		/// <param name="conditional_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileConditionalExpression(CompileArguments location, ConditionalExpressionContext conditional_expression, out IInstance value) {

			#region Argument Checks
			if(conditional_expression is null)
				throw new ArgumentNullException(nameof(conditional_expression));
			#endregion

			value = null;

			NullCoalescingExpressionContext null_coalescing_expression = conditional_expression.null_coalescing_expression();
			ResultInfo null_coalescing_result = CompileNullCoalescingExpression(location, null_coalescing_expression, out IInstance null_coalescing_value);
			if(null_coalescing_result.Failure) return null_coalescing_result;

			ExpressionContext[] expressions = conditional_expression.expression();
			if(expressions != null && expressions.Length > 0) {

				IInstance[] expression_values = new IInstance[2];
				ResultInfo[] expression_results = new ResultInfo[2];

				expression_results[0] = CompileExpression(location, expressions[0], out expression_values[0]);
				if(expression_results[0].Failure) return expression_results[0];

				expression_results[1] = CompileExpression(location, expressions[1], out expression_values[1]);
				if(expression_results[1].Failure) return expression_results[1];

				//var condition = DefinedTypes[MCSharpLinkerExtension.BoolIdentifier].InitializeInstance(compile, null) as PrimitiveInstance.BooleanInstance;


				throw new NotImplementedException("Conditional expressions have not been implemented.");

			} else {

				value = null_coalescing_value;
				return ResultInfo.DefaultSuccess;

			}

		}

		/// <summary>
		/// Compiles a null-coalescing expression:
		/// <code>
		/// null_coalescing_expression
		///   : conditional_or_expression ( NULL_COALESCING null_coalescing_expression )?
		///   ;
		/// </code>
		/// </summary>
		/// <param name="null_coalescing_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileNullCoalescingExpression(CompileArguments location, NullCoalescingExpressionContext null_coalescing_expression, out IInstance value) {

			#region Argument Checks
			if(null_coalescing_expression is null)
				throw new ArgumentNullException(nameof(null_coalescing_expression));
			#endregion

			value = null;

			ConditionalOrExpressionContext conditional_or_expression = null_coalescing_expression.conditional_or_expression();
			ResultInfo conditional_or_result = CompileConditionalOrExpression(location, conditional_or_expression, out IInstance conditional_or_value);
			if(conditional_or_result.Failure) return conditional_or_result;

			NullCoalescingExpressionContext null_coalescing_expression_second = null_coalescing_expression.null_coalescing_expression();
			if(null_coalescing_expression_second != null) {

				value = null;

				ResultInfo null_coalescing_second_result = CompileNullCoalescingExpression(location, null_coalescing_expression_second, out IInstance null_coalescing_second_value);
				if(null_coalescing_second_result.Failure) return null_coalescing_second_result;

				throw new NotImplementedException("Null-coalescing expressions have not been implemented.");

			} else {

				value = conditional_or_value;
				return ResultInfo.DefaultSuccess;

			}

		}

		/// <summary>
		/// Compiles a boolean 'OR' expression.
		/// <code>
		/// conditional_or_expression
		///   : conditional_and_expression ( BOOLEAN_OR conditional_and_expression )?
		///   ;
		/// </code>
		/// </summary>
		/// <param name="conditional_or_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileConditionalOrExpression(CompileArguments location, ConditionalOrExpressionContext conditional_or_expression, out IInstance value) {

			#region Argument Checks
			if(conditional_or_expression is null)
				throw new ArgumentNullException(nameof(conditional_or_expression));
			#endregion

			ConditionalAndExpressionContext[] expressions = conditional_or_expression.conditional_and_expression();
			ITerminalNode[] operators = conditional_or_expression.BOOLEAN_OR();

			ResultInfo firstResult = CompileConditionalAndExpression(location, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			// TODO: Stop when any instance can evaluate to 'true'.

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileConditionalAndExpression(location, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				ResultInfo opResult = CompileSimpleOperation(location, Operation.BooleanOR, value, expressionValue, out value);
				if(opResult.Failure) return location.GetLocation(operators[i - 1]) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles a boolean 'AND' expression:
		/// <code>
		/// conditional_and_expression
		///   : inclusive_or_expression ( BOOLEAN_AND inclusive_or_expression )?
		///   ;
		/// </code>
		/// </summary>
		/// <param name="conditional_and_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileConditionalAndExpression(CompileArguments location, ConditionalAndExpressionContext conditional_and_expression, out IInstance value) {

			#region Argument Checks
			if(conditional_and_expression is null)
				throw new ArgumentNullException(nameof(conditional_and_expression));
			#endregion

			InclusiveOrExpressionContext[] expressions = conditional_and_expression.inclusive_or_expression();
			ITerminalNode[] operators = conditional_and_expression.BOOLEAN_AND();

			ResultInfo firstResult = CompileInclusiveOrExpression(location, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			// TODO: Stop when any instance can evaluate to 'false'.

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileInclusiveOrExpression(location, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				ResultInfo opResult = CompileSimpleOperation(location, Operation.BooleanAND, value, expressionValue, out value);
				if(opResult.Failure) return location.GetLocation(operators[i - 1]) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles a bitwise 'OR' expression:
		/// <code>
		/// inclusive_or_expression
		///   : exclusive_or_expression ( BITWISE_OR exclusive_or_expression )?
		///   ;
		/// </code>
		/// </summary>
		/// <param name="inclusive_or_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileInclusiveOrExpression(CompileArguments location, InclusiveOrExpressionContext inclusive_or_expression, out IInstance value) {

			#region Argument Checks
			if(inclusive_or_expression is null)
				throw new ArgumentNullException(nameof(inclusive_or_expression));
			#endregion

			ExclusiveOrExpressionContext[] expressions = inclusive_or_expression.exclusive_or_expression();
			ITerminalNode[] operators = inclusive_or_expression.BITWISE_OR();

			ResultInfo firstResult = CompileExclusiveOrExpression(location, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileExclusiveOrExpression(location, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				ResultInfo opResult = CompileSimpleOperation(location, Operation.BitwiseOR, value, expressionValue, out value);
				if(opResult.Failure) return location.GetLocation(operators[i - 1]) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles a bitwise 'XOR' expression:
		/// <code>
		/// exclusive_or_expression
		///   : and_expression ( BITWISE_XOR and_expression )?
		///   ;
		/// </code>
		/// </summary>
		/// <param name="exclusive_or_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileExclusiveOrExpression(CompileArguments location, ExclusiveOrExpressionContext exclusive_or_expression, out IInstance value) {

			#region Argument Checks
			if(exclusive_or_expression is null)
				throw new ArgumentNullException(nameof(exclusive_or_expression));
			#endregion

			AndExpressionContext[] expressions = exclusive_or_expression.and_expression();
			ITerminalNode[] operators = exclusive_or_expression.BITWISE_XOR();

			ResultInfo firstResult = CompileAndExpression(location, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileAndExpression(location, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				ResultInfo opResult = CompileSimpleOperation(location, Operation.BitwiseXOR, value, expressionValue, out value);
				if(opResult.Failure) return location.GetLocation(operators[i - 1]) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles a bitwise 'AND' expression:
		/// <code>
		/// and_expression
		///   : equality_expression ( BITWISE_AND equality_expression )?
		///   ;
		/// </code>
		/// </summary>
		/// <param name="and_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileAndExpression(CompileArguments location, AndExpressionContext and_expression, out IInstance value) {

			#region Argument Checks
			if(and_expression is null)
				throw new ArgumentNullException(nameof(and_expression));
			#endregion

			EqualityExpressionContext[] expressions = and_expression.equality_expression();
			ITerminalNode[] operators = and_expression.BITWISE_AND();

			ResultInfo firstResult = CompileEqualityExpression(location, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileEqualityExpression(location, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				ResultInfo opResult = CompileSimpleOperation(location, Operation.BitwiseAND, value, expressionValue, out value);
				if(opResult.Failure) return location.GetLocation(operators[i - 1]) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles an equality expression:
		/// <code>
		/// equality_expression
		///   : relational_expression ( equality_operator relational_expression )?
		///   ;
		/// </code>
		/// </summary>
		/// <param name="equality_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileEqualityExpression(CompileArguments location, EqualityExpressionContext equality_expression, out IInstance value) {

			#region Argument Checks
			if(equality_expression is null)
				throw new ArgumentNullException(nameof(equality_expression));
			#endregion

			RelationalExpressionContext[] expressions = equality_expression.relational_expression();
			MCSharpParser.Equality_operatorContext[] operators = equality_expression.equality_operator();

			ResultInfo firstResult = CompileRelationalExpression(location, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileRelationalExpression(location, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				Operation op;
				MCSharpParser.Equality_operatorContext equality_operator = operators[i - 1];
				if(equality_operator.EQUIVALENT() != null) op = Operation.Equality;
				else op = Operation.Inequality;

				ResultInfo opResult = CompileSimpleOperation(location, op, value, expressionValue, out value);
				if(opResult.Failure) return location.GetLocation(equality_operator) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles a relational expression:
		/// <code>
		/// relational_expression
		///   : shift_expression ( relation_or_type_check )*
		///   ;
		/// </code>
		/// </summary>
		/// <param name="relational_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileRelationalExpression(CompileArguments location, RelationalExpressionContext relational_expression, out IInstance value) {

			#region Argument Checks
			if(relational_expression is null)
				throw new ArgumentNullException(nameof(relational_expression));
			#endregion

			ResultInfo shift_result = CompileShiftExpression(location, relational_expression.shift_expression(), out value);
			if(shift_result.Failure) { value = null; return shift_result; }

			RelationOrTypeCheckContext[] relation_or_type_checks = relational_expression.relation_or_type_check();
			int count = relation_or_type_checks?.Length ?? 0;
			for(int i = 0; i < count; i++) {

				ResultInfo rotcResult = CompileRelationOrTypeCheck(location, value, relation_or_type_checks[i], out value);
				if(rotcResult.Failure) return rotcResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles a relation or type check expression:
		/// <code>
		/// relation_or_type_check
		///   : relation_operator shift_expression
		///   | ( IS | AS ) identifier
		///   ;
		/// </code>
		/// </summary>
		/// <param name="operandLeft">The left operand.</param>
		/// <param name="relation_or_type_check">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileRelationOrTypeCheck(CompileArguments location, IInstance operandLeft, RelationOrTypeCheckContext relation_or_type_check, out IInstance value) {

			#region Argument Checks
			if(relation_or_type_check is null)
				throw new ArgumentNullException(nameof(relation_or_type_check));
			#endregion

			MCSharpParser.Relation_operatorContext relation_operator = relation_or_type_check.relation_operator();
			if(relation_operator != null) {

				ShiftExpressionContext shift_expression = relation_or_type_check.shift_expression();
				ResultInfo expressionResult = CompileShiftExpression(location, shift_expression, out IInstance operandRight);
				if(expressionResult.Failure) { value = null; return expressionResult; }

				Operation op;
				if(relation_operator.LESS_THAN() != null) op = Operation.LessThan;
				else if(relation_operator.GREATER_THAN() != null) op = Operation.GreaterThan;
				else if(relation_operator.LESS_THAN_EQUAL() != null) op = Operation.LessThanOrEqual;
				else op = Operation.GreaterThanOrEqual;

				ResultInfo relationResult = CompileSimpleOperation(location, op, operandLeft, operandRight, out value);
				if(relationResult.Failure) return location.GetLocation(relation_or_type_check) + relationResult;

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

		/// <summary>
		/// Compiles a shift expression:
		/// <code>
		/// shift_expression
		///   : additive_expression ( shift_operator additive_expression )*
		///   ;
		/// </code>
		/// </summary>
		/// <param name="shift_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileShiftExpression(CompileArguments location, ShiftExpressionContext shift_expression, out IInstance value) {

			#region Argument Checks
			if(shift_expression is null)
				throw new ArgumentNullException(nameof(shift_expression));
			#endregion

			var additive_expressions = shift_expression.additive_expression();
			int count = additive_expressions.Length;
			ResultInfo[] additive_results = new ResultInfo[count];
			IInstance[] additive_values = new IInstance[count];

			value = null;

			additive_results[0] = CompileAdditiveExpression(location, additive_expressions[0], out additive_values[0]);
			if(additive_results[0].Failure) return additive_results[0];

			if(count == 2 && additive_expressions[1] != null) {

				additive_results[1] = CompileAdditiveExpression(location, additive_expressions[1], out additive_values[1]);
				if(additive_results[1].Failure) return additive_results[1];

				throw new NotImplementedException("Shift expressions have not been implemented.");

			} else {

				value = additive_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		/// <summary>
		/// Compiles an additive expression:
		/// <code>
		/// additive_expression
		///   : multiplicative_expression ( additive_operator multiplicative_expression )*
		///   ;
		/// </code>
		/// </summary>
		/// <param name="additive_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileAdditiveExpression(CompileArguments location, AdditiveExpressionContext additive_expression, out IInstance value) {

			#region Argument Checks
			if(additive_expression is null)
				throw new ArgumentNullException(nameof(additive_expression));
			#endregion

			MultiplicativeExpressionContext[] expressions = additive_expression.multiplicative_expression();
			MCSharpParser.Additive_operatorContext[] operators = additive_expression.additive_operator();

			ResultInfo firstResult = CompileMultiplicativeExpression(location, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileMultiplicativeExpression(location, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				Operation op;
				MCSharpParser.Additive_operatorContext additive_operator = operators[i - 1];
				if(additive_operator.PLUS() != null) op = Operation.Addition;
				else op = Operation.Subtraction;

				ResultInfo opResult = CompileSimpleOperation(location, op, value, expressionValue, out value);
				if(opResult.Failure) return location.GetLocation(additive_operator) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles a multiplicative expression:
		/// <code>
		/// multiplicative_expression
		///   : with_expression ( multiplicative_operator with_expression )*
		///   ;
		/// </code>
		/// </summary>
		/// <param name="multiplicative_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileMultiplicativeExpression(CompileArguments location, MultiplicativeExpressionContext multiplicative_expression, out IInstance value) {

			#region Argument Checks
			if(multiplicative_expression is null)
				throw new ArgumentNullException(nameof(multiplicative_expression));
			#endregion

			WithExpressionContext[] expressions = multiplicative_expression.with_expression();
			MCSharpParser.Multiplicative_operatorContext[] operators = multiplicative_expression.multiplicative_operator();

			ResultInfo firstResult = CompileWithExpression(location, expressions[0], out value);
			if(firstResult.Failure) return firstResult;

			int count = expressions.Length;
			for(int i = 1; i < count; i++) {

				ResultInfo exResult = CompileWithExpression(location, expressions[i], out IInstance expressionValue);
				if(exResult.Failure) return exResult;

				Operation op;
				MCSharpParser.Multiplicative_operatorContext multiplicative_operator = operators[i - 1];
				if(multiplicative_operator.MULTIPLY() != null) op = Operation.Multiplication;
				else if(multiplicative_operator.DIVIDE() != null) op = Operation.Division;
				else op = Operation.Modulo;

				ResultInfo opResult = CompileSimpleOperation(location, op, value, expressionValue, out value);
				if(opResult.Failure) return location.GetLocation(multiplicative_operator) + opResult;

			}

			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles a with expression:
		/// <code>
		/// with_expression
		///   : range_expression ( WITH anonymous_element_initializer )?
		///   ;
		/// </code>
		/// </summary>
		/// <param name="with_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileWithExpression(CompileArguments location, WithExpressionContext with_expression, out IInstance value) {

			#region Argument Checks
			if(with_expression is null)
				throw new ArgumentNullException(nameof(with_expression));
			#endregion

			value = null;

			RangeExpressionContext range_expression = with_expression.range_expression();
			ResultInfo range_result = CompileRangeExpression(location, range_expression, out IInstance range_value);
			if(range_result.Failure) return range_result;

			MCSharpParser.Anonymous_element_initializerContext anonymous_element_initializer = with_expression.anonymous_element_initializer();
			if(anonymous_element_initializer != null) {

				throw new NotImplementedException("With epxressions have not been implemented.");

			}

			value = range_value;
			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles a range expression:
		/// <code>
		/// range_expression
		///   : unary_expression ( range_operator unary_expression )?
		///   ;
		/// </code>
		/// </summary>
		/// <param name="range_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileRangeExpression(CompileArguments location, RangeExpressionContext range_expression, out IInstance value) {

			#region Argument Checks
			if(range_expression is null)
				throw new ArgumentNullException(nameof(range_expression));
			#endregion

			UnaryExpressionContext[] unary_expressions = range_expression.unary_expression();
			int count = unary_expressions.Length;
			ResultInfo[] unary_results = new ResultInfo[count];
			IInstance[] unary_values = new IInstance[count];

			value = null;

			unary_results[0] = CompileUnaryExpression(location, unary_expressions[0], out unary_values[0]);
			if(unary_results[0].Failure) return unary_results[0];

			if(count == 2 && unary_expressions[1] != null) {

				unary_results[1] = CompileUnaryExpression(location, unary_expressions[1], out unary_values[1]);
				if(unary_results[1].Failure) return unary_results[1];

				throw new NotImplementedException("Range expressions have not been implemented.");

			} else {

				value = unary_values[0];
				return ResultInfo.DefaultSuccess;

			}

		}

		/// <summary>
		/// Compiles a pre-step expression:
		/// <code>
		/// pre_step_expression
		///   : pre_step_operator unary_expression
		///   ;
		/// </code>
		/// </summary>
		/// <param name="pre_step_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompilePreStepExpression(CompileArguments location, PreStepExpressionContext pre_step_expression, out IInstance value) {

			#region Argument Checks
			if(pre_step_expression is null)
				throw new ArgumentNullException(nameof(pre_step_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		/// <summary>
		/// Compiles a post-step expression:
		/// <code>
		/// post_step_expression
		///   : post_step_operator unary_expression
		///   ;
		/// </code>
		/// </summary>
		/// <param name="post_step_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompilePostStepExpression(CompileArguments location, PostStepExpressionContext post_step_expression, out IInstance value) {

			#region Argument Checks
			if(post_step_expression is null)
				throw new ArgumentNullException(nameof(post_step_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		/// <summary>
		/// Compiles a unary expression:
		/// <code>
		/// unary_expression
		///   : primary_expression
		///   | unary_operator unary_expression
		///   | pre_step_expression
		///   | cast_expression
		///   | pointer_indirection_expression
		///   | address_of_expression
		///   ;
		/// </code>
		/// </summary>
		/// <param name="unary_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileUnaryExpression(CompileArguments location, UnaryExpressionContext unary_expression, out IInstance value) {

			#region Argument Checks
			if(unary_expression is null)
				throw new ArgumentNullException(nameof(unary_expression));
			#endregion

			PrimaryExpressionContext primary_expression = unary_expression.primary_expression();
			if(primary_expression != null) {

				ResultInfo result = CompilePrimaryExpression(location, primary_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			UnaryExpressionContext subunary_expression = unary_expression.unary_expression();
			if(subunary_expression != null) {

				value = null;

				ResultInfo result = CompileUnaryExpression(location, subunary_expression, out IInstance unary_value);
				if(result.Failure) return result;

				throw new NotImplementedException("Unary expressions have not been implemented.");

			}

			PreStepExpressionContext pre_step_expression = unary_expression.pre_step_expression();
			if(pre_step_expression != null) {

				ResultInfo result = CompilePreStepExpression(location, pre_step_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			CastExpressionContext cast_expression = unary_expression.cast_expression();
			if(cast_expression != null) {

				ResultInfo result = CompileCastExpression(location, cast_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			PointerIndirectionExpressionContext pointer_indirection_expression = unary_expression.pointer_indirection_expression();
			if(pointer_indirection_expression != null) {

				ResultInfo result = CompilePointerIndirectionExpression(location, pointer_indirection_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			AddressofExpressionContext addressof_expression = unary_expression.addressof_expression();
			if(addressof_expression != null) {

				ResultInfo result = CompileAddressofExpression(location, addressof_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Unary expression could not be determined.");

		}

		/// <summary>
		/// Compiles a cast expression:
		/// <code>
		/// cast_expression
		///   : OP NAME CP unary_expression
		///   ;
		/// </code>
		/// </summary>
		/// <param name="cast_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileCastExpression(CompileArguments location, CastExpressionContext cast_expression, out IInstance value) {

			#region Argument Checks
			if(cast_expression is null)
				throw new ArgumentNullException(nameof(cast_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		/// <summary>
		/// Compiles a pointer indirection expression:
		/// <code>
		/// pointer_indirection_expression
		///   : MULTIPLY unary_expression
		///   ;
		/// </code>
		/// </summary>
		/// <param name="pointer_indirection_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompilePointerIndirectionExpression(CompileArguments location, PointerIndirectionExpressionContext pointer_indirection_expression, out IInstance value) {

			#region Argument Checks
			if(pointer_indirection_expression is null)
				throw new ArgumentNullException(nameof(pointer_indirection_expression));
			#endregion

			throw new NotImplementedException($"{MethodBase.GetCurrentMethod().Name} has not been implemented.");

		}

		/// <summary>
		/// Compiles an address of expression:
		/// <code>
		/// address_of_expression
		///   : BITWISE_AND unary_expression
		///   ;
		/// </code>
		/// </summary>
		/// <param name="address_of_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileAddressofExpression(CompileArguments location, AddressofExpressionContext addressof_expression, out IInstance value) {

			#region Argument Checks
			if(addressof_expression is null)
				throw new ArgumentNullException(nameof(addressof_expression));
			#endregion

			value = null;

			UnaryExpressionContext unary_expression = addressof_expression.unary_expression();
			ResultInfo unary_result = CompileUnaryExpression(location, unary_expression, out IInstance unary_value);
			if(unary_result.Failure) return unary_result;

			throw new NotImplementedException("Addressof expressions have not been implemented.");

		}

		/// <summary>
		/// Compiles an assignment expression:
		/// <code>
		/// assignment_expression
		///   : unary_expression assignment_operator expression
		///   ;
		/// </code>
		/// </summary>
		/// <param name="assignment_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileAssignmentExpression(CompileArguments location, AssignmentExpressionContext assignment_expression, out IInstance value) {

			#region Argument Checks
			if(assignment_expression is null)
				throw new ArgumentNullException(nameof(assignment_expression));
			#endregion

			value = null;

			UnaryExpressionContext unary_expression = assignment_expression.unary_expression();
			ResultInfo unary_result = CompileUnaryExpression(location, unary_expression, out IInstance unary_value);
			if(unary_result.Failure) return unary_result;

			ExpressionContext expression = assignment_expression.expression();
			ResultInfo expression_result = CompileExpression(location, expression, out IInstance expression_value);
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

			ResultInfo simple_operation_result = CompileSimpleOperation(location, op, unary_value, expression_value, out value);
			if(simple_operation_result.Failure) return location.GetLocation(assignment_expression) + simple_operation_result;

			return ResultInfo.DefaultSuccess;

		}

		/// <summary>
		/// Compiles a primary expression:
		/// <code>
		/// primary_expression
		///   : array_creation_expression
		///   : primary_no_array_creation_expression
		///   ;
		/// </code>
		/// </summary>
		/// <param name="primary_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompilePrimaryExpression(CompileArguments location, PrimaryExpressionContext primary_expression, out IInstance value) {

			#region Argument Checks
			if(primary_expression is null)
				throw new ArgumentNullException(nameof(primary_expression));
			#endregion

			value = null;

			ArrayCreationExpressionContext array_creation_expression = primary_expression.array_creation_expression();
			if(array_creation_expression != null) {

				ResultInfo result = CompileArrayCreationExpression(location, array_creation_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			PrimaryNoArrayCreationExpressionContext primary_no_array_creation_expression = primary_expression.primary_no_array_creation_expression();
			if(primary_no_array_creation_expression != null) {

				ResultInfo result = CompilePrimaryNoArrayCreationExpression(location, primary_no_array_creation_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Primary expression could not be determined.");

		}

		/// <summary>
		/// Compiles an array creation expression:
		/// <code>
		/// array_creation_expression
		///   : NEW indexer_arguments array_rank_specifier? array_initializer?
		///   ;
		/// </code>
		/// </summary>
		/// <param name="array_creation_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileArrayCreationExpression(CompileArguments location, ArrayCreationExpressionContext array_creation_expression, out IInstance value) {

			#region Argument Checks
			if(array_creation_expression is null)
				throw new ArgumentNullException(nameof(array_creation_expression));
			#endregion

			throw new NotImplementedException("Array-creation expression evaluation has not been implented.");

		}

		/// <summary>
		/// Compiles a primary no-array-creation expression:
		/// <code>
		/// primary_no_array_creation_expression
		///   : literal
		///   : short_identifier
		///   : OP expression CP
		///   : member_access
		///   : post_step_expression
		///   : keyword_expression
		///   ;
		/// </code>
		/// </summary>
		/// <param name="primary_no_array_creation_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompilePrimaryNoArrayCreationExpression(CompileArguments location, PrimaryNoArrayCreationExpressionContext primary_no_array_creation_expression, out IInstance value) {

			#region Argument Checks
			if(primary_no_array_creation_expression is null)
				throw new ArgumentNullException(nameof(primary_no_array_creation_expression));
			#endregion

			MCSharpParser.LiteralContext literal = primary_no_array_creation_expression.literal();
			if(literal != null) {

				ResultInfo result = CompileLiteral(location, literal, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			MCSharpParser.Short_identifierContext identifier = primary_no_array_creation_expression.short_identifier();
			if(identifier != null) {

				string name = identifier.NAME().GetText();

				value = location.Scope.FindFirstInstanceByName(name);
				if(value == null) return new ResultInfo(false, $"{location.GetLocation(identifier)}Instance '{name}' does not exist yet in this scope.");

				return ResultInfo.DefaultSuccess;

			}

			ExpressionContext expression = primary_no_array_creation_expression.expression();
			if(expression != null) {

				ResultInfo result = CompileExpression(location, expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			MCSharpParser.Member_accessContext member_access = primary_no_array_creation_expression.member_access();
			if(member_access != null) {

				ResultInfo result = CompileMemberAccess(location, member_access, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			PostStepExpressionContext post_step_expression = primary_no_array_creation_expression.post_step_expression();
			if(post_step_expression != null) {

				throw new NotImplementedException("Post-step expression evaluation have not been implemented.");

			}

			KeywordExpressionContext keyword_expression = primary_no_array_creation_expression.keyword_expression();
			if(keyword_expression != null) {

				ResultInfo result = CompileKeywordExpression(location, keyword_expression, out value);
				if(result.Failure) return result;

				return ResultInfo.DefaultSuccess;

			}

			throw new Exception("Primary no-array-creation expression could not be determined.");

		}

		/// <summary>
		/// Compiles a member access expression:
		/// <code>
		/// member_access
		///   : member_access_prefix* short_identifier generic_arguments? ( method_arguments | indexer_arguments )?
		///   ;
		/// </code>
		/// </summary>
		/// <param name="member_access">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, ExpressionContext, out IInstance)"/>
		public ResultInfo CompileMemberAccess(CompileArguments location, MCSharpParser.Member_accessContext member_access, out IInstance value) {

			MCSharpParser.Member_access_prefixContext[] member_access_prefixes = member_access.member_access_prefix();
			MCSharpParser.Short_identifierContext member_identifier = member_access.short_identifier();
			MCSharpParser.Generic_argumentsContext generic_arguments = member_access.generic_arguments();
			MCSharpParser.Method_argumentsContext method_arguments = member_access.method_arguments();
			MCSharpParser.Indexer_argumentsContext indexer_arguments = member_access.indexer_arguments();

			(IInstance Instance, IType Type) holder = (null, null);

			// Access a member of an instance (holder).
			ResultInfo AccessFromInstance(CompileArguments location, IInstance holder, ITerminalNode identifier, out IInstance value,
				MCSharpParser.Generic_argumentsContext generic_arguments, MCSharpParser.Method_argumentsContext method_arguments, MCSharpParser.Indexer_argumentsContext indexer_arguments) {

				// Get the accessed member by name.
				IMember accessedMember = null;
				foreach(IMember member in holder.Type.Members) {
					if(member.Identifier == identifier.GetText()) {
						accessedMember = member;
						break;
					}
				}

				// TODO: Check inherited types.

				if(accessedMember == null) {
					
					// Accessed member does not exist.
					value = null;
					return new ResultInfo(false, $"{location.GetLocation(identifier)}'{identifier.GetText()}' does not exist in type '{holder.Type.Identifier}'.");

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
								return new ResultInfo(false, location.GetLocation(identifier)+"This property is not get-able.");
							}

							// Invoke 'get' method.
							ResultInfo result = property.Getter.Invoke(location, new IType[] { }, new IInstance[] { }, out value);
							if(result.Failure) return location.GetLocation(identifier) + result;

							return ResultInfo.DefaultSuccess;

						}

						case IMethod method: {

							// Get generic arguments.
							IType[] generics;
							if(generic_arguments == null) {
								generics = new IType[] { };
							} else {
								throw new NotImplementedException(location.GetLocation(generic_arguments)+"Invoking methods with generic arguments has not been implemented.");
							}

							// Get method arguments.
							IInstance[] arguments;
							if(method_arguments == null) {
								value = null;
								return new ResultInfo(false, $"{location.GetLocation(member_access)}Expected method arguments for accessing method.");
							} else {
								MCSharpParser.Argument_listContext argument_list = method_arguments.argument_list();
								MCSharpParser.ArgumentContext[] arguments_context = argument_list.argument();
								arguments = new IInstance[arguments_context.Length];
								for(int i = 0; i < arguments_context.Length; i++) {
									ResultInfo argument_result = CompileExpression(location, arguments_context[i].expression(), out IInstance argument);
									if(argument_result.Failure)  {
										value = null;
										return argument_result;
									}
									arguments[i] = argument;
								}
							}

							// Invoke method.
							ResultInfo result = method.Invoker.Invoke(location, generics, arguments, out value);
							if(result.Failure) return location.GetLocation(member_access) + result;

							return ResultInfo.DefaultSuccess;

						}

						default: throw new Exception($"Unsupported type of {nameof(IMember)}: '{definition.GetType().FullName}'.");
					}

				}

			}

			// Access a member of a type (holder).
			ResultInfo AccessFromStaticType(CompileArguments location, IType holder, ITerminalNode identifier, out IInstance value,
				MCSharpParser.Generic_argumentsContext generic_arguments, MCSharpParser.Method_argumentsContext method_arguments, MCSharpParser.Indexer_argumentsContext indexer_arguments) {

				// Get the accessed member by name.
				IMember accessedMember = null;
				foreach(IMember member in holder.Members) {
					if((member.Modifiers & Modifier.Static) != 0 && member.Identifier == identifier.GetText()) {
						accessedMember = member;
						break;
					}
				}

				// TODO: Check inherited types.

				if(accessedMember == null) {
					
					// Accessed member does not exist.
					value = null;
					return new ResultInfo(false, location.GetLocation(identifier) + $"'{identifier.GetText()}' does not exist static in type '{holder.Identifier}'.");

				} else {

					IMemberDefinition definition = accessedMember.Definition;

					switch(definition) {

						case IField field: {

							throw new NotImplementedException(location.GetLocation(identifier)+"Accessing static fields has not been implemented.");

						}

						case IProperty property: {

							// Get getter.
							var getter = property.Getter;
							if(getter == null) {
								value = null;
								return new ResultInfo(false, location.GetLocation(identifier)+"This property is not get-able.");
							}

							// Invoke 'get' method.
							ResultInfo result = property.Getter.Invoke(location, new IType[] { }, new IInstance[] { }, out value);
							if(result.Failure) return location.GetLocation(identifier) + result;

							return ResultInfo.DefaultSuccess;

						}

						case IMethod method: {

							// Get generic arguments.
							IType[] generics;
							if(generic_arguments == null) {
								generics = new IType[] { };
							} else {
								throw new NotImplementedException(location.GetLocation(generic_arguments)+"Invoking methods with generic arguments has not been implemented.");
							}

							// Get method arguments.
							IInstance[] arguments;
							if(method_arguments == null) {
								value = null;
								return new ResultInfo(false, $"{location.GetLocation(member_access)}Expected method arguments for accessing method.");
							} else {

								MCSharpParser.Argument_listContext argument_list = method_arguments.argument_list();
								if(argument_list != null) {

									MCSharpParser.ArgumentContext[] arguments_context = argument_list.argument();
									arguments = new IInstance[arguments_context.Length];

									for(int i = 0; i < arguments_context.Length; i++) {

										ResultInfo argument_result = CompileExpression(location, arguments_context[i].expression(), out IInstance argument);
										if(argument_result.Failure) {
											value = null;
											return argument_result;
										}

										arguments[i] = argument;

									}

								} else {

									arguments = new IInstance[] { };

                                }

							}

							// Invoke method.
							ResultInfo result = method.Invoker.Invoke(location, generics, arguments, out value);
							if(result.Failure) return location.GetLocation(member_access) + result;

							return ResultInfo.DefaultSuccess;

						}

						default: throw new Exception($"Unsupported type of {nameof(IMember)}: '{definition.GetType().FullName}'.");
					}

				}

			}

			// Get the instance to access from.

			// Evaluate chain.
			if(member_access_prefixes.Length > 0) {

				{

					var prefix = member_access_prefixes[0];

					// Start of chain (set holder).
					ArrayCreationExpressionContext prefix_array_creation_expression;
					MCSharpParser.LiteralContext prefix_literal;
					MCSharpParser.Short_identifierContext prefix_short_identifier;
					ExpressionContext prefix_expression;
					PostStepExpressionContext prefix_post_step_expression;
					KeywordExpressionContext prefix_keyword_expression;

					if((prefix_array_creation_expression = prefix.array_creation_expression()) != null) {

						// Accessing starts at a new array.
						ResultInfo prefixResult = CompileArrayCreationExpression(location, prefix_array_creation_expression, out IInstance instanceHolder);
						if(prefixResult.Failure) {
							value = null;
							return location.GetLocation(prefix_array_creation_expression) + prefixResult;
						}

						holder = (instanceHolder, null);

					} else if((prefix_literal = prefix.literal()) != null) {

						// Accessing starts at a new instance created by a literal.
						ResultInfo prefixResult = CompileLiteral(location, prefix_literal, out IInstance instanceHolder);
						if(prefixResult.Failure) {
							value = null;
							return location.GetLocation(prefix_literal) + prefixResult;
						}

						holder = (instanceHolder, null);

					} else if((prefix_short_identifier = prefix.short_identifier()) != null) {

						// Accessing starts at something found by a short identifier, possibly with arguments.

						// Get argument contexts.
						MCSharpParser.Generic_argumentsContext prefix_generic_arguments = prefix.generic_arguments();
						MCSharpParser.Method_argumentsContext prefix_method_arguments = prefix.method_arguments();
						MCSharpParser.Indexer_argumentsContext prefix_indexer_arguments = prefix.indexer_arguments();

						// Get identifier node and string.
						ITerminalNode identifier = prefix_short_identifier.NAME();
						string name = identifier.GetText();

						// Local method for accessing through "this" keyword.
						ResultInfo AccessFromThis(Scope scope = null) {

							// Access "this".
							if(scope == null) scope = location.Scope;
							var _holder = scope.Holder;

							if(_holder == null) {

								// "this" does not exist.
								return new ResultInfo(success: false, location.GetLocation(identifier)+"Accessing 'this' is not possible here.");

							} else if(_holder is IInstance instance) {

								// "this" is an instance.
								holder = (instance, null);
								return ResultInfo.DefaultSuccess;

							} else if(_holder is IType type) {

								// "this" is a type.
								var staticResult = AccessFromStaticType(location, type, identifier, out IInstance instanceHolder, prefix_generic_arguments, prefix_method_arguments, prefix_indexer_arguments);
								holder = (instanceHolder, null);
								return staticResult;

							} else {

								// "this" is a member of a type.
								return AccessFromThis(scope.Parent);

							}

						}

						// Local method for accessing through "base" keyword.
						ResultInfo AccessFromBase() {

							throw new NotImplementedException(location.GetLocation(identifier)+"Accessing 'base' has not been implemented.");

						}

						// Options:

						if(name == "this") {
							
							// (1) Accessing through "this".
							var thisResult = AccessFromThis();
							if(thisResult.Failure) {
								value = null;
								return thisResult;
							}

						} else if(name == "base") {

							// (2) Accessing through "base".
							var baseResult = AccessFromBase();
							if(baseResult.Failure) {
								value = null;
								return baseResult;
							}

						} else {

							// (3) Accessing through a member of a type.
							var instanceHolder = location.Scope.FindFirstInstanceByName(name);

                            if(instanceHolder != null) {

								holder = (instanceHolder, null);

                            } else {

                                // (4) The identifier requires an implicit 'this' call.
                                var thisResult = AccessFromThis();

                                if(thisResult.Failure) {

                                    if(DefinedTypes.ContainsKey(name)) {

                                		// (5) The identifier is a type name.
										holder = (null, DefinedTypes[name]);

                                    } else {
                                        value = null;
                                        return location.GetLocation(identifier) + new ResultInfo(success: false, $"Unknown identifier: '{name}'.");
                                    }

                                }

                            }

                        }

					} else if((prefix_expression = prefix.expression()) != null) {

						// Accessing starts at the result of an expression.
						ResultInfo prefixResult = CompileExpression(location, prefix_expression, out IInstance instanceHolder);
						if(prefixResult.Failure) {
							value = null;
							return location.GetLocation(prefix_expression) + prefixResult;
						}

						holder = (instanceHolder, null);

					} else if((prefix_post_step_expression = prefix.post_step_expression()) != null) {

						// Accessing starts at the result of a post-step expression.
						ResultInfo prefixResult = CompilePostStepExpression(location, prefix_post_step_expression, out IInstance instanceHolder);
						if(prefixResult.Failure) {
							value = null;
							return location.GetLocation(prefix_post_step_expression) + prefixResult;
						}

						holder = (instanceHolder, null);

					} else if((prefix_keyword_expression = prefix.keyword_expression()) != null) {

						// Accessing starts at the result of a keyword expression.
						ResultInfo prefixResult = CompileKeywordExpression(location, prefix_keyword_expression, out IInstance instanceHolder);
						if(prefixResult.Failure) {
							value = null;
							return prefixResult;
						}

						holder = (instanceHolder, null);

					} else {

						throw new Exception("Member access prefix (start) could not be determined.");

					}

				}

				for(int i = 1; i < member_access_prefixes.Length; i++) {

					var prefix = member_access_prefixes[i];
					MCSharpParser.Short_identifierContext prefix_short_identifier;
					PostStepExpressionContext prefix_post_step_expression;

					// Access from holder.

					if((prefix_short_identifier = prefix.short_identifier()) != null) {

						MCSharpParser.Generic_argumentsContext prefix_generic_arguments = prefix.generic_arguments();
						MCSharpParser.Method_argumentsContext prefix_method_arguments = prefix.method_arguments();
						MCSharpParser.Indexer_argumentsContext prefix_indexer_arguments = prefix.indexer_arguments();

						ITerminalNode identifier = prefix_short_identifier.NAME();
						string name = identifier.GetText();

						if(holder.Type != null) {

							// Access from a type.
							IType typeHolder = holder.Type;

							ResultInfo result = AccessFromStaticType(location, typeHolder, identifier, out IInstance instanceHolder, prefix_generic_arguments, prefix_method_arguments, prefix_indexer_arguments);
							if(result.Failure) {
								value = null;
								return result;
							}

							holder = (instanceHolder, null);

						} else {

							// Access from an instance.
							IInstance instanceHolder = holder.Instance;
							
							ResultInfo result = AccessFromInstance(location, instanceHolder, identifier, out instanceHolder, prefix_generic_arguments, prefix_method_arguments, prefix_indexer_arguments);
							if(result.Failure) {
								value = null;
								return result;
							}

							holder = (instanceHolder, null);
							
						}

					} else if((prefix_post_step_expression = prefix.post_step_expression()) != null && prefix_post_step_expression.literal() != null) {

						ResultInfo prefixResult = CompilePostStepExpression(location, prefix_post_step_expression, out IInstance instance);
						if(prefixResult.Failure) {
							value = null;
							return location.GetLocation(prefix_post_step_expression) + prefixResult;
						}

						ITerminalNode identifier = prefix_post_step_expression.identifier().NAME()[0];
						string name = identifier.GetText();

						if(holder.Type != null) {

							// Access from a type.
							IType typeHolder = holder.Type;

							ResultInfo result = AccessFromStaticType(location, typeHolder, identifier, out IInstance instanceHolder, null, null, null);
							if(result.Failure) {
								value = null;
								return result;
							}

							holder = (instanceHolder, null);

						} else {

							// Access from an instance.
							IInstance instanceHolder = holder.Instance;
							
							ResultInfo result = AccessFromInstance(location, instanceHolder, identifier, out instanceHolder, null, null, null);
							if(result.Failure) {
								value = null;
								return result;
							}

							holder = (instanceHolder, null);
							
						}

					} else {

						value = null;
						return new ResultInfo(false, location.GetLocation(prefix) + "Expected an identifier to access a member.");

					}

				}

			} else {

				throw new NotImplementedException(location.GetLocation(member_access) + "Implicit 'this' access has not been implemented.");

			}
			
			// Final access in chain.
			if(holder.Type != null) {

				// Access from a type.
				IType typeHolder = holder.Type;

				ResultInfo finalResult = AccessFromStaticType(location, typeHolder, member_identifier.NAME(), out value, generic_arguments, method_arguments, indexer_arguments);
				if(finalResult.Failure) return finalResult;
				else return ResultInfo.DefaultSuccess;

			} else {

				// Access from an instance.
				IInstance instanceHolder = holder.Instance;

				ResultInfo finalResult = AccessFromInstance(location, instanceHolder, member_identifier.NAME(), out value, generic_arguments, method_arguments, indexer_arguments);
				if(finalResult.Failure) return finalResult;
				else return ResultInfo.DefaultSuccess;

			}

		}

		/// <summary>
		/// Compiles a literal.
		/// </summary>
		/// <param name="location">The location of the literal.</param>
		/// <param name="literal">The literal to compile.</param>
		/// <param name="value">The instance to assign the literal to.</param>
		/// <returns>The result of the compilation.</returns>
		public ResultInfo CompileLiteral(CompileArguments location, MCSharpParser.LiteralContext literal, out IInstance value) {

			// Parse integer literal.
			ITerminalNode integer_literal = literal.INTEGER();
			if(integer_literal != null) {

				string text = integer_literal.GetText();
				int _value = int.Parse(text);

				IType type = DefinedTypes[MCSharpLinkerExtension.IntIdentifier];
				value = new PrimitiveInstance.IntegerInstance.Constant(type, null, _value);
				ResultInfo scopeResult = location.Scope.AddInstance(value);
				if(scopeResult.Failure) return location.GetLocation(integer_literal) + scopeResult;

				return ResultInfo.DefaultSuccess;

			}

			// Parse boolean literal.
			ITerminalNode boolean_literal = literal.BOOLEAN();
			if(boolean_literal != null) {

				string text = boolean_literal.GetText();
				bool _value = bool.Parse(text);

				IType type = DefinedTypes[MCSharpLinkerExtension.BoolIdentifier];
				value = new PrimitiveInstance.BooleanInstance.Constant(type, null, _value);
				ResultInfo scopeResult = location.Scope.AddInstance(value);
				if(scopeResult.Failure) return location.GetLocation(boolean_literal) + scopeResult;

				return ResultInfo.DefaultSuccess;

			}

			// Parse string literal.
			ITerminalNode string_literal = literal.STRING();
			if(string_literal != null) {

				string text = string_literal.GetText();
				string _value = text[1..^1];

				IType type = DefinedTypes[MCSharpLinkerExtension.StringIdentifier];
				value = new PrimitiveInstance.StringInstance(type, null, _value);
				ResultInfo scopeResult = location.Scope.AddInstance(value);
				if(scopeResult.Failure) return location.GetLocation(string_literal) + scopeResult;

				return ResultInfo.DefaultSuccess;

			}

			// Parse decimal literal.
			ITerminalNode decimal_literal = literal.DECIMAL();
			if(decimal_literal != null) {

				string text = decimal_literal.GetText();
				double _value = double.Parse(text);

				throw new NotImplementedException("Decimal/float/ect. literals have not been implemented.");

			}

			throw new Exception("Literal context could not be determined.");

		}

		/// <summary>
		/// Compiles a keyword expression:
		/// <code>
		/// keyword_expression
		///   : NEW NAME ( ( method_arguments object_or_collection_initializer? ) | ( object_or_collection_initializer ) )
		///   | NEW anonymous_object_initializer
		///   ;
		/// </code>
		/// </summary>
		/// <param name="keyword_expression">The expression to compile.</param>
		/// <inheritdoc cref="CompileExpression(CompileArguments, MCSharpParser.ExpressionContext, out IInstance)"/>
		public ResultInfo CompileKeywordExpression(CompileArguments location, KeywordExpressionContext keyword_expression, out IInstance value) {

			MCSharpParser.New_keyword_expressionContext new_keyword_expression = keyword_expression.new_keyword_expression();
			if(new_keyword_expression != null) {

				// NEW NAME ( ( method_arguments object_or_collection_initializer? ) | ( object_or_collection_initializer ) )
				MCSharpParser.Method_argumentsContext method_arguments = new_keyword_expression.method_arguments();
				MCSharpParser.Object_or_collection_initializerContext object_or_collection_initializer = new_keyword_expression.object_or_collection_initializer();
				if(method_arguments != null || object_or_collection_initializer != null) {

					ITerminalNode nameNode = new_keyword_expression.NAME();
					string name = nameNode.GetText();

					if(object_or_collection_initializer != null) {

						// NEW NAME method_arguments? object_or_collection_initializer

						throw new NotImplementedException("Object/collection initializers for keyword 'new' expressions have not been implemented.");

					} else {

						// NEW NAME method_arguments

						if(!DefinedTypes.ContainsKey(name)) {
							value = null;
							return new ResultInfo(false, location.GetLocation(nameNode) + $"Type '{name}' does not exist.");
						}
						IType type = DefinedTypes[name];

						// Get method arguments.
						IInstance[] methodArguments;
						{
							MCSharpParser.Argument_listContext argument_list = method_arguments.argument_list();
							MCSharpParser.ArgumentContext[] arguments_context = argument_list?.argument() ?? new MCSharpParser.ArgumentContext[] { };
							methodArguments = new IInstance[arguments_context.Length];
							for(int i = 0; i < arguments_context.Length; i++) {
								MCSharpParser.ArgumentContext argument_context = arguments_context[i];
								IInstance argument;
								ResultInfo argument_result = CompileExpression(location, argument_context.expression(), out argument);
								if(argument_result.Failure) {
									value = null;
									return argument_result;
								}
								methodArguments[i] = argument;
							}
						}
						
						// Get generic arguments.
						var genericArguments = new IType[] { };

						// Get constructor.
						var constructor = type.FindBestConstructor(genericArguments, methodArguments);

						// Invoke constructor.
						location.Writer.WriteComments(
							$"Invoke constructor: {type.Identifier}");
						ResultInfo result = constructor.Invoker.Invoke(location, new IType[] { }, methodArguments, out value);
						if(result.Failure) return location.GetLocation(new_keyword_expression) + result;

						return ResultInfo.DefaultSuccess;

					}

				}

				// NEW anonymous_object_initializer
				var anonymous_object_initializer = new_keyword_expression.anonymous_object_initializer();
				if(anonymous_object_initializer != null) {

					throw new NotImplementedException("Anonymous object initializers have not been implemented.");
				}

				throw new Exception("Keyword 'new' expression could not be determined.");

			}

			MCSharpParser.Typeof_keyword_expressionContext typeof_keyword_expression = keyword_expression.typeof_keyword_expression();
			if(typeof_keyword_expression != null) {
				
				// TYPEOF OP NAME CP

				ITerminalNode nameNode = typeof_keyword_expression.NAME();
				string name = nameNode.GetText();
				if(!DefinedTypes.ContainsKey(name)) {
					value = null;
					return new ResultInfo(false, location.GetLocation(nameNode) + $"Type '{name}' does not exist.");
				}

				IType type = DefinedTypes[name];
				value = new PrimitiveInstance.StringInstance(DefinedTypes[MCSharpLinkerExtension.StringIdentifier], null, type.Identifier);

				return ResultInfo.DefaultSuccess;

			}

			MCSharpParser.Checked_expressionContext checked_keyword_expression = keyword_expression.checked_expression();
			if(checked_keyword_expression != null) {
				
				// CHECKED OP expression CP
				throw new NotImplementedException("Keyword 'checked' expressions have not been implemented.");

			}

			MCSharpParser.Unchecked_expressionContext unchecked_keyword_expression = keyword_expression.unchecked_expression();
			if(unchecked_keyword_expression != null) {

				// UNCHECKED OP expression CP
				throw new NotImplementedException("Keyword 'unchecked' expressions have not been implemented.");

			}

			MCSharpParser.Default_keyword_expressionContext default_keyword_expression = keyword_expression.default_keyword_expression();
			if(default_keyword_expression != null) {

				// DEFAULT ( OP expression CP )?
				throw new NotImplementedException("Keyword 'default' expressions have not been implemented.");

			}

			MCSharpParser.Sizeof_keyword_expressionContext sizeof_keyword_expression = keyword_expression.sizeof_keyword_expression();
			if(sizeof_keyword_expression != null) {
				
				// SIZEOF OP NAME CP
				throw new NotImplementedException("Keyword 'sizeof' expressions have not been implemented.");

			}

			throw new Exception("Keyword expression could not be determined.");

		}

		#endregion

		#endregion

		#region IStatement Creation

		/// <summary>
		/// Creates an <see cref="IStatement"/> from a <see cref="StatementContext"/>.
		/// </summary>
		/// <param name="statement">The <see cref="StatementContext"/> to create the <see cref="IStatement"/> from.</param>
		/// <param name="predefined">Whether or not the <see cref="IStatement"/> is a <see cref="PredefinedStatement"/>.</param>
		/// <returns>The <see cref="IStatement"/> created.</returns>
		private IStatement CreateIStatement(StatementContext statement, bool predefined) {

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

		/// <summary>
		/// Creates an array of <see cref="IStatement"/>s from an array of <see cref="StatementContext"/>s.
		/// </summary>
		/// <param name="statements">The array of <see cref="StatementContext"/>s to create the <see cref="IStatement"/>s from.</param>
		/// <param name="predefined">Whether or not the <see cref="IStatement"/>s are <see cref="PredefinedStatement"/>s.</param>
		/// <returns>The array of <see cref="IStatement"/>s created.</returns>
		private IStatement[] CreateIStatements(StatementContext[] statements, bool predefined) {

			#region Argument Checks
			if(statements is null)
				throw new ArgumentNullException(nameof(statements));
			#endregion

			int length = statements.Length;
			IStatement[] array = new IStatement[length];

			if(predefined) {
				for(int i = 0; i < length; i++) {
					array[i] = new ScriptStatement(statements[i]);
				}
			} else {
				for(int i = 0; i < length; i++) {
					array[i] = new PredefinedStatement(statements[i]);
				}
			}

			return array;

		}

		/// <summary>
		/// Creates an array of <see cref="IStatement"/>s from a <see cref="StatementContext"/>.
		/// </summary>
		/// <param name="statement">The <see cref="StatementContext"/> to create the <see cref="IStatement"/>s from.</param>
		/// <param name="predefined">Whether or not the <see cref="IStatement"/>s are <see cref="PredefinedStatement"/>s.</param>
		/// <returns>The array of <see cref="IStatement"/>s created.</returns>
		private IStatement[] CreateIStatements(StatementContext statement, bool predefined) {

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

			// TODO: Remove this reliance on static.
			Objective.ClearAnonymousNames();

		}

		#region Subtypes

		/// <summary>
		/// Contains information to link source code to output code.
		/// </summary>
		public struct CompileArguments {

			/// <summary>
			/// The <see cref="Compilation.Compiler"/>.
			/// </summary>
			public Compiler Compiler { get; }

			/// <summary>
			/// The <see cref="StandaloneStatementFunction"/> being written to.
			/// </summary>
			public StandaloneStatementFunction Function { get; }

			/// <summary>
			/// The <see cref="FunctionWriter"/> of <see cref="Function"/>.
			/// </summary>
			public FunctionWriter Writer => Function.Writer;

			/// <summary>
			/// The current <see cref="Compilation.Scope"/>.
			/// </summary>
			public Scope Scope { get; }

			/// <summary>
			/// Whether or not the <see cref="IStatement"/>s are <see cref="PredefinedStatement"/>s.
			/// </summary>
			public bool Predefined { get; }


			/// <summary>
			/// Creates a new <see cref="CompileArguments"/>.
			/// </summary>
			/// <param name="compiler">The <see cref="Compilation.Compiler"/>.</param>
			/// <param name="function">The <see cref="StandaloneStatementFunction"/> being written to.</param>
			/// <param name="scope">The current <see cref="Compilation.Scope"/>.</param>
			/// <param name="predefined">Whether or not the <see cref="IStatement"/>s are <see cref="PredefinedStatement"/>s.</param>
			public CompileArguments(Compiler compiler, StandaloneStatementFunction function, Scope scope, bool predefined) {
				Compiler = compiler ?? throw new ArgumentNullException(nameof(compiler));
				Function = function ?? throw new ArgumentNullException(nameof(function));
				Scope = scope ?? throw new ArgumentNullException(nameof(scope));
				Predefined = predefined;
			}


			/// <summary>
			/// Finds the file name and line/column number of the given <see cref="ParserRuleContext"/>.
			/// </summary>
			/// <param name="context">The <see cref="ParserRuleContext"/> to find the file name and line/column number of.</param>
			/// <returns>The location in a user-friendly format.</returns>
			public string GetLocation(ParserRuleContext context) => GetLocation(context.Start);

			/// <summary>
			/// Finds the file name and line/column number of the given <see cref="ITerminalNode"/>.
			/// </summary>
			/// <param name="node">The <see cref="ITerminalNode"/> to find the file name and line/column number of.</param>
			/// <returns>The location in a user-friendly format.</returns>
			public string GetLocation(ITerminalNode node) => GetLocation(node.Symbol);

			/// <summary>
			/// Finds the file name and line/column number of the given <see cref="IToken"/>.
			/// </summary>
			/// <param name="token">The <see cref="IToken"/> to find the file name and line/column number of.</param>
			/// <returns>The location in a user-friendly format.</returns>
			public string GetLocation(IToken token) {
				if(Predefined) {

					int line = token.Line;
					int column = token.Column;
					return $"Predefined {line}:{column} ";

				} else {

					string scriptDirectory = Compiler.Settings.Datapack.ScriptDirectory;

					string file = token.InputStream.SourceName;
					if(file.StartsWith(scriptDirectory)) file = file[(scriptDirectory.Length + 1)..];
					int line = token.Line;
					int column = token.Column;
					return $"{file} {line}:{column} ";

				}
			}

		}

		public class ScriptClassWalker : MCSharpBaseListener {

			Compiler Compiler { get; }
			Scope RootScope { get; }
			event Action<Compiler> postFirstPass;
			event Action<CompileArguments> onLoad;

			private TypeDefinitionContext CurrentTypeContext { get; set; }
			private ICollection<MemberDefinitionContext> CurrentMemberContexts { get; set; } = new LinkedList<MemberDefinitionContext>();
			private ICollection<ConstructorDefinitionContext> CurrentConstructorContexts { get; set; } = new LinkedList<ConstructorDefinitionContext>();

			public ScriptClassWalker(Compiler compiler, Scope rootScope) {
				Compiler = compiler;
				RootScope = rootScope;
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
				
				Scope typeScope = new Scope(context.NAME().GetText(), RootScope);
				
				var scriptType = new ScriptType(
					CurrentTypeContext, CurrentMemberContexts.ToArray(), CurrentConstructorContexts.ToArray(),
					Compiler.Settings, Compiler.VirtualMachine, typeScope,
					out Action<Compiler> postFirstPass, out Action<CompileArguments> onLoad
				);

				this.postFirstPass += postFirstPass;
				this.onLoad += onLoad;
				Compiler.DefinedTypes.Add(scriptType.Identifier.GetText(), scriptType);

			}

			public void PostFirstPass() {
				postFirstPass.Invoke(Compiler);
			}

			public void OnLoad(Compiler.CompileArguments location) {
				onLoad.Invoke(location);
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
