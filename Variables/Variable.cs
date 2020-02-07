using System;
using System.Collections.Generic;

namespace MCSharp.Variables {

    /// <summary>
    /// Base class for classes that represent a thing in the game.
    /// </summary>
    public abstract class Variable {

        public static Dictionary<string, Action<string, string, string, string[]>> Compilers { get; } = new Dictionary<string, Action<string, string, string, string[]>>();

        public abstract ICollection<string> AllowedModifiers { get; }
        public abstract string TypeName { get; }
        public string Modifier { get; }
        public string ObjectName { get; }
        public string Scope { get; }


        public Variable() => Compilers.Add(TypeName, Compile);

        public Variable(string modifier, string name, string scope) {
            ObjectName = name;
            if(AllowedModifiers.Contains(modifier)) Modifier = modifier;
            else throw new InvalidModifierException(modifier, TypeName);
            Scope = scope;
            Compiler.AddVariable(this);
        }

        /// <summary>
        /// Adds an item to <see cref="Compilers"/>.
        /// </summary>
        protected abstract void Compile(string modifier, string objectName, string scope, string[] arguments);

        /// <summary>
        /// Writes the initialization commands to <see cref="Compiler.FunctionStack"/> and <see cref="Compiler.PrepFunction"/>
        /// </summary>
        public abstract void Initialize(bool prep);

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
