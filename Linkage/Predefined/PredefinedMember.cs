using MCSharp.Compilation;
using System;

namespace MCSharp.Linkage.Predefined;

/// <summary>
/// Represents a predefined member.
/// </summary>
public class PredefinedMember : IMember {

	/// <summary>
	/// The predefined type that has defined this member.
	/// </summary>
	public PredefinedType Declarer { get; set; }

	/// <inheritdoc/>
	IType IMember.Declarer => Declarer;

	/// <inheritdoc/>
	public Scope Scope { get; }

	/// <inheritdoc/>
	public Modifier Modifiers { get; }

	/// <inheritdoc/>
	public string TypeIdentifier { get; }

	/// <inheritdoc/>
	public string Identifier { get; }

	/// <inheritdoc/>
	public MemberType MemberType { get; }

	/// <inheritdoc/>
	public IMemberDefinition Definition { get; }


	/// <summary>
	/// Creates a new predefined member definition.
	/// </summary>
	/// <param name="declarer">The predefined type that has defined this member definition.</param>
	/// <param name="modifiers">The modifiers that affect this member definition.</param>
	/// <param name="returnTypeIdentifier">The local identifier for the return type of this member definition.</param>
	/// <param name="identifier">The local identifier for this member definition.</param>
	/// <param name="memberType">Whether this member is a field, property, or method.</param>
	/// <param name="definition">The <see cref="IMemberDefinition"/>.</param>
	public PredefinedMember(Scope scope, PredefinedType declarer, Modifier modifiers, string returnTypeIdentifier, string identifier, MemberType memberType, PredefinedMemberDefinition definition) {

		Scope = scope;
		Scope.Holder = this;

		Declarer = declarer;
		Modifiers = modifiers;
		TypeIdentifier = returnTypeIdentifier ?? throw new ArgumentNullException(nameof(returnTypeIdentifier));
		Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
		MemberType = memberType;
		Definition = definition ?? throw new ArgumentNullException(nameof(definition));

	}

	public void Dispose() {
		Definition.Dispose();
	}

}
