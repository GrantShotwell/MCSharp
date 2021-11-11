using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Dfa;
using Antlr4.Runtime.Sharpen;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MCSharp.Linkage.Predefined {

	/// <summary>
	/// Represents a predefined expression.
	/// </summary>
	public class PredefinedExpression : IExpression {

		public MCSharpParser.ExpressionContext Context { get; }

		/// <summary>
		/// Creates a new <see cref="PredefinedExpression"/> by using Antlr to lex/parse <paramref name="expression"/>.
		/// </summary>
		/// <param name="expression">The expression string to parse.</param>
		/// <exception cref="InvalidPredefinedExpressionException">Thrown when Antlr sends a syntax error while lexing/parsing <paramref name="expression"/>.</exception>
		public PredefinedExpression(string expression) {

			// Use Antlr generated classes to parse the expression.

			ICharStream stream = CharStreams.fromString(expression);
			var errorListener = new ErrorListener(expression);

			MCSharpLexer lexer = new MCSharpLexer(stream);
			lexer.RemoveErrorListeners();
			lexer.AddErrorListener(errorListener);

			CommonTokenStream tokens = new CommonTokenStream(lexer);

			var parser = new MCSharpParser(tokens);
			parser.RemoveErrorListeners();
			parser.ErrorListeners.Add(errorListener);

			Context = parser.expression();

		}

		public class InvalidPredefinedExpressionException : Exception {

			public InvalidPredefinedExpressionException(string statement, string message) : base($"The predefined expression '{statement}' is invalid. Antlr message follows.\n{message}") { }

		}

		public class ErrorListener : IAntlrErrorListener<IToken>, IAntlrErrorListener<int> {

			public string Statement { get; }


			public ErrorListener(string statement) {
				Statement = statement;
			}

			/// <exception cref="InvalidPredefinedExpressionException">Thrown when Antlr sends a syntax error while lexing/parsing.</exception>
			void IAntlrErrorListener<IToken>.SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {
				string message = $"line {line}:{charPositionInLine} {msg}";
				throw new InvalidPredefinedExpressionException(Statement, message);
			}

			/// <exception cref="InvalidPredefinedExpressionException">Thrown when Antlr sends a syntax error while lexing/parsing.</exception>
			void IAntlrErrorListener<int>.SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e) {
				string message = $"line {line}:{charPositionInLine} {msg}";
				throw new InvalidPredefinedExpressionException(Statement, message);
			}

		}

	}

}
