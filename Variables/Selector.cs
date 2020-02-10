using MCSharp.Compilation;
using System;
using System.Collections.Generic;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents an entity/player selector string.
    /// </summary>
    public class Selector : Variable {

        public override int Order => 100;
        public override string TypeName => "Selector";
        public string String { get; }

        public override ICollection<AccessModifier> AllowedAccessModifiers => new AccessModifier[] { AccessModifier.Private, AccessModifier.Public };
        public override ICollection<UsageModifier> AllowedUsageModifiers => new UsageModifier[] { UsageModifier.Constant, UsageModifier.Static };


        public Selector() : base() { }

        public Selector(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, string value) :
        base(accessModifiers, usageModifiers, objectName, scope) {
            String = value;
        }

        protected override void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, Wild[] arguments) {
            throw new NotImplementedException();
        }

        public override void Initialize(bool prep) { }

    }

}
