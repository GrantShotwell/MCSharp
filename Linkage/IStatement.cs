using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	/// <summary>
	/// Represents a statement in code.
	/// </summary>
	public interface IStatement {

		public MCSharpParser.StatementContext Context { get; }

	}

}
