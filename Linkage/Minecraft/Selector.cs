using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Minecraft {

	public class Selector {

		private string TargetSelectorString { get; }


		/// <summary>
		/// Creates a new <see cref="Selector"/> from a raw string value.
		/// </summary>
		/// <param name="value">The selector in string form.</param>
		public Selector(string value) {
			TargetSelectorString = value;
		}


		public override string ToString() {
			return TargetSelectorString;
		}

	}

}
