using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	/// <summary>
	/// Represents a member, except constructors, for some type.
	/// </summary>
	public interface IMember {

		/// <summary>
		/// The type that has defined this member.
		/// </summary>
		public IType Declarer { get; }
		/// <summary>
		/// The modifiers that affect this member.
		/// </summary>
		public Modifier Modifiers { get; }
		/// <summary>
		/// The local identifier that represents the return type.
		/// </summary>
		public string ReturnTypeIdentifier { get; }
		/// <summary>
		/// The local identifier the represents this member.
		/// </summary>
		public string Identifier { get; }
		/// <summary>
		/// Specifies if the <see cref="Definition"/> is a <see cref="IField"/>, <see cref="IProperty"/>, or <see cref="IMethod"/>.
		/// </summary>
		public MemberType MemberType { get; }
		/// <summary>
		/// The <see cref="IMemberDefinition"/> of this <see cref="IMember"/>.
		/// </summary>
		public IMemberDefinition Definition { get; }

	}

}
