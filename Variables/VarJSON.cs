using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

	public class VarJSON : Variable {

		public override string TypeName => "JSON";

		public string Value { get; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant };


		public VarJSON() : base() { }

		public VarJSON(Access access, Usage usage, string objectName, Compiler.Scope scope, string value) :
		base(access, usage, objectName, scope) {
			Value = value;
		}


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) {
			base.Initialize(access, usage, name, scope, trace);

			throw new NotImplementedException();

		}

		public override string GetConstant() => Value;

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
