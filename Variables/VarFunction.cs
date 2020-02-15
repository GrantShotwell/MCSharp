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
        public string Arguments { get; }

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Constant, Usage.Static };


        public VarFunction() : base() { }

        public VarFunction(Access access, Usage usage, string objectName, Compiler.Scope scope, string gamePath, string arguments) :
        base(access, usage, objectName, scope) {
            GamePath = gamePath;
            FolderPath = gamePath.Replace(':', '/') + ".mcfunction";
            Arguments = arguments;
        }


        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            throw new NotImplementedException();
        }

        public override void WriteInit() {
            StreamWriter function = File.CreateText(Program.Datapack.Path + "\\" + FolderPath);
            Compiler.FunctionStack.Push(function);

            _ = Compiler.FunctionStack.Pop();
        }

        public override void CompileOperation(ScriptWord operation, ScriptWild[] arguments) {
            throw new NotImplementedException();
        }

        public override void WritePrep() {
            throw new NotImplementedException();
        }
    }

}
