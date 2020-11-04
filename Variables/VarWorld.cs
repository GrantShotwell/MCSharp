using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Variables {
	class VarWorld : Variable {

		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "World";

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Static };


		public VarWorld() : base() {
			Compiler.StaticClassObjects.Add("World", new VarWorld("World"));
		}

		public VarWorld(string name) : base(Access.Public, Usage.Static, name, Compiler.RootScope) {

			Compiler.Scope[] scopesSetTime = InnerScope.CreateChildren(1);
			ParameterInfo[] infoSetTime = new ParameterInfo[] {
				new ParameterInfo(
					(true, VarString.StaticTypeName, "time", scopesSetTime[0])
				)
			};
			Methods.Add("SetTime", arguments => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(infoSetTime, arguments);
				match.Grab(arguments);

				switch(index) {
					case 0:
						new Spy(null, $"time set {match["time"].Value.GetConstant()}", null);
						return null;

					default: throw new MissingOverloadException($"{TypeName}.SetTime", index, arguments);
				}
			});

			Compiler.Scope[] scopesAddTime = InnerScope.CreateChildren(1);
			ParameterInfo[] infoAddTime = new ParameterInfo[] {
				new ParameterInfo(
					(true, VarString.StaticTypeName, "time", scopesAddTime[0])
				)
			};
			Methods.Add("AddTime", arguments => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(infoAddTime, arguments);
				match.Grab(arguments);

				string amount;
				switch(index) {
					case 0:
						amount = match["time"].Value.GetConstant();
						goto AddTime;

						AddTime:
						new Spy(null, $"time add {amount}", null);
						return null;

					default: throw new MissingOverloadException($"{TypeName}.AddTime", index, arguments);
				}
			});

			Compiler.Scope[] scopesGetTime = InnerScope.CreateChildren(2);
			ParameterInfo[] infoGetTime = new ParameterInfo[] {
				new ParameterInfo(
					(true, VarString.StaticTypeName, "type", scopesGetTime[0])
				),
			};
			Methods.Add("GetTime", arguments => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(infoGetTime, arguments);
				match.Grab(arguments);

				VarInt result = new VarInt(Access.Private, Usage.Default, GetNextHiddenID(), Compiler.CurrentScope);
				string type;
				switch(index) {
					case 0:
						type = match[0].Value.GetConstant();
						result.SetValue(-1);
						goto GetTime;
					case 1:
						type = match[0].Value.GetConstant();
						result.SetValue(match[1].Value as VarSelector, match[2].Value as VarObjective);
						goto GetTime;

						GetTime:
						new Spy(null, $"execute store result score {result.Selector.GetConstant()} {result.Objective.GetConstant()} run time query {type}", null);
						return result;

					default: throw new MissingOverloadException($"{TypeName}.GetTime", index, arguments);
				}
			});

		}


		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", trace);
		public override Variable Construct(ArgumentInfo passed) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", Compiler.CurrentScriptTrace);
		public override void ConstructAsPasser() => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", Compiler.CurrentScriptTrace);


	}
}
