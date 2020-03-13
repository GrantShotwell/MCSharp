using MCSharp.Variables;
using System.Diagnostics;

namespace MCSharp.Compilation {

	[DebuggerDisplay("{Access} {Usage} {TypeName} {FullAlias}")]
	public class ScriptProperty : ScriptMember {

		public enum Accessors { Get, Set }

		public ScriptMethod GetMethod { get; }
		public Variable.GetProperty GetFunc { get; }
		public ScriptMethod SetMethod { get; }
		public Variable.SetProperty SetFunc { get; }

		public ScriptProperty(string alias, string type, ScriptMethod get, ScriptMethod set,
		  Access access, Usage usage, ScriptClass declaringType, ScriptTrace trace)
		: base(alias, type, access, usage, declaringType, trace) {

			if(get is null && set is null) throw new Compiler.SyntaxException("Expected at least one accessor for property.", trace);
			GetMethod = get;
			GetFunc = get is null ? (Variable.GetProperty)null : (() => get.Func.Invoke(new Variable[] { }));
			SetMethod = set;
			SetFunc = set is null ? (Variable.SetProperty)null : ((x) => set.Func.Invoke(new Variable[] { x }));

		}

	}

}
