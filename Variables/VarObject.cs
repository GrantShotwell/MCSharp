using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;
using static MCSharp.Compilation.ScriptObject;

namespace MCSharp.Variables {

	public class VarObject : VarGeneric {

		public override ICollection<Access> AllowedAccessModifiers => new Access[] {
			Access.Public, Access.Private };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] {
			Usage.Abstract, Usage.Virtual, Usage.Override, Usage.Default, Usage.Static };

		public VarSelector Selector { get; private set; }


		public VarObject() : base() { }
		public VarObject(ScriptObject script) : base(script) { }
		public VarObject(Access access, Usage usage, string name, Compiler.Scope scope, ScriptObject script)
		: base(access, usage, name, scope, script) { }


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => new VarObject(access, usage, name, scope, ScriptClass);

		public override Variable Construct(ArgumentInfo passed) {
			//TODO: better 'finder' for overflows
			foreach(Constructor constructor in Constructors) {
				try {
					return constructor.Invoke(passed);
				} catch(InvalidArgumentsException) {
					continue;
				} catch(InvalidCastException) {
					continue;
				}
			}
			throw new Compiler.SyntaxException("Could not find a valid overflow for constructor.", Compiler.CurrentScriptTrace);
		}

		public override (GetProperty get, SetProperty set) CompileProperty(ScriptProperty property) {
			GetProperty get = null;
			SetProperty set = null;
			if(!(property.GetFunc is null)) {
				Compiler.WriteFunction<Variable>(Scope, this, property.GetMethod);
				get = property.GetFunc;
			}
			if(!(property.SetFunc is null)) {
				Compiler.WriteFunction<Variable>(Scope, this, property.SetMethod);
				set = property.SetFunc;
			}
			return (get, set);
		}

		public override MethodDelegate CompileMethod(ScriptMethod function) {
			Compiler.WriteFunction<Variable>(Scope, this, function);
			return (arguments) => {
				if(arguments.Count != function.Parameters.Length)
					throw new InvalidArgumentsException($"Wrong number of arguments for '{function.Alias}'.Invoke(_).", Compiler.CurrentScriptTrace);
				new Spy(null, (func) => {
					for(int i = 0; i < arguments.Count; i++) arguments[i].Value.WriteCopyTo(Compiler.FunctionStack.Peek(), function.Parameters[i]);
					func.WriteLine($"function {function.GameName}");
				}, null);
				return function.ReturnValue;
			};
		}

		public override Variable CompileField(ScriptField field) {
			if(field is null) throw new ArgumentNullException(nameof(field));
			ScriptWild init = field.Init;
			Variable fieldVariable = Initializers[field.TypeName].Invoke(field.Access, field.Usage, field.Alias, Scope, field.ScriptTrace);
			if(init.Array.Length > 0) {
				if(init.IsWord || !(init.Wilds[0] == "=")) throw new Compiler.SyntaxException("Expected '='.", init.ScriptTrace);
				if(init.Wilds.Count < 2) throw new Compiler.SyntaxException("Expected value.", init.ScriptTrace);
				Variable value = Compiler.ParseValue(new ScriptWild(init[1..].Array, "(\\)", ' '), Compiler.CurrentScope);
				fieldVariable.InvokeOperation(Operation.Set, value, init.ScriptTrace);
			}
			return fieldVariable;
		}

		public override Constructor CompileConstructor(ScriptConstructor constructor) {
			return (arguments) => {
				if(arguments.Count != constructor.Parameters.Length)
					throw new InvalidArgumentsException($"Wrong number of arguments for '{constructor.Alias}'.Invoke(_).", Compiler.CurrentScriptTrace);
				Variable buffer = Initialize(Access.Private, Usage.Default, GetNextHiddenID(),
					new Compiler.Scope(Compiler.CurrentScope), Compiler.CurrentScriptTrace);
				Compiler.WriteFunction<Variable>(Compiler.CurrentScope, buffer, constructor);
				new Spy(null, (func) => {
					for(int i = 0; i < arguments.Count; i++) arguments[i].Value.WriteCopyTo(Compiler.FunctionStack.Peek(), constructor.Parameters[i]);
					func.WriteLine($"function {constructor.GameName}");
				}, null);
				return constructor.ReturnValue;
			};
		}

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			switch(operation) {

				case Operation.Set: {
					if((operand is VarObject varObject || operand.TryCast(out varObject))
					&& varObject.TypeName == TypeName /* || [TODO:  user-defined casts] */) {
						Selector = varObject.Selector;
						return this;
					} else throw new Compiler.SyntaxException($"Cannot cast from '{operand.TypeName}' to '{TypeName}'.", trace);
				}

				default: return base.InvokeOperation(operation, operand, trace);
			}
		}

	}

}
