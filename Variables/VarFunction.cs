using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;
using static MCSharp.Compilation.ScriptObject;

namespace MCSharp.Variables {

	/// <summary>
	/// Represents a .mcfunction file.
	/// </summary>
	public class VarFunction : Variable {

		public override int Order => 100;
		public override string TypeName => "Function";
		public string FolderPath { get; }
		public string GamePath { get; }
		public List<string> Commands { get; } = new List<string>();
		public IReadOnlyList<Variable> Parameters => ScriptMethod.Parameters;
		public ScriptMethod ScriptMethod { get; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


		public VarFunction() : base() { }

		public VarFunction(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptMethod function) :
		base(access, usage, objectName, scope) {
			GamePath = $"{Program.Datapack.Name}:{function.Alias.Replace('\\', '/')}";
			FolderPath = $"{Program.Datapack.Name}:{function.FullAlias}.mcfunction";
			ScriptMethod = function;
			Methods.Add("Invoke", (args) => {
				if(args.Length != Parameters.Count)
					throw new InvalidArgumentsException($"Wrong number of arguments for '{this}'.Invoke(_).", Compiler.CurrentScriptTrace);
				new Spy(null, (function) => {
					for(int i = 0; i < args.Length; i++) args[i].WriteCopyTo(Compiler.FunctionStack.Peek(), Parameters[i]);
					function.WriteLine($"function {GamePath}");
				}, null);
				return null;
			});
		}


		protected override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) {
			base.Initialize(access, usage, name, scope, trace);

			throw new NotImplementedException();

		}

		public override void WriteInit(StreamWriter function) {
			base.WriteInit(function);
			Compiler.WriteFunction<VarVoid>(Scope, null, ScriptMethod);
		}

		public struct Parameter {

			public string Type { get; }
			public string Name { get; }

			public Parameter(string type, string name) { Type = type; Name = name; }

			public Variable GetVariable(Compiler.Scope scope) {
				if(Initializers.TryGetValue(Type, out var compiler)) {
					return compiler.Invoke(Access.Private, Usage.Default, Name, scope, scope.DeclaringMethod.ScriptTrace);
				} else throw new Compiler.SyntaxException($"Unknown type '{Type}'.", Compiler.CurrentScriptTrace);
			}

		}

	}

}
