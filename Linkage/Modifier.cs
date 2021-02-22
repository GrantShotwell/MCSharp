using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public enum Modifier {
		Public = 0b0000001,
		Private = 0b0000010,
		Protected = 0b0000100,
		Static = 0b0001000,
		Abstract = 0b0010000,
		Virtual = 0b0100000,
		Override = 0b1000000
	}

}
