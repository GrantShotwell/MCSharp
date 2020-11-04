using MCSharp.GameJson.Nbt;
using MCSharp.GameSerialization.Text;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MCSharp.GameSerialization.Selector {

	public class SelectorData {

		public char Variable { get; set; }
		public (float? Argument, bool Inverted) X { get; set; }
		public (float? Argument, bool Inverted) Y { get; set; }
		public (float? Argument, bool Inverted) Z { get; set; }
		public (float? Argument, bool Inverted) Distance { get; set; }
		public (float? Argument, bool Inverted) R { get; set; }
		public (float? Argument, bool Inverted) Rm { get; set; }
		public (float? Argument, bool Inverted) Dx { get; set; }
		public (float? Argument, bool Inverted) Dy { get; set; }
		public (float? Argument, bool Inverted) Dz { get; set; }
		public (ScoreData[] Argument, bool Inverted) Scores { get; set; }
		public (string Argument, bool Inverted) Team { get; set; }
		public (int? Argument, bool Inverted) Limit { get; set; }
		public (string Argument, bool Inverted) Sort { get; set; }
		public (int? Argument, bool Inverted) Level { get; set; }
		public (string Argument, bool Inverted) Gamemode { get; set; }
		public (string Argument, bool Inverted) Name { get; set; }
		public (string Argument, bool Inverted) Type { get; set; }
		public (NbtData Argument, bool Inverted) Nbt { get; set; }
		public (string Argument, bool Inverted) Predicate { get; set; }

		/*
		 * TODO:
		 *  - x_rotation
		 *  - y_rotation
		 *  - advancements
		 */

		public SelectorData(char variable) => Variable = variable;

		public SelectorData(string selector) {
			throw new NotImplementedException();
		}

		public string GetSelector() {

			PropertyInfo[] properties = GetType().GetProperties();
			var data = new List<string>(properties.Length);
			foreach(PropertyInfo info in properties) {

				Type type = info.PropertyType;
				string name = RawText.SerializeNamingPolicy.ConvertName(info.Name);

				if(type.IsAssignableFrom(typeof((int? Argument, bool Inverted)))) {

					// 'int?' Property
					(int? Argument, bool Inverted)? property = info.GetValue(this) as (int? Argument, bool Inverted)?;
					if(property.HasValue) {
						int? argument = property.Value.Argument;
						bool inverted = property.Value.Inverted;
						string equals = inverted ? "=!" : "=";
						if(inverted || argument != null) data.Add($"{name}{equals}{argument?.ToString() ?? ""}");
					}

				} else if(type.IsAssignableFrom(typeof((float? Argument, bool Inverted)))) {

					// 'float?' Property
					(float? Argument, bool Inverted)? property = info.GetValue(this) as (float? Argument, bool Inverted)?;
					if(property.HasValue) {
						float? argument = property.Value.Argument;
						bool inverted = property.Value.Inverted;
						string equals = property.Value.Inverted ? "=!" : "=";
						if(inverted || argument != null) data.Add($"{name}{equals}{argument?.ToString() ?? ""}");
					}

				} else if(type.IsAssignableFrom(typeof((string Argument, bool Inverted)))) {

					// 'string' Property
					(string Argument, bool Inverted)? property = info.GetValue(this) as (string Argument, bool Inverted)?;
					if(property.HasValue) {
						string argument = property.Value.Argument;
						bool inverted = property.Value.Inverted;
						string equals = property.Value.Inverted ? "=!" : "=";
						if(inverted || argument != null) data.Add($"{name}{equals}{argument?.ToString() ?? ""}");
					}

				} else if(type.IsAssignableFrom(typeof((ScoreData[] Argument, bool Inverted)))) {

					// 'ScoreData[]' Property
					(ScoreData[] Argument, bool Inverted)? property = info.GetValue(this) as (ScoreData[] Argument, bool Inverted)?;
					if(property.HasValue) {
						ScoreData[] argument = property.Value.Argument;
						bool inverted = property.Value.Inverted;
						string equals = property.Value.Inverted ? "=!" : "=";
						if(inverted || argument != null) {
							if(argument == null) data.Add($"{name}{equals}");
							else data.Add($"{name}{equals}[{string.Join<ScoreData>(',', argument)}]");
						}
					}

				} else if(type.IsAssignableFrom(typeof((NbtData Argument, bool Inverted)))) {

					// 'NbtData' Property
					(NbtData Argument, bool Inverted)? property = info.GetValue(this) as (NbtData Argument, bool Inverted)?;
					if(property.HasValue) {
						// TODO
						continue;
					}

				}

			}

			return $"@{Variable}[{string.Join(',', data)}]";
		}

		public override string ToString() => GetSelector();

	}

}
