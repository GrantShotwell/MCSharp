using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MCSharp;

public static class Program {

	public static ConsoleColor DefaultForegroundColor { get; } = ConsoleColor.Gray;
	private static Regex SettingsRegex { get; } = new Regex(@"(?'Setting'\w+)\s*=\s*(?'Value'.+)");

	private static string DatapackFolder { get; set; }
	private static string DatapackName { get; set; }

	public static void Main() {

		// Find all commands through reflection.
		foreach(MethodInfo method in typeof(Program).GetMethods()) {
			if(method.GetCustomAttribute(typeof(CommandAttribute), true) is CommandAttribute attribute) {
				var command = new Command(attribute, Delegate.CreateDelegate(typeof(CommandAction), method) as CommandAction);
				AddCommand(command);
			}
		}

		// Open and read settings.
		if(File.Exists("settings.txt")) {
			try {
				StreamReader reader = File.OpenText("settings.txt");
				string line;
				while((line = reader.ReadLine()) != null) {
					line = line.Trim();
					if(line.StartsWith("//")) continue;
					Match match = SettingsRegex.Match(line);
					if(match.Success) {
						string setting = match.Groups["Setting"].Value;
						string value = match.Groups["Value"].Value;
						string success = $"Applied '{setting}' setting.";
						switch(setting) {
							case "default_datapacks_folder": DatapackFolder = value; PrintSuccess(success); break;
							case "default_datapack_name": DatapackName = value; PrintSuccess(success); break;
							default: PrintWarning($"Skipped unknown setting '{setting}'."); break;
						}
					}
				}
			}
			catch(Exception e) {
				PrintError(e);
			}
		}

		// User input loop.
		while(true) {
			ExecuteInputFromUser(out bool exit);
			if(exit) break;
		}

	}

	private static void ExecuteInputFromUser(out bool exit) {

		var input = PrintPrompt();
		if(!CommandDictionary.ContainsKey(input.Command)) {

			PrintError($"Command '{input.Command}' does not exist.");
			exit = false;

		} else {

			CommandAction command = CommandDictionary[input.Command].Action;

#if DEBUG
			command(input, out exit);
#else
				try {
					command(input, out exit);
				} catch(Exception e) {
					PrintError(e);
					exit = false;
				}
#endif

		}

	}

	private static bool GetDatapack(out Datapack datapack) {

		bool setfolder = DatapackFolder is null;
		bool setname = DatapackName is null;
		if(setfolder || setname) {
			if(setfolder) PrintError("The datapack location has not been set. Use 'setfolder' to set the datapack location.");
			if(setname) PrintError("The datapack name has not been set. Use 'setname' to set the datapack name.");
			datapack = null;
			return false;
		} else {
			datapack = new Datapack($"{DatapackFolder}\\{DatapackName}", DatapackName);
			return true;
		}

	}

	#region Console Helpers

	public static void PrintText(string text) {
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.WriteLine(text);
		Console.ForegroundColor = DefaultForegroundColor;
	}

	public static void PrintError(string message) {
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(message);
		Console.ForegroundColor = DefaultForegroundColor;
	}

	public static void PrintError(Exception e) {
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine("An exception occured within the program.");
		Console.WriteLine($"{e.GetType().Name}: {e.Message}");
		Console.WriteLine(e.StackTrace);
		Console.ForegroundColor = DefaultForegroundColor;
	}

	public static void PrintWarning(string message) {
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine(message);
		Console.ForegroundColor = DefaultForegroundColor;
	}

	public static void PrintSuccess(string message) {
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine(message);
		Console.ForegroundColor = DefaultForegroundColor;
	}

	public static Input PrintPrompt() {
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write("\n> ");
		Input input = new Input(Console.ReadLine());
		Console.ForegroundColor = DefaultForegroundColor;
		return input;
	}

