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
                if(parameters.Length != 2) throw new Compiler.SyntaxException("Chat.Say has exactly two arguments!");
                if(!(parameters[0] is VarSelector arg1))
                    throw new Compiler.SyntaxException("The first argument of Chat.Say needs to be a selector!");
                if(!(parameters[1] is VarString arg2))
                    throw new Compiler.SyntaxException("The second argument of Chat.Say needs to be a string!");
                string[] init = new string[] { $"say {arg1.GetConstant()}" };
                new MethodSpy(Compiler.CurrentScope, null, init, null);
            }

        }

        public class Tellraw : MethodGroup {

            public override string Call => "Chat.Tellraw";

            public override void Invoke(params Variable[] parameters) {
                if(parameters.Length != 2) throw new Compiler.SyntaxException("Chat.Tellraw has exactly two arguments!");
                if(!(parameters[0] is VarSelector arg1))
                    throw new Compiler.SyntaxException("The first argument of Chat.Tellraw needs to be a selector!");
                if(!(parameters[1] is VarJSON arg2))
                    throw new Compiler.SyntaxException("The second argument of Chat.Tellraw needs to be a JSON!");
                string[] init = new string[] { $"tellraw {arg1.GetConstant()} {arg2.GetConstant()}" };
                new MethodSpy(Compiler.CurrentScope, null, init, null);
            }

        }

    }

}
