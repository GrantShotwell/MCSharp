using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Converters {

	public static class NameStyleConverter {

		/// <summary>
		/// "folder\exampleText" → "folder\example_text"
		/// </summary>
		/// <param name="camel">A string in pascal case.</param>
		/// <returns>Returns a string in snake case.</returns>
		public static string CamelToSnake(string camel) => PascalToSnake(camel);

		/// <summary>
		/// "Folder\ExampleText" → "folder\example_text"
		/// </summary>
		/// <param name="pascal">A string in pascal case.</param>
		/// <returns>Returns a string in snake case.</returns>
		public static string PascalToSnake(string pascal) {

			string[] words = new string[pascal.Length];
			for(int i = 0; i < words.Length; i++) words[i] = null;
			char[] current = new char[pascal.Length];
			for(int i = 0; i < current.Length; i++) current[i] = (char)0;

			int wordCount = 0, charCount = 0;
			char lastChar = (char)0;
			for(int i = 0; i < pascal.Length; i++) {
				char c = pascal[i];

				if(char.IsUpper(c) && lastChar != '\\' && lastChar != '/') {
					// Start new word.
					if(current[0] != (char)0) {
						// Add current word.
						words[wordCount++] = new string(current[0..charCount]);
					}
					charCount = 0;
				}

				// Add character to current word.
				current[charCount++] = c;

				// Add last word.
				if(i >= pascal.Length - 1) {
					words[wordCount++] = new string(current[0..charCount]);
					charCount = 0;
				}

			}

			string[] new_words = new string[wordCount];
			for(int i = 0; i < wordCount; i++) {
				new_words[i] = words[i].ToLower();
			}

			string snake = string.Join('_', new_words);
			return snake;

		}

	}

}
