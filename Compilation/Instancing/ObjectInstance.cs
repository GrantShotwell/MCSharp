using MCSharp.Linkage;
using MCSharp.Linkage.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation.Instancing {

	public class ObjectInstance : IInstance {

		/// <inheritdoc/>
		public IType Type { get; }

		/// <inheritdoc/>
		public string Identifier { get; }

		/// <summary>
		/// The number ID used to access this <see cref="ObjectInstance"/> by score selection.
		/// </summary>
		public PrimitiveInstance.IntegerInstance ObjectId { get; }


		public ObjectInstance(Compiler.CompileArguments location, IType type, string identifier, PrimitiveInstance.IntegerInstance objectId) {

			Type = type ?? throw new ArgumentNullException(nameof(type));
			Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
			ObjectId = objectId ?? throw new ArgumentNullException(nameof(objectId));

			location.Scope.AddInstance(this);

		}


		/// <inheritdoc/>
		public IInstance Copy(Compiler.CompileArguments compile, string identifier) {
			throw new NotImplementedException();
		}

	}

}
