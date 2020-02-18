using MCSharp.Compilation;
using System;
using System.Collections.Generic;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents an entity/player selector string.
    /// </summary>
    public class VarSelector : Variable {

        public override int Order => 100;
        public override string TypeName => "Selector";
        public string String { get; }

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


        public VarSelector() : base() { }

        public VarSelector(Access accessModifier, Usage usageModifier, string objectName, Compiler.Scope scope, string value) :
        base(accessModifier, usageModifier, objectName, scope) {
            String = value;
        }

        protected override Variable Compile(Access accessModifier, Usage usageModifier, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            //expected format Selector sel = "@a[team = blue, gamemode = adventure]";
            //                Selector sel = "@a[team=blue,gamemode=adventure]";
            if(arguments.Length < 2 || arguments[0].IsWilds || arguments[0].Word != "=")
                throw new Compiler.SyntaxException("Expected format of '= \"...\"'.");
            string value = ((string)new ScriptWild(arguments[1..], " \\ ", ' ')).Trim();
            if(value[0] != '\"' || value[^1] != '\"') throw new Compiler.SyntaxException("Expected a string for declaring a Selector.");
            value = value[1..^1];
            return new VarSelector(accessModifier, usageModifier, objectName, scope, value);
        }

        public override string GetConstant() => String;

    }

}
