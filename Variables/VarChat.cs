using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Variables {

    public class VarChat : Variable {

        public override int Order => 100;
        public override string TypeName => "Chat";
        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Static };

        public static Func<Variable[], Variable> Say { get; } = (args) => {
            if(args.Length > 1) throw new InvalidArgumentsException($"'Chat.Say' takes no more than one argument!", Compiler.CurrentScriptTrace);
            if(args.Length == 0) new Spy(null, "say", null);
            else {
                Variable arg0 = args[0];
                if(arg0 is VarString varString || arg0.TryCast(out varString)) {
                    new Spy(null, $"say {varString.GetConstant()}", null);
                } else {
                    throw new InvalidArgumentsException($"Unknown how to interpret variable '{arg0}' as type 'string'.", Compiler.CurrentScriptTrace);
                }
            }
            return null;
        };

        public static Func<Variable[], Variable> Tellraw { get; } = (args) => {
            if(args.Length > 2) throw new InvalidArgumentsException($"'Chat.Tellraw' takes no more than two arguments!", Compiler.CurrentScriptTrace);
            if(args.Length == 0) new Spy(null, $"tellraw @a {{\"text\":\"\"}}", null);
            if(args.Length == 1) {
                Variable arg0 = args[0];
                if(arg0 is VarSelector varSelector || arg0.TryCast(out varSelector)) {
                    new Spy(null, $"tellraw {varSelector.GetConstant()} {{\"text\":\"\"}}", null);
                } else throw new InvalidArgumentsException($"Unknown how to interpret variable '{arg0}' as type 'string'.", Compiler.CurrentScriptTrace);
            } else {
                Variable arg0 = args[0], arg1 = args[1];
                if((arg0 is VarSelector varSelector || arg0.TryCast(out varSelector))
                && (arg1 is VarJSON varJSON || arg1.TryCast(out varJSON))) {
                    new Spy(null, $"tellraw {varSelector.GetConstant()} {varJSON.GetConstant()}", null);
                } else {
                    if(varSelector == null) throw new InvalidCastException(arg0, "string", Compiler.CurrentScriptTrace);
                    else throw new InvalidCastException(arg1, "JSON", Compiler.CurrentScriptTrace);
                }
            }
            return null;
        };

        public VarChat() : base() {
            Compiler.StaticClassObjects.Add("Chat", new VarChat("Chat"));
        }

        public VarChat(string name) : base(Access.Public, Usage.Static, name, Compiler.RootScope) {
            Methods.Add("Say", Say);
            Methods.Add("Tellraw", Tellraw);
        }

        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            throw new Compiler.SyntaxException("Cannot make an instance of a static class.", arguments[0].ScriptTrace);
        }

    }

}
