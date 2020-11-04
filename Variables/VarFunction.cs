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
		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "Function";
		public string FolderPath { get; }
		public string GamePath { get; }
		public List<string> Commands { get; } = new List<string>();
		public ParameterInfo Parameters => ScriptMethod.Parameters;
		public ScriptMethod ScriptMethod { get; }

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Private, Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Default, Usage.Constant, Usage.Static };


		public VarFunction() : base() { }

		public VarFunction(Access access, Usage usage, string objectName, Compiler.Scope scope, ScriptMethod function) :
		base(access, usage, objectName, scope) {
			GamePath = $"{Program.Datapack.Name}:{function.Alias.Replace('\\', '/')}";
			FolderPath = $"{Program.Datapack.Name}:{function.FullAlias}.mcfunction";
			ScriptMethod = function;
			Methods.Add("Invoke", (arguments) => {
				Parameters.Grab(arguments);
				new Spy(null, (function) => {
					//for(int i = 0; i < arguments.Count; i++) arguments[i].Value.WriteCopyTo(Compiler.FunctionStack.Peek(), Parameters[i].Value);
					function.WriteLine($"function {GamePath}");
				}, null);
				return null;
			});
		}


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => throw new NotImplementedException();
		public override Variable Construct(ArgumentInfo passed) => throw new NotImplementedException();
		public override void ConstructAsPasser() => throw new NotImplementedException();

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
