using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Script {

	public class ScriptExpression : IExpression {

		public MCSharpParser.ExpressionContext Context { get; }

		public ScriptExpression(MCSharpParser.ExpressionContext context) {
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public ScriptExpression[] CreateArrayFromArray(MCSharpParser.ExpressionContext[] contexts) {

			int size = contexts.Length;
			ScriptExpression[] expressions = new ScriptExpression[size];
			for(int i = 0; i < size; i++) expressions[i] = new ScriptExpression(contexts[i]);
			return expressions;

		}

	}

}
