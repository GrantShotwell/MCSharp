using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Predefined {

	/// <summary>
	/// Represents a predefined member.
	/// </summary>
	public class PredefinedMember : IMember {

		/// <summary>
		/// The predefined type that has defined this member.
		/// </summary>
		public PredefinedType Declarer { get; }
		/// <inheritdoc/>
		IType IMember.Declarer => Declarer;
		/// <inheritdoc/>
		public Modifier Modifiers { get; }
		/// <inheritdoc/>
		public string ReturnTypeIdentifier { get; }
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
		public PredefinedMember(PredefinedType declarer, Modifier modifiers, string returnTypeIdentifier, string identifier, MemberType memberType, PredefinedMemberDefinition definition) {
			Declarer = declarer ?? throw new ArgumentNullException(nameof(declarer));
			Modifiers = modifiers;
			ReturnTypeIdentifier = returnTypeIdentifier ?? throw new ArgumentNullException(nameof(returnTypeIdentifier));
			Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
			MemberType = memberType;
			Definition = definition ?? throw new ArgumentNullException(nameof(definition));
		}

	}

}
