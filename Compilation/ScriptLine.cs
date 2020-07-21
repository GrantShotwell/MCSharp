using MCSharp.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Transactions;

namespace MCSharp.Compilation {

	public struct ScriptLine : IReadOnlyList<ScriptWild> {

		public static string[] BlockTypes { get; } = new string[] { "{\\}", "[\\]", "(\\)" };
		public static char[] SeparatorTypes { get; } = new char[] { ',', ';', ' ' };
		const int maxOperatorSize = 2;
		public static IReadOnlyList<char> BlockTypesStart {
			get {
				var starts = new char[BlockTypes.Length];
				for(int i = 0; i < starts.Length; i++)
					starts[i] = BlockTypes[i].Split('\\')[0][0];
				return starts;
			}
		}
		public static IReadOnlyList<char> BlockTypesEnd {
			get {
				var ends = new char[BlockTypes.Length];
				for(int i = 0; i < ends.Length; i++)
					ends[i] = BlockTypes[i].Split('\\')[1][0];
				return ends;
			}
		}

		private readonly ScriptWild[] wilds;
		public readonly string str;

		public ScriptWild this[int index] {
			[DebuggerStepThrough]
			get => wilds[index];
		}

		public int Length => wilds.Length;
		int IReadOnlyCollection<ScriptWild>.Count => Length;
		public ScriptTrace ScriptTrace { get; }


		public ScriptLine(ScriptString line) : this(GetWilds(line)) { }

		public ScriptLine(ScriptWild wild) {

			wilds = wild.Array;
			ScriptTrace = wild.ScriptTrace;

			int length = wilds.Length;
			string[] array = new string[length];
			for(int i = 0; i < length; i++) array[i] = wilds[i];
			string str = "";
			foreach(string s in array)
				str += " " + s;
			this.str = str.Length > 0 ? str[1..] : "";

		}

		public ScriptLine(ScriptWild[] wilds) {

			this.wilds = new ScriptWild[wilds.Length];
			wilds.CopyTo(this.wilds, 0);
			ScriptTrace = wilds.Length == 0 ? Compiler.AnonScriptTrace : wilds[0].ScriptTrace;

			int length = wilds.Length;
			string[] array = new string[length];
			for(int i = 0; i < length; i++) array[i] = wilds[i];
			string str = "";
			foreach(string s in array)
				str += " " + s;
			this.str = str.Length > 0 ? str[1..] : "";

		}


		public static ScriptWild GetWilds(ScriptString phrase) {

			ScriptString[] split = Separate(phrase);

			var stack = new Stack<(char? Separator, string Block, List<ScriptWild> List)>();
			stack.Push((null, " \\ ", new List<ScriptWild>()));

			for(int i = 0; i < split.Length; i++) {
				ScriptString str = split[i];

				if(str.Length == 1) {

					char character = (char)str[0];

					// Is this the start of a new block?
					if(IsBlockCharStart(character, out string block)) {
						var current = stack.Peek();

                        // Is the current block empty?
                        if(current.Block == " \\ " && current.List.Count == 0) stack.Pop();
						// Starting a new block.
                        stack.Push((null, block, new List<ScriptWild>()));
						
						continue;

					}
					
					// Is it separation time?
					if(IsSeparatorChar(character)) {
						var current = stack.Pop();
						(char? Separator, string Block, List<ScriptWild> List)? under = stack.Count == 0 ? ((char? Separator, string Block, List<ScriptWild> List)?)null : stack.Peek();
						stack.Push(current);

						// Does the 'under' block have the same separator?
						if(under.HasValue && under.Value.Separator == character) {

							// Add 'current' to 'under'.
							current = stack.Pop();
							under.Value.List.Add(new ScriptWild(current.List.ToArray(), current.Block, current.Separator ?? ' '));
							// Start the next item in the separated list by adding an empty item.
							stack.Push((' ', " \\ ", new List<ScriptWild>()));

							continue;

                        }

						// Does the current block have no separator?
						if(current.Separator == null) {

							// Set the separator to this one.
							current = stack.Pop();
							stack.Push((character, current.Block, current.List));
							// Start the next item in the separated list by adding an empty item.
							stack.Push((' ', " \\ ", new List<ScriptWild>()));
							
							continue;

                        }

						// The current block is a different separated list.
                        {

							// Is the current block an item in an unfinished separated block?
							current = stack.Pop();
                            if(stack.Count == 0 || stack.Peek().Block != " \\ ") {

								// Combine the current block into it.
								stack.Push((character, current.Block, new List<ScriptWild>()));
                                stack.Peek().List.Add(new ScriptWild(current.List.ToArray(), " \\ ", current.Separator ?? ' '));
								// Start the next item in the separated list by adding an empty item.
								stack.Push((' ', " \\ ", new List<ScriptWild>()));

								continue;
                            }
							stack.Push(current);
							
							// Can we set the separator of this block to this separator?
							if(current.Block == " \\ " && current.List.Count >= 1) {

								// Set the separator of the current block to this separator.
								current = stack.Pop();
                                stack.Push((character, current.Block, current.List));
								// Start the next item in the separated list by adding an empty item.
								stack.Push((' ', " \\ ", new List<ScriptWild>()));

								continue;
                            }

							// Is the current block's list combinable into one?
							if(current.Separator == ' ') {

								// Add 'current' into a new separated block.
								current = stack.Pop();
								(char? Separator, string Block, List<ScriptWild> List) next = (character, current.Block, new List<ScriptWild>());
								next.List.Add(new ScriptWild(current.List.ToArray(), " \\ ", current.Separator ?? ' '));
								stack.Push(next);
								// Start the next item in the separated list by adding an empty block.
								stack.Push((null, " \\ ", new List<ScriptWild>()));

								continue;
                            }

							throw new Exception();

                        }


					}
					
					// Is this the end of a block?
					if(IsBlockCharEnd(character, out block)) {

						// The current block is the next item in the block we are ending.
						var item = stack.Pop();
						(char? Separator, string Block, List<ScriptWild> List) list;
						if(stack.Count == 0) list = (null, " \\ ", new List<ScriptWild>());
						else list = stack.Pop();
						list.List.Add(new ScriptWild(item.List.ToArray(), item.Block, item.Separator ?? ' '));

						// Remove empty item, if any.
						if(list.List.Count > 0) {
							var last = list.List[^1];
							if(last.IsWord) {
								if((string)last.Word == string.Empty)
									list.List.RemoveAt(list.List.Count - 1);
                            } else {
								if(last.Count == 0 && last.FullBlockType == " \\ ")
									list.List.RemoveAt(list.List.Count - 1);
                            }
                        }

						// The current block is the block we are ending. Finished blocks become a ScriptWild.
						if(stack.Count == 0) stack.Push((null, " \\ ", new List<ScriptWild>()));
						stack.Peek().List.Add(new ScriptWild(list.List.ToArray(), list.Block, list.Separator ?? ' '));

						continue;

					}

				}

				// 'str' is not a separator character or a block character.
				{

					// Add 'str' to the current block.
					var current = stack.Peek();
					current.List.Add(new ScriptWord(str, true));
					// If we just made current.List.Count 2, then we know the separator is whitespace.
					if(current.List.Count == 2) {
						current = stack.Pop();
						stack.Push((' ', current.Block, current.List));
                    }

					continue;

				}

			}

			; // debug break

			// Combine remaining stack into one.
			ScriptWild result = new ScriptWild(stack.Reverse().ToArray(), " \\ ", ' ');
            return result;

		}

