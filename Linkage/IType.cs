using MCSharp.Collections;
using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MCSharp.Linkage {

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

	public static class ITypeExtensions {

		/// <summary>
		/// Enumerates through all values in <see cref="IType.SubTypes"/> recursively, starting from direct children of <paramref name="type"/>.
		/// </summary>
		/// <param name="type">The <see cref="IType"/> to enumerate from.</param>
		/// <returns>Returns an <see cref="IEnumerable{IType}"/> created by '<see langword="yield"/>-ing' over <see cref="IType.SubTypes"/>'s <see cref="IEnumerator{IType}"/>.</returns>
		public static IEnumerable<IType> EnumerateSubtypeTree(this IType type) {

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

			int count = 0;

			// Base cases.
			if(type.ClassType == ClassType.Class) return 1;
			if(type.Identifier == MCSharpLinkerExtension.IntIdentifier) return 1;
			if(type.Identifier == MCSharpLinkerExtension.BoolIdentifier) return 1;

			// Count field sizes.
			foreach(var member in type.Members) {
				if(member.MemberType != MemberType.Field) continue;
				var memberType = compiler.DefinedTypes[member.ReturnTypeIdentifier];
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

			int count = 0;

			// Sum every member's size.
			foreach(var member in type.Members) {
				if(member.MemberType != MemberType.Field) continue;
				var memberType = compiler.DefinedTypes[member.ReturnTypeIdentifier];
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
		public static IConstructor FindBestConstructor(this IType type, IType[] genericArguments, IInstance[] methodArguments) {

			IType[] types = new IType[methodArguments.Length];
			for(int i = 0; i < types.Length; i++)
				types[i] = methodArguments[i].Type;
			return type.FindBestConstructor(genericArguments, types);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="genericArguments"></param>
		/// <param name="methodArguments"></param>
		public static IConstructor FindBestConstructor(this IType type, IType[] genericArguments, IType[] methodArguments) {

			(int Score, IConstructor Constructor) best = (-1, null);
			foreach(IConstructor constructor in type.Constructors) {

				Minecraft.IFunction invoker = constructor.Invoker;
				var methodParameters = invoker.MethodParameters;
				var genericParameters = invoker.GenericParameters;

				int genericCount = genericParameters.Count;
				if(genericCount != genericArguments.Length) continue;

				int methodCount = methodParameters.Count;
				if(methodCount != methodArguments.Length) continue;

				int score = methodCount;

				for(int i = 0; i < genericCount; i++) {
					// TODO: Test if generics match.
				}

				for(int i = 0; i < methodCount; i++) {
					// TODO: Test if arguments match.
				}

				if(score > best.Score) best = (score, constructor);
				else continue;

			}

			return best.Constructor;

		}

		/// <summary>
		/// Determines if <paramref name="type1"/> is directly assignable from <paramref name="type2"/>.
		/// </summary>
		/// <param name="type1"></param>
		/// <param name="type2"></param>
		/// <returns></returns>
		public static bool IsAssignableFrom(this IType type1, IType type2) {

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

}
