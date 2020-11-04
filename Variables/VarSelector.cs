using MCSharp.Compilation;
using MCSharp.GameSerialization.Text;
using System;
using System.Collections.Generic;

namespace MCSharp.Variables {

	/// <summary>
	/// Represents an entity/player selector string.
	/// </summary>
	public class VarSelector : Variable {

		private VarString String { get; set; }

		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "selector";

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant };


		public VarSelector() : base() { }
		public VarSelector(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) {
			AddAutoProperty(String = new VarString(Access.Public, Usage.Default, char.ToLower(VarString.StaticTypeName[0]) + VarString.StaticTypeName.Substring(1), InnerScope));
		}

		public static explicit operator VarSelector(string str) => Constructors[StaticTypeName](new ArgumentInfo(new Variable[] { (VarString)str }, Compiler.CurrentScriptTrace)) as VarSelector;

		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => new VarSelector(access, usage, name, scope);

		private Compiler.Scope[] ConstructorScopes;
		public override Variable Construct(ArgumentInfo arguments) {
			if(ConstructorScopes is null) ConstructorScopes = InnerScope.CreateChildren(1);
			(ParameterInfo match, int index) = ParameterInfo.HighestMatch(new ParameterInfo[] {
				new (bool, string, string, Compiler.Scope)[] { (true, VarString.StaticTypeName, "value", ConstructorScopes[0]) }
			}, arguments);
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

		public override void ConstructAsPasser() => throw new NotImplementedException();

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			switch(operation) {

				case Operation.Set:
					if(operand is VarString varString || operand.TryCast(VarString.StaticTypeName, out varString)) {
						String = varString;
						return this;
					} else throw new InvalidCastException(operand, VarString.StaticTypeName, trace);

				default: return base.InvokeOperation(operation, operand, trace);
			}
		}

		public override IDictionary<string, Caster> GetCasters_From() {
			IDictionary<string, Caster> casters = base.GetCasters_From();
			casters.Add(VarString.StaticTypeName, value => {
				var result = new VarSelector(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
				return Constructors[result.TypeName](new ArgumentInfo(new Variable[] { value }, Compiler.CurrentScriptTrace));
			});
			return casters;
		}

		public override string GetConstant() => String.GetConstant();
		public override RawText GetRawText() => String.GetRawText();
		public override VarString GetString() => String;

	}

}
