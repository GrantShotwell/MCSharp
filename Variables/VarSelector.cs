using MCSharp.Compilation;
using System;
using System.Collections.Generic;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents an entity/player selector string.
    /// </summary>
    public class VarSelector : Variable {

        private string str;

        public override string TypeName => "Selector";

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


        public VarSelector() : base() { }
        public VarSelector(Access accessModifier, Usage usageModifier, string objectName, Compiler.Scope scope, string value) :
        base(accessModifier, usageModifier, objectName, scope) {
            str = value;
        }


        protected override Variable Compile(Access accessModifier, Usage usageModifier, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            //expected format Selector sel = "...";
            if(arguments.Length < 2 || arguments[0].IsWilds || arguments[0].Word != "=")
                throw new Compiler.SyntaxException("Expected format of '= \"...\"'.");
            string value = ((string)new ScriptWild(arguments[1..], " \\ ", ' ')).Trim();
            if(value[0] != '\"' || value[^1] != '\"') throw new Compiler.SyntaxException("Expected a string for declaring a Selector.");
            value = value[1..^1];
            return new VarSelector(accessModifier, usageModifier, objectName, scope, value);
        }

        public override string GetConstant() => str;
        public override string GetJSON() => $"{{\"text\":\"{str}\"}}";

    }

}
