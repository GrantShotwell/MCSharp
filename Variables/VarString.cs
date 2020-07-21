using MCSharp.Compilation;
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
				var json = new LinkedList<string>();

                for(int start = -1, end = 0; end < format.Length; end++) {
                    switch(format[end]) {

                        case '{': {
							// Make string into JSON. 
							string prev = format[(start + 1)..end];
                            json.AddLast(((VarString)prev).GetJSON());
							break;
						}

                        case '}': {
                            // Make value into JSON. 
                            string prev = format[(start + 1)..end];
                            json.AddLast(Compiler.ParseValue(new ScriptLine(new ScriptString(prev)).ToWild(), Compiler.CurrentScope).GetJSON());
							break;
						}

						default: continue;
                    }

					start = end;
                }

                string[] array = new string[json.Count];
                LinkedListNode<string> node = json.First;
				for(int i = 0; node != null; i++) {
					array[i] = node.Value;
					node = node.Next;
				}
				return (VarJSON)$"[{string.Join(',', array)}]";

			});

		}


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => new VarString(access, usage, name, scope);
		public override Variable Construct(ArgumentInfo passed) => throw new Compiler.SyntaxException($"'{TypeName}' types cannot be constructed.", Compiler.CurrentScriptTrace);

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
		public override string GetJSON() => $"{{\"text\":\"{Value}\"}}";

		public static explicit operator VarString(string str) => new VarString(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope) { Value = str };
	}

}
