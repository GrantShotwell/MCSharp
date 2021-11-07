using Antlr4.Runtime.Tree;
using MCSharp.Collections;
using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Extensions;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Linq;
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
		/// 
		/// </summary>
		private Func<Compiler.CompileArguments, string, IInstance> InitInstance { get; }


		/// <summary>
		/// Creates a new type definition defined by script.
		/// </summary>
		/// <param name="typeContext">The <see cref="MCSharpParser.Type_definitionContext"/> value taken from script.</param>
		/// <param name="memberContexts">The <see cref="MCSharpParser.Member_definitionContext"/> values taken from script.</param>
		/// <param name="constructorContexts">The <see cref="MCSharpParser.Constructor_definitionContext"/> values taken from script.</param>
		/// <param name="settings">Value passed to create <see cref="StandaloneStatementFunction"/>(s).</param>
		/// <param name="virtualMachine">Value passed to create <see cref="StandaloneStatementFunction"/>(s).</param>
		public ScriptType(TypeDefinitionContext typeContext, MemberDefinitionContext[] memberContexts, ConstructorDefinitionContext[] constructorContexts, Settings settings, VirtualMachine virtualMachine, out Action<Compiler> postFirstPass) {

			Modifiers = EnumLinker.LinkModifiers(typeContext.modifier());
			ClassType = EnumLinker.LinkClassType(typeContext.class_type());
			Identifier = typeContext.NAME();

			// Create a list of actions.
			var secondPassActions = new List<Action<Compiler>>();
			// postFirstPass will trigger all of these actions.
			postFirstPass = (location) => { foreach (var action in secondPassActions) action(location); };

			// Get base types.
			List<IType> baseTypes = new List<IType>();
			// If the type is a class, add the object type.
			if(ClassType == ClassType.Class) {
				// We need to wait until the second pass, so all types are defined.
				secondPassActions.Add((compiler) => { baseTypes.Add(compiler.DefinedTypes[MCSharpLinkerExtension.ObjectIdentifier]); });
			}
			// TODO: Add base types.
			BaseTypes = baseTypes;

			// Update derived types.
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

			// Set 'InitializeInstance' value.
			switch(ClassType) {

				case ClassType.Class: {

					// Use closure to have a field dictionary per type.
					var objectives = new Dictionary<IField, Objective[]>();
					// Use closure to run needed actions during compilation.
					var actions = new List<Action<Compiler.CompileArguments, Compiler.CompileArguments>>(Members.Count);

					foreach(IMember member in Members) {
						switch(member.MemberType) {

							case MemberType.Field: {
								var field = member.Definition as IField;
								actions.Add((typeLocation, initLocation) => {

									// Evaluate default value.
									typeLocation.Compiler.CompileExpression(typeLocation, field.Initializer.Context, out IInstance value);

									// TODO: Move to ???, not initialization.
									//// When an object is created, all fields, stored in objectives, are assigned to that object's entity.
									//if(!objectives.ContainsKey(field)) {
									//	var block = new Objective[typeLocation.Compiler.DefinedTypes[member.ReturnTypeIdentifier].GetBlockSize(initLocation.Compiler)];
									//	typeLocation.Writer.WriteComments(
									//		$"Create block of objectives for field '{member.ReturnTypeIdentifier} {member.Identifier ?? "[anonymous]"}' for storage in type '{Identifier}'.");
									//	for(int i = 0; i < block.Length; i++)
									//		block[i] = Objective.AddObjective(typeLocation.Writer, null, "dummy");
									//	objectives.Add(field, block);
									//}
									//value.SaveToBlock(initLocation, MCSharpLinkerExtension.StorageSelector, objectives[field]);

								});
								break;
							}

							case MemberType.Property: {
								// TODO
								break;
							}

						}
					}

					InitInstance = (initLocation, identifier) => {

						var typeLocation = new Compiler.CompileArguments(initLocation.Compiler, initLocation.Function, Scope, initLocation.Predefined);
						foreach(var action in actions)
							action.Invoke(initLocation, typeLocation);

						initLocation.Writer.WriteComments(
							$"Create pointer to '{Identifier} {identifier ?? "[anonymous]"}'.");
						PrimitiveInstance.IntegerInstance pointer = new PrimitiveInstance.IntegerInstance(
							type: initLocation.Compiler.DefinedTypes[MCSharpLinkerExtension.IntIdentifier],
							identifier: null,
							objective: Objective.AddObjective(initLocation.Writer, null, "dummy")
						);
						return new ClassInstance(initLocation, this, identifier, pointer, objectives);

					};

					break;

				}

				case ClassType.Struct: {

					InitInstance = (location, identifier) => {
						return new StructInstance(location, this, identifier);
					};

					break;

				}

				case ClassType.Primitive: {

					InitInstance = (location, identifier) => {
						throw new InvalidOperationException($"Script-defined types cannot be primitive ({nameof(ClassType)} is {ClassType.Primitive}).");
					};

					break;

				}

				default:
					throw new InvalidOperationException($"Unknown {nameof(Linkage.ClassType)} '{ClassType}'.");

			}

			// TODO
			SubTypes = new List<ScriptType>();
			Conversions = new Dictionary<IType, IConversion>();
			Operations = new HashSetDictionary<Operation, IOperation>();

		}


		/// <inheritdoc/>
		public IInstance InitializeInstance(Compiler.CompileArguments location, string identifier) {
			return InitInstance(location, identifier);
		}

		/// <inheritdoc/>
		public void Dispose() {
			foreach(ScriptMember member in Members) member.Dispose();
		}
	}

}
