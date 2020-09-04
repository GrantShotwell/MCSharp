using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace MCSharp.Compilation {

	[DebuggerDisplay("{ToString(),nq}")]
	public struct ScriptWord : IReadOnlyList<ScriptChar> {

		private ScriptString ScriptString { get; }
		public ScriptChar this[int index] => ScriptString[index];
		public ScriptWord this[Range range] => new ScriptWord(ScriptString[range]);
		public int Length => ScriptString.Length;
		public ScriptTrace ScriptTrace => ScriptString.ScriptTrace;

		int IReadOnlyCollection<ScriptChar>.Count => Length;


		[DebuggerStepThrough]
		public ScriptWord(ScriptString characters, bool ignoreCohesion = false) {
			if(ignoreCohesion || (characters.Length >= 2 && characters[0] == '"' && characters[^1] == '"')) {
			} else {
				foreach(ScriptChar character in characters) {
					if(char.IsWhiteSpace((char)character))
						throw new ArgumentException("Given array is not a cohesive word!", nameof(characters));
				}
			}
			ScriptString = characters;
		}


		public IEnumerator<ScriptChar> GetEnumerator() => ScriptString.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ScriptString.GetEnumerator();

		[DebuggerStepThrough]
		public static implicit operator ScriptString(ScriptWord word) => new ScriptString(word.ScriptString);
		[DebuggerStepThrough]
		public static implicit operator ScriptWord(ScriptString str) => new ScriptWord(str);
		[DebuggerStepThrough]
		public static explicit operator string(ScriptWord word) => (string)(ScriptString)word;

		public static bool operator ==(ScriptWord left, string right) => (string)left == right;
		public static bool operator !=(ScriptWord left, string right) => (string)left != right;
		public static bool operator ==(string left, ScriptWord right) => left == (string)right;
		public static bool operator !=(string left, ScriptWord right) => left != (string)right;

		public override string ToString() => $"[{ScriptTrace}] \"{(string)this}\"";

	}

}
