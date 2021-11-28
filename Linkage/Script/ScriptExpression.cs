using System;

namespace MCSharp.Linkage.Script;

/// <summary>
/// Represents an expression defined from script.
/// </summary>
public class ScriptExpression : IExpression {

	public ExpressionContext Context { get; }

	public ScriptExpression(ExpressionContext context) {
		Context = context ?? throw new ArgumentNullException(nameof(context));
	}

	/// <summary>
	/// Creates an array of <see cref="ScriptExpression"/>s using the individual elements of <paramref name="contexts"/> as arguments for constructors.
	/// </summary>
	/// <param name="contexts">The collection of <see cref="ExpressionContext"/>s to convert into <see cref="ScriptExpression"/>s.</param>
	/// <returns>Returns an array of <see cref="ScriptExpression"/></returns>
	public ScriptExpression[] CreateArrayFromArray(ExpressionContext[] contexts) {

		int size = contexts.Length;
		ScriptExpression[] expressions = new ScriptExpression[size];
		for(int i = 0; i < size; i++) expressions[i] = new ScriptExpression(contexts[i]);
		return expressions;

	}

}
