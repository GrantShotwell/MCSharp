﻿using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace MCSharp.Variables {

    public class VarJSON : Variable {

		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "JSON";

		private string Value { get; set; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant };


		public VarJSON() : base() { }
		public VarJSON(Access access, Usage usage, string objectName, Compiler.Scope scope) : base(access, usage, objectName, scope) { }

		public static explicit operator VarJSON(string str) => new VarJSON(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope) { Value = str };


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => throw new NotImplementedException();

        public override Variable Construct(ArgumentInfo arguments) {
			ParameterInfo[] description = new ParameterInfo[] {
				new (Type, bool)[] { (typeof(VarString), true) }
			};

            (ParameterInfo match, int index) = ParameterInfo.HighestMatch(description, arguments);
			match.Grab(arguments);
			
			string value;
			switch(index) {

				case 0:
					value = (match[0].Value as VarString).GetConstant();
					goto Construct;

					Construct:
					VarJSON json = new VarJSON(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
					json.SetValue(value);
					return json;

				default: throw new MissingOverloadException($"{TypeName} constructor", index, arguments);
            }

        }

        public void SetValue(string value) => Value = value;

		public override string GetConstant() => Value;
        public override string GetJSON() => Value;

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
