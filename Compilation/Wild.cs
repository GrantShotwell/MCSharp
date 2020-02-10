using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

    /// <summary>
    /// Can either be a single <see cref="Compilation.Word"/> or... more <see cref="Wild"/>s, in which case you can consider it a 'semi-<see cref="Phrase"/>'.
    /// </summary>
    public struct Wild : IReadOnlyCollection<Word> {

        //todo: add "block type" property as string

        private readonly Word? word;
        private readonly Wild[] wilds;
        private readonly string str;

        public bool IsWord => word != null;
        public Word Word => IsWord ? word.Value : throw new InvalidOperationException();
        public bool IsWilds => wilds != null;
        public IReadOnlyList<Wild> Wilds => IsWilds ? wilds : throw new InvalidOperationException();

        public int Count {
            get {
                if(IsWord) return 1;
                else {
                    int sum = 0;
                    foreach(Wild wild in wilds)
                        sum += wild.Count;
                    return sum;
                }
            }
        }

        public Wild(Word word) {

            this.word = word;
            wilds = null;

            str = word;

        }

        public Wild(Wild[] wilds) {

            this.wilds = new Wild[wilds.Length];
            wilds.CopyTo(this.wilds, 0);
            word = null;

            str = "";
            foreach(string wld in wilds)
                str += " " + wld;

        }

        public static implicit operator string(Wild wild) => wild.str;

        public IEnumerator<Word> GetEnumerator() {
            if(IsWord) return ((IEnumerable<Word>)new Word[] { Word }).GetEnumerator();
            else {
                var words = new LinkedList<Word>();
                foreach(Wild wild in Wilds) {
                    foreach(Word word in wild) {
                        words.AddLast(word);
                    }
                }
                return words.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => str;

    }

}
