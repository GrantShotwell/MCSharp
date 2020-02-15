using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Variables {

    public class VarJSON : Variable {

        public override int Order => 100;
        public override string TypeName => "JSON";

        public string Value { get; }

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant };


        public VarJSON() : base() { }

        public VarJSON(Access access, Usage usage, string objectName, Compiler.Scope scope, string value) :
        base(access, usage, objectName, scope) {
            Value = value;
        }


        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            if(arguments.Length < 2 || arguments[0].IsWilds || arguments[0].Word != "=")
                throw new Compiler.SyntaxException("Expected format of '= \"...\"'.");
            StreamReader reader = File.OpenText(Program.ScriptsFolder + "\\" + VarString.CompileStringValue(arguments[1..]));
            string json = reader.ReadToEnd().Replace('\n', ' ').Replace('\t', ' ').Replace('\r', ' ');
            reader.Close();
            return new VarJSON(access, usage, objectName, scope, json);
        }

        public override void CompileOperation(ScriptWord operation, ScriptWild[] arguments) {
            throw new NotImplementedException();
        }

        public override string GetConstant() => Value;

    }

}
