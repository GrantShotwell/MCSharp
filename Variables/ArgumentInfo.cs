using MCSharp.Compilation;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Variables {
    public class ArgumentInfo : IReadOnlyList<ArgumentInfo.Argument> {

        private IList<Argument> Arguments { get; }
        public Argument this[int index] => Arguments[index];
        public int Count => Arguments.Count;

        public ScriptTrace ScriptTrace { get; }

        public ArgumentInfo(Variable[] arguments, ScriptTrace trace) {
            int length = arguments.Length;
            var args = new Argument[length];
            for(int i = 0; i < length; i++) {
                args[i] = (arguments[i].TypeName, arguments[i]);
            }
            Arguments = args;
            ScriptTrace = trace;
        }

        public IEnumerator<Argument> GetEnumerator() => Arguments.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Arguments).GetEnumerator();

        public struct Argument {

            public string Type { get; }
            public Variable Value { get; }

            public Argument(string type, Variable value) {
                Type = type;
                Value = value;
            }

            public override bool Equals(object obj) => obj is Argument other && Type == other.Type && EqualityComparer<Variable>.Default.Equals(Value, other.Value);
            public override int GetHashCode() => HashCode.Combine(Type, Value);

            public void Deconstruct(out string type, out Variable value) {
                type = Type;
                value = Value;
            }

            public static implicit operator (string Type, Variable Value)(Argument value) => (value.Type, value.Value);
            public static implicit operator Argument((string Type, Variable Value) value) => new Argument(value.Type, value.Value);

        }

    }

}
