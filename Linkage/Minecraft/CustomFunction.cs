﻿using MCSharp.Compilation;
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

		public delegate ResultInfo InvokeDelegate(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result);

		private InvokeDelegate Delegate { get; }

		/// <summary>
		/// 
		/// </summary>
		public IReadOnlyList<PredefinedGenericParameter> GenericParameters { get; }
		/// <inheritdoc/>
		IReadOnlyList<IGenericParameter> IFunction.GenericParameters => GenericParameters;

		/// <summary>
		/// 
		/// </summary>
		public IReadOnlyList<PredefinedMethodParameter> MethodParameters { get; }
		/// <inheritdoc/>
		IReadOnlyList<IMethodParameter> IFunction.MethodParameters => MethodParameters;

		/// <inheritdoc/>
		public string ReturnTypeIdentifier { get; }


		public CustomFunction(string returnTypeIdentifier, PredefinedGenericParameter[] genericParameters, PredefinedMethodParameter[] methodParameters, InvokeDelegate @delegate) {
			Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
			GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
			MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
			ReturnTypeIdentifier = returnTypeIdentifier ?? throw new ArgumentNullException(nameof(returnTypeIdentifier));
		}


		public ResultInfo Invoke(Compiler.CompileArguments compile, IType[] generic, IInstance[] arguments, out IInstance result) {
			return Delegate.Invoke(compile, generic, arguments, out result);
		}

		public void Dispose() {
			// Nothing to dispose of.
		}

	}

}
