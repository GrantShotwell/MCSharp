using MCSharp.Variables;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MCSharp.Compilation {

    public class ScriptClass : IDictionary<string, ScriptMember> {


        #region Properties

        public UserClass UserClass { get; }

        private IDictionary<string, ScriptMember> Members { get; }
        public Access Access { get; }
        public Usage Usage { get; }

        public ScriptMember this[string key] {
            get => Members[key];
            set => Members[key] = value;
        }

        public ICollection<string> Keys => Members.Keys;
        public ICollection<ScriptMember> Values => Members.Values;
        public int Count => Members.Count;
        public bool IsReadOnly => Members.IsReadOnly;

        public string Alias { get; }
        public ScriptTrace ScriptTrace { get; }

        #endregion


        #region Constructors

        public ScriptClass(string alias, ScriptString scriptClass, Access access = Access.Private, Usage usage = Usage.Default)
        : this(alias, GetMembers(alias, scriptClass), scriptClass.ScriptTrace, access, usage) { }
        private ScriptClass(string alias, Dictionary<string, ScriptMember> members, ScriptTrace scriptTrace, Access access, Usage usage) {

            Alias = alias;
            Access = access;
            Usage = usage;
            Members = members;
            ScriptTrace = scriptTrace;
            UserClass = new UserClass(this);

        }

        #endregion


        #region Methods

        public static Dictionary<string, ScriptMember> GetMembers(string root, ScriptString scriptClass) {
            var functions = new Dictionary<string, ScriptMember>();

            int start = 0, blocks = 0;
            string word = "", lastWord = "", alias = "";
            bool foundUsage = false, foundAccess = false;

            Access access = Access.Private;
            Usage usage = Usage.Default;

            void Rotate() { lastWord = word; word = ""; }
            for(int end = 0; end < scriptClass.Length; end++) {

                char current = (char)scriptClass[end];
                if(char.IsWhiteSpace(current)) {
                    if(blocks == 0) {
                        if(word != "") {
                            Rotate();
                            if(Compiler.AccessModifiers.TryGetValue(lastWord, out access)) {
                                if(foundAccess) throw new Compiler.SyntaxException("Unexpected second access modifier.", scriptClass[end].ScriptTrace);
                                else foundAccess = true;
                            }
                            if(Compiler.UsageModifiers.TryGetValue(lastWord, out usage)) {
                                if(foundUsage) throw new Compiler.SyntaxException("Unexpected second usage modifier.", scriptClass[end].ScriptTrace);
                                else foundUsage = true;
                            }
                        }
                    }
                } else if(current == '{') {
                    blocks++;
                    if(blocks == 1) {
                        start = end + 1;
                        alias = lastWord.Split('(', 2)[0];
                    }
                    Rotate();
                } else if(current == '}') {
                    blocks--;
                    if(blocks == 0) {
                        functions.Add(new ScriptFunction(root + "\\" + alias, scriptClass[start..end], access, usage));
                        foundUsage = false;
                        foundAccess = false;
                    }
                    Rotate();
                } else if(blocks == 0) {
                    word += current;
                }

            }

            return functions;
        }

        public void Add(string key, ScriptMember value) => Members.Add(key, value);
        public bool ContainsKey(string key) => Members.ContainsKey(key);
        public bool Remove(string key) => Members.Remove(key);
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out ScriptMember value) => Members.TryGetValue(key, out value);
        public void Add(KeyValuePair<string, ScriptMember> item) => Members.Add(item);
        public void Clear() => Members.Clear();
        public bool Contains(KeyValuePair<string, ScriptMember> item) => Members.Contains(item);
        public void CopyTo(KeyValuePair<string, ScriptMember>[] array, int arrayIndex) => Members.CopyTo(array, arrayIndex);
        public bool Remove(KeyValuePair<string, ScriptMember> item) => Members.Remove(item);
        public IEnumerator<KeyValuePair<string, ScriptMember>> GetEnumerator() => Members.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Members.GetEnumerator();

        #endregion


    }
}
