﻿using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCSharp.Variables {

    public class VarGeneric : Variable {

        public ScriptClass ScriptClass { get; }
        public override string TypeName => ScriptClass.Alias;

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Static };


        public VarGeneric() : base() { }

        public VarGeneric(ScriptClass scriptClass)
        : base(scriptClass.Access, scriptClass.Usage, scriptClass.Alias, Compiler.RootScope) {

            ScriptClass = scriptClass;
            foreach(var member in from ScriptMember member in ScriptClass.Values where member.Usage == Usage.Static select member)
                AddMember(member);
            Compilers.Add(TypeName, Compile);

        }

        public VarGeneric(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptClass scriptClass)
        : base(access, usage, objectName, scope) {

            ScriptClass = scriptClass;
            foreach(var member in from ScriptMember member in ScriptClass.Values where member.Usage != Usage.Static select member)
                AddMember(member);

        }


        public void AddMember(ScriptMember member) {
            if(member is ScriptProperty property)
                Properties.Add(property.Alias, CompileProperty(property));
            else if(member is ScriptMethod function)
                Methods.Add(function.Alias, CompileFunction(function));
        }
        
        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            if(ScriptClass.Usage == Usage.Static)
                throw new Compiler.SyntaxException($"Cannot create instance of static class '{ScriptClass}'.", arguments[0].ScriptTrace);
            return new VarGeneric(access, usage, objectName, scope, ScriptClass);
        }

        public static Tuple<GetProperty, SetProperty> CompileProperty(ScriptProperty property) {
            throw new NotImplementedException();
        }

        public static Func<Variable[], Variable> CompileFunction(ScriptMethod function) {
            Compiler.WriteFunction<Variable>(Compiler.CurrentScope, function);
            return (args) => {
                if(args.Length != function.Parameters.Length)
                    throw new InvalidArgumentsException($"Wrong number of arguments for '{function.Alias}'.Invoke(_).", Compiler.CurrentScriptTrace);
                new Spy(null, (func) => {
                    for(int i = 0; i < args.Length; i++) args[i].WriteCopyTo(Compiler.FunctionStack.Peek(), function.Parameters[i]);
                    func.WriteLine($"function {function.GameName}");
                }, null);
                return null;
            };
        }

    }

}
