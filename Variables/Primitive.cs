using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Variables {

    public abstract class Primitive : Variable {

        public VarSelector Selector { get; }
        public VarObjective Objective { get; }
        public int? InitValue { get; } = null;
        public VarSelector FromSelector { get; } = null;
        public VarObjective FromObjective { get; } = null;


        public Primitive() : base() { }

        public Primitive(Access access, Usage usage, string objectName, Compiler.Scope scope, int initValue,
                         VarSelector selector, VarObjective objective) : base(access, usage, objectName, scope) {
            Selector = selector;
            Objective = objective;
            InitValue = initValue;
        }

        public Primitive(Access access, Usage usage, string objectName, Compiler.Scope scope, VarSelector fromSelector,
                         VarObjective fromObjective, VarSelector selector, VarObjective objective) : base(access, usage, objectName, scope) {
            Selector = selector;
            Objective = objective;
            FromSelector = fromSelector;
            FromObjective = fromObjective;
        }


        public override void WriteInit() {
            StreamWriter function = Compiler.FunctionStack.Peek();
            if(InitValue.HasValue) function.WriteLine($"scoreboard players set {Selector.String} {Objective.ID} {InitValue}");
            else function.WriteLine($"scoreboard players operation {Selector.String} {Objective.ID} = {FromSelector.String} {FromObjective.ID}");
        }

        public override void WriteDemo() {
            StreamWriter function = Compiler.FunctionStack.Peek();

        }

    }

}
