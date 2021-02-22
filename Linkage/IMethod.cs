using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public interface IMethod : IMemberDefinition {

		public Function Invoker { get; }

	}

}
