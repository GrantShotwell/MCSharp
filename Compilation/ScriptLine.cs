using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

    public struct ScriptLine : IReadOnlyList<ScriptWild> {

        public static string[] BlockTypes { get; } = new string[] { "{\\}", "[\\]", "(\\)", "\"\\\"" };
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

        public ScriptWild this[int index] => wilds[index];
        public int Length => wilds.Length;

        int IReadOnlyCollection<ScriptWild>.Count => Length;

        public ScriptLine(string line) : this(GetWilds(line)) { }

        public ScriptLine(ScriptWild[] wilds) {

            this.wilds = new ScriptWild[wilds.Length];
            wilds.CopyTo(this.wilds, 0);

            int length = wilds.Length;
            string[] array = new string[length];
            for(int i = 0; i < length; i++) array[i] = wilds[i];
            string str = "";
            foreach(string s in array)
                str += " " + s;
            this.str = str.Length > 0 ? str[1..] : "";

        }

        private static ScriptWild[] GetWilds(string phrase) {
            if(phrase is null) return new ScriptWild[] { };
            SpaceBlockChars(ref phrase);
            List<ScriptWild> wilds;

            var split = new List<string>(phrase.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries));
            var stack1 = new Stack<List<ScriptWild>>();
            var stack2 = new Stack<string>();
            stack1.Push(wilds = new List<ScriptWild>());
            for(int i = 0; i < split.Count; i++) {
                string str = split[i];

                //If there is a block character, separate it.
                var strList = new List<char>(str);
                for(int j = 0; j < strList.Count - 1; j++) {
                    char chr = strList[j];
                    if(IsBlockChar(chr, out _)) {
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
                    char c = str[0];
                    if(IsBlockCharStart(c, out string block)) { //start wilds
                        stack1.Push(wilds = new List<ScriptWild>());
                        stack2.Push(block);
                        continue;
                    } else if(IsBlockCharEnd(c, out string blockType)) {
                        var poped = stack1.Pop();
                        if(stack2.Pop() != blockType) throw new Compiler.SyntaxException("");
                        (wilds = stack1.Peek()).Add(new ScriptWild(poped.ToArray(), blockType));
                        continue;
                    }
                }

                //If we got to here, we know that it is not a block character, and is just a normal word.
                wilds.Add(new ScriptWild(new ScriptWord(str)));

            }

            return wilds.ToArray();
        }

        private static void SpaceBlockChars(ref string str) {
            //todo: fix slow - don't use string.Insert()
            for(int i = 1; i < str.Length; i++) {
                char current = str[i];
                if(IsBlockChar(current, out _)) {
                    char last = str[i - 1];
                    char next = str.Length - 1 != i ? str[i + 1] : ' ';
                    if(!char.IsWhiteSpace(next)) {
                        str = str.Insert(i + 1, " ");
                    }
                    if(!char.IsWhiteSpace(last)) {
                        str = str.Insert(i++, " ");
                    }
                }
            }
        }

        public static bool IsBlockChar(char chr, out string blockType)
            => IsBlockCharStart(chr, out blockType) ? true : IsBlockCharEnd(chr, out blockType);

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

        public static bool IsBlockCharEnd(char chr, out string blockType) {
            for(int i = 0; i < BlockTypesEnd.Count; i++) {
                if(chr == BlockTypesEnd[i]) {
                    blockType = BlockTypes[i];
                    return true;
                }
            }
            blockType = null;
            return false;
        }

        public IEnumerator<ScriptWild> GetEnumerator() => ((IReadOnlyList<ScriptWild>)wilds).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<ScriptWild>)wilds).GetEnumerator();

        public static implicit operator string(ScriptLine phrase) => phrase.str;

        //public override string ToString() => $"Line {FileLine}:\n{str}";

    }

}
