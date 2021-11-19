using System.Collections.Generic;
using MCSharp.Compilation.Instancing;
using MCSharp.Compilation;
using MCSharp.Collections;

namespace MCSharp.Linkage.Predefined;

/// <summary>
/// Represents a predefined type.
/// </summary>
public class PredefinedType : IType {

	/// <inheritdoc/>
	public Modifier Modifiers { get; }

	/// <inheritdoc/>
	public ClassType ClassType { get; }

	/// <inheritdoc/>
	public string Identifier { get; }

	/// <inheritdoc/>
	public IReadOnlyCollection<IType> BaseTypes { get; }

	/// <inheritdoc/>
	public ICollection<IType> DerivedTypes { get; } = new List<IType>();

	/// <inheritdoc/>
	public Scope Scope { get; }

	/// <inheritdoc cref="IType.Members"/>
	public IReadOnlyCollection<PredefinedMember> Members { get; }
	/// <inheritdoc/>
	IReadOnlyCollection<IMember> IType.Members => Members;

	/// <inheritdoc cref="IType.Constructors"/>
	public IReadOnlyCollection<PredefinedConstructor> Constructors { get; }
	/// <inheritdoc/>
	IReadOnlyCollection<IConstructor> IType.Constructors => Constructors;

	/// <inheritdoc cref="IType.SubTypes"/>
	public IReadOnlyCollection<PredefinedType> SubTypes { get; }
	/// <inheritdoc/>
	IReadOnlyCollection<IType> IType.SubTypes => SubTypes;

	private IType.InitializeInstanceDelegate Init { get; }

	/// <inheritdoc/>
	public IHashSetDictionary<Operation, IOperation> Operations { get; }

	/// <inheritdoc/>
	public IDictionary<IType, IConversion> Conversions { get; }

	/// <inheritdoc/>
	public IReadOnlyDictionary<IField, IInstance> StaticFieldInstances { get; }


	/// <summary>
	/// Creates a new predefined type definition.
	/// </summary>
	/// <param name="modifiers">The modifiers that affect this type definition.</param>
	/// <param name="classType">Whether this type is a class or a struct.</param>
	/// <param name="identifier">The local identifier for this type definition.</param>
	/// <param name="members">The members defined by this type definition.</param>
	/// <param name="subTypes">The type definitions defined by this type definition.</param>
	public PredefinedType(Scope scope, Modifier modifiers, ClassType classType, string identifier, PredefinedMember[] members,
	PredefinedConstructor[] constructors, PredefinedType[] subTypes, IType.InitializeInstanceDelegate init,
	IHashSetDictionary<Operation, IOperation> operations, IDictionary<IType, IConversion> conversions) {

		Scope = scope;
		Scope.Holder = this;

		// TODO
		BaseTypes = new List<IType>();

		Modifiers = modifiers;
		ClassType = classType;
		Identifier = identifier;
		Members = members;
		Constructors = constructors;
		SubTypes = subTypes;
		Init = init;
		Operations = operations;
		Conversions = conversions;

	}


	/// <inheritdoc/>
	public IInstance InitializeInstance(Compiler.CompileArguments location, string identifier) {

		IInstance result = Init(location, identifier);
		location.Scope.AddInstance(result);
		return result;

	}

	/// <inheritdoc/>
	public void Dispose() {
		foreach(PredefinedMember member in Members) member.Dispose();
	}

}
