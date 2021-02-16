using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Script {

	public struct ScriptStatement : IReadOnlyList<ScriptToken> {

		public int Count => throw new NotImplementedException();

		public ScriptToken this[int index] => throw new NotImplementedException();

		public IEnumerator<ScriptToken> GetEnumerator() => throw new NotImplementedException();
		IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

	}

}
