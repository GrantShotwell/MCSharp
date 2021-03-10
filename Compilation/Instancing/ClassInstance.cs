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

		/// <summary>
		/// The local identifier of this instance.
		/// </summary>
		public ITerminalNode Identifier { get; }
		/// <inheritdoc/>
		string IInstance.Identifier => Identifier.GetText();

		public PrimitiveInstance.IntegerInstance.Constant ObjectId { get; }


		public ClassInstance(IType type, ITerminalNode identifier, PrimitiveInstance.IntegerInstance.Constant objectId) {

			if(type.ClassType != ClassType.Class)
				throw new IInstance.InvalidTypeException(type, "any class");
			Type = type ?? throw new ArgumentNullException(nameof(type));

			Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));

			ObjectId = objectId ?? throw new ArgumentNullException(nameof(objectId));

		}

	}

}
