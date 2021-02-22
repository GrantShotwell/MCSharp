using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	/// <summary>
	/// Represents a type definition.
	/// </summary>
	public interface IType : IDisposable {

		/// <summary>
		/// The modifiers that affect this type definition.
		/// </summary>
		public Modifier Modifiers { get; }
		/// <summary>
		/// Whether this type definition is for a class or a struct.
		/// </summary>
		public ClassType ClassType { get; }
		/// <summary>
		/// The local identifier for this type definition.
		/// </summary>
		public string Identifier { get; }
		/// <summary>
		/// The members defined by this type definition.
		/// </summary>
		public IMember[] Members { get; }
		/// <summary>
		/// Type definitions defined within this type definition.
		/// </summary>
		public IType[] SubTypes { get; }

	}

}
