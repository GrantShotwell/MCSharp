using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Compilation {

	public class FunctionWriter : IDisposable {

		public VirtualMachine VirtualMachine { get; }

		public string Name { get; }
		public string LocalFilePath { get; }
		public string FilePath { get; }
		public string GamePath { get; }

		private StreamWriter StreamWriter { get; }


		public FunctionWriter(VirtualMachine virtualMachine, Settings settings, string path, string name) {

			Datapack datapack = settings.Datapack;
			VirtualMachine = virtualMachine;

			Name = name;
			LocalFilePath = $"{path}\\{Name}.mcfunction";
			FilePath = $"{datapack.FunctionDirectory}\\{LocalFilePath}";
			GamePath = $"{datapack.Name}:{path}\\{Name}";

			StreamWriter = datapack.CreateFunctionFile(LocalFilePath);

		}


		public void WriteCommand(string command, string comments = null) {

			if(comments != null && comments.Trim() != string.Empty) {

				StreamWriter.WriteLine();
				string[] lines = command.Split('\n');
				foreach(string line in lines) {
					StreamWriter.WriteLine($"# {line.Trim()}");
				}

			}

			StreamWriter.WriteLine(command);

		}

		public void Dispose() {
			StreamWriter.Dispose();
		}

	}

}
