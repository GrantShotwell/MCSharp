using MCSharp.Variables;
using static MCSharp.Compiler;

namespace MCSharp {

    public class Datapack {

        public string Name { get; set; }
        public string Path => Program.Directory + "\\" + Name;
        public Function Load { get; set; }
        public Function Main { get; set; }

    }

}
