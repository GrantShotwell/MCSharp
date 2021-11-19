namespace MCSharp.Linkage;

/// <summary>
/// Represents a collection of modifiers for types/members.
/// </summary>
public enum Modifier {
	/// <summary> "public" </summary>
	Public = 0b0000001,
	/// <summary> "private" </summary>
	Private = 0b0000010,
	/// <summary> "protected" </summary>
	Protected = 0b0000100,
	/// <summary> "static" </summary>
	Static = 0b0001000,
	/// <summary> "abstract" </summary>
	Abstract = 0b0010000,
	/// <summary> "virtual" </summary>
	Virtual = 0b0100000,
	/// <summary> "override" </summary>
	Override = 0b1000000
}
