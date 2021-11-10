using MCSharp.Collections;
using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage;
using MCSharp.Linkage.Minecraft;
using MCSharp.Linkage.Predefined;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Extensions {

	public class MCSharpLinkerExtension : LinkerExtension {

		public static string StorageSelector => "mcs.value";
		public static string ObjectIdentifier => "object";
		public static string IntIdentifier => "int";
		public static string BoolIdentifier => "bool";
		public static string ObjectiveIdentifier => "objective";
		public static string StringIdentifier => "string";
		public static string SelectorIdentifier => "selector";
		public static string FloatIdentifier => "float";

		public MCSharpLinkerExtension(Compiler compiler) : base(compiler) { }

		/// <inheritdoc/>
		public override void CreatePredefinedTypes(Scope rootScope, out Action<Compiler.CompileArguments> onLoad, out Action<Compiler.CompileArguments> onTick) {

			onLoad = (location) => {
				location.Writer.WriteComments(
					"Add objective to store object IDs.");
				Objective.AddObjective(location.Writer, ObjectInstance.AddressObjective);
			};
			onTick = (location) => { };

			// Add the object type.
			PredefinedType @object = CreatePredefinedObject(rootScope);
			Compiler.DefinedTypes.Add(@object.Identifier, @object);

			// Add the int type.
			PredefinedType @int = CreatePredefinedInt(rootScope);
			Compiler.DefinedTypes.Add(@int.Identifier, @int);

			// Add the bool type.
			PredefinedType @bool = CreatePredefinedBool(rootScope);
			Compiler.DefinedTypes.Add(@bool.Identifier, @bool);

			// Add the objective type.
			PredefinedType objective = CreatePredefinedObjective(rootScope);
			Compiler.DefinedTypes.Add(objective.Identifier, objective);

			// Add the string type.
			PredefinedType @string = CreatePredefinedString(rootScope);
			Compiler.DefinedTypes.Add(@string.Identifier, @string);

			// Add the selector type.
			PredefinedType selector = CreatePredefinedSelector(rootScope);
			Compiler.DefinedTypes.Add(selector.Identifier, selector);

		}

		private static PredefinedType CreatePredefinedObject(Scope rootScope) {

			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = ObjectIdentifier;
			PredefinedType predefinedType = null;
			Scope typeScope = new Scope(identifier, rootScope);

			#region Members

			List<PredefinedMember> members = new List<PredefinedMember>();

			#endregion

			#region Constructors

			List<PredefinedConstructor> constructors = new List<PredefinedConstructor>();

			{
				// This type is abstract.
			}

			#endregion

			#region Operations

			HashSetDictionary<Operation, IOperation> operations = new HashSetDictionary<Operation, IOperation>();

			// Assign
			// Assign the value of one pointer to another.
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						IType type = location.Compiler.DefinedTypes[identifier];

						var left = arguments[0] as ObjectInstance;
						var right = arguments[1] as ObjectInstance;

						ResultInfo assignResult = location.Compiler.CompileSimpleOperation(location, Operation.Assign, left.Pointer, right.Pointer, out result);
						if(assignResult.Failure) return assignResult;

						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.Assign;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Comparison: '=='
			// Compare the value of two pointers.
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						IType type = location.Compiler.DefinedTypes[identifier];

						var left = arguments[0] as ObjectInstance;
						var right = arguments[1] as ObjectInstance;

						ResultInfo comparisonResult = location.Compiler.CompileSimpleOperation(location, Operation.Equality, left.Pointer, right.Pointer, out result);
						if(comparisonResult.Failure) return comparisonResult;

						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.Assign;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Comparison: '!='
			// Compare the value of two pointers.
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						IType type = location.Compiler.DefinedTypes[identifier];

						var left = arguments[0] as ObjectInstance;
						var right = arguments[1] as ObjectInstance;

						ResultInfo comparisonResult = location.Compiler.CompileSimpleOperation(location, Operation.Inequality, left.Pointer, right.Pointer, out result);
						if(comparisonResult.Failure) return comparisonResult;

						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.Assign;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {

				if(predefinedType is null) throw new Exception();
				var rand = VirtualMachine.GenerateRandomIntegerInstance(location);
				return new ObjectInstance(location, predefinedType, identifier, rand);

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

		private static PredefinedType CreatePredefinedInt(Scope rootScope) {

			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = IntIdentifier;
			PredefinedType[] subtypes = new PredefinedType[] { };
			PredefinedType predefinedType = null;
			Scope typeScope = new Scope(identifier, rootScope);

			#region Members

			List<PredefinedMember> members = new List<PredefinedMember>();

			// Objective (field)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "objective";
				Scope memberScope = new Scope(member_identifier, typeScope);

				PredefinedExpression initializer = new PredefinedExpression("= new objective(\"dummy\");");
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Field(initializer);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Field, definition);
				members.Add(member);

			}

			// Objective (property)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "Objective";
				Scope memberScope = new Scope(member_identifier, typeScope);

				var getStatements = new IStatement[] { new PredefinedStatement("return objective;") };
				InlineStatementFunction getDefinition = new InlineStatementFunction(new IGenericParameter[] { }, new IMethodParameter[] { }, getStatements, returnType);
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Property(getDefinition, null);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Property, definition);
				members.Add(member);

			}

			#endregion

			#region Constructors

			List<PredefinedConstructor> constructors = new List<PredefinedConstructor>();

			{
				// Compiler creates this type through literals.
			}

			#endregion

			#region Operations

			HashSetDictionary<Operation, IOperation> operations = new HashSetDictionary<Operation, IOperation>();

			static IInstance ScoreboardOperation(Compiler.CompileArguments location, IInstance[] method, PredefinedType predefinedType, string op, Func<int, int, int> evaluateConstants) {

				Scope scope = location.Scope;
				FunctionWriter writer = location.Writer;
				string selector = StorageSelector;

				PrimitiveInstance.IntegerInstance left = method[0] as PrimitiveInstance.IntegerInstance;
				PrimitiveInstance.IntegerInstance.Constant leftConstant = left is null ? method[0] as PrimitiveInstance.IntegerInstance.Constant : null;
				PrimitiveInstance.IntegerInstance right = method[1] as PrimitiveInstance.IntegerInstance;
				PrimitiveInstance.IntegerInstance.Constant rightConstant = right is null ? method[1] as PrimitiveInstance.IntegerInstance.Constant : null;

				if(leftConstant != null && rightConstant != null) {
					int value = evaluateConstants(leftConstant.Value, rightConstant.Value);
					PrimitiveInstance.IntegerInstance.Constant result = new PrimitiveInstance.IntegerInstance.Constant(predefinedType, null, value);
					return result;
				} else if(leftConstant != null) {
					PrimitiveInstance.IntegerInstance result = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
					writer.WriteCommand($"scoreboard players set {selector} {result.Objective.Name} {leftConstant.Value}");
					writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} {op} {selector} {right.Objective.Name}");
					return result;
				} else if(rightConstant != null) {
					PrimitiveInstance.IntegerInstance result = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
					writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} = {selector} {left.Objective.Name}");
					right = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
					writer.WriteCommand($"scoreboard players set {selector} {right.Objective.Name} {rightConstant.Value}");
					writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} {op} {selector} {right.Objective.Name}");
					return result;
				} else {
					PrimitiveInstance.IntegerInstance result = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
					writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} = {selector} {left.Objective.Name}");
					writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} {op} {selector} {right.Objective.Name}");
					return result;
				}

			}

			static IInstance ScoreboardDirectOperation(Compiler.CompileArguments location, IInstance[] method, PredefinedType predefinedType, string op, string compact) {

				Scope scope = location.Scope;
				FunctionWriter writer = location.Writer;
				string selector = StorageSelector;

				PrimitiveInstance.IntegerInstance left = method[0] as PrimitiveInstance.IntegerInstance;
				PrimitiveInstance.IntegerInstance.Constant leftConstant = left is null ? method[0] as PrimitiveInstance.IntegerInstance.Constant : null;
				PrimitiveInstance.IntegerInstance right = method[1] as PrimitiveInstance.IntegerInstance;
				PrimitiveInstance.IntegerInstance.Constant rightConstant = right is null ? method[1] as PrimitiveInstance.IntegerInstance.Constant : null;

				if(leftConstant != null) {
					throw new InvalidOperationException("Cannot assign to a constant.");
				} else if(rightConstant != null) {
					if(compact != null) {
						writer.WriteCommand($"scoreboard players {compact} {selector} {left.Objective.Name} {rightConstant.Value}");
					} else {
						right = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
						writer.WriteCommand($"scoreboard players set {selector} {right.Objective.Name} {rightConstant.Value}");
						writer.WriteCommand($"scoreboard players operation {selector} {left.Objective.Name} {op} {selector} {right.Objective.Name}");
					}
					return left;
				} else {
					writer.WriteCommand($"scoreboard players operation {selector} {left.Objective.Name} {op} {selector} {right.Objective.Name}");
					return left;
				}

			}

			static IInstance ExecuteScoreComparison(Compiler.CompileArguments location, IInstance[] method, PredefinedType predefinedType, string op,
				Func<int, string> matchConstant, Func<int, string> matchConstantFlipped, Func<int, int, bool> evaluateConstants) {

				Scope scope = location.Scope;
				FunctionWriter writer = location.Writer;
				string selector = StorageSelector;

				PrimitiveInstance.IntegerInstance left = method[0] as PrimitiveInstance.IntegerInstance;
				PrimitiveInstance.IntegerInstance.Constant leftConstant = left is null ? method[0] as PrimitiveInstance.IntegerInstance.Constant : null;
				PrimitiveInstance.IntegerInstance right = method[1] as PrimitiveInstance.IntegerInstance;
				PrimitiveInstance.IntegerInstance.Constant rightConstant = right is null ? method[1] as PrimitiveInstance.IntegerInstance.Constant : null;

				if(leftConstant != null && rightConstant != null) {
					bool value = evaluateConstants(leftConstant.Value, rightConstant.Value);
					PrimitiveInstance.BooleanInstance.Constant result = new PrimitiveInstance.BooleanInstance.Constant(predefinedType, null, value);
					return result;
				} else if(leftConstant != null) {
					PrimitiveInstance.BooleanInstance result = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.BooleanInstance;
					writer.WriteCommand($"scoreboard players set {selector} {result.Objective.Name} 0");
					writer.WriteCommand($"execute if score {selector} {right.Objective.Name} matches {matchConstantFlipped(leftConstant.Value)} run scoreboard players set {selector} {result.Objective.Name} 1");
					return result;
				} else if(rightConstant != null) {
					PrimitiveInstance.BooleanInstance result = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.BooleanInstance;
					writer.WriteCommand($"scoreboard players set {selector} {result.Objective.Name} 0");
					writer.WriteCommand($"execute if score {selector} {left.Objective.Name} matches {matchConstant(rightConstant.Value)} run scoreboard players set {selector} {result.Objective.Name} 1");
					return result;
				} else {
					PrimitiveInstance.BooleanInstance result = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.BooleanInstance;
					writer.WriteCommand($"scoreboard players set {selector} {result.Objective.Name} 0");
					writer.WriteCommand($"execute if score {selector} {left.Objective.Name} {op} {selector} {right.Objective.Name} run scoreboard players set {selector} {result.Objective.Name} 1");
					return result;
				}

			}

			// Assign
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						string selector = StorageSelector;

						PrimitiveInstance.IntegerInstance left = arguments[0] as PrimitiveInstance.IntegerInstance;
						PrimitiveInstance.IntegerInstance.Constant leftConstant = left is null ? arguments[0] as PrimitiveInstance.IntegerInstance.Constant : null;
						PrimitiveInstance.IntegerInstance right = arguments[1] as PrimitiveInstance.IntegerInstance;
						PrimitiveInstance.IntegerInstance.Constant rightConstant = right is null ? arguments[1] as PrimitiveInstance.IntegerInstance.Constant : null;

						if(leftConstant != null) {
							throw new InvalidOperationException("Cannot assign to a constant.");
						} else if(rightConstant != null) {
							location.Writer.WriteCommand($"scoreboard players set {selector} {left.Objective.Name} {rightConstant.Value}");
						} else {
							location.Writer.WriteCommand($"scoreboard players operation {selector} {left.Objective.Name} = {selector} {right.Objective.Name}");
						}

						result = left;
						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.Assign;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Addition
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardOperation(location, arguments, predefinedType, "+=", (left, right) => left + right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.Addition;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Addition (Assign)
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardDirectOperation(location, arguments, predefinedType, "+=", "add");
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.AssignAddition;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Subtraction
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardOperation(location, arguments, predefinedType, "-=", (left, right) => left - right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.Subtraction;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Subtraction (Assign)
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardDirectOperation(location, arguments, predefinedType, "-=", "remove");
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.AssignSubtraction;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Multiplication
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardOperation(location, arguments, predefinedType, "*=", (left, right) => left * right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.Multiplication;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Multiplication (Assign)
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardDirectOperation(location, arguments, predefinedType, "*=", null);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.AssignMultiplication;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Division
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardOperation(location, arguments, predefinedType, "/=", (left, right) => left / right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.Division;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Division (Assign)
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardDirectOperation(location, arguments, predefinedType, "/=", null);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.AssignDivision;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Modulo
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardOperation(location, arguments, predefinedType, "%=", (left, right) => left % right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.Modulo;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Modulo (Assign)
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardDirectOperation(location, arguments, predefinedType, "%=", null);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.AssignModulo;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Comparison: '<'
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ExecuteScoreComparison(location, arguments, location.Compiler.DefinedTypes[BoolIdentifier] as PredefinedType, "<",
							(constant) => ".." + (constant + 1), (constant) => constant - 1 + "..", (left, right) => left < right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.LessThan;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Comparison: '<='
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ExecuteScoreComparison(location, arguments, location.Compiler.DefinedTypes[BoolIdentifier] as PredefinedType, "<=",
							(constant) => ".." + constant, (constant) => constant + "..", (left, right) => left <= right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.LessThanOrEqual;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Comparison: '>'
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ExecuteScoreComparison(location, arguments, location.Compiler.DefinedTypes[BoolIdentifier] as PredefinedType, ">",
							(constant) => constant - 1 + "..", (constant) => ".." + (constant + 1), (left, right) => left > right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.GreaterThan;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Comparison: '>='
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ExecuteScoreComparison(location, arguments, location.Compiler.DefinedTypes[BoolIdentifier] as PredefinedType, ">=",
							(constant) => constant + "..", (constant) => ".." + constant, (left, right) => left >= right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.GreaterThanOrEqual;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Comparison: '=='
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ExecuteScoreComparison(location, arguments, location.Compiler.DefinedTypes[BoolIdentifier] as PredefinedType, "=",
							(constant) => constant.ToString(), (constant) => constant.ToString(), (left, right) => left == right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.Equality;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Comparison: '!='
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						Scope scope = location.Scope;
						FunctionWriter writer = location.Writer;
						string selector = StorageSelector;

						PrimitiveInstance.IntegerInstance left = arguments[0] as PrimitiveInstance.IntegerInstance;
						PrimitiveInstance.IntegerInstance.Constant leftConstant = left is null ? arguments[0] as PrimitiveInstance.IntegerInstance.Constant : null;
						PrimitiveInstance.IntegerInstance right = arguments[1] as PrimitiveInstance.IntegerInstance;
						PrimitiveInstance.IntegerInstance.Constant rightConstant = right is null ? arguments[1] as PrimitiveInstance.IntegerInstance.Constant : null;

						if(leftConstant != null && rightConstant != null) {

							bool value = leftConstant.Value != rightConstant.Value;
							result = new PrimitiveInstance.BooleanInstance.Constant(predefinedType, null, value);

						} else if(leftConstant != null) {

							PrimitiveInstance.BooleanInstance resultBool = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.BooleanInstance;
							writer.WriteCommand($"scoreboard players set {selector} {resultBool.Objective.Name} 0");
							writer.WriteCommand($"execute unless score {selector} {right.Objective.Name} matches {leftConstant.Value} run scoreboard players set {selector} {resultBool.Objective.Name} 1");

							result = resultBool;

						} else if(rightConstant != null) {

							PrimitiveInstance.BooleanInstance resultBool = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.BooleanInstance;
							writer.WriteCommand($"scoreboard players set {selector} {resultBool.Objective.Name} 0");
							writer.WriteCommand($"execute unless score {selector} {left.Objective.Name} matches {rightConstant.Value} run scoreboard players set {selector} {resultBool.Objective.Name} 1");

							result = resultBool;

						} else {

							PrimitiveInstance.BooleanInstance resultBool = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.BooleanInstance;
							writer.WriteCommand($"scoreboard players set {selector} {resultBool.Objective.Name} 0");
							writer.WriteCommand($"execute unless score {selector} {left.Objective.Name} = {selector} {right.Objective.Name} run scoreboard players set {selector} {resultBool.Objective.Name} 1");

							result = resultBool;

						}

						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.Inequality;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {

				if(predefinedType is null) throw new Exception();
				var objective = Objective.AddObjective(location.Writer, identifier, "dummy");
				return new PrimitiveInstance.IntegerInstance(predefinedType, identifier, objective);

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

		private static PredefinedType CreatePredefinedBool(Scope rootScope) {

			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = BoolIdentifier;
			PredefinedType[] subtypes = new PredefinedType[] { };
			PredefinedType predefinedType = null;
			Scope typeScope = new Scope(identifier, rootScope);

			#region Members

			List<PredefinedMember> members = new List<PredefinedMember>();

			// Objective (field)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "objective";
				Scope memberScope = new Scope(member_identifier, typeScope);

				PredefinedExpression initializer = new PredefinedExpression("= new objective(\"dummy\");");
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Field(initializer);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Field, definition);
				members.Add(member);

			}

			// Objective (property)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "Objective";
				Scope memberScope = new Scope(member_identifier, typeScope);

				var getStatements = new IStatement[] { new PredefinedStatement("return objective;") };
				InlineStatementFunction getDefinition = new InlineStatementFunction(new IGenericParameter[] { }, new IMethodParameter[] { }, getStatements, returnType);
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Property(getDefinition, null);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Property, definition);
				members.Add(member);

			}

			#endregion

			#region Constructors

			List<PredefinedConstructor> constructors = new List<PredefinedConstructor>();

			{
				// Compiler creates this type through literals.
			}

			#endregion

			#region Operations

			HashSetDictionary<Operation, IOperation> operations = new HashSetDictionary<Operation, IOperation>();

			// Assign
			{

				CustomFunction function = new CustomFunction(BoolIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(BoolIdentifier, "left"),
						new PredefinedMethodParameter(BoolIdentifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						string selector = StorageSelector;

						PrimitiveInstance.BooleanInstance left = arguments[0] as PrimitiveInstance.BooleanInstance;
						PrimitiveInstance.BooleanInstance.Constant leftConstant = left is null ? arguments[0] as PrimitiveInstance.BooleanInstance.Constant : null;
						PrimitiveInstance.BooleanInstance right = arguments[1] as PrimitiveInstance.BooleanInstance;
						PrimitiveInstance.BooleanInstance.Constant rightConstant = right is null ? arguments[1] as PrimitiveInstance.BooleanInstance.Constant : null;

						if(leftConstant != null) {
							throw new InvalidOperationException("Cannot assign to a constant.");
						} else if(rightConstant != null) {
							location.Writer.WriteCommand($"scoreboard players set {selector} {left.Objective.Name} {(rightConstant.Value ? 1 : 0)}");
						} else {
							location.Writer.WriteCommand($"scoreboard players operation {selector} {left.Objective.Name} = {selector} {right.Objective.Name}");
						}

						result = left;
						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.Assign;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Boolean AND
			{

				CustomFunction function = new CustomFunction(BoolIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(BoolIdentifier, "left"),
						new PredefinedMethodParameter(BoolIdentifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						string selector = StorageSelector;

						PrimitiveInstance.BooleanInstance left = arguments[0] as PrimitiveInstance.BooleanInstance;
						PrimitiveInstance.BooleanInstance.Constant leftConstant = left is null ? arguments[0] as PrimitiveInstance.BooleanInstance.Constant : null;
						PrimitiveInstance.BooleanInstance right = arguments[1] as PrimitiveInstance.BooleanInstance;
						PrimitiveInstance.BooleanInstance.Constant rightConstant = right is null ? arguments[1] as PrimitiveInstance.BooleanInstance.Constant : null;

						if(leftConstant != null && rightConstant != null) {
							result = new PrimitiveInstance.BooleanInstance.Constant(predefinedType, null, leftConstant.Value && rightConstant.Value);
						} else if(leftConstant != null) {
							if(!leftConstant.Value) {
								result = leftConstant;
							} else {
								PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(location, null)) as PrimitiveInstance.BooleanInstance;
								location.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} {(leftConstant.Value ? 1 : 0)}");
							}
						} else if(rightConstant != null) {
							if(!rightConstant.Value) {
								result = rightConstant;
							} else {
								PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(location, null)) as PrimitiveInstance.BooleanInstance;
								location.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} {(rightConstant.Value ? 1 : 0)}");
								result = instance;
							}
						} else {
							PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(location, null)) as PrimitiveInstance.BooleanInstance;
							location.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} 0");
							location.Writer.WriteCommand($"execute if score {selector} {left.Objective.Name} matches 1.. if score {selector} {right.Objective.Name} matches 1.. " +
								$"run scoreboard players set {selector} {instance.Objective.Name} 1");
						}

						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.BooleanAND;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Boolean OR
			{

				CustomFunction function = new CustomFunction(BoolIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(BoolIdentifier, "left"),
						new PredefinedMethodParameter(BoolIdentifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						string selector = StorageSelector;

						PrimitiveInstance.BooleanInstance left = arguments[0] as PrimitiveInstance.BooleanInstance;
						PrimitiveInstance.BooleanInstance.Constant leftConstant = left is null ? arguments[0] as PrimitiveInstance.BooleanInstance.Constant : null;
						PrimitiveInstance.BooleanInstance right = arguments[1] as PrimitiveInstance.BooleanInstance;
						PrimitiveInstance.BooleanInstance.Constant rightConstant = right is null ? arguments[1] as PrimitiveInstance.BooleanInstance.Constant : null;

						if(leftConstant != null && rightConstant != null) {
							result = new PrimitiveInstance.BooleanInstance.Constant(predefinedType, null, leftConstant.Value && rightConstant.Value);
						} else if(leftConstant != null) {
							if(leftConstant.Value) {
								result = leftConstant;
							} else {
								PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(location, null)) as PrimitiveInstance.BooleanInstance;
								location.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} {(leftConstant.Value ? 1 : 0)}");
							}
						} else if(rightConstant != null) {
							if(rightConstant.Value) {
								result = rightConstant;
							} else {
								PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(location, null)) as PrimitiveInstance.BooleanInstance;
								location.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} {(rightConstant.Value ? 1 : 0)}");
							}
						} else {
							PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(location, null)) as PrimitiveInstance.BooleanInstance;
							location.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} 1");
							location.Writer.WriteCommand($"execute if score {selector} {left.Objective.Name} matches ..0 if score {selector} {right.Objective.Name} matches ..0 " +
								$"run scoreboard players set {selector} {instance.Objective.Name} 0");
						}

						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.BooleanOR;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {

				if(predefinedType is null) throw new Exception();
				var objective = Objective.AddObjective(location.Writer, identifier, "dummy");
				return new PrimitiveInstance.BooleanInstance(predefinedType, identifier, objective);

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

		private static PredefinedType CreatePredefinedObjective(Scope rootScope) {

			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = ObjectiveIdentifier;
			PredefinedType[] subtypes = new PredefinedType[] { };
			PredefinedType predefinedType = null;
			Scope typeScope = new Scope(identifier, rootScope);

			#region Members

			List<PredefinedMember> members = new List<PredefinedMember>();

			// Name (field)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "name";
				Scope memberScope = new Scope(member_identifier, typeScope);

				PredefinedExpression initializer = null;
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Field(initializer);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Field, definition);
				members.Add(member);

			}

			// Name (property)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "Name";
				Scope memberScope = new Scope(member_identifier, typeScope);

				var getStatements = new IStatement[] { new PredefinedStatement("return name;") };
				InlineStatementFunction getDefinition = new InlineStatementFunction(new IGenericParameter[] { }, new IMethodParameter[] { }, getStatements, returnType);
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Property(getDefinition, null);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Property, definition);
				members.Add(member);

			}

			#endregion

			#region Constructors

			List<PredefinedConstructor> constructors = new List<PredefinedConstructor>();

			{
				// TODO
			}

			#endregion

			#region Operations

			HashSetDictionary<Operation, IOperation> operations = new HashSetDictionary<Operation, IOperation>();

			{

			}

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {

				throw new NotImplementedException($"'{ObjectiveIdentifier}' initialization has not been implemented.");

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

		private static PredefinedType CreatePredefinedString(Scope rootScope) {

			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = StringIdentifier;
			PredefinedType[] subtypes = new PredefinedType[] { };
			PredefinedType predefinedType = null;
			Scope typeScope = new Scope(identifier, rootScope);

			#region Members

			List<PredefinedMember> members = new List<PredefinedMember>();

			{

			}

			#endregion

			#region Constructors

			List<PredefinedConstructor> constructors = new List<PredefinedConstructor>();

			{
				// Compiler creates this type through literals.
			}

			#endregion

			#region Operations

			HashSetDictionary<Operation, IOperation> operations = new HashSetDictionary<Operation, IOperation>();

			{

			}

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {

				throw new NotImplementedException($"'{StringIdentifier}' initialization has not been implemented.");

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

		private static PredefinedType CreatePredefinedSelector(Scope rootScope) {

			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = SelectorIdentifier;
			PredefinedType predefinedType = null;
			Scope typeScope = new Scope(identifier, rootScope);

			#region Members

			List<PredefinedMember> members = new List<PredefinedMember>();

			{

			}

			#endregion

			#region Constructors

			List<PredefinedConstructor> constructors = new List<PredefinedConstructor>();

			{
				
			}

			#endregion

			#region Operations

			HashSetDictionary<Operation, IOperation> operations = new HashSetDictionary<Operation, IOperation>();

			{

			}

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {

				throw new NotImplementedException($"'{SelectorIdentifier}' initialization has not been implemented.");

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

	}

}
