using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public interface IOperation {

		public Operation Operation { get; }

		public IFunction Function { get; }

	}

}
