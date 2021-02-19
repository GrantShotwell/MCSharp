using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp {

	public class Compiler {

		public Settings Settings { get; }

		public Compiler(Settings settings) {
			Settings = settings;
		}

		#region Data
		#endregion

		public bool Compile(out string message) {

			// Find & parse all script files.
			foreach(string file in Settings.Datapack.GetScriptFiles()) {

				// Use Antlr generated classes to parse the file.
				ICharStream stream = CharStreams.fromString(File.ReadAllText(file));
				ITokenSource lexer = new MCSharpLexer(stream);
				ITokenStream tokens = new CommonTokenStream(lexer);
				MCSharpParser parser = new MCSharpParser(tokens) { BuildParseTree = true };
				IParseTree tree = parser.script();
				ScriptKeyPrinter printer = new ScriptKeyPrinter();
				ParseTreeWalker.Default.Walk(printer, tree);

			}

			message = "Finished parsing.";
			return true;

		}

		public class ScriptKeyPrinter : MCSharpBaseListener {

			public override void ExitScript([NotNull] MCSharpParser.ScriptContext context) {
				base.ExitScript(context);
				Program.PrintText($"Script Token: {context.GetText()}");
			}

		}


	}

}
