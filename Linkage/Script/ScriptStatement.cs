using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Linkage.Script {

	class ScriptStatement : IStatement {

		public MCSharpParser.StatementContext ScriptContext { get; }
		MCSharpParser.StatementContext IStatement.Context => ScriptContext;

		public ScriptStatement(MCSharpParser.StatementContext scriptContext) {
			ScriptContext = scriptContext;
		}

		public static ScriptStatement[] CreateArrayFromArray(MCSharpParser.StatementContext[] contexts) {

			int size = contexts.Length;
			ScriptStatement[] statements = new ScriptStatement[size];
			for(int i = 0; i < size; i++) statements[i] = new ScriptStatement(contexts[i]);
			return statements;

		}

	}

}
