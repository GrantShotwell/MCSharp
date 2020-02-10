namespace MCSharp.Variables {

    /// <summary>
    /// Represents a standard <see cref="bool"/>.
    /// </summary>
    public class Bool : Primitive<bool> {

        public override string TypeName => "bool";
        public override string ShortTypeName => "b";
        public override int InitialValueScore => InitialValue ? 1 : 0;

        public Bool() : base() { }

        public Bool(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, bool value) :
        base(accessModifiers, usageModifiers, objectName, scope, value) { }

        protected override bool ValueFromString(string str) => bool.Parse(str);

        protected override void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, bool value)
            => new Bool(accessModifiers, usageModifiers, objectName, scope, value);

    }

}
