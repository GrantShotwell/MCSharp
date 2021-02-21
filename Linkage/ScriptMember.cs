using Antlr4.Runtime.Tree;
using MCSharp.Compilation;
using MCSharp.Compilation.Linkage;
using MemberDefinitionContext = MCSharpParser.Member_definitionContext;

namespace MCSharp.Linkage {

	public class ScriptMember {

		public ScriptClass Declarer { get; }
		public MemberDefinitionContext FullContext { get; }

		public Modifier Modifiers { get; }
		public ITerminalNode ReturnTypeIdentifier { get; }
		public ITerminalNode Identifier { get; }
		public MemberType MemberType { get; }
		public ScriptMemberDefinition Definition { get; }

		public ScriptMember(ScriptClass declarer, MemberDefinitionContext context, Settings settings, VirtualMachine virtualMachine) {

			Declarer = declarer;
			FullContext = context;

			Modifiers = EnumLinker.LinkModifiers(context.modifier());

			ITerminalNode[] names = context.NAME();
			ReturnTypeIdentifier = names[0];
			Identifier = names[1];

			MemberType = EnumLinker.LinkMemberType(context);

			Definition = ScriptMemberDefinition.CreateMemberDefinitionLink(this, settings, virtualMachine);

		}

	}

}
