using MCSharp.Linkage.Minecraft;
using System;

namespace MCSharp.Linkage.Script;

public class ScriptOperation : IOperation {

	public Operation Operation { get; }

	public IFunction Function { get; }

	public ScriptOperation(Operation operation, IFunction function) {
		Operation = operation;
		Function = function ?? throw new ArgumentNullException(nameof(function));
	}

}
