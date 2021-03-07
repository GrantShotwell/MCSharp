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

		public static string StorageSelector => "value";
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
				PredefinedMemberDefinition definition = new	PredefinedMemberDefinition.Property(getDefinition, null);

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

			HashSetDictionary<Operation, PredefinedOperation> operations = new HashSetDictionary<Operation, PredefinedOperation>();

			static IInstance ScoreboardOperation(Compiler.CompileArguments compile, IInstance[] method, PredefinedType predefinedType, string op) {
				Scope scope = compile.Scope;
				FunctionWriter writer = compile.Writer;
				string selector = StorageSelector;

				PrimitiveInstance.IntegerInstance left = method[0] as PrimitiveInstance.IntegerInstance;
				PrimitiveInstance.IntegerInstance right = method[1] as PrimitiveInstance.IntegerInstance;

				PrimitiveInstance.IntegerInstance result = predefinedType.InitializeInstance(writer, scope, null) as PrimitiveInstance.IntegerInstance;
				writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} = {selector} {left.Objective.Name}");
				writer.WriteCommand($"scoreboard players operation {selector} {result.Objective.Name} {op} {selector} {right.Objective.Name}");

				return result;
			}

			// Addition
			{

				CustomFunction function = new CustomFunction("int",
					new PredefinedGenericParameter[] {
						
					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter("int", "left"),
						new PredefinedMethodParameter("int", "right")
					},
					(compile, generic, method) => {
						return ScoreboardOperation(compile, method, predefinedType, "+=");
					}
				);

				Operation op = Operation.Addition;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			// Subtraction
			{
				
				CustomFunction function = new CustomFunction("int",
					new PredefinedGenericParameter[] {

					},
					new PredefinedMethodParameter[] {
						new PredefinedMethodParameter("int", "left"),
						new PredefinedMethodParameter("int", "right")
					},
					(compile, generic, method) => {
						return ScoreboardOperation(compile, method, predefinedType, "-=");
					}
				);

				Operation op = Operation.Subtraction;
				operations.Add(op, new PredefinedOperation(op, function));

			}

			#endregion
			PredefinedType.InitializeInstanceDelegate init = (writer, scope, identifier) => {

				if(predefinedType is null) throw new Exception();
				var objective = Objective.AddObjective(writer, identifier.GetText(), "dummy");
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

			HashSetDictionary<Operation, PredefinedOperation> operations = new HashSetDictionary<Operation, PredefinedOperation>();

			{

			}

			#endregion
			PredefinedType.InitializeInstanceDelegate init = (writer, scope, identifier) => {

				if(predefinedType is null) throw new Exception();
				var objective = Objective.AddObjective(writer, identifier.GetText(), "dummy");
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

			HashSetDictionary<Operation, PredefinedOperation> operations = new HashSetDictionary<Operation, PredefinedOperation>();

			{

			}

			#endregion

			PredefinedType.InitializeInstanceDelegate init = (writer, scope, identifier) => {

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

			HashSetDictionary<Operation, PredefinedOperation> operations = new HashSetDictionary<Operation, PredefinedOperation>();

			{

			}

			#endregion

			PredefinedType.InitializeInstanceDelegate init = (writer, scope, identifier) => {

				throw new NotImplementedException("'string' initialization has not been implemented.");

			};

			var predefinedType = new PredefinedType(modifiers, classType, identifier, members.ToArray(), constructors.ToArray(), subtypes, init, operations);
			foreach(PredefinedMember member in members) member.Declarer = predefinedType;
			foreach(PredefinedConstructor constructor in constructors) constructor.Declarer = predefinedType;
			return predefinedType;

		}

	}

}
