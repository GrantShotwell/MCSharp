using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

    /// <summary>
    /// Can either be a single <see cref="ScriptWord"/> or... more <see cref="ScriptWild"/>s, in which case you can consider it a <see cref="ScriptLine"/>.
    /// </summary>
    public struct ScriptWild : IReadOnlyCollection<ScriptWord> {

        //todo: add "block type" property as string

        private readonly ScriptWord? word;
        private readonly ScriptWild[] wilds;
        private readonly string str;

        /// <summary>
        /// 
        /// </summary>
        public bool IsWord => word != null;

        /// <summary>
        /// When a <see cref="ScriptWild"/> is just a <see cref="Compilation.ScriptWord"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException()">Thrown when <see cref="IsWord"/> is false.</exception>
        public ScriptWord Word => IsWord ? word.Value : throw new InvalidOperationException();

        /// <summary>
        /// 
        /// </summary>
        public bool IsWilds => wilds != null;

        /// <summary>
        /// When a <see cref="ScriptWild"/> is just more <see cref="ScriptWild"/>s.
        /// </summary>
        /// <exception cref="InvalidOperationException()">Thrown when <see cref="IsWilds"/> is false.</exception>
        public IReadOnlyList<ScriptWild> Wilds => IsWilds ? wilds : throw new InvalidOperationException();

        /// <summary>
        /// Format: "[OPEN]\[CLOSE]"
        /// </summary>
        public string BlockType { get; }

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


        /// <summary>
        /// Creates a new <see cref="ScriptWild"/> that is just a <see cref="Compilation.ScriptWord"/>.
        /// </summary>
        public ScriptWild(ScriptWord word) {

            this.word = word;
            wilds = null;

            BlockType = null;

            str = word;

        }

        /// <summary>
        /// Creates a new <see cref="ScriptWild"/> that is just more <see cref="ScriptWild"/>s.
        /// </summary>
        public ScriptWild(ScriptWild[] wilds, string blockType) {

            this.wilds = new ScriptWild[wilds.Length];
            wilds.CopyTo(this.wilds, 0);
            word = null;

            BlockType = blockType;

            string str = "";
            foreach(string wld in wilds)
                str += " " + wld;
            this.str = str.Length > 0 ? str[1..] : "";

        }

        public IEnumerator<ScriptWord> GetEnumerator() {
            if(IsWord) return ((IEnumerable<ScriptWord>)new ScriptWord[] { Word }).GetEnumerator();
            else {
                var words = new LinkedList<ScriptWord>();
                foreach(ScriptWild wild in Wilds) {
                    foreach(ScriptWord word in wild) {
                        words.AddLast(word);
                    }
                }
                return words.GetEnumerator();
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => str;

        public static implicit operator string(ScriptWild wild) => wild.str;

    }

}
