using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;

namespace MCSharp.Variables {

	public class VarString : Variable {

		public override int Order => 100;
		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "string";
		private string Value { get; set; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant };


		public VarString() : base() { }
		public VarString(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) { }


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => new VarString(access, usage, name, scope);
		public override Variable Construct(ArgumentInfo passed) => throw new Compiler.SyntaxException($"'{TypeName}' types cannot be constructed.", Compiler.CurrentScriptTrace);

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			switch(operation) {
				case Operation.Set:
					if(operand is VarString varString || operand.TryCast(out varString)) {
						SetValue(varString.Value);
						return this;
					} else throw new Exception();
				default: return base.InvokeOperation(operation, operand, trace);
			}
		}

		public void SetValue(string value) => Value = value;

		public override string GetConstant() => Value;
		public override VarString GetString() => this;
		public override string GetJSON() => $"{{\"text\":\"{Value}\"}}";

		public static explicit operator VarString(string str) => new VarString(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope) { Value = str };
	}

}
