using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Minecraft {

	public class InlineStatementFunction : IStatementFunction {

		public IReadOnlyList<IGenericParameter> GenericParameters { get; }
		public IReadOnlyList<IMethodParameter> MethodParameters { get; }
		public IStatement[] Statements { get; }
		public string ReturnTypeIdentifier { get; }

		public InlineStatementFunction(IGenericParameter[] genericParameters, IMethodParameter[] methodParameters, IStatement[] statements, string returnTypeIdentifier) {

			GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
			MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
			Statements = statements ?? throw new ArgumentNullException(nameof(statements));
			ReturnTypeIdentifier = returnTypeIdentifier ?? throw new ArgumentNullException(nameof(returnTypeIdentifier));

		}

		public void Dispose() { /* Nothing to dispose of */ }

	}

}
