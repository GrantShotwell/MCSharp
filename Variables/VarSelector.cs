using MCSharp.Compilation;
using System;
using System.Collections.Generic;

namespace MCSharp.Variables {

	/// <summary>
	/// Represents an entity/player selector string.
	/// </summary>
	public class VarSelector : Variable {

		private VarString String { get; set; }

		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "Selector";

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default };


		public VarSelector() : base() { }
		public VarSelector(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) {
			AddAutoProperty(String = new VarString(Access.Public, Usage.Default, char.ToLower(VarString.StaticTypeName[0]) + VarString.StaticTypeName.Substring(1), InnerScope));
		}

		public static explicit operator VarSelector(string str) => Constructors[StaticTypeName](new ArgumentInfo(new Variable[] { (VarString)str }, Compiler.CurrentScriptTrace)) as VarSelector;

		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => new VarSelector(access, usage, name, scope);

		private static readonly ParameterInfo[] ConstructorOverloads = new ParameterInfo[] {
			new (Type Type, bool Reference)[] { (typeof(VarString), true) }
		};
		public override Variable Construct(ArgumentInfo arguments) {
			(ParameterInfo match, int index) = ParameterInfo.HighestMatch(ConstructorOverloads, arguments);
			match.Grab(arguments);

			switch(index) {
				case 0:
					var value = new VarSelector(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
					value.String.InvokeOperation(Operation.Set, match[0].Value as VarString, Compiler.CurrentScriptTrace);
					value.Constructed = true;
					return value;

				default: throw new Compiler.InternalError($"Not all Objective constructor overflows were accounted for ({index}).", arguments.ScriptTrace);
			}
		}

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			switch(operation) {

				case Operation.Set:
					if(operand is VarString varString || operand.TryCast(out varString)) {
						String = varString;
						return this;
					} else throw new InvalidCastException(operand, TypeName, trace);

				default: return base.InvokeOperation(operation, operand, trace);
			}
		}

		public override IDictionary<Type, Caster> GetCasters_From() {
			IDictionary<Type, Caster> casters = base.GetCasters_From();
			casters.Add(typeof(VarString), value => {
				var result = new VarSelector(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
				return Constructors[result.TypeName](new ArgumentInfo(new Variable[] { value }, Compiler.CurrentScriptTrace));
			});
			return casters;
		}

		public override string GetConstant() => String.GetConstant();
		public override string GetJSON() => String.GetJSON();
		public override VarString GetString() => String;

	}

}
