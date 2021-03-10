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

		/// <summary>
		/// The local identifier for this instance.
		/// </summary>
		public ITerminalNode Identifier { get; }
		/// <inheritdoc/>
		string IInstance.Identifier => Identifier.GetText();


		public StructInstance(IType type, ITerminalNode identifier, Scope parent) {

			if(type.ClassType != ClassType.Struct)
				throw new IInstance.InvalidTypeException(type, "any struct");
			Type = type;

			Identifier = identifier;

		}

	}

}
