using Antlr4.Runtime.Tree;
using MCSharp.Collections;
using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MCSharp.Linkage;

/// <summary>
/// Represents a type definition.
/// </summary>
public interface IType : IScopeHolder, IDisposable {

	/// <summary>
	/// The modifiers that affect this type definition.
	/// </summary>
	public Modifier Modifiers { get; }

	/// <summary>
	/// Whether this type definition is for a class, struct, or primitive.
	/// </summary>
	public ClassType ClassType { get; }

	/// <summary>
	/// The local identifier for this type definition.
	/// </summary>
	public string Identifier { get; }

	/// <summary>
	/// The <see cref="IType"/>(s) that this <see cref="IType"/> inherits from.
	/// </summary>
	/// <seealso cref="DerivedTypes"/>
	public IReadOnlyCollection<IType> BaseTypes { get; }

	/// <summary>
	/// The <see cref="IType"/>(s) that this <see cref="IType"/> gives inheritance to.
	/// </summary>
	/// <remarks><i>Not to be confused with <see cref="SubTypes"/>.</i></remarks>
	/// <seealso cref="BaseTypes"/>
	public ICollection<IType> DerivedTypes { get; }

	/// <summary>
	/// The members defined by this type definition.
	/// </summary>
	public IReadOnlyCollection<IMember> Members { get; }

	/// <summary>
	/// The constructors defined by this type definition.
	/// </summary>
	public IReadOnlyCollection<IConstructor> Constructors { get; }

	/// <summary>
	/// Type definitions defined within this type definition.
	/// </summary>
	/// <remarks><i>Not to be confused with <see cref="DerivedTypes"/>.</i></remarks>
	public IReadOnlyCollection<IType> SubTypes { get; }

	/// <summary>
	/// 
	/// </summary>
	public IHashSetDictionary<Operation, IOperation> Operations { get; }

	/// <summary>
	/// 
	/// </summary>
	public IDictionary<IType, IConversion> Conversions { get; }

	/// <summary>
	///
	/// </summary>
	public IReadOnlyDictionary<IField, IInstance> StaticFieldInstances { get; }

	/// <summary>
	/// 
	/// </summary>
	/// <param name="location"></param>
	/// <param name="identifier"></param>
	/// <returns></returns>
	public delegate IInstance InitializeInstanceDelegate(Compiler.CompileArguments location, string identifier);

	/// <summary>
	/// Creates an assignable <see cref="IInstance"/> from nothing.
	/// </summary>
	/// <param name="location">The location in code to create the <see cref="IInstance"/> in.</param>
	/// <param name="identifier">The <see cref="IInstance.Identifier"/> value used as a name.</param>
	/// <returns>Returns a new <see cref="IInstance"/>.</returns>
	public IInstance InitializeInstance(Compiler.CompileArguments location, string identifier);

}

/// <summary>
/// Extensions for the <see cref="IType"/> interface.
/// </summary>
public static class ITypeExtensions {

	/// <summary>
	/// Enumerates through all values in <see cref="IType.SubTypes"/> recursively, starting from direct children of <paramref name="type"/>.
	/// </summary>
	/// <param name="type">The <see cref="IType"/> to enumerate from.</param>
	/// <returns>Returns an <see cref="IEnumerable{IType}"/> created by '<see langword="yield"/>-ing' over <see cref="IType.SubTypes"/>'s <see cref="IEnumerator{IType}"/>.</returns>
	public static IEnumerable<IType> EnumerateSubtypeTree(this IType type) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		#endregion

