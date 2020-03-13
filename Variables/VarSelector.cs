using MCSharp.Compilation;
using System;
using System.Collections.Generic;

namespace MCSharp.Variables {

	/// <summary>
	/// Represents an entity/player selector string.
	/// </summary>
	public class VarSelector : Variable {

		private string constant;

		private string Value {
			get => constant;
			set {
				if(constant == null) constant = value;
				else throw new InvalidOperationException($"Cannot set the value of '{nameof(Value)}' because it already had been set.");
			}
		}

		public override string TypeName => "Selector";

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


		public VarSelector() : base() { }
		public VarSelector(Access access, Usage usage, string name, Compiler.Scope scope, string value) :
		base(access, Usage.Constant, name, scope) {
			Value = value;
		}


		protected override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) {
			base.Initialize(access, usage, name, scope, trace);
			return new VarSelector(access, usage, name, scope, null);
		}

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			switch(operation) {

				case Operation.Set:
				if(operand is VarString varString || operand.TryCast(out varString)) {
					//if(Usage == Usage.Static || Usage == Usage.Constant) throw new Compiler.SyntaxException("Cannot set a static/constant variable.", trace);
					Value = varString.GetConstant();
					return this;
				} else throw new InvalidCastException(operand, TypeName, trace);

				default: return base.InvokeOperation(operation, operand, trace);
			}
		}

		public override string GetConstant() => Usage == Usage.Constant ? Value : base.GetConstant();
		public override string GetJSON() => $"{{\"text\":\"{Value}\"}}";

	}

}
