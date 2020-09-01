using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MCSharp.GameJSON.Text {

	public class RawText {

		#region Plain Text
		/// <summary>A string containing plain text to display directly. Can also be a number or boolean that is displayed directly.</summary>
		public string Text { get; set; }
		#endregion
		#region Translated Text
		/// <summary>A translation identifier, to be displayed as the corresponding text in the player's selected language.
		/// If no corresponding translation can be found, the identifier itself will be used as the translation text.
		/// This identifier is the same as the identifiers found in lang files from assets or resource packs.</summary>
		public string Translate { get; set; }
		#endregion
		#region Scoreboard Value
		/// <summary>Displays a score holder's current score in an objective.
		/// Displays nothing if the given score holder or the given objective do not exist, or if the score holder is not tracked in the objective.</summary>
		public Score Score { get; set; }
		#endregion
		#region Entity Names
		/// <summary>A string containing a selector. Displayed as the name of the player or entity found by the selector.
		/// If more than one player or entity is found by the selector, their names are displayed in either the form "Name1 and Name2" or the form "Name1, Name2, Name3, and Name4".
		/// Hovering over a name will show a tooltip with the name, type, and UUID of the target. Clicking a player's name suggests a command to whisper to that player.
		/// Shift-clicking a player's name inserts that name into chat. Shift-clicking a non-player entity's name inserts its UUID into chat.</summary>
		public string Selector { get; set; }
		#endregion
		#region Keybinds
		/// <summary>A keybind identifier, to be displayed as the name of the button that is currently bound to a certain action.
		/// For example, <c>key.inventory</c> will display "e" if the player is using the default control scheme.</summary>
		public string Keybind { get; set; }
		#endregion
		#region NBT Values
		/// <summary> The NBT path used for looking up NBT values from an entity, a block entity or an NBT storage.
		/// NBT strings display their contents. Other NBT values are displayed as SNBT with no spacing or linebreaks.
		/// How values are displayed depends on the value of  interpret.
		/// If more than one NBT value is found, either by selecting multiple entities or by using a multi-value path, they are displayed in the form "Value1, Value2, Value3, Value4".
		/// Requires one of block, entity, or storage. Having more than one is allowed, but only one will be used.</summary>
		public string Nbt { get; set; }
		/// <summary>Optional, defaults to false. If true, the game will try to parse the text of each NBT value as a raw JSON text component.
		/// This usually only works if the value is an NBT string containing JSON, since JSON and SNBT are not compatible.
		/// If parsing fails, displays nothing. Ignored if <see cref="Nbt"/> is not present.</summary>
		public bool? Interpret { get; set; }
		/// <summary>A string specifying the coordinates of the block entity from which the NBT value is obtained. The coordinates can be absolute or relative. Ignored if <see cref="Nbt"/> is not present.</summary>
		public string Block { get; set; }
		/// <summary>A string specifying the target selector for the entity or entities from which the NBT value is obtained. Ignored if <see cref="Nbt"/> is not present.</summary>
		public string Entity { get; set; }
		/// <summary>A string specifying the namespaced ID of the command storage from which the NBT value is obtained. Ignored if <see cref="Nbt"/> is not present.</summary>
		public string Storage { get; set; }
		#endregion
		#region Children
		/// <summary>A list of additional raw JSON text components to be displayed after this one.</summary>
		public RawText[] Extra { get; set; }
		#endregion
		#region Formatting
		/// <summary>Optional. The color to render the content in.</summary>
		public string Color { get; set; }
		/// <summary>Optional. The resource location of the font for this component in the resource pack within <c>assets/[namespace]/font</c>.
		/// Defaults to <c>minecraft:default</c>.</summary>
		public string Font { get; set; }
		/// <summary>Optional. Whether to render the content in bold.</summary>
		public bool? Bold { get; set; }
		/// <summary>Optional. Whether to render the content in italics.
		/// Note that text which is italicized by default, such as custom item names, can be unitalicized by setting this to false.</summary>
		public bool? Italic { get; set; }
		/// <summary>Optional. Whether to underline the content.</summary>
		public bool? Underlined { get; set; }
		/// <summary>Optional. Whether to strikethrough the content.</summary>
		public bool? Strikethrough { get; set; }
		/// <summary>Optional. Whether to render the content obfuscated.</summary>
		public bool? Obfuscated { get; set; }
		#endregion
		#region Interactivity
		/// <summary>Optional. When the text is shift-clicked by a player, this string is inserted in their chat input.
		/// It does not overwrite any existing text the player was writing. This only works in chat messages.</summary>
		public string Insertion { get; set; }
		/// <summary>Optional. Allows for events to occur when the player clicks on text. Only work in chat messages and written books, unless specified otherwise.</summary>
		public ClickEvent ClickEvent { get; set; }
		/// <summary>Optional. Allows for a tooltip to be displayed when the player hovers their mouse over text.</summary>
		public HoverEvent HoverEvent { get; set; }
		#endregion


		public static RawText FromJson(string json) => JsonSerializer.Deserialize<RawText>(json, DeserializerOptions);
		public string GetJson() {
			string json = JsonSerializer.Serialize(this, SerializerOptions);
			return json;
		}

		private static JsonSerializerOptions serializerOptions = null;
		public static JsonNamingPolicy SerializeNamingPolicy { get; } = new MinecraftNamingPolicy();
		public static JsonSerializerOptions SerializerOptions {
			get {
				if(serializerOptions is null) {
					serializerOptions = new JsonSerializerOptions() {
						PropertyNamingPolicy = SerializeNamingPolicy,
						IgnoreNullValues = true,
						WriteIndented = false
					};
					serializerOptions.Converters.Add(NullableBoolConverter);
				}
				return serializerOptions;
			}
		}

		private static JsonSerializerOptions deserializerOptions = null;
		public static JsonNamingPolicy DeserializeNamingPolicy { get; } = new MinecraftNamingPolicy();
		public static JsonSerializerOptions DeserializerOptions {
			get {
				if(deserializerOptions is null) {
					deserializerOptions = new JsonSerializerOptions() {
						PropertyNamingPolicy = DeserializeNamingPolicy,
						IgnoreNullValues = true,
						WriteIndented = false
					};
					deserializerOptions.Converters.Add(NullableBoolConverter);
				}
				return deserializerOptions;
			}
		}

		public static NullableBoolJsonConverter NullableBoolConverter { get; } = new NullableBoolJsonConverter();

		public class NullableBoolJsonConverter : JsonConverter<bool?> {
			public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
				bool result = bool.Parse(reader.GetString());
				return result;
			}
			public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options) {
				string result = value.Value.ToString();
				writer.WriteStringValue(result);
			}
		}

		public class MinecraftNamingPolicy : JsonNamingPolicy {
			public override string ConvertName(string name) {
				if(name.ToLower().StartsWith("show")) {
					string sub = name.Substring("show".Length);
					string result = $"show_{ConvertName(sub)}";
					return result;
				} else {
					char[] chars = new char[name.Length];
					chars[0] = char.ToLower(name[0]);
					for(int i = 1; i < name.Length; i++)
						chars[i] = name[i];
					string result = new string(chars);
					return result;
				}
			}
		}

		public class DotNetNamingPolicy : JsonNamingPolicy {
			public override string ConvertName(string name) {
				if(name.ToLower().StartsWith("show")) {
					string sub = name.Substring("show".Length);
					string result = $"Show{ConvertName(sub)}";
					return result;
				} else {
					char[] chars = new char[name.Length];
					chars[0] = char.ToUpper(name[0]);
					for(int i = 1; i < name.Length; i++)
						chars[i] = name[i];
					string result = new string(chars);
					return result;
				}
			}
		}

	}

}
