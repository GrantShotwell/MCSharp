using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MCSharp.Compilation {

	/// <summary>
	/// Can either be a single <see cref="ScriptWord"/> or... more <see cref="ScriptWild"/>s, in which case you can consider it a <see cref="ScriptLine"/>.
	/// </summary>
	[DebuggerStepThrough]
	public struct ScriptWild : IReadOnlyList<ScriptWild> {

		private readonly ScriptWord? word;
		private readonly ScriptWild[] wilds;

		/// <summary>
		/// 
		/// </summary>
		public bool IsWord => word != null;

		/// <summary>
		/// When a <see cref="ScriptWild"/> is just a <see cref="ScriptWord"/>.
		/// </summary>
		/// <exception cref="InvalidOperationException()">Thrown when <see cref="IsWord"/> is false.</exception>
		public ScriptWord Word => IsWord ? word.Value
			: throw new InvalidOperationException($"Cannot get '{nameof(Word)}' because '{nameof(IsWord)}' is false!");

		/// <summary>
		/// 
		/// </summary>
		public bool IsWilds => wilds != null;

		public bool IsEmpty => IsWilds && wilds.Length == 0;
		public bool IsSpace => IsEmpty && IsSpaced;
		public bool IsSpaced => IsWord || FullBlockType == " \\ \\ ";

		/// <summary>
		/// When a <see cref="ScriptWild"/> is just more <see cref="ScriptWild"/>s.
		/// </summary>
		/// <exception cref="InvalidOperationException()">Thrown when <see cref="IsWilds"/> is false.</exception>
		public IReadOnlyList<ScriptWild> Wilds => IsWilds ? wilds
			: throw new InvalidOperationException($"Cannot get '{nameof(Wilds)}' because '{nameof(IsWilds)}' is false!");
		public ScriptWild[] Array {
			get {
				if(IsWilds) {
					var wilds = new ScriptWild[this.wilds.Length];
					this.wilds.CopyTo(wilds, 0);
					return wilds;
				} else {
					return new ScriptWild[] { Word };
				}
			}
		}

		/// <summary>
		/// Creates a new <see cref="ScriptWild"/> from the given <see cref="Range"/> applied to <see cref="Wilds"/>.
		/// </summary>
		/// <exception cref="InvalidOperationException()">Thrown when <see cref="IsWilds"/> is false.</exception>
		public ScriptWild this[Range range] => new ScriptWild(wilds[range], Block, Separator.Value);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public ScriptWild this[int index] => IsWord ? Word : Wilds[index];

		/// <summary>
		/// Format: "[OPEN]\[CLOSE]"
		/// </summary>
		public string Block { get; }
		public char? Separator { get; }
		public string FullBlockType => IsWilds ? $"{Block[0]}\\{Separator}\\{Block[2]}"
			: throw new InvalidOperationException($"Cannot get '{nameof(FullBlockType)}' because '{nameof(IsWilds)}' is false!");

		public int Count {
			get {
				if(IsWord) return 1;
				else {
					int sum = 0;
					foreach(ScriptWild wild in wilds)
						sum += wild.Count;
					return sum;
				}
			}
		}

		public ScriptTrace ScriptTrace => IsWord ? word.Value.ScriptTrace : (wilds.Length > 0 ? wilds[0].ScriptTrace : Compiler.AnonScriptTrace);


		/// <summary>
		/// Creates a new <see cref="ScriptWild"/> that is just a <see cref="ScriptWord"/>.
		/// </summary>
		public ScriptWild(ScriptWord word) {
			this.word = word;
			wilds = null;
			Block = null;
			Separator = null;
		}

		/// <summary>
		/// Creates a new <see cref="ScriptWild"/> that is just more <see cref="ScriptWild"/>s.
		/// </summary>
		public ScriptWild(IReadOnlyList<ScriptWild> list, string block, char separator) {

			if(list is null)
				throw new ArgumentNullException(nameof(list));
			if(string.IsNullOrEmpty(block))
				throw new ArgumentException("Argument cannot be null or empty.", nameof(block));

			int count = list.Count;
			bool IsSpaced = block == " \\ " && separator == ' ';

			if(count == 0) {

				// Construct empty.
				word = null;
				wilds = new ScriptWild[] { };
				Block = block;
				Separator = separator;

			} else {

				if(count == 1) {

					var item = list[0];

					bool needThisBlockInfo = !IsSpaced;
					bool needItemBlockInfo = !item.IsSpaced;

					if(needThisBlockInfo) {

						word = null;
						Block = block;
						Separator = separator;

						if(needItemBlockInfo) {
							wilds = new ScriptWild[] { item };
						} else {
							if(item.IsWord) {
								wilds = new ScriptWild[] { item.Word };
							} else {
								wilds = item.Wilds.ToArray();
							}
						}

					} else {

						if(item.IsWord) {

							word = item.Word;
							wilds = null;
							Block = null;
							Separator = null;

						} else {

							word = null;
							wilds = item.Wilds.ToArray();
							Block = item.Block;
							Separator = item.Separator;

						}

					}

				} else {

					// Construct normal block.
					word = null;

					var list2 = new List<ScriptWild>(list);
					for(int i = 0; i < list2.Count; i++) {
						var item = list2[i];
						if(item.IsWilds && item.Block == " \\ " && item.Separator == separator) {
							list2.RemoveAt(i--);
							foreach(var itm in item.Wilds) {
								list2.Add(itm);
							}
						}
					}

					wilds = list2.ToArray();
					Block = block;
					Separator = separator;

				}

			}

        }

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<ScriptWild> GetEnumerator()
			=> IsWord ? ((IEnumerable<ScriptWild>)(new ScriptWild[] { Word })).GetEnumerator() : ((IEnumerable<ScriptWild>)wilds).GetEnumerator();

		public override string ToString() {
			return $"[{ScriptTrace}] {(string)this}";
		}

		public static implicit operator string(ScriptWild wild) {
			return wild.IsWilds
				? $"{wild.Block[0]}" +
				  $"{string.Join((wild.Separator ?? ' ') == ' ' ? " " : $"{wild.Separator.Value} ", new ToStringEnumerable(wild.Array))}" +
				  $"{wild.Block[2]}"
				: (string)wild.Word;
		}

		public static implicit operator ScriptWild(ScriptWord word) => new ScriptWild(word);

		private class ToStringEnumerable : IEnumerable<string> {

			IReadOnlyList<ScriptWild> Array { get; }

			public ToStringEnumerable(IReadOnlyList<ScriptWild> array) {
				Array = array;
			}

			public IEnumerator<string> GetEnumerator() => new ToStringEnumerator(Array);
			IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		}

		private class ToStringEnumerator : IEnumerator<string> {

			private int Index { get; set; }

			public string Current => (string)Array[Index];
			object IEnumerator.Current => Current;

			IReadOnlyList<ScriptWild> Array { get; }

			public ToStringEnumerator(IReadOnlyList<ScriptWild> array) {
				Array = array;
				Reset();
			}

			public bool MoveNext() {
				Index++;
				return Index < Array.Count;
			}
			public void Reset() {
				Index = -1;
			}
			public void Dispose() { }

		}

	}

}
