using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Variables {

    public abstract class Primitive : Variable {

        public string[] Init { get; protected set; }
        public string[] Copy { get; protected set; }
        public string[] Demo { get; protected set; }
        public string[] Prep { get; protected set; }

        public Primitive() : base() { }

        public Primitive(AccessModifier[] accessModifiers, UsageModifier[] usageModifiers, string objectName, Compiler.Scope scope) :
        base(accessModifiers, usageModifiers, objectName, scope) { }

        public override void WriteInit() {
            StreamWriter function = Compiler.FunctionStack.Peek();
            foreach(string command in Init) function.WriteLine(command);
        }

        public override void WritePrep() {
            StreamWriter function = Compiler.PrepFunction;
            foreach(string command in Prep) function.WriteLine(command);
        }

    }

}
