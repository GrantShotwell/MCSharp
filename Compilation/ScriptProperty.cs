using MCSharp.Variables;
using System;
using System.Diagnostics;

namespace MCSharp.Compilation {

	[DebuggerDisplay("{Access} {Usage} {TypeName} {FullAlias}")]
	public class ScriptProperty : ScriptMember {

		public enum Accessors { Get, Set }

		public ScriptMethod GetMethod { get; }
		public Variable.GetProperty GetFunc { get; }
		public ScriptMethod SetMethod { get; }
		public Variable.SetProperty SetFunc { get; }

		public override ScriptClass DeclaringType {
			get => base.DeclaringType;
			set {
				base.DeclaringType = value;
				if(!(GetMethod is null)) GetMethod.DeclaringType = DeclaringType;
				if(!(SetMethod is null)) SetMethod.DeclaringType = DeclaringType;
			}
		}

		public ScriptProperty(string alias, string type, ScriptMethod get, ScriptMethod set,
		  Access access, Usage usage, ScriptClass declaringType, ScriptTrace trace)
		: base(alias, type, access, usage, declaringType, trace) {

			bool getNull = get is null, setNull = set is null;
			if(getNull && setNull) throw new Compiler.SyntaxException("Expected at least one accessor for property.", trace);
			if(getNull ? false : get.Parameters.Length != 0) throw new Exception("034503232020a"); //Get method should have no parameters.
			if(setNull ? false : set.Parameters.Length != 1) throw new Exception("034503232020b"); //Set method should have one parameter.

			GetMethod = get;
			GetFunc = getNull ? (Variable.GetProperty)null : (() => get.Func.Invoke(new Variable[] { }));
			SetMethod = set;
			SetFunc = setNull ? (Variable.SetProperty)null : ((x) => set.Func.Invoke(new Variable[] { x }));

			if(!getNull) GetMethod.DeclaringType = declaringType;
			if(!setNull) SetMethod.DeclaringType = declaringType;

		}

	}

}
