using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

    public struct Word : IReadOnlyList<Character> {

        private readonly Character[] characters;
        private readonly string str;

        public Character this[int index] => characters[index];
        public int Length => characters.Length;
        public int FileLine => characters[0].FileLine;

        int IReadOnlyCollection<Character>.Count => Length;

        public Word(string word) : this(GetCharacters(word)) { }

        public Word(Character[] characters) {

            this.characters = new Character[characters.Length];
            characters.CopyTo(this.characters, 0);

            int length = characters.Length;
            char[] array = new char[length];
            for(int i = 0; i < length; i++) array[i] = characters[i];
            str = new string(array);

        }

        private static Character[] GetCharacters(string word) {
            if(word is null) return new Character[] { };
            var characters = new List<Character>();

            for(int i = 0; i < word.Length; i++) {
                var chr = new Character(0, word[i]);
                if(char.IsWhiteSpace(chr)) throw new ArgumentException("Given argument is not a cohesive word.", nameof(word));
                characters.Add(chr);
            }

            return characters.ToArray();
        }

        public IEnumerator<Character> GetEnumerator() => ((IEnumerable<Character>)characters).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Character>)characters).GetEnumerator();

        public static implicit operator string(Word word) => word.str;

        public override string ToString() => $"Line {FileLine}: \"{str}\"";

    }

}
