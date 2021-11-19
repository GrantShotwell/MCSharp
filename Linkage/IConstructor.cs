using MCSharp.Linkage.Minecraft;
using System;

namespace MCSharp.Linkage;

/// <summary>
/// Represents a constructor for some type.
/// </summary>
public interface IConstructor : IDisposable {

	/// <summary>
	/// The type that has defined this constructor.
	/// </summary>
	public IType Declarer { get; }

	/// <summary>
	/// The modifiers that affect this constructor.
	/// </summary>
	public Modifier Modifiers { get; }

	/// <summary>
	/// The mcfunction file that will contain the final commands to execute this constructor.
	/// </summary>
	public IFunction Invoker { get; }

}
