using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Predefined;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Minecraft {

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
		public IInstance ReturnInstance { get; private set; }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="returnTypeIdentifier"></param>
		/// <param name="genericParameters"></param>
		/// <param name="methodParameters"></param>
		/// <param name="delegate"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public CustomFunction(string returnTypeIdentifier, PredefinedGenericParameter[] genericParameters, PredefinedMethodParameter[] methodParameters, InvokeDelegate @delegate) {
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
			MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
			ReturnTypeIdentifier = returnTypeIdentifier ?? throw new ArgumentNullException(nameof(returnTypeIdentifier));
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="compile"></param>
		/// <param name="generic"></param>
		/// <param name="arguments"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		public ResultInfo Invoke(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) {

			// Cast the arguments to the correct type.
			for (int i = 0; i < arguments.Length; i++) {
				IInstance argument = arguments[i];
				IType parameterType = compile.Compiler.DefinedTypes[MethodParameters[i].TypeIdentifier];
				if(argument.Type == parameterType) continue;
				argument.Type.Conversions[parameterType].Function.Invoke(compile, Array.Empty<IType>(), new IInstance[] { argument }, out arguments[i]);
			}

			// Invoke the function.
			return Delegate.Invoke(compile, generic, arguments, out result);

		}

		/// <inheritdoc/>
		public void Dispose() {
			// Nothing to dispose of.
		}

	}

}
