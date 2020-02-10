using MCSharp.Compilation;
using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// A <see cref="Variable"/> that is stored as a scoreboard objective.
    /// </summary>
    public abstract class Primitive : Variable {

        public override int Order => 100;
        public abstract string ShortTypeName { get; }
        public abstract int InitialValueScore { get; }
        public Selector Selector { get; }
        public Objective Objective { get; }

        public override ICollection<AccessModifier> AllowedAccessModifiers => new AccessModifier[] { AccessModifier.Private, AccessModifier.Public };
        public override ICollection<UsageModifier> AllowedUsageModifiers => new UsageModifier[] { UsageModifier.Constant, UsageModifier.Static };


        public Primitive() : base() { }

        public Primitive(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope) : base(accessModifiers, usageModifiers, objectName, scope) {
            Selector = new Selector(new AccessModifier[] { AccessModifier.Private },
                                    new UsageModifier[] { UsageModifier.Constant },
                                    $"{objectName}.Selector", scope, "var");
            Objective = new Objective(new AccessModifier[] { AccessModifier.Private },
                                      new UsageModifier[] { UsageModifier.Constant },
                                      $"{objectName}.Objective", scope, "dummy");
        }


        public override void Initialize(bool prep) {
            StreamWriter function = prep ? Compiler.PrepFunction : Compiler.FunctionStack.Peek();
            function.WriteLine($"scoreboard players set {Selector.String} {Objective.ID} {InitialValueScore}");
        }

        public void CopyTo(Selector selector, Objective objective) {
            StreamWriter function = Compiler.FunctionStack.Peek();
            function.WriteLine($"scoreboard players operation {selector.String} {objective.ID} = {Selector.String} {Objective.ID}");
        }

        public void CopyFrom(Selector selector, Objective objective) {
            StreamWriter function = Compiler.FunctionStack.Peek();
            function.WriteLine($"scoreboard players operation {Selector.String} {Objective.ID} = {selector.String} {objective.ID}");
        }

        public void Undo() {
            StreamWriter function = Compiler.UndoFunction;
            function.WriteLine($"scoreboard objectives remove {Objective.ID}");
        }

    }

    /// <summary>
    /// A <see cref="Variable"/> that is stored as a scoreboard objective.
    /// </summary>
    /// <typeparam name="T">The data type that is used during <i>compilation</i>.</typeparam>
    public abstract class Primitive<T> : Primitive {

        public T InitialValue { get; }


        public Primitive() : base() { }

        public Primitive(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, T value) :
        base(accessModifiers, usageModifiers, objectName, scope) {
            InitialValue = value;
        }


        protected abstract T ValueFromString(string str);

        protected override void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, Wild[] arguments) {
            if(arguments.Length != 2) throw new InvalidArgumentsException($"'{TypeName}' has an invalid declaration.");
            T value = ValueFromString(arguments[1]);
            Compile(accessModifiers, usageModifiers, objectName, scope, value);
        }

        protected abstract void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, T value);

    }

}
