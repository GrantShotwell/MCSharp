using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp {

	public struct Settings {
		
		public Datapack Datapack { get; }

		public Settings(Datapack datapack) {
			Datapack = datapack;
		}

	}

}
