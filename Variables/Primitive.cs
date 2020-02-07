using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// A <see cref="Variable"/> that is stored as a scoreboard objective.
    /// </summary>
    /// <typeparam name="T">The data type that is used during <i>compilation</i>.</typeparam>
    public abstract class Primitive<T> : Variable {

        public override ICollection<string> AllowedModifiers => new string[] { "public", "private", "const" };
        public abstract string ShortTypeName { get; }
        public T InitialValue { get; }
        public abstract int InitialValueScore { get; }
        public Selector Selector { get; }
        public Objective Objective { get; }


        public Primitive() : base() { }

        public Primitive(string modifier, string objectName, string scope, T value) : base(modifier, objectName, scope) {
            InitialValue = value;
            Selector = new Selector("private", $"{objectName}.Selector", scope, "var");
            string objectiveName = $"{ShortTypeName}{Scope}{ObjectName}";
            Objective = new Objective("const", $"{objectName}.Objective", scope, objectiveName);
        }


        protected abstract T ValueFromString(string str);

        protected override void Compile(string modifier, string objectName, string scope, string[] arguments) {
            if(modifier == "") modifier = "private";
            if(arguments.Length != 2) throw new InvalidArgumentsException($"'{TypeName}' has an invalid declaration.");
            T value = ValueFromString(arguments[1]);
            Compile(modifier, objectName, scope, value);
        }

        protected abstract void Compile(string modifier, string objectName, string scope, T value);

        public override void Initialize(bool prep) {
            StreamWriter function = prep ? Compiler.PrepFunction : Compiler.FunctionStack.Peek();
            function.WriteLine($"scoreboard objectives add {Objective.Name} dummy");
            function.WriteLine($"scoreboard players set {Selector} {Objective.Name} {InitialValueScore}");
        }

        public void CopyTo(string selector, string objective) {
            StreamWriter function = Compiler.FunctionStack.Peek();
            function.WriteLine($"scoreboard players operation {selector} {objective} = {Selector} {Objective.Name}");
        }

        public void Undo() {
            StreamWriter function = Compiler.UndoFunction;
            function.WriteLine($"scoreboard objectives remove {Objective.Name}");
        }

    }

}
