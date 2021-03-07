using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Minecraft {

	/// <summary>
	/// Represents in-game code with arguments and a returnable value.
	/// </summary>
	/// <seealso cref="IStatementFunction"/>
	/// <seealso cref="ICustomFunction"/>
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

	}

}
