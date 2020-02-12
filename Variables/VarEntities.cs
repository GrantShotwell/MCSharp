using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents a set of Minecraft entities that were selected upon creation of this object.
    /// </summary>
    public class VarEntities : Variable {

        public override int Order => 100;
        public override string TypeName => "Entity";
        public string Selector { get; }

        public override ICollection<AccessModifier> AllowedAccessModifiers => new AccessModifier[] { AccessModifier.Private, AccessModifier.Public };
        public override ICollection<UsageModifier> AllowedUsageModifiers => new UsageModifier[] { UsageModifier.Constant, UsageModifier.Static };


        public VarEntities() : base() { }

        public VarEntities(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, VarSelector selector) :
        base(accessModifiers, usageModifiers, objectName, scope) {
            Selector = selector.String;
        }


        protected override void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            throw new NotImplementedException();
        }

        public override void WriteInit() {
            StreamWriter function = Compiler.FunctionStack.Peek();
            function.WriteLine($"tag {(Selector.StartsWith("@e") ? "@e" : "@a")} remove var_{TypeName}_{ObjectName}");
            function.WriteLine($"tag {Selector} add var_{TypeName}_{ObjectName}");
        }

        public override void WritePrep() {
            StreamWriter function = Compiler.PrepFunction;
            function.WriteLine($"tag {(Selector.StartsWith("@e") ? "@e" : "@a")} remove var_{TypeName}_{ObjectName}");
            function.WriteLine($"tag {Selector} add var_{TypeName}_{ObjectName}");
        }
    }

}
