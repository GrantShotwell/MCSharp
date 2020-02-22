using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation {

    public struct ScriptLine : IReadOnlyList<ScriptWild> {

        public static string[] BlockTypes { get; } = new string[] { "{\\}", "[\\]", "(\\)" };
        public static char[] SeparatorTypes { get; } = new char[] { '.', ',', ';', ' ' };
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

        public static ScriptWild[] GetWilds(string phrase) {
            if(phrase is null) return new ScriptWild[] { };

            ScriptWord[] split = Separate(phrase);

            var stack = new Stack<Tuple<char?, string, List<ScriptWild>>>();
            stack.Push(new Tuple<char?, string, List<ScriptWild>>(null, " \\ ", new List<ScriptWild>()));

            for(int i = 0; i < split.Length; i++) {
                string str = split[i];

                if(str.Length == 1) {
                    char chr = str[0];
                    if(IsBlockCharStart(chr, out string blk)) {
                        //Starting a new block tuple.
                        var tuple = new Tuple<char?, string, List<ScriptWild>>(null, blk, new List<ScriptWild>());
                        stack.Push(tuple);
                        continue;


                    } else if(IsSeparatorChar(chr) && chr != '.') {
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
                            if(chr == '.') {
                                //Special case for dot: parse all right now and package it as a single ScriptWild.
                                var item3 = tuple.Item3;
                                var stolen = item3[^1];
                                item3.RemoveAt(item3.Count - 1);
                                var list = new List<ScriptWild> { stolen };
                                while(chr == '.') {
                                    str = split[++i];
                                    if(IsBlockChar(str[0], out _) || IsSeparatorChar(str[0]))
                                        throw new Compiler.SyntaxException($"Unexpected '{str}' after '.' operator.");
                                    list.Add(new ScriptWord(str));
                                    chr = split[++i][0];
                                }
                                i--;
                                var dotted = new ScriptWild(list.ToArray(), " \\ ", '.');
                                tuple.Item3.Add(dotted);
                                stack.Push(tuple);
                            } else {
                                //Make a new list.
                                var next = new Tuple<char?, string, List<ScriptWild>>(chr, tuple.Item2, new List<ScriptWild>());
                                next.Item3.Add(new ScriptWild(tuple.Item3.ToArray(), " \\ ", ' '));
                                stack.Push(next);
                            }
                        } else {
                            throw new Compiler.SyntaxException($"Expected '{last.Item1?.ToString() ?? "[nothing]"}' but got '{chr}'.");
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
                            if(tuple.Item2 != blk) throw new Compiler.SyntaxException($"Expected '{tuple.Item2[2]}' but got '{chr}'.");
                            stack.Peek().Item3.Add(new ScriptWild(tuple.Item3.ToArray(), tuple.Item2, tuple.Item1 ?? ' '));
                        }
                        continue;


                    }
                }

                {
                    //Getting here means this is not a keyword.
                    //Just add it to whatever tuple we're on.
                    var tuple = stack.Peek();
                    if(tuple.Item1 == null) {
                        var list = tuple.Item3;
                        list.Add(new ScriptWord(str));
                        if(list.Count > 1) {
                            stack.Pop();
                            stack.Push(new Tuple<char?, string, List<ScriptWild>>(' ', tuple.Item2, list));
                        }
                    } else if(tuple.Item1 != ';') {
                        var list = tuple.Item3;
                        list.Add(new ScriptWord(str));
                    } else {
                        var next = new Tuple<char?, string, List<ScriptWild>>(null, " \\ ", new List<ScriptWild>());
                        var list = next.Item3;
                        list.Add(new ScriptWord(str));
                        stack.Push(next);
                    }
                }

            }

            while(stack.Count > 1 && stack.Peek().Item2[2] == ' ') {
                var tuple = stack.Pop();
                if(tuple.Item3.Count > 1) {
                    if(stack.Peek().Item3.Count == 0 && stack.Peek().Item2 == tuple.Item2 && stack.Peek().Item1 == tuple.Item1) {
                        stack.Pop();
                        stack.Push(tuple);
                    } else stack.Peek().Item3.Add(new ScriptWild(tuple.Item3.ToArray(), tuple.Item2, tuple.Item1 ?? ' '));
                } else {
                    if(tuple.Item3[0].IsWord) stack.Peek().Item3.Add(new ScriptWord(tuple.Item3[0]));
                    else stack.Peek().Item3.Add(new ScriptWild(tuple.Item3.ToArray(), tuple.Item2, tuple.Item1.Value));
                }
            }
            if(stack.Count > 1) throw new Compiler.SyntaxException($"Missing '{stack.Peek().Item2[2]}'.");
            return stack.Peek().Item3.ToArray();
        }

        private static ScriptWord[] Separate(string str) {

            var original = new LinkedList<char>(str);
            var separated = new List<ScriptWord>(str.Length / 2); //guess
            var current = new LinkedList<char>();
            bool inStr = false, escaped = false;

            foreach(char chr in original) {

                if(!escaped) {
                    if(chr == '\\') {
                        escaped = true;
                        continue;
                    } else if(chr == '"') inStr = !inStr;
                } else escaped = false;

                if(!inStr && (IsBlockChar(chr, out _) || IsSeparatorChar(chr) || char.IsWhiteSpace(chr))) {
                    char[] array = new char[current.Count];
                    current.CopyTo(array, 0);
                    current = new LinkedList<char>();
                    if(array.Length > 0) separated.Add(new ScriptWord(new string(array)));
                    if(!char.IsWhiteSpace(chr)) {
                        current.AddLast(chr);
                        separated.Add(new ScriptWord(chr.ToString()));
                        current = new LinkedList<char>();
                    }
                } else {
                    current.AddLast(chr);
                }

            }

            if(current.Count > 0) {
                char[] array = new char[current.Count];
                current.CopyTo(array, 0);
                string word = new string(array);
                separated.Add(new ScriptWord(word, ignoreCohesion: true));
            }

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

        public static implicit operator string(ScriptLine phrase) => phrase.str;

        //public override string ToString() => $"Line {FileLine}:\n{str}";

    }

}
