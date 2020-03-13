using MCSharp.Statements;
using MCSharp.Variables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace MCSharp.Compilation {

	[DebuggerDisplay("{Access} {Usage} {TypeName} {FullAlias} ([{Parameters.Length}])")]
	public class ScriptMethod : ScriptMember, IReadOnlyList<ScriptLine> {

		private readonly string toString;

		private ScriptLine[] ScriptLines { get; }
		public ScriptLine this[int index] => ScriptLines[index];
		public Func<Variable[], Variable> Func { get; }
		public Variable ReturnValue { get; }
		public Variable[] Parameters { get; }
		public int Length => ScriptLines.Length;

		public override ScriptClass DeclaringType {
			get => base.DeclaringType;
			set {
				DeclaringType?.Overloads.Remove(this);
				value?.Overloads.Add(this);
				base.DeclaringType = value;
			}
		}

		int IReadOnlyCollection<ScriptLine>.Count => ((IReadOnlyList<ScriptLine>)ScriptLines).Count;


		public ScriptMethod(string alias, string type, Variable[] parameters, ScriptClass declaringType, ScriptString script,
		  Access access = Access.Private, Usage usage = Usage.Default)
		: this(alias, type, parameters, declaringType, GetLines(script), access, usage, script.ScriptTrace) { }

		public ScriptMethod(string alias, string type, Variable[] parameters, ScriptClass declaringType, ScriptWild wild,
		  Access access = Access.Private, Usage usage = Usage.Default)
		: this(alias, type, parameters, declaringType, GetLines(wild), access, usage, wild.ScriptTrace) { }

		public ScriptMethod(string alias, string type, Variable[] parameters, ScriptClass declaringType, ScriptLine[] phrases,
		  Access access, Usage usage, ScriptTrace trace)
		: base(alias, type, access, usage, declaringType, trace) {

			if(string.IsNullOrEmpty(alias))
				throw new ArgumentException("Argument cannot be null or empty.", nameof(alias));
			if(string.IsNullOrEmpty(type))
				throw new ArgumentException("Argument cannot be null or empty.", nameof(type));
			if(trace is null)
				throw new ArgumentNullException(nameof(trace));
			if(!Variable.Compilers.TryGetValue(type, out var func))
				throw new Compiler.SyntaxException($"Type '{type}' does not exist.", phrases[0].ScriptTrace);

			ScriptLines = phrases ?? throw new ArgumentNullException(nameof(phrases));
			DeclaringType?.Overloads.Add(this);
			Parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
			ReturnValue = func.Invoke(Access.Private, Usage.Default, Variable.GetNextHiddenID(), Compiler.CurrentScope, trace);

			Func = (arguments) => {
				for(int i = 0; i < arguments.Length; i++) {
					Variable argument = arguments[i], parameter = Parameters[i];
					if(argument.GetType().IsAssignableFrom(parameter.GetType()) || argument.TryCast(parameter.GetType(), out argument)) {
						parameter.InvokeOperation(Variable.Operation.Set, argument, Compiler.AnonScriptTrace);
					} else throw new Variable.InvalidCastException(argument, parameter.TypeName, trace);
				}
				new Spy(null, $"function {GameName}", null);
				return ReturnValue;
			};

			int length = phrases.Length;
			string[] array = new string[length];
			for(int i = 0; i < length; i++) array[i] = phrases[i];
			string str = "";
			foreach(string s in array) str += " " + s;
			toString = str.Length > 0 ? str[1..] : "";

		}


		public static ScriptLine[] GetLines(ScriptWild wild) {
			if(wild.FullBlockType == "{\\;\\}") {
				ScriptLine[] array = new ScriptLine[wild.Wilds.Count];
				for(int i = 0; i < wild.Wilds.Count; i++) array[i] = new ScriptLine(wild.Wilds[i]);
				return array;
			} else if(wild.SeparationType == ' ') {
				return new ScriptLine[] { new ScriptLine(wild.Array) };
			} else throw new Exception();
		}

		public static ScriptLine[] GetLines(ScriptString function) {
			var lines = new List<ScriptLine>();

			bool inString = false;
			int start = 0;
			var stack = new Stack<string>();
			for(int end = 0; end < function.Length; end++) {
				bool inBlock = stack.Count > 0;
				char chr = (char)function[end];

				if(chr == ';' && !inBlock && !inString) {
					lines.Add(new ScriptLine(function[start..end]));
					start = end + 1;
				} else {
					//Check if starting/ending a string.
					if(chr == '"' && (end == 0 || (char)function[end - 1] != '\\')) {
						inString = !inString;
						if(inString) stack.Push("\"\\\"");
						else stack.Pop();
					} else {
						if(char.IsWhiteSpace(chr)) {
							string s = (string)function[start..end];
							if(Statement.Dictionary.TryGetValue(s.Trim(), out Tuple<Statement.Reader, Statement.Writer> statement)) {
								//Read a statement.
								statement.Item1.Invoke(ref lines, ref start, ref end, ref function);
							}
						} else if(ScriptLine.IsBlockCharStart(chr, out string block)) {
							string s = (string)function[start..end];
							if(Statement.Dictionary.TryGetValue(s.Trim(), out Tuple<Statement.Reader, Statement.Writer> statement)) {
								//Read a statement.
								statement.Item1.Invoke(ref lines, ref start, ref end, ref function);
							} else {
								//Start a block.
								stack.Push(block);
							}
						} else if(ScriptLine.IsBlockCharEnd(chr, out block)) {
							//Check for mistakes.
							if(!inBlock) throw new Compiler.SyntaxException(
								"Got too many end-of-block characters without enough start-of-block characters to match!", function.ScriptTrace);
							if(stack.Peek() != block) throw new Compiler.SyntaxException(
								"Expected a different end-of-block character.", function.ScriptTrace);
							//If not a mistake, then end the block.
							stack.Pop();
						}
					}
				}

			}

			return lines.Count > 0 ? lines.ToArray()
				: (new ScriptLine[] { new ScriptLine(new ScriptWild[] { new ScriptWild(new ScriptWord(new ScriptString(
					"", function.ScriptTrace.FilePath, function.ScriptTrace.FileLine))) }) });
		}

		public IEnumerator<ScriptLine> GetEnumerator() => ((IReadOnlyList<ScriptLine>)ScriptLines).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => ((IReadOnlyList<ScriptLine>)ScriptLines).GetEnumerator();

		public override string ToString() => $"{ScriptTrace}: {toString}";

	}

}
