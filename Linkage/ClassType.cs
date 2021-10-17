using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	/// <summary>
	/// Represents the names for the different ways data is stored in objects.
	/// </summary>
	public enum ClassType {
		/// <summary>"class"</summary>
		Class,
		/// <summary>"struct"</summary>
		Struct,
		/// <summary>Only possible through MCSharp extensions.</summary>
		Primitive
	}

}
