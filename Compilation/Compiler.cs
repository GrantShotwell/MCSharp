using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp {

	public class Compiler {

		public static string DefaultSuccess { get; } = "Success.";
		public static string DefaultNoPath { get; } = "No path.";


		public Settings Settings { get; }

		public Compiler(Settings settings) {
			Settings = settings;
		}

		#region Data
		#endregion

		public bool Compile(out string message) {

			// Find & read all script files.
			foreach(string file in Settings.Datapack.GetScriptFiles()) {

				ICharStream stream = CharStreams.fromString(File.ReadAllText(file));
				ITokenSource lexer = new MCSharpLexer(stream);
				ITokenStream tokens = new CommonTokenStream(lexer);
				MCSharpParser parser = new MCSharpParser(tokens);
				parser.BuildParseTree = true;
				IParseTree tree = parser.script();
				TestKeyPrinter printer = new TestKeyPrinter();
				ParseTreeWalker.Default.Walk(printer, tree);

			}

			message = DefaultSuccess;
			return true;

		}

		public class TestKeyPrinter : MCSharpBaseListener {

			public override void ExitScript([NotNull] MCSharpParser.ScriptContext context) {
				base.ExitScript(context);
				Program.PrintText($"Script Token: {context.GetText()}");
			}

		}


	}

}
