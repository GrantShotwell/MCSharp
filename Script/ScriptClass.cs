using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MCSharp.Script {

	public struct ScriptClass {

		public static Regex NameRegex { get; } = new Regex(@"[a-zA-Z_][a-zA-Z0-9_]*");
		public static IReadOnlyCollection<string> Modifiers { get; } = new string[] {
			"public",
			"private",
			"static"
		};
		public static IReadOnlyCollection<string> ClassTypes { get; } = new string[] {
			"class",
			"struct"
		};

		public IReadOnlyCollection<ScriptToken> Prefixes { get; }
		public ScriptToken Type { get; }
		public ScriptToken Name { get; }
		public IReadOnlyCollection<ScriptMember> Members { get; }

		public ScriptTrace Trace => Name.Trace;


		private ScriptClass(LinkedList<ScriptToken> prefixes, ScriptToken type, ScriptToken name, LinkedList<ScriptMember> members) {
			Type = type;
			Name = name;
			Prefixes = prefixes?.ToArray() ?? throw new ArgumentNullException(nameof(prefixes));
			Members = members?.ToArray() ?? throw new ArgumentNullException(nameof(members));
		}

		/// <summary>
		/// [prefixes] [name] "{" {script_member} "}"
		/// </summary>
		public static bool ReadClass(ref TokenReader reader, out string message, out ScriptClass? result) {
			result = null;

			// [prefixes]
			if(!ReadPrefixes(ref reader, out message, out LinkedList<ScriptToken> prefixes, out ScriptToken? type)) {
				return false;
			}

			// [name]
			if(!ReadName(ref reader, out message, out ScriptToken? name)) {
				return false;
			}

			// "{"
			if(!reader.ReadNextToken("{", out message, out _)) {
				return false;
			}

			// {script_member}
			LinkedList<ScriptMember> members = new LinkedList<ScriptMember>();
			while(!reader.LookAhead("}")) {
				TokenReader branch = reader.Branch();
				if(!ScriptMember.ReadMember(ref branch, out message, out ScriptMember? member)) return false;
				members.AddLast(member.Value);
				reader = branch;
			}

			// "}"
			if(!reader.ReadNextToken("}", out message, out _)) {
				return false;
			}

			// Success.
			result = new ScriptClass(prefixes, type.Value, name.Value, members);
			return true;

		}

		/// <summary>
		/// modifiers class_type
		/// </summary>
		private static bool ReadPrefixes(ref TokenReader reader, out string message, out LinkedList<ScriptToken> prefixes, out ScriptToken? type) {

			// {modifier}
			prefixes = new LinkedList<ScriptToken>();
			while(true) {
				TokenReader branch = reader.Branch();
				if(!branch.MoveNext()) break;
				if(!Modifiers.Contains((string)branch.Current)) break;
				prefixes.AddLast(branch.Current);
				reader = branch;
			}

			// class_type
			if(!reader.MoveNext()) {
				type = null;
				message = $"After {reader.Current}: Expected a class type but found end of file.";
				return false;
			} else if(ClassTypes.Contains((string)reader.Current)) {
				type = reader.Current;
				message = Compiler.DefaultSuccess;
				return true;
			} else {
				type = null;
				message = $"{reader.Current.Trace}: Expected a class type but found '{reader.Current}'.";
				return false;
			}

		}

		/// <summary>
		/// class_name
		/// </summary>
		private static bool ReadName(ref TokenReader reader, out string message, out ScriptToken? result) {

			if(!reader.MoveNext()) {
				result = null;
				message = $"After {reader.Current}: Expected a class definition name but found end of file.";
				return false;
			} else {
				result = reader.Current;
				message = Compiler.DefaultSuccess;
				return true;
			}
		}

	}

}
