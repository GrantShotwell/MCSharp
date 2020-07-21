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
				new (Type, bool)[] { (typeof(VarSelector), true), (typeof(VarObjective), true) }
			};
			Methods.Add("GetScore", arguments => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(GetScoreInfo, arguments);
				match.Grab(arguments);

				VarInt value = new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
				VarSelector selector;
				VarObjective objective;
				switch(index) {
					case 0:
						selector = match[0].Value as VarSelector;
						objective = match[1].Value as VarObjective;
						goto GetScore;

						GetScore:
						value.SetValue(selector, objective);
						return value;

					default: throw new Compiler.InternalError($"Not all Scoreboard.GetScore overflows were accounted for ({index}).", arguments.ScriptTrace);

				}

			});

			ParameterInfo[] SetScoreInfo = new ParameterInfo[] {
				new (Type, bool)[] { (typeof(VarSelector), true), (typeof(VarObjective), true), (typeof(VarInt), true) },
				new (Type, bool)[] { (typeof(VarSelector), true), (typeof(VarObjective), true), (typeof(VarSelector), true), (typeof(VarObjective), true) }
			};
			Methods.Add("SetScore", arguments => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(SetScoreInfo, arguments);
				match.Grab(arguments);

				(VarSelector Selector, VarObjective Objective) to, from;
				switch(index) {
					case 0:
						to = (match[0].Value as VarSelector, match[1].Value as VarObjective);
						from = ((match[2].Value as VarInt).Selector, (match[2].Value as VarInt).Objective);
						goto SetScore;
					case 1:
						to = (match[0].Value as VarSelector, match[1].Value as VarObjective);
						from = (match[2].Value as VarSelector, match[3].Value as VarObjective);
						goto SetScore;

						SetScore:
						new Spy(null, $"scoreboard players operation " +
							$"{to.Selector.GetConstant()} {to.Objective.GetConstant()} = " +
							$"{from.Selector.GetConstant()} {from.Objective.GetConstant()}", null);
						return null;

					default: throw new Compiler.InternalError($"Not all Scoreboard.SetScore overflows were accounted for ({index}).", arguments.ScriptTrace);

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

					default: throw new Compiler.InternalError($"Not all Scoreboard.SetDisplay overflows were accounted for ({index}).", arguments.ScriptTrace);
                }

			});

		}

		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", trace);
		public override Variable Construct(ArgumentInfo passed) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", Compiler.CurrentScriptTrace);

	}

}
