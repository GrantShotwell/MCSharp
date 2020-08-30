using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MCSharp.Variables {

	public abstract class PrimitiveType : Variable {
		private int constant;
		private VarObjective objective;
		private VarSelector selector;

		public override string TypeName => "primitive";

		/// <summary>The <see cref="VarSelector"/> that contains the value.</summary>
		public VarSelector Selector {
			[DebuggerStepThrough]
			get {
				if(Usage == Usage.Constant) throw new InvalidOperationException("Cannot get the selector of a constant primitive.");
				else return selector;
			}
			[DebuggerStepThrough]
			protected set {
				if(Usage == Usage.Constant) throw new InvalidOperationException("Cannot set the selector of a constant primitive.");
				else selector = value;
			}
		}
		/// <summary>The <see cref="VarObjective"/> that contains the value.</summary>
		public VarObjective Objective {
			[DebuggerStepThrough]
			get {
				if(Usage == Usage.Constant) throw new InvalidOperationException("Cannot get the objective of a constant primitive.");
				else return objective;
			}
			[DebuggerStepThrough]
			protected set {
				if(Usage == Usage.Constant) throw new InvalidOperationException("Cannot set the objective of a constant primitive.");
				else objective = value;
			}
		}

		public int Constant {
			[DebuggerStepThrough]
			get {
				if(Usage != Usage.Constant) throw new InvalidOperationException("Cannot get the constant value of a non-constant primitive.");
				else if(!Constructed) throw new InvalidOperationException("Cannot get a constant value of a constant primitive that has not been set.");
				else return constant;
			}
			[DebuggerStepThrough]
			protected set {
				if(Usage != Usage.Constant) throw new InvalidOperationException("Cannot set the constant value of a non-constant primitive.");
				else if(Constructed) throw new InvalidOperationException("Cannot change the constant value of a constant primitive.");
				else { constant = value; Constructed = true; }
			}
		}

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Static, Usage.Constant };


		public PrimitiveType() : base() { }

		public PrimitiveType(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) {
		}


		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			switch(operation) {
				case Operation.Set:
					if(operand is PrimitiveType right || operand.TryCast(out right)) {
						
						if(Usage == Usage.Constant) {
							if(right.Usage == Usage.Constant) {
								SetValue(right.Constant);
								return this;
							} else {
								throw new InvalidOperationException("Cannot set a constant to a non-constant value.");
							}
						} else {
							if(right.Usage == Usage.Constant) {
								SetValue(right.Constant);
								return this;
							} else {
								SetValue(right.Selector, right.Objective);
								return this;
							}
						}

					} else throw new InvalidCastException(operand, "primitive", trace);
				default: return base.InvokeOperation(operation, operand, trace);
			}
		}

		public override void WriteInit(StreamWriter function) => base.WriteInit(function);

		/// <summary>
		/// Writes the command(s) to set the value.
		/// </summary>
		/// <param name="value">The value to copy from.</param>
		public void SetValue(int value) {
			if(Usage == Usage.Constant) {

				Constant = value;

			} else {

				if(!Constructed) {
					AddAutoProperty(Selector = new VarSelector(Access.Private, Usage.Constant, char.ToLower(VarSelector.StaticTypeName[0]) + VarSelector.StaticTypeName.Substring(1), InnerScope));
					Selector.InvokeOperation(Operation.Set, Constructors[Selector.TypeName](new ArgumentInfo(new Variable[] { (VarString)"var" }, Compiler.CurrentScriptTrace)), Compiler.CurrentScriptTrace);
#if DEBUG_OUT
					VarObjective.NextID = $"{name}@{Scope}";
#endif
					AddAutoProperty(Objective = new VarObjective(Access.Private, Usage.Constant, char.ToLower(VarObjective.StaticTypeName[0]) + VarObjective.StaticTypeName.Substring(1), InnerScope));
					Objective.InvokeOperation(Operation.Set, Constructors[Objective.TypeName](new ArgumentInfo(new Variable[] { (VarString)"dummy" }, Compiler.CurrentScriptTrace)), Compiler.CurrentScriptTrace);
					Constructed = true;
				}

				new Spy(null, $"scoreboard players set {Selector.GetConstant()} {Objective.GetConstant()} {value}", null);

			}
		}

		/// <summary>
		/// Writes the command(s) to set the value.
		/// </summary>
		/// <param name="selector">The selector to copy from.</param>
		/// <param name="objective">The objective to copy from.</param>
		public void SetValue(VarSelector selector, VarObjective objective) {

			if(Usage == Usage.Constant)
				throw new InvalidOperationException("Cannot set the selector/objective value of a constant primitive.");
			if(selector is null)
				throw new ArgumentNullException(nameof(selector));
			if(objective is null)
				throw new ArgumentNullException(nameof(objective));

			if(!Constructed) {

				AddAutoProperty(Selector = new VarSelector(Access.Private, Usage.Default, char.ToLower(VarSelector.StaticTypeName[0]) + VarSelector.StaticTypeName.Substring(1), InnerScope));
				Selector.InvokeOperation(Operation.Set, Constructors[Selector.TypeName](new ArgumentInfo(new Variable[] { (VarString)"var" }, Compiler.CurrentScriptTrace)), Compiler.CurrentScriptTrace);
#if DEBUG_OUT
			VarObjective.NextID = $"{name}@{Scope}";
#endif
				AddAutoProperty(Objective = new VarObjective(Access.Private, Usage.Default, char.ToLower(VarObjective.StaticTypeName[0]) + VarObjective.StaticTypeName.Substring(1), InnerScope));
				Objective.InvokeOperation(Operation.Set, Constructors[Objective.TypeName](new ArgumentInfo(new Variable[] { (VarString)"dummy" }, Compiler.CurrentScriptTrace)), Compiler.CurrentScriptTrace);
				Constructed = true;

			}

			new Spy(null, $"scoreboard players operation {Selector.GetConstant()} {Objective.GetConstant()} = {selector.GetConstant()} {objective.GetConstant()}", null);

		}

	}

}
