using MCSharp.Variables;
using System;
using System.Collections.Generic;

namespace MCSharp.Compilation {

    public abstract class ScriptMember {

        public string Alias { get; }
        public string AliasDotted => Alias.Replace('\\', '.');
        public ScriptClass DeclaringType { get; }
        public Access Access { get; }
        public Usage Usage { get; }
        public ScriptTrace ScriptTrace { get; }

        public ScriptMember(string alias, Access access, Usage usage, ScriptClass declaringType, ScriptTrace scriptTrace) {
            DeclaringType = declaringType;
            ScriptTrace = scriptTrace;
            Alias = alias;
            Access = access;
            Usage = usage;
        }

    }

    public static class ScriptMemberExtensions {
        public static void Add(this Dictionary<string, ScriptMember> dictionary, ScriptFunction function) => dictionary.Add(function.Alias, function);
    }

}
