using MCSharp.Collections;
using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Minecraft;
using MCSharp.Linkage.Minecraft.Text;
using MCSharp.Linkage.Predefined;
using System;
using System.Collections.Generic;

namespace MCSharp.Linkage.Extensions;

/// <summary>
/// 
/// </summary>
public class MCSharpLinkerExtension : LinkerExtension {

	/// <summary>
	/// The selector for default storage.
	/// </summary>
	public static string StorageSelector => "mcs.value";

	#region Primitive Type Identifiers
	/// <summary>Identifier for the object type.</summary>
	public static string ObjectIdentifier => "object";
	/// <summary>Identifier for the objective type.</summary>
	public static string ObjectiveIdentifier => "objective";
	/// <summary>Identifier for the integer type.</summary>
	public static string IntIdentifier => "int";
	/// <summary>Identifier for the boolean type.</summary>
	public static string FloatIdentifier => "float";
	/// <summary>Identifier for the string type.</summary>
	public static string BoolIdentifier => "bool";
	/// <summary>Identifier for the floating-point type.</summary>
	public static string StringIdentifier => "string";
	/// <summary>Identifier for the selector type.</summary>
	public static string SelectorIdentifier => "Selector";
	/// <summary>Identifier for the JSON type.</summary>
	public static string JsonIdentifier => "Json";
	#endregion

	#region Static Type Identifiers
	/// <summary>Identifier for the "chat" class.</summary>
	public static string ChatIdentifier => "Chat";
	/// <summary>Identifier for the "world" class.</summary>
	public static string WorldIdentifier => "World";
	/// <summary>Identifier for the "math" class.</summary>
	public static string MathIdentifier => "Math";
	#endregion

	#region Attributes Type Identifiers
	/// <summary>Identifier for the attribute abstract class.</summary>
	public static string AttributeIdentifier => "Attribute";
	/// <summary>Identifier for the entity attribute.</summary>
	public static string EntityAttributeIdentifier => "Entity";
	#endregion


	/// <inheritdoc/>
	public MCSharpLinkerExtension(Compiler compiler) : base(compiler) { }

	/// <inheritdoc/>
	public override void CreatePredefinedTypes(Scope rootScope, ref Compiler.OnLoadDelegate onLoad, ref Compiler.OnTickDelegate onTick) {

		onLoad += (location) => {
			location.Writer.WriteComments(
				"Add objective to store object IDs.");
			Objective.AddObjective(location.Writer, ObjectInstance.AddressObjective);
			return ResultInfo.DefaultSuccess;
		};
		onTick += (location) => ResultInfo.DefaultSuccess;

		// Add the object type.
		PredefinedType @object = CreatePredefinedObject(rootScope, ref onLoad);
		Compiler.DefinedTypes.Add(@object.Identifier, @object);

		// Add the objective type.
		PredefinedType objective = CreatePredefinedObjective(rootScope, ref onLoad);
		Compiler.DefinedTypes.Add(objective.Identifier, objective);

		// Add the int type.
		PredefinedType @int = CreatePredefinedInt(rootScope, ref onLoad);
		Compiler.DefinedTypes.Add(@int.Identifier, @int);

		// Add the float type.
		//PredefinedType @float = CreatePredefinedFloat(rootScope, ref onLoad);
		//Compiler.DefinedTypes.Add(@float.Identifier, @float);

		// Add the bool type.
		PredefinedType @bool = CreatePredefinedBool(rootScope, ref onLoad);
		Compiler.DefinedTypes.Add(@bool.Identifier, @bool);

		// Add the string type.
		PredefinedType @string = CreatePredefinedString(rootScope, ref onLoad);
		Compiler.DefinedTypes.Add(@string.Identifier, @string);

		// Add the Selector type.
		PredefinedType selector = CreatePredefinedSelector(rootScope, ref onLoad);
		Compiler.DefinedTypes.Add(selector.Identifier, selector);

		// Add the Json type.
		PredefinedType json = CreatePredefinedJson(rootScope, ref onLoad);
		Compiler.DefinedTypes.Add(json.Identifier, json);

		// Add the Chat type.
		PredefinedType chat = CreatePredefinedChat(rootScope, ref onLoad);
		Compiler.DefinedTypes.Add(chat.Identifier, chat);

		// Add the World type.
		PredefinedType world = CreatePredefinedWorld(rootScope, ref onLoad);
		Compiler.DefinedTypes.Add(world.Identifier, world);

		// Add the Math type.
		PredefinedType math = CreatePredefinedMath(rootScope, ref onLoad);
		Compiler.DefinedTypes.Add(math.Identifier, math);

		// Add the Attribute type.
		PredefinedType attribute = CreatePredefinedAttribute(rootScope, ref onLoad);
		Compiler.DefinedTypes.Add(attribute.Identifier, attribute);

	}

