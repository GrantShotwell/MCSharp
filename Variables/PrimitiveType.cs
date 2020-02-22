using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace MCSharp.Variables {

    public abstract class PrimitiveType : Variable {

        public VarSelector Selector { get; }
        public VarObjective Objective { get; }
        public int? InitValue { get; } = null;
        public VarSelector FromSelector { get; } = null;
        public VarObjective FromObjective { get; } = null;

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Constant, Usage.Static, Usage.Default };


        public PrimitiveType() : base() { }
        public PrimitiveType(Access access, Usage usage, string objectName, Compiler.Scope scope, int initValue,
        VarSelector selector, VarObjective objective) : base(access, usage, objectName, scope) {
            Selector = selector;
            Objective = objective;
            Members.Add(Selector);
            Members.Add(Objective);
            InitValue = initValue;
        }
        public PrimitiveType(Access access, Usage usage, string objectName, Compiler.Scope scope, VarSelector fromSelector,
        VarObjective fromObjective, VarSelector selector, VarObjective objective) : base(access, usage, objectName, scope) {
            Selector = selector;
            Objective = objective;
            Members.Add(Selector);
            Members.Add(Objective);
            FromSelector = fromSelector;
            FromObjective = fromObjective;
        }


        public override void WriteInit(StreamWriter function) {
            if(InitValue.HasValue) function.WriteLine($"scoreboard players set {Selector.GetConstant()} {Objective.ID} {InitValue}");
            else function.WriteLine($"scoreboard players operation {Selector.GetConstant()} {Objective.ID} = {FromSelector.GetConstant()} {FromObjective.ID}");
        }

    }

}
