using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

    public struct ScriptFunction : IReadOnlyList<ScriptLine> {

        private readonly ScriptLine[] phrases;
        private readonly string str;

        public ScriptLine this[int index] => phrases[index];
        public int Length => phrases.Length;
        public string FilePath { get; }
        public string FileName { get; }
        public string Alias { get; }

        int IReadOnlyCollection<ScriptLine>.Count => ((IReadOnlyList<ScriptLine>)phrases).Count;


        public ScriptFunction(string file, string alias, string script) : this(file, alias, GetLines(script)) { }

        public ScriptFunction(string file, string alias, ScriptLine[] phrases) {

            FilePath = file;
            FileName = file.Split('\\')[^1];
            Alias = alias;
            this.phrases = phrases;

            int length = phrases.Length;
            string[] array = new string[length];
            for(int i = 0; i < length; i++) array[i] = phrases[i];
            string str = "";
            foreach(string s in array) str += " " + s;
            this.str = str.Length > 0 ? str[1..] : "";

        }

        public static ScriptLine[] GetLines(string scriptFunction) {
            if(scriptFunction is null) return new ScriptLine[] { };
            var lines = new List<ScriptLine>();

            int start = 0;
            var stack = new Stack<string>();
            for(int end = 0; end < scriptFunction.Length; end++) {
                bool final = end == scriptFunction.Length - 1;
                bool inBlock = stack.Count > 0;
                char chr = scriptFunction[end];

                if(chr == ';' && !inBlock) {
                    lines.Add(new ScriptLine(scriptFunction[start..end]));
                    start = end + 1;
                } else {
                    if(ScriptLine.IsBlockCharStart(chr, out string block)) {
                        //Check for mistakes.

                        //If not a mistake, then start a block.
                        stack.Push(block);
                    }
                    if(ScriptLine.IsBlockCharEnd(chr, out block)) {
                        //Check for mistakes.
                        if(!inBlock) throw new Compiler.SyntaxException("Got too many end-of-block characters without enough start-of-block characters to match!");
                        if(stack.Peek() != block) throw new Compiler.SyntaxException("Expected a different end-of-block character.");
                        //If not a mistake, then end the block.
                        stack.Pop();
                    }
                }

            }

            return lines.ToArray();
        }

        public IEnumerator<ScriptLine> GetEnumerator() => ((IReadOnlyList<ScriptLine>)phrases).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<ScriptLine>)phrases).GetEnumerator();

        public override string ToString() => $"{str}";

    }

}
