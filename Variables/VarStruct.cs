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

		public override Variable Construct(ArgumentInfo arguments) {
			ParameterInfo[] overloads = new ParameterInfo[Constructors.Count];
			for(int i = 0; i < overloads.Length; i++) overloads[i] = Constructors[i].Parameters;
			(ParameterInfo match, int index) = ParameterInfo.HighestMatch(overloads, arguments);
			return Constructors[index].Constructor(arguments);
		}

		public override void ConstructAsPasser() {
			foreach(KeyValuePair<string, Variable> field in Fields) {
				field.Value.ConstructAsPasser();
			}
		}

		public override void AddField(ScriptField field) {
			Variable value = Initializers[field.TypeName](field.Access, field.Usage, field.Alias, InnerScope, field.ScriptTrace);
			value.InvokeOperation(Operation.Set, Compiler.ParseValue(field.Init, field.Scope), field.ScriptTrace);
			Fields.Add(value.ObjectName, value);
		}

		public override void AddProperty(ScriptProperty property) {
			var get = property.GetMethod;
			get = new ScriptMethod(get.Alias, get.TypeName, get.Parameters.Copy(), get.DeclaringType, get.ScriptLines, InnerScope, true, get.Access, get.Usage, get.ScriptTrace);
			var set = property.SetMethod;
			set = new ScriptMethod(set.Alias, set.TypeName, set.Parameters.Copy(), set.DeclaringType, set.ScriptLines, InnerScope, true, set.Access, set.Usage, set.ScriptTrace);
			property = new ScriptProperty(property.Alias, property.TypeName, get, set, property.Access, property.Usage, property.DeclaringType, property.ScriptTrace, InnerScope);
			Properties.Add(property.Alias, (property.GetFunc, property.SetFunc));
		}

		public override void AddConstructor(ScriptConstructor constructor) {
			//Constructors.Add(constructor.Delegate);
		}

		public override void AddMethod(ScriptMethod method) {
			Methods.Add(method.Alias, method.Delegate);
		}

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) {


			switch(operation) {

				case Operation.Set: {
#if DEBUG_OUT
					new Spy(null, $"# OP # {this}@{Scope} := {operand}@{operand.Scope}", null);
#endif
					if((operand is VarStruct varStruct || operand.TryCast(TypeName, out varStruct))
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
