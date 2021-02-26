﻿using MCSharp.Compilation;
using System;
using System.Collections.Generic;
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
		private Objective(string name, string criterion) {
			Name = name;
			Criterion = criterion;
		}


		/// <summary>
		/// Creates a new <see cref="Objective"/> and writes its 'add' command to the given <see cref="FunctionWriter"/>.
		/// </summary>
		/// <param name="function">The function writer to write the 'add' command to.</param>
		/// <param name="name">The in-game name of the objective.</param>
		/// <param name="criterion">The in-game criterion of the objective.</param>
		/// <returns></returns>
		public static Objective AddObjective(FunctionWriter function, string name, string criterion) {

			function.WriteCommand($"scoreboard objectives add {name} {criterion}");
			return new Objective(name, criterion);

		}

		public void WriteRemove(FunctionWriter function) {

			string comment =
				$"Remove objective '{Name}' of criterion '{Criterion}'.";
			
			function.WriteCommand($"scoreboard objectives remove {Name}", comment);

		}

	}

}
