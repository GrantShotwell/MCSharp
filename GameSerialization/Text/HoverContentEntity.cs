using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.GameSerialization.Text {

	public class HoverContentEntity {

		/// <summary>Optional. Hidden if not present. A raw JSON text that is displayed as the name of the entity.</summary>
		public RawText Name { get; set; }
		/// <summary>A string containing the type of the entity. Should be a namespaced entity ID. Present <c>minecraft:pig</c> if invalid.</summary>
		public string Type { get; set; }
		/// <summary>A string containing the UUID of the entity in the hyphenated hexadecimal format. Should be a valid UUID.</summary>
		public string Id { get; set; }

	}

}
