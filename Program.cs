using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.IO;

namespace MCSharp {

    public static class Program {

        public static bool Debug { get; private set; } = true;

        public static string MinecraftVersion => "1.15";
        public static string ProgramVersion => "0.0.0";

        public static string Directory { get; private set; }
        public static Datapack Datapack { get; private set; }

        public static string ScriptsFolder => $"{Directory}\\{Datapack.Name}\\data\\{Datapack.Name}\\scripts\\";
        public static string FunctionsFolder => $"{Directory}\\{Datapack.Name}\\data\\{Datapack.Name}\\functions\\";

        public static string LoadMCScriptDefault {
            get {
                using StreamReader template = File.OpenText("load_template.mcscript");
                return template.ReadToEnd();
            }
        }

        public static string MainMCScriptDefault {
            get {
                using StreamReader template = File.OpenText("main_template.mcscript");
                return template.ReadToEnd();
            }
        }

        static void Main(string[] args) {

            Console.Title = $"MCSharp {ProgramVersion} - Minecraft {MinecraftVersion}";
            Console.WindowWidth = (int)(Console.WindowWidth * 1.25);

            var commands = new Dictionary<string, Action<string>>() {
                { "folder", Folder },
                { "pack", Pack },
                { "create", Create },
                { "compile", Compile },
                { "c", Compile },
                { "exit", Exit }
            };
            commands.Add("help", (arguments) => {
                if(arguments == "") {
                    string keys = "";
                    foreach(string key in commands.Keys) keys += key + " ";
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(string.Format("There are {0} commands: {1}", commands.Keys.Count, keys));
                } else {
                    if(arguments == null) {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("help\n\t=> Lists all commands for this console.\n" +
                                          "help {command}\n\t=> Details the given command.");
                    } else if(commands.TryGetValue(arguments, out Action<string> cmdValue)) {
                        cmdValue.Invoke(null);
                    }
                }
            });

            try {
                using StreamReader settings = File.OpenText("settings.txt");
                string line;
                while((line = settings.ReadLine()) != null) {
                    string[] split = line.Split('=');
                    if(split.Length < 2) continue;
                    string setting = split[0].Trim();
                    string argument = split[1].Trim();
                    switch(setting) {
                        case "default_datapacks_folder":
                            Folder(argument);
                            break;
                        case "default_datapack_name":
                            Pack(argument);
                            break;
                    }
                }
            } catch(FileNotFoundException) {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Could not find the settings file, 'settings.txt'. Folder and pack will need to be set manually.");
            }

            while(true) {

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n> ");

                string[] cmdSplit = Console.ReadLine().Split(' ', 2);
                if(cmdSplit.Length == 0) continue;
                string cmdKey = cmdSplit[0];

                if(commands.TryGetValue(cmdKey, out Action<string> cmdValue)) {
                    if(Debug) {
                        if(cmdSplit.Length >= 2) cmdValue.Invoke(cmdSplit[1]);
                        else cmdValue.Invoke("");
                    } else {
                        try {
                            if(cmdSplit.Length >= 2) cmdValue.Invoke(cmdSplit[1]);
                            else cmdValue.Invoke("");
                        } catch(Exception e) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{e.GetType()}: {e.Message}");
                        }
                    }
                } else if(cmdKey == "") {
                    // Do nothing.
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Command not found.");
                }

            }

        }


