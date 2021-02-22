using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using MCSharp.Linkage.Script;
using MCSharp.Linkage.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ConstructorDefinitionContext = MCSharpParser.Constructor_definitionContext;
using MemberDefinitionContext = MCSharpParser.Member_definitionContext;
using TypeDefinitionContext = MCSharpParser.Type_definitionContext;
using System.Threading;

namespace MCSharp.Compilation {

	public class Compiler {

		public Settings Settings { get; }
		public VirtualMachine VirtualMachine { get; }
		public ICollection<Assembly> Assemblies { get; }

		public Compiler(Settings settings, ICollection<Assembly> assemblies) {
			Settings = settings;
			VirtualMachine = new VirtualMachine();
			Assemblies = assemblies;
		}

		#region Data

		public ICollection<ScriptType> DefinedTypes { get; } = new LinkedList<ScriptType>();
		public ICollection<LinkerExtension> LinkerExtensions { get; } = new LinkedList<LinkerExtension>();

		#endregion

		public bool Compile(out string message) {

			// TODO: Multithread first pass walk and adding redefined types (?).

			// Find, parse, and first pass walk (types, members) all script files.
			void FirstPassWalk() {
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
			}

			// Add linker extensions.
			void AddLinkerExtensions() {
				foreach(Assembly assembly in Assemblies) {
					foreach(Type type in assembly.ExportedTypes) {

						if(!type.IsAbstract && typeof(LinkerExtension).IsAssignableFrom(type)) {

							ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(Compiler) });
							LinkerExtension extension = constructor.Invoke(new object[] { this }) as LinkerExtension;
							LinkerExtensions.Add(extension);

						}

					}
				}
			}

			// Apply linker extensions.
			void ApplyLinkerExtensions() {
				foreach(LinkerExtension extension in LinkerExtensions) {

					extension.CreatePredefinedTypes();

				}
			}

			{

				Thread thread1 = new Thread(new ThreadStart(() => {
					FirstPassWalk();
				}));
				Thread thread2 = new Thread(new ThreadStart(() => {
					AddLinkerExtensions();
					ApplyLinkerExtensions();
				}));

				thread1.Start();
				thread2.Start();

				thread1.Join();
				thread2.Join();

			}

			// Compile every class/struct member.
			foreach(ScriptType type in DefinedTypes) {

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
				Compiler.DefinedTypes.Add(scriptType);
			}

		}

	}

}
