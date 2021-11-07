using MCSharp.Compilation;
using MCSharp.Linkage.Minecraft;
using System;
using ConstructorDefinitionContext = MCSharpParser.Constructor_definitionContext;

namespace MCSharp.Linkage.Script {

	/// <summary>
	/// Represents a constructor defined by script.
	/// </summary>
	public class ScriptConstructor : IConstructor, IScopeHolder {

		/// <summary>
		/// The type, defined by script, that has defined this constructor.
		/// </summary>
		public ScriptType Declarer { get; }
		IType IConstructor.Declarer => Declarer;

		/// <inheritdoc/>
		public Modifier Modifiers { get; }

		/// <summary>
		/// The mcfunction file that will contain the commands to execute this constructor.
		/// </summary>
		public StandaloneStatementFunction Invoker { get; }
		/// <inheritdoc/>
		IFunction IConstructor.Invoker => Invoker;

		/// <inheritdoc/>
		public Scope Scope { get; set; }


		/// <summary>
		/// Creates a new constructor that has been defined by script.
		/// </summary>
		/// <param name="declarer">The type that has defined this constructor.</param>
		/// <param name="context">The parser context used to create the constructor.</param>
		/// <param name="settings">Value passed to create <see cref="StandaloneStatementFunction"/>(s).</param>
		/// <param name="virtualMachine">Value passed to create <see cref="StandaloneStatementFunction"/>(s).</param>
		public ScriptConstructor(ScriptType declarer, ConstructorDefinitionContext context, Settings settings, VirtualMachine virtualMachine) {

			Declarer = declarer;

			var writer = new FunctionWriter(virtualMachine, settings, Declarer.Identifier.GetText(), $"{Declarer.Identifier.GetText()}_{Declarer.i_constructor++}");
			ScriptMethodParameter[] parameters = ScriptMethodParameter.CreateArrayFromArray(context.method_parameters().method_parameter_list()?.method_parameter());

			// Get the statement list for the function.
			ScriptStatement[] statements;
			if(context.LAMBDA() != null) {
				// Return expression.
				throw new NotImplementedException("Lambda definitions for constructors have not been implemented.");
			} else {
				// Code block.
				statements = ScriptStatement.CreateArrayFromArray(context.code_block().statement());
			}

			Scope = new Scope(null, declarer.Scope, this);
			Invoker = new StandaloneStatementFunction(writer, Scope, new ScriptGenericParameter[] { }, parameters, statements, Declarer.Identifier.GetText());

		}

		public void Dispose() {
			Invoker.Dispose();
		}

	}

}
