using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.GameJSON.Text {

	public class HoverContentItem {

		/// <summary>The namespaced item ID. Present <c>minecraft:air</c> if invalid.</summary>
		public string Id { get; set; }
		/// <summary>Optional. Size of the item stack.</summary>
		public int Count { get; set; }
		/// <summary>Optional. A string containing the serialized NBT of the additional information about the item.</summary>
		public string Tag { get; set; }

	}

}