		foreach(IType subtype in type.SubTypes) {
			yield return subtype;
			foreach(IType enumerated in subtype.EnumerateSubtypeTree()) {
				yield return enumerated;
			}
		}

	}

	/// <summary>
	/// Enumerates through all values in <see cref="IType.SubTypes"/> recursively, starting from the furthest children of <paramref name="type"/>.
	/// </summary>
	/// <param name="type">The <see cref="IType"/> to enumerate from.</param>
	/// <returns>Returns an <see cref="IEnumerable{IType}"/> created by '<see langword="yield"/>-ing' over <see cref="IType.SubTypes"/>'s <see cref="IEnumerator{IType}"/>.</returns>
	public static IEnumerable<IType> EnumerateSubtypeTreeReversed(this IType type) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		#endregion

		foreach(IType subtype in type.SubTypes) {
			foreach(IType enumerated in subtype.EnumerateSubtypeTreeReversed()) {
				yield return enumerated;
			}
			yield return subtype;
		}

	}

	/// <summary>
	/// Enumerates through all values in <see cref="IType.BaseTypes"/> recursively, starting from the direct parents of <paramref name="type"/>.
	/// </summary>
	/// <param name="type">The <see cref="IType"/> to enumerate from.</param>
	/// <returns>Returns an <see cref="IEnumerable{IType}"/> created by '<see langword="yield"/>-ing' over <see cref="IType.BaseTypes"/>'s <see cref="IEnumerator{IType}"/>.</returns>
	public static IEnumerable<IType> EnumerateBaseTypeTree(this IType type) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		#endregion

		foreach(IType baseType in type.BaseTypes) {
			yield return baseType;
			foreach(IType enumerated in baseType.EnumerateBaseTypeTree()) {
				yield return enumerated;
			}
		}

	}

	/// <summary>
	/// Enumerates through all values in <see cref="IType.BaseTypes"/> recursively, starting from the furthest parents of <paramref name="type"/>.
	/// </summary>
	/// <param name="type">The <see cref="IType"/> to enumerate from.</param>
	/// <returns>Returns an <see cref="IEnumerable{IType}"/> created by '<see langword="yield"/>-ing' over <see cref="IType.BaseTypes"/>'s <see cref="IEnumerator{IType}"/>.</returns>
	public static IEnumerable<IType> EnumerateBasetypeTreeReversed(this IType type) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		#endregion

		foreach(IType baseType in type.BaseTypes) {
			foreach(IType enumerated in baseType.EnumerateBasetypeTreeReversed()) {
				yield return enumerated;
			}
			yield return baseType;
		}

	}

	/// <summary>
	/// Enumerates through all values in <see cref="IType.DerivedTypes"/> recursively, starting from the direct children of <paramref name="type"/>.
	/// </summary>
	/// <param name="type">The <see cref="IType"/> to enumerate from.</param>
	/// <returns>Returns an <see cref="IEnumerable{IType}"/> created by '<see langword="yield"/>-ing' over <see cref="IType.DerivedTypes"/>'s <see cref="IEnumerator{IType}"/>.</returns>
	public static IEnumerable<IType> EnumerateDerivedTypeTree(this IType type) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		#endregion

		foreach(IType derivedType in type.DerivedTypes) {
			yield return derivedType;
			foreach(IType enumerated in derivedType.EnumerateDerivedTypeTree()) {
				yield return enumerated;
			}
		}

	}

	/// <summary>
	/// Enumerates through all values in <see cref="IType.DerivedTypes"/> recursively, starting from the furthest children of <paramref name="type"/>.
	/// </summary>
	/// <param name="type">The <see cref="IType"/> to enumerate from.</param>
	/// <returns>Returns an <see cref="IEnumerable{IType}"/> created by '<see langword="yield"/>-ing' over <see cref="IType.DerivedTypes"/>'s <see cref="IEnumerator{IType}"/>.</returns>
	public static IEnumerable<IType> EnumerateDerivedTypeTreeReversed(this IType type) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		#endregion

		foreach(IType derivedType in type.DerivedTypes) {
			foreach(IType enumerated in derivedType.EnumerateDerivedTypeTreeReversed()) {
				yield return enumerated;
			}
			yield return derivedType;
		}

	}

	/// <summary>
	/// Counts the number of <see cref="Minecraft.Objective"/>s needed to store an <see cref="IInstance"/> of type <paramref name="type"/>.
	/// </summary>
	/// <param name="type"></param>
	/// <param name="compiler"></param>
	/// <returns></returns>
	public static int GetBlockSize(this IType type, Compiler compiler) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		if(compiler is null)
			throw new ArgumentNullException(nameof(compiler));
		#endregion

		int count = 0;

		// Base cases.
		if(type.ClassType == ClassType.Class) return 1;
		if(type.Identifier == MCSharpLinkerExtension.IntIdentifier) return 1;
		if(type.Identifier == MCSharpLinkerExtension.BoolIdentifier) return 1;

		// Count field sizes.
		foreach(var member in type.Members) {
			if(member.MemberType != MemberType.Field) continue;
			var memberType = compiler.DefinedTypes[member.TypeIdentifier];
			count += memberType.GetBlockSize(compiler);
		}

		// Return final count.
		return count;

	}

	/// <summary>
	/// Sums the value of <see cref="GetBlockSize(IType, Compiler)"/> for every <see cref="IField"/> in <see cref="IType.Members"/>.
	/// Only differs from <see cref="GetBlockSize(IType, Compiler)"/> when <paramref name="type"/> is not an <see cref="ObjectInstance"/>.
	/// </summary>
	/// <param name="type"></param>
	/// <param name="compiler"></param>
	/// <returns></returns>
	public static int GetFieldBlockSizes(this IType type, Compiler compiler) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		if(compiler is null)
			throw new ArgumentNullException(nameof(compiler));
		#endregion

		int count = 0;

		// Sum every member's size.
		foreach(var member in type.Members) {
			if(member.MemberType != MemberType.Field) continue;
			var memberType = compiler.DefinedTypes[member.TypeIdentifier];
			count += memberType.GetBlockSize(compiler);
		}

		// Return final count.
		return count;

	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="type"></param>
	/// <param name="genericArguments"></param>
	/// <param name="methodArguments"></param>
	public static IConstructor FindBestConstructor(this IType type, Compiler compiler, IType[] genericArguments, IInstance[] methodArguments) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		if(compiler is null)
			throw new ArgumentNullException(nameof(compiler));
		if(genericArguments is null)
			throw new ArgumentNullException(nameof(genericArguments));
		if(genericArguments.Contains(null))
			throw new ArgumentException($"'{nameof(genericArguments)}' cannot contain null values.");
		if(methodArguments is null)
			throw new ArgumentNullException(nameof(methodArguments));
		if(methodArguments.Contains(null))
			throw new ArgumentException($"'{nameof(methodArguments)}' cannot contain null values.");
		#endregion

		IType[] types = new IType[methodArguments.Length];
		for(int i = 0; i < types.Length; i++)
			types[i] = methodArguments[i].Type;
		return type.FindBestConstructor(compiler, genericArguments, types);

	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="type"></param>
	/// <param name="genericArguments"></param>
	/// <param name="methodArguments"></param>
	public static IConstructor FindBestConstructor(this IType type, Compiler compiler, IType[] genericArguments, IType[] methodArguments) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		if(compiler is null)
			throw new ArgumentNullException(nameof(compiler));
		if(genericArguments is null)
			throw new ArgumentNullException(nameof(genericArguments));
		if(genericArguments.Contains(null))
			throw new ArgumentException($"'{nameof(genericArguments)}' cannot contain null values.");
		if(methodArguments is null)
			throw new ArgumentNullException(nameof(methodArguments));
		if(methodArguments.Contains(null))
			throw new ArgumentException($"'{nameof(methodArguments)}' cannot contain null values.");
		#endregion

		(int Score, IConstructor Constructor) best = (-1, null);
		foreach(IConstructor constructor in type.Constructors) {

			// Get the parameters.
			Minecraft.IFunction invoker = constructor.Invoker;
			var methodParameters = invoker.MethodParameters;
			var genericParameters = invoker.GenericParameters;

			// Check if generic parameter count matches.
			int genericCount = genericParameters.Count;
			if(genericCount != genericArguments.Length) continue;

			// Check if method parameter count matches.
			int methodCount = methodParameters.Count;
			if(methodCount != methodArguments.Length) continue;

			// Set maximum score to number of method parameters.
			int score = methodCount;

			// Check if generic parameters match.
			for(int i = 0; i < genericCount; i++) {
				// TODO
			}

			// Check if method parameters match.
			for(int i = 0; i < methodCount; i++) {

				// Get types of argument and parameter.
				IType argumentType = methodArguments[i];
				IType parameterType = compiler.DefinedTypes[methodParameters[i].TypeIdentifier];

				// If the argument is directly assignable to the parameter, don't subtract score.
				if(parameterType.IsAssignableFrom(argumentType)) {
					continue;
				}

				// If the argument has a cast to the parameter type, subtract 1 from the score.
				if(argumentType.Conversions.ContainsKey(parameterType)) {
					score--;
					continue;
				}

				// The argument is not assignable to the parameter. Don't use this method.
				score = -1;
				break;

			}

			// If the score is better than the best, use this constructor.
			if(score > best.Score) best = (score, constructor);
			else continue;

		}

		return best.Constructor;

	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="type"></param>
	/// <param name="compiler"></param>
	/// <param name="genericArguments"></param>
	/// <param name="methodArguments"></param>
	/// <param name="name"></param>
	/// <returns></returns>
	public static IMember FindBestMethod(this IType type, Compiler compiler, IType[] genericArguments, IType[] methodArguments, string name) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		if(compiler is null)
			throw new ArgumentNullException(nameof(compiler));
		if(genericArguments is null)
			throw new ArgumentNullException(nameof(genericArguments));
		if(genericArguments.Contains(null))
			throw new ArgumentException($"'{nameof(genericArguments)}' cannot contain null values.");
		if(methodArguments is null)
			throw new ArgumentNullException(nameof(methodArguments));
		if(methodArguments.Contains(null))
			throw new ArgumentException($"'{nameof(methodArguments)}' cannot contain null values.");
		if(string.IsNullOrEmpty(name))
			throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
		#endregion

		(int Score, IMember Member) best = (-1, null);

		// Local function to allow for recursion.
		void FindBestMethod(IType type) {

			// Local function for checking if a method is the best.
			void CheckMethod(IMember member) {

				// Iterate over all methods.
				if(member.MemberType != MemberType.Method) return;
				IMethod method = member.Definition as IMethod;
				if(member.Identifier != name) return;

				// Get the parameters.
				Minecraft.IFunction invoker = method.Invoker;
				var methodParameters = invoker.MethodParameters;
				var genericParameters = invoker.GenericParameters;

				// Check if generic parameter count matches.
				int genericCount = genericParameters.Count;
				if(genericCount != genericArguments.Length) return;

				// Check if method parameter count matches.
				int methodCount = methodParameters.Count;
				if(methodCount != methodArguments.Length) return;

				// Set maximum score to number of method parameters.
				int score = methodCount;

				// Test if generics match.
				for(int i = 0; i < genericCount; i++) {
					// TODO
				}

				// Test if arguments match.
				for(int i = 0; i < methodCount; i++) {

					// Get types of argument and parameter.
					IType argumentType = methodArguments[i];
					IType parameterType = compiler.DefinedTypes[methodParameters[i].TypeIdentifier];

					// If the argument is directly assignable to the parameter, don't subtract score.
					if(parameterType.IsAssignableFrom(argumentType)) {
						continue;
					}

					// If the argument has a cast to the parameter type, subtract 1 from the score.
					if(argumentType.Conversions.ContainsKey(parameterType)) {
						score--;
						continue;
					}

					// The argument is not assignable to the parameter. Don't use this method.
					score = -1;
					break;

				}

				// If the score is better than the best, use this method.
				if(score > best.Score) best = (score, member);

			}

			// Iterate over all members in this type.
			foreach(IMember member in type.Members) {
				CheckMethod(member);
			}

			// Iterate over all members in base types.
			foreach(IType baseType in type.BaseTypes) {
				FindBestMethod(baseType);
			}

		}

		// Find the best method.
		FindBestMethod(type);
		return best.Member;

	}

	public static ResultInfo FindBestMethodFromContext(this IType type, Compiler.CompileArguments location, ITerminalNode identifier,
	GenericArgumentsContext generic_arguments, MethodArgumentsContext method_arguments,
	out IMember member, out IType[] genericTypes, out IType[] argumentTypes, out IInstance[] argumentInstances) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		if(identifier is null)
			throw new ArgumentNullException(nameof(identifier));
		if(method_arguments is null)
			throw new ArgumentNullException(nameof(method_arguments));
		#endregion

		// Get argument argument types and instances.
		ResultInfo genericResult = Compiler.GetGenericArguments(location, generic_arguments, out genericTypes);
		if(genericResult.Failure) {
			member = null;
			argumentTypes = null;
			argumentInstances = null;
			return genericResult;
		}
		ResultInfo argumentResult = Compiler.GetMethodArguments(location, method_arguments, out argumentTypes, out argumentInstances);
		if(argumentResult.Failure) {
			member = null;
			return argumentResult;
		}

		// Get the method by best.
		if((member = type.FindBestMethod(location.Compiler, genericTypes, argumentTypes, identifier.GetText())).Definition is not IMethod) {

			// If no method was found, then return failure.

			// If a method with that name exists, give an error about no matching overflow.
			// Otherwise, give an error about no method with that name existing.

			bool exists = false;
			foreach(IMember m in type.Members) {
				if(m.Identifier == identifier.GetText()) {
					exists = true;
					break;
				}
			}

			if(exists) {
				string[] identifiers = new string[argumentTypes.Length];
				for(int i = 0; i < argumentTypes.Length; i++) identifiers[i] = argumentTypes[i].Identifier;
				return new ResultInfo(false, $"{location.GetLocation(identifier)}No matching overload for method '{identifier.GetText()}' in type '{type.Identifier}' " +
					$"with arguments: ({string.Join(", ", identifiers)}).");
			} else {
				return new ResultInfo(false, $"{location.GetLocation(identifier)}Method '{identifier.GetText()}' does not exist in type '{type.Identifier}'.");
			}

		}

		// Return success.
		return ResultInfo.DefaultSuccess;

	}

	public static ResultInfo InvokeBestMethodFromContext(this IType type, Compiler.CompileArguments location, ITerminalNode identifier,
	GenericArgumentsContext generic_arguments, MethodArgumentsContext method_arguments, out IInstance value) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		if(identifier is null)
			throw new ArgumentNullException(nameof(identifier));
		if(method_arguments is null)
			throw new ArgumentNullException(nameof(method_arguments));
		#endregion

		// Find the best method.
		ResultInfo searchResult = type.FindBestMethodFromContext(location, identifier, generic_arguments, method_arguments,
			out IMember member, out IType[] genericTypes, out IType[] argumentTypes, out IInstance[] argumentInstances);
		if(searchResult.Failure) {
			value = null;
			return searchResult;
		}
		IMethod method = member.Definition as IMethod;

		// Invoke method.
		ResultInfo invokeResult = method.Invoker.Invoke(location, genericTypes, argumentInstances, out value);
		if(invokeResult.Failure) return location.GetLocation(identifier) + invokeResult;

		return ResultInfo.DefaultSuccess;

	}

	/// <summary>
	///
	/// </summary>
	/// <param name="type"></param>
	/// <param name="name"></param>
	/// <returns></returns>
	public static IMember FindPropertyOrField(this IType type, string name) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		if(string.IsNullOrEmpty(name))
			throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
		#endregion

		// Iterate over all members in this type.
		foreach(IMember member in type.Members) {
			if(member.MemberType != MemberType.Property && member.MemberType != MemberType.Field) continue;
			if(member.Identifier != name) continue;
			return member;
		}

		// Iterate over all members in base types.
		foreach(IType baseType in type.BaseTypes) {
			IMember member = baseType.FindPropertyOrField(name);
			if(member != null) return member;
		}

		// No member found.
		return null;

	}

	public static ResultInfo InvokePropertyOrFieldFromContext(this IType type, Compiler.CompileArguments location, ITerminalNode identifier, IMember propertyOrField, out IInstance value) {

		#region Argument Checks
		if(type is null)
			throw new ArgumentNullException(nameof(type));
		if(identifier is null)
			throw new ArgumentNullException(nameof(identifier));
		if(propertyOrField is null)
			throw new ArgumentNullException(nameof(propertyOrField));
		#endregion

		IMemberDefinition definition = propertyOrField.Definition;

		if(definition is IProperty property) {

			// Invoke property.
			// Property is static, so no instance is needed.

			// Get getter.
			var getter = property.Getter;
			if(getter == null) {
				value = null;
				return new ResultInfo(false, location.GetLocation(identifier) + "This property is not get-able.");
			}

			// Invoke 'get' method.
			ResultInfo result = property.Getter.Invoke(location, Array.Empty<IType>(), Array.Empty<IInstance>(), out value);
			if(result.Failure) return location.GetLocation(identifier) + result;

			// Return success.
			return ResultInfo.DefaultSuccess;

		} else if(definition is IField field) {

			// Invoke field.
			// Field is static, so no instance is needed.

			// Get static field value.
			value = type.StaticFieldInstances[field];

			// Return success.
			return ResultInfo.DefaultSuccess;

		} else {
			throw new ArgumentOutOfRangeException(nameof(propertyOrField), $"Member definition is not a property or field.");
		}

	}

	/// <summary>
	/// Determines if <paramref name="type1"/> is directly assignable from <paramref name="type2"/>.
	/// </summary>
	/// <param name="type1"></param>
	/// <param name="type2"></param>
	/// <returns></returns>
	public static bool IsAssignableFrom(this IType type1, IType type2) {

		#region Argument Checks
		if(type1 is null)
			throw new ArgumentNullException(nameof(type1));
		if(type2 is null)
			throw new ArgumentNullException(nameof(type2));
		#endregion

		// Check if the types are the same.
		if(type1 == type2) return true;

		// Check if type2 inherits from type1 using EnumerateBaseTypeTree.
		foreach(IType type in type2.EnumerateBaseTypeTree()) {
			if(type == type1) return true;
		}

		// Not assignable.
		return false;

	}

}
