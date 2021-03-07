using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using System;

namespace MCSharp.Compilation.Instancing {

	/// <summary>
	/// Represents an instance of some type.
	/// </summary>
	public interface IInstance {

		/// <summary>
		/// The <see cref="IType"/> that defines this instance.
		/// </summary>
		public IType Type { get; }

		/// <summary>
		/// The local identifier for this instance.
		/// </summary>
		public ITerminalNode Identifier { get; }


		public class InvalidTypeException : Exception {
			public InvalidTypeException(IType given, string expected) : base($"Cannot assign '{given.Identifier}' to {nameof(Type)} when '{expected}' was expected.") { }
		}

	}

}
