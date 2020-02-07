using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents a set of Minecraft entities that were selected upon creation of this object.
    /// </summary>
    public class Entities : Variable {

        public override ICollection<string> AllowedModifiers => new string[] { "public", "const" };
        public override string TypeName => "Entity";
        public string Selector { get; }

        public Entities() : base() { }

        public Entities(string modifier, string objectName, string scope, Selector selector) : base(modifier, objectName, scope) {
            Selector = selector.String;
        }


        protected override void Compile(string modifier, string objectName, string scope, string[] arguments) {

        }

        public override void Initialize(bool prep) {
            StreamWriter function = prep ? Compiler.PrepFunction : Compiler.FunctionStack.Peek();
            function.WriteLine($"tag {(Selector.StartsWith("@e") ? "@e" : "@a")} remove var_{TypeName}_{ObjectName}");
            function.WriteLine($"tag {Selector} add var_{TypeName}_{ObjectName}");
        }

    }

}
