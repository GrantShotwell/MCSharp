using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Predefined;
using System;
using System.Collections.Generic;

namespace MCSharp.Linkage.Minecraft;

/// <summary>
/// Represents a <see cref="IFunction"/> that is defined by running some compiler extension code to create in-game code.
/// </summary>
public class CustomFunction : IFunction {

	/// <summary>
	/// 
	/// </summary>
	/// <param name="compile"></param>
	/// <param name="generic"></param>
	/// <param name="arguments"></param>
	/// <param name="result"></param>
	/// <returns></returns>
	public delegate ResultInfo InvokeDelegate(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result);

	private InvokeDelegate Delegate { get; }

	/// <inheritdoc cref="IFunction.GenericParameters"/>
	public IReadOnlyList<PredefinedGenericParameter> GenericParameters { get; }
	/// <inheritdoc/>
	IReadOnlyList<IGenericParameter> IFunction.GenericParameters => GenericParameters;

	/// <inheritdoc cref="IFunction.MethodParameters"/>
	public IReadOnlyList<PredefinedMethodParameter> MethodParameters { get; }
	/// <inheritdoc/>
	IReadOnlyList<IMethodParameter> IFunction.MethodParameters => MethodParameters;

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
	/// <param name="returnType"></param>
	/// <param name="thisType">The local identifier of the 'this' type for this <see cref="IFunction"/>. Pass <see langword="null"/> to define this <see cref="IFunction"/> as static.</param>
	/// <param name="genericParameters"></param>
	/// <param name="methodParameters"></param>
	/// <param name="delegate"></param>
	/// <exception cref="ArgumentNullException"></exception>
	public CustomFunction(string returnType, string thisType, PredefinedGenericParameter[] genericParameters, PredefinedMethodParameter[] methodParameters, InvokeDelegate @delegate) {

		Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
		GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
		MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
		ReturnTypeIdentifier = returnType ?? throw new ArgumentNullException(nameof(returnType));
		ThisTypeIdentifier = thisType;

	}


	/// <summary>
	/// 
	/// </summary>
	/// <param name="compile"></param>
	/// <param name="generics"></param>
	/// <param name="arguments"></param>
	/// <param name="result"></param>
	/// <returns></returns>
	public ResultInfo InvokeStatic(Compiler.CompileArguments compile, IType[] generics, IInstance[] arguments, out IInstance result) {

		#region Argument Checks
		if(generics is null) throw new ArgumentNullException(nameof(generics));
		foreach(IInstance generic in generics)
			if(generic is null) throw new ArgumentNullException(nameof(generics), "Cannot contain any null values.");
		if(arguments is null) throw new ArgumentNullException(nameof(arguments));
		foreach(IInstance argument in arguments)
			if(argument is null) throw new ArgumentNullException(nameof(arguments), "Cannot contain any null values.");
		#endregion

		// Cast the arguments to the correct type.
		for(int i = 0; i < arguments.Length; i++) {
			IInstance argument = arguments[i];
			IType parameterType = compile.Compiler.DefinedTypes[MethodParameters[i].TypeIdentifier];
			if(argument.Type == parameterType) continue;
			argument.Type.Conversions[parameterType].Function.InvokeStatic(compile, Array.Empty<IType>(), new IInstance[] { argument }, out arguments[i]);
		}

		// Invoke the function.
		return Delegate.Invoke(compile, generics, arguments, out result);

	}

	/// <inheritdoc/>
	public ResultInfo InvokeNonStatic(Compiler.CompileArguments location, IInstance context, IType[] generic, IInstance[] arguments, out IInstance result) {

		throw new NotImplementedException();

	}

	/// <inheritdoc/>
	public void Dispose() {

		GC.SuppressFinalize(this);

	}

}