	/// <summary>
	/// Creates the <see cref="PredefinedType"/> for the <see cref="ObjectInstance"/> type "object".
	/// </summary>
	private static PredefinedType CreatePredefinedObject(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					IType type = location.Compiler.DefinedTypes[identifier];

					var left = arguments[0] as ObjectInstance;
					var right = arguments[1] as ObjectInstance;

					ResultInfo assignResult = Compiler.CompileSimpleOperation(location, Operation.Assign, left.Pointer, right.Pointer, out result);
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					IType type = location.Compiler.DefinedTypes[identifier];

					var left = arguments[0] as ObjectInstance;
					var right = arguments[1] as ObjectInstance;

					ResultInfo comparisonResult = Compiler.CompileSimpleOperation(location, Operation.Equality, left.Pointer, right.Pointer, out result);
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					IType type = location.Compiler.DefinedTypes[identifier];

					var left = arguments[0] as ObjectInstance;
					var right = arguments[1] as ObjectInstance;

					ResultInfo comparisonResult = Compiler.CompileSimpleOperation(location, Operation.Inequality, left.Pointer, right.Pointer, out result);
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

		ResultInfo OnLoad(Compiler.CompileArguments location) {

			return ResultInfo.DefaultSuccess;

		}

		onLoad += OnLoad;

		#endregion

		IInstance Init(Compiler.CompileArguments location, string identifier) {

			if(predefinedType is null) throw new Exception();
			var rand = VirtualMachine.GenerateRandomIntegerInstance(location);
			return new ObjectInstance(location, predefinedType, identifier, rand);

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}

	/// <summary>
	/// Creates the <see cref="PredefinedType"/> for the <see cref="PrimitiveInstance.IntegerInstance"/> type "int".
	/// </summary>
	private static PredefinedType CreatePredefinedInt(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

		Modifier modifiers = Modifier.Public;
		ClassType classType = ClassType.Primitive;
		string identifier = IntIdentifier;
		PredefinedType[] subtypes = Array.Empty<PredefinedType>();
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
			InlineStatementFunction getDefinition = new InlineStatementFunction(Array.Empty<IGenericParameter>(), Array.Empty<IMethodParameter>(), getStatements, returnType, identifier);
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {
					// Get arguments.
					PrimitiveInstance.IntegerInstance.Constant leftConstant = arguments[0] is not PrimitiveInstance.IntegerInstance left ? arguments[0] as PrimitiveInstance.IntegerInstance.Constant : null;
					PrimitiveInstance.IntegerInstance.Constant rightConstant = arguments[1] is not PrimitiveInstance.IntegerInstance right ? arguments[1] as PrimitiveInstance.IntegerInstance.Constant : null;

					throw new NotImplementedException();

				}
			);

			Operation op = Operation.BitwiseAND;
			operations.Add(op, new PredefinedOperation(op, function));

		}

		#endregion

		#region Casts

		Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

