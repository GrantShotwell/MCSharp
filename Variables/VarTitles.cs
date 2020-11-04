using MCSharp.Compilation;
using System.Collections.Generic;

namespace MCSharp.Variables {

	public class VarTitles : Variable {

		public override int Order => 100;
		public override string TypeName => StaticTypeName;
		public static string StaticTypeName => "Titles";

		public override ICollection<Access> AllowedAccessModifiers => new Access[] { Access.Public };
		public override ICollection<Usage> AllowedUsageModifiers => new Usage[] { Usage.Static };

		public VarTitles() : base() {
			Compiler.StaticClassObjects.Add("Titles", new VarTitles("Titles"));
		}

		public VarTitles(string name) : base(Access.Public, Usage.Static, name, Compiler.RootScope) {
			Methods.Add("ShowTitle", args => Show("title", args));
			Methods.Add("ShowSubtitle", args => Show("subtitle", args));
			Methods.Add("ShowActionbar", args => Show("actionbar", args));
		}

		public override Variable Initialize(Access access, Usage usage, string name, Compiler.Scope scope, ScriptTrace trace) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", trace);
		public override Variable Construct(ArgumentInfo passed) => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", passed.ScriptTrace);
		public override void ConstructAsPasser() => throw new Compiler.SyntaxException("Cannot make an instance of a static class.", Compiler.CurrentScriptTrace);

		private Variable Show(string type, ArgumentInfo arguments) {
			if(arguments.Count != 2)
				throw new InvalidArgumentsException($"Invalid number of arguments for '{TypeName}' method.", Compiler.CurrentScriptTrace);
			if(!(arguments[0].Value is VarSelector selector) && !arguments[0].Value.TryCast(VarSelector.StaticTypeName, out selector))
				throw new InvalidCastException(arguments[0].Value, "Selector", Compiler.CurrentScriptTrace);
			if(!(arguments[1].Value is VarJson json) && !arguments[1].Value.TryCast(VarJson.StaticTypeName, out json))
				throw new InvalidCastException(arguments[1].Value, "JSON", Compiler.CurrentScriptTrace);

			new Spy(null, new string[] { $"title {selector.GetConstant()} {type} {json.GetConstant()}" }, null);
			return null;

		}

	}

}
