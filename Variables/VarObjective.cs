using LargeBaseNumbers;
using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents a Minecraft scoreboard objective.
    /// </summary>
    public class VarObjective : Variable {

        public static int NextID { get; private set; }

        public override int Order => 50;
        public override string TypeName => "Objective";
        public string ID { get; }
        public string Type { get; }

        public override ICollection<AccessModifier> AllowedAccessModifiers => new AccessModifier[] { AccessModifier.Private, AccessModifier.Public };
        public override ICollection<UsageModifier> AllowedUsageModifiers => new UsageModifier[] { UsageModifier.Constant, UsageModifier.Static };


        public VarObjective() : base() { }

        public VarObjective(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, string type) :
        base(accessModifiers, usageModifiers, objectName, scope) {
            ID = $"mcs.{BaseConverter.Convert(NextID++, 62)}";
            Type = type;
        }


        protected override void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers,
                                        string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            //always expecting format: new Objective(string);
            if(arguments.Length != 4
                || arguments[0].IsWilds || arguments[0].Word != "="
                || arguments[1].IsWilds || arguments[1].Word != "new"
                || arguments[2].IsWilds || arguments[2].Word != "Objective"
                || arguments[3].IsWord || arguments[3].BlockType != "(\\)" || arguments[3].Wilds[0].IsWilds) {
                throw new Compiler.SyntaxException("Expected ' = new Objective([type]);'.");
            } else {
                new VarObjective(accessModifiers, usageModifiers, objectName, scope, arguments[3].Wilds[0].Word);
            }

        }

        public override void WriteInit() {
            StreamWriter function = Compiler.FunctionStack.Peek();
            function.WriteLine($"scoreboard objectives add {ID} {Type}");
        }

        public override void WritePrep() {
            StreamWriter function = Compiler.PrepFunction;
            function.WriteLine($"scoreboard objectives add {ID} {Type}");
        }

        public override void WriteDemo() {
            StreamWriter function = Compiler.FunctionStack.Peek();
            function.WriteLine($"scoreboard objectives remove {ID}");
        }

        public static void ResetID() => NextID = 0;

    }

}
