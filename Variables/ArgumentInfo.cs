using System;
using System.Collections.Generic;

namespace MCSharp.Variables {
	public class ArgumentInfo {

		public IReadOnlyList<(Type Type, Variable Value)> Arguments { get; }


		public ArgumentInfo(Variable[] arguments) {
			int length = arguments.Length;
			var args = new (Type Type, Variable Value)[length];
			for(int i = 0; i < length; i++) {
				args[i] = (arguments[i].GetType(), arguments[i]);
			}
			Arguments = args;
		}

		public static implicit operator ArgumentInfo(Variable[] arguments) => new ArgumentInfo(arguments);


	}
}
