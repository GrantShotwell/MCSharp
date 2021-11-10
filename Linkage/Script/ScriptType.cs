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
		public Scope Scope { get; }

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
		public ScriptType(TypeDefinitionContext typeContext, MemberDefinitionContext[] memberContexts, ConstructorDefinitionContext[] constructorContexts, Settings settings, VirtualMachine virtualMachine, Scope scope,
		out Action<Compiler> postFirstPass, out Action<Compiler.CompileArguments> onLoad) {

			Scope = scope;
			Scope.Holder = this;

			Modifiers = EnumLinker.LinkModifiers(typeContext.modifier());
			ClassType = EnumLinker.LinkClassType(typeContext.class_type());
			Identifier = typeContext.NAME();

			// Create a list of actions for post-first-pass.
			var postFirstPassActions = new List<Action<Compiler>>();
			// postFirstPass will trigger all of these actions.
			postFirstPass = (location) => { foreach (var action in postFirstPassActions) action(location); };

			// Create a list of actions for on-load.
			var onLoadActions = new List<Action<Compiler.CompileArguments>>();
			// onLoad will trigger all of these actions.
			onLoad = (location) => { foreach (var action in onLoadActions) action(location); };

			// Get base types.
			List<IType> baseTypes = new List<IType>();
			// If the type is a class, add the object type.
			if(ClassType == ClassType.Class) {
				// We need to wait until the second pass, so all types are defined.
				postFirstPassActions.Add((compiler) => { baseTypes.Add(compiler.DefinedTypes[MCSharpLinkerExtension.ObjectIdentifier]); });
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
				var context = memberContexts[i];
				var memberScope = new Scope(context.NAME()[1].GetText(), Scope);
				members[i] = new ScriptMember(memberScope, this, context, settings, virtualMachine);
			}
			Members = members;

			// Get constructors.
			ScriptConstructor[] constructors = new ScriptConstructor[constructorContexts.Length];
			for(int i = 0; i < constructorContexts.Length; i++) {
				var context = constructorContexts[i];
				var memberScope = new Scope(context.NAME().GetText(), Scope);
				constructors[i] = new ScriptConstructor(memberScope, this, context, settings, virtualMachine);
			}
			Constructors = constructors;

			// Set 'InitializeInstance' value.
			switch(ClassType) {

				case ClassType.Class: {

					// Use closure to have a field dictionary per type.
					var objectives = new Dictionary<IField, Objective[]>();
					// Use closure to run needed actions during compilation.
					var initInstanceActions = new List<Action<Compiler.CompileArguments, Compiler.CompileArguments>>(Members.Count);
				
					foreach(IMember member in Members) {
						switch(member.MemberType) {

							case MemberType.Field: {

								// Get the field.
								var field = member.Definition as IField;

								// Create the objectives for the field on load.
								onLoadActions.Add((location) => {

									// Get the compiler from the location.
									var compiler = location.Compiler;

									// Get the field's type.
									var fieldType = compiler.DefinedTypes[member.TypeIdentifier];

									// Create the field's objectives array by using IType.GetBlockSize(compiler) to get the size of the array.
									var objectivesArray = new Objective[fieldType.GetBlockSize(compiler)];

									// Add the objectives to the field's objectives array.
									location.Writer.WriteComments(
										$"Create objectives for field '{fieldType.Identifier} {member.Identifier}' for class type '{this.Identifier}'.");
									for(int i = 0; i < objectivesArray.Length; i++) {
										objectivesArray[i] = Objective.AddObjective(location.Writer, null, "dummy");
									}

									// Add the objectives to the dictionary.
									objectives.Add(field, objectivesArray);

								});

								// Initialize the field during initialization.
								initInstanceActions.Add((typeLocation, initLocation) => {
									typeLocation.Compiler.CompileExpression(typeLocation, field.Initializer.Context, out IInstance value);
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
						foreach(var action in initInstanceActions)
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
			
			// Dispose of every member.
			foreach(IMember member in Members)
				member.Dispose();

			// Dispose of every constructor.
			foreach(ScriptConstructor constructor in Constructors)
				constructor.Dispose();
			
		}
	}

}
