using MCSharp.Variables;
using System;
using System.Collections.Generic;

namespace MCSharp.Compilation {

	public abstract class ScriptMember {

		public string Alias { get; }
		public string FullAlias => DeclaringType is null ? Alias : $"{DeclaringType.FullAlias}.{Alias}";
		public string PartialAlias => DeclaringType is null ? Alias : $"{DeclaringType.Alias}.{Alias}";
		public string FileName => Script.FixAlias($"{FullAlias.Replace('.', '\\')}.mcfunction");
		public string FilePath => $"{Program.FunctionsFolder}\\{FileName}";
		public string GameName => $"{Program.Datapack.Name}:{Script.FixAlias(FullAlias.Replace('.', '/'))}";
		public string TypeName { get; }
		public virtual ScriptClass DeclaringType { get; set; }
		public Access Access { get; }
		public Usage Usage { get; }
		public ScriptTrace ScriptTrace { get; }

		public ScriptMember(string alias, string type, Access access, Usage usage, ScriptClass declarer, ScriptTrace trace) {
			DeclaringType = declarer;
			ScriptTrace = trace ?? throw new ArgumentNullException(nameof(trace));
			TypeName = type ?? throw new ArgumentNullException(nameof(type));
			Access = access;
			Usage = usage;
			Alias = alias ?? throw new ArgumentNullException(nameof(alias));
		}

	}

	public static class ScriptMemberExtensions {
		public static void Add(this Dictionary<string, ScriptMember> dictionary, ScriptMember member) => dictionary.Add(member.Alias, member);
	}

}
