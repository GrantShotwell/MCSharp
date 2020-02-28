using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents a .mcfunction file.
    /// </summary>
    public class VarFunction : Variable {

        public override int Order => 100;
        public override string TypeName => "Function";
        public string FolderPath { get; }
        public string GamePath { get; }
        public List<string> Commands { get; } = new List<string>();
        public IReadOnlyList<Variable> Parameters { get; private set; }
        public ScriptFunction ScriptFunction { get; }
        public Compiler.Scope FunctionScope { get; private set; }

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


        public VarFunction() : base() { }

        public VarFunction(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptFunction function, params Variable[] parameters) :
        base(access, usage, objectName, scope) {
            GamePath = $"{Program.Datapack.Name}:{function.Alias.Replace('\\', '/')}";
            FolderPath = $"{Program.Datapack.Name}:{function.AliasDotted}.mcfunction";
            Parameters = parameters;
            ScriptFunction = function;
            Methods.Add("Invoke", (args) => {
                if(args.Length != Parameters.Count)
                    throw new InvalidArgumentsException($"Wrong number of arguments for '{this}'.Invoke(_).", Compiler.CurrentScriptTrace);
                new Spy(null, (function) => {
                    for(int i = 0; i < args.Length; i++) args[i].WriteCopyTo(Compiler.FunctionStack.Peek(), Parameters[i]);
                    function.WriteLine($"function {GamePath}");
                }, null);
                return null;
            });
        }


        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            if(arguments.Length != 4 || arguments[0].IsWilds || arguments[1].IsWord || arguments[2].IsWilds || arguments[3].IsWord)
                throw new Compiler.SyntaxException($"Expected format of '= (...) => {{...}};' for creating '{TypeName}'.", arguments[0].ScriptTrace);
            string str = arguments[1];
            string[] split = str[1..^1].Split(',');
            Variable[] parameters = new Variable[split.Length];
            for(int i = 0; i < split.Length; i++) {
                string arg = split[i];
                string[] argargs = arg.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if(argargs.Length != 2) throw new Compiler.SyntaxException("Expected '[type] [name]'.", arguments[0].ScriptTrace);
                parameters[i] = new Parameter(argargs[0], argargs[1]).GetVariable(scope);
            }
            return new VarFunction(access, usage, objectName, scope, new ScriptFunction($"{scope}\\{objectName}\\Invoke", arguments[3]), parameters);
        }

        public override void WriteInit(StreamWriter function) => Compiler.WriteFunction(Scope, ScriptFunction);

        public struct Parameter {
            readonly string type, name;
            public Parameter(string type, string name) {
                this.type = type;
                this.name = name;
            }
            public Variable GetVariable(Compiler.Scope scope) {
                if(Compilers.TryGetValue(type, out var compiler)) {
                    return compiler.Invoke(Access.Private, Usage.Default, name, scope, new ScriptWild[] { });
                } else throw new Compiler.SyntaxException($"Unknown type '{type}'.", Compiler.CurrentScriptTrace);
            }
        }

    }

}
