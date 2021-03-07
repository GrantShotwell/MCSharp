using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation.Instancing {

	/// <summary>
	/// Represents an <see cref="IInstance"/> with a constant value of an unknown type.
	/// </summary>
	public interface IConstantInstance : IInstance {

		/// <summary>
		/// The value of this constant.
		/// </summary>
		public object Value { get; }

	}

	/// <summary>
	/// Represents an <see cref="IInstance"/> with a constant value of a known type.
	/// </summary>
	/// <typeparam name="TValue">Represents the value this <see cref="IConstantInstance"/> stores.</typeparam>
	public interface IConstantInstance<TValue> : IConstantInstance {

		/// <summary>
		/// The value of this constant.
		/// </summary>
		public new TValue Value { get; }

	} 

}
