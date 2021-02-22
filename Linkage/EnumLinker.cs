using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public static class EnumLinker {

		public static Modifier LinkModifiers(MCSharpParser.ModifierContext[] modifiers) {

			Modifier mods = 0;

			foreach(MCSharpParser.ModifierContext modifier in modifiers) {

				if(modifier.PUBLIC() != null) mods |= Modifier.Public;
				else if(modifier.PRIVATE() != null) mods |= Modifier.Private;
				else if(modifier.PROTECTED() != null) mods |= Modifier.Protected;
				else if(modifier.STATIC() != null) mods |= Modifier.Static;
				else if(modifier.ABSTRACT() != null) mods |= Modifier.Abstract;
				else if(modifier.VIRTUAL() != null) mods |= Modifier.Virtual;

			}

			return mods;

		}

		public static ClassType LinkClassType(MCSharpParser.Class_typeContext type) {

			if(type.CLASS() != null) return ClassType.Class;
			else if(type.STRUCT() != null) return ClassType.Struct;
			else return 0;

		}

		public static MemberType LinkMemberType(MCSharpParser.Member_definitionContext definition) {

			if(definition.field_definition() != null) return MemberType.Field;
			else if(definition.property_definition() != null) return MemberType.Property;
			else if(definition.method_definition() != null) return MemberType.Method;
			else return 0;

		}

		public static bool CheckStandardModifierRules(Modifier modifiers) {
			throw new NotImplementedException();
		}

	}

}
