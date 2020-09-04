using MCSharp.Compilation;
using MCSharp.Variables;
using System;
using System.Collections.Generic;
using static MCSharp.Compilation.ScriptObject;

namespace MCSharp.Statements {

	public class StmtIf : Statement {

		public override string Call => "if";

		public override void Read(ref List<ScriptLine> list, ref int start, ref int end, ref ScriptString function) {

			var wilds = new List<ScriptWild> { new ScriptWord(new ScriptString("if", Program.ScriptsFolder + "60302262020")) };

			end--;
			var stack = new Stack<string>();
			bool expectingCondition = true;
			bool expectingInstruction = false;

			while(++end < function.Length) {
				char chr = (char)function[end];

				// Skip whitespaces, since the parsing is done mostly done in the separate loops below.
				if(char.IsWhiteSpace(chr)) continue;

				if(expectingCondition) {
					// <<Expecting Condition>>
					if(chr == '(') stack.Push("(\\)");
					else throw new Compiler.SyntaxException("Expected '(...)' after keyword 'if'.", Compiler.CurrentScriptTrace);
					start = end;
					// Find the end of the condition parenthesies.
					while(++end < function.Length) {
						chr = (char)function[end];
						if(ScriptLine.IsBlockCharStart(chr, out string block)) {
							// Start a new block.
							stack.Push(block);
						} else if(ScriptLine.IsBlockCharEnd(chr, out block)) {
							// End the current block.
							string b = stack.Pop();
							if(block != b) throw new Compiler.SyntaxException($"Expected '{b[2]}', but got '{block[2]}'.", Compiler.CurrentScriptTrace);
							if(stack.Count == 0) /* <<End of Conditional>> */ break;
						}
					}
					// Get the full conditional as a string.
					ScriptString s = function[start..(end + 1)];
					// Parse the conditional using ScriptLine.GetWilds(...).
					var parsed = ScriptLine.GetWilds(s);
					// Add the conditional to the list.
					wilds.Add(parsed);

					// No longer expecting condition.
					expectingCondition = false;
					// Next is an instruction.
					expectingInstruction = true;


				} else if(expectingInstruction) {
					// <<Expecting Instruction>>
					if(chr == '{') {
						// <<Parsing Code Block>>
						stack.Push("{\\}");
						start = end;
						//Find the end of the code block brackets.
						while(++end < function.Length) {
							chr = (char)function[end];
							if(ScriptLine.IsBlockCharStart(chr, out string block)) {
								// Start a new block.
								stack.Push(block);
							} else if(ScriptLine.IsBlockCharEnd(chr, out block)) {
								// End the current block.
								string b = stack.Pop();
								if(block != b) throw new Compiler.SyntaxException($"Expected '{b[2]}', but got '{block[2]}'.", Compiler.CurrentScriptTrace);
								if(stack.Count == 0) break; // End of Code Block
							}
						}
						// Get the full code block as a string.
						ScriptString s = function[start..(end + 1)];
						// Parse the code block using ScriptLine.GetWilds(...).
						var parsed = ScriptLine.GetWilds(s);
						// Add the code block to the list.
						wilds.Add(parsed);

					} else {
						// <<Parsing Single Line>>
						start = end;
						// Find the ';'.
						while(++end < function.Length) {
							chr = (char)function[end];
							if(ScriptLine.IsBlockCharStart(chr, out string block)) {
								//Start a new block.
								stack.Push(block);
							} else if(ScriptLine.IsBlockCharEnd(chr, out block)) {
								//End the current block.
								string b = stack.Pop();
								if(block != b) throw new Compiler.SyntaxException($"Expected '{b[2]}', but got '{block[2]}'.", Compiler.CurrentScriptTrace);
							} else if(stack.Count == 0) {
								if(chr != ';') throw new Compiler.SyntaxException("Expected ';'.", Compiler.CurrentScriptTrace);
								else break; // End of Statement.
							}
						}
                        // Parse the conditional using ScriptLine.GetWilds(...).
                        ScriptWild parsed = ScriptLine.GetWilds(function[start..end]);
						// Add the conditional to the list.
						wilds.Add(parsed);
					}

					// No longer expecting an instruction.
					expectingInstruction = false;
					// Next is 'else', if at all.


				} else {
					// <<Expecting 'else' Statement>>
					if(!LookFor("else", (string)function, start, end, out int i)) {
						// <<Parsing 'else' Statement>>
						end = i - 1 + 4;
						start = end - 3;
						bool parsingBlock = false, parsingSingle = false;
						while(++end < function.Length) {
							chr = (char)function[end];

							if(ScriptLine.IsBlockCharStart(chr, out string block)) {
								stack.Push(block);
							} else if(ScriptLine.IsBlockCharEnd(chr, out block)) {
								string b = stack.Pop();
								if(block != b) throw new Compiler.SyntaxException($"Expected '{b[2]}', but got '{block[2]}'.", Compiler.CurrentScriptTrace);
							}

							if(parsingBlock) {
								// <<Finding an End of Block>>
								if(chr == '}' && stack.Count == 0) break;
							} else if(parsingSingle) {
								// <<Finding an End of Instruction>>
								if(chr == ';' && stack.Count == 0) break;
							} else {
								// Find what to find.
								if(!char.IsWhiteSpace(chr)) {
									if(chr == '{') parsingBlock = true;
									else parsingSingle = true;
								}
							}
						}
                        // Parse the instruction using ScriptLine.GetWilds(...).
                        ScriptWild parsed = ScriptLine.GetWilds(function[start..(end + 1)]);
						// Add the instruction to the list.
						wilds.Add(parsed[0]);
						wilds.Add(parsed[1..2]);
						start = end += 2;
						break;

					} else {
						// <<No 'else' Statement>>
						start = end++;
						break;
					}
				}

			}

			// Add 'wilds' as the ScriptLine.
			list.Add(new ScriptLine(wilds.ToArray()));

		}

