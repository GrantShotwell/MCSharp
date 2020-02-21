using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using LargeBaseNumbers;
using System.Collections;

namespace MCSharp.Variables {

    /// <summary>
    /// Base class for classes that represent a thing in the game.
    /// </summary>
    public abstract class Variable {

        public static Dictionary<string, Func<Access, Usage, string, Compiler.Scope, ScriptWild[], Variable>> Compilers { get; }
                = new Dictionary<string, Func<Access, Usage, string, Compiler.Scope, ScriptWild[], Variable>>();

        private static int hiddenID = 0;
        public static string NextHiddenID => $"anon_{BaseConverter.Convert(hiddenID++, 62)}";

        public virtual int Order => 0;
        public abstract string TypeName { get; }
        public abstract ICollection<Access> AllowedAccessModifiers { get; }
        public Access AccessModifier { get; }
        public abstract ICollection<Usage> AllowedUsageModifiers { get; }
        public Usage UsageModifier { get; }
        public string ObjectName { get; }
        public Compiler.Scope Scope { get; }
        protected MemberCollection Members { get; } = new MemberCollection();
        protected Dictionary<string, Func<Variable[], Variable>> Methods { get; } = new Dictionary<string, Func<Variable[], Variable>>();


        public Variable() {
            if(GetType() != typeof(Spy)) Compilers.Add(TypeName, Compile);
        }

        public Variable(Access access, Usage usage, string objectName, Compiler.Scope scope) {

            if(objectName == null) throw new ArgumentNullException(nameof(objectName));
            if(scope == null) throw new ArgumentNullException(nameof(scope));
            if(GetType() != typeof(Spy)) {
                if(!AllowedAccessModifiers.Contains(access)) throw new InvalidModifierException(access.ToString(), TypeName);
                if(!AllowedUsageModifiers.Contains(usage)) throw new InvalidModifierException(usage.ToString(), TypeName);
            }

            ObjectName = objectName;
            AccessModifier = access;
            UsageModifier = usage;
            Scope = scope;
            Scope.Variables.Add(this);

            if(!Compiler.VariableNames.TryGetValue(ObjectName, out Dictionary<Compiler.Scope, Variable> scopes)) {
                //The item doesn't exist yet. Make it.
                scopes = new Dictionary<Compiler.Scope, Variable>();
                Compiler.VariableNames.Add(ObjectName, scopes);
            }
            //Whether we just made it or just found it, add the variable to the dictionary.
            scopes.Add(Scope, this);

            if(!Compiler.VariableScopes.TryGetValue(Scope, out List<Variable> variables)) {
                //The item doesn't exist yet. Make it.
                variables = new List<Variable>();
                Compiler.VariableScopes.Add(Scope, variables);
            }
            //Whether we just made it or just found it, add the variable to the dictionary.
            variables.Add(this);

        }

        /// <summary>
        /// Calls the constructor to create a new <see cref="Variable"/>.
        /// </summary>
        protected abstract Variable Compile(Access accessModifier, Usage usageModifier, string objectName, Compiler.Scope scope, ScriptWild[] arguments);

        /// <summary>
        /// 
        /// </summary>
        public virtual Variable Operation(ScriptWord operation, ScriptWild[] args) {
            if(operation == ".") {
                if(args.Length == 1 && args[0].IsWord) {
                    return Members[args[0].Word];
                } else if(args.Length == 2) {
                    Variable[] variables = new Variable[args[1].Wilds.Count];
                    for(int i = 0; i < args[1].Wilds.Count; i++) {
                        if(Compiler.TryParseValue(args[1].Wilds[i], Scope, out Variable variable)) {
                            variables[i] = variable;
                        } else throw new InvalidArgumentsException($"Could not parse '{args[1].Wilds[i]}' into a variable.");
                    }
                    return Methods[args[0].Word].Invoke(variables);
                } else {
                    throw new InvalidArgumentsException("Too many arguments for '.' operator.");
                }
            } else {
                throw new InvalidOperationException($"Type '{TypeName}' has not defined the operation '{operation}'.");
            }
        }

        /// <summary>
        /// Writes the initialization commands to <see cref="Compiler.FunctionStack"/>.
        /// </summary>
        public virtual void WriteInit(StreamWriter function) { }

        /// <summary>
        /// Writes commands needed to keep this variable 'maintained' for the next tick, if it is required.
        /// </summary>
        public virtual void WriteTick(StreamWriter function) { }

        /// <summary>
        /// Writes the initialization commands to <see cref="Compiler.PrepFunction"/>.
        /// </summary>
        public virtual void WritePrep(StreamWriter function) { }

        /// <summary>
        /// Writes 'dispose' commands.
        /// </summary>
        public virtual void WriteDemo(StreamWriter function) { }

        /// <summary>
        /// Creates a new <see cref="Spy"/> that will copy the value of this to <paramref name="variable"/>.
        /// </summary>
        public virtual void WritePass(StreamWriter function, Variable variable)
            => throw new Compiler.SyntaxException($"Cannot pass the value of type '{TypeName}' to other variables!");

