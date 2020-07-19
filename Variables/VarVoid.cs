using MCSharp.Compilation;
using System.Collections.Generic;

namespace MCSharp.Variables {
	class VarVoid : Variable {

		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "void";

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Abstract, Usage.Default, Usage.Static, Usage.Constant };


		public VarVoid() : base() { }
		public VarVoid(Access access, Usage usage, string objectName, Compiler.Scope scope)
		: base(access, usage, objectName, scope) { }


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace)
			=> new VarVoid(access, usage, name, scope);
		public override Variable Construct(ArgumentInfo passed) => throw new Compiler.SyntaxException($"{TypeName} cannot be constructed.", Compiler.CurrentScriptTrace);

	}
}
