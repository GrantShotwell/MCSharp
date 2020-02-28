using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

    public struct ScriptFile : IReadOnlyCollection<ScriptClass> {


        public static Dictionary<string, ScriptFile> Files { get; } = new Dictionary<string, ScriptFile>();

        private ScriptClass[] ScriptClasses { get; }
        public ScriptClass this[int index] => ScriptClasses[index];
        public int Length => ScriptClasses.Length;
        int IReadOnlyCollection<ScriptClass>.Count => Length;
        public ScriptTrace ScriptTrace => ScriptClasses[0].ScriptTrace;


        public ScriptFile(ScriptString scriptFile) : this(GetClasses(scriptFile)) { }

        public ScriptFile(ScriptClass[] classes) {
            ScriptClasses = classes;
            Files.Add(ScriptTrace.FilePath, this);
        }


        public static ScriptClass[] GetClasses(ScriptString file) {
            var functions = new List<ScriptClass>();

            int start = 0, blocks = 0;
            string word = "", lastWord = "";
            string alias = "";

            void Rotate() {
                lastWord = word;
                word = "";
            }

            bool inString = false;
            for(int end = 0; end < file.Length; end++) {
                char current = (char)file[end];

                if(current == '"' && (char)file[end - 1] != '\\') {
                    inString = !inString;
                    continue;
                }

                if(char.IsWhiteSpace(current)) {
                    if(word != "" && blocks == 0) Rotate();
                } else if(current == '{') {
                    blocks++;
                    if(blocks == 1) {
                        start = end + 1;
                        alias = lastWord.Split('(', 2)[0];
                    }
                    Rotate();
                } else if(current == '}') {
                    blocks--;
                    if(blocks == 0) {
                        functions.Add(new ScriptClass(alias, file[start..end]));
                    }
                    Rotate();
                } else if(blocks == 0) {
                    word += current;
                }

            }

            if(inString) throw new Compiler.SyntaxException("Unclosed string!", file.ScriptTrace);

            return functions.ToArray();
        }

        public IEnumerator<ScriptClass> GetEnumerator() => ((IReadOnlyCollection<ScriptClass>)ScriptClasses).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyCollection<ScriptClass>)ScriptClasses).GetEnumerator();

    }

}
