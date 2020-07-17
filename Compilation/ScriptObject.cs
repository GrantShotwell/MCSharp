﻿using MCSharp.Variables;
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
		: this(alias, dataType, GetMembers(alias, scriptClass), scriptClass.ScriptTrace, access, usage) { }
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

		public static Dictionary<string, ScriptMember> GetMembers(ScriptString className, ScriptString scriptClass) {
			var members = new Dictionary<string, ScriptMember>();

			var blocks = new Stack<string>();
			ScriptWild?[] words = new ScriptWild?[3];

			ScriptWord? alias = null, type = null;
			Variable[] parameters = null;
			Access? access = null;
			Usage? usage = null;

			void Rotate() {
				for(int i = words.Length - 1; i > 0; i--)
					words[i] = words[i - 1];
				words[0] = null;
			}

			for(int start = 0, end = 0; end < scriptClass.Length; end++) {

				ScriptChar current = scriptClass[end];
				if(char.IsWhiteSpace((char)current) || ScriptLine.IsBlockChar((char)current, out _)) {
					if(blocks.Count == 0) {
						if(end - 1 > start) {
							ScriptString s = scriptClass[start..end];
							if(!s.ContainsWhitespace()) {
								words[0] = (ScriptWord)s;
								Rotate();
								if(Compiler.AccessModifiers.TryGetValue(words[1] ?? "", out Access accessOut)) {
									if(access.HasValue) throw new Compiler.SyntaxException("Unexpected second access modifier.", scriptClass[end].ScriptTrace);
									else access = accessOut;
								} else if(Compiler.UsageModifiers.TryGetValue(words[1] ?? "", out Usage usageOut)) {
									if(usage.HasValue) throw new Compiler.SyntaxException("Unexpected second usage modifier.", scriptClass[end].ScriptTrace);
									else usage = usageOut;
								} else {
									if(Initializers.TryGetValue(words[1], out _)) {
										//if(type.HasValue) throw new Compiler.SyntaxException("Unexpected second type keyword.", scriptClass[end].ScriptTrace);
										//else type = words[0].Value.Word;
									} else {
										//if(alias = )
									}
								}
							}
						}
						start = end + 1;
					}
				}

				if(!char.IsWhiteSpace((char)current)) {
					switch((char)current) {

						case '{': {
							blocks.Push("{\\}");
							if(blocks.Count == 1) {
								start = end + 1;
								alias = words[1].Value.Word;
								type = alias.Value == (string)className ? null : (ScriptWord?)words[2].Value.Word;
							}
							Rotate();
							break;
						}

						case '}': {
							if(blocks.Pop() != "{\\}") throw new Compiler.SyntaxException("Expected '}'.", scriptClass[end].ScriptTrace);
							if(blocks.Count == 0) {
								if(parameters == null) {
									// <<Property>>
									ScriptString accessors = scriptClass[start..end];
									//Find get/set methods.
									ScriptMethod get = null, set = null;
									ScriptProperty.Accessors? accessor = null;
									var blks = new Stack<string>();
									for(int a = 0, b = 0; b < accessors.Length; b++) {
										var c = accessors[b];

										bool whitespace = char.IsWhiteSpace((char)c);
										if(blks.Count == 0 && (whitespace || ScriptLine.IsBlockChar((char)c, out _))) {

											if(b - a > 1) {
												ScriptString word = accessors[a..b];
												accessor = ((string)word) switch
												{ //this is certainly a feature that exists
													"get" => ScriptProperty.Accessors.Get,
													"set" => ScriptProperty.Accessors.Set,
													_ => throw new Compiler.SyntaxException(
													$"Expected 'get' or 'set', but got '{(string)word}'.", word.ScriptTrace),
												};
											}

											if(whitespace) {
												if(blks.Count == 0)
													a = b + 1;
												continue;
											}

										}

										if(ScriptLine.IsBlockCharStart((char)c, out string block)) {

											blks.Push(block);
											if(blks.Count == 1) {
												// <<Start of Accessor>>
												a = b + 1;
											}

										} else if(ScriptLine.IsBlockCharEnd((char)c, out block)) {

											if(blks.Count == 0) throw new Compiler.SyntaxException($"Unexpected '{block[2]}'.", c.ScriptTrace);
											string peek = blks.Peek();
											if(peek != block) throw new Compiler.SyntaxException($"Expected '{peek[2]}' but got '{block[2]}'.", c.ScriptTrace);
											else blks.Pop();
											if(blks.Count == 0) {
												// <<End of Accessor>>
												switch(accessor ?? throw new Exception("104503102020")) {
													case ScriptProperty.Accessors.Get: {
														if(get == null) get = new ScriptMethod($"get_{(string)alias}", (string)type, new Variable[] {
															// No parameters.
														}, null, accessors[a..b], Access.Private, usage ?? Usage.Default);
														else throw new Compiler.SyntaxException("Unexpected second 'get' accessor.", c.ScriptTrace);
														break;
													}
													case ScriptProperty.Accessors.Set: {
														if(set == null) set = new ScriptMethod($"set_{(string)alias}", (string)type, new Variable[] {
                                                            // 'value'
															Initializers[(string)type].Invoke(Access.Private, Usage.Default,
																GetNextHiddenID(), Compiler.CurrentScope, c.ScriptTrace)
														}, null, accessors[a..b], Access.Private, usage ?? Usage.Default);
														else throw new Compiler.SyntaxException("Unexpected second 'set' accessor.", c.ScriptTrace);
														break;
													}
													default: throw new Exception("104303102020");
												}
												a = b + 1;
											}

										}

									}
									members.Add(new ScriptProperty((string)alias, (string)type, get, set,
										access ?? Access.Private, usage ?? Usage.Default, null, alias.Value.ScriptTrace));
								} else {
									if(type.HasValue) {
										// <<Method>>
										members.Add(new ScriptMethod((string)alias, (string)type,
											parameters, null, scriptClass[start..end],
											access ?? Access.Private, usage ?? Usage.Default));
									} else {
										// <<Constructor>>
										members.Add(new ScriptConstructor(
											parameters, (string)className, scriptClass[start..end],
											access ?? Access.Private, usage ?? Usage.Default));
									}
								}
								parameters = null;
								alias = null;
								type = null;
								access = null;
								usage = null;
							}
							Rotate();
							break;
						}

						case '(': {
							blocks.Push("(\\)");
							if(blocks.Count == 1) {
								// <<Start of Parameters>>
								start = end + 1;
							}
							break;
						}

						case ')': {
							if(blocks.Pop() != "(\\)") throw new Compiler.SyntaxException("Expected ')'.", scriptClass[end].ScriptTrace);
							if(blocks.Count == 0) {
								// <<End of Parameters>>
								ScriptWild paramDefs = ScriptLine.GetWilds(scriptClass[(start - 1)..(end + 1)])[0];

								//Make sense of the parameter definition.
								if(paramDefs.FullBlockType == "(\\,\\)") {
									parameters = new Variable[paramDefs.Count];
								} else if(paramDefs.Count == 0) {
									parameters = new Variable[0];
									goto End;
								} else {
									parameters = new Variable[1];
									paramDefs = new ScriptWild(new ScriptWild[] { new ScriptWild(paramDefs.Array, " \\ ", ' ') }, "(\\)", ',');
								}

								//Create parameter variables.
								for(int i = 0; i < paramDefs.Wilds.Count; i++) {
									if(paramDefs.Wilds[i].IsWilds) {
										ScriptWild paramDef = paramDefs.Wilds[i];
										if(paramDef.Count != 2 || paramDef[0].IsWilds || paramDef[1].IsWilds)
											throw new Compiler.SyntaxException("Expected [type] [name].", paramDefs.Wilds[i].ScriptTrace);
										if(Initializers.TryGetValue(paramDef[0], out var compiler)) {
											parameters[i] = compiler.Invoke(Access.Private, Usage.Default,
												paramDef[1], Compiler.CurrentScope, paramDef[1].ScriptTrace);
										} else throw new Compiler.SyntaxException($"Unknown type '{type}'.", Compiler.CurrentScriptTrace);
									}
								}

								End: start = end + 1;
							}
							break;
						}

						case '=':
						case ';':
							if(blocks.Count == 0) {

								start = end--;
								alias = words[1].Value.Word;
								type = words[2].Value.Word;

								while((current = scriptClass[++end]) != ';' && end < scriptClass.Length) ;
								var field = new ScriptField((string)alias.Value, (string)type.Value,
									access ?? Access.Private, usage ?? Usage.Default, null, scriptClass[start..end]);
								members.Add(field);

								parameters = null;
								alias = null;
								type = null;
								access = null;
								usage = null;

							}
							break;

					}
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

			public ScriptField(string alias, string type, Access access, Usage usage, ScriptObject declarer, ScriptString phrase)
			: this(alias, type, access, usage, declarer, new ScriptWild(ScriptLine.GetWilds(phrase), "(\\)", ' ')) { }

			public ScriptField(string alias, string type, Access access, Usage usage, ScriptObject declarer, ScriptWild init)
			: base(alias, type, access, usage, declarer, init.ScriptTrace) {

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
			  Access access, Usage usage, ScriptObject declaringType, ScriptTrace trace)
			: base(alias, type, access, usage, declaringType, trace) {

				bool getNull = get is null, setNull = set is null;
				if(getNull && setNull) throw new Compiler.SyntaxException("Expected at least one accessor for property.", trace);
				if(!getNull && get.Parameters.Length != 0) throw new Exception("034503232020a"); //Get method should have no parameters.
				if(!setNull && set.Parameters.Length != 1) throw new Exception("034503232020b"); //Set method should have one parameter.

				GetMethod = get;
				GetFunc = getNull ? (GetProperty)null : (() => get.Delegate.Invoke(new Variable[] { }));
				SetMethod = set;
				SetFunc = setNull ? (SetProperty)null : ((x) => set.Delegate.Invoke(new Variable[] { x }));

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
				//[DebuggerStepThrough]
				get {
					if(returnValue != null) return returnValue;
					else if(Initializers.TryGetValue(TypeName, out Initializer initializer))
						return ReturnValue = initializer.Invoke(Access.Private, Usage.Default, GetNextHiddenID(), Scope, ScriptTrace);
					else throw new Compiler.SyntaxException($"Type '{TypeName}' could not be found.", ScriptTrace);
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
			public ScriptConstructor(Variable[] parameters, ScriptObject declarer, ScriptString script,
			  Access access = Access.Private, Usage usage = Usage.Default)
			: this(parameters, declarer, GetLines(script), script.ScriptTrace, access, usage) { }

			[DebuggerStepThrough]
			public ScriptConstructor(Variable[] parameters, ScriptObject declarer, ScriptWild wild,
			  Access access = Access.Private, Usage usage = Usage.Default)
			: this(parameters, declarer, GetLines(wild), wild.ScriptTrace, access, usage) { }

			[DebuggerStepThrough]
			private ScriptConstructor(Variable[] parameters, ScriptObject declarer, ScriptLine[] lines,
			  ScriptTrace trace, Access access, Usage usage)
			: base(declarer?.Alias, declarer?.Alias, parameters, declarer, lines, access, usage, trace) { }


			[DebuggerStepThrough]
			public ScriptConstructor(Variable[] parameters, string type, ScriptString script,
			  Access access = Access.Private, Usage usage = Usage.Default)
			: this(parameters, type, GetLines(script), script.ScriptTrace, access, usage) { }

			[DebuggerStepThrough]
			public ScriptConstructor(Variable[] parameters, string type, ScriptWild wild,
			  Access access = Access.Private, Usage usage = Usage.Default)
			: this(parameters, type, GetLines(wild), wild.ScriptTrace, access, usage) { }

			[DebuggerStepThrough]
			public ScriptConstructor(Variable[] parameters, string type, ScriptLine[] lines,
			  ScriptTrace trace, Access access = Access.Private, Usage usage = Usage.Default)
			: base(type, null, parameters, null, lines, access, usage, trace) { }


		}

		/// <summary>
		/// Represents an organized character structure of a method.
		/// </summary>
		[DebuggerDisplay("{Access.ToString().ToLower(),nq} {Usage.ToString().ToLower(),nq} {TypeName,nq} {FullAlias,nq}(params = {Parameters.Length,nq})")]
		public class ScriptMethod : ScriptMember, IReadOnlyList<ScriptLine> {

			protected ScriptLine[] ScriptLines { get; }
			public ScriptLine this[int index] => ScriptLines[index];
			public MethodDelegate Delegate { get; }
			public virtual Variable ReturnValue { get; protected set; }
			public Variable[] Parameters { get; }
			public int Length => ScriptLines.Length;

			public override ScriptObject DeclaringType {
				[DebuggerStepThrough]
				get => base.DeclaringType;
				[DebuggerStepThrough]
				set {
					if(value is null) return;
					value.Methods.Add(this);
					base.DeclaringType = value;
				}
			}

			int IReadOnlyCollection<ScriptLine>.Count => ((IReadOnlyList<ScriptLine>)ScriptLines).Count;


			[DebuggerStepThrough]
			public ScriptMethod(string alias, string type, Variable[] parameters, ScriptObject declaringType, ScriptString script,
			  Access access = Access.Private, Usage usage = Usage.Default)
			: this(alias, type, parameters, declaringType, GetLines(script), access, usage, script.ScriptTrace) { }

			[DebuggerStepThrough]
			public ScriptMethod(string alias, string type, Variable[] parameters, ScriptObject declaringType, ScriptWild wild,
			  Access access = Access.Private, Usage usage = Usage.Default)
			: this(alias, type, parameters, declaringType, GetLines(wild), access, usage, wild.ScriptTrace) { }

			public ScriptMethod(string alias, string type, Variable[] parameters, ScriptObject declaringType, ScriptLine[] phrases,
			  Access access, Usage usage, ScriptTrace trace)
			: base(alias, type, access, usage, declaringType, trace) {

				if(trace is null)
					throw new ArgumentNullException(nameof(trace));

				ScriptLines = phrases ?? throw new ArgumentNullException(nameof(phrases));
				Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));

				if(!(type is null)) {
					if(Initializers.TryGetValue(type, out Initializer init))
						ReturnValue = init.Invoke(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope, trace);
					else throw new Compiler.SyntaxException($"Type '{type}' does not exist.", phrases[0].ScriptTrace);
				}

				Delegate = (arguments) => {
					for(int i = 0; i < arguments.Length; i++) {
						Variable argument = arguments[i], parameter = Parameters[i];
						if(argument.GetType().IsAssignableFrom(parameter.GetType()) || argument.TryCast(parameter.GetType(), out argument)) {
							parameter.InvokeOperation(Operation.Set, argument, Compiler.AnonScriptTrace);
						} else throw new Variable.InvalidCastException(argument, parameter.TypeName, trace);
					}
					new Spy(null, $"function {GameName}", null);
					return ReturnValue;
				};

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
