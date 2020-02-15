using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace MCSharp.Variables {
    public class MethodSpy : Variable {

        public string[] Prep { get; }
        public string[] Init { get; }
        public string[] Demo { get; }

        public override int Order => throw new InvalidOperationException();
        public override string TypeName => throw new InvalidOperationException();
        public override ICollection<Access> AllowedAccessModifiers => throw new InvalidOperationException("Spies are not variables.");
        public override ICollection<Usage> AllowedUsageModifiers => throw new InvalidOperationException("Spies are not variables.");

        public MethodSpy() { }

        public MethodSpy(Compiler.Scope scope, string[] prep, string[] init, string[] demo) :
        base(Access.Private, Usage.Default, NextHiddenID, scope) {
            Prep = prep;
            Init = init;
            Demo = demo;
        }

        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) { return null; }

        public override void WritePrep() {
            if(Prep == null) return;
            StreamWriter function = Compiler.FunctionStack.Peek();
            foreach(string command in Prep) function.WriteLine(command);
        }

        public override void WriteInit() {
            if(Init == null) return;
            StreamWriter function = Compiler.FunctionStack.Peek();
            foreach(string command in Init) function.WriteLine(command);
        }

        public override void WriteDemo() {
            if(Demo == null) return;
            StreamWriter function = Compiler.FunctionStack.Peek();
            foreach(string command in Demo) function.WriteLine(command);
        }

        public override void CompileOperation(ScriptWord operation, ScriptWild[] arguments) => throw new NotImplementedException();

    }
}
