using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace MCSharp.Variables {

	/// <summary>
	/// Base class for classes that represent a thing in the game.
	/// </summary>
	[DebuggerDisplay("{TypeName,nq} {ObjectName,nq}")]
	public abstract class Variable {

		/// <summary>
		/// A collection of all <see cref="Initializer"/>s organized by their <see cref="TypeName"/>.
		/// </summary>
		public static Dictionary<string, Initializer> Initializers { get; } = new Dictionary<string, Initializer>();

		/// <summary>
		/// A collection of all <see cref="Constructor"/>s organized by their <see cref="TypeName"/>.
		/// </summary>
		public static Dictionary<string, Constructor> Constructors { get; } = new Dictionary<string, Constructor>();

		public static Dictionary<(Type from, Type to), Caster[]> Casters { get; } = new Dictionary<(Type from, Type to), Caster[]>();

		/// <summary>
		/// Tries to cast <paramref name="value"/> into another type.
		/// </summary>
		public delegate Variable Caster(Variable value);

		#region Operations
		public enum Operation { New, Access, Set, Add, Subtract, Multiply, Divide, Modulo, GreaterThan, GreaterThanOrEqual, Equal, LessThan, LessThanOrEqual, BooleanAnd, BooleanOr, BooleanNot }
		public enum OperationType { Set, Arithmetic, Boolean, Misc }
		public static IReadOnlyDictionary<string, Operation> OperationDictionary { get; } = new Dictionary<string, Operation>() {
			// Misc
			{ ".", Operation.Access }, { "new", Operation.New },
			// Set
			{ "=", Operation.Set },
			// Arithmetic
			{ "+", Operation.Add }, { "-", Operation.Subtract },
			{ "*", Operation.Multiply }, { "/", Operation.Divide }, { "%", Operation.Modulo },
			{ ">", Operation.GreaterThan }, {">=", Operation.GreaterThanOrEqual },
			{ "<", Operation.LessThan }, { "<=", Operation.LessThanOrEqual },
			{ "==", Operation.Equal },
			// Boolean
			{ "&&", Operation.BooleanAnd }, { "||", Operation.BooleanOr }, { "!", Operation.BooleanNot }
		};
		public static IReadOnlyDictionary<Operation, OperationType> OperationTypeDictionary { get; } = new Dictionary<Operation, OperationType>() {
			//Misc
			{ Operation.Access, OperationType.Misc }, { Operation.New, OperationType.Misc },
			//Set
			{ Operation.Set, OperationType.Set },
			//Arithmetic
			{ Operation.Add, OperationType.Arithmetic }, { Operation.Subtract, OperationType.Arithmetic },
			{ Operation.Multiply, OperationType.Arithmetic }, { Operation.Divide, OperationType.Arithmetic }, { Operation.Modulo, OperationType.Arithmetic },
			{ Operation.GreaterThan, OperationType.Arithmetic }, { Operation.GreaterThanOrEqual, OperationType.Arithmetic },
			{ Operation.LessThan, OperationType.Arithmetic }, { Operation.LessThanOrEqual, OperationType.Arithmetic },
			{ Operation.Equal, OperationType.Arithmetic },
			//Boolean
			{ Operation.BooleanAnd, OperationType.Boolean }, { Operation.BooleanOr, OperationType.Boolean }, { Operation.BooleanNot, OperationType.Boolean }
		};
		#endregion

		#region IDs
		private static int hiddenID = 0;

		public static string GetNextHiddenID() => $"anon_{BaseConverter.Convert(hiddenID++, 62)}";
		public static void ResetHiddenID() => hiddenID = 0;
		#endregion

		#region Fields
		private Compiler.Scope innerScope;
		#endregion

		#region Properties
		public virtual int Order => 0;
		/// <summary>The name of the type of this variable.</summary>
		public abstract string TypeName { get; }
		/// <summary>A collection of <see cref="Variables.Access"/> that are allowed.</summary>
		public abstract ICollection<Access> AllowedAccessModifiers { get; }
		/// <summary>The <see cref="Variables.Access"/> value of this variable.</summary>
		public Access Access { get; }
		/// <summary>A collection of <see cref="Variables.Usage"/> that are allowed.</summary>
		public abstract ICollection<Usage> AllowedUsageModifiers { get; }
		/// <summary>The <see cref="Variables.Usage"/> value of this variable.</summary>
		public Usage Usage { get; }
		/// <summary>The name of this object in code.</summary>
		public string ObjectName { get; }
		/// <summary>The scope that contains this variable.</summary>
		public Compiler.Scope Scope { get; }
		/// <summary>The scope that contains this variable's members.</summary>
		public Compiler.Scope InnerScope => innerScope ??= new Compiler.Scope(Scope, this);
		protected Dictionary<string, Variable> Fields { get; } = new Dictionary<string, Variable>();
		protected Dictionary<string, (GetProperty Get, SetProperty Set)> Properties { get; } = new Dictionary<string, (GetProperty Get, SetProperty Set)>();
		protected Dictionary<string, MethodDelegate> Methods { get; } = new Dictionary<string, MethodDelegate>();
		#endregion

		#region Members
		/// <summary>
		/// Retrieves the member of this object by name.
		/// </summary>
		/// <param name="name">The name of the member to access.</param>
		/// <param name="value">The member found.</param>
		/// <returns>Returns true if the member exists.</returns>
		public bool TryGetMember(string name, [NotNullWhen(true)] out object value) {
			if(Fields.TryGetValue(name, out var field)) {
				value = field;
				return true;
			}
			if(Properties.TryGetValue(name, out var property)) {
				value = property;
				return true;
			}
			if(Methods.TryGetValue(name, out var method)) {
				value = method;
				return true;
			}
			value = null;
			return false;
		}

		/// <summary>
		/// Initializes a variable.
		/// </summary>
		/// <returns>Returns the initialized variable.</returns>
		public delegate Variable Initializer(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace);
		/// <summary>
		/// Constructs a value.
		/// </summary>
		/// <param name="arguments">The arguments passed to the constructor.</param>
		/// <returns>Returns the value constructed.</returns>
		public delegate Variable Constructor(Variable[] arguments);

		/// <summary>
		/// Represents a 'get' method of a property.
		/// </summary>
		/// <returns>Returns the value got.</returns>
		public delegate Variable GetProperty();
		/// <summary>
		/// Represents a 'set' method of a property.
		/// </summary>
		/// <param name="variable">The value to set.</param>
		public delegate void SetProperty(Variable variable);
		/// <summary>
		/// Represents a method.
		/// </summary>
		/// <param name="arguments">The arguments of the method.</param>
		/// <returns>Returns the return value of the method.</returns>
		public delegate Variable MethodDelegate(Variable[] arguments);
		#endregion


		#region Constructors
		public Variable() {
			Type type = GetType();
			if(!IsIgnoredType(type)) {

				// Add initializer.
				MethodInfo initInfo = GetType().GetMethod(nameof(Initialize), BindingFlags.Instance | BindingFlags.Public);
				if(initInfo.GetBaseDefinition().DeclaringType != initInfo.DeclaringType) Initializers.Add(TypeName, Initialize);
				else throw new Compiler.InternalError($"All Variables must override {nameof(Initialize)}.");

				// Add constructor.
				MethodInfo cnstInfo = GetType().GetMethod(nameof(Construct), BindingFlags.Instance | BindingFlags.Public);
				if(cnstInfo.GetBaseDefinition().DeclaringType != cnstInfo.DeclaringType) Constructors.Add(TypeName, Construct);

				// Add casters 'to'.
				IDictionary<Type, Caster> castersTo = GetCasters_To();
				foreach(KeyValuePair<Type, Caster> pair in castersTo) {
					(Type type, Type key) key = (type, pair.Key);
					Caster[] array;
					if(!Casters.ContainsKey(key)) Casters.Add(key, array = new Caster[2]);
					else array = Casters[key];
					array[0] = pair.Value;
				}

				// Add casters 'from'.
				IDictionary<Type, Caster> castersFrom = GetCasters_From();
				foreach(KeyValuePair<Type, Caster> pair in castersFrom) {
					(Type type, Type key) key = (pair.Key, type);
					Caster[] array;
					if(!Casters.ContainsKey(key)) Casters.Add(key, array = new Caster[2]);
					else array = Casters[key];
					array[1] = pair.Value;
				}

			}
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
		#endregion


		#region Methods

		public static bool IsIgnoredType(Type type) {
			return typeof(Spy).IsAssignableFrom(type)
				|| typeof(VarGeneric).IsAssignableFrom(type)
				|| typeof(Pointer).IsAssignableFrom(type);
		}

		public virtual Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) {
			if(string.IsNullOrEmpty(name))
				throw new ArgumentException("message", nameof(name));
			if(scope is null)
				throw new ArgumentNullException(nameof(scope));
			if(trace is null)
				throw new ArgumentNullException(nameof(trace));
			return this;
		}

		public virtual Variable Construct(Variable[] arguments) {
			if(arguments is null)
				throw new ArgumentNullException(nameof(arguments));
			return this;
		}

		public Variable InvokeOperation(ScriptWord operation, ScriptWild[] args) {

			if(!OperationDictionary.TryGetValue((string)operation, out Operation op))
				throw new Compiler.SyntaxException($"Unknown operator '{(string)operation}'.", operation.ScriptTrace);


			if(op == Operation.Access) {

#if DEBUG_OUT
				new Spy(null, $"# OP # {this}@{Scope} . {(string)args[0]}", null);
#endif

				if(TryGetMember(args[0], out object member)) {

					int argsCount = args.Length;
					if(member is Variable field) {
						// <<Accessing Field>>
						if(argsCount > 1) {
							//Apply an operation to the field.
							if(args[1].IsWilds) throw new Compiler.SyntaxException("Expected an operator.", args[1].ScriptTrace);
							else return field.InvokeOperation(args[1].Word, argsCount > 2 ? args[2..] : new ScriptWild[] { });
						} else return field;


					} else if(member is (GetProperty get, SetProperty set)) {
						// <<Accessing Property>>
						if(argsCount > 1) {
							if(args[1].IsWilds) throw new Compiler.SyntaxException("Expected an operator.", args[1].ScriptTrace);
							else if(args[1].Word == "=") {
								//Set property
								if(argsCount >= 2) {
									Variable value = Compiler.ParseValue(new ScriptWild(args[2..], "(\\)", ' '), Compiler.CurrentScope);
									set.Invoke(value);
									return null;
								} else throw new Compiler.SyntaxException("Expected a value.", args[0].ScriptTrace);
							} else {
								//Get property + Operation.
								if(args[1].IsWilds) throw new Compiler.SyntaxException("Expected an operator.", args[1].ScriptTrace);
								else return get.Invoke().InvokeOperation(args[1].Word, argsCount > 2 ? args[1..] : new ScriptWild[] { });
							}
						} else {
							//Get property.
							return get.Invoke();
						}


					} else if(member is MethodDelegate method) {
						// <<Accessing Method>>
						if(argsCount > 1) {
							var name = args[0];
							var arrr = args[1];
							Variable[] variables = new Variable[arrr.Wilds.Count];
							for(int i = 0; i < arrr.Wilds.Count; i++) {
								Variable variable = Compiler.ParseValue(arrr.Wilds[i], Compiler.CurrentScope);
								variables[i] = variable;
							}
							return Methods[(string)name.Word].Invoke(variables);
						} else throw new Exception("Internal Error: 015704082020");


					} else throw new Exception("Internal Error: 012504082020");

				} else throw new Exception("Internal Error: 0158504082020");


			} else {
				Variable operand = Compiler.ParseValue(new ScriptWild(args, " \\ ", ' '), Compiler.CurrentScope);
#if DEBUG_OUT
				new Spy(null, $"# OP # {this}@{Scope} {op} {operand}@{operand.Scope}", null);
#endif
				return InvokeOperation(op, operand, operation.ScriptTrace);
			}
		}

		public virtual Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			if(OperationTypeDictionary[operation] == OperationType.Misc)
				throw new Compiler.InternalError($"Operation type '{nameof(OperationType.Misc)}' cannot be used in the virtual {nameof(InvokeOperation)}.");
			else throw new InvalidArgumentsException($"Type '{TypeName}' has not defined the '{operation}' operation.", trace);
		}

		#region Write Events
		/// <summary>
		/// Writes the initialization commands to <see cref="Compiler.FunctionStack"/>.
		/// </summary>
		public virtual void WriteInit(StreamWriter function) {
#if DEBUG_OUT
			//if(!(this is Spy)) function.WriteLine($"# INIT # {this}@{Scope}");
#endif
		}

		/// <summary>
		/// Writes commands needed to keep this variable 'maintained' for the next tick, if it is required.
		/// </summary>
		public virtual void WriteTick(StreamWriter function) {
#if DEBUG_OUT
			//if(!(this is Spy)) function.WriteLine($"# TICK # {this}@{Scope}");
#endif
		}

		/// <summary>
		/// Writes the initialization commands to <see cref="Compiler.PrepFunction"/>.
		/// </summary>
		public virtual void WritePrep(StreamWriter function) {
#if DEBUG_OUT
			//if(!(this is Spy)) function.WriteLine($"# PREP # {this}@{Scope}");
#endif
		}

		/// <summary>
		/// Writes commands needed for when exiting scope.
		/// </summary>
		public virtual void WriteDele(StreamWriter function) {
#if DEBUG_OUT
			//if(!(this is Spy)) function.WriteLine($"# DELE # {this}@{Scope}");
#endif
		}

		/// <summary>
		/// Writes commands to remove all trace of this variable.
		/// </summary>
		public virtual void WriteDemo(StreamWriter function) {
#if DEBUG_OUT
			//if(!(this is Spy)) function.WriteLine($"# DEMO # {this}@{Scope}");
#endif
		}
		#endregion

		/// <summary>
		/// Creates a new <see cref="Spy"/> that will copy the value of this to <paramref name="variable"/>.
		/// </summary>
		public virtual void WriteCopyTo(StreamWriter function, Variable variable) {
			if(variable is Pointer<Variable> pointer) pointer.Variable = this;
			else throw new Compiler.SyntaxException($"This type can only be passed to {nameof(Pointer<Variable>)}.", Compiler.CurrentScriptTrace);
		}

		public virtual IDictionary<Type, Caster> GetCasters_To() {
			var casters = new Dictionary<Type, Caster> {
				//{ GetType(), value => value },
				{ typeof(VarString), value => value.GetString() },
				{ typeof(VarJSON), value => new VarJSON(Access.Private, Usage.Constant, GetNextHiddenID(), value.Scope, value.GetJSON()) }
			};
			return casters;
		}

		public virtual IDictionary<Type, Caster> GetCasters_From() {
			var casters = new Dictionary<Type, Caster> { };
			return casters;
		}

		public bool TryCast<TVariable>([NotNullWhen(true)] out TVariable result) where TVariable : Variable {
			(Type from, Type to) castInfo = (GetType(), typeof(TVariable));
			if(!Casters.ContainsKey(castInfo)) {
				result = null;
				return false;
			} else {
				Caster[] casters = Casters[castInfo];
				result = casters[0]?.Invoke(this) as TVariable ?? casters[1](this) as TVariable;
				return result != null;
			}
		}

		public bool TryCast(Type type, out Variable result) {
			(Type from, Type to) castInfo = (GetType(), type);
			if(!Casters.ContainsKey(castInfo)) {
				result = null;
				return false;
			} else {
				Caster[] casters = Casters[castInfo];
				result = casters[0]?.Invoke(this) ?? casters[1](this);
				return result != null;
			}
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

		public override string ToString() => this is Spy ? nameof(Spy) : $"{TypeName} {ObjectName}";
		#endregion

		#region Exceptions
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
		#endregion

	}

}
