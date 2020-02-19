using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Variables {

    public class VarTitles : Variable {

        public override int Order => 100;
        public override string TypeName => "Titles";

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Static };

        public VarTitles() : base() {
            Compiler.StaticClassObjects.Add("Titles", new VarTitles("Titles"));
        }

        public VarTitles(string name) : base(Access.Public, Usage.Static, name, Compiler.RootScope) {
            Methods.Add("ShowTitle", (args) => Show("title", args));
            Methods.Add("ShowSubtitle", (args) => Show("subtitle", args));
            Methods.Add("ShowActionbar", (args) => Show("actionbar", args));
        }

        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            throw new Compiler.SyntaxException("Cannot create an instance of a static class!");
        }

        private Variable Show(string type, Variable[] args) {
            if(args.Length != 2) throw new InvalidArgumentsException($"Invalid number of arguments for '{TypeName}' method.");
            if(!(args[0] is VarSelector selector) && !args[0].TryCast(out selector))
                throw new InvalidCastException(args[0], "Selector");
            if(!(args[1] is VarJSON json) && !args[1].TryCast(out json))
                throw new InvalidCastException(args[1], "JSON");
            new Spy(null, new string[] { $"title {selector.GetConstant()} {type} {json.GetConstant()}" }, null);
            return null;
        }

    }

}