		ResultInfo OnLoad(Compiler.CompileArguments location) {

			// Json (implicit)
			{

				// Get cast types.
				IType reference = location.Compiler.DefinedTypes[IntIdentifier];
				IType target = location.Compiler.DefinedTypes[JsonIdentifier];

				// Create cast function.
				CustomFunction function = new CustomFunction(JsonIdentifier, null,
					Array.Empty<PredefinedGenericParameter>(),
					new PredefinedMethodParameter[] {
							new PredefinedMethodParameter(IntIdentifier, "value")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						// Get arguments.
						PrimitiveInstance.IntegerInstance value = arguments[0] as PrimitiveInstance.IntegerInstance;
						PrimitiveInstance.IntegerInstance.Constant constant = value is null ? arguments[0] as PrimitiveInstance.IntegerInstance.Constant : null;

						// Make JSON text from boolean.
						RawText json;
						if(constant != null) {

							// Use constant value.
							json = new RawText() {
								Text = constant.Value.ToString()
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

						// Create JSON instance.
						result = new PrimitiveInstance.JsonInstance(target, null, json);

						// Return a success.
						return ResultInfo.DefaultSuccess;

					}
				);

				// Create and add cast.
				var cast = new PredefinedConversion(reference, target, function, @implicit: true);
				casts.Add(target, cast);

			}

			return ResultInfo.DefaultSuccess;

		}

		onLoad += OnLoad;

		#endregion

		IInstance Init(Compiler.CompileArguments location, string identifier) {

			if(predefinedType is null) throw new Exception();
			Objective objective = Objective.AddObjective(location.Writer, null, "dummy");
			location.Writer.WriteCommand($"scoreboard players set {StorageSelector} {objective.Name} 0");
			return new PrimitiveInstance.IntegerInstance(predefinedType, identifier, objective);

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}

	/// <summary>
	/// Creates the <see cref="PredefinedType"/> for the <see cref="PrimitiveType.FloatInstance"/> type.
	/// </summary>
	private static PredefinedType CreatePredefinedFloat(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

		Modifier modifiers = Modifier.Public;
		ClassType classType = ClassType.Primitive;
		string identifier = FloatIdentifier;
		PredefinedType[] subtypes = Array.Empty<PredefinedType>();
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

		static IInstance ScoreboardOperation(
			Compiler.CompileArguments location, IInstance[] method, PredefinedType predefinedType,
			string op, bool align,
			Func<(int, int), (int, int), (int, int)> evaluateConstants
		) {

			Scope scope = location.Scope;
			FunctionWriter writer = location.Writer;
			string selector = StorageSelector;

			PrimitiveInstance.FloatInstance left = method[0] as PrimitiveInstance.FloatInstance;
			PrimitiveInstance.FloatInstance.Constant leftConstant = left is null ? method[0] as PrimitiveInstance.FloatInstance.Constant : null;
			PrimitiveInstance.FloatInstance right = method[1] as PrimitiveInstance.FloatInstance;
			PrimitiveInstance.FloatInstance.Constant rightConstant = right is null ? method[1] as PrimitiveInstance.FloatInstance.Constant : null;

			if(leftConstant is not null && rightConstant is not null) {

				(int, int) value = evaluateConstants(leftConstant.Value, rightConstant.Value);
				PrimitiveInstance.FloatInstance.Constant result = new PrimitiveInstance.FloatInstance.Constant(predefinedType, null, value);
				return result;

			} else if(leftConstant is not null) {

				PrimitiveInstance.FloatInstance result = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.FloatInstance;
				writer.WriteCommand($"scoreboard players set {selector} {result.ValueObjective.Name} {leftConstant.Value.Value}");
				writer.WriteCommand($"scoreboard players set {selector} {result.ScaleObjective.Name} {leftConstant.Value.Scale}");

				throw new NotImplementedException();
				return result;

			} else if(rightConstant is not null) {

				PrimitiveInstance.FloatInstance result = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.FloatInstance;
				//writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} = {selector} {left.Objective.Name}");
				right = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.FloatInstance;

				throw new NotImplementedException();
				return result;

			} else {

				PrimitiveInstance.FloatInstance _left = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.FloatInstance;
				writer.WriteCommand($"scoreboard players operation {selector} {_left.ValueObjective.Name} = {selector} {left.ValueObjective.Name}");
				writer.WriteCommand($"scoreboard players operation {selector} {_left.ScaleObjective.Name} = {selector} {left.ScaleObjective.Name}");

				PrimitiveInstance.FloatInstance _right = predefinedType.InitializeInstance(location, null) as PrimitiveInstance.FloatInstance;
				writer.WriteCommand($"scoreboard players operation {selector} {_right.ValueObjective.Name} {op} {selector} {right.ValueObjective.Name}");
				writer.WriteCommand($"scoreboard players operation {selector} {_right.ScaleObjective.Name} {op} {selector} {right.ScaleObjective.Name}");

				if(align) {

					throw new NotImplementedException();

				} else {

					// Apply the operation to the exponent.
					writer.WriteCommand($"scoreboard players operation {selector} {_left.ScaleObjective.Name} {op} {selector} {_right.ScaleObjective.Name}");
					writer.WriteCommand($"scoreboard players operation {selector} {_left.ValueObjective.Name} {op} {selector} {_right.ValueObjective.Name}");

				}
				return _left;

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

			CustomFunction function = new CustomFunction(identifier, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(identifier, "left"),
						new PredefinedMethodParameter(identifier, "right")
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					string selector = StorageSelector;

					PrimitiveInstance.FloatInstance left = arguments[0] as PrimitiveInstance.FloatInstance;
					PrimitiveInstance.FloatInstance.Constant leftConstant = left is null ? arguments[0] as PrimitiveInstance.FloatInstance.Constant : null;
					PrimitiveInstance.FloatInstance right = arguments[1] as PrimitiveInstance.FloatInstance;
					PrimitiveInstance.FloatInstance.Constant rightConstant = right is null ? arguments[1] as PrimitiveInstance.FloatInstance.Constant : null;

					if(leftConstant != null) {
						throw new InvalidOperationException("Cannot assign to a constant.");
					} else if(rightConstant != null) {
						location.Writer.WriteCommand($"scoreboard players set {selector} {left.ValueObjective.Name} {rightConstant.Value.Value}");
						location.Writer.WriteCommand($"scoreboard players set {selector} {left.ScaleObjective.Name} {rightConstant.Value.Scale}");
					} else {
						location.Writer.WriteCommand($"scoreboard players operation {selector} {left.ValueObjective.Name} = {selector} {right.ValueObjective.Name}");
						location.Writer.WriteCommand($"scoreboard players operation {selector} {left.ScaleObjective.Name} = {selector} {right.ScaleObjective.Name}");
					}

					result = left;
					return ResultInfo.DefaultSuccess;

				}
			);

			Operation op = Operation.Assign;
			operations.Add(op, new PredefinedOperation(op, function));

		}

		#endregion

		#region Casts

		Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

		ResultInfo OnLoad(Compiler.CompileArguments location) {

			// Json (implicit)
			{

				// Get cast types.
				IType reference = location.Compiler.DefinedTypes[IntIdentifier];
				IType target = location.Compiler.DefinedTypes[JsonIdentifier];

				// Create cast function.
				CustomFunction function = new CustomFunction(JsonIdentifier, null,
					Array.Empty<PredefinedGenericParameter>(),
					new PredefinedMethodParameter[] {
							new PredefinedMethodParameter(IntIdentifier, "value")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						// Get arguments.
						PrimitiveInstance.IntegerInstance value = arguments[0] as PrimitiveInstance.IntegerInstance;
						PrimitiveInstance.IntegerInstance.Constant constant = value is null ? arguments[0] as PrimitiveInstance.IntegerInstance.Constant : null;

						// Make JSON text from boolean.
						RawText json;
						if(constant != null) {

							// Use constant value.
							json = new RawText() {
								Text = constant.Value.ToString()
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

						// Create JSON instance.
						result = new PrimitiveInstance.JsonInstance(target, null, json);

						// Return a success.
						return ResultInfo.DefaultSuccess;

					}
				);

				// Create and add cast.
				var cast = new PredefinedConversion(reference, target, function, @implicit: true);
				casts.Add(target, cast);

			}

			return ResultInfo.DefaultSuccess;

		}

		onLoad += OnLoad;

		#endregion

		IInstance Init(Compiler.CompileArguments location, string identifier) {

			if(predefinedType is null) throw new Exception();
			Objective objective = Objective.AddObjective(location.Writer, null, "dummy");
			location.Writer.WriteCommand($"scoreboard players set {StorageSelector} {objective.Name} 0");
			return new PrimitiveInstance.IntegerInstance(predefinedType, identifier, objective);

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}

	/// <summary>
	/// Creates the <see cref="PredefinedType"/> for the <see cref="PrimitiveInstance.BooleanInstance"/> type "string".
	/// </summary>
	private static PredefinedType CreatePredefinedBool(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

		Modifier modifiers = Modifier.Public;
		ClassType classType = ClassType.Primitive;
		string identifier = BoolIdentifier;
		PredefinedType[] subtypes = Array.Empty<PredefinedType>();
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
			InlineStatementFunction getDefinition = new InlineStatementFunction(Array.Empty<IGenericParameter>(), Array.Empty<IMethodParameter>(), getStatements, returnType, identifier);
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

			CustomFunction function = new CustomFunction(BoolIdentifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(BoolIdentifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(BoolIdentifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

		ResultInfo OnLoad(Compiler.CompileArguments location) {

			// Json (implicit)
			{

				// Get cast types.
				IType reference = location.Compiler.DefinedTypes[BoolIdentifier];
				IType target = location.Compiler.DefinedTypes[JsonIdentifier];

				// Create cast function.
				CustomFunction function = new CustomFunction(JsonIdentifier, null,
					Array.Empty<PredefinedGenericParameter>(),
					new PredefinedMethodParameter[] {
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

						// Create JSON instance.
						result = new PrimitiveInstance.JsonInstance(target, null, json);

						// Return a success.
						return ResultInfo.DefaultSuccess;

					}
				);

				// Create and add cast.
				var cast = new PredefinedConversion(reference, target, function, @implicit: true);
				casts.Add(target, cast);

			}

			return ResultInfo.DefaultSuccess;

		}

		onLoad += OnLoad;

		#endregion

		IInstance Init(Compiler.CompileArguments location, string identifier) {

			if(predefinedType is null) throw new Exception();
			var objective = Objective.AddObjective(location.Writer, identifier, "dummy");
			return new PrimitiveInstance.BooleanInstance(predefinedType, identifier, objective);

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}

	/// <summary>
	/// Creates the <see cref="PredefinedType"/> for the <see cref="PrimitiveInstance.ObjectiveInstance"/> type "objective".
	/// </summary>
	private static PredefinedType CreatePredefinedObjective(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

		Modifier modifiers = Modifier.Public;
		ClassType classType = ClassType.Primitive;
		string identifier = ObjectiveIdentifier;
		PredefinedType[] subtypes = Array.Empty<PredefinedType>();
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
			InlineStatementFunction getDefinition = new InlineStatementFunction(Array.Empty<IGenericParameter>(), Array.Empty<IMethodParameter>(), getStatements, returnType, identifier);
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

		ResultInfo OnLoad(Compiler.CompileArguments location) {

			return ResultInfo.DefaultSuccess;

		}

		onLoad += OnLoad;

		#endregion

		static IInstance Init(Compiler.CompileArguments location, string identifier) {

			throw new NotImplementedException($"'{ObjectiveIdentifier}' initialization has not been implemented.");

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}

	/// <summary>
	/// Creates the <see cref="PredefinedType"/> for the <see cref="PrimitiveInstance.StringInstance"/> type "string".
	/// </summary>
	private static PredefinedType CreatePredefinedString(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

		Modifier modifiers = Modifier.Public;
		ClassType classType = ClassType.Primitive;
		string identifier = StringIdentifier;
		PredefinedType[] subtypes = Array.Empty<PredefinedType>();
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

		// Addition (string + string)
		{

			CustomFunction function = new CustomFunction(StringIdentifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

		// Addition (string + json)
		{

			string returnType = JsonIdentifier;

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(StringIdentifier, "left"),
						new PredefinedMethodParameter(JsonIdentifier, "right"),
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					PrimitiveInstance.StringInstance left = arguments[0] as PrimitiveInstance.StringInstance;
					PrimitiveInstance.JsonInstance right = arguments[1] as PrimitiveInstance.JsonInstance;

					RawTextList newList = new RawTextList(1 + right.Value.Count) { new() { Text = left.Value } };
					foreach(RawText text in right.Value) newList.Add(text);
					result = new PrimitiveInstance.JsonInstance(location.Compiler.DefinedTypes[returnType], null, newList);

					return ResultInfo.DefaultSuccess;

				}
			);

			Operation op = Operation.Addition;
			operations.Add(op, new PredefinedOperation(op, function));

		}

		// Addition (json + string)
		{

			string returnType = JsonIdentifier;

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(JsonIdentifier, "left"),
						new PredefinedMethodParameter(StringIdentifier, "right"),
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					PrimitiveInstance.JsonInstance left = arguments[0] as PrimitiveInstance.JsonInstance;
					PrimitiveInstance.StringInstance right = arguments[1] as PrimitiveInstance.StringInstance;

					RawTextList newList = new RawTextList(1 + left.Value.Count);
					foreach(RawText text in left.Value) newList.Add(text);
					newList.Add(new() { Text = right.Value });
					result = new PrimitiveInstance.JsonInstance(location.Compiler.DefinedTypes[returnType], null, newList);

					return ResultInfo.DefaultSuccess;

				}
			);

			Operation op = Operation.Addition;
			operations.Add(op, new PredefinedOperation(op, function));

		}

		// Initialization Assign
		{

			CustomFunction function = new CustomFunction(StringIdentifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

		ResultInfo OnLoad(Compiler.CompileArguments location) {

			// Json (implicit)
			{

				// Get cast types.
				IType reference = location.Compiler.DefinedTypes[StringIdentifier];
				IType target = location.Compiler.DefinedTypes[JsonIdentifier];

				// Create cast function.
				CustomFunction function = new CustomFunction(JsonIdentifier, null,
					Array.Empty<PredefinedGenericParameter>(),
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(StringIdentifier, "value")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						// Get arguments.
						PrimitiveInstance.StringInstance value = arguments[0] as PrimitiveInstance.StringInstance;

						// Make JSON text from string. 
						RawText json = new() { Text = value.Value };
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
				CustomFunction function = new CustomFunction(SelectorIdentifier, null,
					Array.Empty<PredefinedGenericParameter>(),
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(StringIdentifier, "value")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						// Get arguments.
						PrimitiveInstance.StringInstance value = arguments[0] as PrimitiveInstance.StringInstance;

						// Make selector from string.
						Selector selector = new(value.Value);
						result = new PrimitiveInstance.SelectorInstance(target, null, selector);

						// Return a success.
						return ResultInfo.DefaultSuccess;

					}
				);

				// Create and add cast.
				var cast = new PredefinedConversion(reference, target, function, @implicit: true);
				casts.Add(target, cast);

			}

			return ResultInfo.DefaultSuccess;

		}

		onLoad += OnLoad;

		#endregion

		IInstance Init(Compiler.CompileArguments location, string identifier) {

			if(predefinedType is null) throw new Exception();
			return new PrimitiveInstance.StringInstance(predefinedType, identifier, null);

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}

	/// <summary>
	/// Creates the <see cref="PredefinedType"/> for the <see cref="PrimitiveInstance.SelectorInstance"/> type "Selector".
	/// </summary>
	private static PredefinedType CreatePredefinedSelector(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

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

			CustomFunction function = new CustomFunction(SelectorIdentifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

		ResultInfo OnLoad(Compiler.CompileArguments location) {

			return ResultInfo.DefaultSuccess;

		}

		onLoad += OnLoad;

		#endregion

		static IInstance Init(Compiler.CompileArguments location, string identifier) {

			throw new NotImplementedException($"'{SelectorIdentifier}' initialization has not been implemented.");

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}

	/// <summary>
	/// Creates the <see cref="PredefinedType"/> for the type "Json".
	/// </summary>
	private static PredefinedType CreatePredefinedJson(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

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

			CustomFunction function = new CustomFunction(JsonIdentifier, null,
				Array.Empty<PredefinedGenericParameter>(),
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

		// Addition
		{

			string returnType = JsonIdentifier;

			CustomFunction function = new CustomFunction(JsonIdentifier, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(JsonIdentifier, "left"),
						new PredefinedMethodParameter(JsonIdentifier, "right")
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					PrimitiveInstance.JsonInstance left = arguments[0] as PrimitiveInstance.JsonInstance;
					PrimitiveInstance.JsonInstance right = arguments[1] as PrimitiveInstance.JsonInstance;

					RawTextList newList = new(left.Value.Count + right.Value.Count);
					foreach(var item in left.Value) newList.Add(item);
					foreach(var item in right.Value) newList.Add(item);

					result = new PrimitiveInstance.JsonInstance(predefinedType, null, newList);
					return ResultInfo.DefaultSuccess;

				}
			);

			Operation op = Operation.Addition;
			operations.Add(op, new PredefinedOperation(op, function));

		}

		#endregion

		#region Casts

		Dictionary<IType, IConversion> casts = new Dictionary<IType, IConversion>();

		ResultInfo OnLoad(Compiler.CompileArguments location) {

			return ResultInfo.DefaultSuccess;

		}

		onLoad += OnLoad;

		#endregion

		IInstance Init(Compiler.CompileArguments location, string identifier) {

			if(predefinedType is null) throw new Exception();
			return new PrimitiveInstance.JsonInstance(predefinedType, identifier, null);

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}

	/// <summary>
	/// Creates the <see cref="PredefinedType"/> for the type "Chat".
	/// </summary>
	private static PredefinedType CreatePredefinedChat(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

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

			CustomFunction function = new CustomFunction("void", null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction("void", null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction("void", null,
				Array.Empty<PredefinedGenericParameter>(),
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

		ResultInfo OnLoad(Compiler.CompileArguments location) {

			return ResultInfo.DefaultSuccess;

		}

		onLoad += OnLoad;

		#endregion

		static IInstance Init(Compiler.CompileArguments location, string identifier) {

			throw new NotImplementedException($"'{ChatIdentifier}' initialization has not been implemented.");

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}

	/// <summary>
	/// Creates the <see cref="PredefinedType"/> for the type "World".
	/// </summary>
	public static PredefinedType CreatePredefinedWorld(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

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

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
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

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
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

		static IInstance Init(Compiler.CompileArguments location, string identifier) {

			throw new NotImplementedException($"'{identifier}' initialization has not been implemented.");

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}

	/// <summary>
	/// Creates the <see cref="PredefinedType"/> for the type "Math".
	/// </summary>
	public static PredefinedType CreatePredefinedMath(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

		Modifier modifiers = Modifier.Public | Modifier.Static;
		ClassType classType = ClassType.Struct;
		string identifier = MathIdentifier;
		PredefinedType predefinedType = null;
		Scope typeScope = new Scope(identifier, rootScope);

		#region Members

		List<PredefinedMember> members = new List<PredefinedMember>();

		// static int Sin(int value)
		{

			Modifier member_modifiers = Modifier.Public | Modifier.Static;
			string returnType = IntIdentifier;
			string member_identifier = "Sin";
			Scope memberScope = new Scope(member_identifier, typeScope);

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "value")
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					// Get arguments.
					PrimitiveInstance.IntegerInstance value = arguments[0] as PrimitiveInstance.IntegerInstance;

					// Find the Sin value.
					throw new NotImplementedException($"Sin has not been implemented.");

					// Return success.
					return ResultInfo.DefaultSuccess;

				}
			);

			PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

			PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
			members.Add(member);

		}

		// static int Cos(int value)
		{

			Modifier member_modifiers = Modifier.Public | Modifier.Static;
			string returnType = IntIdentifier;
			string member_identifier = "Cos";
			Scope memberScope = new Scope(member_identifier, typeScope);

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "value")
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					// Get arguments.
					PrimitiveInstance.IntegerInstance value = arguments[0] as PrimitiveInstance.IntegerInstance;

					// Find the Cos value.
					throw new NotImplementedException($"Cos has not been implemented.");

					// Return success.
					return ResultInfo.DefaultSuccess;

				}
			);

			PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

			PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
			members.Add(member);

		}

		// static int Tan(int value)
		{

			Modifier member_modifiers = Modifier.Public | Modifier.Static;
			string returnType = IntIdentifier;
			string member_identifier = "Tan";
			Scope memberScope = new Scope(member_identifier, typeScope);

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "value")
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					// Get arguments.
					PrimitiveInstance.IntegerInstance value = arguments[0] as PrimitiveInstance.IntegerInstance;

					// Find the Tan value.
					throw new NotImplementedException($"Tan has not been implemented.");

					// Return success.
					return ResultInfo.DefaultSuccess;

				}
			);

			PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

			PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
			members.Add(member);

		}

		// static int Sqrt(int value)
		{

			Modifier member_modifiers = Modifier.Public | Modifier.Static;
			string returnType = IntIdentifier;
			string member_identifier = "Sqrt";
			Scope memberScope = new Scope(member_identifier, typeScope);

			IFunction sqrtLoop = null;
			PrimitiveInstance.IntegerInstance @in, @out, temp, two;

			onLoad += (location) => {

				var definedTypes = location.Compiler.DefinedTypes;
				temp = definedTypes[IntIdentifier].InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
				@in  = definedTypes[IntIdentifier].InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
				@out = definedTypes[IntIdentifier].InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
				two = new PrimitiveInstance.IntegerInstance.Constant(definedTypes[IntIdentifier], null, 2).Copy(location, null) as PrimitiveInstance.IntegerInstance;

				FunctionWriter writer = new(location.Compiler.VirtualMachine, location.Compiler.Settings, identifier, member_identifier);
				predefinedType.OnDispose += writer.Dispose;

				// Use NOPEname's algorithm.
				// https://www.youtube.com/watch?v=o8Te4FPjFKU
				writer.WriteCommand($"scoreboard players operation {StorageSelector} {@out.Objective.Name} += {StorageSelector} {temp.Objective.Name}");
				writer.WriteCommand($"scoreboard players operation {StorageSelector} {@out.Objective.Name} /= {StorageSelector} {two.Objective.Name}");
				writer.WriteCommand($"scoreboard players operation {StorageSelector} {temp.Objective.Name} = {StorageSelector} {@in.Objective.Name}");
				writer.WriteCommand($"scoreboard players operation {StorageSelector} {temp.Objective.Name} /= {StorageSelector} {@out.Objective.Name}");
				writer.WriteCommand($"execute if score {StorageSelector} {@out.Objective.Name} > {StorageSelector} {temp.Objective.Name} run function {writer.GamePath}");

				sqrtLoop = new CustomFunction(returnType, null,
					Array.Empty<PredefinedGenericParameter>(),
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "value")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						// Get arguments.
						PrimitiveInstance.IntegerInstance value = arguments[0] as PrimitiveInstance.IntegerInstance;

						// Invoke function.
						location.Writer.WriteComments(
							"Math.Sqrt\n" +
							"Assign value to 'in' and 'out'.");
						Compiler.CompileSimpleOperation(location, Operation.Assign, @in, value, out _);
						location.Writer.WriteCommand($"execute if score {StorageSelector} {@in.Objective.Name} matches {int.MaxValue} run scoreboard players remove {StorageSelector} {@in.Objective.Name} 1",
							"Max integer value results in an integer overflow. Subtracting one gives the same result thanks to rounding.");
						Compiler.CompileSimpleOperation(location, Operation.Assign, @out, @in, out _);
						location.Writer.WriteCommand($"scoreboard players set {StorageSelector} {temp.Objective.Name} 1",
							"Assign 'temp' to 1.");
						location.Writer.WriteCommand($"execute if score {StorageSelector} {@out.Objective.Name} > {StorageSelector} {temp.Objective.Name} run function {writer.GamePath}",
							"Start loop.");

						// Set result.
						result = @out.Copy(location, null);
						location.Writer.AddBufferedLines(1);

						// Return success.
						return ResultInfo.DefaultSuccess;

					}
				);

				return ResultInfo.DefaultSuccess;

			};

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "value")
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					// Get arguments.
					PrimitiveInstance.IntegerInstance value = arguments[0] as PrimitiveInstance.IntegerInstance;

					// Find the square root.
					ResultInfo sqrtResult = sqrtLoop.InvokeStatic(location, Array.Empty<IType>(), new IInstance[] { value }, out result);
					if(sqrtResult.Failure) return sqrtResult;

					// Return success.
					return ResultInfo.DefaultSuccess;

				}
			);

			PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Method(function);

			PredefinedMember member = new PredefinedMember(memberScope, null, member_modifiers, returnType, member_identifier, MemberType.Method, definition);
			members.Add(member);

		}

		// static int Pow(int value, int exponent)
		{

			Modifier member_modifiers = Modifier.Public | Modifier.Static;
			string returnType = IntIdentifier;
			string member_identifier = "Pow";
			Scope memberScope = new Scope(member_identifier, typeScope);

			IFunction powLoop = null;
			PrimitiveInstance.IntegerInstance n, m, p;

			onLoad += (location) => {

				var definedTypes = location.Compiler.DefinedTypes;
				n = definedTypes[IntIdentifier].InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
				m = definedTypes[IntIdentifier].InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;
				p = definedTypes[IntIdentifier].InitializeInstance(location, null) as PrimitiveInstance.IntegerInstance;

				FunctionWriter writer = new(location.Compiler.VirtualMachine, location.Compiler.Settings, identifier, member_identifier);
				predefinedType.OnDispose += writer.Dispose;
				
				// Simple multiplication loop.
				writer.WriteCommand($"scoreboard players operation {StorageSelector} {n.Objective.Name} *= {StorageSelector} {m.Objective.Name}");
				writer.WriteCommand($"scoreboard players remove {StorageSelector} {p.Objective.Name} 1");
				writer.WriteCommand($"execute if score {StorageSelector} {p.Objective.Name} matches 2.. run function {writer.GamePath}");

				powLoop = new CustomFunction(returnType, null,
					Array.Empty<PredefinedGenericParameter>(),
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "value"),
						new PredefinedMethodParameter(IntIdentifier, "exponent")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

						// Get arguments.
						PrimitiveInstance.IntegerInstance value = arguments[0] as PrimitiveInstance.IntegerInstance;
						PrimitiveInstance.IntegerInstance exponent = arguments[1] as PrimitiveInstance.IntegerInstance;

						// Invoke function.
						location.Writer.WriteCommand($"scoreboard players operation {StorageSelector} {n.Objective.Name} = {StorageSelector} {value.Objective.Name}",
							"Math.Pow\n" +
							"Assign 'value' to 'n' and 'm'.");
						location.Writer.WriteCommand($"scoreboard players operation {StorageSelector} {m.Objective.Name} = {StorageSelector} {value.Objective.Name}");
						location.Writer.WriteCommand($"scoreboard players operation {StorageSelector} {p.Objective.Name} = {StorageSelector} {exponent.Objective.Name}",
							"Assign 'exponent' to 'p'.");
						location.Writer.WriteCommand($"execute if score {StorageSelector} {p.Objective.Name} matches ..-1 run scoreboard players set {StorageSelector} {n.Objective.Name} 0",
							"Negative exponents are always rounded down to 0.");
						location.Writer.WriteCommand($"execute if score {StorageSelector} {p.Objective.Name} matches 0 run scoreboard players set {StorageSelector} {n.Objective.Name} 1",
							"Exponent of zero evaluates exactly to 1.");
						location.Writer.WriteCommand($"execute if score {StorageSelector} {p.Objective.Name} matches 2.. run function {writer.GamePath}",
							"Start loop.");

						// Set result.
						result = n.Copy(location, null);
						location.Writer.AddBufferedLines(1);

						// Return success.
						return ResultInfo.DefaultSuccess;

					}
				);

				return ResultInfo.DefaultSuccess;

			};

			CustomFunction function = new CustomFunction(returnType, null,
				Array.Empty<PredefinedGenericParameter>(),
				new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "value"),
						new PredefinedMethodParameter(IntIdentifier, "exponent")
				},
				(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

					// Get arguments.
					PrimitiveInstance.IntegerInstance value = arguments[0] as PrimitiveInstance.IntegerInstance;
					PrimitiveInstance.IntegerInstance exponent = arguments[1] as PrimitiveInstance.IntegerInstance;

					// Evaluate the power.
					ResultInfo powResult = powLoop.InvokeStatic(location, Array.Empty<IType>(), new IInstance[] { value, exponent }, out result);
					if(powResult.Failure) return powResult;

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

		static IInstance Init(Compiler.CompileArguments location, string identifier) {

			throw new NotImplementedException($"'{identifier}' initialization has not been implemented.");

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}


	/// <summary>
	/// Creates the <see cref="PredefinedType"/>s for the type "Attribute".
	/// </summary>
	public static PredefinedType CreatePredefinedAttribute(Scope rootScope, ref Compiler.OnLoadDelegate onLoad) {

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

		static IInstance Init(Compiler.CompileArguments location, string identifier) {

			throw new NotImplementedException($"'{AttributeIdentifier}' initialization has not been implemented.");

		}

		predefinedType = new PredefinedType(typeScope, modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), Array.Empty<PredefinedType>(), Init, operations, casts);
		foreach(PredefinedMember member in members) member.Declarer = predefinedType;
		foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
		return predefinedType;

	}

}
