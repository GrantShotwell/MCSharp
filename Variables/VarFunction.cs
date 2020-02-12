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

        public override ICollection<AccessModifier> AllowedAccessModifiers => new AccessModifier[] { AccessModifier.Private, AccessModifier.Public };
        public override ICollection<UsageModifier> AllowedUsageModifiers => new UsageModifier[] { UsageModifier.Constant, UsageModifier.Static };


        public VarFunction() : base() { }

        public VarFunction(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, string gamePath, string arguments) :
        base(accessModifiers, usageModifiers, objectName, scope) {
            GamePath = gamePath;
            FolderPath = gamePath.Replace(':', '/') + ".mcfunction";
            Arguments = arguments;
        }


        protected override void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            throw new NotImplementedException();
        }

        public override void WriteInit() {
            StreamWriter function = File.CreateText(Program.Datapack.Path + "\\" + FolderPath);
            Compiler.FunctionStack.Push(function);
            //todo
            _ = Compiler.FunctionStack.Pop();
        }

        public override void WritePrep() {
            throw new NotImplementedException();
        }
    }

}
