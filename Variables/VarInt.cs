using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace MCSharp.Variables {

    public class VarInt : PrimitiveType {

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
                arguments = new ScriptWild[] { new ScriptWord(new ScriptString("=", "06012262020a")), new ScriptWord(new ScriptString("0", "06012262020b")) };
            if(arguments.Length < 2) 
                throw new Compiler.SyntaxException("Expected more arguments for declaring an int.", arguments[0].ScriptTrace);
            if(arguments[0].IsWilds || (string)arguments[0].Word != "=") 
                throw new Compiler.SyntaxException("Expected '=' for creating a new int.", arguments[0].ScriptTrace);

            if(arguments.Length > 2 || arguments[1].IsWilds) {
                if(Compiler.TryParseValue(new ScriptWild(arguments[1..], "(\\)", ' '), scope, out Variable parsed)) {
                    if(parsed is VarInt varInt || parsed.TryCast(out varInt)) {
                        return new VarInt(access, usage, objectName, scope,
                                          varInt.Selector, varInt.Objective,
                                          new VarSelector(access, usage, $"{objectName}.Selector", scope, varInt.Selector.GetConstant()),
                                          new VarObjective(access, usage, $"{objectName}.Objective", scope, "dummy"));
                    } else throw new Compiler.SyntaxException($"Unknown how to cast '{parsed}' into '{TypeName}'.", arguments[0].ScriptTrace);
                } else throw new Compiler.SyntaxException($"Unknown how to interpret into '{TypeName}'.", arguments[0].ScriptTrace);
            } else {
                ScriptWord word = arguments[1].Word;
                if(char.IsDigit((char)word[0])) {
                    int value = int.Parse((string)word);
                    return new VarInt(access, usage, objectName, scope, value,
                                      new VarSelector(access, usage, $"{objectName}.Selector", scope, "var"),
                                      new VarObjective(access, usage, $"{objectName}.Objective", scope, "dummy"));
                } else if(Compiler.TryGetVariable((string)word, scope, out Variable variable)) {
                    if(variable is VarInt varInt || variable.TryCast(out varInt)) {
                        return new VarInt(access, usage, objectName, scope,
                                          varInt.Selector, varInt.Objective,
                                          new VarSelector(access, usage, $"{objectName}.Selector", scope, varInt.Selector.GetConstant()),
                                          new VarObjective(access, usage, $"{objectName}.Objective", scope, "dummy"));
                    } else throw new Compiler.SyntaxException($"Unknown how to cast '{variable}' into '{TypeName}'.", arguments[0].ScriptTrace);
                } else throw new Compiler.SyntaxException($"Unknown how to interpret '{word}' as an '{TypeName}'.", arguments[0].ScriptTrace);
            }
        }


        public override void WriteCopyTo(StreamWriter function, Variable variable) {
            if(variable is VarInt varInt) {
                function.WriteLine($"scoreboard players operation var {varInt.Objective.ID} = var {Objective.ID}");
            } else throw new InvalidArgumentsException($"Unknown how to interpret '{variable}' as '{TypeName}'.", Compiler.CurrentScriptTrace);
        }

        public override string GetJSON() => $"{{\"score\":{{\"name\":\"var\",\"objective\":\"{Objective.ID}\"}}}}";

        public override Variable Operation(ScriptWord operation, ScriptWild[] args) {
            switch((string)operation) {
                case "+=": {
                    if(Compiler.TryParseValue(new ScriptWild(args, " \\ ", ' '), Compiler.CurrentScope, out Variable var)
                    && (var is VarInt varInt || var.TryCast(out varInt))) {
                        new Spy(null, $"scoreboard players operation var {Objective.ID} += var {varInt.Objective.ID}", null);
                        return this;
                    } else throw new Exception();
                }
                case "-=": {
                    if(Compiler.TryParseValue(new ScriptWild(args, " \\ ", ' '), Compiler.CurrentScope, out Variable var)
                    && (var is VarInt varInt || var.TryCast(out varInt))) {
                        new Spy(null, $"scoreboard players operation var {Objective.ID} -= var {varInt.Objective.ID}", null);
                        return this;
                    } else throw new Exception();
                }
                case "*=": {
                    if(Compiler.TryParseValue(new ScriptWild(args, " \\ ", ' '), Compiler.CurrentScope, out Variable var)
                    && (var is VarInt varInt || var.TryCast(out varInt))) {
                        new Spy(null, $"scoreboard players operation var {Objective.ID} *= var {varInt.Objective.ID}", null);
                        return this;
                    } else throw new Exception();
                }
                case "/=": {
                    if(Compiler.TryParseValue(new ScriptWild(args, " \\ ", ' '), Compiler.CurrentScope, out Variable var)
                    && (var is VarInt varInt || var.TryCast(out varInt))) {
                        new Spy(null, $"scoreboard players operation var {Objective.ID} /= var {varInt.Objective.ID}", null);
                        return this;
                    } else throw new Exception();
                }
                case "%=": {
                    if(Compiler.TryParseValue(new ScriptWild(args, " \\ ", ' '), Compiler.CurrentScope, out Variable var)
                    && (var is VarInt varInt || var.TryCast(out varInt))) {
                        new Spy(null, $"scoreboard players operation var {Objective.ID} %= var {varInt.Objective.ID}", null);
                        return this;
                    } else throw new Exception();
                }
                default: return base.Operation(operation, args);
            }
        }

        public override bool TryCast<TVariable>([MaybeNullWhen(false)] out TVariable result) {
            if(typeof(TVariable).IsAssignableFrom(typeof(VarBool))) {
                return (result = new VarBool(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, Selector, Objective,
                    new VarSelector(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "var"),
                    new VarObjective(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope, "dummy")) as TVariable) != null;
            } else return base.TryCast(out result);
        }
    }

}
