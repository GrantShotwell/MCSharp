using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {
    public struct ScriptClass : IReadOnlyCollection<ScriptFunction> {

        private readonly ScriptFunction[] functions;

        public int Count => ((IReadOnlyCollection<ScriptFunction>)functions).Count;
        public string FilePath { get; }
        public string FileName { get; }
        public string Alias { get; }
        

        public ScriptClass(string file, string alias, string scriptClass) : this(file, alias, GetFunctions(file, scriptClass)) { }

        public ScriptClass(string file, string alias, ScriptFunction[] functions) {
            FilePath = file;
            FileName = file.Split('\\')[^1];
            Alias = alias;
            this.functions = functions;
        }


        public static ScriptFunction[] GetFunctions(string file, string scriptClass) {
            if(scriptClass is null) return new ScriptFunction[] { };
            var functions = new List<ScriptFunction>();

            int start = 0, blocks = 0;
            string word = "", lastWord = "";
            string alias = "";

            void Rotate() {
                lastWord = word;
                word = "";
            }

            for(int end = 0; end < scriptClass.Length; end++) {

                char current = scriptClass[end];
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
                        functions.Add(new ScriptFunction(file, alias, scriptClass[start..end]));
                    }
                    Rotate();
                } else if(blocks == 0) {
                    word += current;
                }

            }

            return functions.ToArray();
        }

        public IEnumerator<ScriptFunction> GetEnumerator() => ((IReadOnlyCollection<ScriptFunction>)functions).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyCollection<ScriptFunction>)functions).GetEnumerator();

    }
}
