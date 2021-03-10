using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Minecraft {

	public class InlineStatementFunction : IStatementFunction {

		/// <inheritdoc/>
		public IReadOnlyList<IGenericParameter> GenericParameters { get; }

		/// <inheritdoc/>
		public IReadOnlyList<IMethodParameter> MethodParameters { get; }

		/// <inheritdoc/>
		public IStatement[] Statements { get; }

		/// <inheritdoc/>
		public string ReturnTypeIdentifier { get; }


		public InlineStatementFunction(IGenericParameter[] genericParameters, IMethodParameter[] methodParameters, IStatement[] statements, string returnTypeIdentifier) {

			GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
			MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
			Statements = statements ?? throw new ArgumentNullException(nameof(statements));
			ReturnTypeIdentifier = returnTypeIdentifier ?? throw new ArgumentNullException(nameof(returnTypeIdentifier));

		}


		/// <inheritdoc/>
		public ResultInfo Invoke(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) {

			location.Compiler.CompileStatements(location.Function, new Scope(null, location.Scope, null), Statements);
			result = null;
			return ResultInfo.DefaultSuccess;

		}

		public void Dispose() { /* Nothing to dispose of */ }
	}

}
