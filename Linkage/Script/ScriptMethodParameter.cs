using Antlr4.Runtime.Tree;
using MCSharp.Compilation.Instancing;

namespace MCSharp.Linkage.Script;

public class ScriptMethodParameter : IMethodParameter {

	/// <summary>
	/// 
	/// </summary>
	public ITerminalNode TypeIdentifier { get; }
	/// <inheritdoc/>
	string IMethodParameter.TypeIdentifier => TypeIdentifier.GetText();

	/// <summary>
	/// 
	/// </summary>
	public ITerminalNode Identifier { get; }
	/// <inheritdoc/>
	string IMethodParameter.Identifier => Identifier.GetText();

	public IInstance Instance { get; set; }

	public ScriptMethodParameter(MCSharpParser.Method_parameterContext context) {

		var names = context.NAME();
		TypeIdentifier = names[0];
		Identifier = names[1];

	}

	/// <summary>
	/// Creates an array of <see cref="ScriptMethodParameter"/>s using the individual elements of <paramref name="contexts"/> as arguments for constructors.
	/// </summary>
	/// <param name="contexts">The collection of <see cref="MCSharpParser.Method_parameterContext"/>s to convert into <see cref="ScriptMethodParameter"/>s.</param>
	/// <returns>Returns an array of <see cref="ScriptMethodParameter"/></returns>
	public static ScriptMethodParameter[] CreateArrayFromArray(MCSharpParser.Method_parameterContext[] contexts) {

		if(contexts == null) return System.Array.Empty<ScriptMethodParameter>();
		var parameters = new ScriptMethodParameter[contexts.Length];

		for(int i = 0; i < contexts.Length; i++) {
			parameters[i] = new ScriptMethodParameter(contexts[i]);
		}

		return parameters;

	}

}
