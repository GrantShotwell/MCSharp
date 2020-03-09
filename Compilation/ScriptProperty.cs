using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MCSharp.Compilation {

    public class ScriptProperty : ScriptMember {

        public ScriptMethod Get { get; }
        public ScriptMethod Set { get; }

        public ScriptProperty(string alias, string type, ScriptMethod get_, ScriptMethod set_,
            Access access, Usage usage, ScriptClass declaringType, ScriptTrace trace) :
            base(alias, type, access, usage, declaringType, trace) {

            Get = get_ ?? throw new ArgumentNullException(nameof(get_));
            Set = set_ ?? throw new ArgumentNullException(nameof(set_));

        }

    }

}
