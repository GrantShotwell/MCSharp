using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation.Linkage {

	public class ScriptClass {

		public Modifier Modifiers { get; }
		public ClassType ClassType { get; }
		public ScriptMember[] Members { get; }
		public ScriptClass[] SubTypes { get; }

		public ScriptClass(MCSharpParser.Type_definitionContext typeContext, MCSharpParser.Member_definitionContext[] memberContexts) {

			Modifiers = EnumLinker.LinkModifiers(typeContext.modifier());

			ClassType = EnumLinker.LinkClassType(typeContext.class_type());

			Members = new ScriptMember[memberContexts.Length];
			for(int i = 0; i < memberContexts.Length; i++) {
				Members[i] = new ScriptMember(memberContexts[i]);
			}

			// Planned feature.
			SubTypes = null;

		}

	}

}
