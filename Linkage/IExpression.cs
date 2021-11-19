namespace MCSharp.Linkage;

/// <summary>
/// Represents an expression in code.
/// </summary>
public interface IExpression {

	public MCSharpParser.ExpressionContext Context { get; }

}
