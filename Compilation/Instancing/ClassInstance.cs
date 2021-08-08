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
		public string Identifier { get; }

		public PrimitiveInstance.IntegerInstance.Constant ObjectId { get; }


		public ClassInstance(IType type, string identifier, PrimitiveInstance.IntegerInstance.Constant objectId) {

			#region Argument Checks
			if(type.ClassType != ClassType.Class)
				throw new IInstance.InvalidTypeException(type, "any class");
			#endregion

			Type = type ?? throw new ArgumentNullException(nameof(type));
			Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
			ObjectId = objectId ?? throw new ArgumentNullException(nameof(objectId));

		}


		/// <inheritdoc/>
		public IInstance Copy(Compiler.CompileArguments location, string identifier) {
			throw new NotImplementedException();
		}

	}

}
