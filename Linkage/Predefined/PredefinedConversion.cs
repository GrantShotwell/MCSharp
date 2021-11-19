using MCSharp.Linkage.Minecraft;
using System;

namespace MCSharp.Linkage.Predefined;

/// <summary>
/// Represents a predefined constructor.
/// </summary>
public class PredefinedConversion : IConversion {

	/// <inheritdoc/>
	public bool Explicit { get; }

	/// <inheritdoc/>
	public IType ReferenceType { get; }

	/// <inheritdoc/>
	public IType TargetType { get; }

	/// <inheritdoc/>
	public IFunction Function { get; }

	/// <summary>
	/// Creates a new instance of <see cref="PredefinedConversion"/>.
	/// </summary>
	/// <param name="referenceType">The reference type.</param>
	/// <param name="targetType">The target type.</param>
	/// <param name="function">The function.</param>
	/// <param name="implicit">Whether the conversion is explicit.</param>
	public PredefinedConversion(IType referenceType, IType targetType, IFunction function, bool @implicit = false) {
		ReferenceType = referenceType ?? throw new ArgumentNullException(nameof(referenceType));
		TargetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
		Function = function ?? throw new ArgumentNullException(nameof(function));
		Explicit = @implicit;
	}

}
