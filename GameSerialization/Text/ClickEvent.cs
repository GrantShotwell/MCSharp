using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.GameSerialization.Text {

	public class ClickEvent {

		/// <summary>The action to perform when clicked.</summary>
		public string Action { get; set; }
		/// <summary>The URL, file path, chat, command or book page used by the specified action.</summary>
		public string Value { get; set; }

	}

}
