namespace MCSharp.Linkage;

/// <summary>
/// Represents a field member definition.
/// </summary>
public interface IField : IMemberDefinition {

	/// <summary>
	/// The expression to evaluate to set the initial value for this field.
	/// </summary>
	public IExpression Initializer { get; }

}
