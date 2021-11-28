namespace MCSharp.Linkage.Script;

class ScriptStatement : IStatement {

	public StatementContext ScriptContext { get; }
	StatementContext IStatement.Context => ScriptContext;

	public ScriptStatement(StatementContext scriptContext) {
		ScriptContext = scriptContext;
	}

	public static ScriptStatement[] CreateArrayFromArray(StatementContext[] contexts) {

		int size = contexts.Length;
		ScriptStatement[] statements = new ScriptStatement[size];
		for(int i = 0; i < size; i++) statements[i] = new ScriptStatement(contexts[i]);
		return statements;

	}

}
