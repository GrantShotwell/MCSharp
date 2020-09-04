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

			var stack = new Stack<IncompleteLine>();
			stack.Push(new IncompleteLine(" \\ ", ' '));

			foreach(ScriptString current in split) {
                char chr = (char)current[0];
                IncompleteLine top = stack.Peek();

                if(IsBlockCharStart(chr, out string block)) {

					// Start new block.
					IncompleteLine line = new IncompleteLine(block, null);
					stack.Push(line);

				} else if(IsBlockCharEnd(chr, out block)) {

					// Check block type.
					if(top.Block == block) {
						// Close top block.
						ScriptWild complete = top.Complete();
						stack.Pop();
						// Add it to next.
						stack.Peek().Add(complete);
					} else {
						// Wrong block closed.
						throw new Compiler.SyntaxException($"Unexpected '{chr}'.", current.ScriptTrace);
                    }

				} else {

					// Add to top block.
					top.Add(current);

				}

			}

			if(stack.Count != 1) {
				throw new Compiler.SyntaxException($"{stack.Count - 1} blocks are left unclosed.", phrase.ScriptTrace);
			} else {
				ScriptWild result = stack.Peek().Complete();
				return result;
			}

		}

		public static ScriptString[] Separate(ScriptString str) {

			var separated = new List<ScriptString>(str.Length);
			var characters = new LinkedList<ScriptChar>();
			bool inString = false, escaped = false, lastWasOperator = false;

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

						if(lastWasOperator) {

                            ScriptString last = separated[^1];
							separated.RemoveAt(separated.Count - 1);
							separated.Add(last + current);

                        } else {

							separated.Add(i + 1 < str.Length
							 && Variable.OperationTypeDictionary[thisOp] == Variable.OperationType.Arithmetic
							 && Variable.OperationDictionary.TryGetValue(((char)str[i + 1]).ToString(), out Variable.Operation nextOp)
							 && Variable.OperationTypeDictionary[nextOp] == Variable.OperationType.Set
								? (ScriptWord)(current + str[++i]) : current);

                        }

						lastWasOperator = true;
						continue;

					}

					if(IsBlockChar((char)current, out _) || IsSeparatorChar((char)current) || char.IsWhiteSpace((char)current)) {

						ScriptChar[] array = new ScriptChar[characters.Count];
						characters.CopyTo(array, 0);
						characters.Clear();
						if(array.Length > 0) separated.Add(array);

						if(!char.IsWhiteSpace((char)current)) {
							characters.AddLast(current);
							separated.Add(new ScriptString(characters));
							characters = new LinkedList<ScriptChar>();
						}

						lastWasOperator = false;
						continue;

					}

				}

				characters.AddLast(current);

				lastWasOperator = false;
				continue;

			}

			if(characters.Count > 0) separated.Add(new ScriptString(characters));

			return separated.ToArray();

		}

        public ScriptWild ToWild() => new ScriptWild(wilds, "{\\}", ';');

        public static bool IsBlockChar(char chr, out string type)
			=> IsBlockCharStart(chr, out type) || IsBlockCharEnd(chr, out type);

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

		private class IncompleteLine {

            #region Properties

			public string Block { get; set; }

			public char? Separator { get; set; }

			private List<ScriptWild> Items { get; } = new List<ScriptWild>();

			private List<ScriptWild> Subitems { get; } = new List<ScriptWild>();

            #endregion


            #region Constuctors

            public IncompleteLine(string block, char? separator) {
				Block = block;
				Separator = separator;
			}

            #endregion


            #region Methods

            public void Add(ScriptString item) => Add((ScriptWild)(ScriptWord)item);

			public void Add(ScriptWild item) {
				char? chr = item.IsWord ? (char?)item.Word[0] : null;
				if(chr.HasValue && item.Word.Length == 1 && IsSeparatorChar(chr.Value)) {
					if(Separator.HasValue) {
						if(Separator.Value == (char)item.Word[0]) {
							// Complete subitems.
							ScriptWild subitems = new ScriptWild(Subitems, " \\ ", ' ');
							Subitems.Clear();
							// Add to items.
							Items.Add(subitems);
						} else {
							throw new Compiler.SyntaxException("Unexpected separator.", item.ScriptTrace);
						}
					} else {
						// Set separator.
						Separator = chr.Value;
						// Complete subitems.
						ScriptWild subitems = new ScriptWild(Subitems, " \\ ", ' ');
						Subitems.Clear();
						// Add to items.
						Items.Add(subitems);
					}
                } else {
					// Add to subitems.
					Subitems.Add(item);
                }
            }

            public ScriptWild Complete() {
				// Complete subitems.
				ScriptWild subitems = new ScriptWild(Subitems, " \\ ", ' ');
				// Add to items.
				Items.Add(subitems);
				// Complete items.
				ScriptWild items = new ScriptWild(Items, Block, Separator ?? ' ');
				// Return items.
				return items;
			}

            #endregion

        }

    }
}
