using MCSharp.Compilation;
using MCSharp.GameJSON.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MCSharp.Variables {

    public class VarJson : Variable {

		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "Json";

		private RawText RawText { get; set; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant };


		public VarJson() : base() { }
		public VarJson(Access access, Usage usage, string objectName, Compiler.Scope scope) : base(access, usage, objectName, scope) { }

		public static implicit operator VarJson(RawText raw) => new VarJson(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope) {
			RawText = raw
		};
		public static explicit operator VarJson(RawTextList raw) => new VarJson(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope) {
			RawText = new RawText() { Text = "", Extra = raw.ToArray() }
		};

		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => throw new NotImplementedException();

        public override Variable Construct(ArgumentInfo arguments) {
			ParameterInfo[] description = new ParameterInfo[] {
				new (Type, bool)[] { (typeof(VarString), true) }
			};

            (ParameterInfo match, int index) = ParameterInfo.HighestMatch(description, arguments);
			match.Grab(arguments);
			
			switch(index) {

				case 0:
					string value = (match[0].Value as VarString).GetConstant();
					VarJson json = new VarJson(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
					json.SetValue(value);
					return json;

				default: throw new MissingOverloadException($"{TypeName} constructor", index, arguments);
            }

        }

        public void SetValue(string value) => RawText = RawText.FromJson(value);

		public override RawText GetRawText() => RawText;

		public static string EscapeValue(string value, int escapes) {
			var original = new LinkedList<char>(value);
			var escaped = new LinkedList<char>();

			foreach(char character in original) {
				if(character == '"' || character == '\\') {
					for(int esc = escapes; esc >= 0; esc--)
						escaped.AddLast('\\');
				}
				escaped.AddLast(character);
			}

			char[] array = new char[escaped.Count];
			original.CopyTo(array, 0);
			return new string(array);
		}

	}

}
