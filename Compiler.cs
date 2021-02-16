using MCSharp.Script;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp {

	public class Compiler {

		public static string DefaultSuccess { get; } = "Success.";
		public static string DefaultNoPath { get; } = "No path.";


		public Settings Settings { get; }

		public Compiler(Settings settings) {
			Settings = settings;
		}

		#region Data
		#endregion

		public bool Compile(out string message) {

			// Find & read all script files.
			ICollection<ScriptFile> scripts = new List<ScriptFile>();
			foreach(string file in Settings.Datapack.GetScriptFiles()) {
				if(!ScriptFile.ReadFile(file, Settings.Datapack, out message, out ScriptFile script)) return false;
				else scripts.Add(script);
			}

			int memberCount = 0, classCount = 0, scriptCount = 0;
			foreach(var script in scripts) {
				scriptCount++;
				foreach(var @class in script) {
					classCount++;
					foreach(var member in @class.Members) {
						memberCount++;
					}
				}
			}

			Program.PrintSuccess($"Parsing pass found " +
				$"{scriptCount} script{(scriptCount == 1 ? "" : "s")} with " +
				$"{classCount} class{(classCount == 1 ? "" : "es")} with " +
				$"{memberCount} member{(memberCount == 1 ? "" : "s")}.");

			// Define all mcfunction names and files.
			// todo

			// Parse all tokens.
			// todo

			message = DefaultSuccess;
			return true;

		}

	}

}
