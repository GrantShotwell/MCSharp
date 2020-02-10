using LargeBaseNumbers;
using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents a Minecraft scoreboard objective.
    /// </summary>
    public class Objective : Variable {

        public static int NextID { get; private set; }

        public override int Order => 50;
        public override string TypeName => "Objective";
        public string ID { get; }
        public string Type { get; }

        public override ICollection<AccessModifier> AllowedAccessModifiers => new AccessModifier[] { AccessModifier.Private, AccessModifier.Public };
        public override ICollection<UsageModifier> AllowedUsageModifiers => new UsageModifier[] { UsageModifier.Constant, UsageModifier.Static };


        public Objective() : base() { }

        public Objective(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, string type) :
        base(accessModifiers, usageModifiers, objectName, scope) {
            ID = $"mcs.{BaseConverter.Convert(NextID++, 62)}";
            Type = type;
        }

        protected override void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, Wild[] arguments) {
            throw new NotImplementedException();
        }

        public override void Initialize(bool prep) {
            StreamWriter function = prep ? Compiler.PrepFunction : Compiler.FunctionStack.Peek();
            function.WriteLine($"scoreboard objectives add {ID} {Type}");
        }

        public static void ResetID() => NextID = 0;

    }

}
