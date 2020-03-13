using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;

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


		//protected override Variable Compile(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptWild[] arguments) {
		//	if(arguments.Length != 4 || arguments[0].IsWilds || arguments[1].IsWord || arguments[2].IsWilds || arguments[3].IsWord)
		//		throw new Compiler.SyntaxException($"Expected format of '= (...) => {{...}};' for creating '{TypeName}'.", arguments[0].ScriptTrace);
		//	string str = arguments[1];
		//	string[] split = str[1..^1].Split(',');
		//	Variable[] parameters = new Variable[split.Length];
		//	for(int i = 0; i < split.Length; i++) {
		//		string arg = split[i];
		//		string[] argargs = arg.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
		//		if(argargs.Length != 2) throw new Compiler.SyntaxException("Expected '[type] [name]'.", arguments[0].ScriptTrace);
		//		parameters[i] = new Parameter(argargs[0], argargs[1]).GetVariable(scope);
		//	}
		//	return new VarFunction(access, usage, objectName, scope, new ScriptMethod($"{scope}\\{objectName}\\Invoke",
		//		"void", parameters, Compiler.CurrentScope.DeclaringType, arguments[3]));
		//}

		protected override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) {
			base.Initialize(access, usage, name, scope, trace);

			throw new NotImplementedException();

		}

		public override void WriteInit(StreamWriter function) => Compiler.WriteFunction<VarVoid>(Scope, ScriptMethod);

		public struct Parameter {

			public string Type { get; }
			public string Name { get; }

			public Parameter(string type, string name) { Type = type; Name = name; }

			public Variable GetVariable(Compiler.Scope scope) {
				if(Compilers.TryGetValue(Type, out var compiler)) {
					return compiler.Invoke(Access.Private, Usage.Default, Name, scope, scope.DeclaringMethod.ScriptTrace);
				} else throw new Compiler.SyntaxException($"Unknown type '{Type}'.", Compiler.CurrentScriptTrace);
			}

		}

	}

}
