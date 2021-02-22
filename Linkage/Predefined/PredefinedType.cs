using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Antlr4.Runtime.Tree;

namespace MCSharp.Linkage.Predefined {

	public class PredefinedType : IType {

		public Modifier Modifiers { get; }
		public ClassType ClassType { get; }
		public string Identifier { get; }
		public PredefinedMember[] Members { get; }
		IMember[] IType.Members => Members;
		public PredefinedType[] SubTypes { get; }
		IType[] IType.SubTypes => SubTypes;

		public PredefinedType(Modifier modifiers, ClassType classType, string identifier, PredefinedMember[] members, PredefinedType[] subTypes) {
			Modifiers = modifiers;
			ClassType = classType;
			Identifier = identifier;
			Members = members;
			SubTypes = subTypes;
		}

	}

}
