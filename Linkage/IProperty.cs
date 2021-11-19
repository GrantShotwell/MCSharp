using MCSharp.Linkage.Minecraft;

namespace MCSharp.Linkage;

/// <summary>
/// Represents a property member definition.
/// </summary>
public interface IProperty : IMemberDefinition {

	/// <summary>
	/// The mcfunction file that will contain the commands to execute the 'getter' for the property.
	/// </summary>
	public IFunction Getter { get; }

	/// <summary>
	/// The mcfunction file that will contain the commands to execute the 'setter' for the property.
	/// </summary>
	public IFunction Setter { get; }

}
