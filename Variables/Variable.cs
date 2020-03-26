using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace MCSharp.Variables {

	/// <summary>
	/// Base class for classes that represent a thing in the game.
	/// </summary>
	public abstract class Variable {

		public static Dictionary<string, Initializer> Compilers { get; }
				= new Dictionary<string, Initializer>();

		public enum Operation { Access, Set, Add, Subtract, Multiply, Divide, Modulo, BooleanAnd, BooleanOr, BooleanNot }
		public enum OperationType { Set, Arithmetic, Boolean, Misc }
		public static IReadOnlyDictionary<string, Operation> OperationDictionary { get; } = new Dictionary<string, Operation>() {
			{ ".", Operation.Access }, { "=", Operation.Set },
			{ "+", Operation.Add }, { "-", Operation.Subtract },
			{ "*", Operation.Multiply }, { "/", Operation.Divide }, { "%", Operation.Modulo },
			{ "&&", Operation.BooleanAnd }, { "||", Operation.BooleanOr }, { "!", Operation.BooleanNot }
		};
		public static IReadOnlyDictionary<Operation, OperationType> OperationTypeDictionary { get; } = new Dictionary<Operation, OperationType>() {
			{ Operation.Access, OperationType.Misc }, { Operation.Set, OperationType.Set },
			{ Operation.Add, OperationType.Arithmetic }, { Operation.Subtract, OperationType.Arithmetic },
			{ Operation.Multiply, OperationType.Arithmetic }, { Operation.Divide, OperationType.Arithmetic }, { Operation.Modulo, OperationType.Arithmetic },
			{ Operation.BooleanAnd, OperationType.Boolean }, { Operation.BooleanOr, OperationType.Boolean }, { Operation.BooleanNot, OperationType.Boolean }
		};

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

		public delegate Variable Initializer(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptTrace trace);
		public delegate Variable GetProperty();
		public delegate void SetProperty(Variable variable);


		public Variable() {
			Type type = GetType();
			if(!typeof(Spy).IsAssignableFrom(type) && !typeof(VarGeneric).IsAssignableFrom(type)) Compilers.Add(TypeName, Initialize);
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


		protected virtual Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) {
			if(string.IsNullOrEmpty(name))
				throw new ArgumentException("message", nameof(name));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(trace is null)
				throw new ArgumentNullException(nameof(trace));
			return null;
		}

		public Variable InvokeOperation(ScriptWord operation, ScriptWild[] args) {
			if(!OperationDictionary.TryGetValue((string)operation, out Operation op))
				throw new Compiler.SyntaxException($"Unknown operator '{(string)operation}'.", operation.ScriptTrace);
			if(op == Operation.Access) {
				if(args.Length == 1 && args[0].IsWord) {
					// <<Accessing Field or Property>>
					string word = args[0];
					if(Fields.TryGetValue(word, out Variable field)) {
						return field;
					} else if(Properties.TryGetValue(word, out Tuple<GetProperty, SetProperty> property)) {
						return property.Item1.Invoke();
					} else throw new Exception("023903132020");
				} else if(args.Length == 2) {
					// <<Accessing Method>>
					var name = args[0];
					var arrr = args[1];
					Variable[] variables = new Variable[arrr.Wilds.Count];
					for(int i = 0; i < arrr.Wilds.Count; i++) {
						if(Compiler.TryParseValue(arrr.Wilds[i], Compiler.CurrentScope, out Variable variable)) {
							variables[i] = variable;
						} else throw new InvalidArgumentsException($"Could not parse '{(string)arrr.Wilds[i]}' into a variable.", arrr.Wilds[i].ScriptTrace);
					}
					return Methods[(string)name.Word].Invoke(variables);
				} else if(args.Length >= 3) {
					if(Properties.TryGetValue(args[0], out var property)) {
						// <<Setting Property>>
						if(args[1] != "=") throw new Compiler.SyntaxException("Expected '='.", operation.ScriptTrace);
						var wild = new ScriptWild(args[2..], "(\\)", ' ');
						if(Compiler.TryParseValue(wild, Compiler.CurrentScope, out Variable value)) {
							property.Item2.Invoke(value);
							return null;
						} else throw new Compiler.SyntaxException("Could not parse into a value.", wild.ScriptTrace);
					} else throw new Exception("111103262020");
				} else throw new InvalidArgumentsException("Invalid arguments for '.' operator.", Compiler.CurrentScriptTrace);
			} else {
				if(!Compiler.TryParseValue(new ScriptWild(args, " \\ ", ' '), Compiler.CurrentScope, out Variable operand))
					throw new Compiler.SyntaxException("Could not parse into a value.", operation.ScriptTrace);
				return InvokeOperation(op, operand, operation.ScriptTrace);
			}
		}

		public virtual Variable InvokeOperation(Operation operation, Variable value, ScriptTrace trace) {
			if(operation == Operation.Access) throw new Exception("022503112020");
			else throw new InvalidArgumentsException($"Type '{TypeName}' has not defined the '{operation}' operation.", trace);
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
			if(result is null && success) throw new Exception("123903042020");
			return success;
		}

		/// <summary>
		/// Attempts to create a new <see cref="Variable"/> of the given type.
		/// </summary>
		public virtual bool TryCast(Type type, [NotNullWhen(true)] out Variable result) {
			return type.IsAssignableFrom(typeof(VarString)) ? (result = GetString()) != null
				 : type.IsAssignableFrom(typeof(VarJSON)) ? (result = new VarJSON(Access.Private, Usage.Constant, GetNextHiddenID(), Scope, GetJSON())) != null
				 : type.IsAssignableFrom((result = this).GetType());
		}

		/// <summary>
		/// Returns a value that can be inserted directly into commands on compile-time.
		/// </summary>
		/// <exception cref="NotImplementedException">Thrown when this method should have worked, but has not been overridden by this class.</exception>
		/// <exception cref="InvalidOperationException">Thrown when calling this method is not possible.</exception>
		public virtual string GetConstant() => ObjectName;

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
