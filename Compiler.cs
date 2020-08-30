using MCSharp.Compilation;
using MCSharp.Statements;
using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using static MCSharp.Compilation.ScriptObject;
using static MCSharp.Variables.Variable;

namespace MCSharp {

	public static class Compiler {

		private static int highestFunctionStackSize = 0;
		private static readonly Dictionary<int, Scope> allScopes = new Dictionary<int, Scope>();

		public static ICollection<string> BinaryOperators { get; } = new string[] { "+", "-", "*", "/", "%" };
		public static ICollection<string> BooleanOperators { get; } = new string[] { "&&", "||" };

		public static StreamWriter PrepFunction { get; private set; }
		public static StreamWriter DemoFunction { get; private set; }
		public static StreamWriter TickFunction { get; private set; }
		public static Stack<StreamWriter> FunctionStack { get; } = new Stack<StreamWriter>();
		public static ScriptTrace AnonScriptTrace { get; private set; }

		public static Dictionary<string, Dictionary<Scope, Variable>> VariableNames { get; } = new Dictionary<string, Dictionary<Scope, Variable>>();
		public static Dictionary<Scope, List<Variable>> VariableScopes { get; } = new Dictionary<Scope, List<Variable>>();
		public static Dictionary<string, Variable> StaticClassObjects { get; } = new Dictionary<string, Variable>();

		public static ScriptTrace CurrentScriptTrace { get; private set; }
		public static IReadOnlyDictionary<int, Scope> AllScopes => allScopes;
		public static Scope RootScope { get; private set; }
		private static Stack<Scope> ScopeStack { get; } = new Stack<Scope>();
		public static Scope CurrentScope {
			[DebuggerStepThrough]
			get => ScopeStack.Peek();
		}

		public static Dictionary<string, Access> AccessModifiers { get; } = new Dictionary<string, Access>() {
			{ "public", Access.Public },
			{ "private", Access.Private }
		};
		public static Dictionary<string, Usage> UsageModifiers { get; } = new Dictionary<string, Usage>() {
			{ "static", Usage.Static },
			{ "const", Usage.Constant }
		};
		public static Dictionary<string, Type> Datatypes { get; } = new Dictionary<string, Type>();

		public static void Compile(string directory) {

			//Clear and reset saved values from last compile.
			Reset();

			string functionsPath = directory + "\\functions";
			string[] functions = Directory.GetFiles(functionsPath);
			string scriptsPath = directory + "\\scripts";
			string[] scripts = Directory.GetFiles(scriptsPath);

			var funcDirectory = new DirectoryInfo(functionsPath);
			//foreach(FileInfo file in funcDirectory.GetFiles()) file.Delete();
			//foreach(DirectoryInfo d in funcDirectory.GetDirectories()) d.Delete(true);

			Directory.CreateDirectory(functionsPath + "\\mcscript");
			PrepFunction = File.CreateText(functionsPath + "\\mcscript\\prep.mcfunction");
			DemoFunction = File.CreateText(functionsPath + "\\mcscript\\demo.mcfunction");
			TickFunction = File.CreateText(functionsPath + "\\mcscript\\tick.mcfunction");

			foreach(string scriptPath in scripts) {

				string scriptName = scriptPath.Split('\\')[^1];
				string functionName = scriptName.Replace(".mcsharp", ".mcfunction");
				string functionPath = $"{functionsPath}\\{functionName}";
				if(!scriptPath.EndsWith(".mcsharp")) continue;

				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine($"Compiling '{scriptName}'... ");

				using(StreamReader reader = File.OpenText(scriptPath)) {
					new ScriptFile(new ScriptString(reader.ReadToEnd(), scriptPath));
				}

				Console.CursorTop -= highestFunctionStackSize + 1;
				Program.ClearCurrentConsoleLine();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"Compiled '{scriptPath.Split('\\')[^1]}'.");
				Console.CursorTop += highestFunctionStackSize;

				highestFunctionStackSize = 0;

			}

			VariableScopes.TryGetValue(RootScope, out List<Variable> publicVariables);
			var allVariables = new List<Variable>();
			foreach(KeyValuePair<Scope, List<Variable>> pair in VariableScopes)
				foreach(Variable variable in pair.Value) allVariables.Add(variable);
			allVariables.Sort((a, b) => a.Order - b.Order);

