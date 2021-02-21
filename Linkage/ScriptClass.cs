using Antlr4.Runtime.Tree;
using MCSharp.Compilation;
using MCSharp.Compilation.Linkage;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public class ScriptClass {

		public Modifier Modifiers { get; }
		public ClassType ClassType { get; }
		public ITerminalNode Identifier { get; }
		public ScriptMember[] Members { get; }
		public ScriptClass[] SubTypes { get; }

		public ScriptClass(MCSharpParser.Type_definitionContext typeContext, MCSharpParser.Member_definitionContext[] memberContexts, Settings settings, VirtualMachine virtualMachine) {

			Modifiers = EnumLinker.LinkModifiers(typeContext.modifier());

			ClassType = EnumLinker.LinkClassType(typeContext.class_type());

			Identifier = typeContext.NAME();

			Members = new ScriptMember[memberContexts.Length];
			for(int i = 0; i < memberContexts.Length; i++) {
				Members[i] = new ScriptMember(this, memberContexts[i], settings, virtualMachine);
			}

			// Planned feature.
			SubTypes = null;

		}

	}

}
