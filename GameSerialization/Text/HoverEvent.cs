using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.GameSerialization.Text {

	public class HoverEvent {

		/// <summary>The type of tooltip to show.</summary>
		public string Action { get; set; }
		/// <summary>The formatting and type of this tag varies depending on the action. Deprecated, use <see cref="Contents"/> instead.</summary>
		public string Value { get; set; }
		/// <summary>The formatting of this tag varies depending on the action.</summary>
		public HoverContents Contents { get; set; }

	}

}
