using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Variables {
    class VarVoid : Variable {

        public override string TypeName => "void";

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Abstract, Usage.Default, Usage.Static, Usage.Constant };

        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) => throw new NotImplementedException();

    }
}
