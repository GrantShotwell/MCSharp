using MCSharp.Linkage.Minecraft;

namespace MCSharp.Linkage;

/// <summary>
/// Represents a method member definition.
/// </summary>
public interface IMethod : IMemberDefinition {

	/// <summary>
	/// The mcfunction file that will contain the final commands to execute this method.
	/// </summary>
	public IFunction Invoker { get; }

}
