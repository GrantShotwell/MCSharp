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
		public string Identifier { get; }


		/// <summary>
		/// <see cref="Exception"/> thrown when <see cref="Type"/> is assigned an invalid value.
		/// </summary>
		public class InvalidTypeException : Exception {
			public InvalidTypeException(IType given, string expected) : base($"Cannot assign '{given.Identifier}' to {nameof(Type)} when '{expected}' was expected.") { }
		}


		/// <summary>
		/// Assigns the value of this <see cref="IInstance"/> into a new <see cref="IInstance"/>.
		/// If possible, not of the <see cref="IConstantInstance"/> interface.
		/// </summary>
		/// <param name="compile"></param>
		/// <returns>Returns a new <see cref="IInstance"/>.</returns>
		/// <param name="identifier">The <see cref="Identifier"/> of the new <see cref="IInstance"/>.</param>
		public IInstance Copy(Compiler.CompileArguments compile, string identifier);

	}

}
