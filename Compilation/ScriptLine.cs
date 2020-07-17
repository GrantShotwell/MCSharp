using MCSharp.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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


		public static ScriptWild[] GetWilds(ScriptString phrase) {

			ScriptString[] split = Separate(phrase);

			var stack = new Stack<Tuple<char?, string, List<ScriptWild>>>();
			stack.Push(new Tuple<char?, string, List<ScriptWild>>(null, " \\ ", new List<ScriptWild>()));

			for(int i = 0; i < split.Length; i++) {
				ScriptString str = split[i];

				if(str.Length == 1) {

					char chr = (char)str[0];
					if(IsBlockCharStart(chr, out string blk)) {
						//Starting a new block tuple.
						var tuple = new Tuple<char?, string, List<ScriptWild>>(null, blk, new List<ScriptWild>());
						stack.Push(tuple);
						continue;


					} else if(IsSeparatorChar(chr)) {
						var tuple = stack.Pop();
						if(!stack.TryPeek(out var last)) {
							last = new Tuple<char?, string, List<ScriptWild>>(null, " \\ ", new List<ScriptWild>());
							stack.Push(last);
						}
						//Check if a list is in progress.
						if(last.Item1 == chr) {
							//Continue that list.
							last.Item3.Add(new ScriptWild(tuple.Item3.ToArray(), " \\ ", ' '));
						} else if(last.Item1 == null || last.Item1 == ' ' || chr == '.') {
							//Make a new list.
							var next = new Tuple<char?, string, List<ScriptWild>>(chr, tuple.Item2, new List<ScriptWild>());
							next.Item3.Add(new ScriptWild(tuple.Item3.ToArray(), " \\ ", ' '));
							stack.Push(next);
						} else {
							throw new Compiler.SyntaxException($"Expected '{last.Item1?.ToString() ?? "[nothing]"}' but got '{chr}'.", phrase.ScriptTrace);
						}
						continue;


					} else if(IsBlockCharEnd(chr, out blk)) {
						//Ending the last block tuple.
						var tuple = stack.Pop();
						if(tuple.Item2 == " \\ ") {
							do {
								var next = stack.Pop();
								if(tuple.Item3.Count > 1) next.Item3.Add(new ScriptWild(tuple.Item3.ToArray(), tuple.Item2, tuple.Item1 ?? ' '));
								else next.Item3.Add(tuple.Item3[0]);
								tuple = next;
							} while(tuple.Item2[2] == ' ');
							stack.Push(tuple);
						} else {
							if(tuple.Item2 != blk) throw new Compiler.SyntaxException($"Expected '{tuple.Item2[2]}' but got '{chr}'.", phrase.ScriptTrace);
							stack.Peek().Item3.Add(new ScriptWild(tuple.Item3.ToArray(), tuple.Item2, tuple.Item1 ?? ' '));
						}
						continue;


					}

				}

				{

					//Getting here means this is not a keyword.
					//Add the word to whatever tuple we're on.
					var tuple = stack.Peek();

					if(tuple.Item1 == null) {

						var list = tuple.Item3;
						list.Add((ScriptWord)str);
						if(list.Count > 1) {
							stack.Pop();
							stack.Push(new Tuple<char?, string, List<ScriptWild>>(' ', tuple.Item2, list));
						}

					} else if(tuple.Item1 == ';') {

						var next = new Tuple<char?, string, List<ScriptWild>>(null, " \\ ", new List<ScriptWild>());
						var list = next.Item3;
						list.Add((ScriptWord)str);
						stack.Push(next);

					} else if(tuple.Item1 != ' ') {

						var list = tuple.Item3;
						var sub = new LinkedList<ScriptWild>();
						sub.AddFirst((ScriptWord)str);
						int index = list.Count - 1;
						while((index > 0) && (list[index].IsWilds ? list[index].FullBlockType == $" \\{tuple.Item1}\\ " : true)) {
							var item = list[index];
							list.RemoveAt(index--);
							sub.AddFirst(item);
						}
						var array = new ScriptWild[sub.Count];
						sub.CopyTo(array, 0);
						list.Add(array.Length > 1 ? new ScriptWild(array, " \\ ", ' ') : (ScriptWord)str);

					} else {

						//var next = new Tuple<char?, string, List<ScriptWild>>(null, " \\ ", new List<ScriptWild>());
						//var list = next.Item3;
						//list.Add(new ScriptWord(str));
						//stack.Push(next);

						var list = tuple.Item3;
						list.Add((ScriptWord)str);

					}

				}

			}

			; //debug break

			//Try to collapse redundant blocks.
			CollapseLoop: while(stack.Count > 1 && stack.Peek().Item2 == " \\ ") {
				var tuple = stack.Pop();
				if(tuple.Item3.Count > 1) {
					var peek = stack.Peek();
					bool equalItems = (peek.Item1 ?? ' ') == (tuple.Item1 ?? ' ') && peek.Item2 == tuple.Item2;
					bool combinable = peek.Item2 == " \\ " && equalItems;
					if(peek.Item3.Count == 0 && equalItems) {
						stack.Pop();
						stack.Push(tuple);
					} else if(peek.Item3.Count > 0 && combinable) {
						foreach(var wild in tuple.Item3) peek.Item3.Add(wild);
					} else peek.Item3.Add(new ScriptWild(tuple.Item3.ToArray(), tuple.Item2, tuple.Item1 ?? ' '));
				} else {
					if(tuple.Item3[0].IsWord) stack.Peek().Item3.Add(tuple.Item3[0].Word);
					else stack.Peek().Item3.Add(new ScriptWild(tuple.Item3.ToArray(), tuple.Item2, tuple.Item1.Value));
				}
			}

			//Try to remove empty blocks.
			if(stack.Count > 1) {
				var pop = stack.Pop();
				var peek = stack.Peek();
				if((peek.Item1 ?? ' ') == ' ' && peek.Item2 == " \\ " && peek.Item3.Count == 0) {
					stack.Pop();
					stack.Push(pop);
					goto CollapseLoop;
				} else {
					throw new Compiler.SyntaxException($"Missing '{stack.Peek().Item2[2]}'.", phrase.ScriptTrace);
				}
			}

			//Try to package into a single " \\ " block.
			// (stack has to be size 1 to reach here)
			if(stack.Peek().Item2 != " \\ ") {
				var pop = stack.Pop();
				var next = new Tuple<char?, string, List<ScriptWild>>(' ', " \\ ",
					new List<ScriptWild>() { new ScriptWild(pop.Item3.ToArray(), pop.Item2, pop.Item1 ?? ' ') });
				stack.Push(next);
			}

			//Return.
			return stack.Peek().Item3.ToArray();
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

				ScriptChar[] c;
				if(characters.Count < maxOperatorSize) {
					c = new ScriptChar[characters.Count + 1];
					c[^1] = current;
					characters.CopyTo(c, 0);
				} else c = new ScriptChar[0];
				var s = new ScriptString(c);

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

				} else if(!inString && (IsBlockChar((char)current, out _) || IsSeparatorChar((char)current) || char.IsWhiteSpace((char)current))) {

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

				} else {

					characters.AddLast(current);

				}

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
