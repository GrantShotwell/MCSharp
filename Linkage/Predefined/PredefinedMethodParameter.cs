using Antlr4.Runtime.Tree;
using MCSharp.Compilation.Instancing;
using System;

namespace MCSharp.Linkage.Predefined {

	public class PredefinedMethodParameter : IMethodParameter {

		/// <inheritdoc/>
		public string TypeIdentifier { get; }

		/// <inheritdoc/>
		public string Identifier { get; }

		/// <inheritdoc/>
		public IInstance Instance { get; set; }


		public PredefinedMethodParameter(string typeIdentifier, string identifier) {
			TypeIdentifier = typeIdentifier ?? throw new ArgumentNullException(nameof(typeIdentifier));
			Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
		}

	}

}
