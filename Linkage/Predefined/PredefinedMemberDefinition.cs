using MCSharp.Compilation.Instancing;
using MCSharp.Linkage.Minecraft;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Predefined {

	public abstract class PredefinedMemberDefinition : IMemberDefinition {

		public class Field : PredefinedMemberDefinition, IField {

			public PredefinedExpression Initializer { get; }
			IExpression IField.Initializer => Initializer;

			public Field(PredefinedExpression initializer) {
				Initializer = initializer;
			}

			public override void Dispose() {
				// Nothing to dispose of.
			}

		}

		public class Property : PredefinedMemberDefinition, IProperty {

			public IFunction Getter { get; }

			public IFunction Setter { get; }

			public Property(IFunction getter, IFunction setter) {
				Getter = getter;
				Setter = setter;
			}

			public override void Dispose() {
				Getter?.Dispose();
				Setter?.Dispose();
			}

		}

		public class Method : PredefinedMemberDefinition, IMethod {

			public IFunction Invoker { get; }

			public Method(IFunction invoker) {
				Invoker = invoker ?? throw new ArgumentNullException(nameof(invoker));
			}

			public override void Dispose() {
				Invoker.Dispose();
			}

		}

		public abstract void Dispose();

	}

}
