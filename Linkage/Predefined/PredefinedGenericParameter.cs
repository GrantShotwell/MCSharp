using System;

namespace MCSharp.Linkage.Predefined;

public class PredefinedGenericParameter : IGenericParameter {

	public string TypeIdentifier { get; }

	public PredefinedGenericParameter(string typeIdentifier) {
		TypeIdentifier = typeIdentifier ?? throw new ArgumentNullException(nameof(typeIdentifier));
	}

}
