﻿using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	/// <summary>
	/// Represents a custom type conversion operation.
	/// </summary>
	public interface IConversion {

		/// <summary>
		/// Wether or not this <see cref="IConversion"/> is defined as being explicit.
		/// </summary>
		/// <remarks>Returns the opposite of <see cref="Implicit"/>.</remarks>
		public bool Explicit { get; }

		/// <summary>
		/// Wether or not this <see cref="IConversion"/> is defined as being implicit.
		/// </summary>
		/// <remarks>Returns the opposite of <see cref="Explicit"/>.</remarks>
		public bool Implicit => !Explicit;

		/// <summary>
		/// The <see cref="IType"/> of <see cref="IInstance"/>s that will be cast to <see cref="ToType"/>.
		/// </summary>
		public IType FromType { get; }

		/// <summary>
		/// The <see cref="IType"/> of <see cref="IInstance"/>s that will be cast from <see cref="FromType"/>.
		/// </summary>
		public IType ToType { get; }

		/// <summary>
		/// The <see cref="IFunction"/> used to invoke the conversion.
		/// </summary>
		public IFunction Function { get; }

	}

}
