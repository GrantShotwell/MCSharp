using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

    public struct Phrase : IReadOnlyList<Wild> {

        public static char[] blockChars = new char[] { '{', '}', '[', ']', '(', ')' };
        public static char[] blockCharsStart = new char[] { '{', '[', '(' };
        public static char[] blockCharsEnd = new char[] { '}', ']', ')' };

        private readonly Wild[] wilds;
        public readonly string str;

        public Wild this[int index] => wilds[index];
        public int Length => wilds.Length;

        int IReadOnlyCollection<Wild>.Count => Length;

        public Phrase(string phrase) : this(GetWilds(phrase)) { }

        public Phrase(Wild[] wilds) {

            this.wilds = new Wild[wilds.Length];
            wilds.CopyTo(this.wilds, 0);

            int length = wilds.Length;
            string[] array = new string[length];
            for(int i = 0; i < length; i++) array[i] = wilds[i];
            string str = "";
            foreach(string s in array)
                str += " " + s;
            if(str.Length > 0) this.str = str[1..];
            else this.str = "";

        }

        private static Wild[] GetWilds(string phrase) {
            if(phrase is null) return new Wild[] { };
            List<Wild> wilds;

            var split = new List<string>(phrase.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            var stack = new Stack<List<Wild>>();
            stack.Push(wilds = new List<Wild>());
            for(int i = 0; i < split.Count; i++) {
                string str = split[i];

                //If there is a block character, separate it.
                var strList = new List<char>(str);
                for(int j = 0; j < strList.Count - 1; j++) {
                    char chr = strList[j];
                    if(IsBlockChar(chr)) {
                        strList.Insert(j++ + 1, ' ');
                    }
                }
                if(strList.Count != str.Length) {
                    string[] strListSplit = new string(strList.ToArray()).Split(' ');
                    split[i] = strListSplit[0];
                    for(int j = 1; j < strListSplit.Length; j++) {
                        int J = j + i;
                        split.Insert(J, strListSplit[j]);
                    }
                }

                //That last spaghetti ensures that if it's a block character, it will be 'alone'.
                str = split[i];
                if(str.Length == 1) {
                    if(str == "{" || str == "[" || str == "(") { //start wilds
                        stack.Push(wilds = new List<Wild>());
                        continue;
                    } else if(str == "}" || str == "]" || str == ")") {
                        var poped = stack.Pop();
                        (wilds = stack.Peek()).Add(new Wild(poped.ToArray()));
                        continue;
                    }
                }

                //If we got to here, we know that it is not a block character, and is just a normal word.
                wilds.Add(new Wild(new Word(str)));

            }

            return wilds.ToArray();
        }

        public static bool IsBlockChar(char c) {
            foreach(char blockChar in blockChars) {
                if(c == blockChar) return true;
            }
            return false;
        }

        public static bool IsBlockCharStart(char c) {
            foreach(char blockChar in blockCharsStart) {
                if(c == blockChar) return true;
            }
            return false;
        }

        public static bool IsBlockCharEnd(char c) {
            foreach(char blockChar in blockCharsEnd) {
                if(c == blockChar) return true;
            }
            return false;
        }

        public IEnumerator<Wild> GetEnumerator() => ((IReadOnlyList<Wild>)wilds).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<Wild>)wilds).GetEnumerator();

        public static implicit operator string(Phrase phrase) => phrase.str;

        //public override string ToString() => $"Line {FileLine}:\n{str}";

    }

}
