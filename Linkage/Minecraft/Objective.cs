using MCSharp.Compilation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MCSharp.Linkage.Minecraft {

	public class Objective {
		
		/// <summary>
		/// The in-game name of the objective.
		/// </summary>
		public string Name { get; }

		/// <summary>
		/// The in-game criterion of the objective.
		/// </summary>
		public string Criterion { get; }


		/// <summary>
		/// Creates a new <see cref="Objective"/>.
		/// </summary>
		/// <param name="name">The in-game name of the objective.</param>
		/// <param name="criterion">The in-game criterion of the objective.</param>
		public Objective(string name, string criterion) {
			Name = name;
			Criterion = criterion;
		}


		/// <summary>
		/// Creates a new <see cref="Objective"/> and writes its 'add' command to the given <see cref="FunctionWriter"/>.
		/// </summary>
		/// <param name="function">The function writer to write the 'add' command to.</param>
		/// <param name="name">The in-game name of the objective. If <see langword="null"/>, generates one.</param>
		/// <param name="criterion">The in-game criterion of the objective.</param>
		/// <returns>Returns a new <see cref="Objective"/>.</returns>
		public static Objective AddObjective(FunctionWriter function, string name, string criterion) {
			if(name is null) name = MakeNextAnonymousName();
			function.WriteCommand($"scoreboard objectives add {name} {criterion}");
			return new Objective(name, criterion);
		}

		/// <summary>
		/// Writes an existing <see cref="Objective"/>'s 'add' command to the given <see cref="FunctionWriter"/>.
		/// </summary>
		/// <param name="function">The function writer to write the 'add' command to.</param>
		/// <param name="objective"></param>
		public static void AddObjective(FunctionWriter function, Objective objective) {
			function.WriteCommand($"scoreboard objectives add {objective.Name} {objective.Criterion}");
		}

		private static int anonCount = 0;
		private static string MakeNextAnonymousName() {
			return $"mcs.{anonCount++}";
		}

		public static void ClearAnonymousNames() {
			anonCount = 0;
		}

		public void WriteRemove(FunctionWriter function) {

			string comment =
				$"Remove objective '{Name}' of criterion '{Criterion}'.";
			
			function.WriteCommand($"scoreboard objectives remove {Name}", comment);

		}

		/// </inheritdoc>
		public override string ToString() => $"{Name} ({Criterion})";

	}

}
