using MCSharp.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Compilation {

	public class FunctionWriter : IDisposable {

		public VirtualMachine VirtualMachine { get; }

		public string Name { get; }
		public string LocalDirectory { get; }
		public string LocalFilePath { get; }
		public string FilePath { get; }
		public string GamePath { get; }

		private StreamWriter StreamWriter { get; }
		private int BufferedLines { get; set; } = 0;

		public FunctionWriter(VirtualMachine virtualMachine, Settings settings, string path, string name) {

			Datapack datapack = settings.Datapack;
			VirtualMachine = virtualMachine;

			Name = NameStyleConverter.PascalToSnake(name);
			LocalDirectory = NameStyleConverter.PascalToSnake(path);
			LocalFilePath = $"{LocalDirectory}\\{Name}.mcfunction";
			FilePath = $"{datapack.FunctionDirectory}\\{LocalFilePath}";
			GamePath = $"{datapack.Name}:{LocalDirectory}\\{Name}".Replace('\\', '/');

			StreamWriter = datapack.CreateFunctionFile(LocalFilePath);

			Program.PrintText($"Created function '{LocalFilePath}'.");

		}


		public void WriteCommand(string command, string comments = null, bool indentBefore = false, bool indentAfter = false) {

			#region Argument Checks
			if(command is null)
				throw new ArgumentNullException(nameof(command));
			#endregion

			if(indentBefore)
				IndentFromBufferedLines(1);

			FillBufferedLines();
			if(comments != null)
				WriteComments(comments, indentBefore: false, indentAfter: false);
			StreamWriter.WriteLine(command);

			if(indentAfter)
				AddBufferedLines(1);

		}

		public void WriteComments(string comments = null, bool indentBefore = false, bool indentAfter = false) {

			if(indentBefore)
				IndentFromBufferedLines(1);

			if(comments != null) {
				FillBufferedLines();
				string[] lines = comments.Split('\n');
				foreach(string line in lines)
					StreamWriter.WriteLine($"# {line.Trim()}");
			} else {
				StreamWriter.WriteLine();
			}

			if(indentAfter)
				AddBufferedLines(1);

		}

		public void WriteTitle(string title, bool indentBefore = false, bool indentAfter = false) {
			
			#region Argument Checks
			if(title is null)
				throw new ArgumentNullException(nameof(title));
			#endregion

			int width = 0;

			string[] lines = title.Split('\n');
			foreach(string line in lines) {
				int w = line.Length + 4;
				if(w > width) width = w;
			}

			char[] barChars = new char[width];
			for(int i = 0; i < width; i++)
				barChars[i] = '#';
			string bar = new string(barChars);

			if(indentBefore)
				IndentFromBufferedLines(1);

			FillBufferedLines();
			StreamWriter.WriteLine(bar);

			foreach(string line in lines) {
				int w = line.Length + 4 - width;
				StreamWriter.Write("# ");
				StreamWriter.Write(line);
				while(w-- > 0) StreamWriter.Write(" ");
				StreamWriter.WriteLine(" #");
			}

			StreamWriter.WriteLine(bar);

			if(indentAfter)
				AddBufferedLines(1);

		}

		public void AddBufferedLines(int count) {

			if(count < 0) throw new ArgumentOutOfRangeException(nameof(count), count, $"Value cannot be negative.");
			else BufferedLines += count;

		}

		private void IndentFromBufferedLines(int count = 1) {

			while(count-- > 0) {
				if(BufferedLines > 0) BufferedLines--;
				StreamWriter.WriteLine();
			}

		}

		private void FillBufferedLines() {

			while(BufferedLines > 0) {
				BufferedLines--;
				StreamWriter.WriteLine();
			}

		}

		public void Dispose() {
			StreamWriter.Dispose();
		}

	}

}
