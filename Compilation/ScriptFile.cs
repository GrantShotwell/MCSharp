using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

    public struct ScriptFile : IReadOnlyCollection<ScriptClass> {

        private readonly ScriptClass[] classes;

        public static Dictionary<string, ScriptFile> Files { get; } = new Dictionary<string, ScriptFile>();

        public int Count => ((IReadOnlyCollection<ScriptClass>)classes).Count;
        public string FilePath { get; }
        public string FileName { get; }


        public ScriptFile(string file, string scriptFile) : this(file, GetClasses(file, scriptFile)) { }

        public ScriptFile(string file, ScriptClass[] classes) {
            FilePath = file;
            FileName = file.Split('\\')[^1];
            this.classes = classes;
            Files.Add(FilePath, this);
        }


        public static ScriptClass[] GetClasses(string file, string scriptFile) {
            if(scriptFile is null) return new ScriptClass[] { };
            var functions = new List<ScriptClass>();

            int start = 0, blocks = 0;
            string word = "", lastWord = "";
            string alias = "";

            void Rotate() {
                lastWord = word;
                word = "";
            }

            for(int end = 0; end < scriptFile.Length; end++) {

                char current = scriptFile[end];
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
                        functions.Add(new ScriptClass(file, alias, scriptFile[start..end]));
                    }
                    Rotate();
                } else if(blocks == 0) {
                    word += current;
                }

            }

            return functions.ToArray();
        }

        public IEnumerator<ScriptClass> GetEnumerator() => ((IReadOnlyCollection<ScriptClass>)classes).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyCollection<ScriptClass>)classes).GetEnumerator();

    }

}
