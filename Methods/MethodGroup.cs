using MCSharp.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MCSharp.Methods {

    public abstract class MethodGroup {

        public static Dictionary<string, Action<Variable[]>> Dictionary { get; } = new Dictionary<string, Action<Variable[]>>();

        public static ICollection<GenericDictionary> GenericDictionaries { get; } = new HashSet<GenericDictionary>();

        public abstract string Call { get; }


        public MethodGroup() {
            Dictionary.Add(Call, Invoke);
        }


        public abstract void Invoke(params Variable[] parameters);


        public abstract class GenericDictionary : IReadOnlyDictionary<string, Action<Variable, Variable[]>> {

            protected IDictionary Dictionary { get; }

            public abstract Type Type { get; }

            public int Count => Dictionary.Count;

            public abstract Action<Variable, Variable[]> this[string key] { get; }

            public abstract IEnumerable<Action<Variable, Variable[]>> Values { get; }

            public IEnumerable<string> Keys {
                get {
                    var keys = new List<string>();
                    foreach(object key in Dictionary.Keys)
                        keys.Add(key as string);
                    return keys;
                }
            }


            public GenericDictionary(IDictionary dictionary) {
                Dictionary = dictionary;
            }

            public void Clear() => Dictionary.Clear();

            public abstract bool ContainsKey(string key);

            public abstract bool TryGetValue(string key, [MaybeNullWhen(false)] out Action<Variable, Variable[]> value);

            public abstract IEnumerator<KeyValuePair<string, Action<Variable, Variable[]>>> GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => Dictionary.GetEnumerator();

        }


    }

    public abstract class MethodGroup<TVariable> : MethodGroup where TVariable : Variable {

        new public static Dictionary<string, Action<TVariable, Variable[]>> Dictionary { get; } = new Dictionary<string, Action<TVariable, Variable[]>>();
        

        public MethodGroup() {
            Dictionary.Add(Call, Invoke);
            GenericDictionaries.Add(new GenericDictionary(Dictionary));
        }


        public override void Invoke(params Variable[] parameters) => throw new InvalidOperationException("This MethodGroup is non-static.");
        public abstract void Invoke(TVariable variable, params Variable[] parameters);


        new public class GenericDictionary : MethodGroup.GenericDictionary {

            new protected Dictionary<string, Action<TVariable, Variable[]>> Dictionary { get; }

            public override Type Type => typeof(TVariable);

            public override Action<Variable, Variable[]> this[string key] => (variable, arguments) => Dictionary[key].Invoke(variable as TVariable, arguments);

            public override IEnumerable<Action<Variable, Variable[]>> Values => throw new NotImplementedException();


            public GenericDictionary() : base(new Dictionary<string, Action<TVariable, Variable[]>>()) {
                Dictionary = base.Dictionary as Dictionary<string, Action<TVariable, Variable[]>>;
            }

            public GenericDictionary(Dictionary<string, Action<TVariable, Variable[]>> dictionary) : base(dictionary) {
                Dictionary = dictionary;
            }


            public override bool ContainsKey(string key) => Dictionary.ContainsKey(key);

            public override IEnumerator<KeyValuePair<string, Action<Variable, Variable[]>>> GetEnumerator() => new Enumerator(Dictionary.GetEnumerator());

            public override bool TryGetValue(string key, [MaybeNullWhen(false)] out Action<Variable, Variable[]> value) {
                if(Dictionary.TryGetValue(key, out Action<TVariable, Variable[]> act)) {
                    value = (variable, arguments) => act.Invoke(variable as TVariable, arguments);
                    return true;
                } else {
                    value = null;
                    return false;
                }
            }

            public class Enumerator : IEnumerator<KeyValuePair<string, Action<Variable, Variable[]>>> {

                private readonly IEnumerator<KeyValuePair<string, Action<TVariable, Variable[]>>> enumerator;

                public KeyValuePair<string, Action<Variable, Variable[]>> Current {
                    get {
                        var current = enumerator.Current;
                        return new KeyValuePair<string, Action<Variable, Variable[]>>(
                            current.Key, (variable, arguments) => current.Value.Invoke(variable as TVariable, arguments));
                    }
                }

                object IEnumerator.Current => Current;


                public Enumerator(IEnumerator<KeyValuePair<string, Action<TVariable, Variable[]>>> enumerator) {
                    this.enumerator = enumerator;
                }

                public void Dispose() => enumerator.Dispose();
                public bool MoveNext() => enumerator.MoveNext();
                public void Reset() => enumerator.Reset();

            }

        }


    }

}
