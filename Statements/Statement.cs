using MCSharp.Compilation;
using System;
using System.Collections.Generic;

namespace MCSharp.Statements {

    public abstract class Statement {

        public static Dictionary<string, Tuple<Reader, Writer>> Dictionary { get; } = new Dictionary<string, Tuple<Reader, Writer>>();

        public abstract string Call { get; }

        public Statement() => Dictionary.Add(Call, new Tuple<Reader, Writer>(Read, Write));

        public delegate void Reader(ref List<ScriptLine> lines, ref int start, ref int end, ref ScriptString function);
        public abstract void Read(ref List<ScriptLine> lines, ref int start, ref int end, ref ScriptString function);

        public delegate void Writer(ScriptLine line);
        public abstract void Write(ScriptLine line);

    }

}
