using System.Diagnostics;

namespace MCSharp.Compilation {

	[DebuggerDisplay("{ToString(),nq}")]
	public struct ScriptChar {

		private char Character { get; }
		public ScriptTrace ScriptTrace { get; }

		public ScriptChar(char character, string path, int line) {
			Character = character;
			ScriptTrace = new ScriptTrace(path, line);
		}


		public static ScriptString operator +(ScriptChar left, ScriptChar right) => new ScriptString(new ScriptChar[] { left, right });

		public static bool operator ==(ScriptChar left, char right) => (char)left == right;
		public static bool operator ==(char left, ScriptChar right) => left == (char)right;
		public static bool operator !=(ScriptChar left, char right) => (char)left != right;
		public static bool operator !=(char left, ScriptChar right) => left != (char)right;

		public static explicit operator char(ScriptChar character) => character.Character;
		public static implicit operator ScriptWord(ScriptChar character) => new ScriptWord(character);

		public override string ToString() => $"[{ScriptTrace}] '{Character}'";

	}

}
