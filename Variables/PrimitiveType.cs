using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

	public abstract class PrimitiveType : Variable {

		public override string TypeName => "primitive";

		public VarSelector Selector { get; }
		public VarObjective Objective { get; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Constant, Usage.Static, Usage.Default };


		public PrimitiveType() : base() { }

		public PrimitiveType(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) {
			Fields.Add("selector", Selector = new VarSelector(access, usage, GetNextHiddenID(), scope, "var"));
			Fields.Add("objective", Objective = new VarObjective(access, usage, GetNextHiddenID(), scope, "dummy"));
		}


		public override void WriteInit(StreamWriter function) { }

		public void SetValue(int value) => new Spy(null, $"scoreboard players set {Selector.GetConstant()} {Objective.GetConstant()} {value}", null);
		public void SetValue(VarSelector selector, VarObjective objective) => SetValue(selector.GetConstant(), objective.GetConstant());
		public void SetValue(string selector, string objective) {
			new Spy(null, $"scoreboard players operation " +
				$"{Selector.GetConstant()} {Objective.GetConstant()} = " +
				$"{selector} {objective}", null);
		}

	}

}
