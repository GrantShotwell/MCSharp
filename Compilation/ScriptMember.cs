using MCSharp.Variables;
using System;
using System.Collections.Generic;

namespace MCSharp.Compilation {

	public abstract class ScriptMember {

		public string Alias { get; }
		public string FullAlias => DeclaringType is null ? Alias : $"{DeclaringType.FullAlias}.{Alias}";
		public string PartialAlias => DeclaringType is null ? Alias : $"{DeclaringType.Alias}.{Alias}";
		public string FileName { get; }
		public string FilePath { get; }
		public string GameName { get; }
		public string TypeName { get; }
		public virtual ScriptClass DeclaringType { get; set; }
		public Access Access { get; }
		public Usage Usage { get; }
		public ScriptTrace ScriptTrace { get; }

		public ScriptMember(string alias, string type, Access access, Usage usage, ScriptClass declaringType, ScriptTrace scriptTrace) {
			DeclaringType = declaringType;
			ScriptTrace = scriptTrace ?? throw new ArgumentNullException(nameof(scriptTrace));
			TypeName = type ?? throw new ArgumentNullException(nameof(type));
			Access = access;
			Usage = usage;
			Alias = alias?.Split('\\')[^1] ?? throw new ArgumentNullException(nameof(alias));
			FileName = Script.FixAlias($"{alias}.mcfunction");
			FilePath = $"{Program.FunctionsFolder}\\{FileName}";
			GameName = $"{Program.Datapack.Name}:{Script.FixAlias(alias).Replace('\\', '/')}";
		}

	}

	public static class ScriptMemberExtensions {
		public static void Add(this Dictionary<string, ScriptMember> dictionary, ScriptMember member) => dictionary.Add(member.Alias, member);
	}

}
