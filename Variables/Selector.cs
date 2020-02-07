using System.Collections.Generic;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents an entity/player selector string.
    /// </summary>
    public class Selector : Variable {

        public override ICollection<string> AllowedModifiers => new string[] { "private", "public", "const" };
        public override string TypeName => "Selector";
        public string String { get; }

        public Selector() : base() { }

        public Selector(string modifier, string objectName, string scope, string value) : base(modifier, objectName, scope) => String = value;


        protected override void Compile(string modifier, string objectName, string scope, string[] arguments) {

        }

        public override void Initialize(bool prep) { }

    }

}
