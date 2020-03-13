using MCSharp.Variables;

namespace MCSharp {

	public class Datapack {

		public string Name { get; set; }
		public string Path => Program.Directory + "\\" + Name;
		public VarFunction Load { get; set; }
		public VarFunction Main { get; set; }

	}

}
