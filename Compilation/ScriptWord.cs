using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

	public struct ScriptWord : IReadOnlyList<ScriptChar> {

		private ScriptString ScriptString { get; }
		public ScriptChar this[int index] => ScriptString[index];
		public ScriptWord this[Range range] => new ScriptWord(ScriptString[range]);
		public int Length => ScriptString.Length;
		public ScriptTrace ScriptTrace => ScriptString.ScriptTrace;

		int IReadOnlyCollection<ScriptChar>.Count => Length;


		public ScriptWord(ScriptString characters, bool ignoreCohesion = false) {
			if(!ignoreCohesion) {
				foreach(ScriptChar character in characters) {
					if(char.IsWhiteSpace((char)character))
						throw new ArgumentException("Given array is not a cohesive word!", nameof(characters));
				}
			}
			ScriptString = characters;
		}


		public IEnumerator<ScriptChar> GetEnumerator() => ((IEnumerable<ScriptChar>)ScriptString).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<ScriptChar>)ScriptString).GetEnumerator();

		public static implicit operator ScriptString(ScriptWord word) => new ScriptString(word.ScriptString);
		public static implicit operator ScriptWord(ScriptString str) => new ScriptWord(str);
		public static explicit operator string(ScriptWord word) => (string)(ScriptString)word;

		public static bool operator ==(ScriptWord left, string right) => (string)left == right;
		public static bool operator !=(ScriptWord left, string right) => (string)left != right;
		public static bool operator ==(string left, ScriptWord right) => left == (string)right;
		public static bool operator !=(string left, ScriptWord right) => left != (string)right;

		public override string ToString() => $"{ScriptTrace}: \"{ScriptString}\"";

	}

}
