using MCSharp.Variables;
using System;
using System.Collections.Generic;
using VarString = MCSharp.Variables.VarString;

namespace MCSharp.Methods {

    public class ChatObject {

        public class Say : MethodGroup {

            private static readonly Type[] overflowExecuter = new Type[] { typeof(VarString) };
            private static readonly Type[] overflowSelector = new Type[] { typeof(VarSelector), typeof(VarString) };

            public override string Call => "Chat.Say";
            public override IReadOnlyCollection<IReadOnlyList<Type>> Overflows => new Type[][] { overflowExecuter, overflowSelector };

            public override void Invoke(params Variable[] parameters) {

            }

        }

        public class Tellraw : MethodGroup {

            public override string Call => "Chat.Tellraw";
            public override IReadOnlyCollection<IReadOnlyList<Type>> Overflows => new Type[][] {
                new Type[] { typeof(VarString) }
            };

            public override void Invoke(params Variable[] parameters) {
            }

        }

    }

}
