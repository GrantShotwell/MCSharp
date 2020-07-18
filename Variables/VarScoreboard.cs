using MCSharp.Compilation;
using System.Collections.Generic;

namespace MCSharp.Variables {

	public class VarScoreboard : Variable {

		public override int Order => 100;
		public override string TypeName => "Scoreboard";

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Static };

		public VarScoreboard() : base() {
			Compiler.StaticClassObjects.Add("Scoreboard", new VarScoreboard("Scoreboard"));
		}

		public VarScoreboard(string name) : base(Access.Public, Usage.Static, name, Compiler.RootScope) {
			Methods.Add("GetScore", args => GetScore(args));
			Methods.Add("SetDisplay", args => SetDisplay(args));
			Methods.Add("ClearDisplay", args => ClearDisplay(args));
		}

		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) {
			base.Initialize(access, usage, name, scope, trace);
			throw new Compiler.SyntaxException("Cannot make an instance of a static class.", trace);
		}

		private Variable GetScore(Variable[] args) {
			if(args.Length != 2)
				throw new InvalidArgumentsException($"Invalid number of arguments for '{TypeName}' method.", Compiler.CurrentScriptTrace);
			if(!(args[0] is VarSelector target) && !args[0].TryCast(out target))
				throw new InvalidCastException(args[0], "Selector", Compiler.CurrentScriptTrace);
			if(!(args[1] is VarObjective objective) && !args[1].TryCast(out objective))
				throw new InvalidCastException(args[1], "Objective", Compiler.CurrentScriptTrace);

			var value = new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
			value.SetValue(target.GetConstant(), objective.GetConstant());
			return value;

		}

		private Variable ClearDisplay(Variable[] args) {
			if(args.Length != 1)
				throw new InvalidArgumentsException($"Invalid number of arguments for '{TypeName}' method.", Compiler.CurrentScriptTrace);
			if(!(args[0] is VarString slot) && !args[0].TryCast(out slot))
				throw new InvalidCastException(args[0], "String", Compiler.CurrentScriptTrace);

			new Spy(null, $"scoreboard objectives setdisplay {slot.GetConstant()}", null);
			return null;

		}

		private Variable SetDisplay(Variable[] args) {
			if(args.Length != 2)
				throw new InvalidArgumentsException($"Invalid number of arguments for '{TypeName}' method.", Compiler.CurrentScriptTrace);
			if(!(args[0] is VarString slot) && !args[0].TryCast(out slot))
				throw new InvalidCastException(args[0], "String", Compiler.CurrentScriptTrace);
			if(!(args[1] is VarObjective objective) && !args[1].TryCast(out objective))
				throw new InvalidCastException(args[1], "Objective", Compiler.CurrentScriptTrace);

			new Spy(null, $"scoreboard objectives setdisplay {slot.GetConstant()} {objective.GetConstant()}", null);
			return null;

		}

	}

}
