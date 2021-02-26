using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Minecraft {

	public class InlineFunction : IFunction {

		public IGenericParameter[] GenericParameters { get; }
		public IMethodParameter[] MethodParameters { get; }
		public IStatement[] Statements { get; }
		public string ReturnTypeIdentifier { get; }

		public InlineFunction(IGenericParameter[] genericParameters, IMethodParameter[] methodParameters, IStatement[] statements, string returnTypeIdentifier) {

			GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
			MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
			Statements = statements ?? throw new ArgumentNullException(nameof(statements));
			ReturnTypeIdentifier = returnTypeIdentifier ?? throw new ArgumentNullException(nameof(returnTypeIdentifier));

		}

		public void Dispose() { /* Nothing to dispose of */ }

	}

}
