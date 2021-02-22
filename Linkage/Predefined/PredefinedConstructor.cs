using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Predefined {

	public class PredefinedConstructor : IConstructor {

		public PredefinedType Declarer { get; }
		IType IConstructor.Declarer => Declarer;

		public Modifier Modifiers { get; }
		public Function Invoker { get; }

		public PredefinedConstructor(PredefinedType declarer, Modifier modifiers, Function invoker) {
			Declarer = declarer ?? throw new ArgumentNullException(nameof(declarer));
			Modifiers = modifiers;
			Invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
		}

	}

}
