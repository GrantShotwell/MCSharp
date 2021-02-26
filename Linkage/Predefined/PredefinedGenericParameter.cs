using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Predefined {

	class PredefinedGenericParameter : IGenericParameter {

		public string TypeIdentifier { get; }

		public PredefinedGenericParameter(string typeIdentifier) {
			TypeIdentifier = typeIdentifier ?? throw new ArgumentNullException(nameof(typeIdentifier));
		}

	}

}