	public static bool PrintYN(string message = "") {
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write(message);
		string prompt = "Y/N: ";
		Console.Write(prompt);
		while(true) {
			switch(Console.ReadKey(true).Key) {
				case ConsoleKey.Y:
					Console.CursorLeft -= prompt.Length;
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write('Y');
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write('/');
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write('N');
					Console.ForegroundColor = DefaultForegroundColor;
					for(int n = 3; n < prompt.Length; n++) Console.Write(' ');
					Console.WriteLine();
					return true;
				case ConsoleKey.N:
					Console.CursorLeft -= prompt.Length;
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write('Y');
					Console.ForegroundColor = ConsoleColor.White;
					Console.Write('/');
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write('N');
					Console.ForegroundColor = DefaultForegroundColor;
					for(int n = 3; n < prompt.Length; n++) Console.Write(' ');
					Console.WriteLine();
					return false;
				default: continue;
			}
		}
	}

	public static void PrintCommandName(string alias) {
		if(alias is null) throw new ArgumentNullException(nameof(alias));
		if(!CommandDictionary.ContainsKey(alias)) PrintText($"A command with the alias '{alias}' does not exist.");
		else {
			Command command = CommandDictionary[alias];
			PrintCommandName(command);
		}
	}

	private static void PrintCommandName(Command command) {
		if(command is null) throw new ArgumentNullException(nameof(command));
		CommandAttribute attribute = command.Attribute;
		if(attribute.Aliases.Length == 0) PrintText($"This command has no name.");
		else PrintText(attribute.Aliases[0]);
	}

	public static void PrintCommandDescription(string alias) {
		if(alias is null) throw new ArgumentNullException(nameof(alias));
		if(!CommandDictionary.ContainsKey(alias)) PrintText($"A command with the alias '{alias}' does not exist.");
		else {
			Command command = CommandDictionary[alias];
			PrintCommandDescription(command);
		}
	}

	private static void PrintCommandDescription(Command command) {
		if(command is null) throw new ArgumentNullException(nameof(command));
		CommandAttribute attribute = command.Attribute;
		PrintText(attribute.Description);
	}

	public static void PrintCommandAliases(string alias) {
		if(alias is null) throw new ArgumentNullException(nameof(alias));
		if(!CommandDictionary.ContainsKey(alias)) PrintText($"A command with the alias '{alias}' does not exist.");
		else {
			Command command = CommandDictionary[alias];
			PrintCommandAliases(command);
		}
	}

	private static void PrintCommandAliases(Command command) {
		if(command is null) throw new ArgumentNullException(nameof(command));
		CommandAttribute attribute = command.Attribute;
		if(attribute.Aliases.Length == 0) PrintText("This command has no aliases.");
		else PrintText(string.Join(", ", attribute.Aliases));
	}

	#endregion

	#region Commands

	private static void AddCommand(Command command) {

		foreach(string alias in command.Attribute.Aliases) {
			CommandDictionary.Add(alias, command);
		}

	}

	public delegate void CommandAction(Input input, out bool exit);
	private static Dictionary<string, Command> CommandDictionary { get; } = new Dictionary<string, Command>();

	[Command("Provides information about commands.", "help", "h")]
	public static void Command_Help(Input input, out bool exit) {

		if(input.Arguments != string.Empty) {

			// Display help for given command.
			PrintCommandDescription(input.Arguments);
			PrintCommandAliases(input.Arguments);

			exit = false;

		} else {

			// Display all commands.
			HashSet<Command> commands = new HashSet<Command>();
			foreach(var pair in CommandDictionary) if(!commands.Contains(pair.Value)) commands.Add(pair.Value);
			foreach(var command in commands) PrintCommandName(command);

			exit = false;

		}

	}

	[Command("Sets the location of datapacks to save/edit.", "setfolder")]
	public static void Command_SetFolder(Input input, out bool exit) {

		DatapackFolder = input.Arguments;
		PrintSuccess($"Set current datapack folder to '{input.Arguments}'.");
		exit = false;

	}

	[Command("Sets the name of the datapack to be edited.", "setname")]
	public static void Command_SetName(Input input, out bool exit) {

		DatapackName = input.Arguments;
		PrintSuccess($"Set current datapack to '{input.Arguments}'.");
		exit = false;

	}

