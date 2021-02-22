using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public interface IConstructor {

		public IType Declarer { get; }
		public Modifier Modifiers { get; }
		public Function Invoker { get; }

	}

}
