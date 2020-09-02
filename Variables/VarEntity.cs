using MCSharp.Compilation;
using MCSharp.GameJSON.Text;
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

			AddAutoProperty(Selector = new VarSelector(Access.Public, Usage.Default, NamingStyleConverter.FromSingleToCamel(VarSelector.StaticTypeName), InnerScope));

			ParameterInfo[] MergeDataInfo = new ParameterInfo[] {
				new (Type, bool)[] { (typeof(VarString), true), (typeof(VarString), true) }
			};
			Methods.Add("MergeData", (arguments) => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(MergeDataInfo, arguments);
				match.Grab(arguments);

				switch(index) {
					case 0:
						VarString name = match[0].Value as VarString;
						VarString data = match[1].Value as VarString;
						new Spy(null, $"data merge entity {Selector.GetConstant()} {{{name.GetConstant()}:{data.GetConstant()}}}", null);
						return null;

					default: throw new MissingOverloadException("MergeData", index, arguments);
				}

			});

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

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			switch(operation) {

				case Operation.Set: {
					if(operand is VarEntity entity || operand.TryCast(out entity)) {
						Selector = entity.Selector;
						Constructed = true;
						return this;
					} else {
						throw new InvalidCastException(operand, StaticTypeName, trace);
					}
				}

				default: return base.InvokeOperation(operation, operand, trace);
			}
		}

		public override RawText GetRawText() => new RawText() {
			Entity = Selector.GetConstant()
		};

	}
}
