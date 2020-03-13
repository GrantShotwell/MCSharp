using MCSharp.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MCSharp.Compilation {

	public class ScriptClass : IDictionary<string, ScriptMember> {


		#region Properties

		public VarGeneric StaticObject { get; }
		public MethodDictionary Overloads { get; } = new MethodDictionary();

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

		public ScriptClass(string alias, ScriptString scriptClass, Access access = Access.Private, Usage usage = Usage.Default)
		: this(alias, GetMembers(alias, scriptClass), scriptClass.ScriptTrace, access, usage) { }
		private ScriptClass(string alias, Dictionary<string, ScriptMember> members, ScriptTrace scriptTrace, Access access, Usage usage) {

			Alias = alias;
			Access = access;
			Usage = usage;
			Members = members;
			ScriptTrace = scriptTrace;
			StaticObject = new VarGeneric(this);

			foreach(ScriptMember member in members.Values) {
				if(member is ScriptMethod method) method.DeclaringType = this;
			}

		}

		#endregion


		#region Methods

		public static Dictionary<string, ScriptMember> GetMembers(string root, ScriptString scriptClass) {
			var members = new Dictionary<string, ScriptMember>();

			var blocks = new Stack<string>();
			ScriptWild?[] words = new ScriptWild?[3] { null, null, null };

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
									if(Variable.Compilers.TryGetValue(words[1], out _)) {
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
								type = words[2].Value.Word;
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
														if(get == null) get = new ScriptMethod($"get_{(string)alias}", (string)type, new Variable[] { },
															null, accessors[a..b], Access.Private, usage ?? Usage.Default);
														else throw new Compiler.SyntaxException("Unexpected second 'get' accessor.", c.ScriptTrace);
														break;
													}
													case ScriptProperty.Accessors.Set: {
														if(set == null) set = new ScriptMethod($"set_{(string)alias}", (string)type, new Variable[] {
                                                            /* TODO! */
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
									members.Add(new ScriptProperty(root + "\\" + (string)alias, (string)type, get, set,
										access ?? Access.Private, usage ?? Usage.Default, null, alias.Value.ScriptTrace));
								} else {
									// <<Method>>
									members.Add(new ScriptMethod(root + "\\" + (string)alias, (string)type,
										parameters, null, scriptClass[start..end],
										access ?? Access.Private, usage ?? Usage.Default));
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
								// <<Start of Method Parameters>>
								start = end + 1;
							}
							break;
						}

						case ')': {
							if(blocks.Pop() != "(\\)") throw new Compiler.SyntaxException("Expected ')'.", scriptClass[end].ScriptTrace);
							if(blocks.Count == 0) {
								// <<End of Method Parameters>>
								var wilds = ScriptLine.GetWilds(scriptClass[(start - 1)..(end + 1)])[0];
								if(wilds.FullBlockType == "(\\,\\)") {
									parameters = new Variable[wilds.Count];
								} else if(wilds.Count == 0) {
									parameters = new Variable[0];
									goto End;
								} else {
									parameters = new Variable[1];
									wilds = new ScriptWild(new ScriptWild[] { new ScriptWild(wilds.Array, " \\ ", ' ') }, "(\\)", ',');
								}
								for(int i = 0; i < wilds.Wilds.Count; i++) {
									if(wilds.Wilds[i].IsWilds) {
										var ws = wilds.Wilds[i].Wilds;
										if(ws.Count != 2 || ws[0].IsWilds || ws[1].IsWilds)
											throw new Compiler.SyntaxException("Expected [type] [name].", wilds.Wilds[i].ScriptTrace);
										if(Variable.Compilers.TryGetValue(ws[0], out var compiler)) {
											parameters[i] = compiler.Invoke(Access.Private, Usage.Default, ws[1], Compiler.CurrentScope, ws[1].ScriptTrace);
										} else throw new Compiler.SyntaxException($"Unknown type '{type}'.", Compiler.CurrentScriptTrace);
									}
								}
								End: start = end + 1;
							}
							break;
						}

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


	}
}
