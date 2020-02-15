using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Variables {

    public class VarString : Variable {

        public override int Order => 100;
        public override string TypeName => "string";
        public string ConstantValue { get; }
        public VarSelector SelectorValue { get; }
        public bool IsConstant => ConstantValue != null;
        public bool IsSelector => SelectorValue != null;

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant };


        public VarString() : base() { }

        public VarString(Access access, string objectName, Compiler.Scope scope, string value) : base(access, Usage.Constant, objectName, scope) {
            ConstantValue = value;
        }

        public VarString(Access access, string objectName, Compiler.Scope scope, VarSelector value) : base(access, Usage.Constant, objectName, scope) {
            SelectorValue = value;
        }

        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            if(arguments.Length < 2 || arguments[0].IsWilds || arguments[0].Word != "=")
                throw new Compiler.SyntaxException("Unexpected format for declaring a 'string'.");
            if(((string)arguments[1])[0] == '"' && usage == Usage.Default) {
                string value = CompileStringValue(arguments[1..]);
                return new VarString(access, objectName, scope, value);
            } else if(Compiler.TryGetVariable(new ScriptWild(arguments[1..], null), scope, out Variable variable)
                && (variable is VarSelector selector || variable.TryCast(out selector))) {
                return new VarString(access, objectName, scope, selector);
            } else throw new Compiler.SyntaxException("Unexpected 'string' declaration.");
        }

        public static string CompileStringValue(ScriptWild[] arguments) {
            string value = ((string)new ScriptWild(arguments, "\"\\\"")).Trim();
            if(value[0] != '\"' || value[^1] != '\"') throw new Compiler.SyntaxException("Expected a string for declaring a string.");
            return value[1..^1];
        }

        public override void CompileOperation(ScriptWord operation, ScriptWild[] arguments) {
            throw new NotImplementedException();
        }

        public override string GetConstant() => ConstantValue;
        public override VarString GetString() => this;

    }

}
