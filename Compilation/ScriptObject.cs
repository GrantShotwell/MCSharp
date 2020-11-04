using MCSharp.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static MCSharp.Variables.Variable;
using System.Text.RegularExpressions;

namespace MCSharp.Compilation {

	[DebuggerDisplay("{FullAlias,nq}")]
	public class ScriptObject : IDictionary<string, ScriptMember> {

		public static Regex AliasRegex { get; } = new Regex("[(\\w)]");

		public enum DataType { Struct, Class }
		public static IReadOnlyDictionary<string, DataType> DataTypeDictionary { get; }
			= new Dictionary<string, DataType> {
				{ "struct", DataType.Struct },
				{ "class", DataType.Class }
			};

		#region Properties

		public DataType Type { get; }
		public VarGeneric StaticObject { get; }
		public MethodDictionary Methods { get; } = new MethodDictionary();

		private IDictionary<string, ScriptMember> Members { get; }
		public Access Access { get; }
		public Usage Usage { get; }

		public ScriptMember this[string key] {
			get => Members[key];
			set => Members[key] = value;
		}

		public ICollection<string> Keys => Members.Keys;
		public ICollection<ScriptMember> Values => Members.Values;
		public int Count => Members.Count;
		public bool IsReadOnly => Members.IsReadOnly;

		public string Alias { get; }
		public string FullAlias => Alias;
		public ScriptTrace ScriptTrace { get; }

		#endregion


		#region Constructors

		public ScriptObject(ScriptString alias, ScriptString dataType, ScriptString scriptClass, Access access = Access.Private, Usage usage = Usage.Default)
		: this(alias, dataType, GetMembers(alias, scriptClass, new Compiler.Scope(Compiler.CurrentScope)), scriptClass.ScriptTrace, access, usage) { }
		private ScriptObject(ScriptString alias, ScriptString dataType, Dictionary<string, ScriptMember> members, ScriptTrace race, Access access, Usage usage) {

			if(DataTypeDictionary.ContainsKey((string)dataType)) Type = DataTypeDictionary[(string)dataType];
			else throw new Compiler.SyntaxException($"'{(string)dataType}' is not a valid object data type.", dataType.ScriptTrace);
			if(AliasRegex.IsMatch((string)alias)) Alias = (string)alias;
			else throw new Compiler.SyntaxException($"'{(string)alias}' does not use word characters.", alias.ScriptTrace);
			Access = access;
			Usage = usage;
			Members = members;
			ScriptTrace = race;


			foreach(ScriptMember member in members.Values)
				member.DeclaringType = this;

			StaticObject = Type switch
			{
				DataType.Struct => new VarStruct(this),
				DataType.Class => new VarObject(this),
				_ => throw new Compiler.InternalError("123604212020"),
			};
		}

		#endregion


		#region Methods

