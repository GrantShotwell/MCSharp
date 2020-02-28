using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

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
