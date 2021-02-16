using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MCSharp.Script {

	public struct ScriptToken : IReadOnlyList<char> {

		public ScriptTrace Trace { get; }
		private char[] Array { get; }
		public int Count => Array.Length;

		public char this[int index] => Array[index];
		public char this[Index index] => Array[index];
		public char[] this[Range range] => Array[range];

		public ScriptToken(string characters, ScriptTrace trace) {
			Array = characters?.ToArray() ?? throw new ArgumentNullException(nameof(characters));
			Trace = trace ?? throw new ArgumentNullException(nameof(trace));
		}
		public ScriptToken(IList<char> characters, ScriptTrace trace) {
			Array = characters?.ToArray() ?? throw new ArgumentNullException(nameof(characters));
			Trace = trace ?? throw new ArgumentNullException(nameof(trace));
		}
		public ScriptToken(IReadOnlyList<char> characters, ScriptTrace trace) {
			Array = characters?.ToArray() ?? throw new ArgumentNullException(nameof(characters));
			Trace = trace ?? throw new ArgumentNullException(nameof(trace));
		}

		public IEnumerator<char> GetEnumerator() => ((IEnumerable<char>)Array).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => Array.GetEnumerator();

		public static explicit operator string(ScriptToken token) => new string(token.Array);
		public override string ToString() => $"{Trace} '{(string)this}'";

	}

}
