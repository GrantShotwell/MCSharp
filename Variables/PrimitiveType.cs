using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

	public abstract class PrimitiveType : Variable {

		public override string TypeName => "primitive";

		/// <summary>The <see cref="VarSelector"/> that contains the value.</summary>
		public VarSelector Selector { get; }
		/// <summary>The <see cref="VarObjective"/> that contains the value.</summary>
		public VarObjective Objective { get; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Constant, Usage.Static, Usage.Default };


		public PrimitiveType() : base() { }

		public PrimitiveType(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) {
			Fields.Add("selector", Selector = new VarSelector(access, usage, GetNextHiddenID(), scope, "var"));
#if DEBUG_OUT
			VarObjective.NextID = $"{name}@{Scope}";
#endif
			Fields.Add("objective", Objective = new VarObjective(access, usage, GetNextHiddenID(), scope, "dummy"));
		}


		public override void WriteInit(StreamWriter function) => base.WriteInit(function);
		
		/// <summary>
		/// Writes the command(s) to set the value.
		/// </summary>
		/// <param name="value">The value to copy from.</param>
		public void SetValue(int value) => new Spy(null, $"scoreboard players set {Selector.GetConstant()} {Objective.GetConstant()} {value}", null);

		/// <summary>
		/// Writes the command(s) to set the value.
		/// </summary>
		/// <param name="selector">The selector to copy from.</param>
		/// <param name="objective">The objective to copy from.</param>
		public void SetValue(VarSelector selector, VarObjective objective) => SetValue(selector.GetConstant(), objective.GetConstant());

		/// <summary>
		/// Writes the command(s) to set the value.
		/// </summary>
		/// <param name="selector">The selector to copy from.</param>
		/// <param name="objective">The objective to copy from.</param>
		public void SetValue(string selector, string objective) {
			new Spy(null, $"scoreboard players operation " +
				$"{Selector.GetConstant()} {Objective.GetConstant()} = " +
				$"{selector} {objective}", null);
		}

	}

}
