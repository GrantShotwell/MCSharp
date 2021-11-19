using System;
using System.Collections.Generic;
using System.IO;

namespace MCSharp.Compilation;

/// <summary>
/// Represents a Minecraft datapack and its file structure.
/// </summary>
public class Datapack {

	/// <summary>
	/// The name of the datapack.
	/// </summary>
	public string Name { get; }

	/// <summary>
	/// The root directory of the datapack.
	/// </summary>
	public string RootDirectory { get; }

	/// <summary>
	/// The directory of the input '.mcsharp' files.
	/// </summary>
	public string ScriptDirectory => $"{RootDirectory}\\data\\{Name}\\scripts";

	/// <summary>
	/// The directory of the output '.mcfunction' files.
	/// </summary>
	public string FunctionDirectory => $"{RootDirectory}\\data\\{Name}\\functions";

	/// <summary>
	/// The location of the datapack's 'load.json' file.
	/// </summary>
	public string LoadJsonFile => $"{RootDirectory}\\data\\minecraft\\tags\\load.json";

	/// <summary>
	/// The location of the datapack's 'tick.json' file.
	/// </summary>
	public string TickJsonFile => $"{RootDirectory}\\data\\minecraft\\tags\\tick.json";

	/// <summary>
	/// The location of the datapack's program MCSharp file.
	/// </summary>
	public string ProgramScriptFile => $"{ScriptDirectory}\\{ProgramClassName}.mcsharp";

	/// <summary>
	/// The name of the program class.
	/// </summary>
	public string ProgramClassName => "Program";

	/// <summary>
	/// The name of the program class's load method.
	/// </summary>
	public string ProgramLoadName => "Load";

	/// <summary>
	/// The name of the program class's tick method.
	/// </summary>
	public string ProgramTickName => "Tick";


	/// <summary>
	/// Creates a new datapack.
	/// </summary>
	/// <param name="root">The root directory of the datapack.</param>
	/// <param name="name">The name of the datapack.</param>
	public Datapack(string root, string name) {

		if(string.IsNullOrWhiteSpace(name))
			throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace", nameof(name));
		Name = name;

		if(string.IsNullOrWhiteSpace(root))
			throw new ArgumentException($"'{nameof(root)}' cannot be null or whitespace", nameof(root));
		RootDirectory = root;

	}


	/// <summary>
	/// Creates a '.mcfunction' in the given local path.
	/// </summary>
	/// <param name="localPath">The local path to create the '.mcfunction' in.</param>
	/// <returns>The created <see cref="StreamWriter"/> for the '.mcfunction' file.</returns>
	public StreamWriter CreateFunctionFile(string localPath) {

		string[] separated = localPath.Split(new char[] { '\\', '/' });
		string localDirectory = string.Join('\\', separated[0..^1]);
		Directory.CreateDirectory($"{FunctionDirectory}\\{localDirectory}");

		return File.CreateText($"{FunctionDirectory}\\{localPath}");

	}

	/// <summary>
	/// Enumerates all '.mcfunction' files in the datapack.
	/// </summary>
	/// <returns>An enumerable of all '.mcfunction' files in the datapack.</returns>
	public IEnumerable<string> EnumerateFunctionFiles() {
		return Directory.EnumerateFiles(FunctionDirectory, "*.mcfunction", SearchOption.AllDirectories);
	}

	/// <summary>
	/// Enumerates all '.mcsharp' files in the datapack.
	/// </summary>
	/// <returns>An enumerable of all '.mcsharp' files in the datapack.</returns>
	public IEnumerable<string> EnumerateScriptFiles() {
		return Directory.EnumerateFiles(ScriptDirectory, "*.mcsharp", SearchOption.AllDirectories);
	}

}