			{
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Write("Compiling 'mcsript\\prep.mcfunction'...");

				PrepFunction.WriteLine($"function {Program.Datapack.Name}:mcscript/demo");
				foreach(Variable variable in allVariables) variable.WritePrep(PrepFunction);
				PrepFunction.WriteLine($"function {Program.Datapack.Name}:program/load");

				PrepFunction.Close();

				Program.ClearCurrentConsoleLine();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Compiled 'mcsript\\prep.mcfunction'.");
			}

			{
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Write("Compiling 'mcscript\\tick.mcfunction'...");

				TickFunction.WriteLine($"function {Program.Datapack.Name}:program/main");
				foreach(Variable variable in allVariables) variable.WriteTick(TickFunction);

				TickFunction.Close();

				Program.ClearCurrentConsoleLine();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Compiled 'mcsript\\tick.mcfunction'.");
			}

			{
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Write("Compiling 'mcscript\\demo.mcfunction'...");

				foreach(Variable variable in allVariables) variable.WriteDemo(DemoFunction);

				DemoFunction.Close();

				Program.ClearCurrentConsoleLine();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Compiled 'mcsript\\demo.mcfunction'.");
			}

		}

		public static Scope WriteFunction<TReturn>(Scope parent, Variable declarer, ScriptMethod method) where TReturn : Variable {
			return WriteFunction<TReturn>(parent, declarer, method, method.FilePath, method.FileName);
		}

		/// <summary>
		/// Compiles a new function file with the given <paramref name="functionPath"/> from the given <paramref name="method"/>.
		/// </summary>
		private static Scope WriteFunction<TReturn>(Scope parent, Variable declarer, ScriptMethod method, string path, string name) where TReturn : Variable {

			//string path = method.FilePath;
			//string name = method.FileName;
			CurrentScriptTrace = method.ScriptTrace;

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write($" · Writing '{name}'... ");
			highestFunctionStackSize++;

			string[] directorySplit = path.Split('\\')[..^1];
			string directory = "";
			foreach(string part in directorySplit) directory += '\\' + part;
			Directory.CreateDirectory(directory[1..]);
			StreamWriter writer = File.CreateText(path.Split("(", 2)[0]);

			FunctionStack.Push(writer);
			ScopeStack.Push(new Scope(parent, declarer, method));

#if DEBUG_OUT
			writer.Write($"# {method.FullAlias}@{CurrentScope.ID}\n");
#endif


			var lines = (IReadOnlyList<ScriptLine>)method;
			for(int i = 0; i < lines.Count; i++) {
				ScriptLine line = lines[i];
				if(line.Length == 0) continue; // Empty line.

				if(line[0].IsWord && Statement.Dictionary.TryGetValue(line[0], out Tuple<Statement.Reader, Statement.Writer> tuple)) {

					tuple.Item2.Invoke(line);

				} else {

					var args = new List<ScriptWild>();
					Action onFinish = null;
					var wilds = (IReadOnlyList<ScriptWild>)line;

					for(int j = 0; j < wilds.Count; j++) {
						var wild = wilds[j];
						if(wild == "") continue;
						if(onFinish == null) {

							//Look for STATIC CLASS OBJECT
							if(wild.IsWord && StaticClassObjects.TryGetValue((string)wild.Word, out Variable v0)
							&& j + 1 < wilds.Count && OperationDictionary.TryGetValue((string)wilds[j + 1].Word, out Operation operation)
							&& operation == Operation.Access) {
								onFinish = () => {
									v0.InvokeOperation(args[0].Word, args.ToArray()[1..]);
								};
								continue;
							}

							//Look for TYPE NAME
							if(wild.IsWord && Initializers.TryGetValue((string)wild.Word, out var initializer)) {
								onFinish = () => {
									initializer.Invoke(Access.Private, Usage.Default, args[0], CurrentScope, args[0].ScriptTrace);
									if(args.Count > 1) _ = ParseValue(new ScriptWild(args, " \\ ", ' '), CurrentScope);
								};
								continue;
							}

							//Look for VALUE
							Variable v1 = ParseValue(wild, CurrentScope);
							if(!StaticClassObjects.ContainsValue(v1)) {
								onFinish = () => {
									if(args.Count > 0) v1.InvokeOperation(args[0].Word, args.ToArray()[1..]);
									else throw new Exception("115002272020");
								};
								continue;
							}

							throw new SyntaxException($"Could not identify word '{(string)wild}'.", CurrentScriptTrace);

						} else {
							args.Add(wild);
						}
					}

					if(onFinish != null) onFinish.Invoke();
					CurrentScriptTrace = method.ScriptTrace;

				}

			}

			if(VariableScopes.TryGetValue(CurrentScope, out List<Variable> variables)) {
				for(int i = 0; i < variables.Count; i++) variables[i].WriteInit(FunctionStack.Peek());
				for(int i = 0; i < variables.Count; i++) variables[i].WriteDele(FunctionStack.Peek());
			}


			FunctionStack.Pop().Close();
			Scope outScope = ScopeStack.Pop();

			Program.ClearCurrentConsoleLine();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($" · Wrote '{name}'.");

			return outScope;

		}

