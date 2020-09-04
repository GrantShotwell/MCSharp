using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static MCSharp.Compilation.ScriptObject;

namespace MCSharp.Variables {

	public class VarStruct : VarGeneric {

		private int callID = 0;
		private int GetNextCallID() => ++callID;

		public override ICollection<Access> AllowedAccessModifiers => new Access[] {
			Access.Public, Access.Private };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] {
			Usage.Abstract, Usage.Virtual, Usage.Override, Usage.Default, Usage.Static, Usage.Constant };


		public VarStruct() : base() { }
		public VarStruct(ScriptObject script) : base(script) { }
		public VarStruct(Access access, Usage usage, string name, Compiler.Scope scope, ScriptObject script)
		: base(access, usage, name, scope, script) { }


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => new VarStruct(access, usage, name, scope, ScriptClass);

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

		public override void ConstructAsPasser() {
			foreach(KeyValuePair<string, Variable> field in Fields) {
				field.Value.ConstructAsPasser();
			}
		}

		public override (GetProperty get, SetProperty set) CompileProperty(ScriptProperty property) {
			GetProperty get = null;
			if(!(property.GetFunc is null)) {
				Compiler.WriteFunction<Variable>(Scope, this, property.GetMethod);
				get = property.GetFunc;
			}
			SetProperty set = null;
			if(!(property.SetFunc is null)) {
				Compiler.WriteFunction<Variable>(Scope, this, property.SetMethod);
				set = property.SetFunc;
			}
			return (get, set);
		}

		public override MethodDelegate CompileMethod(ScriptMethod method) {

			// Write function.
			Compiler.WriteFunction<Variable>(method.Scope, this, method);

			// Make delegate.
			return (arguments) => {
				// Check number of arguments.
				if(arguments.Count != method.Parameters.Length)
					throw new InvalidArgumentsException($"Wrong number of arguments for '{method.Alias}'.Invoke(_).", Compiler.CurrentScriptTrace);
				new Spy(null, (function) => {
					// Copy arguments to parameters.
					for(int i = 0; i < arguments.Count; i++) arguments[i].Value.WriteCopyTo(Compiler.FunctionStack.Peek(), method.Parameters[i]);
					// Call the function.
					function.WriteLine($"function {method.GameName}");
				}, null);
				// Give return value of method.
				return method.ReturnValue;
			};

		}

		public override Variable CompileField(ScriptField field) {
			if(field is null) throw new ArgumentNullException(nameof(field));
			ScriptWild init = field.Init;
			Variable fieldVariable = Initializers[field.TypeName].Invoke(field.Access, field.Usage, $"{field.Alias}@{ObjectName}", InnerScope, field.ScriptTrace);
			if(init.Array.Length > 0) {
				if(init.IsWord || !(init.Wilds[0] == "=")) throw new Compiler.SyntaxException("Expected '='.", init.ScriptTrace);
				if(init.Wilds.Count < 2) throw new Compiler.SyntaxException("Expected value.", init.ScriptTrace);
				Variable value = Compiler.ParseValue(new ScriptWild(init[1..].Array, "(\\)", ' '), Compiler.CurrentScope);
				fieldVariable.InvokeOperation(Operation.Set, value, init.ScriptTrace);
			}
			return fieldVariable;
		}

		public override Constructor CompileConstructor(ScriptConstructor constructor) {

			// Check if function has already been written.
			if(!CompiledConstructors.Contains(constructor)) {
				// Record the function has been written.
				CompiledConstructors.Add(constructor);
				// Write function.
				Compiler.WriteFunction<VarStruct>(constructor.Scope, constructor.ReturnValue, constructor);
			}

			// Make delegate.
			return (arguments) => {

				// Check that the number of given arguments is correct.
				if(arguments.Count != constructor.Parameters.Length)
					throw new InvalidArgumentsException($"Wrong number of arguments for '{constructor.Alias}'.Invoke(_).", Compiler.CurrentScriptTrace);

				// Reset metadata.
				Variable reset = Initializers[TypeName].Invoke(Access.Private, Usage.Default, GetNextHiddenID(), constructor.Scope, Compiler.CurrentScriptTrace);

				// Call the constructor.
				new Spy(null, function => {
					// Reset metadata.
					reset.WriteCopyTo(function, constructor.ReturnValue);
					// Copy arguments to parameters.
					for(int i = 0; i < arguments.Count; i++) arguments[i].Value.WriteCopyTo(Compiler.FunctionStack.Peek(), constructor.Parameters[i]);
					// Call the constructor function.
					function.WriteLine($"function {constructor.GameName}");
				}, null);

				// Give the return value.
				return constructor.ReturnValue;

			};

		}

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {
			switch(operation) {

				case Operation.Set: {
					if((operand is VarStruct varStruct || operand.TryCast(out varStruct))
					&& varStruct.TypeName == TypeName /* || [TODO:  user-defined casts] */) {
						foreach((string field, Variable value) in varStruct.Fields)
							Fields[field].InvokeOperation(Operation.Set, value, trace);
						return this;
					} else throw new Compiler.SyntaxException($"Cannot cast from '{operand.TypeName}' to '{TypeName}'.", trace);
				}

				default: return base.InvokeOperation(operation, operand, trace);
			}
		}

		public override void WriteCopyTo(StreamWriter function, Variable variable) {
			if(variable is Pointer<VarStruct> pointer) pointer.Variable = this;
			else if(variable is VarStruct varStruct && varStruct.ScriptClass == ScriptClass) {
				foreach(KeyValuePair<string, Variable> pair in Fields) {
					string name = pair.Key;
					Variable value = pair.Value;
					value.WriteCopyTo(function, varStruct.Fields[name]);
				}
			} else throw new Exception();

		}
	}

}
