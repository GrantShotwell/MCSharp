using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

    public struct Character {

        private readonly char chr;

        public int FileLine { get; }

        public Character(int line, char character) {
            FileLine = line;
            chr = character;
        }

        public static implicit operator char(Character character) => character.chr;

        public override string ToString() => $"Line {FileLine}: '{chr.ToString()}'";

    }

}
