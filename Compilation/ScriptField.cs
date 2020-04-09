using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

	public class ScriptField : ScriptMember {

		public ScriptWild Init { get; }


		public ScriptField(string alias, string type, Access access, Usage usage, ScriptClass declarer, ScriptString phrase)
			: this(alias, type, access, usage, declarer, new ScriptWild(ScriptLine.GetWilds(phrase), "(\\)", ' ')) { }

		public ScriptField(string alias, string type, Access access, Usage usage, ScriptClass declarer, ScriptWild init)
			: base(alias, type, access, usage, declarer, init.ScriptTrace) {

			Init = init;

		}

	}

}
