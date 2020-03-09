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
            if(arguments[0] != "=") throw new Compiler.SyntaxException("Expected '='.", arguments[0].ScriptTrace);
            var args = arguments[1..];
            if(Compiler.TryParseValue(args.Length > 1 ? new ScriptWild(args, " \\ ", ' ') : args[0], Compiler.CurrentScope, out Variable variable)) {
                if(variable is VarBool varBool || variable.TryCast(out varBool)) {
                    return new VarBool(access, usage, objectName, scope, varBool.Selector, varBool.Objective,
                        new VarSelector(access, usage, GetNextHiddenID(), scope, "var"),
                        new VarObjective(access, usage, GetNextHiddenID(), scope, "dummy"));
                } else throw new Compiler.SyntaxException($"Could not cast '{variable}' as 'bool'.", arguments[0].ScriptTrace);
            } else throw new Compiler.SyntaxException("Could not interpret as 'bool'.", arguments[0].ScriptTrace);
        }

        public override Variable Operation(ScriptWord operation, ScriptWild[] args) {
            switch((string)operation) {
                case "=": {
                    if(Compiler.TryParseValue(new ScriptWild(args, " \\ ", ' '), Compiler.CurrentScope, out Variable variable)
                    && (variable is PrimitiveType primitive || variable.TryCast(out primitive))) {
                        new Spy(null, $"scoreboard players operation var {Objective.GetConstant()} = var {primitive.Objective.GetConstant()}", null);
                        return this;
                    } else {
                        if(variable == null) throw new InvalidArgumentsException($"Unknown how to determine value as '{TypeName}'.", operation.ScriptTrace);
                        else throw new InvalidCastException(variable, TypeName, operation.ScriptTrace);
                    }
                }
                case "!": {
                    if(args.Length != 0) throw new InvalidArgumentsException("022502282020", operation.ScriptTrace);
                    string id = GetNextHiddenID();
                    //Create temp variable from this.
                    var anon = new VarBool(Access.Private, Usage.Default, id, Compiler.CurrentScope, Selector, Objective,
                        new VarSelector(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope, "var"),
                        new VarObjective(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope, "dummy"));
                    //Write function: 'set anon to the opposite of this'.
                    var function = new ScriptMethod($"{Compiler.CurrentScope}\\{Compiler.CurrentScope.GetNextInnerID()}",
                        "void", new Variable[] { }, Compiler.CurrentScope.DeclaringType,
                        new ScriptString($"if({anon.ObjectName}) {{ {anon.ObjectName} = false; }} else {{ {anon.ObjectName} = true; }}"));
                    Compiler.WriteFunction<VarVoid>(Compiler.CurrentScope, function);
                    //Write command: 'run that function'.
                    new Spy(null, $"function {function.GameName}", null);
                    return anon;
                }
                case "&&": {
                    //Create temp variable from this.
                    var anon = new VarBool(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope, Selector, Objective,
                        new VarSelector(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope, "var"),
                        new VarObjective(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope, "dummy"));
                    //Write function: 'if anon is true, set anon to args'.
                    var function = new ScriptMethod($"{Compiler.CurrentScope}\\{Compiler.CurrentScope.GetNextInnerID()}",
                        "void", new Variable[] { }, Compiler.CurrentScope.DeclaringType,
                        new ScriptString($"if({anon.ObjectName}) {{ {anon.ObjectName} = {(string)new ScriptWild(args, "(\\)", ' ')}; }}"));
                    Compiler.WriteFunction<VarVoid>(Compiler.CurrentScope, function);
                    //Write command: 'run that function'.
                    new Spy(null, $"function {function.GameName}", null);
                    return anon;
                }
                default: return base.Operation(operation, args);
            }
        }

    }

}
