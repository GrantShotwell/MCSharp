using Antlr4.Runtime.Tree;
using MCSharp.Compilation;
using MemberDefinitionContext = MCSharpParser.Member_definitionContext;

namespace MCSharp.Linkage.Script {

	public class ScriptMember : IMember {

		/// <summary>
		/// The type, defined by script, that has defined this member.
		/// </summary>
		public ScriptType Declarer { get; }
		/// <inheritdoc/>
		IType IMember.Declarer => Declarer;
		/// <summary>
		/// TODO: Possibly remove this.
		/// </summary>
		public MemberDefinitionContext FullContext { get; }

		/// <inheritdoc/>
		public Modifier Modifiers { get; }
		/// <summary>
		/// The local identifier that represents the return type.
		/// </summary>
		public ITerminalNode ReturnTypeIdentifier { get; }
		/// <inheritdoc/>
		string IMember.ReturnTypeIdentifier => ReturnTypeIdentifier.GetText();
		/// <summary>
		/// The local identifier that represents this member.
		/// </summary>
		public ITerminalNode Identifier { get; }
		/// <inheritdoc/>
		string IMember.Identifier => Identifier.GetText();
		/// <inheritdoc/>
		public MemberType MemberType { get; }
		/// <summary>
		/// The <see cref="ScriptMemberDefinition"/> of this <see cref="ScriptMember"/>
		/// </summary>
		public ScriptMemberDefinition Definition { get; }
		/// <inheritdoc/>
		IMemberDefinition IMember.Definition => Definition;

		/// <summary>
		/// Creates a new member defined by script.
		/// </summary>
		/// <param name="declarer">The type that has defined this member.</param>
		/// <param name="context"></param>
		/// <param name="settings">Value passed to create <see cref="Minecraft.Function"/>(s).</param>
		/// <param name="virtualMachine">Value passed to create <see cref="Minecraft.Function"/>(s).</param>
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

		public void Dispose() {
			Definition.Dispose();
		}

	}

}
