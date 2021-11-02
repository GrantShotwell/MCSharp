using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections;
using System.Collections.Generic;

namespace MCSharp.Compilation.Instancing {

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
		/// <seealso cref="SaveToBlock(IInstance, Compiler.CompileArguments, Objective[])"/>
		public void SaveToBlock(Compiler.CompileArguments location, string selector, Objective[] block, Range range);

	}

	public static class IInstanceExtensions {

		public static ResultInfo ConvertType(this IInstance instance, Compiler.CompileArguments location, IType toType, out IInstance value) {

			IType fromType = instance.Type;
			IDictionary<IType, IConversion> conversions = fromType.Conversions;

			if(conversions.ContainsKey(toType)) {

				var conversion = conversions[toType];
				ResultInfo conversionResult = conversion.Function.Invoke(location, new IType[] { }, new IInstance[] { instance }, out value);
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

	}

}
