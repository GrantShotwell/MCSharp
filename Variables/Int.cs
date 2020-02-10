namespace MCSharp.Variables {

    /// <summary>
    /// Represents a standard <see cref="int"/>.
    /// </summary>
    public class Int : Primitive<int> {

        public override string TypeName => "int";
        public override string ShortTypeName => "i";
        public override int InitialValueScore => InitialValue;

        public Int() : base() { }

        public Int(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, int value) :
        base(accessModifiers, usageModifiers, objectName, scope, value) { }

        protected override int ValueFromString(string str) => int.Parse(str);

        protected override void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, int value)
            => new Int(accessModifiers, usageModifiers, objectName, scope, value);

    }

}
