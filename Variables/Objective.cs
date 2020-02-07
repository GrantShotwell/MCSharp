using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents a Minecraft scoreboard objective.
    /// </summary>
    public class Objective : Variable {

        public override ICollection<string> AllowedModifiers => new string[] { "private", "public", "const" };
        public override string TypeName => "Objective";
        public string Name { get; }
        public string Type { get; }

        public Objective() : base() { }

        public Objective(string modifier, string objectName, string scope, string name) : base(modifier, objectName, scope) {
            if(name.Length > 16) throw new InvalidNameException(name, "too long", TypeName);
            else Name = name;
        }


        protected override void Compile(string modifier, string objectName, string scope, string[] arguments) {

        }

        public override void Initialize(bool prep) {
            StreamWriter function = Compiler.FunctionStack.Peek();
            function.WriteLine($"scoreboard objectives add {Name} {Type}");
        }

    }

}