        /// <summary>
        /// Attempts to create a new <see cref="Variable"/> of the given type.
        /// </summary>
        public virtual bool TryCast<TVariable>([MaybeNullWhen(false)] out TVariable result) where TVariable : Variable {
            if(typeof(TVariable).IsAssignableFrom(typeof(VarString)))
                return (result = GetString() as TVariable) != null;
            if(typeof(TVariable).IsAssignableFrom(typeof(VarJSON)))
                return (result = new VarJSON(Access.Private, Usage.Constant, NextHiddenID, Scope, GetJSON()) as TVariable) != null;
            else return (result = this as TVariable) != null;
        }

        /// <summary>
        /// Returns a value that can be inserted directly into commands on compile-time, if possible.
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown when this method should have worked, but has not been overridden by this class.</exception>
        /// <exception cref="InvalidOperationException">Thrown when calling this method is not possible.</exception>
        public virtual string GetConstant() {
            if(AllowedUsageModifiers.Contains(Usage.Constant))
                throw new NotImplementedException($"Calling '{nameof(GetConstant)}' on this object should have worked, but the writer of the class did not implement it.");
            else throw new InvalidOperationException($"Cannot call '{nameof(GetConstant)}' on this object because '{TypeName}' does not allow the 'constant' usage modifier.");
        }

        /// <summary>
        /// Returns the raw JSON text that will return something useful when used in-game.
        /// </summary>
        public virtual string GetJSON() => $"{{\"text\":\"{TypeName}\"}}";

        /// <summary>
        /// The equivalent of <see cref="object.ToString()"/>.
        /// </summary>
        public virtual VarString GetString() => new VarString(Access.Private, NextHiddenID, Scope, ToString());

        public class InvalidModifierException : Exception {
            public InvalidModifierException(string modifier, string type)
                : base($"{Compiler.GetCurrentLocationTrace(Compiler.TraceFormat.CapitalizedPhrase)}: The modifier '{modifier}' is not valid for the type '{type}'.") { }
        }

        public class InvalidNameException : Exception {
            public InvalidNameException(string name, string reason, string type)
                : base($"{Compiler.GetCurrentLocationTrace(Compiler.TraceFormat.CapitalizedPhrase)}: The name '{name}' is {reason} for the type '{type}'.") { }
        }

        public class InvalidArgumentsException : Exception {
            public InvalidArgumentsException(string message)
                : base($"{Compiler.GetCurrentLocationTrace(Compiler.TraceFormat.CapitalizedPhrase)}: {message}") { }
            public InvalidArgumentsException(string message, Exception inner)
                : base($"{Compiler.GetCurrentLocationTrace(Compiler.TraceFormat.CapitalizedPhrase)}: {message}", inner) { }
        }

        public class InvalidCastException : Exception {
            public InvalidCastException(Variable variable, string type)
                : base($"Cannot cast '{variable}' to type '{type}'.") { }
        }

        public override string ToString() => $"{TypeName} {ObjectName}";


        public class MemberCollection : IDictionary<string, Variable> {

            private readonly Dictionary<string, Variable> dictionary = new Dictionary<string, Variable>();
            public void Add(Variable variable) => Add(variable.ObjectName, variable);

            public Variable this[string key] { get => ((IDictionary<string, Variable>)dictionary)[key]; set => ((IDictionary<string, Variable>)dictionary)[key] = value; }
            public ICollection<string> Keys => ((IDictionary<string, Variable>)dictionary).Keys;
            public ICollection<Variable> Values => ((IDictionary<string, Variable>)dictionary).Values;
            public int Count => ((IDictionary<string, Variable>)dictionary).Count;
            public bool IsReadOnly => ((IDictionary<string, Variable>)dictionary).IsReadOnly;

            public void Add(string key, Variable value) => ((IDictionary<string, Variable>)dictionary).Add(key, value);
            public void Add(KeyValuePair<string, Variable> item) => ((IDictionary<string, Variable>)dictionary).Add(item);
            public void Clear() => ((IDictionary<string, Variable>)dictionary).Clear();
            public bool Contains(KeyValuePair<string, Variable> item) => ((IDictionary<string, Variable>)dictionary).Contains(item);
            public bool ContainsKey(string key) => ((IDictionary<string, Variable>)dictionary).ContainsKey(key);
            public void CopyTo(KeyValuePair<string, Variable>[] array, int arrayIndex) => ((IDictionary<string, Variable>)dictionary).CopyTo(array, arrayIndex);
            public IEnumerator<KeyValuePair<string, Variable>> GetEnumerator() => ((IDictionary<string, Variable>)dictionary).GetEnumerator();
            public bool Remove(string key) => ((IDictionary<string, Variable>)dictionary).Remove(key);
            public bool Remove(KeyValuePair<string, Variable> item) => ((IDictionary<string, Variable>)dictionary).Remove(item);
            public bool TryGetValue(string key, [MaybeNullWhen(false)] out Variable value) => ((IDictionary<string, Variable>)dictionary).TryGetValue(key, out value);
            IEnumerator IEnumerable.GetEnumerator() => ((IDictionary<string, Variable>)dictionary).GetEnumerator();

        }


    }

}
