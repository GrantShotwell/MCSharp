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

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


        public VarEntities() : base() { }

        public VarEntities(Access access, Usage usage, string objectName, Compiler.Scope scope, VarSelector selector) :
        base(access, usage, objectName, scope) {
            Selector = selector.String;
        }


        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            return new VarEntities(access, usage, objectName, scope,
                new VarSelector(access, usage, $"{objectName}.Selector", scope, arguments[1].Word));
        }

        public override void WriteInit(StreamWriter function) {
            function.WriteLine($"tag {(Selector.StartsWith("@e") ? "@e" : "@a")} remove var_{TypeName}_{ObjectName}");
            function.WriteLine($"tag {Selector} add var_{TypeName}_{ObjectName}");
        }

        public override void WritePrep(StreamWriter function) {
            function.WriteLine($"tag {(Selector.StartsWith("@e") ? "@e" : "@a")} remove var_{TypeName}_{ObjectName}");
            function.WriteLine($"tag {Selector} add var_{TypeName}_{ObjectName}");
        }

    }

}
