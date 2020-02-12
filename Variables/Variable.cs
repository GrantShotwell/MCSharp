using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;

namespace MCSharp.Variables {

    /// <summary>
    /// Base class for classes that represent a thing in the game.
    /// </summary>
    public abstract class Variable {

        public static Dictionary<string, Action<AccessModifier[], UsageModifier[], string, Compiler.Scope, ScriptWild[]>> Compilers { get; }
                = new Dictionary<string, Action<AccessModifier[], UsageModifier[], string, Compiler.Scope, ScriptWild[]>>();

        public abstract int Order { get; }
        public abstract string TypeName { get; }
        public abstract ICollection<AccessModifier> AllowedAccessModifiers { get; }
        public IReadOnlyList<AccessModifier> AccessModifiers { get; }
        public abstract ICollection<UsageModifier> AllowedUsageModifiers { get; }
        public IReadOnlyList<UsageModifier> UsageModifiers { get; }
        public string ObjectName { get; }
        public Compiler.Scope Scope { get; }


        public Variable() => Compilers.Add(TypeName, Compile);

        public Variable(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope) {

            if(objectName == null) throw new ArgumentNullException(nameof(objectName));
            if(scope == null) throw new ArgumentNullException(nameof(scope));

            ObjectName = objectName;
            foreach(AccessModifier modifier in accessModifiers)
                if(!AllowedAccessModifiers.Contains(modifier)) throw new InvalidModifierException(modifier.ToString(), TypeName);
            foreach(UsageModifier modifier in usageModifiers)
                if(!AllowedUsageModifiers.Contains(modifier)) throw new InvalidModifierException(modifier.ToString(), TypeName);
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
        /// Adds an item to <see cref="Compilers"/>.
        /// </summary>
        protected abstract void Compile(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope, ScriptWild[] arguments);

        /// <summary>
        /// Writes the initialization commands to <see cref="Compiler.FunctionStack"/>.
        /// </summary>
        public virtual void WriteInit() { }

        /// <summary>
        /// Writes the initialization commands to <see cref="Compiler.PrepFunction"/>.
        /// </summary>
        public virtual void WritePrep() { }

        /// <summary>
        /// Writes 'dispose' commands to <see cref="Compiler.FunctionStack"/>.
        /// </summary>
        public virtual void WriteDemo() { }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GetConstant() {
            if(AllowedUsageModifiers.Contains(UsageModifier.Constant))
                throw new NotImplementedException($"Calling '{nameof(GetConstant)}' on this object should have worked, but the writer of the class didn't not implement it.");
            else throw new InvalidOperationException($"Cannot call '{nameof(GetConstant)}' on this object because it is does not allow the 'constant' usage modifier.");
        }

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

    }

}
