using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace MCSharp.Variables {

	public abstract class VarPrimitive : Variable {

		private int constant;
		private VarObjective objective;
		private VarSelector selector;

		public override string TypeName => "primitive";

		/// <summary>The <see cref="VarSelector"/> that contains the value.</summary>
		public VarSelector Selector {
			[DebuggerStepThrough]
			get {
				if(Usage == Usage.Constant) throw new InvalidOperationException("Cannot get the selector of a constant primitive.");
				else if(!Constructed) throw new InvalidOperationException("Cannot get the selector of a primitive that has not been constructed.");
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
				else if(!Constructed) throw new InvalidOperationException("Cannot get the objective of a primitive that has not been constructed.");
				else return objective;
			}
			[DebuggerStepThrough]
			protected set {
				if(Usage == Usage.Constant) throw new InvalidOperationException("Cannot set the objective of a constant primitive.");
				else objective = value;
			}
		}
		/// <summary>The constant value.</summary>
		public int Constant {
			[DebuggerStepThrough]
			get {
				if(Usage != Usage.Constant) throw new InvalidOperationException("Cannot get the constant value of a non-constant primitive.");
				else if(!Constructed) throw new InvalidOperationException("Cannot get the constant value of a constant primitive that has not been set.");
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


		public VarPrimitive() : base() { }
		public VarPrimitive(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) { }


		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			switch(operation) {

				case Operation.Equal: {
					if(operand is VarPrimitive right || operand.TryCast(out right)) {

						VarBool result;
						if(Usage == Usage.Constant) {
							if(right.Usage == Usage.Constant) {
								result = new VarBool(Access.Private, Usage.Constant, GetNextHiddenID(), Scope);
								result.SetValue(Constant == right.Constant ? 1 : 0);
							} else {
								result = new VarBool(Access.Private, Usage.Default, GetNextHiddenID(), Scope);
								result.SetValue(0);
								new Spy(null, $"execute if score " +
									$"{right.Selector.GetConstant()} {right.Objective.GetConstant()} matches " +
									$"{Constant} run " +
									$"scoreboard players set {result.Selector.GetConstant()} {result.Objective.GetConstant()} 1", null);
							}
						} else {
							if(right.Usage == Usage.Constant) {
								result = new VarBool(Access.Private, Usage.Default, GetNextHiddenID(), Scope);
								result.SetValue(0);
								new Spy(null, $"execute if score " +
									$"{Selector.GetConstant()} {Objective.GetConstant()} matches " +
									$"{right.Constant} run " +
									$"scoreboard players set {result.Selector.GetConstant()} {result.Objective.GetConstant()} 1", null);
							} else {
								result = new VarBool(Access.Private, Usage.Default, GetNextHiddenID(), Scope);
								result.SetValue(0);
								new Spy(null, $"execute if score " +
									$"{Selector.GetConstant()} {Objective.GetConstant()} = " +
									$"{right.Selector.GetConstant()} {right.Objective.GetConstant()} run " +
									$"scoreboard players set {result.Selector.GetConstant()} {result.Objective.GetConstant()} 1", null);
							}
						}
						return result;

					} else throw new InvalidCastException(operand, "primitive", trace);
				}

				case Operation.Set: {
					if(operand is VarPrimitive right || operand.TryCast(out right)) {

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
				}

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

					VarSelector sel;
					AddAutoProperty(Selector = sel = new VarSelector(Access.Private, Usage.Constant, char.ToLower(VarSelector.StaticTypeName[0]) + VarSelector.StaticTypeName.Substring(1), InnerScope));
					sel.InvokeOperation(Operation.Set, Constructors[sel.TypeName](new ArgumentInfo(new Variable[] { (VarString)"var" }, Compiler.CurrentScriptTrace)), Compiler.CurrentScriptTrace);
#if DEBUG_OUT
					VarObjective.NextID = $"{name}@{Scope}";
#endif
					VarObjective obj;
					AddAutoProperty(Objective = obj = new VarObjective(Access.Private, Usage.Constant, char.ToLower(VarObjective.StaticTypeName[0]) + VarObjective.StaticTypeName.Substring(1), InnerScope));
					obj.InvokeOperation(Operation.Set, Constructors[obj.TypeName](new ArgumentInfo(new Variable[] { (VarString)"dummy" }, Compiler.CurrentScriptTrace)), Compiler.CurrentScriptTrace);

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

				VarSelector sel;
				AddAutoProperty(Selector = sel = new VarSelector(Access.Private, Usage.Default, char.ToLower(VarSelector.StaticTypeName[0]) + VarSelector.StaticTypeName.Substring(1), InnerScope));
				sel.InvokeOperation(Operation.Set, Constructors[sel.TypeName](new ArgumentInfo(new Variable[] { (VarString)"var" }, Compiler.CurrentScriptTrace)), Compiler.CurrentScriptTrace);
#if DEBUG_OUT
			VarObjective.NextID = $"{name}@{Scope}";
#endif
				VarObjective obj;
				AddAutoProperty(Objective = obj = new VarObjective(Access.Private, Usage.Default, char.ToLower(VarObjective.StaticTypeName[0]) + VarObjective.StaticTypeName.Substring(1), InnerScope));
				obj.InvokeOperation(Operation.Set, Constructors[obj.TypeName](new ArgumentInfo(new Variable[] { (VarString)"dummy" }, Compiler.CurrentScriptTrace)), Compiler.CurrentScriptTrace);

				Constructed = true;

			}

			new Spy(null, $"scoreboard players operation {Selector.GetConstant()} {Objective.GetConstant()} = {selector.GetConstant()} {objective.GetConstant()}", null);

		}

	}

}
