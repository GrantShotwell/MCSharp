using MCSharp.Statements;
using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MCSharp.Compilation {

	/// <summary>
	/// Represents an organized character structure of some class member.
	/// </summary>
	public abstract class ScriptMember {
		protected string alias;
		protected string typeName;
		protected ScriptObject declaringType;
		private Compiler.Scope scope;

		/// <summary>The in-code keyword for this member.</summary>
		public virtual string Alias {
			[DebuggerStepThrough]
			get {
				if(alias != null) return alias;
				else throw new FakeReadonlyException(false);
			}
			[DebuggerStepThrough]
			set {
				if(alias == null) alias = value;
				else throw new FakeReadonlyException(true);
			}
		}
		/// <summary>The alias of this member with its declaring type's <see cref="ScriptObject.FullAlias"/> shown as well.</summary>
		public string FullAlias {
			[DebuggerStepThrough]
			get => DeclaringType is null ? Alias : $"{DeclaringType.FullAlias}.{Alias}";
		}

		/// <summary>The alias of this member with its declaring type's <see cref="ScriptObject.Alias"/> shown as well.</summary>
		public string PartialAlias {
			[DebuggerStepThrough]
			get => DeclaringType is null ? Alias : $"{DeclaringType.Alias}.{Alias}";
		}

		/// <summary>The name of the '.mcfunction' folder.</summary>
		public string FileName {
			[DebuggerStepThrough]
			get => Script.FixAlias($"{FullAlias.Replace('.', '\\')}.mcfunction");
		}

		/// <summary>The path of the '.mcfunction' folder.</summary>
		public string FilePath {
			[DebuggerStepThrough]
			get => $"{Program.FunctionsFolder}\\{FileName}";
		}

		/// <summary>The name of this member if Minecraft gave it a function name.</summary>
		public string GameName {
			[DebuggerStepThrough]
			get => $"{Program.Datapack.Name}:{Script.FixAlias(FullAlias.Replace('.', '/').Replace('\\', '/'))}";
		}

		/// <summary>The <see cref="Variable.TypeName"/> for the type of this member.</summary>
		public virtual string TypeName {
			[DebuggerStepThrough]
			get {
				if(typeName != null) return typeName;
				else throw new FakeReadonlyException(false);
			}
			[DebuggerStepThrough]
			set {
				if(typeName == null) typeName = value;
				else throw new FakeReadonlyException(true);
			}
		}
		/// <summary>The <see cref="ScriptObject"/> that defines this member.</summary>
		public virtual ScriptObject DeclaringType {
			[DebuggerStepThrough]
			get {
				if(declaringType != null) return declaringType;
				else throw new FakeReadonlyException(false);
			}
			[DebuggerStepThrough]
			set {
				if(declaringType == null) declaringType = value;
				else throw new FakeReadonlyException(true);
			}
		}
		public Access Access { get; }
		public Usage Usage { get; }
		public ScriptTrace ScriptTrace { get; }
		public virtual Compiler.Scope Scope { get; }


		public ScriptMember(string alias, string type, Access access, Usage usage, ScriptObject declarer, ScriptTrace trace) {
			ScriptTrace = trace ?? throw new ArgumentNullException(nameof(trace));
			DeclaringType = declarer;
			TypeName = type;
			Access = access;
			Usage = usage;
			Alias = alias;
			Scope = Compiler.CurrentScope;
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
							//If no mistakes, then end the block.
							stack.Pop();
						}
					}
				}

			}

			return lines.Count > 0 ? lines.ToArray()
				: (new ScriptLine[] { new ScriptLine(new ScriptWild[] { new ScriptWild(new ScriptWord(new ScriptString(
					"", function.ScriptTrace.FilePath, function.ScriptTrace.FileLine))) }) });
		}


		public static ScriptLine[] GetLines(ScriptWild wild) {
			if(wild.FullBlockType == "{\\;\\}") {
				ScriptLine[] array = new ScriptLine[wild.Wilds.Count];
				for(int i = 0; i < wild.Wilds.Count; i++) array[i] = new ScriptLine(wild.Wilds[i]);
				return array;
			} else if(wild.Separator == ' ') {
				return new ScriptLine[] { new ScriptLine(wild.Array) };
			} else throw new ArgumentException("Block/Separation was not correct, which means there was a parsing error somewhere.", nameof(wild));
		}

		public class FakeReadonlyException : Compiler.InternalError {
			public FakeReadonlyException(bool set, [CallerMemberName] string caller = "")
			: base($"Cannot {(set ? "set" : "get")} {caller} because " +
			  $"{(set ? "the value has already been set" : "the value has not been set")}.") { }
		}

	}

	public static class ScriptMemberExtensions {
		public static void Add(this Dictionary<string, ScriptMember> dictionary, ScriptMember member) => dictionary.Add(member.Alias, member);
	}

}
