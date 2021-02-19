using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Compilation {

	/// <summary>
	/// Represents a Minecraft datapack and its file structure.
	/// </summary>
	public class Datapack {

		public string Name { get; }
		public string RootDirectory { get; }
		public string ScriptDirectory => $"{RootDirectory}\\data\\{Name}\\scripts";
		public string FunctionDirectory => $"{RootDirectory}\\data\\{Name}\\functions";
		public string LoadJsonFile => $"{RootDirectory}\\data\\minecraft\\tags\\load.json";
		public string TickJsonFile => $"{RootDirectory}\\data\\minecraft\\tags\\tick.json";
		public string ProgramScriptFile => $"{ScriptDirectory}\\Program.mcsharp";


		public Datapack(string root, string name) {

			if(string.IsNullOrWhiteSpace(name))
				throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
			Name = name;

			if(string.IsNullOrWhiteSpace(root))
				throw new ArgumentException($"'{nameof(root)}' cannot be null or whitespace", nameof(root));
			RootDirectory = root;

		}


		public StreamWriter CreateFunctionFile(string localPath) {

			return File.CreateText($"{FunctionDirectory}\\{localPath}");

		}


		public IReadOnlyCollection<string> GetScriptFiles() {
			ICollection<string> files = new LinkedList<string>();
			void RecurseDirectory(string root) {
				foreach(string file in Directory.EnumerateFiles(root, "*.mcsharp")) files.Add(file);
				foreach(string directory in Directory.EnumerateDirectories(root)) RecurseDirectory(directory);
			}
			RecurseDirectory(ScriptDirectory);
			return (IReadOnlyCollection<string>)files;
		}

		public IReadOnlyCollection<string> GetFunctionFiles() {
			ICollection<string> files = new LinkedList<string>();
			void RecurseDirectory(string root) {
				foreach(string file in Directory.EnumerateFiles(root, "*.mcfunction")) files.Add(file);
				foreach(string directory in Directory.EnumerateDirectories(root)) RecurseDirectory(directory);
			}
			RecurseDirectory(FunctionDirectory);
			return (IReadOnlyCollection<string>)files;
		}

	}

}
