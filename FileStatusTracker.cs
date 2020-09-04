using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp {

	public class FileStatusTracker {

		public int Position { get; private set; }
		public int Height => Files.Count;
		private IList<FileStatus> Files { get; } = new List<FileStatus>();
		public int Recursion { get; private set; }

		public void Add(FileStatus file) {

			if(Files.Contains(file)) throw new InvalidOperationException($"'{file}' has already been added.");
			Files.Add(file);

			Goto(Height - 1);
			Program.ClearCurrentConsoleLine();
			Console.ForegroundColor = ConsoleColor.Gray;
			if(file.Indent > 0) {
				for(int i = 1; i < file.Indent; i++) Console.Write("  ");
				Console.Write(" · Writing ");
			} else {
				Console.Write("Compiling ");
			}
			Console.Write(file.Name);
			Console.Write("...\n");

			Recursion++;
			file.Complete = false;

		}

		public void Finish(FileStatus file) {
			
			if(!Files.Contains(file)) throw new InvalidOperationException($"'{file}' is not being tracked.");
			int position = Files.IndexOf(file);

			Goto(position);
			Program.ClearCurrentConsoleLine();
			Console.ForegroundColor = ConsoleColor.Green;
			if(file.Indent > 0) {
				for(int i = 1; i < file.Indent; i++) Console.Write("  ");
				Console.Write(" · Wrote ");
			} else {
				Console.Write("Compiled ");
			}
			Console.Write(file.Name);
			Console.Write(".\n");

			Recursion--;
			file.Complete = true;

		}

		public void Goto(int height) {
			int delta = height - (Position + 1);
			Console.CursorTop += delta;
			Position = height;
		}

	}

}