		private static ScriptString[] Separate(ScriptString str) {

			var separated = new List<ScriptString>(str.Length);
			var characters = new LinkedList<ScriptChar>();
			bool inString = false, escaped = false;

			for(int i = 0; i < str.Length; i++) {
				ScriptChar current = str[i];

				if(!escaped) {
					if((char)current == '\\') {
						escaped = true;
						continue;
					} else if((char)current == '"') inString = !inString;
				} else escaped = false;

				if(!inString) {

					if(Variable.OperationDictionary.TryGetValue(((char)current).ToString(), out Variable.Operation thisOp)) {

						ScriptChar[] array = new ScriptChar[characters.Count];
						characters.CopyTo(array, 0);
						characters.Clear();
						if(array.Length > 0) separated.Add(array);

						separated.Add(i + 1 < str.Length
						 && Variable.OperationTypeDictionary[thisOp] == Variable.OperationType.Arithmetic
						 && Variable.OperationDictionary.TryGetValue(((char)str[i + 1]).ToString(), out Variable.Operation nextOp)
						 && Variable.OperationTypeDictionary[nextOp] == Variable.OperationType.Set
							? (ScriptWord)(current + str[++i]) : current);

						continue;

					}

					if(IsBlockChar((char)current, out _) || IsSeparatorChar((char)current) || char.IsWhiteSpace((char)current)) {

						ScriptChar[] array = new ScriptChar[characters.Count];
						characters.CopyTo(array, 0);
						characters.Clear();
						if(array.Length > 0)
							separated.Add(array);
						if(!char.IsWhiteSpace((char)current)) {
							characters.AddLast(current);
							separated.Add(new ScriptString(characters));
							characters = new LinkedList<ScriptChar>();
						}

						continue;

					}

				}

				characters.AddLast(current);

			}

			if(characters.Count > 0) separated.Add(new ScriptString(characters));

			return separated.ToArray();

		}

		public static bool IsBlockChar(char chr, out string type)
			=> IsBlockCharStart(chr, out type) ? true : IsBlockCharEnd(chr, out type);

		public static bool IsBlockCharStart(char chr, out string block) {
			for(int i = 0; i < BlockTypesStart.Count; i++) {
				if(chr == BlockTypesStart[i]) {
					block = BlockTypes[i];
					return true;
				}
			}
			block = null;
			return false;
		}

		public static bool IsBlockCharEnd(char chr, out string type) {
			for(int i = 0; i < BlockTypesEnd.Count; i++) {
				if(chr == BlockTypesEnd[i]) {
					type = BlockTypes[i];
					return true;
				}
			}
			type = null;
			return false;
		}

		public static bool IsSeparatorChar(char chr) {
			foreach(char type in SeparatorTypes)
				if(chr == type) return true;
			return false;
		}

		public IEnumerator<ScriptWild> GetEnumerator() => ((IReadOnlyList<ScriptWild>)wilds).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<ScriptWild>)wilds).GetEnumerator();

		public static implicit operator string(ScriptLine line) => line.str;
		public static implicit operator ScriptWild[](ScriptLine line) => line.wilds;
		public static implicit operator ScriptWild(ScriptLine line) => new ScriptWild(line.wilds, " \\ ", ' ');

		public override string ToString() => $"Line (?):\n{str}";

	}

}
