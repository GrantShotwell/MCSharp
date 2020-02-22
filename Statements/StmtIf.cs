using MCSharp.Compilation;
using MCSharp.Variables;
using System;
using System.Collections.Generic;
using System.Text;

namespace MCSharp.Statements {

    public class StmtIf : Statement {

        public override string Call => "if";

        public override void Read(ref List<ScriptLine> list, ref int start, ref int end, ref string function) {

            var wilds = new List<ScriptWild> { new ScriptWord("if") };

            end--;
            var stack = new Stack<string>();
            bool expectingCondition = true;
            bool expectingBlock = false;

            while(++end < function.Length) {
                char chr = function[end];

                if(expectingCondition) {
                    if(char.IsWhiteSpace(chr)) start = end + 1;
                    else if(chr == '(') {
                        expectingCondition = false;
                        expectingBlock = true;
                        stack.Push("(\\)");
                        start = end + 1;
                        while(++end < function.Length) {
                            chr = function[end];
                            if(ScriptLine.IsBlockCharStart(chr, out string block)) {
                                stack.Push(block);
                                continue;
                            } else if(ScriptLine.IsBlockCharEnd(chr, out block)) {
                                string b = stack.Pop();
                                if(b != block) throw new Compiler.SyntaxException($"Expected '{b[2]}' but got '{block[2]}'.");
                                if(stack.Count == 0) {
                                    ScriptWild[] w = ScriptLine.GetWilds(function[start..end]);
                                    wilds.Add(new ScriptWild(w, block, ','));
                                    break;
                                }
                            }
                        }
                        continue;
                    } else throw new Compiler.SyntaxException("Expected '(' for 'if' statement.");
                } else if(expectingBlock) {
                    if(char.IsWhiteSpace(chr)) start = end + 1;
                    else if(chr == '{') {
                        expectingBlock = false;
                        stack.Push("{\\}");
                        start = end + 1;
                        while(++end < function.Length) {
                            chr = function[end];
                            if(ScriptLine.IsBlockCharStart(chr, out string block)) {
                                stack.Push(block);
                                continue;
                            } else if(ScriptLine.IsBlockCharEnd(chr, out block)) {
                                string b = stack.Pop();
                                if(b != block) throw new Compiler.SyntaxException($"Expected '{b[2]}' but got '{block[2]}'.");
                                if(stack.Count == 0) {
                                    ScriptWild[] w = ScriptLine.GetWilds(function[start..end]);
                                    wilds.Add(new ScriptWild(w, "{\\}", ' '));
                                    break;
                                }
                            }
                        }
                        if(stack.Count == 0) {
                            var wild = new ScriptWild(wilds.ToArray(), " \\ ", ' ');
                            var line = new ScriptLine(wild);
                            list.Add(line);
                            start = ++end;
                            break;
                        }
                    } else throw new Compiler.SyntaxException("Expected '{' for 'if' statement.");
                } else {
                    // Expecting 'else'.
                    //TODO
                }

            }
        }

        public override void Write(ScriptLine line) {
            ScriptWild conditionWild = line[1];
            ScriptWild statementWild = line[2];
            if(Compiler.TryParseValue(conditionWild, Compiler.CurrentScope, out Variable conditionVariable)) {
                if(conditionVariable is VarBool varBool || conditionVariable.TryCast(out varBool)) {
                    var statement = new ScriptFunction($"{Compiler.CurrentScope}\\{Compiler.CurrentScope.GetNextInnerID()}", statementWild);
                    Compiler.WriteFunction(Compiler.CurrentScope, statement);
                    new Spy(null, $"execute if score {varBool.Selector.GetConstant()} {varBool.Objective.GetConstant()} matches 1.. " +
                        $"run function {statement.GamePath}", null);
                } else throw new Variable.InvalidArgumentsException($"Could not cast '{conditionVariable}' as a 'bool'.");
            } else throw new Exception();
        }

    }

}
