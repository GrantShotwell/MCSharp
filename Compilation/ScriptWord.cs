using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

    public struct ScriptWord : IReadOnlyList<ScriptChar> {

        private readonly ScriptChar[] characters;
        private readonly string str;

        public ScriptChar this[int index] => characters[index];
        public int Length => characters.Length;
        public int FileLine => characters[0].FileLine;

        int IReadOnlyCollection<ScriptChar>.Count => Length;

        public ScriptWord(string word, bool ignoreCohesion = false) : this(GetCharacters(word, ignoreCohesion)) { }

        public ScriptWord(ScriptChar[] characters) {

            this.characters = new ScriptChar[characters.Length];
            characters.CopyTo(this.characters, 0);

            int length = characters.Length;
            char[] array = new char[length];
            for(int i = 0; i < length; i++) array[i] = characters[i];
            str = new string(array);

        }

        private static ScriptChar[] GetCharacters(string word, bool ignoreCohesion) {
            if(word is null) return new ScriptChar[] { };
            var characters = new List<ScriptChar>();

            for(int i = 0; i < word.Length; i++) {
                var chr = new ScriptChar(0, word[i]);
                if(!ignoreCohesion && char.IsWhiteSpace(chr))
                    throw new ArgumentException("Given argument is not a cohesive word.", nameof(word));
                characters.Add(chr);
            }

            return characters.ToArray();
        }

        public IEnumerator<ScriptChar> GetEnumerator() => ((IEnumerable<ScriptChar>)characters).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<ScriptChar>)characters).GetEnumerator();

        public static implicit operator string(ScriptWord word) => word.str;
        public static implicit operator ScriptWord(string str) => new ScriptWord(str);

        public override string ToString() => $"Line {FileLine}: \"{str}\"";

    }

}
