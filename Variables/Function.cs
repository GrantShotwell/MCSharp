using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents a .mcfunction file.
    /// </summary>
    public class Function : Variable {

        public override ICollection<string> AllowedModifiers => new string[] { "const" };
        public override string TypeName => "Function";
        public string FolderPath { get; }
        public string GamePath { get; }
        public List<string> Commands { get; } = new List<string>();
        public string Arguments { get; }

        public Function() : base() { }

        public Function(string modifier, string objectName, string scope, string gamePath, string arguments) : base(modifier, objectName, scope) {
            GamePath = gamePath;
            FolderPath = gamePath.Replace(':', '/') + ".mcfunction";
            Arguments = arguments;
        }


        protected override void Compile(string modifier, string objectName, string scope, string[] arguments) {

        }

        public override void Initialize(bool prep) {
            if(prep) {
                //todo
            } else {
                StreamWriter function = File.CreateText(Program.Datapack.Path + "\\" + FolderPath);
                Compiler.FunctionStack.Push(function);

                _ = Compiler.FunctionStack.Pop();
            }
        }

    }

}
