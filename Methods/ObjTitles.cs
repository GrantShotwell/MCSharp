using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Methods {

    public static class ObjTitles {

        public static string Call => "Titles";

        public class ShowTitle : MethodGroup {
            public override string Call => $"{ObjTitles.Call}.ShowTitle";
            public override void Invoke(params Variable[] parameters) {
                if(!(parameters[0] is VarSelector selector) && !parameters[0].TryCast(out selector))
                    throw new Variable.InvalidCastException(parameters[0], "Selector");
                if(!(parameters[1] is VarJSON json) && !parameters[1].TryCast(out json))
                    throw new Variable.InvalidCastException(parameters[1], "JSON");
                Show(selector, "title", json);
            }
        }
        public class ShowSubtitle : MethodGroup {
            public override string Call => $"{ObjTitles.Call}.ShowSubtitle";
            public override void Invoke(params Variable[] parameters) {
                if(!(parameters[0] is VarSelector selector) && !parameters[0].TryCast(out selector))
                    throw new Variable.InvalidCastException(parameters[0], "Selector");
                if(!(parameters[1] is VarJSON json) && !parameters[1].TryCast(out json))
                    throw new Variable.InvalidCastException(parameters[1], "JSON");
                Show(selector, "subtitle", json);
            }
        }
        public class ShowActionbar : MethodGroup {
            public override string Call => $"{ObjTitles.Call}.ShowActionbar";
            public override void Invoke(params Variable[] parameters) {
                if(!(parameters[0] is VarSelector selector) && !parameters[0].TryCast(out selector))
                    throw new Variable.InvalidCastException(parameters[0], "Selector");
                if(!(parameters[1] is VarJSON json) && !parameters[1].TryCast(out json))
                    throw new Variable.InvalidCastException(parameters[1], "JSON");
                Show(selector, "actionbar", json);
            }
        }

        private static void Show(VarSelector selector, string type, VarJSON json) {
            new Spy(null, new string[] { $"title {selector.GetConstant()} {type} {json.GetConstant()}" }, null);
        }

    }

}
