using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp {

	public class FileStatus {

		public string Name { get; }
		public bool Complete { get; set; }
		public int Indent { get; }
		
		public FileStatus(string name, int indent) {
			Name = name;
			Complete = false;
			Indent = indent;
		}

	}

}
