using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage {

	public interface IField : IMemberDefinition {

		public IExpression Initializer { get; }

	}

}
