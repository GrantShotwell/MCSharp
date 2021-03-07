using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCSharp.Linkage.Minecraft {

	public class StandaloneStatementFunction : IStatementFunction {

		/// <summary>
		/// The <see cref="FunctionWriter"/> this <see cref="IFunction"/> writes to.
		/// </summary>
		public FunctionWriter Writer { get; }

		/// <inheritdoc/>
		public IReadOnlyList<IGenericParameter> GenericParameters { get; }

		/// <inheritdoc/>
		public IReadOnlyList<IMethodParameter> MethodParameters { get; }

		/// <inheritdoc/>
		public IStatement[] Statements { get; }

		/// <inheritdoc/>
		public string ReturnTypeIdentifier { get; }


		/// <summary>
		/// Creates a new <see cref="StandaloneStatementFunction"/>.
		/// </summary>
		/// <param name="writer">The <see cref="FunctionWriter"/> this <see cref="IFunction"/> will write to.</param>
		/// <param name="genericParameters">The <see cref="IGenericParameter"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="methodParameters">The <see cref="IMethodParameter"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="statements">The <see cref="IStatement"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="returnType">The local identifier of the return type for this <see cref="IFunction"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when any argument is <see langword="null"/></exception>
		public StandaloneStatementFunction(FunctionWriter writer, IGenericParameter[] genericParameters, IMethodParameter[] methodParameters, IStatement[] statements, string returnType) {

			Writer = writer ?? throw new ArgumentNullException(nameof(writer));

			GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
			MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
			Statements = statements ?? throw new ArgumentNullException(nameof(statements));
			ReturnTypeIdentifier = returnType ?? throw new ArgumentNullException(nameof(returnType));

		}


		/// <summary>
		/// Creates a new <see cref="StandaloneStatementFunction"/> located in a subdirectory of this <see cref="StandaloneStatementFunction"/>.
		/// </summary>
		/// <param name="statements">The <see cref="Statements"/> of the new <see cref="StandaloneStatementFunction"/>.</param>
		/// <param name="settings">The <see cref="Settings"/> used to create the <see cref="FunctionWriter"/>.</param>
		/// <param name="name">The <see cref="FunctionWriter.Name"/> of the child function. Can be <see langword="null"/> to use the next integer value starting from zero.</param>
		/// <returns>Returns the created <see cref="StandaloneStatementFunction"/>.</returns>
		/// <exception cref="ArgumentNullException">Thrown when <paramref name="statements"/> is <see langword="null"/>.</exception>
		public StandaloneStatementFunction CreateChildFunction(IStatement[] statements, Settings settings, string name = null) {

			#region Argument Checks
			if(statements is null)
				throw new ArgumentNullException(nameof(statements));
			#endregion

			if(name == null) name = GenerateNewChildName();

			FunctionWriter writer = new FunctionWriter(Writer.VirtualMachine, settings, $"{Writer.LocalFilePath}\\{Writer.Name}", name);
			StandaloneStatementFunction sub = new StandaloneStatementFunction(writer, GenericParameters.ToArray(), MethodParameters.ToArray(), statements, ReturnTypeIdentifier);
			return sub;

		}

		private static int childcount = 0;
		private string GenerateNewChildName() {
			return $"{Writer.Name}\\{childcount++}";
		}

		public void Dispose() {
			Writer.Dispose();
		}

	}

}
