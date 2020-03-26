using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

	public class ScriptFile : IReadOnlyCollection<ScriptClass> {


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
			var classes = new List<ScriptClass>();

			int start = 0, blocks = 0;

			ScriptString?[] words = new ScriptString?[3];
			ScriptString? alias = null;

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
					if((words[0].HasValue ? words[0].Value != "" : false) && blocks == 0) Rotate();
				} else if(current == '{') {
					blocks++;
					if(blocks == 1) {
						start = end + 1;
						alias = words[1].Value;
					}
					Rotate();
				} else if(current == '}') {
					blocks--;
					if(blocks == 0) {
						classes.Add(new ScriptClass((string)alias.Value, file[start..end]));
					}
					Rotate();
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

		public IEnumerator<ScriptClass> GetEnumerator() => ((IReadOnlyCollection<ScriptClass>)ScriptClasses).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyCollection<ScriptClass>)ScriptClasses).GetEnumerator();

	}

}
