using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;

namespace MCSharp.Compilation.Instancing;

/// <summary>
/// Represents an instance of some type.
/// </summary>
public interface IInstance {

	/// <summary>
	/// The <see cref="IType"/> that defines this instance.
	/// </summary>
	public IType Type { get; }

	/// <summary>
	/// The local identifier for this instance.
	/// </summary>
	public string Identifier { get; }


	/// <summary>
	/// <see cref="Exception"/> thrown when <see cref="Type"/> is assigned an invalid value.
	/// </summary>
	public class InvalidTypeException : Exception {
		public InvalidTypeException(IType given, string expected) : base($"Cannot assign '{given.Identifier}' to {nameof(Type)} when '{expected}' was expected.") { }
	}

	protected static Exception GenerateInvalidBlockRangeException(int length, int expected)
		=> new InvalidOperationException($"The given range of length {length} does not match the expected range of length {expected}.");


	/// <summary>
	/// Assigns the value of this <see cref="IInstance"/> into a new <see cref="IInstance"/>.
	/// If possible, not of the <see cref="IConstantInstance"/> interface.
	/// </summary>
	/// <param name="compile"></param>
	/// <param name="identifier">The <see cref="Identifier"/> of the new <see cref="IInstance"/>.</param>
	/// <returns>Returns a new <see cref="IInstance"/>.</returns>
	public IInstance Copy(Compiler.CompileArguments compile, string identifier);

	/// <inheritdoc cref="IInstanceExtensions.SaveToBlock(IInstance, Compiler.CompileArguments, string, Objective[])"/>
	/// <param name="range"></param>
	/// <seealso cref="IInstanceExtensions.SaveToBlock(IInstance, Compiler.CompileArguments, string, Objective[])"/>
	public void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range);

	/// <inheritdoc cref="IInstanceExtensions.LoadFromBlock(IInstance, Compiler.CompileArguments, string, Objective[])"/>
	/// <param name="range"></param>
	/// <seealso cref="IInstanceExtensions.LoadFromBlock(IInstance, Compiler.CompileArguments, string, Objective[])"/>
	public void LoadFromBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range);

}

public static class IInstanceExtensions {

	public static ResultInfo ConvertType(this IInstance instance, Compiler.CompileArguments location, IType toType, out IInstance value) {

		IType fromType = instance.Type;
		IDictionary<IType, IConversion> conversions = fromType.Conversions;

		if(conversions.ContainsKey(toType)) {

			var conversion = conversions[toType];
			ResultInfo conversionResult = conversion.Function.InvokeStatic(location, Array.Empty<IType>(), new IInstance[] { instance }, out value);
			if(conversionResult.Failure) { value = null; return conversionResult; }

			return ResultInfo.DefaultSuccess;

		} else {

			value = null;
			return new ResultInfo(false, $"Cannot cast '{fromType.Identifier}' as '{toType.Identifier}'.");

		}

	}

	/// <summary>
	/// Saves this <see cref="IInstance"/> to the given set of <see cref="Objective"/>s.
	/// </summary>
	/// <param name="instance">The <see cref="IInstance"/> to save.</param>
	/// <param name="location"></param>
	/// <param name="selector"></param>
	/// <exception cref="InvalidOperationException">Thrown when the relevant length of <paramref name="block"/> does not match <see cref="ITypeExtensions.GetBlockSize(IType, Compiler)"/>.</exception>
	/// <seealso cref="IInstance.SaveToBlock(Compiler.CompileArguments, string, Objective[], Range)"/>
	/// <param name="block">The array of <see cref="Objective"/>s to save to.</param>

	public static void SaveToBlock(this IInstance instance, Compiler.CompileArguments location, string selector, Objective[] block) {
		instance.SaveToBlock(location, selector, block, 0..^0);
	}

