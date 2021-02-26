using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation.Instancing {

	/// <summary>
	/// Represents an instance of a class type.
	/// </summary>
	public class ClassInstance : IInstance {

		/// <inheritdoc/>
		public IType Type { get; }

		/// <inheritdoc/>
		public ITerminalNode Identifier { get; }

		public PrimitiveInstance.IntegerInstance ObjectId { get; }

	}

}
