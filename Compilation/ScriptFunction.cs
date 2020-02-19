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
        public string AliasDotted => Alias.Replace('\\', '.');

        int IReadOnlyCollection<ScriptLine>.Count => ((IReadOnlyList<ScriptLine>)phrases).Count;


        public ScriptFunction(string alias, string script, bool fixAlias = true) : this(alias, GetLines(script), fixAlias) { }

        public ScriptFunction(string alias, ScriptWild wild, bool fixAlias = true) : this(alias, GetLines(wild), fixAlias) { }

        public ScriptFunction(string alias, ScriptLine[] phrases, bool fixAlias = true) {

            FileName = fixAlias ? Script.FixAlias($"{alias}.mcfunction") : alias;
            FilePath = $"{Program.FunctionsFolder}\\{FileName}";
            Alias = fixAlias ? Script.FixAlias(alias) : alias;
            this.phrases = phrases;

            int length = phrases.Length;
            string[] array = new string[length];
            for(int i = 0; i < length; i++) array[i] = phrases[i];
            string str = "";
            foreach(string s in array) str += " " + s;
            this.str = str.Length > 0 ? str[1..] : "";

        }

        public static ScriptLine[] GetLines(ScriptWild wild) {
            if(wild.FullBlockType == "{\\;\\}") {
                ScriptLine[] array = new ScriptLine[wild.Wilds.Count];
                for(int i = 0; i < wild.Wilds.Count; i++)
                    array[i] = new ScriptLine(wild.Wilds[i]);
                return array;
            } else throw new System.Exception();
        }

        public static ScriptLine[] GetLines(string scriptFunction) {
            if(scriptFunction is null) return new ScriptLine[] { };
            var lines = new List<ScriptLine>();

            bool inString = false;
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
                    if(chr == '"' && (end == 0 || scriptFunction[end-1] != '\\')) {
                        inString = !inString;
                        if(inString) {
                            stack.Push("\"\\\"");
                        } else {
                            stack.Pop();
                        }
                    } else {
                        if(ScriptLine.IsBlockCharStart(chr, out string block)) {
                            //Start a block.
                            stack.Push(block);
                            continue;
                        }
                        if(ScriptLine.IsBlockCharEnd(chr, out block)) {
                            //Check for mistakes.
                            if(!inBlock) throw new Compiler.SyntaxException("Got too many end-of-block characters without enough start-of-block characters to match!");
                            if(stack.Peek() != block) throw new Compiler.SyntaxException("Expected a different end-of-block character.");
                            //If not a mistake, then end the block.
                            stack.Pop();
                            continue;
                        }
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