		public override void Write(ScriptLine line) {

			if(line.Length > 5) throw new Compiler.InternalError("053408302020");
			ScriptWild conditionWild = line[1];
			ScriptWild statementWild = line[2];
			ScriptWild? elseWild = line.Length > 4 ? (ScriptWild?)line[4] : null;
			VarBool condition;

			Variable conditionVariable = Compiler.ParseValue(conditionWild, Compiler.CurrentScope);
			if(conditionVariable is VarBool varBool || conditionVariable.TryCast(out varBool)) condition = varBool;
			else throw new Variable.InvalidArgumentsException($"Could not cast '{conditionVariable}' as 'bool'.", line.ScriptTrace);
			
			{

				var statement = new ScriptMethod(Compiler.CurrentScope.GetNextAnonMethodAlias(), "void", new Variable[] { }, null, statementWild, Compiler.CurrentScope) {
					DeclaringType = Compiler.CurrentScope.DeclaringType,
					Anonymous = true
				};

				if(condition.Usage != Usage.Constant) {
					Compiler.WriteFunction<VarVoid>(Compiler.CurrentScope, null, statement);
					new Spy(null, $"execute if score {condition.Selector.GetConstant()} {condition.Objective.GetConstant()} matches 1.. run function {statement.GameName}", null);
				} else if(condition.Constant >= 1) {
					Compiler.WriteFunction<VarVoid>(Compiler.CurrentScope, null, statement);
					new Spy(null, $"function {statement.GameName}", null);
				}

			}

			if(elseWild.HasValue) {

				var statement = new ScriptMethod(Compiler.CurrentScope.GetNextAnonMethodAlias(), "void", new Variable[] { }, null, elseWild.Value, Compiler.CurrentScope) {
					DeclaringType = Compiler.CurrentScope.DeclaringType,
					Anonymous = true
				};

				if(condition.Usage != Usage.Constant) {
					Compiler.WriteFunction<VarVoid>(Compiler.CurrentScope, null, statement);
					new Spy(null, $"execute if score {condition.Selector.GetConstant()} {condition.Objective.GetConstant()} matches 1.. run function {statement.GameName}", null);
				} else if(condition.Constant < 1) {
					Compiler.WriteFunction<VarVoid>(Compiler.CurrentScope, null, statement);
					new Spy(null, $"function {statement.GameName}", null);
				}

			}
		}

		private static bool LookFor(string item, string function, int start, int end, out int i) {
			bool whitespace = true;
			while(++end < function.Length) {
				i = end - 1;
				if(char.IsWhiteSpace(function[end]) && whitespace) start = end + 1;
				else {
					whitespace = false;
					string s = function[start..end];
					if(s.Length >= item.Length) return s == item;
					else continue;
				}
			}
			i = end - 1;
			return false;
		}

	}

}
