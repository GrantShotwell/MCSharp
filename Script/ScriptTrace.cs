using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Script {

	public class ScriptTrace {

		public ScriptFile File { get; }
		public int Line { get; }


		public ScriptTrace(ScriptFile file, int line) {
			File = file ?? throw new ArgumentNullException(nameof(file));
			Line = line;
		}


		public override string ToString() => $"{File}, line {Line}";

	}

}
