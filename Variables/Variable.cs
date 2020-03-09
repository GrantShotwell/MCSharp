using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Collections;

namespace MCSharp.Variables {

    /// <summary>
    /// Base class for classes that represent a thing in the game.
    /// </summary>
    public abstract class Variable {

        public static Dictionary<string, Func<Access, Usage, string, Compiler.Scope, ScriptWild[], Variable>> Compilers { get; }
                = new Dictionary<string, Func<Access, Usage, string, Compiler.Scope, ScriptWild[], Variable>>();

        private static int hiddenID = 0;
        public static string GetNextHiddenID() => $"anon_{BaseConverter.Convert(hiddenID++, 62)}";

        public virtual int Order => 0;
        public abstract string TypeName { get; }
        public abstract ICollection<Access> AllowedAccessModifiers { get; }
        public Access Access { get; }
        public abstract ICollection<Usage> AllowedUsageModifiers { get; }
        public Usage Usage { get; }
        public string ObjectName { get; }
        public Compiler.Scope Scope { get; }
        protected Dictionary<string, Variable> Fields { get; } = new Dictionary<string, Variable>();
        protected Dictionary<string, Tuple<GetProperty, SetProperty>> Properties { get; } = new Dictionary<string, Tuple<GetProperty, SetProperty>>();
        protected Dictionary<string, Func<Variable[], Variable>> Methods { get; } = new Dictionary<string, Func<Variable[], Variable>>();

        public delegate Variable GetProperty();
        public delegate void SetProperty(Variable variable);


        public Variable() {
            Type type = GetType();
            if(!typeof(Spy).IsAssignableFrom(type) && !typeof(VarGeneric).IsAssignableFrom(type)) Compilers.Add(TypeName, Compile);
        }

        public Variable(Access access, Usage usage, string objectName, Compiler.Scope scope) {

            if(objectName == null) throw new ArgumentNullException(nameof(objectName));
            if(scope == null) throw new ArgumentNullException(nameof(scope));
            if(GetType() != typeof(Spy)) {
                if(!AllowedAccessModifiers.Contains(access)) throw new InvalidModifierException(access.ToString(), TypeName, Compiler.CurrentScriptTrace);
                if(!AllowedUsageModifiers.Contains(usage)) throw new InvalidModifierException(usage.ToString(), TypeName, Compiler.CurrentScriptTrace);
            }

            ObjectName = objectName;
            Access = access;
            Usage = usage;
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
        protected abstract Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments);

        /// <summary>
        /// 
        /// </summary>
        public virtual Variable Operation(ScriptWord operation, ScriptWild[] args) {
            if((string)operation == ".") {
                if(args.Length == 1 && args[0].IsWord) {
                    return Fields[(string)args[0].Word];
                } else if(args.Length == 2) {
                    Variable[] variables = new Variable[args[1].Wilds.Count];
                    for(int i = 0; i < args[1].Wilds.Count; i++) {
                        if(Compiler.TryParseValue(args[1].Wilds[i], Compiler.CurrentScope, out Variable variable)) {
                            variables[i] = variable;
                        } else throw new InvalidArgumentsException($"Could not parse '{(string)args[1].Wilds[i]}' into a variable.", args[1].Wilds[i].ScriptTrace);
                    }
                    return Methods[(string)args[0].Word].Invoke(variables);
                } else {
                    throw new InvalidArgumentsException("Too many arguments for '.' operator.", Compiler.CurrentScriptTrace);
                }
            } else {
                throw new InvalidArgumentsException($"Type '{TypeName}' has not defined the operation '{(string)operation}'.", operation.ScriptTrace);
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
        public virtual void WriteCopyTo(StreamWriter function, Variable variable)
            => throw new Compiler.SyntaxException($"Cannot pass the value of type '{TypeName}' to other variables!", Compiler.CurrentScriptTrace);

        public bool TryCast<TVariable>([NotNullWhen(true)] out TVariable result) where TVariable : Variable {
            bool success = TryCast(typeof(TVariable), out Variable variable);
            result = variable as TVariable;
            if(result is null) throw new Exception("123903042020");
            return success;
        }

        /// <summary>
        /// Attempts to create a new <see cref="Variable"/> of the given type.
        /// </summary>
        public virtual bool TryCast(Type type, [NotNullWhen(true)] out Variable result) {
            return type.IsAssignableFrom(typeof(VarString)) ? (result = GetString()) != null
                 : type.IsAssignableFrom(typeof(VarJSON)) ? (result = new VarJSON(Access.Private, Usage.Constant, GetNextHiddenID(), Scope, GetJSON())) != null
                 : (result = this) != null;
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
        public virtual VarString GetString() => new VarString(Access.Private, GetNextHiddenID(), Scope, ToString());

        public class InvalidModifierException : Exception {
            public InvalidModifierException(string modifier, string type, ScriptTrace at)
                : base($"[{at}] The modifier '{modifier}' is not valid for the type '{type}'.") { }
        }

        public class InvalidNameException : Exception {
            public InvalidNameException(string name, string reason, string type, ScriptTrace at)
                : base($"[{at}] The name '{name}' is {reason} for the type '{type}'.") { }
        }

        public class InvalidArgumentsException : Exception {
            public InvalidArgumentsException(string message, ScriptTrace at)
                : base($"[{at}] {message}") { }
            public InvalidArgumentsException(string message, ScriptTrace at, Exception inner)
                : base($"[{at}] {message}", inner) { }
        }

        public class InvalidCastException : Exception {
            public InvalidCastException(Variable variable, string type, ScriptTrace at)
                : base($"[{at}] Cannot cast '{variable}' to type '{type}'.") { }
        }

        public override string ToString() => $"{TypeName} {ObjectName}";


    }

}
