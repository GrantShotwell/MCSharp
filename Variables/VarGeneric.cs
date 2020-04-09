using MCSharp.Compilation;
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
			Compiler.StaticClassObjects.Add(TypeName, this);
			Compilers.Add(TypeName, Initialize);
			foreach(var member in from ScriptMember member in ScriptClass.Values where member.Usage == Usage.Static select member)
				AddMember(member);

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
			else if(member is ScriptField field)
				Fields.Add(field.Alias, CompileField(field));
			else throw new Exception("112104082020");
		}

		protected override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) {
			base.Initialize(access, usage, name, scope, trace);

			throw new NotImplementedException("TODO: constructors");

		}

		public static Tuple<GetProperty, SetProperty> CompileProperty(ScriptProperty property) {
			GetProperty get = null;
			SetProperty set = null;
			if(!(property.GetFunc is null)) {
				Compiler.WriteFunction<Variable>(Compiler.CurrentScope, property.GetMethod);
				get = property.GetFunc;
			}
			if(!(property.SetFunc is null)) {
				Compiler.WriteFunction<Variable>(Compiler.CurrentScope, property.SetMethod);
				set = property.SetFunc;
			}
			return new Tuple<GetProperty, SetProperty>(get, set);
		}

		public static MethodDelegate CompileFunction(ScriptMethod function) {
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

		public static Variable CompileField(ScriptField field) {
			if(field is null) throw new ArgumentNullException(nameof(field));
			ScriptWild init = field.Init;
			Variable fieldVariable = Compilers[field.TypeName].Invoke(field.Access, field.Usage, field.Alias, Compiler.CurrentScope, field.ScriptTrace);
			if(init.Array.Length > 0) {
				if(init.IsWord || !(init.Wilds[0] == "=")) throw new Compiler.SyntaxException("Expected '='.", init.ScriptTrace);
				if(init.Wilds.Count < 2) throw new Compiler.SyntaxException("Expected value.", init.ScriptTrace);
				if(Compiler.TryParseValue(new ScriptWild(init[1..].Array, "(\\)", ' '), Compiler.CurrentScope, out Variable value)) {
					fieldVariable.InvokeOperation(Operation.Set, value, init.ScriptTrace);
				} else throw new Compiler.SyntaxException($"Could not parse into '{field.TypeName}'.", field.ScriptTrace);
			}
			return fieldVariable;
		}

	}

}
