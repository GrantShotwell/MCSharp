using MCSharp.Collections;
using MCSharp.Compilation;
using MCSharp.Compilation.Instancing;
using System;
using System.Collections.Generic;

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
		public static IEnumerable<IType> EnumerateBasetypeTree(this IType type) {

			foreach(IType baseType in type.BaseTypes) {
				yield return baseType;
				foreach(IType enumerated in baseType.EnumerateBasetypeTree()) {
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

	}

}
