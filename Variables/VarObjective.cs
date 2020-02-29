using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents a Minecraft scoreboard objective.
    /// </summary>
    public class VarObjective : Variable {

        public static int NextID { get; private set; }

        public override int Order => base.Order - 10;
        public override string TypeName => "Objective";
        public string ID { get; }
        public string Type { get; }

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


        public VarObjective() : base() { }

        public VarObjective(Access access, Usage usage, string objectName, Compiler.Scope scope, string type) :
        base(access, usage, objectName, scope) {
            ID = $"mcs.{BaseConverter.Convert(NextID++, 62)}";
            Type = type;
            Methods.Add("GetInt", (arguments) => {
                if(arguments.Length > 1) throw new ArgumentException("Expected at most 1 (Selector) argument for 'Objective.GetInt(...)'.");
                if(arguments.Length == 0) {
                    return new VarInt(Access.Private, Usage.Default, NextHiddenID, scope, 
                        new VarSelector(Access.Private, Usage.Default, NextHiddenID, scope, "@e"), this,
                        new VarSelector(Access.Private, Usage.Default, NextHiddenID, scope, "var"),
                        new VarObjective(Access.Private, Usage.Default, NextHiddenID, scope, "dummy"));
                } else if(arguments[0] is VarSelector varSelector || arguments[0].TryCast(out varSelector)) {
                    return new VarInt(Access.Private, Usage.Default, NextHiddenID, scope, varSelector, this,
                        new VarSelector(Access.Private, Usage.Default, NextHiddenID, scope, "var"),
                        new VarObjective(Access.Private, Usage.Default, NextHiddenID, scope, "dummy"));
                } else throw new ArgumentException($"Could not interpret '{arguments[0]}' as 'Selector'.");
            });
        }


        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            //always expecting format: new Objective(string);
            if(arguments.Length != 4
                || arguments[0].IsWilds || (string)arguments[0].Word != "="
                || arguments[1].IsWilds || (string)arguments[1].Word != "new"
                || arguments[2].IsWilds || (string)arguments[2].Word != "Objective"
                || arguments[3].IsWord || arguments[3].BlockType != "(\\)" || arguments[3].Wilds[0].IsWilds) {
                throw new Compiler.SyntaxException("Expected ' = new Objective([type]);'.", arguments[0].ScriptTrace);
            } else {
                return new VarObjective(access, usage, objectName, scope, (string)arguments[3].Wilds[0].Word);
            }

        }

        public override bool TryCast<TVariable>([NotNullWhen(false)] out TVariable result) {
            Type type = typeof(TVariable);

            if(type.IsAssignableFrom(typeof(PrimitiveType))) {
                result = new PrimitiveType(
                    Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope,
                    new VarSelector(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "@e"), this,
                    new VarSelector(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "var"),
                    new VarObjective(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "dummy")) as TVariable;
                return true;
            }
            
            if(type.IsAssignableFrom(typeof(VarInt))) {
                result = new VarInt(
                    Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope,
                    new VarSelector(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "@e"), this,
                    new VarSelector(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "var"),
                    new VarObjective(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "dummy")) as TVariable;
                return true;
            }
            
            if(type.IsAssignableFrom(typeof(VarBool))) {
                result = new VarBool(
                    Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope,
                    new VarSelector(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "@e"), this,
                    new VarSelector(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "var"),
                    new VarObjective(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "dummy")) as TVariable;
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
