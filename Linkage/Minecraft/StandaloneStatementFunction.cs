using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCSharp.Linkage.Minecraft;

/// <summary>
/// 
/// </summary>
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

	private bool ReturnThis { get; }

	/// <inheritdoc/>
	public IInstance ReturnInstance { get; private set; }

	/// <inheritdoc/>
	public IInstance ThisInstance { get; private set; }

	/// <inheritdoc/>
	public string ReturnTypeIdentifier { get; }

	/// <inheritdoc/>
	public string ThisTypeIdentifier { get; }

	private ICollection<StandaloneStatementFunction> ChildFunctions { get; } = new LinkedList<StandaloneStatementFunction>();

	/// <summary>
	/// Whether or not the function has been compiled.
	/// </summary>
	public bool Compiled { get; set; }

	private IScopeHolder ScopeHolder { get; }

	private Scope scope;

	/// <summary>
	/// The <see cref="Compilation.Scope"/> this <see cref="IFunction"/> is in.
	/// </summary>
	public Scope Scope => ScopeHolder?.Scope ?? scope;

	private Compiler.OnLoadDelegate onLoadDelegate;
	private ref Compiler.OnLoadDelegate OnLoadDelegate => ref onLoadDelegate;

	/// <summary>
	/// Creates a new <see cref="StandaloneStatementFunction"/> with a scope of a <see cref="IScopeHolder"/>.
	/// </summary>
	/// <param name="writer">The <see cref="FunctionWriter"/> this <see cref="IFunction"/> will write to.</param>
	/// <param name="holder">The <see cref="IScopeHolder"/> this <see cref="IFunction"/> belongs to.</param>
	/// <param name="genericParameters">The <see cref="IGenericParameter"/>s of this <see cref="IFunction"/>.</param>
	/// <param name="methodParameters">The <see cref="IMethodParameter"/>s of this <see cref="IFunction"/>.</param>
	/// <param name="statements">The <see cref="IStatement"/>s of this <see cref="IFunction"/>.</param>
	/// <param name="returnType">The local identifier of the return type for this <see cref="IFunction"/>.</param>
	/// <param name="thisType">The local identifier of the 'this' type for this <see cref="IFunction"/>. Pass <see langword="null"/> to define this <see cref="IFunction"/> as static.</param>
	/// <exception cref="ArgumentNullException">Thrown when any argument (except <paramref name="thisType"/>) is <see langword="null"/>.</exception>
	public StandaloneStatementFunction(
		FunctionWriter writer, IScopeHolder holder,
		IGenericParameter[] genericParameters, IMethodParameter[] methodParameters, IStatement[] statements,
		string returnType, string thisType, bool ctor, ref Compiler.OnLoadDelegate onLoad
	) {

		Writer = writer ?? throw new ArgumentNullException(nameof(writer));
		GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
		MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
		Statements = statements ?? throw new ArgumentNullException(nameof(statements));
		ReturnTypeIdentifier = returnType ?? throw new ArgumentNullException(nameof(returnType));
		ThisTypeIdentifier = thisType;
		ReturnThis = ctor;

		ScopeHolder = holder ?? throw new ArgumentNullException(nameof(holder));

		OnLoadDelegate = onLoad += OnLoad;

	}

	/// <summary>
	/// Creates a new <see cref="StandaloneStatementFunction"/> with a custom <see cref="Compilation.Scope"/>.
	/// </summary>
	/// <param name="writer">The <see cref="FunctionWriter"/> this <see cref="IFunction"/> will write to.</param>
	/// <param name="scope">The <see cref="Compilation.Scope"/> this <see cref="IFunction"/> belongs to.</param>
	/// <param name="genericParameters">The <see cref="IGenericParameter"/>s of this <see cref="IFunction"/>.</param>
	/// <param name="methodParameters">The <see cref="IMethodParameter"/>s of this <see cref="IFunction"/>.</param>
	/// <param name="statements">The <see cref="IStatement"/>s of this <see cref="IFunction"/>.</param>
	/// <param name="returnType">The local identifier of the return type for this <see cref="IFunction"/>.</param>
	/// <exception cref="ArgumentNullException">Thrown when any argument is <see langword="null"/></exception>
	public StandaloneStatementFunction(
		FunctionWriter writer, Scope scope,
		IGenericParameter[] genericParameters, IMethodParameter[] methodParameters, IStatement[] statements,
		string returnType, string thisType, bool ctor, ref Compiler.OnLoadDelegate onLoad
	) {

		Writer = writer ?? throw new ArgumentNullException(nameof(writer));
		GenericParameters = genericParameters ?? throw new ArgumentNullException(nameof(genericParameters));
		MethodParameters = methodParameters ?? throw new ArgumentNullException(nameof(methodParameters));
		Statements = statements ?? throw new ArgumentNullException(nameof(statements));
		ReturnTypeIdentifier = returnType ?? throw new ArgumentNullException(nameof(returnType));
		ThisTypeIdentifier = thisType;
		ReturnThis = ctor;

		this.scope = scope ?? throw new ArgumentNullException(nameof(scope));

		OnLoadDelegate = onLoad += OnLoad;

	}


	private ResultInfo OnLoad(Compiler.CompileArguments location) {

		// Initialize method parameters when loading the datapack.
		if(MethodParameters.Count > 0) {
			location.Writer.WriteComments(
				$"Create parameters for '{Writer.GamePath}'.");
			MethodParameters.MakeOrGetInstances(new Compiler.CompileArguments(location.Compiler, location.Function, Scope, location.Predefined));
		}

		// Initialize context instances when loading the datapack.
		ResultInfo contextsResult = InitializeContextInstances(location);
		if(contextsResult.Failure) return contextsResult;

		// Return a success.
		return ResultInfo.DefaultSuccess;

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

		StandaloneStatementFunction child = new StandaloneStatementFunction(
			new FunctionWriter(Writer.VirtualMachine, settings, $"{Writer.LocalDirectory}\\{Writer.Name}", name),
			new Scope(null, Scope),
			GenericParameters.ToArray(), Array.Empty<IMethodParameter>(), statements,
			"void", ThisTypeIdentifier, ctor: false, ref OnLoadDelegate
		);

		ChildFunctions.Add(child);
		return child;

	}


	/// <summary>
	/// 
	/// </summary>
	private ResultInfo InitializeContextInstances(Compiler.CompileArguments location) {

		// Create the 'this' instance.
		if(ThisTypeIdentifier is not null) {
			IType thisType = location.Compiler.DefinedTypes[ThisTypeIdentifier];
			if(thisType.ClassType == ClassType.Class) {

				// Since 'this' is by-reference, instead of using a selector each time we want to find 'this',
				// call the function in a way we can use "@s" instead (ie. execute as).
				ClassInstance reference = thisType.InitializeInstance(location, "this") as ClassInstance;
				ThisInstance = new SelfClassInstance(location, reference, "this");

			} else {

				ThisInstance = thisType.InitializeInstance(location, null);

			}
		}

		// Create the 'return' instance.
		if(ReturnThis) {

			// Do not return self, as that would be saying we return @s, which is wrong.
			// Return the address of self instead, which is correct.
			ReturnInstance = ThisInstance is SelfClassInstance self ? self.Reference : ThisInstance;

		} else if(ReturnTypeIdentifier != "void") {

			// Initialize an instance for the return value at the given location.
			ReturnInstance = location.Compiler.DefinedTypes[ReturnTypeIdentifier].InitializeInstance(location, "return");

		}

		// Return a success.
		return ResultInfo.DefaultSuccess;

	}

	/// <summary>
	/// 
	/// </summary>
	private ResultInfo InstantiateContextInstances(Compiler.CompileArguments location) {

		// Instantiate the return instance.
		// Create the entity and assign all initial values to each field.
		if(ReturnInstance is ObjectInstance objectInstance) {

			// Get the function writer.
			FunctionWriter writer = location.Writer;

			// Write comments.
			writer.WriteComments(
				$"Instantiate a new object.",
				indentBefore: true);

			if(!ReturnThis) {
				objectInstance.CreateEntity(location);
			}

			// If the object is a class type, compile initialization expression for each field.
			if(objectInstance is ClassInstance classInstance) {

				foreach(IMember member in classInstance.Type.Members) {

					// Skip non-field members.
					if(member.MemberType != MemberType.Field) continue;
					IField field = member.Definition as IField;

					// Get the field initializer expression context, and compile it.
					if(field.Initializer != null) {

						var initializerContext = field.Initializer.Context;
						ResultInfo initializerResult = Compiler.CompileExpression(location, initializerContext, out IInstance value);
						if(initializerResult.Failure) return location.GetLocation(initializerContext) + initializerResult;

						// Assign the value to the field.
						writer.WriteComments(
							$"Assign the value of the field '{member.TypeIdentifier} {member.Identifier}' to the new object.",
							indentBefore: true);
						value.SaveToBlock(location, classInstance.GetSelector(location), classInstance.FieldObjectives[field]);
						writer.AddBufferedLines(1);
					}

				}

			}

		}

		// Return success.
		return ResultInfo.DefaultSuccess;

	}

	/// <summary>
	/// 
	/// </summary>
	private ResultInfo PrepareAtCall(Compiler.CompileArguments location, IInstance[] arguments, out IInstance value) {

		// Set the result to the return instance.
		value = ReturnInstance;

		// Cast the arguments to the correct type.
		if(arguments.Length > 0)
			location.Writer.WriteComments(
				$"Send arguments for '{Writer.Name}'.",
				indentBefore: true);
		for(int i = 0; i < arguments.Length; i++) {
			IInstance argument = arguments[i];
			IType parameterType = location.Compiler.DefinedTypes[MethodParameters[i].TypeIdentifier];
			if(argument.Type == parameterType) continue;
			argument.Type.Conversions[parameterType].Function.InvokeStatic(location, Array.Empty<IType>(), new IInstance[] { argument }, out arguments[i]);
		}

		// Assign the arguments to the parameters.
		for(int i = 0; i < arguments.Length; i++) {
			// TODO: This is the wrong location for creating the parameters. Do it on load.
			MethodParameters.MakeOrGetInstances(location);
			Compiler.CompileSimpleOperation(location, Operation.Assign, MethodParameters[i].Instance, arguments[i], out _);
		}

		//// If function needs to be called with an "execute as", make the 'this' before calling so that can be done.
		//if(ReturnThis && ReturnInstance is ObjectInstance objectInstance) {
		//	objectInstance.CreateEntity(location);
		//}

		// Return success.
		return ResultInfo.DefaultSuccess;

	}

	/// <summary>
	/// 
	/// </summary>
	private ResultInfo PrepareAtFunction(Compiler.CompileArguments location) {

		// Create the location for the function.
		Compiler.CompileArguments innerLocation = new Compiler.CompileArguments(location.Compiler, this, Scope, false);

		// Construct context instances within the function.
		ResultInfo result = InstantiateContextInstances(innerLocation);
		if(result.Failure) return result;

		// Mark the function as compiled before compiling the statements.
		Compiled = true;
		return innerLocation.Compiler.CompileStatements(this, Scope, Statements);

	}

	/// <inheritdoc/>
	public ResultInfo InvokeStatic(Compiler.CompileArguments location, IType[] generic, IInstance[] arguments, out IInstance result) {

		#region Argument Checks
		if(generic is null) throw new ArgumentNullException(nameof(generic));
		if(arguments is null) throw new ArgumentNullException(nameof(arguments));
		#endregion

		// Verify the function is static.
		if(ThisTypeIdentifier is not null) {
			result = null;
			return new ResultInfo(success: false, "Cannot invoke a static function with an instance for context.");
		}

		// Check if the function is compiled.
		if(!Compiled) {
			ResultInfo compileResult = PrepareAtFunction(location);
			if(compileResult.Failure) {
				result = null;
				return compileResult;
			}
		}

		// Assign to parameters and other actions to do before calling.
		ResultInfo argsResult = PrepareAtCall(location, arguments, out result);
		if(argsResult.Failure) return argsResult;

		// Invoke the function.
		location.Writer.WriteCommand($"function {Writer.GamePath}");

		// Return a success.
		return ResultInfo.DefaultSuccess;

	}

	/// <inheritdoc/>
	public ResultInfo InvokeNonStatic(Compiler.CompileArguments location, IInstance context, IType[] generics, IInstance[] arguments, out IInstance result) {

		#region Argument Checks
		if(context is null) throw new ArgumentNullException(nameof(context));
		foreach(IInstance generic in generics)
			if(generic is null) throw new ArgumentNullException(nameof(generics), "Cannot contain any null values.");
		if(arguments is null) throw new ArgumentNullException(nameof(arguments));
		foreach(IInstance argument in arguments)
			if(argument is null) throw new ArgumentNullException(nameof(arguments), "Cannot contain any null values.");
		#endregion

		// Verify the function is non-static.
		if(ThisTypeIdentifier is null) {
			result = null;
			return new ResultInfo(success: false, "Cannot invoke a non-static function without an instance for context.");
		}

		// Check if the function is compiled.
		if(!Compiled) {
			ResultInfo compileResult = PrepareAtFunction(location);
			if(compileResult.Failure) {
				result = null;
				return compileResult;
			}
		}

		// Assign to parameters and other actions to do before calling.
		ResultInfo argsResult = PrepareAtCall(location, arguments, out result);
		if(argsResult.Failure) return argsResult;

		// Assign to value of 'this'.
		location.Writer.WriteComments(
			$"Assign value of 'this'.",
			indentBefore: true);
		ResultInfo thisResult = Compiler.CompileSimpleOperation(location, Operation.Assign, ThisInstance, context, out _);
		if(thisResult.Failure) return thisResult;

		// Invoke the function.
		if(ThisInstance is ObjectInstance @object) {

			// Execute the function as the 'this' object.
			location.Writer.WriteComments(
				$"Execute function as 'this' entity.");
			if(@object is SelfClassInstance self) @object = self.Reference;
			location.Writer.WriteCommand($"execute {@object.GetExecuteAs()} run function {Writer.GamePath}");

		} else {

			// No entity to execute as.
			location.Writer.WriteCommand($"function {Writer.GamePath}");

		}

		// Update the passed context.
		location.Writer.WriteComments(
			$"Get the returned value and modified 'this' instance.");
		ResultInfo assignResult = Compiler.CompileSimpleOperation(location, Operation.Assign, context, ThisInstance, out _);
		if(assignResult.Failure) return assignResult;

		// Return a success.
		return ResultInfo.DefaultSuccess;

	}

	private string NextChildName() {
		return ChildFunctions.Count.ToString();
	}

	/// <inheritdoc/>
	public void Dispose() {

		// Dispose of the writer.
		Writer.Dispose();

		// Dispose of the child functions.
		foreach(StandaloneStatementFunction child in ChildFunctions)
			child.Dispose();

		GC.SuppressFinalize(this);

	}

}
