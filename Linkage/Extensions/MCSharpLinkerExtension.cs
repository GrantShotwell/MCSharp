using MCSharp.Collections;
using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Minecraft;
using MCSharp.Linkage.Predefined;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Extensions {

	public class MCSharpLinkerExtension : LinkerExtension {

		public static string StorageSelector => "mcs.value";
		public static string IntIdentifier => "int";
		public static string BoolIdentifier => "bool";
		public static string ObjectiveIdentifier => "objective";
		public static string StringIdentifier => "string";

		public MCSharpLinkerExtension(Compiler compiler) : base(compiler) { }

		public override void CreatePredefinedTypes() {

			PredefinedType @int = CreatePredefinedInt();
			Compiler.DefinedTypes.Add(@int.Identifier, @int);

			PredefinedType @bool = CreatePredefinedBool();
			Compiler.DefinedTypes.Add(@bool.Identifier, @bool);

			PredefinedType objective = CreatePredefinedObjective();
			Compiler.DefinedTypes.Add(objective.Identifier, objective);

			PredefinedType @string = CreatePredefinedString();
			Compiler.DefinedTypes.Add(@string.Identifier, @string);

		}

		private static PredefinedType CreatePredefinedInt() {

			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = IntIdentifier;
			PredefinedType[] subtypes = new PredefinedType[] { };
			PredefinedType predefinedType = null;

			#region Members

			List<PredefinedMember> members = new List<PredefinedMember>();

			// Objective (field)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "objective";

				PredefinedExpression initializer = new PredefinedExpression("= new objective(\"dummy\");");
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Field(initializer);

				PredefinedMember member = new PredefinedMember(null, member_modifiers, returnType, member_identifier, MemberType.Field, definition);
				members.Add(member);

			}

			// Objective (property)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "Objective";

				var getStatements = new IStatement[] { new PredefinedStatement("return objective;") };
				InlineStatementFunction getDefinition = new InlineStatementFunction(new IGenericParameter[] { }, new IMethodParameter[] { }, getStatements, returnType);
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Property(getDefinition, null);

				PredefinedMember member = new PredefinedMember(null, member_modifiers, returnType, member_identifier, MemberType.Property, definition);
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

			static IInstance ScoreboardOperation(Compiler.CompileArguments compile, IInstance[] method, PredefinedType predefinedType, string op, Func<int, int, int> evaluateConstants) {

				Scope scope = compile.Scope;
				FunctionWriter writer = compile.Writer;
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
					PrimitiveInstance.IntegerInstance result = predefinedType.InitializeInstance(compile, null) as PrimitiveInstance.IntegerInstance;
					writer.WriteCommand($"scoreboard players set {selector} {result.Objective.Name} {leftConstant.Value}");
					writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} {op} {selector} {right.Objective.Name}");
					return result;
				} else if(rightConstant != null) {
					PrimitiveInstance.IntegerInstance result = predefinedType.InitializeInstance(compile, null) as PrimitiveInstance.IntegerInstance;
					writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} = {selector} {left.Objective.Name}");
					right = predefinedType.InitializeInstance(compile, null) as PrimitiveInstance.IntegerInstance;
					writer.WriteCommand($"scoreboard players set {selector} {right.Objective.Name} {rightConstant.Value}");
					writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} {op} {selector} {right.Objective.Name}");
					return result;
				} else {
					PrimitiveInstance.IntegerInstance result = predefinedType.InitializeInstance(compile, null) as PrimitiveInstance.IntegerInstance;
					writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} = {selector} {left.Objective.Name}");
					writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} {op} {selector} {right.Objective.Name}");
					return result;
				}

			}

			static IInstance ScoreboardDirectOperation(Compiler.CompileArguments compile, IInstance[] method, PredefinedType predefinedType, string op, string compact) {

				Scope scope = compile.Scope;
				FunctionWriter writer = compile.Writer;
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
						right = predefinedType.InitializeInstance(compile, null) as PrimitiveInstance.IntegerInstance;
						writer.WriteCommand($"scoreboard players set {selector} {right.Objective.Name} {rightConstant.Value}");
						writer.WriteCommand($"scoreboard players operation {selector} {left.Objective.Name} {op} {selector} {right.Objective.Name}");
					}
					return left;
				} else {
					writer.WriteCommand($"scoreboard players operation {selector} {left.Objective.Name} {op} {selector} {right.Objective.Name}");
					return left;
				}

			}

			// Assign
			{

				CustomFunction function = new CustomFunction(IntIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "left"),
						new PredefinedMethodParameter(IntIdentifier, "right")
					},
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {

						string selector = StorageSelector;

						PrimitiveInstance.IntegerInstance left = arguments[0] as PrimitiveInstance.IntegerInstance;
						PrimitiveInstance.IntegerInstance.Constant leftConstant = left is null ? arguments[0] as PrimitiveInstance.IntegerInstance.Constant : null;
						PrimitiveInstance.IntegerInstance right = arguments[1] as PrimitiveInstance.IntegerInstance;
						PrimitiveInstance.IntegerInstance.Constant rightConstant = right is null ? arguments[1] as PrimitiveInstance.IntegerInstance.Constant : null;

						if(leftConstant != null) {
							throw new InvalidOperationException("Cannot assign to a constant.");
						} else if(rightConstant != null) {
							compile.Writer.WriteCommand($"scoreboard players set {selector} {left.Objective.Name} {rightConstant.Value}");
						} else {
							compile.Writer.WriteCommand($"scoreboard players operation {selector} {left.Objective.Name} = {selector} {right.Objective.Name}");
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

				CustomFunction function = new CustomFunction(IntIdentifier,
					new PredefinedGenericParameter[] {
						
					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "left"),
						new PredefinedMethodParameter(IntIdentifier, "right")
					},
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardOperation(compile, arguments, predefinedType, "+=", (left, right) => left + right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.Addition;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Addition (Assign)
			{

				CustomFunction function = new CustomFunction(IntIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "left"),
						new PredefinedMethodParameter(IntIdentifier, "right")
					},
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardDirectOperation(compile, arguments, predefinedType, "+=", "add");
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.AssignAddition;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Subtraction
			{
				
				CustomFunction function = new CustomFunction(IntIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "left"),
						new PredefinedMethodParameter(IntIdentifier, "right")
					},
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardOperation(compile, arguments, predefinedType, "-=", (left, right) => left - right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.Subtraction;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Subtraction (Assign)
			{
				
				CustomFunction function = new CustomFunction(IntIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "left"),
						new PredefinedMethodParameter(IntIdentifier, "right")
					},
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardDirectOperation(compile, arguments, predefinedType, "-=", "remove");
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.AssignSubtraction;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Multiplication
			{

				CustomFunction function = new CustomFunction(IntIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "left"),
						new PredefinedMethodParameter(IntIdentifier, "right")
					},
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardOperation(compile, arguments, predefinedType, "*=", (left, right) => left * right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.Multiplication;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Multiplication (Assign)
			{

				CustomFunction function = new CustomFunction(IntIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "left"),
						new PredefinedMethodParameter(IntIdentifier, "right")
					},
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardDirectOperation(compile, arguments, predefinedType, "*=", null);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.AssignMultiplication;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Division
			{

				CustomFunction function = new CustomFunction(IntIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "left"),
						new PredefinedMethodParameter(IntIdentifier, "right")
					},
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardOperation(compile, arguments, predefinedType, "/=", (left, right) => left / right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.Division;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Division (Assign)
			{

				CustomFunction function = new CustomFunction(IntIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "left"),
						new PredefinedMethodParameter(IntIdentifier, "right")
					},
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardDirectOperation(compile, arguments, predefinedType, "/=", null);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.AssignDivision;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Modulo
			{

				CustomFunction function = new CustomFunction(IntIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "left"),
						new PredefinedMethodParameter(IntIdentifier, "right")
					},
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardOperation(compile, arguments, predefinedType, "%=", (left, right) => left % right);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.Modulo;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Modulo (Assign)
			{

				CustomFunction function = new CustomFunction(IntIdentifier,
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter(IntIdentifier, "left"),
						new PredefinedMethodParameter(IntIdentifier, "right")
					},
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {
						result = ScoreboardDirectOperation(compile, arguments, predefinedType, "%=", null);
						return ResultInfo.DefaultSuccess;
					}
				);

				Operation op = Operation.AssignModulo;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			#endregion

			PredefinedType.InitializeInstanceDelegate init = (location, identifier) => {

				if(predefinedType is null) throw new Exception();
				var objective = Objective.AddObjective(location.Writer, identifier, "dummy");
				return new PrimitiveInstance.IntegerInstance(predefinedType, identifier, objective);

			};

			predefinedType = new PredefinedType(modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), subtypes, init, operations);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;
		}

		private static PredefinedType CreatePredefinedBool() {

			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = BoolIdentifier;
			PredefinedType[] subtypes = new PredefinedType[] { };
			PredefinedType predefinedType = null;

			#region Members

			List<PredefinedMember> members = new List<PredefinedMember>();

			// Objective (field)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "objective";

				PredefinedExpression initializer = new PredefinedExpression("= new objective(\"dummy\");");
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Field(initializer);

				PredefinedMember member = new PredefinedMember(null, member_modifiers, returnType, member_identifier, MemberType.Field, definition);
				members.Add(member);

			}

			// Objective (property)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "Objective";

				var getStatements = new IStatement[] { new PredefinedStatement("return objective;") };
				InlineStatementFunction getDefinition = new InlineStatementFunction(new IGenericParameter[] { }, new IMethodParameter[] { }, getStatements, returnType);
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Property(getDefinition, null);

				PredefinedMember member = new PredefinedMember(null, member_modifiers, returnType, member_identifier, MemberType.Property, definition);
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
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {

						string selector = StorageSelector;

						PrimitiveInstance.BooleanInstance left = arguments[0] as PrimitiveInstance.BooleanInstance;
						PrimitiveInstance.BooleanInstance.Constant leftConstant = left is null ? arguments[0] as PrimitiveInstance.BooleanInstance.Constant : null;
						PrimitiveInstance.BooleanInstance right = arguments[1] as PrimitiveInstance.BooleanInstance;
						PrimitiveInstance.BooleanInstance.Constant rightConstant = right is null ? arguments[1] as PrimitiveInstance.BooleanInstance.Constant : null;

						if(leftConstant != null) {
							throw new InvalidOperationException("Cannot assign to a constant.");
						} else if(rightConstant != null) {
							compile.Writer.WriteCommand($"scoreboard players set {selector} {left.Objective.Name} {(rightConstant.Value ? 1 : 0)}");
						} else {
							compile.Writer.WriteCommand($"scoreboard players operation {selector} {left.Objective.Name} = {selector} {right.Objective.Name}");
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
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {

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
								PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(compile, null)) as PrimitiveInstance.BooleanInstance;
								compile.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} {(leftConstant.Value ? 1 : 0)}");
							}
						} else if(rightConstant != null) {
							if(!rightConstant.Value) {
								result = rightConstant;
							} else {
								PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(compile, null)) as PrimitiveInstance.BooleanInstance;
								compile.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} {(rightConstant.Value ? 1 : 0)}");
								result = instance;
							}
						} else {
							PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(compile, null)) as PrimitiveInstance.BooleanInstance;
							compile.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} 0");
							compile.Writer.WriteCommand($"execute if score {selector} {left.Objective.Name} matches 1.. if score {selector} {right.Objective.Name} matches 1.. " +
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
					(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) => {

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
								PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(compile, null)) as PrimitiveInstance.BooleanInstance;
								compile.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} {(leftConstant.Value ? 1 : 0)}");
							}
						} else if(rightConstant != null) {
							if(rightConstant.Value) {
								result = rightConstant;
							} else {
								PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(compile, null)) as PrimitiveInstance.BooleanInstance;
								compile.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} {(rightConstant.Value ? 1 : 0)}");
							}
						} else {
							PrimitiveInstance.BooleanInstance instance = (result = predefinedType.InitializeInstance(compile, null)) as PrimitiveInstance.BooleanInstance;
							compile.Writer.WriteCommand($"scoreboard players set {selector} {instance.Objective.Name} 1");
							compile.Writer.WriteCommand($"execute if score {selector} {left.Objective.Name} matches ..0 if score {selector} {right.Objective.Name} matches ..0 " +
								$"run scoreboard players set {selector} {instance.Objective.Name} 0");
						}

						return ResultInfo.DefaultSuccess;

					}
				);

				Operation op = Operation.BooleanOR;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			#endregion
			PredefinedType.InitializeInstanceDelegate init = (location, identifier) => {

				if(predefinedType is null) throw new Exception();
				var objective = Objective.AddObjective(location.Writer, identifier, "dummy");
				return new PrimitiveInstance.BooleanInstance(predefinedType, identifier, objective);

			};

			predefinedType = new PredefinedType(modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), subtypes, init, operations);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

		private static PredefinedType CreatePredefinedObjective() {
			
			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = ObjectiveIdentifier;
			PredefinedType[] subtypes = new PredefinedType[] { };

			#region Members

			List<PredefinedMember> members = new List<PredefinedMember>();

			// Name (field)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "name";

				PredefinedExpression initializer = null;
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Field(initializer);

				PredefinedMember member = new PredefinedMember(null, member_modifiers, returnType, member_identifier, MemberType.Field, definition);
				members.Add(member);

			}

			// Name (property)
			{

				Modifier member_modifiers = Modifier.Private;
				string returnType = ObjectiveIdentifier;
				string member_identifier = "Name";

				var getStatements = new IStatement[] { new PredefinedStatement("return name;") };
				InlineStatementFunction getDefinition = new InlineStatementFunction(new IGenericParameter[] { }, new IMethodParameter[] { }, getStatements, returnType);
				PredefinedMemberDefinition definition = new PredefinedMemberDefinition.Property(getDefinition, null);

				PredefinedMember member = new PredefinedMember(null, member_modifiers, returnType, member_identifier, MemberType.Property, definition);
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

			PredefinedType.InitializeInstanceDelegate init = (location, identifier) => {

				throw new NotImplementedException("'objective' initialization has not been implemented.");

			};

			var predefinedType = new PredefinedType(modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), subtypes, init, operations);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

		private static PredefinedType CreatePredefinedString() {

			Modifier modifiers = Modifier.Public;
			ClassType classType = ClassType.Primitive;
			string identifier = StringIdentifier;
			PredefinedType[] subtypes = new PredefinedType[] { };

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

			PredefinedType.InitializeInstanceDelegate init = (location, identifier) => {

				throw new NotImplementedException("'string' initialization has not been implemented.");

			};

			var predefinedType = new PredefinedType(modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), subtypes, init, operations);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

	}

}
