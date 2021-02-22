using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using MCSharp.Linkage.Script;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConstructorDefinitionContext = MCSharpParser.Constructor_definitionContext;
using MemberDefinitionContext = MCSharpParser.Member_definitionContext;
using TypeDefinitionContext = MCSharpParser.Type_definitionContext;

namespace MCSharp.Compilation {

	public class Compiler {

		public Settings Settings { get; }
		public VirtualMachine VirtualMachine { get; }

		public Compiler(Settings settings) {
			Settings = settings;
			VirtualMachine = new VirtualMachine();
		}

		#region Data

		public ICollection<ScriptType> ScriptTypes { get; } = new List<ScriptType>();

		#endregion

		public bool Compile(out string message) {

			// TODO: Multithread first pass walk and adding redefined types (?).

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

			// Add predefined types.
			// todo

			// Compile every class/struct member.
			foreach(ScriptType type in ScriptTypes) {

				foreach(ScriptMember member in type.Members) {



				}

			}

			message = "Finished.";
			return true;

		}

		public class ScriptClassWalker : MCSharpBaseListener {

			Compiler Compiler { get; }

			private TypeDefinitionContext CurrentTypeContext { get; set; }
			private ICollection<MemberDefinitionContext> CurrentMemberContexts { get; set; } = new LinkedList<MemberDefinitionContext>();
			private ICollection<ConstructorDefinitionContext> CurrentConstructorContexts { get; set; } = new LinkedList<ConstructorDefinitionContext>();

			public ScriptClassWalker(Compiler compiler) {
				Compiler = compiler;
			}

			public override void EnterType_definition([NotNull] TypeDefinitionContext context) {
				CurrentMemberContexts.Clear();
				CurrentTypeContext = context;
			}

			public override void EnterMember_definition([NotNull] MemberDefinitionContext context) {
				CurrentMemberContexts.Add(context);
			}

			public override void EnterConstructor_definition([NotNull] ConstructorDefinitionContext context) {
				CurrentConstructorContexts.Add(context);
			}

			public override void ExitType_definition([NotNull] TypeDefinitionContext context) {
				if(context != CurrentTypeContext) throw new Exception($"Subtypes are currently not supported by {nameof(ScriptClassWalker)}.");
				var scriptType = new ScriptType(CurrentTypeContext, CurrentMemberContexts.ToArray(), CurrentConstructorContexts.ToArray(), Compiler.Settings, Compiler.VirtualMachine);
				Compiler.ScriptTypes.Add(scriptType);
			}

		}

	}

}
