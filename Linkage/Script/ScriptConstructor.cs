using MCSharp.Compilation;
using MCSharp.Linkage.Minecraft;
using System;
using ConstructorDefinitionContext = MCSharpParser.Constructor_definitionContext;

namespace MCSharp.Linkage.Script {

	/// <summary>
	/// Represents a constructor defined by script.
	/// </summary>
	public class ScriptConstructor : IConstructor {

		/// <summary>
		/// The type, defined by script, that has defined this constructor.
		/// </summary>
		public ScriptType Declarer { get; }
		IType IConstructor.Declarer => Declarer;

		/// <inheritdoc/>
		public Modifier Modifiers { get; }
		/// <inheritdoc/>
		public Function Invoker { get; }


		/// <summary>
		/// Creates a new constructor that has been defined by script.
		/// </summary>
		/// <param name="declarer">The type that has defined this constructor.</param>
		/// <param name="context">The parser context used to create the constructor.</param>
		/// <param name="settings">Value passed to create <see cref="Function"/>(s).</param>
		/// <param name="virtualMachine">Value passed to create <see cref="Function"/>(s).</param>
		public ScriptConstructor(ScriptType declarer, ConstructorDefinitionContext context, Settings settings, VirtualMachine virtualMachine) {

			Declarer = declarer;

			var writer = new FunctionWriter(virtualMachine, settings, Declarer.Identifier.GetText(), $"{Declarer.Identifier.GetText()}_{Declarer.i_constructor++}");
			MethodParameter[] parameters = MethodParameter.CreateArrayFromArray(context.method_parameters().method_parameter_list()?.method_parameter());

			// Get the statement list for the function.
			ScriptStatement[] statements;
			if(context.LAMBDA() != null) {
				// Return expression.
				throw new NotImplementedException("Lambda definitions for methods has not been implemented.");
			} else {
				// Code block.
				statements = ScriptStatement.CreateArrayFromArray(context.code_block().statement());
			}

			Invoker = new Function(writer, new GenericParameter[] { }, parameters, statements, Declarer.Identifier);

		}

		public void Dispose() {
			Invoker.Dispose();
		}

	}

}
