using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCSharp {

	public static class NamingStyleConverter {

		/// <summary>Converts 'camelCase' to 'PascalCase'.</summary>
		public static string FromCamelToPascal(string camel) {
			if(camel == string.Empty) return camel;
			else {
				char[] chars = camel.ToCharArray();
				chars[0] = char.ToUpper(chars[0]);
				return new string(chars);
			}
		}

		/// <summary>Converts a single word to 'PascalCase'.</summary>
		public static string FromSingleToPascal(string single) {
			if(single == string.Empty) return single;
			else return FromCamelToPascal(single);
		}

		/// <summary>Converts 'PascalCase' to 'camelCase'.</summary>
		public static string FromPascalToCamel(string pascal) {
			if(pascal == string.Empty) return pascal;
			else {
				char[] chars = pascal.ToCharArray();
				chars[0] = char.ToLower(chars[0]);
				return new string(chars);
			}
		}

		/// <summary>Converts a single word to 'camelCase'.</summary>
		public static string FromSingleToCamel(string single) {
			if(single == string.Empty) return single;
			else return FromPascalToCamel(single);
		}

		/// <summary>Converts 'PascalCase' to 'snake_case'.</summary>
		public static string FromPascalToSnake(string pascal) {
			if(pascal == string.Empty) return pascal;
			else {
				string camel = FromPascalToCamel(pascal);
				return FromCamelToSnake(camel);
			}
		}

		/// <summary>Converts 'camelCase' to 'snake_case'.</summary>
		public static string FromCamelToSnake(string camel) {
			if(camel == string.Empty) return camel;
			else {
				LinkedList<char> chars = new LinkedList<char>(camel);
				LinkedListNode<char> current = chars.First;
				while(current != null) {
					if(char.IsUpper(current.Value)) {
						current.Value = char.ToLower(current.Value);
						chars.AddBefore(current, '_');
					}
					current = current.Next;
				}
				return new string(chars.ToArray());
			}
		}

		/// <summary>Converts 'snake_case' to 'PascalCase'.</summary>
		public static string FromSnakeToPascal(string snake) {
			if(snake == string.Empty) return snake;
			else {
				LinkedList<char> chars = new LinkedList<char>(snake);
				LinkedListNode<char> current = chars.First, space = null;
				while(current != null) {
					if(space == null) {
						if(current.Value == '_') space = current;
					} else {
						current.Value = char.ToUpper(current.Value);
						chars.Remove(space);
						space = null;
					}
					current = current.Next;
				}
				return new string(chars.ToArray());
			}
		}

		/// <summary>Converts 'snake_case' to 'camelCase'.</summary>
		public static string FromSnakeToCamel(string snake) {
			if(snake == string.Empty) return snake;
			else {
				string pascal = FromSnakeToPascal(snake);
				return FromPascalToCamel(pascal);
			}
		}

	}

}
