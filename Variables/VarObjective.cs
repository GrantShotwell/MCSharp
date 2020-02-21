using LargeBaseNumbers;
using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents a Minecraft scoreboard objective.
    /// </summary>
    public class VarObjective : ReferenceType {

        public static int NextID { get; private set; }

        public override int Order => base.Order - 10;
        public override string TypeName => "Objective";
        public string ID { get; }
        public string Type { get; }


        public VarObjective() : base() { }

        public VarObjective(Access access, Usage usage, string objectName, Compiler.Scope scope, string type) :
        base(access, usage, objectName, scope) {
            ID = $"mcs.{BaseConverter.Convert(NextID++, 62)}";
            Type = type;
        }


        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            //always expecting format: new Objective(string);
            if(arguments.Length != 4
                || arguments[0].IsWilds || arguments[0].Word != "="
                || arguments[1].IsWilds || arguments[1].Word != "new"
                || arguments[2].IsWilds || arguments[2].Word != "Objective"
                || arguments[3].IsWord || arguments[3].BlockType != "(\\)" || arguments[3].Wilds[0].IsWilds) {
                throw new Compiler.SyntaxException("Expected ' = new Objective([type]);'.");
            } else {
                return new VarObjective(access, usage, objectName, scope, arguments[3].Wilds[0].Word);
            }

        }

        public override bool TryCast<TVariable>([MaybeNullWhen(false)] out TVariable result) {
            Type type = typeof(TVariable);

            if(type.IsAssignableFrom(typeof(VarInt))) {
                string[] ids = new string[2];
                result = new VarInt(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, 0,
                                    new VarSelector(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "var"),
                                    this) as TVariable;
                return true;
            }

            result = null;
            return false;

        }

        public override void WriteInit(StreamWriter function) => function.WriteLine($"scoreboard objectives add {ID} {Type}");
        public override void WritePrep(StreamWriter function) => function.WriteLine($"scoreboard objectives add {ID} {Type}");
        public override void WriteDemo(StreamWriter function) => function.WriteLine($"scoreboard objectives remove {ID}");

        public override string GetConstant() => ID;

        public static void ResetID() => NextID = 0;

    }

}
