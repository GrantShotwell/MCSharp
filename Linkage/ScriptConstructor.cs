using MCSharp.Compilation;
using MCSharp.Compilation.Linkage;
using MCSharp.Linkage.Minecraft;
using System;
using ConstructorDefinitionContext = MCSharpParser.Constructor_definitionContext;

namespace MCSharp.Linkage {

	public class ScriptConstructor {

		public ScriptClass Declarer { get; }

		public Modifier Modifiers { get; }
		public Function Invoker { get; }

		public ScriptConstructor(ScriptClass declarer, ConstructorDefinitionContext context, Settings settings, VirtualMachine virtualMachine) {

			Declarer = declarer;

			FunctionWriter writer = new FunctionWriter(virtualMachine, settings, Declarer.Identifier.GetText(), $"{Declarer.Identifier.GetText()}_{Declarer.i_constructor++}");
			MethodParameter[] parameters = MethodParameter.CreateArrayFromArray(context.method_parameters().method_parameter_list()?.method_parameter());
			
			// Get the statement list for the function.
			MCSharpParser.StatementContext[] statements;
			if(context.LAMBDA() != null) {
				// Return expression.
				throw new NotImplementedException("Lambda definitions for methods has not been implemented.");
			} else {
				// Code block.
				statements = context.code_block().statement();
			}

			Invoker = new Function(writer, new GenericParameter[] { }, parameters, statements, Declarer.Identifier);

		}


	}

}
