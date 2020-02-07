using System.Collections.Generic;

namespace MCSharp.Variables {

    /// <summary>
    /// Represents a standard <see cref="int"/>.
    /// </summary>
    public class Int : Primitive<int> {

        public override string TypeName => "int";
        public override string ShortTypeName => "i";
        public override int InitialValueScore => InitialValue;

        public Int() : base() { }

        public Int(string modifier, string objectName, string scope, int value) : base(modifier, objectName, scope, value) { }

        protected override int ValueFromString(string str) => int.Parse(str);

        protected override void Compile(string modifier, string objectName, string scope, int value) => new Int(modifier, objectName, scope, value);

    }

}