		public static Dictionary<string, ScriptMember> GetMembers(ScriptString className, ScriptString scriptClass, Compiler.Scope scope) {
			var members = new Dictionary<string, ScriptMember>();

			bool IsStoppingCharacter(char character) {
				if(ScriptLine.IsBlockChar(character, out _)) return true;
				else if(ScriptLine.IsSeparatorChar(character)) return true;
				else return false;
			}

			void GetNextWord(ref int i, out ScriptWord word) {

				GotoNextChar(ref i);

				int start = i;
				char current = (char)scriptClass[i];
				while((!char.IsWhiteSpace(current) && !IsStoppingCharacter(current)) && (i + 1 < scriptClass.Length)) {
					current = (char)scriptClass[++i];
				}

				word = scriptClass[start..i--];

			}

			void GetInitValue(ref int i, out ScriptString init) {

				GotoNextChar(ref i);

				int start = i;
				char current = (char)scriptClass[i];
				while(current != ';') {
					if(i + 1 >= scriptClass.Length) throw new Compiler.SyntaxException("Missing semicolon for field declaration.", scriptClass[^1].ScriptTrace);
					current = (char)scriptClass[++i];
				}

				init = scriptClass[(start + 1)..i];

			}

			void GetBlockWilds(ref int i, string type, out ScriptString wilds) {

				GotoNextChar(ref i);

				int start = i;
				var stack = new Stack<string>();
				do {
					char current = (char)scriptClass[i++];
					if(ScriptLine.IsBlockCharStart(current, out string block)) {
						stack.Push(block);
					}
					if(ScriptLine.IsBlockCharEnd(current, out block)) {
						if(stack.Pop() != block) throw new Compiler.SyntaxException($"Unexpected '{current}'.", scriptClass[i].ScriptTrace);
						else if(stack.Count == 0) {
							if(block == type) break;
							else throw new Compiler.SyntaxException($"Expected '{type}' block.", scriptClass[i].ScriptTrace);
						}
					}
				} while(stack.Count > 0);

				wilds = scriptClass[start..i--];

			}

			void GetParameters(ref int i, Compiler.Scope scope, out ParameterInfo parameters) {

				GetBlockWilds(ref i, "(\\)", out ScriptString content);
				ScriptWild wilds = ScriptLine.GetWilds(content);

				if(wilds.FullBlockType == "(\\,\\)") {
					parameters = ParameterInfo.CreateFromWilds(wilds, scope);
				} else if(wilds.Block == "(\\)") {
					parameters = ParameterInfo.CreateFromWilds(new ScriptWild(wilds.Wilds, "(\\)", ','), scope);
				} else {
					throw new Compiler.SyntaxException("Expected method arguments to be in '(\\,\\)' format.", wilds.ScriptTrace);
				}

			}

			void GetCodeBlock(ref int i, out ScriptString content) {

				GetBlockWilds(ref i, "{\\}", out ScriptString wilds);
				content = wilds;

			}

			void GotoNextChar(ref int i) {
				if(i + 1 >= scriptClass.Length) {
					throw new Compiler.SyntaxException("Last member in type definition is incomplete.", scriptClass[^1].ScriptTrace);
				} else {
					char current = (char)scriptClass[++i];
					while(char.IsWhiteSpace(current) && (i + 1 < scriptClass.Length)) {
						current = (char)scriptClass[++i];
					}
				}
			}

			ScriptChar PreviewNextChar(int i) {
				GotoNextChar(ref i);
				return scriptClass[i];
			}

			// Looks like it loops through every character, but the function calls makes it a loop through every member.
			for(int i = 0; i < scriptClass.Length; i++) {

				if((char)PreviewNextChar(i) == ' ') break;

				Access access;
				Usage usage;
				ScriptWord type;
				ScriptWord? name;

				{
					Access? _access = null;
					Usage? _usage = null;
					ScriptWord? _type = null;
					ScriptWord? _name = null;

					// Get modifiers, type, and name.
					while(!_access.HasValue || !_usage.HasValue || !_type.HasValue || !_name.HasValue) {

						GetNextWord(ref i, out ScriptWord word);
						if(!_type.HasValue) {
							var next = PreviewNextChar(i);
							if(IsStoppingCharacter((char)next)) {
								if(!_access.HasValue) _access = Access.Private;
								if(!_usage.HasValue) _usage = Usage.Default;
								_type = word;
								break;
							}
						}

						if(!_access.HasValue && Compiler.AccessModifiers.TryGetValue((string)word, out Access __access)) {
							_access = __access;
						} else if(!_usage.HasValue && Compiler.UsageModifiers.TryGetValue((string)word, out Usage __usage)) {
							_usage = __usage;
						} else if(!_type.HasValue) {
							if(!_access.HasValue) _access = Access.Private;
							if(!_usage.HasValue) _usage = Usage.Default;
							_type = word;
						} else {
							_name = word;
						}

					}

					access = _access.Value;
					usage = _usage.Value;
					type = _type.Value;
					name = _name;
				}

				GotoNextChar(ref i);
				switch((char)scriptClass[i--]) {

					// Field (no init)
					case ';': {
						members.Add(new ScriptField((string)name.Value, (string)type, access, usage, null, new ScriptString(""), scope));
						break;
					}
					// Field (init)
					case '=': {
						GetInitValue(ref i, out ScriptString init);
						members.Add(new ScriptField((string)name.Value, (string)type, access, usage, null, init, scope));
						break;
					}

					// Property
					case '{': {
						ScriptMethod get = null, set = null;

						i += 1;
						while(PreviewNextChar(i) != '}') {

							GetNextWord(ref i, out ScriptWord word);
							if((string)word == "get") {
								if(get != null) throw new Compiler.SyntaxException("Cannot have two 'get' accessors in a proptery.", word.ScriptTrace);
								// Create 'get' accessor.
								GetCodeBlock(ref i, out ScriptString block);
								Compiler.Scope _scope = new Compiler.Scope(scope);
								get = new ScriptMethod($"get_{(string)name.Value}", (string)type, ParameterInfo.CreateForGetAccessor(type, _scope), null, block[1..^1], _scope, true, access, usage);
								_scope.DeclaringMethod = get;
							} else if((string)word == "set") {
								if(set != null) throw new Compiler.SyntaxException("Cannot have two 'set' accessors in a proptery.", word.ScriptTrace);
								// Create 'set' accessor.
								GetCodeBlock(ref i, out ScriptString block);
								Variable value = Initializers[(string)type](Access.Pass, Usage.Parameter, "value", new Compiler.Scope(scope), word.ScriptTrace);
								value.ConstructAsPasser();
								Compiler.Scope _scope = new Compiler.Scope(scope);
								set = new ScriptMethod($"set_{(string)name.Value}", (string)type, ParameterInfo.CreateForSetAccessor(type, _scope), null, block[1..^1], _scope, true, access, usage);
								_scope.DeclaringMethod = set;
							} else {
								// Throw error.
								throw new Compiler.SyntaxException($"Unexpected '{(string)word}' in property definition (expected 'get' or 'set').", word.ScriptTrace);
							}

						}
						GotoNextChar(ref i);
						i += 1;
						members.Add(new ScriptProperty((string)name.Value, (string)type, get, set, access, usage, null, name.Value.ScriptTrace, scope));
						break;
					}

					// Method or Constructor
					case '(': {
						if(name.HasValue) {
							// Method
							Compiler.Scope _scope = new Compiler.Scope(scope);
							GetParameters(ref i, _scope, out ParameterInfo parameters);
							GetCodeBlock(ref i, out ScriptString content);
							var method = new ScriptMethod((string)name.Value, (string)type, parameters, null, content[1..^1], _scope, false, access, usage);
							_scope.DeclaringMethod = method;
							members.Add(method);
							break;
						} else {
							// Constructor
							Compiler.Scope _scope = new Compiler.Scope(scope);
							GetParameters(ref i, _scope, out ParameterInfo parameters);
							GetCodeBlock(ref i, out ScriptString content);
							var constructor = new ScriptConstructor(parameters, (string)type, content[1..^1], _scope, access, usage);
							_scope.DeclaringMethod = constructor;
							members.Add(constructor);
							break;
						}
					}

					default: throw new Compiler.SyntaxException("There was a problem parsing this type member definition.", scriptClass[i + 1].ScriptTrace);
				}

			}

			return members;
		}



