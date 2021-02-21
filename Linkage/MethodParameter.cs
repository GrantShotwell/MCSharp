using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public class MethodParameter {

		public ITerminalNode TypeIdentifier { get; }
		public ITerminalNode Identifier { get; }


		public MethodParameter(MCSharpParser.Method_parameterContext context) {

			var names = context.NAME();
			TypeIdentifier = names[0];
			Identifier = names[1];

		}


		public static MethodParameter[] CreateArrayFromArray(MCSharpParser.Method_parameterContext[] contexts) {

			if(contexts == null) return new MethodParameter[] { };
			MethodParameter[] parameters = new MethodParameter[contexts.Length];

			for(int i = 0; i < contexts.Length; i++) {
				parameters[i] = new MethodParameter(contexts[i]);
			}

			return parameters;

		}

	}

}
