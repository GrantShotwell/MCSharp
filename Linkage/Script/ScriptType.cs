using Antlr4.Runtime.Tree;
using MCSharp.Compilation;
using ConstructorDefinitionContext = MCSharpParser.Constructor_definitionContext;
using MemberDefinitionContext = MCSharpParser.Member_definitionContext;
using TypeDefinitionContext = MCSharpParser.Type_definitionContext;

namespace MCSharp.Linkage.Script {

	/// <summary>
	/// Represents a type defined by script.
	/// </summary>
	public class ScriptType : IType {

		/// <inheritdoc/>
		public Modifier Modifiers { get; }

		/// <inheritdoc/>
		public ClassType ClassType { get; }

		/// <summary>
		/// The local identifier for this type definition.
		/// </summary>
		public ITerminalNode Identifier { get; }
		/// <inheritdoc/>
		string IType.Identifier => Identifier.GetText();

		/// <summary>
		/// The members defined by this type definition.
		/// </summary>
		public ScriptMember[] Members { get; }
		/// <inheritdoc/>
		IMember[] IType.Members => Members;

		/// <summary>
		/// The constructors defined by this type definition.
		/// </summary>
		public ScriptConstructor[] Constructors { get; }
		/// <inheritdoc/>
		IConstructor[] IType.Constructors => Constructors;

		/// <summary>
		/// The type definitions defined within this type definition.
		/// </summary>
		public ScriptType[] SubTypes { get; }
		/// <inheritdoc/>
		IType[] IType.SubTypes => SubTypes;

		public int i_constructor = 0;

		/// <summary>
		/// Creates a new type definition defined by script.
		/// </summary>
		/// <param name="typeContext">The <see cref="MCSharpParser.Type_definitionContext"/> value taken from script.</param>
		/// <param name="memberContexts">The <see cref="MCSharpParser.Member_definitionContext"/> values taken from script.</param>
		/// <param name="constructorContexts">The <see cref="MCSharpParser.Constructor_definitionContext"/> values taken from script.</param>
		/// <param name="settings">Value passed to create <see cref="Minecraft.Function"/>(s).</param>
		/// <param name="virtualMachine">Value passed to create <see cref="Minecraft.Function"/>(s).</param>
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

		public void Dispose() {
			foreach(ScriptMember member in Members) member.Dispose();
		}

	}

}
