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


        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            switch(usage) {
                case Usage.Constant:
                case Usage.Default:
                case Usage.Static:
                    //expected format Selector sel = "...";
                    if(arguments.Length < 2 || arguments[0].IsWilds || (string)arguments[0].Word != "=")
                        throw new Compiler.SyntaxException("Expected format of '= \"...\"'.", arguments[0].ScriptTrace);
                    string value = ((string)new ScriptWild(arguments[1..], " \\ ", ' ')).Trim();
                    if(value[0] != '\"' || value[^1] != '\"') throw new Compiler.SyntaxException("Expected a string for declaring a Selector.", arguments[0].ScriptTrace);
                    value = value[1..^1];
                    return new VarSelector(access, usage, objectName, scope, value);
                default: throw new ArgumentException($"The given value is not a '{nameof(Variables.Usage)}' value.", nameof(usage));
            }
        }

        public override string GetConstant() => str;
        public override string GetJSON() => $"{{\"text\":\"{str}\"}}";

    }

}
