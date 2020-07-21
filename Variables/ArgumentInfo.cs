using MCSharp.Compilation;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Variables {
    public class ArgumentInfo : IReadOnlyList<(Type Type, Variable Value)> {

        private IList<(Type Type, Variable Value)> Arguments { get; }
        public (Type Type, Variable Value) this[int index] => Arguments[index];
        public int Count => Arguments.Count;

        public ScriptTrace ScriptTrace { get; }

        public ArgumentInfo(Variable[] arguments, ScriptTrace trace) {
            int length = arguments.Length;
            var args = new (Type Type, Variable Value)[length];
            for(int i = 0; i < length; i++) {
                args[i] = (arguments[i].GetType(), arguments[i]);
            }
            Arguments = args;
            ScriptTrace = trace;
        }

        public IEnumerator<(Type Type, Variable Value)> GetEnumerator() => Arguments.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)Arguments).GetEnumerator();

    }
}
