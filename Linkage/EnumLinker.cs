using System;

namespace MCSharp.Linkage;

/// <summary>
/// Returns various <see cref="Enum"/>s from <see cref="Antlr4.Runtime.ParserRuleContext"/>(s).
/// </summary>
public static class EnumLinker {

	/// <summary>
	/// Links the given <see cref="ModifierContext"/>s to their corresponding <see cref="Modifier"/>s.
	/// </summary>
	/// <param name="modifiers">A collection of <see cref="ModifierContext"/> taken from script.</param>
	/// <returns>Returns a <see cref="Modifier"/> value.</returns>
	public static Modifier LinkModifiers(ModifierContext[] modifiers) {

		Modifier mods = 0;

		foreach(ModifierContext modifier in modifiers) {

			if(modifier.PUBLIC() != null) mods |= Modifier.Public;
			else if(modifier.PRIVATE() != null) mods |= Modifier.Private;
			else if(modifier.PROTECTED() != null) mods |= Modifier.Protected;
			else if(modifier.STATIC() != null) mods |= Modifier.Static;
			else if(modifier.ABSTRACT() != null) mods |= Modifier.Abstract;
			else if(modifier.VIRTUAL() != null) mods |= Modifier.Virtual;

		}

		return mods;

	}

	/// <summary>
	/// Links the given <see cref="MCSharpParser.Class_typeContext"/> to its corresponding <see cref="ClassType"/>.
	/// </summary>
	/// <param name="type">A <see cref="MCSharpParser.Class_typeContext"/> taken from script.</param>
	/// <returns>Returns a <see cref="ClassType"/> value.</returns>
	public static ClassType LinkClassType(MCSharpParser.Class_typeContext type) {

		if(type.CLASS() != null) return ClassType.Class;
		else if(type.STRUCT() != null) return ClassType.Struct;
		else return 0;

	}

	/// <summary>
	/// Links the given <see cref="MCSharpParser.Member_definitionContext"/> to its corresponding <see cref="MemberType"/>.
	/// </summary>
	/// <param name="definition">A <see cref="MCSharpParser.Member_definitionContext"/> taken from script.</param>
	/// <returns>Returns a <see cref="MemberType"/> value.</returns>
	public static MemberType LinkMemberType(MCSharpParser.Member_definitionContext definition) {

		if(definition.field_definition() != null) return MemberType.Field;
		else if(definition.property_definition() != null) return MemberType.Property;
		else if(definition.method_definition() != null) return MemberType.Method;
		else return 0;

	}

}
