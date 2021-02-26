using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Minecraft {

	public class String {

		public string Value { get; }

		public String(string value) {
			Value = value ?? throw new ArgumentNullException(nameof(value));
		}

	}

}
