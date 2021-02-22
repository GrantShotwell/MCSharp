using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public class GenericParameter {

		public ITerminalNode TypeIdentifier { get; }


		public GenericParameter(MCSharpParser.Generic_parameterContext context) {

			TypeIdentifier = context.NAME();

		}


		public static GenericParameter[] CreateArrayFromArray(MCSharpParser.Generic_parameterContext[] contexts) {

			if(contexts == null) return new GenericParameter[] { };
			GenericParameter[] parameters = new GenericParameter[contexts.Length];

			for(int i = 0; i < contexts.Length; i++) {
				parameters[i] = new GenericParameter(contexts[i]);
			}

			return parameters;

		}

	}

}
