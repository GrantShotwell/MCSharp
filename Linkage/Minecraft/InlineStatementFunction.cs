using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using System;
using System.Collections.Generic;

namespace MCSharp.Linkage.Minecraft;

public class InlineStatementFunction : IStatementFunction {

	/// <inheritdoc/>
	public IReadOnlyList<IGenericParameter> GenericParameters { get; }

	/// <inheritdoc/>
	public IReadOnlyList<IMethodParameter> MethodParameters { get; }

	/// <inheritdoc/>
	public IStatement[] Statements { get; }

	/// <inheritdoc/>
	public string ReturnTypeIdentifier { get; }

	/// <inheritdoc/>
	public string ThisTypeIdentifier { get; }

	/// <inheritdoc/>
	public IInstance ReturnInstance { get; private set; }

	/// <inheritdoc/>
	public IInstance ThisInstance { get; private set; }


	/// <summary>
	/// 
	/// </summary>
	/// <param name="genericParameters"></param>
	/// <param name="methodParameters"></param>
	/// <param name="statements"></param>
	/// <param name="returnType"></param>
	/// <param name="thisType">The local identifier of the 'this' type for this <see cref="IFunction"/>. Pass <see langword="null"/> to define this <see cref="IFunction"/> as static.</param>
	/// <exception cref="ArgumentNullException"></exception>
	public InlineStatementFunction(IGenericParameter[] genericParameters, IMethodParameter[] methodParameters, IStatement[] statements, string returnType, string thisType) {

		GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
		MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
		Statements = statements ?? throw new ArgumentNullException(nameof(statements));
		ReturnTypeIdentifier = returnType ?? throw new ArgumentNullException(nameof(returnType));
		ThisTypeIdentifier = thisType;

	}


	/// <inheritdoc/>
	public ResultInfo InvokeStatic(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) {

		location.Compiler.CompileStatements(location.Function, new Scope(null, location.Scope), Statements);
		result = null;
		return ResultInfo.DefaultSuccess;

	}

	/// <inheritdoc/>
	public ResultInfo InvokeNonStatic(Compiler.CompileArguments location, IInstance context, IType[] generic, IInstance[] arguments, out IInstance result) {

		throw new NotImplementedException();

	}

	public void Dispose() {

		GC.SuppressFinalize(this);

	}

}
