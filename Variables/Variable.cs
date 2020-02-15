using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using LargeBaseNumbers;

namespace MCSharp.Variables {

    /// <summary>
    /// Base class for classes that represent a thing in the game.
    /// </summary>
    public abstract class Variable {

        public static Dictionary<string, Func<Access, Usage, string, Compiler.Scope, ScriptWild[], Variable>> Compilers { get; }
                = new Dictionary<string, Func<Access, Usage, string, Compiler.Scope, ScriptWild[], Variable>>();

        private static int hiddenID = 0;
        public static string NextHiddenID => $"hidden_{BaseConverter.Convert(hiddenID++, 62)}";

        public abstract int Order { get; }
        public abstract string TypeName { get; }
        public abstract ICollection<Access> AllowedAccessModifiers { get; }
        public Access AccessModifier { get; }
        public abstract ICollection<Usage> AllowedUsageModifiers { get; }
        public Usage UsageModifier { get; }
        public string ObjectName { get; }
        public Compiler.Scope Scope { get; }


        public Variable() {
            if(GetType() != typeof(MethodSpy)) Compilers.Add(TypeName, Compile);
        }

        public Variable(Access accessModifier, Usage usageModifier, string objectName, Compiler.Scope scope) {

            if(objectName == null) throw new ArgumentNullException(nameof(objectName));
            if(scope == null) throw new ArgumentNullException(nameof(scope));
            if(GetType() != typeof(MethodSpy)) {
                if(!AllowedAccessModifiers.Contains(accessModifier)) throw new InvalidModifierException(accessModifier.ToString(), TypeName);
                if(!AllowedUsageModifiers.Contains(usageModifier)) throw new InvalidModifierException(usageModifier.ToString(), TypeName);
            }

            ObjectName = objectName;
            AccessModifier = accessModifier;
            UsageModifier = usageModifier;
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

        public abstract void CompileOperation(ScriptWord operation, ScriptWild[] arguments);

        /// <summary>
        /// Writes the initialization commands to <see cref="Compiler.FunctionStack"/>.
        /// </summary>
        public virtual void WriteInit() { }

        /// <summary>
        /// Writes commands needed to keep this variable loaded if it is required.
        /// </summary>
        public virtual void WriteMain() { }

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
        public virtual bool TryCast<TVariable>([MaybeNullWhen(false)] out TVariable result) where TVariable : Variable 
            => typeof(TVariable).IsAssignableFrom(typeof(VarString))
                ? ((result = GetString() as TVariable) != null)
                : ((result = this as TVariable) != null);

        /// <summary>
        /// Returns a value that can be inserted directly into commands.
        /// </summary>
        public virtual string GetConstant() {
            if(AllowedUsageModifiers.Contains(Usage.Constant))
                throw new NotImplementedException($"Calling '{nameof(GetConstant)}' on this object should have worked, but the writer of the class didn't not implement it.");
            else throw new InvalidOperationException($"Cannot call '{nameof(GetConstant)}' on this object because it is does not allow the 'constant' usage modifier.");
        }

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

    }

}
