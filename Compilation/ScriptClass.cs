using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

    public struct ScriptClass : IReadOnlyCollection<ScriptFunction> {


        private ScriptFunction[] ScriptFunctions { get; }
        public ScriptFunction this[int index] => ScriptFunctions[index];
        public int Length => ScriptFunctions.Length;
        int IReadOnlyCollection<ScriptFunction>.Count => Length;
        public string Alias { get; }
        public ScriptTrace ScriptTrace => ScriptFunctions[0].ScriptTrace;


        public ScriptClass(string alias, ScriptString scriptClass) : this(alias, GetFunctions(alias, scriptClass)) { }
        public ScriptClass(string alias, ScriptFunction[] functions) {
            //FilePath = file;
            //FileName = file[Program.ScriptsFolder.Length..];
            Alias = alias;
            ScriptFunctions = functions;
        }


        public static ScriptFunction[] GetFunctions(string root, ScriptString scriptClass) {
            var functions = new List<ScriptFunction>();

            int start = 0, blocks = 0;
            string word = "", lastWord = "";
            string alias = "";

            void Rotate() {
                lastWord = word;
                word = "";
            }

            for(int end = 0; end < scriptClass.Length; end++) {

                char current = (char)scriptClass[end];
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
                        functions.Add(new ScriptFunction(root + "\\" + alias, scriptClass[start..end]));
                    }
                    Rotate();
                } else if(blocks == 0) {
                    word += current;
                }

            }

            return functions.ToArray();
        }

        public IEnumerator<ScriptFunction> GetEnumerator() => ((IReadOnlyCollection<ScriptFunction>)ScriptFunctions).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyCollection<ScriptFunction>)ScriptFunctions).GetEnumerator();

    }
}
