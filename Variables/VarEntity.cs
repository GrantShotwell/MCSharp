using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Variables {
	class VarEntity : Variable {

		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "Entity";
		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default };

		public VarSelector Selector { get; set; }


		public VarEntity() : base() { }
		public VarEntity(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) {
			AddAutoProperty(Selector = new VarSelector(Access.Public, Usage.Default, char.ToLower(VarSelector.StaticTypeName[0]) + VarSelector.StaticTypeName.Substring(1), InnerScope));
		}


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => new VarEntity(access, usage, name, scope);

		private static readonly ParameterInfo[] ConstructorOverloads = new ParameterInfo[] {
			new (Type Type, bool Reference)[] { (typeof(VarSelector), true) }
		};
		public override Variable Construct(ArgumentInfo passed) {
			(ParameterInfo match, int index) = ParameterInfo.HighestMatch(ConstructorOverloads, passed);
			match.Grab(passed);
			switch(index) {
				case 0:
					var value = new VarEntity(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
					value.Selector.InvokeOperation(Operation.Set, match[0].Value as VarSelector, Compiler.CurrentScriptTrace);
					value.Constructed = true;
					return value;

				default: throw new InvalidArgumentsException("Could not find a constructor overload that matches the given arguments.", Compiler.CurrentScriptTrace);
			}
		}

	}
}
