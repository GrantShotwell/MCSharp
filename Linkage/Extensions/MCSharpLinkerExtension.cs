using MCSharp.Collections;
using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage;
using MCSharp.Linkage.Minecraft;
using MCSharp.Linkage.Minecraft.Text;
using MCSharp.Linkage.Predefined;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Extensions {

	public class MCSharpLinkerExtension : LinkerExtension {

		public static string StorageSelector => "mcs.value";

		// Primitive Types
		public static string ObjectIdentifier => "object";
		public static string ObjectiveIdentifier => "objective";
		public static string IntIdentifier => "int";
		public static string BoolIdentifier => "bool";
		public static string FloatIdentifier => "float";
		public static string StringIdentifier => "string";
		public static string SelectorIdentifier => "Selector";
		public static string JsonIdentifier => "Json";

		// Static Types
		public static string ChatIdentifier => "Chat";
		public static string WorldIdentifier => "World";

		// Attribute Types
		public static string AttributeIdentifier => "Attribute";
		public static string EntityTypeAttributeIdentifier => "EntityType";


		public MCSharpLinkerExtension(Compiler compiler) : base(compiler) { }

		/// <inheritdoc/>
		public override void CreatePredefinedTypes(Scope rootScope, out Action<Compiler.CompileArguments> onLoad, out Action<Compiler.CompileArguments> onTick) {

			var onLoadActions = new List<Action<Compiler.CompileArguments>>();
			onLoad = (location) => {
				location.Writer.WriteComments(
					"Add objective to store object IDs.");
				Objective.AddObjective(location.Writer, ObjectInstance.AddressObjective);
				foreach(var action in onLoadActions) action(location);
			};
			onTick = (location) => { };

			// Add the object type.
			PredefinedType @object = CreatePredefinedObject(rootScope, onLoadActions);
			Compiler.DefinedTypes.Add(@object.Identifier, @object);

			// Add the objective type.
			PredefinedType objective = CreatePredefinedObjective(rootScope, onLoadActions);
			Compiler.DefinedTypes.Add(objective.Identifier, objective);

			// Add the int type.
			PredefinedType @int = CreatePredefinedInt(rootScope, onLoadActions);
			Compiler.DefinedTypes.Add(@int.Identifier, @int);

			// Add the bool type.
			PredefinedType @bool = CreatePredefinedBool(rootScope, onLoadActions);
			Compiler.DefinedTypes.Add(@bool.Identifier, @bool);

			// Add the string type.
			PredefinedType @string = CreatePredefinedString(rootScope, onLoadActions);
			Compiler.DefinedTypes.Add(@string.Identifier, @string);

			// Add the Selector type.
			PredefinedType selector = CreatePredefinedSelector(rootScope, onLoadActions);
			Compiler.DefinedTypes.Add(selector.Identifier, selector);

			// Add the Json type.
			PredefinedType json = CreatePredefinedJson(rootScope, onLoadActions);
			Compiler.DefinedTypes.Add(json.Identifier, json);

			// Add the Chat type.
			PredefinedType chat = CreatePredefinedChat(rootScope, onLoadActions);
			Compiler.DefinedTypes.Add(chat.Identifier, chat);

			// Add the World type.
			PredefinedType world = CreatePredefinedWorld(rootScope, onLoadActions);
			Compiler.DefinedTypes.Add(world.Identifier, world);

			// Add the Attribute type.
			PredefinedType attribute = CreatePredefinedAttribute(rootScope, onLoadActions);
			Compiler.DefinedTypes.Add(attribute.Identifier, attribute);

		}
		
		/// <summary>
		/// Creates the <see cref="PredefinedType"/> for the <see cref="ObjectInstance"/> type "object".
		/// </summary>
		private static PredefinedType CreatePredefinedObject(Scope rootScope, List<Action<Compiler.CompileArguments>> onLoadActions) {

			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = ObjectIdentifier;
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

			#region Casts

			Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

			void OnLoad(Compiler.CompileArguments location) {

				

            }

			onLoadActions.Add(OnLoad);

			#endregion
			
			IType.InitializeInstanceDelegate init = (location, identifier) => {

				if(predefinedType is null) throw new Exception();
				var rand = VirtualMachine.GenerateRandomIntegerInstance(location);
				return new ObjectInstance(location, predefinedType, identifier, rand);

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations, casts);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}
		
		/// <summary>
		/// Creates the <see cref="PredefinedType"/> for the <see cref="PrimitiveInstance.IntegerInstance"/> type "int".
		/// </summary>
		private static PredefinedType CreatePredefinedInt(Scope rootScope, List<Action<Compiler.CompileArguments>> onLoadActions) {

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

			// Bitwise AND
			{

				CustomFunction function = new CustomFunction(identifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						// Get arguments.
						PrimitiveInstance.IntegerInstance left = arguments[0] as PrimitiveInstance.IntegerInstance;
						PrimitiveInstance.IntegerInstance.Constant leftConstant = left is null ? arguments[0] as PrimitiveInstance.IntegerInstance.Constant : null;
						PrimitiveInstance.IntegerInstance right = arguments[1] as PrimitiveInstance.IntegerInstance;
						PrimitiveInstance.IntegerInstance.Constant rightConstant = right is null ? arguments[1] as PrimitiveInstance.IntegerInstance.Constant : null;

						throw new NotImplementedException();

					}
				);

				Operation op = Operation.BitwiseAND;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			#endregion

			#region Casts

			Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

			void OnLoad(Compiler.CompileArguments location) {

				// Json (implicit)
				{

					// Get cast types.
					IType reference = location.Compiler.DefinedTypes[IntIdentifier];
					IType target = location.Compiler.DefinedTypes[JsonIdentifier];

					// Create cast function.
					CustomFunction function = new CustomFunction(JsonIdentifier,
						new PredefinedGenericParameter [] {

                        },
						new PredefinedMethodParameter [] {
							new PredefinedMethodParameter(IntIdentifier, "value")
                        },
						(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

							// Get arguments.
							PrimitiveInstance.IntegerInstance value = arguments[0] as PrimitiveInstance.IntegerInstance;

							// Make JSON text from integer.
							RawText json = new RawText() {
								Score = 
									value is IConstantInstance<int> constant
										// Use constant value.
										? new ScoreData() {
											Name = StorageSelector,
											Value = constant.Value.ToString()
										}
										// Use dynamic value.
										: new ScoreData() {
											Name = StorageSelector,
											Objective = value.Objective.Name
										}
							};
							result = new PrimitiveInstance.JsonInstance(target, null, json);

							// Return a success.
							return ResultInfo.DefaultSuccess;

                        }
					);

					// Create and add cast.
					var cast = new PredefinedConversion(reference, target, function, @implicit: true);
					casts.Add(target, cast);

				}

            }

			onLoadActions.Add(OnLoad);

			#endregion
			
			IType.InitializeInstanceDelegate init = (location, identifier) => {

				if(predefinedType is null) throw new Exception();
				var objective = Objective.AddObjective(location.Writer, identifier, "dummy");
				return new PrimitiveInstance.IntegerInstance(predefinedType, identifier, objective);

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations, casts);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}
		
		/// <summary>
		/// Creates the <see cref="PredefinedType"/> for the <see cref="PrimitiveInstance.BooleanInstance"/> type "string".
		/// </summary>
		private static PredefinedType CreatePredefinedBool(Scope rootScope, List<Action<Compiler.CompileArguments>> onLoadActions) {

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

			#region Casts

			Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

			void OnLoad(Compiler.CompileArguments location) {

				// Json (implicit)
				{

					// Get cast types.
					IType reference = location.Compiler.DefinedTypes[BoolIdentifier];
					IType target = location.Compiler.DefinedTypes[JsonIdentifier];

					// Create cast function.
					CustomFunction function = new CustomFunction(JsonIdentifier,
						new PredefinedGenericParameter [] {

                        },
						new PredefinedMethodParameter [] {
							new PredefinedMethodParameter(BoolIdentifier, "value")
                        },
						(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

							// Get arguments.
							PrimitiveInstance.BooleanInstance value = arguments[0] as PrimitiveInstance.BooleanInstance;
							PrimitiveInstance.BooleanInstance.Constant constant = value is null ? arguments[0] as PrimitiveInstance.BooleanInstance.Constant : null;

							// Make JSON text from boolean.
							RawText json;
							if(constant != null) {

								// Use constant value.
								json = new RawText() {
									Text = constant.Value ? "1" : "0"
								};

							} else {

								// Use dynamic value.
								json = new RawText() {
									Score = new ScoreData() {
										Name = StorageSelector,
										Objective = value.Objective.Name
									}
								};

							}

							
							result = new PrimitiveInstance.JsonInstance(target, null, json);

							// Return a success.
							return ResultInfo.DefaultSuccess;

                        }
					);

					// Create and add cast.
					var cast = new PredefinedConversion(reference, target, function, @implicit: true);
					casts.Add(target, cast);

				}

            }

			onLoadActions.Add(OnLoad);

			#endregion
			
			IType.InitializeInstanceDelegate init = (location, identifier) => {

				if(predefinedType is null) throw new Exception();
				var objective = Objective.AddObjective(location.Writer, identifier, "dummy");
				return new PrimitiveInstance.BooleanInstance(predefinedType, identifier, objective);

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations, casts);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}
		
		/// <summary>
		/// Creates the <see cref="PredefinedType"/> for the <see cref="PrimitiveInstance.ObjectiveInstance"/> type "objective".
		/// </summary>
		private static PredefinedType CreatePredefinedObjective(Scope rootScope, List<Action<Compiler.CompileArguments>> onLoadActions) {

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

			#endregion#region Casts

			#region Casts
			
			Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

			void OnLoad(Compiler.CompileArguments location) {



            }

			onLoadActions.Add(OnLoad);

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {

				throw new NotImplementedException($"'{ObjectiveIdentifier}' initialization has not been implemented.");

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations, casts);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}
		
		/// <summary>
		/// Creates the <see cref="PredefinedType"/> for the <see cref="PrimitiveInstance.StringInstance"/> type "string".
		/// </summary>
		private static PredefinedType CreatePredefinedString(Scope rootScope, List<Action<Compiler.CompileArguments>> onLoadActions) {

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

			// Addition
			{

				CustomFunction function = new CustomFunction(StringIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(StringIdentifier, "left"),
						new PredefinedMethodParameter(StringIdentifier, "right"),
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						PrimitiveInstance.StringInstance left = arguments[0] as PrimitiveInstance.StringInstance;
						PrimitiveInstance.StringInstance right = arguments[1] as PrimitiveInstance.StringInstance;

						result = new PrimitiveInstance.StringInstance(predefinedType, null, left.Value + right.Value);

						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.Addition;
				operations.Add(op, new PredefinedOperation(op, function));

			}


			// Initialization Assign
			{

				CustomFunction function = new CustomFunction(StringIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(StringIdentifier, "left"),
						new PredefinedMethodParameter(StringIdentifier, "right"),
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						PrimitiveInstance.StringInstance left = arguments[0] as PrimitiveInstance.StringInstance;
						PrimitiveInstance.StringInstance right = arguments[1] as PrimitiveInstance.StringInstance;

						result = new PrimitiveInstance.StringInstance(predefinedType, null, left.Value = right.Value);

						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.InitializationAssign;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			#endregion

			#region Casts

			Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

			void OnLoad(Compiler.CompileArguments location) {

                // Json (implicit)
                {

					// Get cast types.
					IType reference = location.Compiler.DefinedTypes[StringIdentifier];
					IType target = location.Compiler.DefinedTypes[JsonIdentifier];

					// Create cast function.
					CustomFunction function = new CustomFunction(JsonIdentifier,
						new PredefinedGenericParameter [] {

                        },
						new PredefinedMethodParameter [] {
							new PredefinedMethodParameter(StringIdentifier, "value")
                        },
						(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

							// Get arguments.
							PrimitiveInstance.StringInstance value = arguments[0] as PrimitiveInstance.StringInstance;

							// Make JSON text from string. 
							RawText json = new RawText() {
								Text = value.Value
							};
							result = new PrimitiveInstance.JsonInstance(target, null, json);

							// Return a success.
							return ResultInfo.DefaultSuccess;

                        }
					);

					// Create and add cast.
					var cast = new PredefinedConversion(reference, target, function, @implicit: true);
					casts.Add(target, cast);

                }

				// Selector (implicit)
				{

					// Get cast types.
					IType reference = location.Compiler.DefinedTypes[StringIdentifier];
					IType target = location.Compiler.DefinedTypes[SelectorIdentifier];

					// Create cast function.
					CustomFunction function = new CustomFunction(SelectorIdentifier,
						new PredefinedGenericParameter [] {

						},
						new PredefinedMethodParameter [] {
							new PredefinedMethodParameter(StringIdentifier, "value")
						},
						(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

							// Get arguments.
							PrimitiveInstance.StringInstance value = arguments[0] as PrimitiveInstance.StringInstance;

							// Make selector from string.
							Selector selector = new Selector(value.Value);
							result = new PrimitiveInstance.SelectorInstance(target, null, selector);

							// Return a success.
							return ResultInfo.DefaultSuccess;

						}
					);

					// Create and add cast.
					var cast = new PredefinedConversion(reference, target, function, @implicit: true);
					casts.Add(target, cast);

				}

            }

			onLoadActions.Add(OnLoad);

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {

				if(predefinedType is null) throw new Exception();
				return new PrimitiveInstance.StringInstance(predefinedType, identifier, null);

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations, casts);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}
		
		/// <summary>
		/// Creates the <see cref="PredefinedType"/> for the <see cref="PrimitiveInstance.SelectorInstance"/> type "Selector".
		/// </summary>
		private static PredefinedType CreatePredefinedSelector(Scope rootScope, List<Action<Compiler.CompileArguments>> onLoadActions) {

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

			// Initialization Assign
			{

				CustomFunction function = new CustomFunction(SelectorIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(SelectorIdentifier, "left"),
						new PredefinedMethodParameter(SelectorIdentifier, "right"),
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						PrimitiveInstance.SelectorInstance left = arguments[0] as PrimitiveInstance.SelectorInstance;
						PrimitiveInstance.SelectorInstance right = arguments[1] as PrimitiveInstance.SelectorInstance;

						result = new PrimitiveInstance.SelectorInstance(predefinedType, null, left.Value = right.Value);

						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.InitializationAssign;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			#endregion

			#region Casts

			Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

			void OnLoad(Compiler.CompileArguments location) {

				

            }

			onLoadActions.Add(OnLoad);

			#endregion
			
			IType.InitializeInstanceDelegate init = (location, identifier) => {

				throw new NotImplementedException($"'{SelectorIdentifier}' initialization has not been implemented.");

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations, casts);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

		/// <summary>
		/// Creates the <see cref="PredefinedType"/> for the type "Json".
		/// </summary>
		private static PredefinedType CreatePredefinedJson(Scope rootScope, List<Action<Compiler.CompileArguments>> onLoadActions) {

			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = JsonIdentifier;
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

			// Initialization Assign
			{

				string returnType = JsonIdentifier;
				string member_identifier = "Initialization";
				Scope memberScope = new Scope(member_identifier, typeScope);

				CustomFunction function = new CustomFunction(JsonIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(JsonIdentifier, "left"),
						new PredefinedMethodParameter(JsonIdentifier, "right")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						PrimitiveInstance.JsonInstance left = arguments[0] as PrimitiveInstance.JsonInstance;
						PrimitiveInstance.JsonInstance right = arguments[1] as PrimitiveInstance.JsonInstance;
						
						result = new PrimitiveInstance.JsonInstance(predefinedType, null, left.Value = right.Value);
						return ResultInfo.DefaultSuccess;

					}
				);
				
				Operation op = Operation.InitializationAssign;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			#endregion

			#region Casts

			Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

			void OnLoad(Compiler.CompileArguments location) {

				

            }

			onLoadActions.Add(OnLoad);

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {
				
				if(predefinedType is null) throw new Exception();
				return new PrimitiveInstance.JsonInstance(predefinedType, identifier, null);

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations, casts);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

		/// <summary>
		/// Creates the <see cref="PredefinedType"/> for the type "Chat".
		/// </summary>
		private static PredefinedType CreatePredefinedChat(Scope rootScope, List<Action<Compiler.CompileArguments>> onLoadActions) {

			Modifier modifiers = Modifier.Public | Modifier.Static;
			ClassType classType = ClassType.Struct;
			string identifier = ChatIdentifier;
			PredefinedType predefinedType = null;
			Scope typeScope = new Scope(identifier, rootScope);

			#region Members

			List<PredefinedMember> members = new List<PredefinedMember>();

			// static void Say(string text)
			{

				Modifier member_modifiers = Modifier.Public | Modifier.Static;
				string returnType = "void";
				string member_identifier = "Say";
				Scope memberScope = new Scope(member_identifier, typeScope);

				CustomFunction function = new CustomFunction("void",
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter("string", "text")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						PrimitiveInstance.StringInstance text = arguments[0] as PrimitiveInstance.StringInstance;
						
						location.Writer.WriteCommand($"say {text.Value}",
							$"Chat.Say");

						result = null;
						return ResultInfo.DefaultSuccess;

					}
				);

				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
				members.Add(member);

			}

			// static void Tellraw(Json json)
			{

				Modifier member_modifiers = Modifier.Public | Modifier.Static;
				string returnType = "void";
				string member_identifier = "Tellraw";
				Scope memberScope = new Scope(member_identifier, typeScope);

				CustomFunction function = new CustomFunction("void",
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(JsonIdentifier, "json")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						PrimitiveInstance.JsonInstance json = arguments[0] as PrimitiveInstance.JsonInstance;
						
						location.Writer.WriteCommand($"tellraw @a {json.Value.GetJson()}",
							$"Chat.Tellraw");

						result = null;
						return ResultInfo.DefaultSuccess;

					}
				);

				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
				members.Add(member);

			}

			// static void Tellraw(Selector selector, Json json)
			{

				Modifier member_modifiers = Modifier.Public | Modifier.Static;
				string returnType = "void";
				string member_identifier = "Tellraw";
				Scope memberScope = new Scope(member_identifier, typeScope);

				CustomFunction function = new CustomFunction("void",
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(SelectorIdentifier, "selector"),
						new PredefinedMethodParameter(JsonIdentifier, "json")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						PrimitiveInstance.SelectorInstance selector = arguments[0] as PrimitiveInstance.SelectorInstance;
						PrimitiveInstance.JsonInstance json = arguments[1] as PrimitiveInstance.JsonInstance;
						
						location.Writer.WriteCommand($"tellraw {selector.Value} {json.Value.GetJson()}",
							$"Chat.Tellraw");

						result = null;
						return ResultInfo.DefaultSuccess;

					}
				);

				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
				members.Add(member);

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

			#region Casts

			Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

			void OnLoad(Compiler.CompileArguments location) {

				

            }

			onLoadActions.Add(OnLoad);

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {

				throw new NotImplementedException($"'{ChatIdentifier}' initialization has not been implemented.");

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations, casts);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

		/// <summary>
		/// Creates the <see cref="PredefinedType"/> for the type "World".
		/// </summary>
		public static PredefinedType CreatePredefinedWorld(Scope rootScope, List<Action<Compiler.CompileArguments>> onLoadActions) {

			Modifier modifiers = Modifier.Public | Modifier.Static;
			ClassType classType = ClassType.Struct;
			string identifier = WorldIdentifier;
			PredefinedType predefinedType = null;
			Scope typeScope = new Scope(identifier, rootScope);

			#region Members

			List<PredefinedMember> members = new List<PredefinedMember>();

			// static void Teleport(string coordinates)
			{

				Modifier member_modifiers = Modifier.Public | Modifier.Static;
				string returnType = "void";
				string member_identifier = "Teleport";
				Scope memberScope = new Scope(member_identifier, typeScope);

				CustomFunction function = new CustomFunction("void",
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(StringIdentifier, "coordinates")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						PrimitiveInstance.StringInstance coordinates = arguments[0] as PrimitiveInstance.StringInstance;
						
						location.Writer.WriteCommand($"tp {coordinates.Value}",
							$"World.Teleport");

						result = null;
						return ResultInfo.DefaultSuccess;

					}
				);

				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
				members.Add(member);

			}

			// static void Teleport(Selector selector, string coordinates)
			{

				Modifier member_modifiers = Modifier.Public | Modifier.Static;
				string returnType = "void";
				string member_identifier = "Teleport";
				Scope memberScope = new Scope(member_identifier, typeScope);

				CustomFunction function = new CustomFunction("void",
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(SelectorIdentifier, "selector"),
						new PredefinedMethodParameter(StringIdentifier, "coordinates")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						// Get arguments.
						PrimitiveInstance.SelectorInstance selector = arguments[0] as PrimitiveInstance.SelectorInstance;
						PrimitiveInstance.StringInstance coordinates = arguments[1] as PrimitiveInstance.StringInstance;
						
						// Run teleport command.
						location.Writer.WriteCommand($"tp {selector.Value} {coordinates.Value}",
							$"World.Teleport");
						result = null;

						// Return success.
						return ResultInfo.DefaultSuccess;

					}
				);

				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
				members.Add(member);

			}

			// static void Teleport(Selector selector, string coordinates, string rotation)
			{

				Modifier member_modifiers = Modifier.Public | Modifier.Static;
				string returnType = "void";
				string member_identifier = "Teleport";
				Scope memberScope = new Scope(member_identifier, typeScope);

				CustomFunction function = new CustomFunction("void",
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(SelectorIdentifier, "selector"),
						new PredefinedMethodParameter(StringIdentifier, "coordinates"),
						new PredefinedMethodParameter(StringIdentifier, "rotation")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						// Get arguments.
						PrimitiveInstance.SelectorInstance selector = arguments[0] as PrimitiveInstance.SelectorInstance;
						PrimitiveInstance.StringInstance coordinates = arguments[1] as PrimitiveInstance.StringInstance;
						PrimitiveInstance.StringInstance rotation = arguments[2] as PrimitiveInstance.StringInstance;
						
						// Run teleport command.
						location.Writer.WriteCommand($"tp {selector.Value} {coordinates.Value} {rotation.Value}",
							$"World.Teleport");
						result = null;

						// Return success.
						return ResultInfo.DefaultSuccess;

					}
				);

				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
				members.Add(member);

			}

			// static void Teleport(Selector selector, string coordinates, Selector target)
			{

				Modifier member_modifiers = Modifier.Public | Modifier.Static;
				string returnType = "void";
				string member_identifier = "Teleport";
				Scope memberScope = new Scope(member_identifier, typeScope);

				CustomFunction function = new CustomFunction("void",
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(SelectorIdentifier, "selector"),
						new PredefinedMethodParameter(StringIdentifier, "coordinates"),
						new PredefinedMethodParameter(SelectorIdentifier, "target")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						// Get arguments.
						PrimitiveInstance.SelectorInstance selector = arguments[0] as PrimitiveInstance.SelectorInstance;
						PrimitiveInstance.StringInstance coordinates = arguments[1] as PrimitiveInstance.StringInstance;
						PrimitiveInstance.SelectorInstance target = arguments[2] as PrimitiveInstance.SelectorInstance;
						
						// Run teleport command.
						location.Writer.WriteCommand($"tp {selector.Value} {coordinates.Value} {target.Value}",
							$"World.Teleport");
						result = null;

						// Return success.
						return ResultInfo.DefaultSuccess;

					}
				);

				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
				members.Add(member);

			}

			// static void Teleport(Selector selector, Selector selector)
			{

				Modifier member_modifiers = Modifier.Public | Modifier.Static;
				string returnType = "void";
				string member_identifier = "Teleport";
				Scope memberScope = new Scope(member_identifier, typeScope);

				CustomFunction function = new CustomFunction("void",
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(SelectorIdentifier, "selector1"),
						new PredefinedMethodParameter(SelectorIdentifier, "selector2")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						// Get arguments.
						PrimitiveInstance.SelectorInstance selector1 = arguments[0] as PrimitiveInstance.SelectorInstance;
						PrimitiveInstance.SelectorInstance selector2 = arguments[1] as PrimitiveInstance.SelectorInstance;
						
						// Run teleport command.
						location.Writer.WriteCommand($"tp {selector1.Value} {selector2.Value}",
							$"World.Teleport");
						result = null;

						// Return success.
						return ResultInfo.DefaultSuccess;

					}
				);

				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
				members.Add(member);

			}

			// static void Fill(string from, string to, string block)
			{

				Modifier member_modifiers = Modifier.Public | Modifier.Static;
				string returnType = "void";
				string member_identifier = "Fill";
				Scope memberScope = new Scope(member_identifier, typeScope);

				CustomFunction function = new CustomFunction("void",
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(StringIdentifier, "from"),
						new PredefinedMethodParameter(StringIdentifier, "to"),
						new PredefinedMethodParameter(StringIdentifier, "block")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						// Get arguments.
						PrimitiveInstance.StringInstance from = arguments[0] as PrimitiveInstance.StringInstance;
						PrimitiveInstance.StringInstance to = arguments[1] as PrimitiveInstance.StringInstance;
						PrimitiveInstance.StringInstance block = arguments[2] as PrimitiveInstance.StringInstance;
						
						// Run fill command.
						location.Writer.WriteCommand($"fill {from.Value} {to.Value} {block.Value}",
							$"World.Fill");
						result = null;

						// Return success.
						return ResultInfo.DefaultSuccess;

					}
				);

				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
				members.Add(member);

			}

			// static void Fill(string from, string to, string block, string type)
			{

				Modifier member_modifiers = Modifier.Public | Modifier.Static;
				string returnType = "void";
				string member_identifier = "Fill";
				Scope memberScope = new Scope(member_identifier, typeScope);

				CustomFunction function = new CustomFunction("void",
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(StringIdentifier, "from"),
						new PredefinedMethodParameter(StringIdentifier, "to"),
						new PredefinedMethodParameter(StringIdentifier, "block"),
						new PredefinedMethodParameter(StringIdentifier, "type")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
						
						// Get arguments.
						PrimitiveInstance.StringInstance from = arguments[0] as PrimitiveInstance.StringInstance;
						PrimitiveInstance.StringInstance to = arguments[1] as PrimitiveInstance.StringInstance;
						PrimitiveInstance.StringInstance block = arguments[2] as PrimitiveInstance.StringInstance;
						PrimitiveInstance.StringInstance type = arguments[3] as PrimitiveInstance.StringInstance;
						
						// Run fill command.
						location.Writer.WriteCommand($"fill {from.Value} {to.Value} {block.Value} {type.Value}",
							$"World.Fill");
						result = null;

						// Return success.
						return ResultInfo.DefaultSuccess;

					}
				);

				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

				PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
				members.Add(member);

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

			#region Casts

			Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

			{

			}

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {

				throw new NotImplementedException($"'{WorldIdentifier}' initialization has not been implemented.");

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations, casts);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

		/// <summary>
		/// Creates the <see cref="PredefinedTypes"/> for the type "Attribute".
		/// </summary>
		public static PredefinedType CreatePredefinedAttribute(Scope rootScope, List<Action<Compiler.CompileArguments>> onLoadActions) {

			Modifier modifiers = Modifier.Public | Modifier.Abstract;
			ClassType classType = ClassType.Struct;
			string identifier = AttributeIdentifier;
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

			#region Casts

			Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

			{

			}

			#endregion

			IType.InitializeInstanceDelegate init = (location, identifier) => {

				throw new NotImplementedException($"'{AttributeIdentifier}' initialization has not been implemented.");

			};

			predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), new PredefinedType[] { }, init, operations, casts);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

	}

}
