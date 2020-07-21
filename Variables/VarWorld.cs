using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Variables {
    class VarWorld : Variable {

        public override string TypeName => StaticTypeName;
        public static string StaticTypeName => "World";

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Static };


        public VarWorld() : base() {
            Compiler.StaticClassObjects.Add("World", new VarWorld("World"));
        }

        public VarWorld(string name) : base(Access.Public, Usage.Static, name, Compiler.RootScope) {

            ParameterInfo[] SetTimeInfo = new ParameterInfo[] {
                new (Type, bool)[] { (typeof(VarString), true) }
            };
            Methods.Add("SetTime", arguments => {
                (ParameterInfo match, int index) = ParameterInfo.HighestMatch(SetTimeInfo, arguments);
                match.Grab(arguments);

                string time;
                switch(index) {
                    case 0:
                        time = match[0].Value.GetConstant();
                        goto SetTime;

                        SetTime:
                        new Spy(null, $"time set {time}", null);
                        return null;

                    default: throw new MissingOverloadException($"{TypeName}.SetTime", index, arguments);
                }
            });

            ParameterInfo[] AddTimeInfo = new ParameterInfo[] {
                new (Type, bool)[] { (typeof(VarString), true) }
            };
            Methods.Add("AddTime", arguments => {
                (ParameterInfo match, int index) = ParameterInfo.HighestMatch(AddTimeInfo, arguments);
                match.Grab(arguments);

                string amount;
                switch(index) {
                    case 0:
                        amount = match[0].Value.GetConstant();
                        goto AddTime;

                        AddTime:
                        new Spy(null, $"time add {amount}", null);
                        return null;

                    default: throw new MissingOverloadException($"{TypeName}.AddTime", index, arguments);
                }
            });
            
            ParameterInfo[] GetTimeInfo = new ParameterInfo[] {
                new (Type, bool)[] { (typeof(VarString), true) },
                new (Type, bool)[] { (typeof(VarString), true), (typeof(VarSelector), true), (typeof(VarObjective), true) }
            };
            Methods.Add("GetTime", arguments => {
                (ParameterInfo match, int index) = ParameterInfo.HighestMatch(GetTimeInfo, arguments);
                match.Grab(arguments);

                VarInt result = new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
                string type;
                switch(index) {
                    case 0:
                        type = match[0].Value.GetConstant();
                        result.SetValue(-1);
                        goto GetTime;
                    case 1:
                        type = match[0].Value.GetConstant();
                        result.SetValue(match[1].Value as VarSelector, match[2].Value as VarObjective);
                        goto GetTime;

                        GetTime:
                        new Spy(null, $"execute store result score {result.Selector.GetConstant()} {result.Objective.GetConstant()} run time query {type}", null);
                        return result;

                    default: throw new MissingOverloadException($"{TypeName}.GetTime", index, arguments);
                }
            });

        }


        public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", trace);
        public override Variable Construct(ArgumentInfo passed) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", Compiler.CurrentScriptTrace);

    }
}