	/// <summary>
	/// Loads a value for this <see cref="IInstance"/> from the given set of <see cref="Objective"/>s.
	/// </summary>
	/// <param name="instance">The <see cref="IInstance"/> to load.</param>
	/// <param name="location"></param>
	/// <param name="selector"></param>
	/// <exception cref="InvalidOperationException">Thrown when the relevant length of <paramref name="block"/> does not match <see cref="ITypeExtensions.GetBlockSize(IType, Compiler)"/>.</exception>
	/// <seealso cref="IInstance.SaveToBlock(Compiler.CompileArguments, string, Objective[], Range)"/>
	/// <param name="block">The array of <see cref="Objective"/>s to load from.</param>
	public static void LoadFromBlock(this IInstance instance, Compiler.CompileArguments location, string selector, Objective[] block) {
		instance.LoadFromBlock(location, selector, block, 0..^0);
	}

	public static ResultInfo InvokeBestMethod(
		this IInstance instance, Compiler.CompileArguments location, ITerminalNode identifier,
		GenericArgumentsContext generic_arguments, MethodArgumentsContext method_arguments, out IInstance value
	) {

		// Find the best method.
		ResultInfo searchResult = instance.Type.FindBestMethodFromContext(location, identifier, generic_arguments, method_arguments,
			out IMember member, out IType[] genericTypes, out _, out IInstance[] argumentInstances);
		if(searchResult.Failure) {
			value = null;
			return searchResult;
		}
		IMethod method = member.Definition as IMethod;

		// Invoke method.
		ResultInfo result = method.Invoker.InvokeNonStatic(location, instance, genericTypes, argumentInstances, out value);
		if(result.Failure) return location.GetLocation(identifier) + result;

		return ResultInfo.DefaultSuccess;

	}

	public static ResultInfo InvokePropertyOrFieldFromContext(this IInstance instance, Compiler.CompileArguments location, ITerminalNode identifier, IMember propertyOrField, out IInstance value) {

		#region Argument Checks
		if(instance is null)
			throw new ArgumentNullException(nameof(instance));
		if(identifier is null)
			throw new ArgumentNullException(nameof(identifier));
		if(propertyOrField is null)
			throw new ArgumentNullException(nameof(propertyOrField));
		#endregion

		IMemberDefinition definition = propertyOrField.Definition;

		if(definition is IProperty property) {

			// Get getter.
			var getter = property.Getter;
			if(getter == null) {
				value = null;
				return new ResultInfo(false, location.GetLocation(identifier) + "This property is not get-able.");
			}

			// Invoke 'get' method.
			ResultInfo result = property.Getter.InvokeNonStatic(location, instance, Array.Empty<IType>(), Array.Empty<IInstance>(), out value);
			if(result.Failure) return location.GetLocation(identifier) + result;

			// Return success.
			return ResultInfo.DefaultSuccess;

		} else if(definition is IField field) {

			// Struct, Class, and Predefined all have different ways of storing fields.
			switch(instance) {

				// Get IInstance value from struct field dictionary.
				case StructInstance structInstance: {
					value = structInstance.FieldInstances[field];
					return ResultInfo.DefaultSuccess;
				}

				// Create IInstance value from object reference.
				// Simple objects do not have fields so we can assume it is a class type.
				case ClassInstance classInstance: {
					value = location.Compiler.DefinedTypes[propertyOrField.TypeIdentifier].InitializeInstance(location, null);
					Objective[] block = classInstance.FieldObjectives[field];
					string selector = classInstance.GetSelector(location);
					value.LoadFromBlock(location, selector, block);
					return ResultInfo.DefaultSuccess;
				}

				// PrimitiveInstance
				case PrimitiveInstance primitiveInstance: {
					value = null;
					return new ResultInfo(false, $"{location.GetLocation(identifier)}Accessing fields of primitive types is not supported.");
				}

				default: throw new Exception($"Unsupported type of {nameof(IInstance)}: '{instance.GetType().FullName}'.");

			}

		} else {

			throw new InvalidOperationException($"{definition.GetType().Name} is not a property or field.");

		}

	}

}
