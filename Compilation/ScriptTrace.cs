using System.Diagnostics;

namespace MCSharp.Compilation {

	[DebuggerDisplay("{ToString(),nq}")]
	public class ScriptTrace {

		public string FilePath { get; }
		public string FileName { get; }
		public int FileLine { get; }

		public ScriptTrace(string path, int line) {
			FilePath = path;
			FileName = path[Program.ScriptsFolder.Length..];
			FileLine = line;
		}

		public override string ToString() => $"'{FileName}', line {FileLine}";

	}

}