		public void Add(string key, ScriptMember value) => Members.Add(key, value);
		public bool ContainsKey(string key) => Members.ContainsKey(key);
		public bool Remove(string key) => Members.Remove(key);
		public bool TryGetValue(string key, [MaybeNullWhen(false)] out ScriptMember value) => Members.TryGetValue(key, out value);
		public void Add(KeyValuePair<string, ScriptMember> item) => Members.Add(item);
		public void Clear() => Members.Clear();
		public bool Contains(KeyValuePair<string, ScriptMember> item) => Members.Contains(item);
		public void CopyTo(KeyValuePair<string, ScriptMember>[] array, int arrayIndex) => Members.CopyTo(array, arrayIndex);
		public bool Remove(KeyValuePair<string, ScriptMember> item) => Members.Remove(item);
		public IEnumerator<KeyValuePair<string, ScriptMember>> GetEnumerator() => Members.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => Members.GetEnumerator();

		#endregion


		#region Types

		#region Attributes

		/// <summary>
		/// Represents an organized character structure of an attribute.
		/// </summary>
		//[DebuggerDisplay("todo")]
		public class ScriptAttribute {

			public string TypeName { get; }
			public ScriptWild Arguments { get; }
			public ScriptObject Parent { get; }

			public ScriptAttribute(string type, ScriptWild arguments, ScriptObject parent) {

				TypeName = type;
				Arguments = arguments;
				Parent = parent;

			}

		}

		#endregion

		#region Members

		/// <summary>
		/// Represents an organized character structure of a field.
		/// </summary>
		[DebuggerDisplay("{Access.ToString().ToLower(),nq} {Usage.ToString().ToLower(),nq} {TypeName,nq} {FullAlias,nq}")]
		public class ScriptField : ScriptMember {

			/// <summary>The initial value to evaluate this field as when the object is created.</summary>
			public ScriptWild Init { get; }

