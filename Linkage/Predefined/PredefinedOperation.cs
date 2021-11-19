using MCSharp.Linkage.Minecraft;
using System;

namespace MCSharp.Linkage.Predefined;

public class PredefinedOperation : IOperation {

	public Operation Operation { get; }

	public IFunction Function { get; }

	public PredefinedOperation(Operation operation, IFunction function) {
		Operation = operation;
		Function = function ?? throw new ArgumentNullException(nameof(function));
	}

}
