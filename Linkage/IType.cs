using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public interface IType {

		public Modifier Modifiers { get; }
		public ClassType ClassType { get; }
		public string Identifier { get; }
		public IMember[] Members { get; }
		public IType[] SubTypes { get; }

	}

}
