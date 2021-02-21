using Antlr4.Runtime.Tree;
using MCSharp.Compilation;
using MCSharp.Compilation.Linkage;
using ConstructorDefinitionContext = MCSharpParser.Constructor_definitionContext;
using MemberDefinitionContext = MCSharpParser.Member_definitionContext;
using TypeDefinitionContext = MCSharpParser.Type_definitionContext;

namespace MCSharp.Linkage {

	public class ScriptClass {

		public Modifier Modifiers { get; }
		public ClassType ClassType { get; }
		public ITerminalNode Identifier { get; }
		public ScriptMember[] Members { get; }
		public ScriptClass[] SubTypes { get; }

		public int i_constructor = 0;

		public ScriptClass(TypeDefinitionContext typeContext, MemberDefinitionContext[] memberContexts, ConstructorDefinitionContext[] constructorContexts, Settings settings, VirtualMachine virtualMachine) {

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
