using Antlr4.Runtime.Tree;
using MCSharp.Compilation;
using ConstructorDefinitionContext = MCSharpParser.Constructor_definitionContext;
using MemberDefinitionContext = MCSharpParser.Member_definitionContext;
using TypeDefinitionContext = MCSharpParser.Type_definitionContext;

namespace MCSharp.Linkage.Script {

	public class ScriptType : IType {

		public Modifier Modifiers { get; }
		public ClassType ClassType { get; }
		public ITerminalNode Identifier { get; }
		string IType.Identifier => Identifier.GetText();
		public ScriptMember[] Members { get; }
		IMember[] IType.Members => Members;
		public ScriptType[] SubTypes { get; }
		IType[] IType.SubTypes => SubTypes;

		public int i_constructor = 0;

		public ScriptType(TypeDefinitionContext typeContext, MemberDefinitionContext[] memberContexts, ConstructorDefinitionContext[] constructorContexts, Settings settings, VirtualMachine virtualMachine) {

			Modifiers = EnumLinker.LinkModifiers(typeContext.modifier());

			ClassType = EnumLinker.LinkClassType(typeContext.class_type());

			Identifier = typeContext.NAME();

			Members = new ScriptMember[memberContexts.Length];
			for(int i = 0; i < memberContexts.Length; i++) {
				Members[i] = new ScriptMember(this, memberContexts[i], settings, virtualMachine);
			}

			// Planned feature.
			SubTypes = null;

		}

	}

}
