using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation.Instancing {

	/// <summary>
	/// Represents an instance of a struct type.
	/// </summary>
	public class StructInstance : IInstance {

		/// <inheritdoc/>
		public IType Type { get; }

		/// <inheritdoc/>
		public ITerminalNode Identifier { get; }

		public IReadOnlyList<IInstance> FieldInstances { get; }

	}

}
