using MCSharp.Compilation;
using MCSharp.Variables;
using System;
using System.Collections.Generic;

namespace MCSharp.Statements {
	public class StmtReturn : Statement {

		public override string Call => "return";

		public override void Read(ref List<ScriptLine> lines, ref int start, ref int end, ref ScriptString function) {
			//Just need " \\return\\ \\(\\...\\)\\ ;".
			start = end;
			var blocks = new Stack<string>();
			var statement = new ScriptWild(function[(end - "return".Length)..end]);
			for(start = end; end < function.Length; end++) {
				ScriptChar character = function[end];
				if(blocks.Count == 0 && char.IsWhiteSpace((char)character)) {
					//nothing??
				} else if(ScriptLine.IsBlockCharStart((char)character, out string block)) {
					blocks.Push(block);
				} else if(ScriptLine.IsBlockCharEnd((char)character, out block)) {
					var pop = blocks.Pop();
					if(pop != block) throw new Compiler.SyntaxException($"Expected '{pop[2]}' but got '{block[2]}'.", character.ScriptTrace);
				} else if(character == ';' && blocks.Count == 0) {
					// <<End of Return>>
					break;
				}
			}
			lines.Add(new ScriptLine(new ScriptWild[] { statement, ScriptLine.GetWilds(function[start..end]) }));
			start = ++end;
		}

		public override void Write(ScriptLine line) {
			var method = Compiler.CurrentScope.DeclaringMethod;
			if(method == null) throw new Compiler.SyntaxException("The 'return' statement requires a method.", line.ScriptTrace);
			Variable value = Compiler.ParseValue(line[1], Compiler.CurrentScope);
			method.ReturnValue.InvokeOperation(Variable.Operation.Set, value, line.ScriptTrace);
		}

	}
}
