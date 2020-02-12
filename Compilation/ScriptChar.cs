using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

    public struct ScriptChar {

        private readonly char chr;

        public int FileLine { get; }

        public ScriptChar(int line, char character) {
            FileLine = line;
            chr = character;
        }

        public static implicit operator char(ScriptChar character) => character.chr;

        public override string ToString() => $"Line {FileLine}: '{chr.ToString()}'";

    }

}
