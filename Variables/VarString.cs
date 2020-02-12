using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Variables {

    public class VarString : Variable {

        public override int Order => 100;
        public override string TypeName => "string";
        public string Value { get; }

        public override ICollection<AccessModifier> AllowedAccessModifiers => new AccessModifier[] { AccessModifier.Public, AccessModifier.Private };
        public override ICollection<UsageModifier> AllowedUsageModifiers => new UsageModifier[] { UsageModifier.Constant };


        public VarString() : base() { }

        public VarString(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectiveName, Compiler.Scope scope, string value) :
        base(accessModifiers, usageModifiers, objectiveName, scope) => Value = value;


        protected override void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {

        }

        public override string GetConstant() => Value;

        public override void WriteInit() { }
        public override void WritePrep() { }

    }

}
