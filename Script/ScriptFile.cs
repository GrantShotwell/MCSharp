using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MCSharp.Script {

	public class ScriptFile : IReadOnlyCollection<ScriptClass> {

		public string FullPath { get; }
		public Datapack Datapack { get; }
		public string ScriptPath => FullPath[(Datapack.ScriptDirectory.Length + 1)..^0];
		private ScriptClass[] Array { get; set; }
		public int Count => Array.Length;


		private ScriptFile(string fullPath, Datapack datapack, ScriptClass[] array) {
			FullPath = fullPath ?? throw new ArgumentNullException(nameof(fullPath));
			Datapack = datapack ?? throw new ArgumentNullException(nameof(datapack));
			Array = array;
		}


		public static bool ReadFile(string path, Datapack datapack, out string message, out ScriptFile result) {

			if(path is null) throw new ArgumentNullException(nameof(path));
			string content = File.ReadAllText(path);

			result = new ScriptFile(path, datapack, null);
			TokenReader reader = new TokenReader(result, content);
			ICollection<ScriptClass> classes = new LinkedList<ScriptClass>();
			while(true) {
				TokenReader branch = reader.Branch();
				if(!branch.MoveNext()) break;
				if(!ScriptClass.ReadClass(ref reader, out message, out ScriptClass? @class)) return false;
				classes.Add(@class.Value);
			}

			result.Array = classes.ToArray();
			message = Compiler.DefaultSuccess;
			return true;

		}


		public IEnumerator<ScriptClass> GetEnumerator() => ((IEnumerable<ScriptClass>)Array).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => Array.GetEnumerator();

		public override string ToString() => ScriptPath;

	}

}
