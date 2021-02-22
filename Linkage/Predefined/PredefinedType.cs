using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Antlr4.Runtime.Tree;

namespace MCSharp.Linkage.Predefined {

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
		/// <summary>
		/// The members defined by this type definition.
		/// </summary>
		public PredefinedMember[] Members { get; }
		/// <inheritdoc/>
		IMember[] IType.Members => Members;
		/// <summary>
		/// The type definitions defined within this type definition.
		/// </summary>
		public PredefinedType[] SubTypes { get; }
		/// <inheritdoc/>
		IType[] IType.SubTypes => SubTypes;

		/// <summary>
		/// Creates a new predefined type definition.
		/// </summary>
		/// <param name="modifiers">The modifiers that affect this type definition.</param>
		/// <param name="classType">Whether this type is a class or a struct.</param>
		/// <param name="identifier">The local identifier for this type definition.</param>
		/// <param name="members">The members defined by this type definition.</param>
		/// <param name="subTypes">The type definitions defined by this type definition.</param>
		public PredefinedType(Modifier modifiers, ClassType classType, string identifier, PredefinedMember[] members, PredefinedType[] subTypes) {
			Modifiers = modifiers;
			ClassType = classType;
			Identifier = identifier;
			Members = members;
			SubTypes = subTypes;
		}

	}

}
