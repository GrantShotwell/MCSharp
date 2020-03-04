using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

    public class UserClass : Variable {

        public ScriptClass ScriptClass { get; }
        public override string TypeName => ScriptClass.Alias;

        public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public, Access.Private };
        public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Abstract, Usage.Static };


        public UserClass() : base() { }

        public UserClass(ScriptClass scriptClass)
        : base(scriptClass.Access, scriptClass.Usage, scriptClass.Alias, Compiler.RootScope) {

            ScriptClass = scriptClass;
            foreach(ScriptMember member in ScriptClass.Values)
                if(member.Usage == Usage.Static) AddMember(member);

        }

        public UserClass(ScriptClass scriptClass, string objectName, Compiler.Scope scope)
        : base(scriptClass.Access, scriptClass.Usage, objectName, scope) {

            ScriptClass = scriptClass;
            foreach(ScriptMember member in ScriptClass.Values)
                if(member.Usage != Usage.Static) AddMember(member);

        }


        public void AddMember(ScriptMember member) {
            if(member is ScriptProperty property)
                Properties.Add(property.Alias, CompileProperty(property));
            else if(member is ScriptFunction function)
                Methods.Add(function.Alias, CompileFunction(function));
        }

        protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
            throw new NotImplementedException();
        }

        public static Tuple<GetProperty, SetProperty> CompileProperty(ScriptProperty property) {
            throw new NotImplementedException();
        }

        public static Func<Variable[], Variable> CompileFunction(ScriptFunction function) {
            throw new NotImplementedException();
        }

    }

}
