using Antlr4.Runtime;
using System;
using System.IO;

namespace MCSharp.Linkage.Predefined;

/// <summary>
/// Represents a predefined statement.
/// </summary>
public class PredefinedStatement : IStatement {

	public MCSharpParser.StatementContext PredefinedContext { get; }
	MCSharpParser.StatementContext IStatement.Context => PredefinedContext;


	/// <summary>
	/// Creates a new <see cref="PredefinedStatement"/> by using Antlr to lex/parse <paramref name="statement"/>.
	/// </summary>
	/// <param name="statement">The statement string to parse.</param>
	/// <exception cref="InvalidPredefinedStatementException">Thrown when Antlr sends a syntax error while lexing/parsing <paramref name="statement"/>.</exception>
	public PredefinedStatement(string statement) {

		// Use Antlr generated classes to parse the statement.

		ICharStream stream = CharStreams.fromString(statement);
		var errorListener = new ErrorListener(statement);

		MCSharpLexer lexer = new MCSharpLexer(stream);
		lexer.RemoveErrorListeners();
		lexer.AddErrorListener(errorListener);

		CommonTokenStream tokens = new CommonTokenStream(lexer);

		MCSharpParser parser = new MCSharpParser(tokens);
		parser.RemoveErrorListeners();
		parser.AddErrorListener(errorListener);

		PredefinedContext = parser.statement();

	}

	public PredefinedStatement(MCSharpParser.StatementContext predefinedContext) {

		PredefinedContext = predefinedContext;

	}


	public class InvalidPredefinedStatementException : Exception {

		public InvalidPredefinedStatementException(string statement) : base($"The predefined statement '{statement}' is invalid.") { }

	}

	public class ErrorListener : IAntlrErrorListener<IToken>, IAntlrErrorListener<int> {

		public string Statement { get; }


		public ErrorListener(string statement) {
			Statement = statement;
		}


		void IAntlrErrorListener<IToken>.SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {

			throw new InvalidPredefinedStatementException(Statement);
		}

		void IAntlrErrorListener<int>.SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {

			throw new InvalidPredefinedStatementException(Statement);
		}

	}

}
