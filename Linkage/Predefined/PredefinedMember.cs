using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Predefined {

	public class PredefinedMember : IMember {

		public PredefinedType Declarer { get; }
		IType IMember.Declarer => Declarer;
		public Modifier Modifiers { get; }
		public string ReturnTypeIdentifier { get; }
		public string Identifier { get; }
		public MemberType MemberType { get; }
		public IMemberDefinition Definition { get; }

		public PredefinedMember(PredefinedType declarer, Modifier modifiers, string returnTypeIdentifier, string identifier, MemberType memberType, PredefinedMemberDefinition definition) {
			Declarer = declarer ?? throw new ArgumentNullException(nameof(declarer));
			Modifiers = modifiers;
			ReturnTypeIdentifier = returnTypeIdentifier ?? throw new ArgumentNullException(nameof(returnTypeIdentifier));
			Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
			MemberType = memberType;
			Definition = definition ?? throw new ArgumentNullException(nameof(definition));
		}

	}

}
