using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Variables {

    public class Int : Primitive {

        public static VarObjective Temp1 { get; private set; }
        public static VarObjective Temp2 { get; private set; }

        public override int Order => 100;
        public override string TypeName => "int";
        public VarObjective Objective { get; }

        public override ICollection<AccessModifier> AllowedAccessModifiers => new AccessModifier[] { AccessModifier.Private, AccessModifier.Public };
        public override ICollection<UsageModifier> AllowedUsageModifiers => new UsageModifier[] { UsageModifier.Constant, UsageModifier.Static };

        public Int() : base() {
            Temp1 = new VarObjective(new AccessModifier[] { AccessModifier.Public }, new UsageModifier[] { }, "int.Temp1", Compiler.RootScope, "dummy");
            Temp1 = new VarObjective(new AccessModifier[] { AccessModifier.Public }, new UsageModifier[] { }, "int.Temp2", Compiler.RootScope, "dummy");
        }

        public Int(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, ScriptWild[] validArgs) :
        base(accessModifiers, usageModifiers, objectName, scope) {
            Objective = new VarObjective(accessModifiers, usageModifiers, $"{objectName}.Objective", scope, "dummy");

            var init = new List<string>();
            ScriptWord value = validArgs[1].Word;
            if(char.IsNumber(value[0])) {
                int c = int.Parse(value);
                init.Add($"scoreboard players set var {Objective.ID} {c}");
            } else {
                if(Compiler.TryGetVariable(value, Scope, out Variable variable)) {
                    if(variable is Int integer) init.Add($"scoreboard players operation var {Objective.ID} = var {integer.Objective.ID}");
                    else throw new Compiler.SyntaxException($"Cannot copy value of '{variable}' to type 'int'.");
                } else throw new Compiler.SyntaxException($"Unexpected 'int' initialization arguments: '{(string)new ScriptLine(validArgs)}'.");
            }

            Init = init.ToArray();
            Prep = init.ToArray();

        }


        protected override void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            if(arguments.Length > 2 || arguments[0].IsWilds) throw new NotImplementedException("Multiple arguments for 'int's (ie. '1 + 2') has not been implemented yet!");

            if(arguments.Length == 0) throw new NotSupportedException("Creating an int without a value is currently not supported.");
            if(arguments.Length < 2) throw new Compiler.SyntaxException("Expected more arguments for declaring an int.");
            if(arguments.Length > 2 || arguments[1].IsWilds) throw new NotImplementedException("Nested operations (ie. 'int x = 1 + 2;') is currently not implemented.");
            if(!arguments[0].IsWord || arguments[0].Word != "=") throw new Compiler.SyntaxException("Expected '=' for creating a new int.");

            new Int(accessModifiers, usageModifiers, objectName, scope, arguments);
        }

    }

}
