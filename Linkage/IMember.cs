using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public interface IMember {

		public IType Declarer { get; }
		public Modifier Modifiers { get; }
		public string ReturnTypeIdentifier { get; }
		public string Identifier { get; }
		public MemberType MemberType { get; }
		public IMemberDefinition Definition { get; }

	}

}
