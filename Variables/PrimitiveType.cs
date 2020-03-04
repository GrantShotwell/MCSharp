using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace MCSharp.Variables {

    public class PrimitiveType : Variable {

        public override string TypeName => "primitive";

        public VarSelector Selector { get; }
        public VarObjective Objective { get; }
        private int? InitValue { get; } = null;
        private VarSelector FromSelector { get; } = null;
        private VarObjective FromObjective { get; } = null;

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Constant, Usage.Static, Usage.Default };

        public PrimitiveType() : base() { }
        public PrimitiveType(Access access, Usage usage, string objectName, Compiler.Scope scope, int initValue,
        VarSelector selector, VarObjective objective) : base(access, usage, objectName, scope) {
            Fields.Add("selector", Selector = selector);
            Fields.Add("objective", Objective = objective);
            InitValue = initValue;
        }
        public PrimitiveType(Access access, Usage usage, string objectName, Compiler.Scope scope, VarSelector fromSelector,
        VarObjective fromObjective, VarSelector selector, VarObjective objective) : base(access, usage, objectName, scope) {
            Fields.Add("selector", Selector = selector);
            Fields.Add("objective", Objective = objective);
            FromSelector = fromSelector;
            FromObjective = fromObjective;
        }


        public override void WriteInit(StreamWriter function) {
            if(InitValue.HasValue) function.WriteLine($"scoreboard players set {Selector.GetConstant()} {Objective.ID} {InitValue.Value}");
            else function.WriteLine($"scoreboard players operation {Selector.GetConstant()} {Objective.ID} = {FromSelector.GetConstant()} {FromObjective.ID}");
        }

        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) 
            => throw new NotImplementedException();

    }

}
