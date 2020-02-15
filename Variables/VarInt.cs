using MCSharp.Compilation;
using MCSharp.Methods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Variables {

    public class VarInt : Primitive {

        public override int Order => 100;
        public override string TypeName => "int";

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


        public VarInt() : base() { }

        public VarInt(Access access, Usage usage, string objectName, Compiler.Scope scope,
            int initValue, VarSelector selector, VarObjective objective)
            : base(access, usage, objectName, scope, initValue, selector, objective) { }

        public VarInt(Access access, Usage usage, string objectName, Compiler.Scope scope,
            VarSelector fromSelector, VarObjective fromObjective, VarSelector selector, VarObjective objective)
            : base(access, usage, objectName, scope, fromSelector, fromObjective, selector, objective) { }


        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {

            if(arguments.Length == 0) 
                throw new NotSupportedException("Creating an int without a value is currently not supported.");
            if(arguments.Length < 2) 
                throw new Compiler.SyntaxException("Expected more arguments for declaring an int.");
            if(arguments.Length > 2 || arguments[1].IsWilds) 
                throw new NotImplementedException("Nested operations (ie. 'int x = 1 + 2;') is currently not implemented.");
            if(arguments[0].IsWilds || arguments[0].Word != "=") 
                throw new Compiler.SyntaxException("Expected '=' for creating a new int.");

            ScriptWord word = arguments[1].Word;
            if(char.IsDigit(word[0])) {
                int value = int.Parse(word);
                return new VarInt(access, usage, objectName, scope, value,
                                  new VarSelector(access, usage, $"{objectName}.Selector", scope, "var"),
                                  new VarObjective(access, usage, $"{objectName}.Objective", scope, "dummy"));
            } else if(Compiler.TryGetVariable(word, scope, out Variable variable)) {
                if(variable is VarInt varInt || variable.TryCast(out varInt)) {
                    return new VarInt(access, usage, objectName, scope,
                                      varInt.Selector, varInt.Objective,
                                      new VarSelector(access, usage, $"{objectName}.Selector", scope, varInt.Selector.String),
                                      new VarObjective(access, usage, $"{objectName}.Objective", scope, "dummy"));
                } else throw new Compiler.SyntaxException($"Unknown how to cast '{variable}' into '{TypeName}'.");
            } else throw new Compiler.SyntaxException($"Unknown how to interpret '{word}' as an '{TypeName}'.");

        }

        public override void CompileOperation(ScriptWord operation, ScriptWild[] arguments) {
            if(arguments.Length > 1 || arguments[0].IsWilds)
                throw new NotImplementedException("Multiple arguments for 'int' operations has not been implemented yet.");
            Compiler.TryGetVariable(arguments[0].Word, Compiler.CurrentScope, out Variable argument);

            if(!(argument is VarInt varInt) && !argument.TryCast(out varInt))
                throw new InvalidArgumentsException("Expected operator on an 'int' to be/castable to an 'int'.");

            switch(operation) {
                case "+=": {
                    string[] init = new string[] { $"scoreboard players operation var {Objective.ID} += var {varInt.Objective.ID}" };
                    new Spy(Compiler.CurrentScope, null, init, null);
                    break;
                }
            }
        }

    }

}