        /// <summary>
        /// Interacts with the datapacks folder.
        /// </summary>
        public static void Folder(string arguments) {
            if(arguments == null) { //help
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("folder {path}\n\t=> Sets the location of the 'datapacks' folder. (eg. 'folder C:\\Users\\___\\AppData\\Roaming\\.minecraft\\saves\\___\\datapacks')\n" +
                                  "folder ?\n\t=> Gets the location of the 'datapacks' folder.");
            }
            else if(arguments == "") { //no arguments
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Enter a 'datapacks' folder location. (eg. 'C:\\Users\\___\\AppData\\Roaming\\.minecraft\\saves\\___\\datapacks')");
            } else if(arguments == "?") {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(string.Format("The datapacks folder is currently set as '{0}'.", Directory));
            } else if(System.IO.Directory.Exists(arguments)) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Format("Datapack folder now set as '{0}'.", Directory = arguments));
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not find the specified folder.");
            }
        }

        /// <summary>
        /// Interacts with the datapack name.
        /// </summary>
        public static void Pack(string arguments) {
            if(arguments == null) { //help
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("pack {name}\n\t=> Specifies which datapack name you are working in.\n" +
                                  "pack ?\n\t=> Gets the datapack name you are working in.");
            } else if(arguments == "") { //no arguments
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Enter a datapack name. (eg. 'my-datapack')");
            } else if(arguments == "?") { //query
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(string.Format("The datapack name is currently set as '{0}'.", Datapack?.Name ?? "null"));
            } else {
                if(Datapack == null) Datapack = new Datapack();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(string.Format("Datapack folder now set as '{0}'. Please make sure that it is a valid name.", Datapack.Name = arguments));
            }
        }

        /// <summary>
        /// Creates the datapack.
        /// </summary>
        public static void Create(string arguments) {
            if(arguments == null) { //help
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("create\n\t=> Creates the datapack. If the datapack already exists, data may be overridden.");
            } else if(arguments == "") { //no arguments
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("If the pack already exists, data may be overriden. Continue?");
                string answr = "_";
                while(answr != "y" && answr != "n") {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Y/N> ");
                    answr = Console.ReadLine().ToLower();
                }
                if(answr == "n") return;

                #region Create

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Creating datapack directories... ");

                //Create directories.
                System.IO.Directory.CreateDirectory(Datapack.Path);
                System.IO.Directory.CreateDirectory(Datapack.Path + "\\data");
                System.IO.Directory.CreateDirectory(Datapack.Path + "\\data\\minecraft");
                System.IO.Directory.CreateDirectory(Datapack.Path + "\\data\\minecraft\\tags");
                System.IO.Directory.CreateDirectory(Datapack.Path + "\\data\\minecraft\\tags\\functions");
                System.IO.Directory.CreateDirectory(Datapack.Path + "\\data\\" + Datapack.Name);
                System.IO.Directory.CreateDirectory(Datapack.Path + "\\data\\" + Datapack.Name + "\\functions");
                System.IO.Directory.CreateDirectory(Datapack.Path + "\\data\\" + Datapack.Name + "\\scripts");

                ClearCurrentConsoleLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Created datapack directories.");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Creating datapack files... ");

                //Create files.
                using(StreamWriter loadJSON_writer = File.CreateText(Datapack.Path + "\\data\\minecraft\\tags\\functions\\load.json")) {
                    loadJSON_writer.WriteLine("{\n\t\"values\": [\n\t\t\"" + Datapack.Load.FolderPath + "\"\n\t]\n}");
                }
                using(StreamWriter tickJSON_writer = File.CreateText(Datapack.Path + "\\data\\minecraft\\tags\\functions\\tick.json")) {
                    tickJSON_writer.WriteLine("{\n\t\"values\": [\n\t\t\"" + Datapack.Main.FolderPath + "\"\n\t]\n}");
                }
                using(StreamWriter loadMCS_writer = File.CreateText(Datapack.Path + "\\data\\" + Datapack.Name + "\\scripts\\load.mcsharp")) {
                    loadMCS_writer.WriteLine(LoadMCScriptDefault);
                }
                using(StreamWriter mainMCS_writer = File.CreateText(Datapack.Path + "\\data\\" + Datapack.Name + "\\scripts\\main.mcsharp")) {
                    mainMCS_writer.WriteLine(MainMCScriptDefault);
                }

                ClearCurrentConsoleLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Created datapack files.");

                //Done!

                #endregion

            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This command does not take any arguments.");
            }
        }

        /// <summary>
        /// Compiles the datapack.
        /// </summary>
        public static void Compile(string arguments) {
            if(arguments == null) { //help
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("compile\n\t=> Compiles the datapack.");
            } else if(arguments == "") { //no arguments

                #region Compile

                Compiler.Compile(Datapack.Path + "\\data\\" + Datapack.Name);

                #endregion

            } else { //any arguments
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This command does not take any arguments.");
            }
        }

        /// <summary>
        /// Closes the console.
        /// </summary>
        public static void Exit(string arguments) {
            if(arguments == null) { //help
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("exit\n\t=> Closes this console.");
            } else if(arguments == "") { //no arguments
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Goodbye!");
                Environment.Exit(001);
            } else { //any arguments
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This command does not take any arguments.");
            }
        }

        /// <summary>
        /// https://stackoverflow.com/questions/5027301/c-sharp-clear-console-last-item-and-replace-new-console-animation
        /// </summary>
        public static void ClearCurrentConsoleLine() {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            for(int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.SetCursorPosition(0, currentLineCursor);
        }

    }

}
