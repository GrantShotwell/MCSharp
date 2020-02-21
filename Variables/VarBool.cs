using MCSharp.Compilation;
using MCSharp.Variables;
using System;

namespace MCSharp.Variables {

    public class VarBool : PrimitiveType {

        public override string TypeName => "bool";

        public VarBool() : base() { }

        public VarBool(Access access, Usage usage, string objectName, Compiler.Scope scope,
        int initValue, VarSelector selector, VarObjective objective)
        : base(access, usage, objectName, scope, initValue, selector, objective) { }
        
        public VarBool(Access access, Usage usage, string objectName, Compiler.Scope scope,
        VarSelector fromSelector, VarObjective fromObjective, VarSelector selector, VarObjective objective)
        : base(access, usage, objectName, scope, fromSelector, fromObjective, selector, objective) { }

        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            if(arguments[0] != "=") throw new Compiler.SyntaxException("Expected '='.");
            new VarBool(access, usage, objectName, scope, arguments[1] == "true" ? 1 : 0,
                new VarSelector(access, usage, NextHiddenID, scope, "var"),
                new VarObjective(access, usage, NextHiddenID, scope, "dummy"));
            return null;
        }

    }

}
