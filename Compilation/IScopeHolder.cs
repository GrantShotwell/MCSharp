using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation {

	/// <summary>
	/// Represents something that can hold a <see cref="Scope"/>.
	/// </summary>
	public interface IScopeHolder {

		/// <summary>
		/// The <see cref="Scope"/> held by this <see cref="IScopeHolder"/>.
		/// </summary>
		public Scope Scope { get; }

	}

}
