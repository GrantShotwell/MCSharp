using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

    /// <summary>
    /// Not to be confused with <see cref="ScriptWord"/>.
    /// </summary>
    public struct ScriptString : IList<ScriptChar> {


        #region Properties

        private ScriptChar[] Characters { get; }

        public ScriptChar this[int index] {
            get => Characters[index];
            set => Characters[index] = value;
        }

        public ScriptString this[Range range] => new ScriptString(Characters[range]);

        public ScriptTrace ScriptTrace { get; }

        public int Length => Characters.Length;

        int ICollection<ScriptChar>.Count => Length;

        bool ICollection<ScriptChar>.IsReadOnly => Characters.IsReadOnly;

        #endregion


        #region Constructors

        private ScriptString(ScriptChar[] from) {
            Characters = from;
            ScriptTrace = Characters.Length > 0 ? Characters[0].ScriptTrace : Compiler.AnonScriptTrace;
        }

        public ScriptString(ICollection<ScriptChar> from) {
            Characters = new ScriptChar[from.Count];
            from.CopyTo(Characters, 0);
            ScriptTrace = Characters.Length > 0 ? Characters[0].ScriptTrace : Compiler.AnonScriptTrace;
        }

        public ScriptString(string file, string path, int line = 1) {
            Characters = new ScriptChar[file.Length];
            ScriptTrace = new ScriptTrace(path, line);
            for(int i = 0; i < file.Length; i++) {
                char character = file[i];
                if(character == '\n') line++;
                Characters[i] = new ScriptChar(char.IsWhiteSpace(character) ? ' ' : character, path, line);
            }
        }

        public ScriptString(string anon) {
            string path = Compiler.AnonScriptTrace.FilePath;
            int line = Compiler.AnonScriptTrace.FileLine;
            Characters = new ScriptChar[anon.Length];
            ScriptTrace = Compiler.AnonScriptTrace;
            for(int i = 0; i < anon.Length; i++) {
                char character = anon[i];
                if(character == '\n') line++;
                Characters[i] = new ScriptChar(char.IsWhiteSpace(character) ? ' ' : character, path, line);
            }
        }

        #endregion


        #region Methods

        public ScriptChar[] ToArray() {
            ScriptChar[] characters = Characters;
            var array = new ScriptChar[characters.Length];
            characters.CopyTo(array, 0);
            return array;
        }

        public int IndexOf(ScriptChar item) => ((IList<ScriptChar>)Characters).IndexOf(item);

        public void Insert(int index, ScriptChar item) => ((IList<ScriptChar>)Characters).Insert(index, item);

        public void RemoveAt(int index) => ((IList<ScriptChar>)Characters).RemoveAt(index);

        public void Add(ScriptChar item) => ((IList<ScriptChar>)Characters).Add(item);

        public void Clear() => ((IList<ScriptChar>)Characters).Clear();

        public bool Contains(ScriptChar item) => ((IList<ScriptChar>)Characters).Contains(item);

        public void CopyTo(ScriptChar[] array, int arrayIndex) => ((IList<ScriptChar>)Characters).CopyTo(array, arrayIndex);

        public bool Remove(ScriptChar item) => ((IList<ScriptChar>)Characters).Remove(item);

        public IEnumerator<ScriptChar> GetEnumerator() => ((IReadOnlyList<ScriptChar>)Characters).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<ScriptChar>)Characters).GetEnumerator();

        public override string ToString() => $"{ScriptTrace}: \"{(string)this}\"";

        #endregion


        #region Operators

        public static ScriptString operator +(ScriptString left, ScriptString right) {
            ScriptChar[] array = new ScriptChar[left.Length + right.Length];
            left.CopyTo(array, 0);
            right.CopyTo(array, left.Length);
            return new ScriptString(array);
        }

        public static bool operator ==(ScriptString left, string right) => (string)left == right;
        public static bool operator !=(ScriptString left, string right) => (string)left != right;
        public static bool operator ==(string left, ScriptString right) => left == (string)right;
        public static bool operator !=(string left, ScriptString right) => left != (string)right;

        public static explicit operator string(ScriptString characters) {
            var array = new char[characters.Length];
            for(int i = 0; i < characters.Length; i++) array[i] = (char)characters[i];
            return new string(array);
        }

        public static implicit operator ScriptString(ScriptChar[] array) => new ScriptString((ICollection<ScriptChar>)array);

        #endregion


    }

}
