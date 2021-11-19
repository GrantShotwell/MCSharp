namespace MCSharp.Linkage;

/// <summary>
/// Represents a member type (field, property, method).
/// </summary>
public enum MemberType {
	/// <summary>Fields are values saved within a type instance.</summary>
	Field = 0b001,
	/// <summary>Properties are abstractions of methods into 'get' and 'set' to be accessed/modified like fields in code.</summary>
	Property = 0b010,
	/// <summary>Methods are blocks of code that can be executed with arguments and return a value.</summary>
	Method = 0b100
}