	[Command("Creates a datapack file structure located at the current save with the current name.", "create")]
	public static void Command_Create(Input input, out bool exit) {

		if(!GetDatapack(out Datapack datapack)) {
			exit = false;
			return;
		}
		PrintText($"Files will be created within '{datapack.RootDirectory}'.\nSome data may be overwritten.");
		if(!PrintYN("Continue? ")) {
			exit = false;
			return;
		}

		Directory.CreateDirectory($"{datapack.RootDirectory}");
		Directory.CreateDirectory($"{datapack.RootDirectory}\\data");
		Directory.CreateDirectory($"{datapack.RootDirectory}\\data\\minecraft");
		Directory.CreateDirectory($"{datapack.RootDirectory}\\data\\minecraft\\tags");
		Directory.CreateDirectory($"{datapack.RootDirectory}\\data\\minecraft\\tags\\functions");
		Directory.CreateDirectory($"{datapack.RootDirectory}\\data\\{datapack.Name}");
		Directory.CreateDirectory($"{datapack.RootDirectory}\\data\\{datapack.Name}\\functions");
		Directory.CreateDirectory($"{datapack.RootDirectory}\\data\\{datapack.Name}\\scripts");

		using(StreamWriter loadJSON_writer = File.CreateText(datapack.LoadJsonFile)) {
			loadJSON_writer.WriteLine($"{{\n\t\"values\": [\n\t\t\"{datapack.Name}:program/tick\"\n\t]\n}}");
		}
		using(StreamWriter tickJSON_writer = File.CreateText(datapack.TickJsonFile)) {
			tickJSON_writer.WriteLine($"{{\n\t\"values\": [\n\t\t\"{datapack.Name}:program/tick\"\n\t]\n}}");
		}
		using(StreamWriter progMCS_writer = File.CreateText(datapack.ProgramScriptFile)) {
			progMCS_writer.WriteLine("public static struct Program {\n\tpublic static void Load() { }\n\tpublic static void Tick() { }\n}");
		}

		PrintSuccess("Created template datapack.");
		exit = false;

	}

	[Command("Attempts to compile the current datapack.", "compile", "c")]
	public static void Command_Compile(Input input, out bool exit) {

		if(!GetDatapack(out Datapack datapack)) {
			exit = false;
			return;
		}
		PrintText($"Files will be edited within '{datapack.RootDirectory}'.\nSome data may be overwritten.");

		if(!PrintYN("Continue? ")) {
			exit = false;
			return;
		}

		ICollection<Assembly> assemblies = new List<Assembly>(1) { Assembly.GetExecutingAssembly() };
		Settings settings = new Settings(datapack);
		Compiler compiler = new Compiler(settings, assemblies);

		ResultInfo result = compiler.Compile();
		if(result.Success) {
			PrintSuccess(result.Message);
		} else {
			PrintError(result.Message);
		}

		compiler.Dispose();


		exit = false;

	}

	[Command("Exits the program.", "exit", "e")]
	public static void Command_Exit(Input input, out bool exit) {

		if(input.Arguments != string.Empty) PrintError("This command takes no arguments.");
		exit = true;

	}

	#endregion

	public struct Input {
		private static Regex Regex { get; } = new Regex(@"(?'Command'[\x21-\x7E]+)[\s]*(?'Arguments'([\s]*[\x21-\x7E]+)*)[\s]*");
		public string Command { get; }
		public string Arguments { get; }
		public Input(string input) {
			Match match = Regex.Match(input);
			Command = match.Groups["Command"].Value;
			Arguments = match.Groups["Arguments"].Value;
		}
	}

	public class Command {
		public CommandAttribute Attribute { get; }
		public CommandAction Action { get; }
		public Command(CommandAttribute attribute, CommandAction action) {
			Attribute = attribute ?? throw new ArgumentNullException(nameof(attribute));
			Action = action ?? throw new ArgumentNullException(nameof(action));
		}
	}

	public class CommandAttribute : Attribute {
		public string Description { get; }
		public string[] Aliases { get; }
		public CommandAttribute(string description, params string[] aliases) {
			Description = description ?? throw new ArgumentNullException(nameof(description));
			Aliases = aliases ?? throw new ArgumentNullException(nameof(aliases));
		}
	}

}
