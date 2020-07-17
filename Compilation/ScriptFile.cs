using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace MCSharp.Compilation {

	//[DebuggerDisplay("ToString(),nq")]
	public class ScriptFile : IReadOnlyCollection<ScriptObject> {


		public static Dictionary<string, ScriptFile> Files { get; } = new Dictionary<string, ScriptFile>();

		private ScriptObject[] ScriptClasses { get; }
		public ScriptObject this[int index] => ScriptClasses[index];
		public int Length => ScriptClasses.Length;
		int IReadOnlyCollection<ScriptObject>.Count => Length;
		public ScriptTrace ScriptTrace => ScriptClasses[0].ScriptTrace;


		public ScriptFile(ScriptString scriptFile) : this(GetClasses(scriptFile)) { }

		public ScriptFile(ScriptObject[] classes) {
			ScriptClasses = classes;
			Files.Add(ScriptTrace.FilePath, this);
		}


		public static ScriptObject[] GetClasses(ScriptString file) {
			var classes = new List<ScriptObject>();

			int start = 0, blocks = 0;

			ScriptString?[] words = new ScriptString?[8];

			void Rotate() {
				for(int i = words.Length - 1; i > 0; i--)
					words[i] = words[i - 1];
				words[0] = null;
			}

			bool inString = false;
			for(int end = 0; end < file.Length; end++) {
				ScriptChar current = file[end];

				if(current == '"' && (char)file[end - 1] != '\\') {
					inString = !inString;
					continue;
				}

				if(char.IsWhiteSpace((char)current)) {
					if(words[0].HasValue && words[0].Value != "" && blocks == 0) Rotate();
				} else if(current == '{') {
					blocks++;
					if(blocks == 1) {
						start = end + 1;
					}
				} else if(current == '}') {
					blocks--;
					if(blocks == 0) {
						//todo: access
						classes.Add(new ScriptObject(words[1].Value, words[2].Value, file[start..end]));
					}
				} else if(blocks == 0) {
					if(!words[0].HasValue) words[0] = current;
					else {
						var word = words[0].Value;
						word += current;
						words[0] = word;
					}
				}

			}

			if(inString) throw new Compiler.SyntaxException("Unclosed string!", file.ScriptTrace);

			return classes.ToArray();
		}

		public IEnumerator<ScriptObject> GetEnumerator() => ((IReadOnlyCollection<ScriptObject>)ScriptClasses).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyCollection<ScriptObject>)ScriptClasses).GetEnumerator();

	}

}
