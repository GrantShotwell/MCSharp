using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Predefined {

	public class PredefinedMethodParameter : IMethodParameter {

		public string TypeIdentifier { get; }

		public string Identifier { get; }

		public PredefinedMethodParameter(string typeIdentifier, string identifier) {
			TypeIdentifier = typeIdentifier ?? throw new ArgumentNullException(nameof(typeIdentifier));
			Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
		}

	}

}
