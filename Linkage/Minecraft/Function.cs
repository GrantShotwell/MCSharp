using Antlr4.Runtime.Tree;
using MCSharp.Compilation;
using MCSharp.Linkage.Script;
using System;

namespace MCSharp.Linkage.Minecraft {

	public class Function : IFunction {

		public FunctionWriter Writer { get; }

		public IGenericParameter[] GenericParameters { get; }
		public IMethodParameter[] MethodParameters { get; }
		public IStatement[] Statements { get; }
		public string ReturnTypeIdentifier { get; }

		public Function(FunctionWriter writer, IGenericParameter[] genericParameters, IMethodParameter[] methodParameters, IStatement[] statements, string returnType) {

			Writer = writer ?? throw new ArgumentNullException(nameof(writer));

			GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
			MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
			Statements = statements ?? throw new ArgumentNullException(nameof(statements));
			ReturnTypeIdentifier = returnType ?? throw new ArgumentNullException(nameof(returnType));

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
