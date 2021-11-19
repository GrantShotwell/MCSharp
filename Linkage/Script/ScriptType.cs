using Antlr4.Runtime.Tree;
using MCSharp.Collections;
using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Extensions;
using MCSharp.Linkage.Minecraft;
using MCSharp.Linkage.Predefined;
using System;
using System.Collections.Generic;

namespace MCSharp.Linkage.Script;

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
	/// <summary>The count of constructors.</summary>
	public int i_constructor = 0;

	/// <inheritdoc cref="IType.SubTypes"/>
	public IReadOnlyCollection<ScriptType> SubTypes { get; }
	/// <inheritdoc/>
	IReadOnlyCollection<IType> IType.SubTypes => SubTypes;

	/// <inheritdoc/>
	public IHashSetDictionary<Operation, IOperation> Operations { get; }

	/// <inheritdoc/>
	public IDictionary<IType, IConversion> Conversions { get; }

	/// <inheritdoc/>
	public IReadOnlyDictionary<IField, IInstance> StaticFieldInstances { get; }

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
	ref Compiler.PostFirstPassDelegate postFirstPass, ref Compiler.OnLoadDelegate onLoad) {

		Scope = scope;
		Scope.Holder = this;

		Modifiers = EnumLinker.LinkModifiers(typeContext.modifier());
		ClassType = EnumLinker.LinkClassType(typeContext.class_type());
		Identifier = typeContext.NAME();

		// Get base types.
		List<IType> baseTypes = new List<IType>();
		// If the type is a class, add the object type.
		if(ClassType == ClassType.Class) {
			// We need to wait until the second pass, so all types are defined.
			postFirstPass += (compiler) => { baseTypes.Add(compiler.DefinedTypes[MCSharpLinkerExtension.ObjectIdentifier]); };
		}
		// TODO: Add base types.
		BaseTypes = baseTypes;

		// Update derived types.
		foreach(IType baseType in BaseTypes) {
			baseType.DerivedTypes.Add(this);
		}

		// Get members (excluding constructors).
		ScriptMember[] members = new ScriptMember[memberContexts.Length];
		Dictionary<IField, IInstance> staticFieldInstances = new Dictionary<IField, IInstance>();
		StaticFieldInstances = staticFieldInstances;
		for(int i = 0; i < memberContexts.Length; i++) {

			// Create the member.
			var context = memberContexts[i];
			var memberScope = new Scope(context.NAME()[1].GetText(), Scope);
			var member = members[i] = new ScriptMember(memberScope, this, context, settings, virtualMachine, ref onLoad);

			// If the member is a static field, add it to the static field instances and initialize it on load.
			if(member.Modifiers.HasFlag(Modifier.Static) && member.Definition is IField field) {
				onLoad += (location) => {
					ResultInfo result = Compiler.CompileExpression(location, field.Initializer.Context, out IInstance value);
					staticFieldInstances.Add(field, value.Copy(location, member.Identifier.GetText()));
					return result;
				};
			}

		}
		Members = members;

		// Get constructors.
		ScriptConstructor[] constructors = new ScriptConstructor[constructorContexts.Length];
		for(int i = 0; i < constructorContexts.Length; i++) {
			var context = constructorContexts[i];
			var memberScope = new Scope(context.NAME().GetText(), Scope);
			constructors[i] = new ScriptConstructor(memberScope, this, context, settings, virtualMachine, ref onLoad);
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
							onLoad += (location) => {

								// Get the compiler from the location.
								var compiler = location.Compiler;

								// Get the field's type.
								var fieldType = compiler.DefinedTypes[member.TypeIdentifier];

								// Create the field's objectives array by using IType.GetBlockSize(compiler) to get the size of the array.
								var objectivesArray = new Objective[fieldType.GetBlockSize(compiler)];

								// Add the objectives to the field's objectives array.
								location.Writer.WriteComments(
									$"Create objectives for field '{fieldType.Identifier} {member.Identifier}' for class type '{Identifier}'.");
								for(int i = 0; i < objectivesArray.Length; i++) {
									objectivesArray[i] = Objective.AddObjective(location.Writer, null, "dummy");
								}

								// Add the objectives to the dictionary.
								objectives.Add(field, objectivesArray);

								return ResultInfo.DefaultSuccess;

							};

							// Initialize the field during initialization.
							initInstanceActions.Add((typeLocation, initLocation) => {
								if(field.Initializer != null)
									Compiler.CompileExpression(typeLocation, field.Initializer.Context, out IInstance value);
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

		// Get casts.
		var casts = new Dictionary<IType, IConversion>();
		onLoad += (location) => {

			// Make implicit casts to all base types.
			foreach(IType baseType in this.EnumerateBaseTypeTree()) {

				// Get cast types.
				IType reference = this;
				IType target = baseType;

				// Create cast function.
				CustomFunction function = new CustomFunction(target.Identifier,
					Array.Empty<PredefinedGenericParameter>(),
					new PredefinedMethodParameter[] {
							new PredefinedMethodParameter(reference.Identifier, "value")
					},
					(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) => {

							// Get arguments.
							ClassInstance value = arguments[0] as ClassInstance;

						if(ClassType == ClassType.Class) {

								// Class instances inherit from object instances, so can just return the value.
								result = value;

						} else if(ClassType == ClassType.Struct) {

							throw new NotImplementedException("Converting a struct instance to a base type has not been implemented.");

						} else {

								// Should never get here.
								throw new Exception();

						}

							// Return a success.
							return ResultInfo.DefaultSuccess;

					}
				);

				// Create and add cast.
				var cast = new PredefinedConversion(reference, target, function, @implicit: true);
				casts.Add(target, cast);

			}

			return ResultInfo.DefaultSuccess;

		};

		// TODO
		SubTypes = new List<ScriptType>();
		Conversions = casts;
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
