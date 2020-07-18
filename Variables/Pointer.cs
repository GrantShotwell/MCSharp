using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Variables {

	class Pointer : Variable {

		public Variable Variable { get; set; }

		public override string TypeName => Variable.TypeName;
		public override ICollection<Access> AllowedAccessModifiers => Variable.AllowedAccessModifiers;
		public override ICollection<Usage> AllowedUsageModifiers => Variable.AllowedUsageModifiers;
		public override int Order => Variable.Order;

		public override Variable Construct(Variable[] arguments) => Variable.Construct(arguments);
		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => Variable.Initialize(access, usage, name, scope, trace);

		public override Variable InvokeOperation(Operation operation, Variable operand, ScriptTrace trace) => Variable.InvokeOperation(operation, operand, trace);
		public override void WriteCopyTo(StreamWriter function, Variable variable) => Variable.WriteCopyTo(function, variable);
		public override IDictionary<Type, Caster> GetCasters_From() => Variable.GetCasters_From();
		public override IDictionary<Type, Caster> GetCasters_To() => Variable.GetCasters_To();

		public override void WritePrep(StreamWriter function) => Variable.WritePrep(function);
		public override void WriteInit(StreamWriter function) => Variable.WriteInit(function);
		public override void WriteTick(StreamWriter function) => Variable.WriteTick(function);
		public override void WriteDele(StreamWriter function) => Variable.WriteDele(function);
		public override void WriteDemo(StreamWriter function) => Variable.WriteDemo(function);

		public override string GetConstant() => Variable.GetConstant();
		public override string GetJSON() => Variable.GetJSON();
		public override VarString GetString() => Variable.GetString();

	}

	class Pointer<TVariable> : Pointer where TVariable : Variable {

		public new TVariable Variable {
			get => base.Variable as TVariable;
			set => base.Variable = value;
		}

	}
}
