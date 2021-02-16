using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MCSharp.Script {
	public class TokenReader : IEnumerator<ScriptToken> {

		public static Regex NextToken { get; } = new Regex(@"(?'Whitespace'[ \r\t]*)(?'Token'(?(?="")(?:""(?:(?:(?:\\\\)+)|(?:\\"")|[^""])*"")|(?:[\n()\[\]{};]|(?:[+*\-\/%><=.]+)|(?:[^ \t\n()\[\]{};+*\-\/%><=.]+))))");

		private int Start { get; }
		private int Index { get; set; }
		private string Content { get; }

		private ScriptFile File { get; }
		private int Line { get; set; }

		public ScriptToken Current { get; private set; }
		object IEnumerator.Current => Current;


		public TokenReader(ScriptFile file, string content) {
			Content = content ?? throw new ArgumentNullException(nameof(content));
			Start = 0;
			File = file;
			Reset();
		}

		private TokenReader(TokenReader reader) {
			Content = reader.Content;
			Start = reader.Start;
			File = reader.File;
			Index = reader.Index;
			Line = reader.Line;
			Current = reader.Current;
		}


		public void Reset() {
			Index = Start;
			Line = 1;
			Current = default;
		}

		public bool MoveNext() {

			if(File == null) throw new InvalidOperationException($"Cannot move forward a {nameof(TokenReader)} when {nameof(File)} is null.");

			Match match = NextToken.Match(Content, Index);
			if(!match.Success) return false;
			Index += match.Length;
			
			string token = match.Groups["Token"].Value;
			if(token == "\n") {
				Line++;
				return MoveNext();
			} else {
				Current = new ScriptToken(token, new ScriptTrace(File, Line));
				return true;
			}

		}

		public void Dispose() {
			// Nothing to dispose of.
		}

		public TokenReader Branch() {
			return new TokenReader(this);
		}

		public bool ReadNextToken(string expected, out string message, out ScriptToken? result) {
			if(!MoveNext()) {
				result = null;
				message = $"After {Current}: Expected '{expected}' but found the end of file.";
				return false;
			} else {
				result = Current;
				if(expected == (string)result) {
					message = Compiler.DefaultSuccess;
					return true;
				} else {
					message = $"{Current.Trace}: Expected '{expected}' but found '{(string)Current}'.";
					return false;
				}
			}
		}

		public bool ReadNextToken(Regex expected, string name, out string message, out ScriptToken? result) {
			if(!MoveNext()) {
				result = null;
				message = $"After {Current}: Expected {name} but found the end of file.";
				return false;
			} else {
				result = Current;
				if(expected.IsMatch((string)result)) {
					message = Compiler.DefaultSuccess;
					return true;
				} else {
					message = $"{Current.Trace}: Expected {name} but found '{(string)Current}'.";
					return false;
				}
			}
		}

		public bool ReadNextToken(ICollection<string> expected, string name, out string message, out ScriptToken? result) {
			if(!MoveNext()) {
				result = null;
				message = $"After {Current}: Expected {name} but found the end of file.";
				return false;
			} else {
				result = Current;
				if(expected.Contains((string)result)) {
					message = Compiler.DefaultSuccess;
					return true;
				} else {
					message = $"{Current.Trace}: Expected {name} but found '{(string)Current}'.";
					return false;
				}
			}
		}

		public bool ReadAnyNextToken(string expected, out string message, out ScriptToken? result) {
			if(!MoveNext()) {
				result = null;
				message = $"After {Current}: Expected '{expected}' but found the end of file.";
				return false;
			} else {
				result = Current;
				message = Compiler.DefaultSuccess;
				return true;
			}
		}

		public bool LookAhead(string expected) {
			TokenReader branch = Branch();
			return branch.ReadNextToken(expected, out _, out _);
		}

		public bool LookAhead(Regex expected) {
			TokenReader branch = Branch();
			return branch.ReadNextToken(expected, "", out _, out _);
		}

		public bool LookAhead(ICollection<string> expected) {
			TokenReader branch = Branch();
			return branch.ReadNextToken(expected, "", out _, out _);
		}

		public bool LookAhead(out ScriptToken? next) {
			TokenReader branch = Branch();
			if(branch.MoveNext()) {
				next = branch.Current;
				return true;
			} else {
				next = null;
				return false;
			}
		}

	}
}
