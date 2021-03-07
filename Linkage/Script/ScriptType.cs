using Antlr4.Runtime.Tree;
using MCSharp.Collections;
using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using System;
using System.Collections.Generic;
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
		public IReadOnlyCollection<ScriptMember> Members { get; }
		/// <inheritdoc/>
		IReadOnlyCollection<IMember> IType.Members => Members;

		/// <summary>
		/// The constructors defined by this type definition.
		/// </summary>
		public IReadOnlyCollection<ScriptConstructor> Constructors { get; }
		/// <inheritdoc/>
		IReadOnlyCollection<IConstructor> IType.Constructors => Constructors;
		public int i_constructor = 0;

		/// <summary>
		/// The type definitions defined within this type definition.
		/// </summary>
		public IReadOnlyCollection<ScriptType> SubTypes { get; }
		/// <inheritdoc/>
		IReadOnlyCollection<IType> IType.SubTypes => SubTypes;

		/// <inheritdoc/>
		public IHashSetDictionary<Operation, IOperation> Operations { get; }


		/// <summary>
		/// Creates a new type definition defined by script.
		/// </summary>
		/// <param name="typeContext">The <see cref="MCSharpParser.Type_definitionContext"/> value taken from script.</param>
		/// <param name="memberContexts">The <see cref="MCSharpParser.Member_definitionContext"/> values taken from script.</param>
		/// <param name="constructorContexts">The <see cref="MCSharpParser.Constructor_definitionContext"/> values taken from script.</param>
		/// <param name="settings">Value passed to create <see cref="Minecraft.StandaloneStatementFunction"/>(s).</param>
		/// <param name="virtualMachine">Value passed to create <see cref="Minecraft.StandaloneStatementFunction"/>(s).</param>
		public ScriptType(TypeDefinitionContext typeContext, MemberDefinitionContext[] memberContexts, ConstructorDefinitionContext[] constructorContexts, Settings settings, VirtualMachine virtualMachine) {

			Modifiers = EnumLinker.LinkModifiers(typeContext.modifier());

			ClassType = EnumLinker.LinkClassType(typeContext.class_type());

			Identifier = typeContext.NAME();

			ScriptMember[] members = new ScriptMember[memberContexts.Length];
			for(int i = 0; i < memberContexts.Length; i++) {
				// todo: operations
				members[i] = new ScriptMember(this, memberContexts[i], settings, virtualMachine);
			}
			Members = members;

			ScriptConstructor[] constructors = new ScriptConstructor[constructorContexts.Length];
			for(int i = 0; i < constructorContexts.Length; i++) {
				constructors[i] = new ScriptConstructor(this, constructorContexts[i], settings, virtualMachine);
			}
			Constructors = constructors;

			// todo
			SubTypes = null;

		}


		public IInstance InitializeInstance(FunctionWriter function, Scope scope, ITerminalNode identifier) {

			throw new NotImplementedException("Initializing script types has not been implemented.");

		}

		public void Dispose() {
			foreach(ScriptMember member in Members) member.Dispose();
		}
	}

}
