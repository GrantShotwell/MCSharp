using MCSharp.Compilation;
using MCSharp.Linkage.Minecraft;
using System;
using ConstructorDefinitionContext = MCSharpParser.Constructor_definitionContext;

namespace MCSharp.Linkage.Script {

	public class ScriptConstructor : IConstructor {

		public ScriptType Declarer { get; }
		IType IConstructor.Declarer => Declarer;

		public Modifier Modifiers { get; }
		public Function Invoker { get; }

		public ScriptConstructor(ScriptType declarer, ConstructorDefinitionContext context, Settings settings, VirtualMachine virtualMachine) {

			Declarer = declarer;

			var writer = new FunctionWriter(virtualMachine, settings, Declarer.Identifier.GetText(), $"{Declarer.Identifier.GetText()}_{Declarer.i_constructor++}");
			MethodParameter[] parameters = MethodParameter.CreateArrayFromArray(context.method_parameters().method_parameter_list()?.method_parameter());

			// Get the statement list for the function.
			ScriptStatement[] statements;
			if(context.LAMBDA() != null)                // Return expression.
				throw new NotImplementedException("Lambda definitions for methods has not been implemented.");
			else {
				// Code block.
				statements = ScriptStatement.CreateArrayFromArray(context.code_block().statement());
			}

			Invoker = new Function(writer, new GenericParameter[] { }, parameters, statements, Declarer.Identifier);

		}


	}

}
