using MCSharp.Statements;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

    public struct ScriptFunction : IReadOnlyList<ScriptLine> {

        private readonly string str;

        private ScriptLine[] ScriptLines { get; }
        public ScriptLine this[int index] => ScriptLines[index];
        public int Length => ScriptLines.Length;
        public string FilePath { get; }
        public string FileName { get; }
        public string GamePath { get; }
        public string Alias { get; }
        public string AliasDotted => Alias.Replace('\\', '.');
        public ScriptTrace ScriptTrace => ScriptLines[0].ScriptTrace;

        int IReadOnlyCollection<ScriptLine>.Count => ((IReadOnlyList<ScriptLine>)ScriptLines).Count;


        public ScriptFunction(string alias, ScriptString script, bool fixAlias = true) : this(alias, GetLines(script), fixAlias) { }
        public ScriptFunction(string alias, ScriptWild wild, bool fixAlias = true) : this(alias, GetLines(wild), fixAlias) { }
        public ScriptFunction(string alias, ScriptLine[] phrases, bool fixAlias = true) {

            FileName = fixAlias ? Script.FixAlias($"{alias}.mcfunction") : alias;
            FilePath = $"{Program.FunctionsFolder}\\{FileName}";
            Alias = fixAlias ? alias = Script.FixAlias(alias) : alias;
            GamePath = $"{Program.Datapack.Name}:{alias.Replace('\\', '/')}";
            this.ScriptLines = phrases;

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
                for(int i = 0; i < wild.Wilds.Count; i++) array[i] = new ScriptLine(wild.Wilds[i]);
                return array;
            } else throw new Exception();
        }

        public static ScriptLine[] GetLines(ScriptString function) {
            var lines = new List<ScriptLine>();

            bool inString = false;
            int start = 0;
            var stack = new Stack<string>();
            for(int end = 0; end < function.Length; end++) {
                bool inBlock = stack.Count > 0;
                char chr = (char)function[end];

                if(chr == ';' && !inBlock && !inString) {
                    lines.Add(new ScriptLine(function[start..end]));
                    start = end + 1;
                } else {
                    //Check if starting/ending a string.
                    if(chr == '"' && (end == 0 || (char)function[end-1] != '\\')) {
                        inString = !inString;
                        if(inString) {
                            stack.Push("\"\\\"");
                        } else {
                            stack.Pop();
                        }
                    } else {
                        if(ScriptLine.IsBlockCharStart(chr, out string block)) {
                            string s = (string)function[start..end];
                            if(Statement.Dictionary.TryGetValue(s.Trim(), out Tuple<Statement.Reader, Statement.Writer> statement)) {
                                //Read a statement.
                                statement.Item1.Invoke(ref lines, ref start, ref end, ref function);
                                continue;
                            } else {
                                //Start a block.
                                stack.Push(block);
                                continue;
                            }
                        }
                        if(ScriptLine.IsBlockCharEnd(chr, out block)) {
                            //Check for mistakes.
                            if(!inBlock) throw new Compiler.SyntaxException("Got too many end-of-block characters without enough start-of-block characters to match!", function.ScriptTrace);
                            if(stack.Peek() != block) throw new Compiler.SyntaxException("Expected a different end-of-block character.", function.ScriptTrace);
                            //If not a mistake, then end the block.
                            stack.Pop();
                            continue;
                        }
                    }
                }

            }

            return lines.Count > 0 ? lines.ToArray()
                : (new ScriptLine[] { new ScriptLine(new ScriptWild[] { new ScriptWild(new ScriptWord(new ScriptString(
                    "", function.ScriptTrace.FilePath, function.ScriptTrace.FileLine))) }) });
        }

        public IEnumerator<ScriptLine> GetEnumerator() => ((IReadOnlyList<ScriptLine>)ScriptLines).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<ScriptLine>)ScriptLines).GetEnumerator();

        public override string ToString() => $"{ScriptTrace}: {str}";

    }

}
