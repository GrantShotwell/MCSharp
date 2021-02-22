using Antlr4.Runtime.Tree;
using MCSharp.Compilation;
using System;

namespace MCSharp.Linkage.Minecraft {

	public class Function : IDisposable {

		public FunctionWriter Writer { get; }

		public GenericParameter[] GenericParameters { get; }
		public MethodParameter[] MethodParameters { get; }
		public IStatement[] Statements { get; }
		public ITerminalNode ReturnTypeIdentifier { get; }

		public Function(FunctionWriter writer, GenericParameter[] genericParameters, MethodParameter[] methodParameters, IStatement[] statements, ITerminalNode returnType) {

			Writer = writer ?? throw new ArgumentNullException(nameof(writer));

			GenericParameters = genericParameters;
			MethodParameters = methodParameters;
			Statements = statements;
			ReturnTypeIdentifier = returnType;

		}

		public Function CreateChildFunction(IStatement[] statements, Settings settings, string name) {

			FunctionWriter writer = new FunctionWriter(Writer.VirtualMachine, settings, $"{Writer.LocalFilePath}\\{Writer.Name}", name);
			Function sub = new Function(writer, GenericParameters, MethodParameters, statements, ReturnTypeIdentifier);
			return sub;

		}

		public void Dispose() {
			Writer.Dispose();
		}

	}

}
