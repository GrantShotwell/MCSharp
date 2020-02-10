using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

    public struct Script : IReadOnlyList<Phrase> {

        private readonly Phrase[] phrases;
        private readonly string str;

        public Phrase this[int index] => phrases[index];
        public int Length => phrases.Length;
        public string FilePath { get; }
        public string FileName { get; }

        int IReadOnlyCollection<Phrase>.Count => ((IReadOnlyList<Phrase>)phrases).Count;


        public Script(string filePath, string script) : this(filePath, GetPhrases(script)) { }

        public Script(string filePath, Phrase[] phrases) {

            FilePath = filePath;
            FileName = filePath.Substring(Program.ScriptsFolder.Length);

            this.phrases = phrases;

            int length = phrases.Length;
            string[] array = new string[length];
            for(int i = 0; i < length; i++) array[i] = phrases[i];
            string str = "";
            foreach(string s in array) str += " " + s;
            if(str.Length > 0) this.str = str[1..];
            else this.str = "";

        }


        public static Phrase[] GetPhrases(string script) {
            if(script is null) return new Phrase[] { };
            var phrases = new List<Phrase>();

            int start = 0, stack = 0;
            for(int end = 0; end < script.Length; end++) {
                char c = script[end];
                if(Phrase.IsBlockCharStart(c)) {
                    if(stack == 0) start = end;
                    stack++;
                } else if(Phrase.IsBlockCharEnd(c)) {
                    stack--;
                    if(stack < 0) throw new Compiler.SyntaxException("Missing a block character.");
                    if(stack == 0) {
                        phrases.Add(new Phrase(script[start..end]));
                        start = end + 1;
                    }
                } else if(c == ';') {
                    phrases.Add(new Phrase(script[start..end]));
                    start = end + 1;
                }
            }

            return phrases.ToArray();
        }

        public IEnumerator<Phrase> GetEnumerator() => ((IReadOnlyList<Phrase>)phrases).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<Phrase>)phrases).GetEnumerator();

        public override string ToString() => $"File {FileName}:\n\n{str}";

    }

}
