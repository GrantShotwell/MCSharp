using Antlr4.Runtime.Tree;
using MCSharp.Compilation;
using MemberDefinitionContext = MCSharpParser.Member_definitionContext;

namespace MCSharp.Linkage.Script {

	public class ScriptMember : IMember {

		public ScriptType Declarer { get; }
		IType IMember.Declarer => Declarer;
		public MemberDefinitionContext FullContext { get; }

		public Modifier Modifiers { get; }
		public ITerminalNode ReturnTypeIdentifier { get; }
		string IMember.ReturnTypeIdentifier => ReturnTypeIdentifier.GetText();
		public ITerminalNode Identifier { get; }
		string IMember.Identifier => Identifier.GetText();
		public MemberType MemberType { get; }
		public ScriptMemberDefinition Definition { get; }
		IMemberDefinition IMember.Definition => Definition;

		public ScriptMember(ScriptType declarer, MemberDefinitionContext context, Settings settings, VirtualMachine virtualMachine) {

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
