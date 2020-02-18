using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using VarString = MCSharp.Variables.VarString;

namespace MCSharp.Methods {

    public static class ObjChat {

        public class Say : MethodGroup {

            public override string Call => "Chat.Say";

            public override void Invoke(params Variable[] parameters) {
                if(parameters.Length != 1) throw new Compiler.SyntaxException("Chat.Say has exactly one argument!");
                if(!(parameters[0] is VarString arg0) && !parameters[0].TryCast(out arg0))
                    throw new Compiler.SyntaxException("The second argument of Chat.Say needs to be a string!");
                new Spy(null, $"say {(arg0.IsSelector ? arg0.SelectorValue.String : arg0.ConstantValue)}", null);
            }

        }

        public class Tellraw : MethodGroup {

            public override string Call => "Chat.Tellraw";

            public override void Invoke(params Variable[] parameters) {
                if(parameters.Length != 2) throw new Compiler.SyntaxException("Chat.Tellraw has exactly two arguments!");
                if(!(parameters[0] is VarSelector arg0) && !parameters[0].TryCast(out arg0))
                    throw new Compiler.SyntaxException("The first argument of Chat.Tellraw needs to be a selector!");
                if(!(parameters[1] is VarJSON arg1) && !parameters[1].TryCast(out arg1))
                    throw new Compiler.SyntaxException("The second argument of Chat.Tellraw needs to be a JSON!");
                string[] init = new string[] { $"tellraw {arg0.GetConstant()} {arg1.GetConstant()}" };
                new Spy(null, init, null);
            }

        }

    }

}
