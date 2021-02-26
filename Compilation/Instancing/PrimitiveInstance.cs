using Antlr4.Runtime.Tree;
using MCSharp.Linkage;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Compilation.Instancing {

	/// <summary>
	/// Represents an instance of a primitive type.
	/// </summary>
	public abstract class PrimitiveInstance : IInstance {

		/// <inheritdoc/>
		public IType Type { get; }
		
		/// <inheritdoc/>
		public ITerminalNode Identifier { get; }


		public PrimitiveInstance(IType type, ITerminalNode identifier) {
			Type = type;
			Identifier = identifier;
		}


		public class ObjectiveInstance : PrimitiveInstance {

			public Objective Objective { get; }

			public ObjectiveInstance(IType type, ITerminalNode identifier, Objective objective) : base(type, identifier) {
				Objective = objective ?? throw new ArgumentNullException(nameof(objective));
			}

		}

		public class IntegerInstance : ObjectiveInstance {

			public IntegerInstance(IType type, ITerminalNode identifier, Objective objective) : base(type, identifier, objective) { }

		}

		public class BooleanInstance : ObjectiveInstance {

			public BooleanInstance(IType type, ITerminalNode identifier, Objective objective) : base(type, identifier, objective) { }

		}

	}

}
