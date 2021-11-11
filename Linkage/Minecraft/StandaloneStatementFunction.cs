using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Extensions;
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
		public IInstance ReturnInstance { get; private set; }

		/// <inheritdoc/>
		public string ReturnTypeIdentifier { get; }
		
		private ICollection<StandaloneStatementFunction> ChildFunctions { get; } = new LinkedList<StandaloneStatementFunction>();

		public bool Compiled { get; set; }

		private IScopeHolder ScopeHolder { get; }
		private Scope scope;

		/// <summary>
		/// The <see cref="Compilation.Scope"/> this <see cref="IFunction"/> is in.
		/// </summary>
		public Scope Scope => ScopeHolder?.Scope ?? scope;


		/// <summary>
		/// Creates a new <see cref="StandaloneStatementFunction"/> with a scope of a <see cref="IScopeHolder"/>.
		/// </summary>
		/// <param name="writer">The <see cref="FunctionWriter"/> this <see cref="IFunction"/> will write to.</param>
		/// <param name="holder">The <see cref="IScopeHolder"/> this <see cref="IFunction"/> belongs to.</param>
		/// <param name="parent">The <see cref="Scope"/> used when writing the function.</param>
		/// <param name="genericParameters">The <see cref="IGenericParameter"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="methodParameters">The <see cref="IMethodParameter"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="statements">The <see cref="IStatement"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="returnType">The local identifier of the return type for this <see cref="IFunction"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when any argument is <see langword="null"/></exception>
		public StandaloneStatementFunction(FunctionWriter writer, IScopeHolder holder, IGenericParameter[] genericParameters, IMethodParameter[] methodParameters, IStatement[] statements, string returnType) {

			Writer = writer ?? throw new ArgumentNullException(nameof(writer));
			GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
			MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
			Statements = statements ?? throw new ArgumentNullException(nameof(statements));
			ReturnTypeIdentifier = returnType ?? throw new ArgumentNullException(nameof(returnType));
			
			ScopeHolder = holder ?? throw new ArgumentNullException(nameof(holder));

		}
		
		/// <summary>
		/// Creates a new <see cref="StandaloneStatementFunction"/> with a custom <see cref="Compilation.Scope"/>.
		/// </summary>
		/// <param name="writer">The <see cref="FunctionWriter"/> this <see cref="IFunction"/> will write to.</param>
		/// <param name="scope">The <see cref="Compilation.Scope"/> this <see cref="IFunction"/> belongs to.</param>
		/// <param name="parent">The <see cref="Scope"/> used when writing the function.</param>
		/// <param name="genericParameters">The <see cref="IGenericParameter"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="methodParameters">The <see cref="IMethodParameter"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="statements">The <see cref="IStatement"/>s of this <see cref="IFunction"/>.</param>
		/// <param name="returnType">The local identifier of the return type for this <see cref="IFunction"/>.</param>
		/// <exception cref="ArgumentNullException">Thrown when any argument is <see langword="null"/></exception>
		public StandaloneStatementFunction(FunctionWriter writer, Scope scope, IGenericParameter[] genericParameters, IMethodParameter[] methodParameters, IStatement[] statements, string returnType) {

			Writer = writer ?? throw new ArgumentNullException(nameof(writer));
			GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
			MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
			Statements = statements ?? throw new ArgumentNullException(nameof(statements));
			ReturnTypeIdentifier = returnType ?? throw new ArgumentNullException(nameof(returnType));
			
			this.scope = scope ?? throw new ArgumentNullException(nameof(scope));

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
			StandaloneStatementFunction child = new StandaloneStatementFunction(writer, new Scope(null, Scope), GenericParameters.ToArray(), MethodParameters.ToArray(), statements, ReturnTypeIdentifier);
			ChildFunctions.Add(child);
			return child;

		}


		/// <inheritdoc/>
		public ResultInfo Invoke(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) {

			if(!Compiled) {

				// Create the location for the function.
				Compiler.CompileArguments location1 = new Compiler.CompileArguments(location.Compiler, this, Scope, false);
				// Create the returned instance inside the function.
				ReturnInstance = location1.Compiler.DefinedTypes[ReturnTypeIdentifier].InitializeInstance(location1, null);

				// If the return instace is an object, create the entity.
				if(ReturnInstance is ObjectInstance objectInstance) {

					// Get the function writer.
					FunctionWriter writer = location1.Writer;

					writer.WriteComments(
						$"Construct a new object.",
						indentBefore: true);

					// Create the entity. Use a tag to identify the entity.
					const string tag = "mcs.new_object";
					writer.WriteCommand($"execute at @p run summon minecraft:area_effect_cloud ~ ~ ~ {{Tags:[{tag}]}}",
						$"Create a new entity for the new object of class type '{ReturnTypeIdentifier}'.");

					// Assign the entity a random address.
					PrimitiveInstance.IntegerInstance random = VirtualMachine.GenerateRandomIntegerInstance(location1);
					writer.WriteCommand($"scoreboard players operation @e[tag={tag}] {ObjectInstance.AddressObjective.Name} = {MCSharpLinkerExtension.StorageSelector} {random.Objective.Name}",
						$"Assign a random address to the new entity.");

					// Remove the temporary tag from the entity.
					writer.WriteCommand($"tag @e[tag={tag}] remove {tag}",
						$"Remove the temporary tag from the new entity.");

					// If the object is a class type, compile initialization expression for each field.
					if(objectInstance is ClassInstance classInstance) {
						foreach(IMember member in objectInstance.Type.Members) {

							// Skip non-field members.
							if(member.MemberType != MemberType.Field) continue;
							IField field = member.Definition as IField;

							// Get the field initializer expression context, and compile it.
							if(field.Initializer != null) {
								var initializerContext = field.Initializer.Context;
								ResultInfo initializerResult = location1.Compiler.CompileExpression(location1, initializerContext, out IInstance value);
								if(initializerResult.Failure) {
									result = null;
									return location1.GetLocation(initializerContext) + initializerResult;
								}


								// Assign the value to the field.
								writer.WriteComments(
									$"Assign the value of the field '{member.TypeIdentifier} {member.Identifier}' to the new object.",
									indentBefore: true);
								value.SaveToBlock(location1, objectInstance.GetSelector(location1), classInstance.FieldObjectives[field]);
								writer.AddBufferedLines(1);
							}

						}

						// Set the pointer to the new entity.
						writer.WriteComments(
							$"Set the pointer to the new entity.");
						location1.Compiler.CompileSimpleOperation(location1, Operation.Assign, objectInstance.Pointer, random, out _);

					}
				}

				// Mark the function as compiled before compiling the statements.
				Compiled = true;
				location1.Compiler.CompileStatements(this, Scope, Statements);

			}

			// Set the result to the return instance.
			result = ReturnInstance;

			// Invoke the function in the datapack and return a success.
			location.Writer.WriteCommand($"function {Writer.GamePath}");
			return ResultInfo.DefaultSuccess;

		}

		private string NextChildName() {
			return ChildFunctions.Count.ToString();
		}

		public void Dispose() {
			
			// Dispose of the writer.
			Writer.Dispose();

			// Dispose of the child functions.
			foreach(StandaloneStatementFunction child in ChildFunctions)
				child.Dispose();

		}

	}

}
