using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
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

		/// <summary>
		/// 
		/// </summary>
		private IInstance ReturnInstance { get; set; }

		/// <inheritdoc/>
		public string ReturnTypeIdentifier { get; }

		private ICollection<StandaloneStatementFunction> ChildFunctions { get; } = new LinkedList<StandaloneStatementFunction>();

		public bool Compiled { get; set; }

		public Scope Scope { get; }


		/// <summary>
		/// Creates a new <see cref="StandaloneStatementFunction"/>.
		/// </summary>
		/// <param name="writer">The <see cref="FunctionWriter"/> this <see cref="IFunction"/> will write to.</param>
		/// <param name="scope">The <see cref="Scope"/> used when writing the function.</param>
		/// <param name="genericParameters">The <see cref="IGenericParameter"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="methodParameters">The <see cref="IMethodParameter"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="statements">The <see cref="IStatement"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="returnType">The local identifier of the return type for this <see cref="IFunction"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when any argument is <see langword="null"/></exception>
		public StandaloneStatementFunction(FunctionWriter writer, Scope scope, IGenericParameter[] genericParameters, IMethodParameter[] methodParameters, IStatement[] statements, string returnType) {

			Writer = writer ?? throw new ArgumentNullException(nameof(writer));
			Scope = scope ?? throw new ArgumentNullException(nameof(scope));
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

			if(name == null) name = NextChildName();
			FunctionWriter writer = new FunctionWriter(Writer.VirtualMachine, settings, $"{Writer.LocalDirectory}\\{Writer.Name}", name);
			StandaloneStatementFunction child = new StandaloneStatementFunction(writer, new Scope(null, Scope, null), GenericParameters.ToArray(), MethodParameters.ToArray(), statements, ReturnTypeIdentifier);
			ChildFunctions.Add(child);
			return child;

		}


		/// <inheritdoc/>
		public ResultInfo Invoke(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) {

			if(!Compiled) {

				Compiler.CompileArguments location1 = new Compiler.CompileArguments(location.Compiler, this, Scope, false);
				ReturnInstance = location.Compiler.DefinedTypes[ReturnTypeIdentifier].InitializeInstance(location1, null);

				Compiled = true;
				location.Compiler.CompileStatements(this, Scope, Statements);

			}

			result = ReturnInstance;

			location.Writer.WriteCommand($"function {Writer.GamePath}");
			return ResultInfo.DefaultSuccess;

		}

		private string NextChildName() {
			return ChildFunctions.Count.ToString();
		}

		public void Dispose() {
			Writer.Dispose();
			foreach(IFunction function in ChildFunctions) {
				function.Dispose();
			}
		}

	}

}
