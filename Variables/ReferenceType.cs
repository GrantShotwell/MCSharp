using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Variables {

    public abstract class ReferenceType : Variable {

        public VarSelector ObjectEntity { get; }

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Constant, Usage.Static, Usage.Default };

        public ReferenceType() : base() { }
        public ReferenceType(Access access, Usage usage, string objectName, Compiler.Scope scope) : base(access, usage, objectName, scope) { }

    }

}
