using Antlr4.Runtime.Tree;

namespace MCSharp.Linkage.Script {

	public class ScriptGenericParameter : IGenericParameter {

		public ITerminalNode TypeIdentifier { get; }
		string IGenericParameter.TypeIdentifier => TypeIdentifier.GetText();

		public ScriptGenericParameter(MCSharpParser.Generic_parameterContext context) {

			TypeIdentifier = context.NAME();

		}


		/// <summary>
		/// Creates an array of <see cref="ScriptGenericParameter"/>s using the individual elements of <paramref name="contexts"/> as arguments for constructors.
		/// </summary>
		/// <param name="contexts">The collection of <see cref="MCSharpParser.Generic_parameterContext"/>s to convert into <see cref="ScriptMethodParameter"/>s.</param>
		/// <returns>Returns an array of <see cref="ScriptGenericParameter"/></returns>
		public static ScriptGenericParameter[] CreateArrayFromArray(MCSharpParser.Generic_parameterContext[] contexts) {

			if(contexts == null) return new ScriptGenericParameter[] { };
			var parameters = new ScriptGenericParameter[contexts.Length];

			for(int i = 0; i < contexts.Length; i++) {
				parameters[i] = new ScriptGenericParameter(contexts[i]);
			}

			return parameters;

		}

	}

}