			public ScriptField(string alias, string type, Access access, Usage usage, ScriptObject declarer, ScriptString phrase, Compiler.Scope scope)
			: this(alias, type, access, usage, declarer, new ScriptWild(ScriptLine.GetWilds(phrase).Array, "(\\)", ' '), scope) { }

			public ScriptField(string alias, string type, Access access, Usage usage, ScriptObject declarer, ScriptWild init, Compiler.Scope scope)
			: base(alias, type, access, usage, declarer, init.ScriptTrace, scope) {

				Init = init;

			}

		}

		/// <summary>
		/// Represents an organized character structure of a property.
		/// </summary>
		[DebuggerDisplay("{Access.ToString().ToLower(),nq} {Usage.ToString().ToLower(),nq} {TypeName,nq} {FullAlias,nq}")]
		public class ScriptProperty : ScriptMember {

			/// <summary>The accessors of a property.</summary>
			public enum Accessors { Get, Set }

			/// <summary>The <see cref="ScriptMethod"/> for the 'get' accessor.</summary>
			public ScriptMethod GetMethod { get; }
			/// <summary>The delegate for the 'get' accessor.</summary>
			public GetProperty GetFunc { get; }

			/// <summary>The <see cref="ScriptMethod"/> for the 'set' accessor.</summary>
			public ScriptMethod SetMethod { get; }
			/// <summary>The delegate for the 'set' accessor.</summary>
			public SetProperty SetFunc { get; }

			/// <summary>The <see cref="ScriptObject"/> that defines this property.</summary>
			public override ScriptObject DeclaringType {
				get => base.DeclaringType;
				set {
					base.DeclaringType = value;
					if(!(GetMethod is null)) GetMethod.DeclaringType = DeclaringType;
					if(!(SetMethod is null)) SetMethod.DeclaringType = DeclaringType;
				}
			}

			public ScriptProperty(string alias, string type, ScriptMethod get, ScriptMethod set,
			  Access access, Usage usage, ScriptObject declaringType, ScriptTrace trace, Compiler.Scope scope)
			: base(alias, type, access, usage, declaringType, trace, scope) {

				bool getNull = get is null, setNull = set is null;
				if(getNull && setNull) throw new Compiler.SyntaxException("Expected at least one accessor for property.", trace);
				if(!getNull && get.Parameters.Count != 0) throw new Exception("034503232020a"); //Get method should have no parameters.
				if(!setNull && set.Parameters.Count != 1) throw new Exception("034503232020b"); //Set method should have one parameter.

				GetMethod = get;
				GetFunc = getNull ? (GetProperty)null : () => 
				get.Delegate.Invoke(new ArgumentInfo(new Variable[] { }, trace));
				SetMethod = set;
				SetFunc = setNull ? (SetProperty)null : (x) => 
				set.Delegate.Invoke(new ArgumentInfo(new Variable[] { x }, trace));

				if(!getNull) GetMethod.DeclaringType = declaringType;
				if(!setNull) SetMethod.DeclaringType = declaringType;

			}

		}

		/// <summary>
		/// Represents an organized character struture of a constructor.
		/// </summary>
		[DebuggerDisplay("{Access.ToString().ToLower(),nq} {Usage.ToString().ToLower(),nq} {TypeName,nq}([{Parameters.Length,nq}])")]
		public class ScriptConstructor : ScriptMethod {

			private Variable returnValue;

			public override Variable ReturnValue {
				[DebuggerStepThrough]
				get {
					if(returnValue != null) return returnValue;
					else if(Initializers.TryGetValue(TypeName, out Initializer initializer)) {
						ReturnValue = initializer.Invoke(Access.Pass, Usage.Return, GetNextHiddenID(), Scope, ScriptTrace);
						ReturnValue.ConstructAsPasser();
						return ReturnValue;
					} else throw new Compiler.SyntaxException($"Type '{TypeName}' could not be found.", ScriptTrace);
				}
				[DebuggerStepThrough]
				protected set {
					if(returnValue == null) returnValue = value;
					else throw new FakeReadonlyException(true);
				}
			}

			public override string TypeName {
				[DebuggerStepThrough]
				get => typeName != null ? base.TypeName : (TypeName = DeclaringType.FullAlias);
				[DebuggerStepThrough]
				set => base.TypeName = value;
			}


			[DebuggerStepThrough]
			public ScriptConstructor(ParameterInfo parameters, ScriptObject declarer, ScriptString script, Compiler.Scope scope,
			  Access access = Access.Private, Usage usage = Usage.Default)
			: this(parameters, declarer, GetLines(script), script.ScriptTrace, scope, access, usage) { }

