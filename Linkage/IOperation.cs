using MCSharp.Linkage.Minecraft;

namespace MCSharp.Linkage;

public interface IOperation {

	/// <summary>
	/// The type of <see cref="Linkage.Operation"/> this is.
	/// </summary>
	public Operation Operation { get; }

	/// <summary>
	/// The <see cref="IFunction"/> used to invoke the operation.
	/// </summary>
	public IFunction Function { get; }

}
