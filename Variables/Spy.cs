using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace MCSharp.Variables {
    public class Spy : Variable {

        public Action<StreamWriter> Prep { get; }
        public Action<StreamWriter> Init { get; }
        public Action<StreamWriter> Demo { get; }

        public override int Order => 100;
        public override string TypeName => throw new InvalidOperationException("Spies are not variables.");
        public override ICollection<Access> AllowedAccessModifiers => throw new InvalidOperationException("Spies are not variables.");
        public override ICollection<Usage> AllowedUsageModifiers => throw new InvalidOperationException("Spies are not variables.");

        public Spy() { }

        public Spy(string prep, string init, string demo) :
        base(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope) {
            Init = (function) => function.WriteLine(init);
            Prep = (function) => function.WriteLine(prep);
            Demo = (function) => function.WriteLine(demo);
        }

        public Spy(string[] prep, string[] init, string[] demo) :
        base(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope) {
            Prep = (function) => { if(prep != null) foreach(string command in prep) function.WriteLine(command); };
            Init = (function) => { if(init != null) foreach(string command in init) function.WriteLine(command); };
            Demo = (function) => { if(demo != null) foreach(string command in demo) function.WriteLine(command); };
        }

        public Spy(Action<StreamWriter> prep, Action<StreamWriter> init, Action<StreamWriter> demo) :
        base(Access.Private, Usage.Default, NextHiddenID, Compiler.CurrentScope) {
            Prep = prep;
            Init = init;
            Demo = demo;
        }

        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) { return null; }

        public override void WriteInit(StreamWriter function) { if(Init != null) Init.Invoke(function); }
        public override void WritePrep(StreamWriter function) { if(Prep != null) Prep.Invoke(function); }
        public override void WriteDemo(StreamWriter function) { if(Demo != null) Demo.Invoke(function); }

    }
}
