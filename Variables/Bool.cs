using System.Collections.Generic;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents a standard <see cref="bool"/>.
    /// </summary>
    public class Bool : Primitive<bool> {

        public override string TypeName => "bool";
        public override string ShortTypeName => "b";
        public override int InitialValueScore => InitialValue ? 1 : 0;

        public Bool() : base() { }

        public Bool(string modifier, string objectName, string scope, bool value) : base(modifier, objectName, scope, value) { }

        protected override bool ValueFromString(string str) => bool.Parse(str);

        protected override void Compile(string modifier, string objectName, string scope, bool value) => new Bool(modifier, objectName, scope, value);
    }

}