			[DebuggerStepThrough]
			public ScriptConstructor(ParameterInfo parameters, ScriptObject declarer, ScriptWild wild, Compiler.Scope scope,
			  Access access = Access.Private, Usage usage = Usage.Default)
			: this(parameters, declarer, GetLines(wild), wild.ScriptTrace, scope, access, usage) { }

			[DebuggerStepThrough]
			private ScriptConstructor(ParameterInfo parameters, ScriptObject declarer, ScriptLine[] lines,
			  ScriptTrace trace, Compiler.Scope scope, Access access, Usage usage)
			: base(declarer?.Alias, declarer?.Alias, parameters, declarer, lines, scope, true, access, usage, trace) { }


			[DebuggerStepThrough]
			public ScriptConstructor(ParameterInfo parameters, string type, ScriptString script, Compiler.Scope scope,
			  Access access = Access.Private, Usage usage = Usage.Default)
			: this(parameters, type, GetLines(script), script.ScriptTrace, scope, access, usage) { }

			[DebuggerStepThrough]
			public ScriptConstructor(ParameterInfo parameters, string type, ScriptWild wild, Compiler.Scope scope,
			  Access access = Access.Private, Usage usage = Usage.Default)
			: this(parameters, type, GetLines(wild), wild.ScriptTrace, scope, access, usage) { }

			[DebuggerStepThrough]
			public ScriptConstructor(ParameterInfo parameters, string type, ScriptLine[] lines,
			  ScriptTrace trace, Compiler.Scope scope, Access access = Access.Private, Usage usage = Usage.Default)
			: base(type, null, parameters, null, lines, scope, true, access, usage, trace) { }


		}

		/// <summary>
		/// Represents an organized character structure of a method.
		/// </summary>
		[DebuggerDisplay("{Access.ToString().ToLower(),nq} {Usage.ToString().ToLower(),nq} {TypeName,nq} {FullAlias,nq}(params = {Parameters.Length,nq})")]
		public class ScriptMethod : ScriptMember, IReadOnlyList<ScriptLine> {

			public ScriptLine[] ScriptLines { get; }
			public ScriptLine this[int index] => ScriptLines[index];
			public MethodDelegate Delegate { get; }
			public virtual Variable ReturnValue { get; protected set; }
			public ParameterInfo Parameters { get; }
			public int Length => ScriptLines.Length;
			public bool Written { get; set; } = false;

			public override ScriptObject DeclaringType {
				[DebuggerStepThrough]
				get => base.DeclaringType;
				[DebuggerStepThrough]
				set {
					if(value is null) return;
					base.DeclaringType = value;
					value.Methods.Add(this);
				}
			}

			int IReadOnlyCollection<ScriptLine>.Count => ((IReadOnlyList<ScriptLine>)ScriptLines).Count;


			[DebuggerStepThrough]
			public ScriptMethod(string alias, string type, ParameterInfo parameters, ScriptObject declaringType, ScriptString script, Compiler.Scope scope,
			  bool inline = false, Access access = Access.Private, Usage usage = Usage.Default)
			: this(alias, type, parameters, declaringType, GetLines(script), scope, inline, access, usage, script.ScriptTrace) { }

			[DebuggerStepThrough]
			public ScriptMethod(string alias, string type, ParameterInfo parameters, ScriptObject declaringType, ScriptWild wild, Compiler.Scope scope,
			  bool inline = false, Access access = Access.Private, Usage usage = Usage.Default)
			: this(alias, type, parameters, declaringType, GetLines(wild), scope, inline, access, usage, wild.ScriptTrace) { }

			public ScriptMethod(string alias, string type, ParameterInfo parameters, ScriptObject declaringType, ScriptLine[] phrases, Compiler.Scope scope,
			  bool inline, Access access, Usage usage, ScriptTrace trace)
			: base(alias, type, access, usage, declaringType, trace, scope) {

				if(trace is null)
					throw new ArgumentNullException(nameof(trace));

				ScriptLines = phrases ?? throw new ArgumentNullException(nameof(phrases));
				Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));

				if(!(type is null)) {
					if(Initializers.TryGetValue(type, out Initializer init))
						ReturnValue = init.Invoke(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope, trace);
					else throw new Compiler.SyntaxException($"Type '{type}' does not exist.", phrases[0].ScriptTrace);
				}

