using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Extensions {

	public class MCSharpLinkerExtension : LinkerExtension {


		public MCSharpLinkerExtension(Compiler compiler) : base(compiler) { }

		public override void CreatePredefinedTypes() {
			//todo
		}
	}

}
