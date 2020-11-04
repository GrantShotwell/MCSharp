using MCSharp.Compilation;
using System;
using System.Collections.Generic;

namespace MCSharp.Variables {

	public class VarChat : Variable {

		public override int Order => 100;
		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "Chat";
		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Static };

		public VarChat() : base() {
			Compiler.StaticClassObjects.Add("Chat", new VarChat("Chat"));
		}

		public VarChat(string name) : base(Access.Public, Usage.Static, name, Compiler.RootScope) {

			Compiler.Scope[] scopeSay = new Compiler.Scope[1];
			for(int i = 0; i < scopeSay.Length; i++) scopeSay[i] = new Compiler.Scope(InnerScope);
			ParameterInfo[] infoSay = new ParameterInfo[] {
				new ParameterInfo(
					(true, VarString.StaticTypeName, "text", scopeSay[0])
				)
			};
			Methods.Add("Say", arguments => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(infoSay, arguments);
				match.Grab(arguments);

				switch(index) {
					case 0:
						string text = match["text"].Value.GetConstant();
						new Spy(null, $"say {text}", null);
						return null;

					default: throw new MissingOverloadException($"{TypeName}.Say", index, arguments);
				}

			});

			Compiler.Scope[] scopeTellraw = new Compiler.Scope[1];
			for(int i = 0; i < scopeTellraw.Length; i++) scopeTellraw[i] = new Compiler.Scope(InnerScope);
			ParameterInfo[] infoTellraw = new ParameterInfo[] {
				new ParameterInfo(
					(true, VarSelector.StaticTypeName, "targets", scopeTellraw[0]),
					(true, VarJson.StaticTypeName, "json", scopeTellraw[0])
				)
			};
			Methods.Add("Tellraw", arguments => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(infoTellraw, arguments);
				match.Grab(arguments);

				switch(index) {
					case 0:
						string selector = match["selector"].Value.GetConstant();
						string json = match["json"].Value.GetRawText().GetJson();
						new Spy(null, $"tellraw {selector} {json}", null);
						return null;

					default: throw new MissingOverloadException($"{TypeName}.Tellraw", index, arguments);
				}

			});

		}

		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", trace);
		public override Variable Construct(ArgumentInfo passed) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", Compiler.CurrentScriptTrace);
		public override void ConstructAsPasser() => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", Compiler.CurrentScriptTrace);

	}

}
