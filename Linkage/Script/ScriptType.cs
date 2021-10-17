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

		/// <inheritdoc cref="IType.Identifier"/>
		public ITerminalNode Identifier { get; }
		/// <inheritdoc/>
		string IType.Identifier => Identifier.GetText();

		/// <inheritdoc/>
		public IReadOnlyCollection<IType> BaseTypes { get; }

		/// <inheritdoc/>
		public ICollection<IType> DerivedTypes { get; } = new List<IType>();

		/// <inheritdoc/>
		public Scope Scope { get; set; }

		/// <inheritdoc cref="IType.Members"/>
		public IReadOnlyCollection<ScriptMember> Members { get; }
		/// <inheritdoc/>
		IReadOnlyCollection<IMember> IType.Members => Members;

		/// <inheritdoc cref="IType.Constructors"/>
		public IReadOnlyCollection<ScriptConstructor> Constructors { get; }
		/// <inheritdoc/>
		IReadOnlyCollection<IConstructor> IType.Constructors => Constructors;
		public int i_constructor = 0;

		/// <inheritdoc cref="IType.SubTypes"/>
		public IReadOnlyCollection<ScriptType> SubTypes { get; }
		/// <inheritdoc/>
		IReadOnlyCollection<IType> IType.SubTypes => SubTypes;

		/// <inheritdoc/>
		public IHashSetDictionary<Operation, IOperation> Operations { get; }

		/// <inheritdoc/>
		public IDictionary<IType, IConversion> Conversions { get; }


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

			// TODO: Get base types.
			BaseTypes = new List<IType>();
			foreach(IType baseType in BaseTypes) {
				baseType.DerivedTypes.Add(this);
			}

			// Get members (excluding constructors).
			ScriptMember[] members = new ScriptMember[memberContexts.Length];
			for(int i = 0; i < memberContexts.Length; i++) {
				members[i] = new ScriptMember(this, memberContexts[i], settings, virtualMachine);
			}
			Members = members;

			// Get constructors.
			ScriptConstructor[] constructors = new ScriptConstructor[constructorContexts.Length];
			for(int i = 0; i < constructorContexts.Length; i++) {
				constructors[i] = new ScriptConstructor(this, constructorContexts[i], settings, virtualMachine);
			}
			Constructors = constructors;

			// TODO
			SubTypes = new List<ScriptType>();
			Conversions = new Dictionary<IType, IConversion>();
			Operations = new HashSetDictionary<Operation, IOperation>();

		}


		/// <inheritdoc/>
		public IInstance InitializeInstance(Compiler.CompileArguments location, string identifier) {

			switch(ClassType) {

				case ClassType.Class:
					throw new NotImplementedException($"Initializing script-defined {ClassType.Class} types have not been implemented.");
				case ClassType.Struct:
					return new StructInstance(location, this, identifier);
				case ClassType.Primitive:
					throw new InvalidOperationException($"Script-defined types cannot be primitive ({nameof(ClassType)} is {ClassType.Primitive}).");
				default:
					throw new InvalidOperationException($"Unknown {nameof(Linkage.ClassType)} '{ClassType}'.");
			}

			throw new NotImplementedException("Initializing script types has not been implemented.");

		}

		/// <inheritdoc/>
		public void Dispose() {
			foreach(ScriptMember member in Members) member.Dispose();
		}
	}

}