				if(inline) {
					Delegate = (arguments) => {
						Parameters.Grab(arguments);
						Compiler.WriteFunction<Variable>(Scope.Parent, Scope.Parent.DeclaringVariable, this, true);
						return ReturnValue;
					};
				} else {
					Delegate = (arguments) => {
						if(Written = !Written) Compiler.WriteFunction<Variable>(Scope.Parent, Scope.Parent.DeclaringVariable, this, false);
						Parameters.Grab(arguments);
						new Spy(null, $"function {GameName}", null);
						return ReturnValue;
					};
				}

			}

			public IEnumerator<ScriptLine> GetEnumerator() => ((IReadOnlyList<ScriptLine>)ScriptLines).GetEnumerator();
			IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<ScriptLine>)ScriptLines).GetEnumerator();

		}

		#endregion

		#region Dictionaries

		public class MethodDictionary : IDictionary<string, HashSet<ScriptMethod>> {

			readonly Dictionary<string, HashSet<ScriptMethod>> dictionary = new Dictionary<string, HashSet<ScriptMethod>>();

			public HashSet<ScriptMethod> this[string key] {
				get => dictionary[key];
				set => dictionary[key] = value;
			}

			ICollection<string> IDictionary<string, HashSet<ScriptMethod>>.Keys => dictionary.Keys;
			ICollection<HashSet<ScriptMethod>> IDictionary<string, HashSet<ScriptMethod>>.Values => dictionary.Values;
			int ICollection<KeyValuePair<string, HashSet<ScriptMethod>>>.Count => dictionary.Count;
			bool ICollection<KeyValuePair<string, HashSet<ScriptMethod>>>.IsReadOnly => ((IDictionary<string, HashSet<ScriptMethod>>)dictionary).IsReadOnly;

			public bool Remove(ScriptMethod method) => dictionary.Remove(method.Alias);
			public void Add(ScriptMethod method) {
				if(!dictionary.TryGetValue(method.Alias, out HashSet<ScriptMethod> collection))
					dictionary.Add(method.Alias, collection = new HashSet<ScriptMethod>());
				collection.Add(method);
			}

			public bool TryGetValue(string key, [MaybeNullWhen(false)] out HashSet<ScriptMethod> value) {
				return dictionary.TryGetValue(key, out value);
			}

			void IDictionary<string, HashSet<ScriptMethod>>.Add(string key, HashSet<ScriptMethod> value)
				=> dictionary.Add(key, value);
			void ICollection<KeyValuePair<string, HashSet<ScriptMethod>>>.Add(KeyValuePair<string, HashSet<ScriptMethod>> item)
				=> ((IDictionary<string, HashSet<ScriptMethod>>)dictionary).Add(item);
			void ICollection<KeyValuePair<string, HashSet<ScriptMethod>>>.Clear()
				=> dictionary.Clear();
			bool ICollection<KeyValuePair<string, HashSet<ScriptMethod>>>.Contains(KeyValuePair<string, HashSet<ScriptMethod>> item)
				=> ((IDictionary<string, HashSet<ScriptMethod>>)dictionary).Contains(item);
			bool IDictionary<string, HashSet<ScriptMethod>>.ContainsKey(string key)
				=> dictionary.ContainsKey(key);
			void ICollection<KeyValuePair<string, HashSet<ScriptMethod>>>.CopyTo(KeyValuePair<string, HashSet<ScriptMethod>>[] array, int arrayIndex)
				=> ((IDictionary<string, HashSet<ScriptMethod>>)dictionary).CopyTo(array, arrayIndex);
			bool IDictionary<string, HashSet<ScriptMethod>>.Remove(string key)
				=> dictionary.Remove(key);
			bool ICollection<KeyValuePair<string, HashSet<ScriptMethod>>>.Remove(KeyValuePair<string, HashSet<ScriptMethod>> item)
				=> ((IDictionary<string, HashSet<ScriptMethod>>)dictionary).Remove(item);
			IEnumerator IEnumerable.GetEnumerator()
				=> ((IDictionary<string, HashSet<ScriptMethod>>)dictionary).GetEnumerator();
			IEnumerator<KeyValuePair<string, HashSet<ScriptMethod>>> IEnumerable<KeyValuePair<string, HashSet<ScriptMethod>>>.GetEnumerator()
				=> ((IDictionary<string, HashSet<ScriptMethod>>)dictionary).GetEnumerator();
		}

		#endregion

		#endregion


	}
}
