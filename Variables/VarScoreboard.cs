using MCSharp.Compilation;
using System;
using System.Collections.Generic;

namespace MCSharp.Variables {

	public class VarScoreboard : Variable {

		public override int Order => 100;
		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "Scoreboard";

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Static };

		public VarScoreboard() : base() {
			Compiler.StaticClassObjects.Add("Scoreboard", new VarScoreboard("Scoreboard"));
		}

		public VarScoreboard(string name) : base(Access.Public, Usage.Static, name, Compiler.RootScope) {

			ParameterInfo[] GetScoreInfo = new ParameterInfo[] {
				new (Type, bool)[] { (typeof(VarObjective), true) },
				new (Type, bool)[] { (typeof(VarObjective), true), (typeof(VarSelector), true) }
			};
			Methods.Add("GetScore", arguments => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(GetScoreInfo, arguments);
				match.Grab(arguments);

				VarInt value = new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
				VarObjective objective;
				VarSelector selector;
				switch(index) {
					case 0:
						objective = match[0].Value as VarObjective;
						selector = (VarSelector)"@e";
						goto GetScore;
					case 1:
						objective = match[0].Value as VarObjective;
						selector = match[1].Value as VarSelector;
						goto GetScore;

						GetScore:
						value.SetValue(selector, objective);
						return value;

					default: throw new Compiler.InternalError($"Not all Scoreboard.SetDisplay overflows were accounted for ({index}).");

				}

			});

			ParameterInfo[] SetDisplayInfo = new ParameterInfo[] {
				new (Type, bool)[] { (typeof(VarString), true) },
				new (Type, bool)[] { (typeof(VarString), true), (typeof(VarObjective), true) }
			};
			Methods.Add("SetDisplay", arguments => {
                (ParameterInfo match, int index) = ParameterInfo.HighestMatch(SetDisplayInfo, arguments);
				match.Grab(arguments);

				string display, objective;
				switch(index) {
					case 0:
						display = match[0].Value.GetConstant();
						objective = string.Empty;
						goto SetDisplay;
					case 1:
						display = match[0].Value.GetConstant();
						objective = match[1].Value.GetConstant();
						goto SetDisplay;

						SetDisplay:
						new Spy(null, $"scoreboard objectives setdisplay {display} {objective}", null);
						return null;

					default: throw new Compiler.InternalError($"Not all Scoreboard.SetDisplay overflows were accounted for ({index}).");
                }

			});

		}

		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", trace);
		public override Variable Construct(ArgumentInfo passed) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", Compiler.CurrentScriptTrace);

	}

}
