using Antlr4.Runtime.Tree;
using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Text;

using static MCSharpParser;

namespace MCSharp.Linkage.Minecraft {

	public class Function {

		public FunctionWriter Writer { get; }

		public GenericParameter[] GenericParameters { get; }
		public MethodParameter[] MethodParameters { get; }
		public StatementContext[] Statements { get; }
		public ITerminalNode ReturnTypeIdentifier { get; }

		public Function(FunctionWriter writer, GenericParameter[] genericParameters, MethodParameter[] methodParameters, StatementContext[] statements, ITerminalNode returnType) {

			Writer = writer ?? throw new ArgumentNullException(nameof(writer));

			GenericParameters = genericParameters;
			MethodParameters = methodParameters;
			Statements = statements;
			ReturnTypeIdentifier = returnType;

		}

		public Function CreateChildFunction(StatementContext[] statements, Settings settings, string name) {

			FunctionWriter writer = new FunctionWriter(Writer.VirtualMachine, settings, $"{Writer.LocalFilePath}\\{Writer.Name}", name);
			Function sub = new Function(writer, GenericParameters, MethodParameters, statements, ReturnTypeIdentifier);
			return sub;

		}

	}

}
