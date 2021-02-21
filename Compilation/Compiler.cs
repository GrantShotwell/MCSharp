using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MCSharp.Compilation {

	public class Compiler {

		public Settings Settings { get; }
		public VirtualMachine VirtualMachine { get; }

		public Compiler(Settings settings) {
			Settings = settings;
			VirtualMachine = new VirtualMachine();
		}

		#region Data

		public ICollection<ScriptClass> ScriptTypes { get; } = new List<ScriptClass>();

		#endregion

		public bool Compile(out string message) {

			// Find, parse, and first pass walk (types, members) all script files.
			foreach(string file in Settings.Datapack.GetScriptFiles()) {

				// Use Antlr generated classes to parse the file.
				ICharStream stream = CharStreams.fromString(File.ReadAllText(file));
				ITokenSource lexer = new MCSharpLexer(stream);
				ITokenStream tokens = new CommonTokenStream(lexer);
				var parser = new MCSharpParser(tokens) { BuildParseTree = true };
				IParseTree tree = parser.script();
				var walker = new ScriptClassWalker(this);
				ParseTreeWalker.Default.Walk(walker, tree);

			}

			// Compile every class/struct member.
			//todo

			message = "Finished.";
			return true;

		}

		public class ScriptClassWalker : MCSharpBaseListener {

			Compiler Compiler { get; }

			private MCSharpParser.Type_definitionContext CurrentTypeContext { get; set; }
			private ICollection<MCSharpParser.Member_definitionContext> CurrentMemberContexts { get; set; } = new LinkedList<MCSharpParser.Member_definitionContext>();

			public ScriptClassWalker(Compiler compiler) {
				Compiler = compiler;
			}

			public override void EnterType_definition([NotNull] MCSharpParser.Type_definitionContext context) {
				CurrentMemberContexts.Clear();
				CurrentTypeContext = context;
			}

			public override void EnterMember_definition([NotNull] MCSharpParser.Member_definitionContext context) {
				CurrentMemberContexts.Add(context);
			}

			public override void ExitType_definition([NotNull] MCSharpParser.Type_definitionContext context) {
				if(context != CurrentTypeContext) throw new Exception($"Subtypes are currently not supported by {nameof(ScriptClassWalker)}.");
				var scriptType = new ScriptClass(CurrentTypeContext, CurrentMemberContexts.ToArray(), Compiler.Settings, Compiler.VirtualMachine);
				Compiler.ScriptTypes.Add(scriptType);
			}

		}

	}

}
