using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public interface IProperty : IMemberDefinition {

		public Function Getter { get; }
		public Function Setter { get; }

	}

}
