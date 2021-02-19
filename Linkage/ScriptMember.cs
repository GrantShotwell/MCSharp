using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation.Linkage {

	public class ScriptMember {

		public MCSharpParser.Member_definitionContext FullContext { get; }

		public Modifier Modifiers { get; }
		public MemberType MemberType { get; }

		public ScriptMember(MCSharpParser.Member_definitionContext context) {

			FullContext = context;

			Modifiers = EnumLinker.LinkModifiers(context.modifier());

			MemberType = EnumLinker.LinkMemberType(context);

		}

	}

}
