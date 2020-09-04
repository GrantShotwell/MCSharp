using MCSharp.Compilation;
using MCSharp.GameJSON.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;

namespace MCSharp.Variables {

	public class VarString : Variable {

		public override int Order => 100;
		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "string";
		private string Value { get; set; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant };


		public VarString() : base() { }
		public VarString(Access access, Usage usage, string name, Compiler.Scope scope) : base(access, usage, name, scope) {

			ParameterInfo[] FormatInfo = new ParameterInfo[] {
				new (Type, bool)[] { }
			};
            Methods.Add("Format", arguments => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(FormatInfo, arguments);
				match.Grab(arguments);

				string format = Value;

				int est = 1;
				char last = '\0';
				foreach(char character in format) if(last != character && last == '{') est++;
				RawTextList json = new RawTextList(est);

				int start, end;
                for(start = (end = 0) - 1; end < format.Length; end++) {
                    switch(format[end]) {

                        case '{': {
							// Make string into JSON. 
							string prev = format[(start + 1)..end];
							var raw = new RawText() { Text = prev };
							json.Add(raw);
							break;
						}

                        case '}': {
                            // Make value into JSON.
                            string prev = format[(start + 1)..end];
							var value = Compiler.ParseValue(new ScriptLine(new ScriptString(prev)).ToWild(), Compiler.CurrentScope);
							var copy = Initializers[value.TypeName](Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope, Compiler.CurrentScriptTrace);
							copy.InvokeOperation(Operation.Set, value, Compiler.CurrentScriptTrace);
							json.Add(copy.GetRawText());
							break;
						}

						default: continue;
                    }

					// Only not skipped if broken out of switch statement.
					start = end;

                }

				if(end - start > 0) {
					// Add last segment into json (string).
					json.Add(((VarString)format[(start + 1)..end]).GetRawText());
				}

				return (VarJson)json;

			});

		}


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => new VarString(access, usage, name, scope);
		public override Variable Construct(ArgumentInfo passed) => throw new Compiler.SyntaxException($"'{TypeName}' types cannot be constructed.", Compiler.CurrentScriptTrace);
		public override void ConstructAsPasser() => throw new NotImplementedException();

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {

			switch(operation) {

				case Operation.Set:
					if(operand is VarString varString1 || operand.TryCast(out varString1)) {
						Value = varString1.Value;
						return this;
					} else throw new InvalidCastException(operand, StaticTypeName, trace);

				case Operation.Add:
					if(operand is VarString varString2 || operand.TryCast(out varString2)) {
						Value += varString2.Value;
						return this;
					} else throw new InvalidCastException(operand, StaticTypeName, trace);

				default: return base.InvokeOperation(operation, operand, trace);
			}
		}

		public void SetValue(string value) => Value = value;

		public override string GetConstant() => Value;
		public override VarString GetString() => this;
		public override RawText GetRawText() => new RawText() { Text = Value };

		public static explicit operator VarString(string str) => new VarString(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope) { Value = str };
	}

}
