using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace MCSharp.Compilation {

	/// <summary>
	/// Can either be a single <see cref="ScriptWord"/> or... more <see cref="ScriptWild"/>s, in which case you can consider it a <see cref="ScriptLine"/>.
	/// </summary>
	[DebuggerStepThrough]
	public struct ScriptWild : IReadOnlyList<ScriptWild> {

		private readonly ScriptWord? word;
		private readonly ScriptWild[] wilds;
		private readonly string str;

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
		public ScriptWild this[Range range] => new ScriptWild(wilds[range], BlockType, SeparationType.Value);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public ScriptWild this[int index] => IsWord ? Word : Wilds[index];

		/// <summary>
		/// Format: "[OPEN]\[CLOSE]"
		/// </summary>
		public string BlockType { get; }
		public char? SeparationType { get; }
		public string FullBlockType => $"{BlockType[0]}\\{SeparationType}\\{BlockType[2]}";

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
			BlockType = null;
			SeparationType = null;
			str = (string)word;
		}

		public ScriptWild(IReadOnlyList<(char? Separator, string Block, List<ScriptWild> List)> list, string block, char separation) {

			int count = list.Count;
			var array = new ScriptWild[count];
			for(int i = 0; i < count; i++) {
				array[i] = new ScriptWild(list[i].List, list[i].Block, list[i].Separator ?? ' ');
            }

			ScriptWild value = new ScriptWild(array, " \\ ", ' ');

			word = value.word;
			wilds = value.wilds;
			BlockType = value.BlockType;
			SeparationType = value.SeparationType;
			str = value.str;

		}

		/// <summary>
		/// Creates a new <see cref="ScriptWild"/> that is just more <see cref="ScriptWild"/>s.
		/// </summary>
		public ScriptWild(IReadOnlyList<ScriptWild> list, string block, char separation) {

			if(list is null)
				throw new ArgumentNullException(nameof(list));
			if(string.IsNullOrEmpty(block))
				throw new ArgumentException("Argument cannot be null or empty.", nameof(block));

			if(list.Count == 1 && block == " \\ " && separation == ' ') {

				ScriptWild value;
				if(list[0].IsWilds) {
					value = new ScriptWild(list[0].Array, list[0].BlockType, list[0].SeparationType.Value);
                } else {
					value = new ScriptWild(list[0].Word);
                }

				word = value.word;
				wilds = value.wilds;
				BlockType = value.BlockType;
				SeparationType = value.SeparationType;
				str = value.str;

            } else if(list.Count == 0) {

				word = null;
				wilds = new ScriptWild[] { };
				BlockType = block;
				SeparationType = separation;
				str = $"{block[0]}{block[2]}";

			} else if(list.Count > 1 || list[0].IsWilds || block != " \\ " || separation != ' ') {

                int count = list.Count;
                wilds = new ScriptWild[count];
                for(int i = 0; i < count; i++) wilds[i] = list[i];
                word = null;

                BlockType = block;
                SeparationType = separation;

                string str = "";
                foreach(string wld in list)
                    str += separation + wld;
                if(separation == ';') this.str = str.Length > 0 ? $"{block[0]}{str[1..]};{block[2]}" : $"{block[0]};{block[2]}";
                else this.str = str.Length > 0 ? $"{block[0]}{str[1..]}{block[2]}" : $"{block[0]}{block[2]}";

            } else {

                word = list[0].Word;
                wilds = null;
                BlockType = null;
                SeparationType = null;
                str = (string)word;

            }
        }

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public IEnumerator<ScriptWild> GetEnumerator()
			=> IsWord ? ((IEnumerable<ScriptWild>)(new ScriptWild[] { Word })).GetEnumerator() : ((IEnumerable<ScriptWild>)wilds).GetEnumerator();

		public override string ToString() => $"[{ScriptTrace}] {str}";

		public static implicit operator string(ScriptWild wild) => wild.str;
		public static implicit operator ScriptWild(ScriptWord word) => new ScriptWild(word);

	}

}
