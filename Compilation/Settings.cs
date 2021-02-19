using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

	public struct Settings {

		public Datapack Datapack { get; }

		public Settings(Datapack datapack) {
			Datapack = datapack;
		}

	}

}
