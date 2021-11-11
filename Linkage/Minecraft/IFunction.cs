using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Minecraft {

	/// <summary>
	/// Represents in-game code with arguments and a returnable value.
	/// </summary>
	/// <seealso cref="IStatementFunction"/>
	/// <seealso cref="CustomFunction"/>
	public interface IFunction : IDisposable {

		/// <summary>
		/// The <see cref="IGenericParameter"/>s for this <see cref="IFunction"/>.
		/// </summary>
		public IReadOnlyList<IGenericParameter> GenericParameters { get; }

		/// <summary>
		/// The <see cref="IMethodParameter"/>s for this <see cref="IFunction"/>.
		/// </summary>
		public IReadOnlyList<IMethodParameter> MethodParameters { get; }

		/// <summary>
		/// The local identifier for the return type for this <see cref="IFunction"/>.
		/// </summary>
		public string ReturnTypeIdentifier { get; }

		/// <summary>
		/// The <see cref="IInstance"/> assigned to when returning.
		/// </summary>
		public IInstance ReturnInstance { get; }

		/// <summary>
		/// Invoke the <see cref="IFunction"/> with the given arguments.
		/// </summary>
		/// <param name="location">The location of the call.</param>
		/// <param name="generic">The generic arguments to invoke with.</param>
		/// <param name="arguments">The instance arguments to invoke with.</param>
		/// <param name="result">The result of the call.</param>
		/// <returns>The result of the compilation.</returns>
		public ResultInfo Invoke(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result);

	}

}
