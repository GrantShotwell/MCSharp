using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Minecraft {

	/// <summary>
	/// Represents a <see cref="IFunction"/> that is defined by a list of <see cref="IStatement"/>s.
	/// </summary>
	public interface IStatementFunction : IFunction {

		/// <summary>
		/// The list of <see cref="IStatement"/>s that defines this <see cref="IFunction"/>.
		/// </summary>
		public IStatement[] Statements { get; }

	}

}
