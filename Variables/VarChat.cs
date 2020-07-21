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

			ParameterInfo[] SayInfo = new ParameterInfo[] {
				new (Type, bool)[] { (typeof(VarString), true) }
			};
			Methods.Add("Say", arguments => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(SayInfo, arguments);
				match.Grab(arguments);

				string text;
				switch(index) {
					case 0:
						text = match[0].Value.GetConstant();
						goto Say;

						Say:
						new Spy(null, $"say {text}", null);
						return null;

					default: throw new MissingOverloadException($"{TypeName}.Say", index, arguments);
				}

			});

			ParameterInfo[] TellrawInfo = new ParameterInfo[] {
				new (Type, bool)[] { (typeof(VarSelector), true), (typeof(VarJSON), true) }
			};
			Methods.Add("Tellraw", arguments => {
				(ParameterInfo match, int index) = ParameterInfo.HighestMatch(TellrawInfo, arguments);
				match.Grab(arguments);

				string selector, json;
				switch(index) {
					case 0:
						selector = match[0].Value.GetConstant();
						json = match[1].Value.GetJSON();
						goto Tellraw;

						Tellraw:
						new Spy(null, $"tellraw {selector} {json}", null);
						return null;

					default: throw new MissingOverloadException($"{TypeName}.Tellraw", index, arguments);
				}

			});

		}

		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", trace);
		public override Variable Construct(ArgumentInfo passed) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", Compiler.CurrentScriptTrace);

	}

}
