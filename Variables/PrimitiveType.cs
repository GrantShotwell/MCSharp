using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

	public abstract class PrimitiveType : Variable {

		public override string TypeName => "primitive";

		/// <summary>The <see cref="VarSelector"/> that contains the value.</summary>
		public VarSelector Selector { get; protected set; }
		/// <summary>The <see cref="VarObjective"/> that contains the value.</summary>
		public VarObjective Objective { get; protected set; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Static };


		public PrimitiveType() : base() { }

		public PrimitiveType(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) {
		}


		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			switch(operation) {
				case Operation.Set:
					if(operand is PrimitiveType primitive || operand.TryCast(out primitive)) {
						SetValue(primitive.Selector, primitive.Objective);
						return this;
					} else throw new Exception();
				default: return base.InvokeOperation(operation, operand, trace);
			}
		}

		public override void WriteInit(StreamWriter function) => base.WriteInit(function);

		/// <summary>
		/// Writes the command(s) to set the value.
		/// </summary>
		/// <param name="value">The value to copy from.</param>
		public void SetValue(int value) {
			if(!Constructed) {
				AddAutoProperty(Selector = new VarSelector(Access.Private, Usage.Default, char.ToLower(VarSelector.StaticTypeName[0]) + VarSelector.StaticTypeName.Substring(1), InnerScope));
				Selector.InvokeOperation(Operation.Set, Constructors[Selector.TypeName](new Variable[] { (VarString)"var" }), Compiler.CurrentScriptTrace);
#if DEBUG_OUT
			VarObjective.NextID = $"{name}@{Scope}";
#endif
				AddAutoProperty(Objective = new VarObjective(Access.Private, Usage.Default, char.ToLower(VarObjective.StaticTypeName[0]) + VarObjective.StaticTypeName.Substring(1), InnerScope));
				Objective.InvokeOperation(Operation.Set, Constructors[Objective.TypeName](new Variable[] { (VarString)"dummy" }), Compiler.CurrentScriptTrace);
				Constructed = true;
			}
			new Spy(null, $"scoreboard players set {Selector.GetConstant()} {Objective.GetConstant()} {value}", null);
		}

		/// <summary>
		/// Writes the command(s) to set the value.
		/// </summary>
		/// <param name="selector">The selector to copy from.</param>
		/// <param name="objective">The objective to copy from.</param>
		public void SetValue(VarSelector selector, VarObjective objective) {

            if(selector is null)
                throw new ArgumentNullException(nameof(selector));
            if(objective is null)
                throw new ArgumentNullException(nameof(objective));

            if(!Constructed) {
				AddAutoProperty(Selector = new VarSelector(Access.Private, Usage.Default, char.ToLower(VarSelector.StaticTypeName[0]) + VarSelector.StaticTypeName.Substring(1), InnerScope));
				Selector.InvokeOperation(Operation.Set, Constructors[Selector.TypeName](new Variable[] { (VarString)"var" }), Compiler.CurrentScriptTrace);
#if DEBUG_OUT
			VarObjective.NextID = $"{name}@{Scope}";
#endif
				AddAutoProperty(Objective = new VarObjective(Access.Private, Usage.Default, char.ToLower(VarObjective.StaticTypeName[0]) + VarObjective.StaticTypeName.Substring(1), InnerScope));
				Objective.InvokeOperation(Operation.Set, Constructors[Objective.TypeName](new Variable[] { (VarString)"dummy" }), Compiler.CurrentScriptTrace);
				Constructed = true;
			}
			new Spy(null, $"scoreboard players operation {Selector.GetConstant()} {Objective.GetConstant()} = {selector.GetConstant()} {objective.GetConstant()}", null);
		}

	}

}