		/// <summary>
		/// Parses MCSharp code into a <see cref="Variable"/>.
		/// </summary>
		/// <param name="wild">The MCSharp code to parse.</param>
		/// <param name="scope">Context.</param>
		/// <returns>Returns the value that was found.</returns>
		public static Variable ParseValue(ScriptWild wild, Scope scope) {

			Variable variable = null;
			ScriptWild[] wilds = wild.Array;
			bool @switch = false;
			ScriptWord? op = null;
			for(int i = 0; i < wilds.Length; i++) {
				ScriptWild current = wilds[i];
				if(@switch = !@switch) {

					Variable x = null;

					X: if(current.IsWord || x != null) {

						if(x == null) {

							string word = (string)current.Word;

							// Special case: "!" operator
							if(x == null && current.Word == "!") {
								ParseValue(wilds[i + 1], scope);
								variable = x.InvokeOperation(new ScriptString("!", "anon"), new ScriptWild[] { });
								if(wilds.Length == i + 2) return variable;
								goto VariableCheck;
							}

							// Special case: strings
							if(x == null && word.StartsWith('"') && word.EndsWith('"')) {
								x = new VarString(Access.Private, Usage.Default, GetNextHiddenID(), CurrentScope);
								((VarString)x).SetValue(word[1..^1]);
								goto VariableCheck;
							}

							// Special case: ints
							if(int.TryParse(current, out int _int)) {
								x = new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), CurrentScope);
								((PrimitiveType)x).SetValue(_int);
								goto VariableCheck;
							}

							// Special case: bools
							if(bool.TryParse(current, out bool _bool)) {
								x = new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), CurrentScope);
								((PrimitiveType)x).SetValue(_bool ? 1 : 0);
								goto VariableCheck;
							}

							// Special case: "new" operator
							// + unexpected operator check
							if(OperationDictionary.TryGetValue((string)current.Word, out Operation opr)) {
								if(opr == Operation.New) {
									if(i + 3 > wilds.Length) throw new SyntaxException("Incomplete 'new' operator.", current.ScriptTrace);
									else if(wilds[i + 1].IsWilds) throw new SyntaxException("Invalid use of 'new' operator.", current.ScriptTrace);
									else if(wilds[i + 2].IsWord) throw new SyntaxException("Invalid use of 'new' operator.", current.ScriptTrace);
									else {
										ScriptWord typeWord = wilds[i + 1].Word;
										ScriptWild argsWild = wilds[i + 2];
										if(argsWild.Block != "(\\)") throw new SyntaxException("Expected '('.", current.ScriptTrace);
										i += 2;
										Variable[] arguments = new Variable[argsWild.Count];
										for(int indx = 0; indx < argsWild.Count; indx++)
											arguments[indx] = ParseValue(argsWild[indx], scope);
										if(!Constructors.TryGetValue((string)typeWord, out Constructor constructor))
											throw new SyntaxException($"Could not find a constructor for type '{(string)typeWord}'.", typeWord.ScriptTrace);
										x = constructor.Invoke(new ArgumentInfo(arguments, argsWild.ScriptTrace));
										if(x == null) throw new InternalError("Constructor returned null.");
									}
								} else throw new SyntaxException("Unexpected operator when a value was expected.", current.ScriptTrace);
								goto VariableCheck;
							}

						}

