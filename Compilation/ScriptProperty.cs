using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

    public class ScriptProperty : ScriptMember {

        private readonly string str;

        public string Type { get; }
        public ScriptFunction Get { get; }
        public ScriptFunction Set { get; }

        public ScriptProperty(string alias, string type, ScriptFunction get_, ScriptFunction set_,
            Access access, Usage usage, ScriptClass declaringType, ScriptTrace trace) :
            base(alias, access, usage, declaringType, trace) {

            Type = type;
            Get = get_;
            Set = set_;

        }

    }

}