						// Check if it's a variable.
						VariableCheck:  if(x != null || TryGetVariable((string)current.Word, scope, out x)) {
							// <<Is a Variable>>
							// Check if we have a variable already.
							if(variable != null) {
								// <<Yes Variable>>
								// Compile an operation.
								ScriptWord opWord = op.Value;
								if(BooleanOperators.Contains((string)opWord)) {
									if(variable is VarBool varBool || variable.TryCast(out varBool))
										variable = varBool.InvokeOperation(opWord, wild.Array[i..]);
									else throw new SyntaxException(
										$"Cannot cast '{variable}' into 'bool' for use in boolean operator '{(string)opWord}'.", opWord.ScriptTrace);
								} else {
									//TODO:  do '+' and '-' operators last
									//  Try grouping when there is a '+' or '-', but not when there is any other operator.
									//  What about booleans?
									Operation operation = OperationDictionary[(string)op.Value];
									OperationType type = OperationTypeDictionary[operation];
									switch(operation) {
										case Operation.Access:
											goto CheckMembers;
										case Operation.Set:
										case Operation.Add:
										case Operation.Subtract:
											// Do operation with priority first.
											ScriptWild[] priority = new ScriptWild[wilds.Length - (i + 0)];
											priority[0] = (ScriptWord)new ScriptString(x.ObjectName);
											wilds[(i + 1)..^0].CopyTo(priority, 1);
											x = ParseValue(new ScriptWild(priority, " \\ ", ' '), scope);
											i = wilds.Length - 1;
											goto Invoke;
										default:
											Invoke: variable = variable.InvokeOperation(operation, x, current.ScriptTrace);
											break;
									}
								}
							} else {
								// <<No Variable>>
								// Save the variable for the next operation.
								variable = x;
							}
						} else goto CheckMembers;
						continue;
						CheckMembers:
						{
							// <<Not a Variable>>
							// The only options are that this is an accessor operator chain or there is a syntax error.

							// The first item of an accessor operator chain is always a value.
							Variable root = variable;
							// The following items are always members.
							var chain = new LinkedList<ScriptWild>();
							// 'current' is the name of the first member.
							if(!current.IsWord) throw new SyntaxException($"Expected member name after '.' operator.", current.ScriptTrace);

							// Find the entire accessor operator chain.
							int lastAccessor = i - 1;
							bool AtEnd() {

								// We are either on a field/property access name or method access name.
								// Are we on a method access name?
								if(i + 1 < wilds.Length && wilds[i + 1].IsWilds && wilds[i + 1].Block == "(\\)") {
									// Add method name and arguments to chain.
									chain.AddLast(wilds[i]);
									chain.AddLast(wilds[++i]);
								} else {
									// Add field/property name to chain.
									chain.AddLast(wilds[i]);
                                }

								// Is there a next access operator?
								if(i + 1 < wilds.Length && wilds[i + 1].IsWord && (string)wilds[i + 1].Word == ".") {
									// Add the operator.
									chain.AddLast(wilds[++i]);
									// The chain continues!
									return false;
                                } else {
									// The chain ends.
									return true;
                                }

							}
							while(!AtEnd()) ;

							// Invoke the access operation.
							variable = variable.InvokeOperation(op.Value, chain.ToArray());

						}
						continue;
					} else {
						// Make recurive call to find the value of the block.
						x = ParseValue(current, scope);
						goto X;
					}
				} else {
					if(current.IsWord) op = current.Word;
					else throw new SyntaxException("Unexpected block when an operator was expected.", CurrentScriptTrace);
					continue;
				}
			}

			return variable;

		}

		/// <summary>
		/// 
		/// </summary>
		public static bool TryGetVariable(string name, Scope scope, out Variable variable) {
			object member = null;
			if(name == "this") return (variable = scope.DeclaringVariable) != null;
			variable = null;
			if(VariableNames.TryGetValue(name, out Dictionary<Scope, Variable> dictionary)) {
				int min = int.MaxValue;
				foreach(KeyValuePair<Scope, Variable> pair in dictionary) {
					Scope s = pair.Key;
					Variable v = pair.Value;
					scope.IsChildOf(s, out int d);
					if(d < min) {
						variable = v;
						min = d;
					}
				}
			} else if(scope.DeclaringVariable?.TryGetMember(name, out member) ?? false) {

				if(member is Variable field) {
					// <<Accessing Field>>
					return (variable = field) != null;

				} else if(member is (GetProperty get, SetProperty set)) {
					// <<Accessing Property>>
					return false;

				} else if(member is MethodDelegate method) {
					// <<Accessing Method>>
					return false;

				}

			}
			return variable != null;
		}

		public static void Reset() {

			//Clear casters.
			Variable.Casters.Clear();

			//Clear constructors.
			VarGeneric.CompiledConstructors.Clear();

			//Reset anon script trace.
			AnonScriptTrace = new ScriptTrace(Program.ScriptsFolder + "TRACE_LOST", 0);

			//Clear static class objects from last compile.
			StaticClassObjects.Clear();

			//Clear files from last compile.
			ScriptFile.Files.Clear();

			//Clear types from last compile.
			Datatypes.Clear();
			Initializers.Clear();
			Constructors.Clear();

			//Reset objective ids.
			VarObjective.ResetID();

			//Clear scopes from last compile.
			nextScopeID = 0;
			allScopes.Clear();
			ScopeStack.Clear();
			ScopeStack.Push(RootScope = new Scope(null));

			//Clear stack from last compile.
			FunctionStack.Clear();

			//Clear variables from last compile.
			VariableNames.Clear();
			VariableScopes.Clear();
			ResetHiddenID();

			//Clear loaded statements from last compile.
			Statement.Dictionary.Clear();

			//Get executing assembly.
			var assembly = Assembly.GetExecutingAssembly();

			//Find the Variable types from this assembly, and add them to 'Datatypes'.
			foreach(TypeInfo info in assembly.DefinedTypes) {
				if(info.IsSubclassOf(typeof(Variable)) && !info.IsAbstract) {
					ConstructorInfo constructor = info.GetConstructor(new Type[] { });
					if(IsIgnoredType(constructor.DeclaringType)) continue;
					var variable = constructor.Invoke(new object[] { }) as Variable;
					Datatypes.Add(variable.TypeName, info.AsType());
				}
			}

			//Find the Statement types from this assembly, and add them to 'Statement.Dictionary'.
			foreach(TypeInfo info in assembly.DefinedTypes) {
				if(info.IsSubclassOf(typeof(Statement)) && !info.IsAbstract) {
					ConstructorInfo constructor = info.GetConstructor(new Type[] { });
					_ = constructor.Invoke(new object[] { }) as Statement;
				}
			}

		}

		private static int nextScopeID = 0;
		public class Scope {

			private int nextInnerID = 0;
			private Scope parent;
			private readonly List<Scope> children = new List<Scope>();
			private readonly Variable declaringVariable;
			private readonly ScriptMethod declaringMethod;

			public int ID { get; }
			public Variable DeclaringVariable => declaringVariable ?? Parent?.DeclaringVariable ?? null;
			public ScriptObject DeclaringType => (DeclaringVariable as VarStruct)?.ScriptClass;
			public ScriptMethod DeclaringMethod => declaringMethod ?? Parent?.DeclaringMethod ?? null;
			public ICollection<Variable> Variables { get; } = new HashSet<Variable>();
			public IReadOnlyCollection<Scope> Children => children;
			public Scope Parent {
				get => parent;
				private set {
					parent?.children.Remove(this);
					value?.children.Add(this);
					parent = value;
				}
			}


			public Scope(Scope parent, Variable declaringVariable = null, ScriptMethod declaringMethod = null) {
				this.declaringVariable = declaringVariable;
				this.declaringMethod = declaringMethod;
				ID = GetNextScopeID();
				Parent = parent;
				allScopes.Add(this);
			}


			public bool IsChildOf(Scope scope, out int delta) {
				Scope parent = Parent;
				delta = -1;
				while(parent != null) {
					delta++;
					if(scope == parent) return true;
					else parent = parent.Parent;
				}
				return false;
			}

			public bool IsParentOf(Scope scope, out int delta) {
				static bool Recursion(Scope me, Scope scope, ref int delta) {
					delta++;
					foreach(Scope child in me.Children) {
						if(scope == child) return true;
						else if(Recursion(child, scope, ref delta)) return true;
					}
					return false;
				}
				delta = -1;
				return Recursion(this, scope, ref delta);
			}

			public static int GetNextScopeID() => nextScopeID++;
			public string GetNextInnerID() => BaseConverter.Convert(nextInnerID++, 62);

			public override string ToString() => BaseConverter.Convert(ID, 62);
			public override int GetHashCode() => ID.GetHashCode();

			public static string ToUnderscoredLowercase(string value) {

				LinkedList<char> chars = new LinkedList<char>(value);
				LinkedListNode<char> current = chars.First;

				while(current != null) {
					if(char.IsUpper(current.Value)) chars.AddBefore(current, '_');
					current.Value = char.ToLower(current.Value);
					current = current.Next;
				}

				return new string(chars.ToArray());

			}

		}

		public static void Add(this Dictionary<int, Scope> dictionary, Scope scope) => dictionary.Add(scope.ID, scope);

		public class SyntaxException : Exception {
			public SyntaxException(string message, ScriptTrace at) : base($"[{at}] {message}") { }
		}

		public class InternalError : Exception {
			public InternalError(string message, ScriptTrace at) : base($"[{at}] {message}") { }
			public InternalError(string message) : base(message) { }
		}

	}

}
