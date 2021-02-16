using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MCSharp.Script {

	public struct ScriptMember {

		public static Regex NameRegex { get; } = new Regex(@"[a-zA-Z_][a-zA-Z0-9_]*");
		public static IReadOnlyCollection<string> ModifierList { get; } = new string[] {
			"public",
			"private",
			"static",
			"const",
			"readonly",
			"abstract",
			"virtual",
			"override"
		};

		public IReadOnlyCollection<ScriptToken> Modifiers { get; }
		public ScriptToken ReturnType { get; }
		public ScriptToken Name { get; }
		public MemberType MemberType { get; }
		public IReadOnlyList<ScriptToken> Definition { get; }

		public ScriptTrace Trace => Name.Trace;


		private ScriptMember(TokenList prefixes, ScriptToken returnType, ScriptToken name, MemberType memberType, TokenList definition) {
			Modifiers = prefixes?.ToArray() ?? throw new ArgumentNullException(nameof(prefixes));
			ReturnType = returnType;
			Name = name;
			MemberType = memberType;
			Definition = definition?.ToArray() ?? throw new ArgumentNullException(nameof(definition));
		}

		/// <summary>
		/// prefixes name (field_def|property_def|method_def)
		/// </summary>
		public static bool ReadMember(ref TokenReader reader, out string message, out ScriptMember? result) {

			// prefixes
			if(!ReadPrefixes(ref reader, out message, out TokenList prefixes, out ScriptToken? returnType)) {
				result = null;
				return false;
			}

			// name
			if(!reader.ReadNextToken(NameRegex, "identifier", out message, out ScriptToken? name)) {
				result = null;
				return false;
			}

			// (field_def|property_def|method_def)
			MemberType memberType;
			TokenList definition = null;
			{

				TokenReader branch = reader.Branch();
				if(!branch.ReadAnyNextToken("first token after member name definition", out string noFirstToken, out ScriptToken? first)) {
					result = null;
					message = noFirstToken;
					return false;
				}
				string firstToken = (string)first.Value;

				if(firstToken == ";" || firstToken == "=") {

					memberType = MemberType.Field;
					if(!ReadFieldDefinition(ref reader, out message, out definition)) {
						result = null;
						return false;
					}

				} else if(firstToken == "(") {

					memberType = MemberType.Method;
					if(!ReadMethodDefinition(ref reader, out message, out definition)) {
						result = null;
						return false;
					}

				} else if(firstToken == "{" || firstToken == "=>") {

					memberType = MemberType.Property;
					if(!ReadPropertyDefinition(ref reader, out message, out definition)) {
						result = null;
						return false;
					}

				} else {

					result = null;
					message = $"Expected first token in member definition, found '{firstToken}'.";
					return false;

				}

			}

			result = new ScriptMember(prefixes, returnType.Value, name.Value, memberType, definition);
			message = Compiler.DefaultSuccess;
			return true;

		}

		public static bool ReadPrefixes(ref TokenReader reader, out string message, out TokenList prefixes, out ScriptToken? type) {
			
			// {modifier}
			prefixes = new TokenList();
			while(true) {
				TokenReader branch = reader.Branch();
				if(!branch.MoveNext()) break;
				if(!ModifierList.Contains((string)branch.Current)) break;
				prefixes.AddLast(branch.Current);
				reader = branch;
			}

			// type
			if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out type)) return false;
			else return true;

		}

		/// <summary>
		/// ["=" expression] ";"
		/// </summary>
		public static bool ReadFieldDefinition(ref TokenReader reader, out string message, out TokenList result) {
			
			result = new TokenList();

			// ["=" expression]
			{
				TokenReader branch = reader.Branch();
				TokenList init = new TokenList();

				// "="
				if(!branch.ReadNextToken("=", out _, out ScriptToken? eq)) goto Break0;
				else init.AddFirst(eq.Value);

				// expression
				if(!ReadExpression(ref branch, out _, out TokenList rv)) goto Break0;
				else init.AddLast(rv);

				reader = branch;
				result.AddLast(init);
			}
			Break0:

			// ";"
			if(!reader.ReadNextToken(";", out message, out ScriptToken? sc)) return false;
			else result.AddLast(sc.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// ("=>" expression)|("{" ([modifiers "get" ";"][modifiers "set" ";"])|([modifiers "get" code_block][modifiers "set" code_block]) "}")
		/// </summary>
		public static bool ReadPropertyDefinition(ref TokenReader reader, out string message, out TokenList property_definition) {
			property_definition = new TokenList();

			bool or0 = true;

			// ("=>" expression)
			{
				TokenReader branch = reader.Branch();
				TokenList def = new TokenList();

				// "=>"
				if(!branch.ReadNextToken("=>", out _, out ScriptToken? gt)) goto Break0;
				else def.AddLast(gt.Value);

				// statement
				if(!ReadStatement(ref branch, out message, out TokenList stmt)) return false;
				else def.AddLast(stmt);

				or0 = false;
				property_definition.AddLast(def);
			}
			Break0:

			// |OR|

			// ("{" ([modifiers "get" ";"][modifiers "set" ";"])|([modifiers "get" code_block][modifiers "set" code_block]) "}")
			if(or0) {

				// "{"
				if(!reader.ReadNextToken("{", out message, out ScriptToken? oc)) return false;
				else property_definition.AddLast(oc.Value);

				// ([modifiers "get" ";"][modifiers "set" ";"])|([modifiers "get" code_block][modifiers "set" code_block])
				{
					bool or1 = true;

					// (["get" ";"]["set" ";"])
					if(or1) {
						TokenReader branch = reader.Branch();
						TokenList list = new TokenList();
						bool locked = false;

						// ["get" ";"]
						{
							
							// "get"

							// ";"

						}
						Break2:

						// ["set" ";"]
						{

							// "set"

							// ";"

						}
						Break3:;

					}
					Break1:

					// |OR|

					// (["get" code_block]["set" code_block])
					if(or1) {
						TokenReader branch = reader.Branch();
						TokenList list = new TokenList();
						bool locked = false;

						// ["get" code_block]
						{

							// "get"

							// code_block

						}
						Break4:

						// ["set" code_block]
						{

							// "set"

							// code_block

						}
						Break5:;

						reader = branch;
						property_definition.AddLast(list);
					}

				}

				// "}"
				if(!reader.ReadNextToken("}", out message, out ScriptToken? cc)) return false;
				else property_definition.AddLast(cc.Value);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// parameters code_block|("=>" expression)
		/// </summary>
		public static bool ReadMethodDefinition(ref TokenReader reader, out string message, out TokenList method_definition) {
			method_definition = new TokenList();

			// parameters
			if(!ReadParameters(ref reader, out message, out TokenList parameters)) return false;
			else method_definition.AddLast(parameters);

			// code_block
			if(!ReadCodeBlock(ref reader, out message, out TokenList code_block)) return false;
			else method_definition.AddLast(code_block);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "(" [type identifier {"," type identifier}] ")"
		/// </summary>
		public static bool ReadParameters(ref TokenReader reader, out string message, out TokenList parameters) {
			parameters = new TokenList();

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else parameters.AddLast(op.Value);

			// [type identifier {"," type identifier}]
			if(!reader.LookAhead(")")) {

				// type
				if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? first_type)) return false;
				else parameters.AddLast(first_type.Value);

				// identifier
				if(!reader.ReadNextToken(NameRegex, "identifier", out message, out ScriptToken? first_name)) return false;
				else parameters.AddLast(first_name.Value);

				// {"," type identifier}
				while(!reader.LookAhead(")")) {

					// ","
					if(!reader.ReadNextToken(",", out message, out ScriptToken? comma)) return false;
					else parameters.AddLast(comma.Value);

					// type
					if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? type)) return false;
					else parameters.AddLast(type.Value);

					// identifier
					if(!reader.ReadNextToken(NameRegex, "identifier", out message, out ScriptToken? name)) return false;
					else parameters.AddLast(name.Value);

				}

			}

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else parameters.AddLast(cp.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "{" {statement} "}"
		/// </summary>
		public static bool ReadCodeBlock(ref TokenReader reader, out string message, out TokenList code_block) {
			code_block = new TokenList();

			// "{"
			if(!reader.ReadNextToken("{", out message, out ScriptToken? oc)) return false;
			else code_block.AddLast(oc.Value);

			// {statement}
			while(!reader.LookAhead("}")) {

				// statement
				if(!ReadStatement(ref reader, out message, out TokenList statement)) return false;
				else code_block.AddLast(statement);

			}

			// "}"
			if(!reader.ReadNextToken("}", out message, out ScriptToken? cc)) return false;
			else code_block.AddLast(cc.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "{" [identifier "=" expression] {"," identifier "=" expression} "}"
		/// </summary>
		public static bool ReadPropertyBlock(ref TokenReader reader, out string message, out TokenList property_block) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// "{" [expression] {"," expression} "}"
		/// </summary>
		public static bool ReadListBlock(ref TokenReader reader, out string message, out TokenList list_block) {
			throw new NotImplementedException();
		}

		/// <summary>
		/// code_block|lang_func|(initialization ";")|(expression ";")
		/// </summary>
		public static bool ReadStatement(ref TokenReader reader, out string message, out TokenList statement) {
			statement = new TokenList();

			bool or0 = true;

			// code_block
			{
				TokenReader branch = reader.Branch();
				TokenList list = new TokenList();
				bool locked = branch.LookAhead("{");

				// code_block
				if(!ReadCodeBlock(ref branch, out message, out TokenList code_block))
					if(locked) return false;
					else goto Break0;
				else list.AddLast(code_block);

				or0 = false;
				reader = branch;
				statement.AddLast(list);
			}
			Break0:

			// |OR|

			// lang_func
			if(or0) {
				TokenReader branch = reader.Branch();
				TokenList list = new TokenList();

				// lang_func
				if(!ReadLangFunc(ref branch, out message, out TokenList lang_func))
					if(message != Compiler.DefaultNoPath) return false;
					else goto Break1;
				else list.AddLast(lang_func);

				or0 = false;
				reader = branch;
				statement.AddLast(list);
			}
			Break1:

			// |OR|

			// initialize ";"
			if(or0) {

				{

					TokenReader branch = reader.Branch();

					// type?
					if(!branch.ReadNextToken(ScriptClass.NameRegex, "type name", out _, out  _)) goto Break2;
					// name?
					if(!branch.ReadNextToken(NameRegex, "variable name", out _, out _)) goto Break2;
					
				}

				// initialize
				if(!ReadInitialization(ref reader, out message, out TokenList initialization)) return false;
				else statement.AddLast(initialization);

				// ";"
				if(!reader.ReadNextToken(";", out message, out ScriptToken? sc)) return false;
				else statement.AddLast(sc.Value);

				or0 = false;

			}
			Break2:

			// |OR|

			// expression ";"
			if(or0) {
				TokenReader branch = reader.Branch();
				TokenList list = new TokenList();

				// expression
				if(!ReadExpression(ref branch, out message, out TokenList expression)) return false;
				else list.AddLast(expression);

				// ";"
				if(!branch.ReadNextToken(";", out message, out ScriptToken? sc)) return false;
				else list.AddLast(sc.Value);

				reader = branch;
				statement.AddLast(list);
			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// if|for|while|do|return|throw|try
		/// </summary>
		public static bool ReadLangFunc(ref TokenReader reader, out string message, out TokenList lang_func) {
			lang_func = new TokenList();

			if(!reader.LookAhead(out ScriptToken? token)) {
				message = Compiler.DefaultNoPath;
				return false;
			}

			switch((string)token) {
				case "if":
					if(!ReadIfLF(ref reader, out message, out lang_func)) return false;
					else return true;
				case "for":
					if(!ReadForLF(ref reader, out message, out lang_func)) return false;
					else return true;
				case "while":
					if(!ReadWhileLF(ref reader, out message, out lang_func)) return false;
					else return true;
				case "do":
					if(!ReadDoLF(ref reader, out message, out lang_func)) return false;
					else return true;
				case "return":
					if(!ReadReturnLF(ref reader, out message, out lang_func)) return false;
					else return true;
				case "throw":
					if(!ReadThrowLF(ref reader, out message, out lang_func)) return false;
					else return true;
				case "try":
					if(!ReadTryLF(ref reader, out message, out lang_func)) return false;
					else return true;
				default:
					message = Compiler.DefaultNoPath;
					return false;
			}

		}

		/// <summary>
		/// "if" "(" expression ")" statement {"else" "if" "(" expression ")" statement} ["else" statement]
		/// </summary>
		public static bool ReadIfLF(ref TokenReader reader, out string message, out TokenList @if) {
			@if = new TokenList();

			// "if"
			if(!reader.ReadNextToken("if", out message, out ScriptToken? if_token0)) return false;
			else @if.AddLast(if_token0.Value);

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op0)) return false;
			else @if.AddLast(op0.Value);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList condition0)) return false;
			else @if.AddLast(condition0);

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp0)) return false;
			else @if.AddLast(cp0.Value);

			// statement
			if(!ReadStatement(ref reader, out message, out TokenList statement0)) return false;
			else @if.AddLast(statement0);

			// {"else" "if" "(" expression ")" statement}
			while(true) {
				TokenReader branch = reader.Branch();
				TokenList list = new TokenList();

				// "else"
				if(!branch.ReadNextToken("else", out _, out ScriptToken? else1)) break;
				else list.AddLast(else1.Value);

				// "if"
				if(!branch.ReadNextToken("if", out _, out ScriptToken? if1)) break;
				else list.AddLast(if1.Value);

				reader = branch;
				@if.AddLast(list);

				// "("
				if(!reader.ReadNextToken("(", out message, out ScriptToken? op1)) return false;
				else @if.AddLast(op1.Value);

				// expression
				if(!ReadExpression(ref reader, out message, out TokenList expression)) return false;
				else @if.AddLast(expression);

				// ")"
				if(!reader.ReadNextToken(")", out message, out ScriptToken? cp1)) return false;
				else @if.AddLast(cp1.Value);

				// statement
				if(!ReadStatement(ref reader, out message, out TokenList statement1)) return false;
				else @if.AddLast(statement1);

			}

			// ["else" statement]
			{
				if(!reader.LookAhead("else")) goto BreakElse;

				// "else"
				if(!reader.ReadNextToken("else", out message, out ScriptToken? else2)) return false;
				else @if.AddLast(else2.Value);

				// statement
				if(!ReadStatement(ref reader, out message, out TokenList statement2)) return false;
				else @if.AddLast(statement2);

			}
			BreakElse:

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "for" "(" initialization|expression ";" expression ";" expression ")" statement
		/// </summary>
		public static bool ReadForLF(ref TokenReader reader, out string message, out TokenList @for) {
			@for = new TokenList();

			// "for"
			if(!reader.ReadNextToken("for", out message, out ScriptToken? for_token)) return false;
			else @for.AddLast(for_token.Value);

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else @for.AddLast(op.Value);

			// initialization|expression
			{
				bool or0 = true;

				// initialization
				if(ReadInitialization(ref reader, out message, out TokenList initialization)) {
					@for.AddLast(initialization);
					or0 = false;
				}

				// |OR|

				// expression
				if(or0) {
					if(!ReadExpression(ref reader, out _, out TokenList expression)) return false;
					else @for.AddLast(expression);
				}

			}

			// ";"
			if(!reader.ReadNextToken(";", out message, out ScriptToken? sc0)) return false;
			else @for.AddLast(sc0.Value);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList returnable1)) return false;
			else @for.AddLast(returnable1);

			// ";"
			if(!reader.ReadNextToken(";", out message, out ScriptToken? sc1)) return false;
			else @for.AddLast(sc1.Value);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList returnable2)) return false;
			else @for.AddLast(returnable2);

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else @for.AddLast(cp.Value);

			// statement
			if(!ReadStatement(ref reader, out message, out TokenList statement)) return false;
			else @for.AddLast(statement);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "while" "(" expression ")" statement
		/// </summary>
		public static bool ReadWhileLF(ref TokenReader reader, out string message, out TokenList @while) {
			@while = new TokenList();

			// "while"
			if(!reader.ReadNextToken("while", out message, out ScriptToken? while_token)) return false;
			else @while.AddLast(while_token.Value);

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else @while.AddLast(op.Value);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList expression)) return false;
			else @while.AddLast(expression);

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else @while.AddLast(cp.Value);

			// statement
			if(!ReadStatement(ref reader, out message, out TokenList statement)) return false;
			else @while.AddLast(statement);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "do" statement "while" "(" expression ")" ";"
		/// </summary>
		public static bool ReadDoLF(ref TokenReader reader, out string message, out TokenList @do) {
			@do = new TokenList();

			// "do"
			if(!reader.ReadNextToken("do", out message, out ScriptToken? do_token)) return false;
			else @do.AddLast(do_token.Value);

			// statement
			if(!ReadStatement(ref reader, out message, out TokenList statement)) return false;
			else @do.AddLast(statement);

			// "while"
			if(!reader.ReadNextToken("while", out message, out ScriptToken? while_token)) return false;
			else @do.AddLast(while_token.Value);

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else @do.AddLast(op.Value);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList expression)) return false;
			else @do.AddLast(expression);

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else @do.AddLast(cp.Value);

			// ";"
			if(!reader.ReadNextToken(";", out message, out ScriptToken? sc)) return false;
			else @do.AddLast(sc.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "return" expression ";"
		/// </summary>
		public static bool ReadReturnLF(ref TokenReader reader, out string message, out TokenList @return) {
			@return = new TokenList();

			// "return"
			if(!reader.ReadNextToken("return", out message, out ScriptToken? return_token)) return false;
			else @return.AddLast(return_token.Value);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList expression)) return false;
			else @return.AddLast(expression);

			// ";"
			if(!reader.ReadNextToken(";", out message, out ScriptToken? sc)) return false;
			else @return.AddLast(sc.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "throw" expression ";"
		/// </summary>
		public static bool ReadThrowLF(ref TokenReader reader, out string message, out TokenList @throw) {
			@throw = new TokenList();

			// "throw"
			if(!reader.ReadNextToken("throw", out message, out ScriptToken? throw_token)) return false;
			else @throw.AddLast(throw_token.Value);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList expression)) return false;
			else @throw.AddLast(expression);

			// ";"
			if(!reader.ReadNextToken(";", out message, out ScriptToken? sc)) return false;
			else @throw.AddLast(sc.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "try" statement ["catch" parameters statement] {"catch" parameters statement} ["finally" statement]
		/// </summary>
		public static bool ReadTryLF(ref TokenReader reader, out string message, out TokenList @try) {
			@try = new TokenList();

			// "try"
			if(!reader.ReadNextToken("try", out message, out ScriptToken? try_token)) return false;
			else @try.AddLast(try_token.Value);

			// statement
			if(!ReadStatement(ref reader, out message, out TokenList statement0)) return false;
			else @try.AddLast(statement0);

			// ["catch" parameters statement]
			if(reader.LookAhead("catch")) {

				// "catch"
				if(!reader.ReadNextToken("catch", out message, out ScriptToken? catch0)) return false;
				else @try.AddLast(catch0.Value);

				// statement
				if(!ReadStatement(ref reader, out message, out TokenList statement1)) return false;
				else @try.AddLast(statement1);

			}

			// {"catch" parameters statement}+
			while(reader.LookAhead("catch")) {

				// "catch"
				if(!reader.ReadNextToken("catch", out message, out ScriptToken? catch1)) return false;
				else @try.AddLast(catch1.Value);

				// statement
				if(!ReadStatement(ref reader, out message, out TokenList statement2)) return false;
				else @try.AddLast(statement2);

			}

			// ["finally" statement]
			if(reader.LookAhead("finally")) {

				// "finally"
				if(!reader.ReadNextToken("finally", out message, out ScriptToken? finally0)) return false;
				else @try.AddLast(finally0.Value);

				// statement
				if(!ReadStatement(ref reader, out message, out TokenList statement3)) return false;
				else @try.AddLast(statement3);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// type identifier ["=" expression]
		/// </summary>
		public static bool ReadInitialization(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// type
			if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? type)) return false;
			else result.AddLast(type.Value);

			// identifier
			if(!reader.ReadNextToken(NameRegex, "identifier", out message, out ScriptToken? name)) return false;
			else result.AddLast(name.Value);

			// ["=" expression]
			if(reader.LookAhead("=")) {

				// "="
				if(!reader.ReadNextToken("=", out message, out ScriptToken? eq)) return false;
				else result.AddLast(eq.Value);

				// expression
				if(!ReadExpression(ref reader, out message, out TokenList expression)) return false;
				else result.AddLast(expression);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// 
		/// </summary>
		public static bool ReadIdentifier(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			if(!reader.ReadNextToken(NameRegex, "identifier", out message, out ScriptToken? identifier)) return false;
			else result.AddLast(identifier.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// assignment_operation|non_assignment_expression
		/// </summary>
		public static bool ReadExpression(ref TokenReader reader, out string message, out TokenList result) {

			bool assignment = false;
			{
				TokenReader lookahead = reader.Branch();
				bool move1 = lookahead.MoveNext();
				bool move2 = lookahead.MoveNext();
				bool op = ReadAssignmentOperator(ref lookahead, out _, out _);
				assignment = move1 && move2 && op;
			}

			// assignment_operation|non_assignment_expression
			if(assignment) {

				// assignment_operation
				if(!ReadAssignmentOperation(ref reader, out message, out result)) return false;
				message = Compiler.DefaultSuccess;
				return true;

			} else {

				// non_assignment_operation
				if(!ReadNonAssignmentOperation(ref reader, out message, out result)) return false;
				message = Compiler.DefaultSuccess;
				return true;

			}

		}

		/// <summary>
		/// "(" expression ")"
		/// </summary>
		public static bool ReadParenthesizedExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else result.AddLast(op.Value);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList expression)) return false;
			else result.AddLast(expression);

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else result.AddLast(cp.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// expression { "," expression }
		/// </summary>
		public static bool ReadExpressionList(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList expression0)) return false;
			else result.AddLast(expression0);

			// { "," expression }
			while(reader.LookAhead(",")) {

				// ","
				if(!reader.ReadAnyNextToken(",", out message, out ScriptToken? cm)) return false;
				else result.AddLast(cm.Value);

				// expression
				if(!ReadExpression(ref reader, out message, out TokenList expression1)) return false;
				else result.AddLast(expression1);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// unary_experssion assignment_operator expression
		/// </summary>
		public static bool ReadAssignmentOperation(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// unary_expression
			if(!ReadUnaryExpression(ref reader, out message, out TokenList unary_expression)) return false;
			else result.AddLast(unary_expression);

			// assignment_operator
			if(!ReadAssignmentOperator(ref reader, out message, out TokenList assignment_operator)) return false;
			else result.AddLast(assignment_operator);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList expression)) return false;
			else result.AddLast(expression);

			message = Compiler.DefaultSuccess;
			return true;

		}

		public static bool ReadAssignmentOperator(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			string[] ops = new string[] { "=", "+=", "-=", "*=", "/=", "%=", "&=", "|=", "^=", "<<=", ">", ">=" };
			if(!reader.ReadNextToken(ops, "assignment operator", out message, out ScriptToken? op)) return false;
			result.AddLast(op.Value);

			message = Compiler.DefaultSuccess;
			return true;
		}

		/// <summary>
		/// primary_expression|("+"|"-"|"!"|"~"|"++"|"--"|"*"|"&" unary_expression)|cast_expression
		/// </summary>
		public static bool ReadUnaryExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// primary_expression
			{

				if(!ReadPrimaryExpression(ref reader, out message, out TokenList primary_expression)) return false;
				else result.AddLast(primary_expression);

			}

			// |OR|

			// ("+"|"-"|"!"|"~"|"++"|"--"|"*"|"&" unary_expression)
			{

			}

			// |OR|

			// cast_expression
			{

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// array_creation_expression|primary_no_array_creation_expression
		/// </summary>
		public static bool ReadPrimaryExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// array_creation_expression
			if(reader.LookAhead("new")) {

				if(!ReadArrayCreationExpression(ref reader, out message, out TokenList array_creation_expression)) return false;
				else result.AddLast(array_creation_expression);

			}

			// |OR|

			// primary_no_array_creation_expression
			else {

				if(!ReadPrimaryNoArrayCreationExpression(ref reader, out message, out TokenList no_array_creation_expression)) return false;
				else result.AddLast(no_array_creation_expression);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "new" type "[" { "," } "]" "{" [ expression_list ] "}"
		/// </summary>
		public static bool ReadArrayCreationExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// "new"
			if(!reader.ReadNextToken("new", out message, out ScriptToken? new_token)) return false;
			else result.AddLast(new_token.Value);

			// type

			// "["
			if(!reader.ReadNextToken("[", out message, out ScriptToken? ob)) return false;
			else result.AddLast(ob.Value);

			// { "," }
			while(reader.LookAhead(",")) {

				// ","
				if(!reader.ReadNextToken(",", out message, out ScriptToken? comma)) return false;
				else result.AddLast(comma.Value);

			}

			// "]"
			if(!reader.ReadNextToken("]", out message, out ScriptToken? cb)) return false;
			else result.AddLast(cb.Value);

			// "{"
			if(!reader.ReadNextToken("{", out message, out ScriptToken? oc)) return false;
			else result.AddLast(oc.Value);

			// [ expression_list ]
			if(!reader.LookAhead("}")) {

				// expression_list
				if(!ReadExpressionList(ref reader, out message, out TokenList expression_list)) return false;
				else result.AddLast(expression_list);

			}

			// "}"
			if(!reader.ReadNextToken("}", out message, out ScriptToken? cc)) return false;
			else result.AddLast(cc.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// literal|identifier|parenthesized_expression|member_access|(primary_expression "++"|"--")|primary_keyword
		/// </summary>
		public static bool ReadPrimaryNoArrayCreationExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// literal
			{

				TokenReader branch = reader.Branch();

				if(!ReadLiteral(ref branch, out message, out TokenList literal)) goto Break0;
				else result.AddLast(literal);

				reader = branch;
				message = Compiler.DefaultSuccess;
				return true;

			}
			Break0:

			// |OR|

			// identifier
			{

				TokenReader branch = reader.Branch();

				if(!ReadIdentifier(ref branch, out message, out TokenList identifier)) goto Break1;
				else result.AddLast(identifier);

				reader = branch;
				message = Compiler.DefaultSuccess;
				return true;

			}
			Break1:

			// |OR|

			// parenthesized_expression
			if(reader.LookAhead("(")) {

				if(!ReadParenthesizedExpression(ref reader, out message, out TokenList parenthesized_expression)) return false;
				else result.AddLast(parenthesized_expression);

				message = Compiler.DefaultSuccess;
				return true;

			}

			message = Compiler.DefaultNoPath;
			return false;

			// |OR|

			// member_access
			{



				message = Compiler.DefaultSuccess;
				return true;

			}

			// |OR|

			// primary_expression "++"|"--"
			{



				message = Compiler.DefaultSuccess;
				return true;

			}

			// |OR|

			// primary_keyword
			{



				message = Compiler.DefaultSuccess;
				return true;

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// (int_literal ["." int_literal])|(string_literal)
		/// </summary>
		public static bool ReadLiteral(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			bool is_int_literal;
			{
				
				TokenReader branch = reader.Branch();
				is_int_literal = ReadIntegerLiteral(ref branch, out _, out TokenList int_literal);
				if(is_int_literal) { result.AddLast(int_literal); reader = branch; }

			}
			if(is_int_literal) {

				TokenReader branch = reader.Branch();
				TokenList list = new TokenList();

				if(!branch.ReadNextToken(".", out _, out ScriptToken? dec)) goto Break0;
				else list.AddLast(dec.Value);

				if(!ReadIntegerLiteral(ref branch, out _, out TokenList int_literal)) goto Break0;
				else list.AddLast(int_literal);

				reader = branch;
				result.AddLast(list);

			} else {

				if(!ReadStringLiteral(ref reader, out message, out TokenList string_literal)) return false;
				else result.AddLast(string_literal);

			}

			Break0:
			message = Compiler.DefaultSuccess;
			return true;
		}

		public static bool ReadStringLiteral(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();
			
			if(!reader.ReadAnyNextToken("literal", out message, out ScriptToken? next)) return false;
			string str = (string)next.Value;
			if(!(str[0] == '"' && str[^1] == '"')) {
				message = $"Expected literal, found {str}";
				return false;
			} else result.AddLast(next.Value);

			message = "String literals have not been implemented.";
			return false;
		}

		public static bool ReadIntegerLiteral(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			if(!reader.ReadAnyNextToken("literal", out message, out ScriptToken? next)) return false;
			string str = (string)next.Value;
			if(!int.TryParse(str, out _)) {
				message = $"Expected literal, found '{str}'.";
				return false;
			} else result.AddLast(next.Value);

			message = Compiler.DefaultSuccess;
			return true;
		}

		/// <summary>
		/// [ primary_expression "." ] identifier [ ( indexer_arguments )|( [ type_arguments ] method_arguments ) ]
		/// </summary>
		public static bool ReadMemberAccess(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// [ primary_expression "." ]
			{

				TokenReader branch = reader.Branch();

				if(!ReadPrimaryExpression(ref branch, out message, out TokenList primary_expression)) goto Break0;
				else result.AddLast(primary_expression);

				reader = branch;

				if(!branch.ReadNextToken(".", out message, out ScriptToken? access_operator)) return false;
				else result.AddLast(access_operator.Value);

			}
			Break0:

			// identifier
			if(!ReadIdentifier(ref reader, out message, out TokenList identifier)) return false;
			else result.AddLast(identifier);

			// [ ( indexer_arguments )|( [ type_arguments ] method_arguments ) ]
			{

				// indexer_arguments
				if(reader.LookAhead("[")) {

				}

				// |OR|

				// [ type_arguments ] method_arguments
				if(reader.LookAhead("<") || reader.LookAhead("(")) {

					// [ type_arguments ]
					if(reader.LookAhead("<")) {

						// type_arguments
						

					}

					// method_arguments
					if(!ReadMethodArguments(ref reader, out message, out TokenList method_arguments)) return false;
					else result.AddLast(method_arguments);

				}

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "(" type ")" unary_expression
		/// </summary>
		public static bool ReadCastExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else result.AddLast(op.Value);

			// type
			if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? type)) return false;
			else result.AddLast(type.Value);

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else result.AddLast(cp.Value);

			// unary_expression
			if(!ReadUnaryExpression(ref reader, out message, out TokenList unary_expression)) return false;
			else result.AddLast(unary_expression);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// conditional_expression|lambda_expression|query_expression
		/// </summary>
		public static bool ReadNonAssignmentOperation(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// query_expression
			if(reader.LookAhead("from")) {

				if(!ReadQueryExpression(ref reader, out message, out TokenList query_expression)) return false;
				else result.AddLast(query_expression);

				message = Compiler.DefaultSuccess;
				return true;

			}

			// |OR|

			// lambda_expression
			bool lambda;
			{
				TokenReader lookahead = reader.Branch();
				bool parameters = ReadParameters(ref lookahead, out _, out _);
				lambda = parameters;
			}
			if(lambda) {

				// lambda_expression
				if(!ReadLambdaExpression(ref reader, out message, out TokenList lambda_expression)) return false;
				else result.AddLast(lambda_expression);

				message = Compiler.DefaultSuccess;
				return true;

			}

			// |OR|

			// conditional_expression
			{

				// conditional_expression
				if(!ReadConditionalExpression(ref reader, out message, out TokenList conditional_expression)) return false;
				else result.AddLast(conditional_expression);

				message = Compiler.DefaultSuccess;
				return true;

			}

		}

		/// <summary>
		/// null_coalescing_expression { "?" expression ":" expression }
		/// </summary>
		public static bool ReadConditionalExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// null_coalescing_expression
			if(!ReadNullCoalescingExpression(ref reader, out message, out TokenList null_coalescing_expression)) return false;
			else result.AddLast(null_coalescing_expression);

			// { "?" expression ":" expression }
			while(reader.LookAhead("?")) {

				// "?"
				if(!reader.ReadNextToken("?", out message, out ScriptToken? op1)) return false;
				else result.AddLast(op1.Value);

				// expression
				if(!ReadExpression(ref reader, out message, out TokenList expr1)) return false;
				else result.AddLast(expr1);

				// ":"
				if(!reader.ReadNextToken(":", out message, out ScriptToken? op2)) return false;
				else result.AddLast(op2.Value);

				// expression
				if(!ReadExpression(ref reader, out message, out TokenList expr2)) return false;
				else result.AddLast(expr2);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// conditional_or_expression [ "??" null_coalescing_expression ]
		/// </summary>
		public static bool ReadNullCoalescingExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// conditional_or_expression
			if(!ReadConditionalOrExpression(ref reader, out message, out TokenList conditional_or_expression)) return false;
			else result.AddLast(conditional_or_expression);

			// [ "??" null_coalescing_expression ]
			if(reader.LookAhead("??")) {

				// "??"
				if(!reader.ReadNextToken("??", out message, out ScriptToken? op)) return false;
				else result.AddLast(op.Value);

				// null_coalescing_expression
				if(!ReadNullCoalescingExpression(ref reader, out message, out TokenList null_coalescing_expression)) return false;
				else result.AddLast(null_coalescing_expression);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// conditional_and_expression { "||" conditional_and_expression }
		/// </summary>
		public static bool ReadConditionalOrExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// conditional_and_expression
			if(!ReadConditionalAndExpression(ref reader, out message, out TokenList expr1)) return false;
			else result.AddLast(expr1);

			// { "||" conditional_and_expression }
			while(reader.LookAhead("||")) {

				// "||"
				if(!reader.ReadNextToken("||", out message, out ScriptToken? op)) return false;
				else result.AddLast(op.Value);

				// conditional_and_expression
				if(!ReadConditionalAndExpression(ref reader, out message, out TokenList expr2)) return false;
				else result.AddLast(expr2);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// inclusive_or_expression { "&&" inclusive_or_expression }
		/// </summary>
		public static bool ReadConditionalAndExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// inclusive_or_expression
			if(!ReadInclusiveOrExpression(ref reader, out message, out TokenList expr1)) return false;
			else result.AddLast(expr1);

			// { "&&" inclusive_or_expression }
			while(reader.LookAhead("&&")) {

				// "&&"
				if(!reader.ReadNextToken("&&", out message, out ScriptToken? op)) return false;
				else result.AddLast(op.Value);

				// inclusive_or_expression
				if(!ReadInclusiveOrExpression(ref reader, out message, out TokenList expr2)) return false;
				else result.AddLast(expr2);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// exclusive_or_expression { "|" exclusive_or_expression }
		/// </summary>
		public static bool ReadInclusiveOrExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// exclusive_or_expression
			if(!ReadExclusiveOrExpression(ref reader, out message, out TokenList expr1)) return false;
			else result.AddLast(expr1);

			// { "|" exclusive_or_expression }
			while(reader.LookAhead("|")) {

				// "|"
				if(!reader.ReadNextToken("|", out message, out ScriptToken? op)) return false;
				else result.AddLast(op.Value);

				// exclusive_or_expression
				if(!ReadExclusiveOrExpression(ref reader, out message, out TokenList expr2)) return false;
				else result.AddLast(expr2);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// and_expression { "^" and_expression }
		/// </summary>
		public static bool ReadExclusiveOrExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// and_expression
			if(!ReadAndExpression(ref reader, out message, out TokenList expr1)) return false;
			else result.AddLast(expr1);

			// { "^" and_expression }
			while(reader.LookAhead("^")) {

				// "^"
				if(!reader.ReadNextToken("^", out message, out ScriptToken? op)) return false;
				else result.AddLast(op.Value);

				// and_expression
				if(!ReadAndExpression(ref reader, out message, out TokenList expr2)) return false;
				else result.AddLast(expr2);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// equality_expresion { "&" equality_expression }
		/// </summary>
		public static bool ReadAndExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// equality_expression
			if(!ReadEqualityExpression(ref reader, out message, out TokenList expr1)) return false;
			else result.AddLast(expr1);

			// { "&" equality_expression }
			while(reader.LookAhead("&")) {

				// "&"
				if(!reader.ReadNextToken("&", out message, out ScriptToken? op)) return false;
				else result.AddLast(op.Value);

				// equality_expression
				if(!ReadEqualityExpression(ref reader, out message, out TokenList expr2)) return false;
				else result.AddLast(expr2);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// relational_expression { ("=="|"!=") relational_expression }
		/// </summary>
		public static bool ReadEqualityExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// relational_expression
			if(!ReadRelationalExpression(ref reader, out message, out TokenList expr1)) return false;
			else result.AddLast(expr1);

			// { ("=="|"!=") relational_expression }
			string[] operators = new string[] { "==", "!=" };
			while(reader.LookAhead(operators)) {

				// ("=="|"!=")
				if(!reader.ReadNextToken(operators, "equality operator", out message, out ScriptToken? op)) return false;
				else result.AddLast(op.Value);

				// relational_expression
				if(!ReadRelationalExpression(ref reader, out message, out TokenList expr2)) return false;
				else result.AddLast(expr2);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// shift_expression { relation_or_type_check }
		/// </summary>
		public static bool ReadRelationalExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// shift_expression
			if(!ReadShiftExpression(ref reader, out message, out TokenList expr1)) return false;
			else result.AddLast(expr1);

			// { relation_or_type_check }
			while(false) {

				// todo

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// additive_expression { ("<<"|">>") additive_expression }
		/// </summary>
		public static bool ReadShiftExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// additive_expression
			if(!ReadAdditiveExpression(ref reader, out message, out TokenList expr1)) return false;
			else result.AddLast(expr1);

			// { ("<<"|">>") additive_expression }
			string[] operators = new string[] { "<<", ">>" };
			while(reader.LookAhead(operators)) {

				// ("<<"|">>")
				if(!reader.ReadNextToken(operators, "shift operator", out message, out ScriptToken? op)) return false;
				else result.AddLast(op.Value);

				// additive_expression
				if(!ReadAdditiveExpression(ref reader, out message, out TokenList expr2)) return false;
				else result.AddLast(expr2);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// multiplicative_expression { ("+"|"-") multiplicative_expression }
		/// </summary>
		public static bool ReadAdditiveExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// multiplicative_expression
			if(!ReadMultiplicativeExpression(ref reader, out message, out TokenList expr1)) return false;
			else result.AddLast(expr1);

			// { ("+"|"-") multiplicative_expression }
			string[] operators = new string[] { "+", "-" };
			while(reader.LookAhead(operators)) {

				// ("+"|"-")
				if(!reader.ReadNextToken(operators, "addition operator", out message, out ScriptToken? op)) return false;
				else result.AddLast(op.Value);

				// multiplicative_expression
				if(!ReadMultiplicativeExpression(ref reader, out message, out TokenList expr2)) return false;
				else result.AddLast(expr2);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// unary_expression { ("*"|"/"|"%") unary_expression }
		/// </summary>
		public static bool ReadMultiplicativeExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// unary_expression
			if(!ReadUnaryExpression(ref reader, out message, out TokenList expr1)) return false;
			else result.AddLast(expr1);

			// { ("*"|"/"|"%") unary_expression }
			string[] operators = new string[] { "*", "/", "%" };
			while(reader.LookAhead(operators)) {

				// ("*"|"/"|"%")
				if(!reader.ReadNextToken(operators, "multiplication operator", out message, out ScriptToken? op)) return false;
				else result.AddLast(op.Value);

				// unary_expression
				if(!ReadUnaryExpression(ref reader, out message, out TokenList expr2)) return false;
				else result.AddLast(expr2);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// parameters "=>" expression|code_block
		/// </summary>
		public static bool ReadLambdaExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();

			// parameters
			if(!ReadParameters(ref reader, out message, out TokenList parameters)) return false;
			else result.AddLast(parameters);

			// "=>"
			if(!reader.ReadNextToken("=>", out message, out ScriptToken? op)) return false;
			else result.AddLast(op.Value);

			// expression|code_block
			{

				// code_block
				if(reader.LookAhead("{")) {

					if(!ReadCodeBlock(ref reader, out message, out TokenList block)) return false;
					else result.AddLast(block);

				}

				// |OR|

				// expression
				else {

					if(!ReadExpression(ref reader, out message, out TokenList expr)) return false;
					else result.AddLast(expr);

				}

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// from_clause|query_body
		/// </summary>
		public static bool ReadQueryExpression(ref TokenReader reader, out string message, out TokenList result) {
			result = new TokenList();
			message = "Query expression not implemented.";
			return false; // todo?
		}

		/// <summary>
		/// new_keyword|typeof_keyword|checked_keyword|unchecked_keyword|default_keyword|nameof_keyword|delegate_keyword|sizeof_keyword
		/// </summary>
		public static bool ReadPrimaryKeyword(ref TokenReader reader, out string message, out TokenList primary_keyword) {
			primary_keyword = new TokenList();

			// new_keyword
			if(reader.LookAhead("new")) {
				if(!ReadNewKeyword(ref reader, out message, out TokenList @new)) return false;
				else primary_keyword.AddLast(@new);
				return true;
			}

			// |OR|

			// typeof_keyword
			if(reader.LookAhead("typeof")) {
				if(!ReadTypeofKeyword(ref reader, out message, out TokenList @typeof)) return false;
				else primary_keyword.AddLast(@typeof);
				return true;
			}

			// |OR|

			// checked_keyword
			if(reader.LookAhead("checked")) {
				if(!ReadCheckedKeyword(ref reader, out message, out TokenList @checked)) return false;
				else primary_keyword.AddLast(@checked);
				return true;
			}

			// |OR|

			// unchecked_keyword
			if(reader.LookAhead("unchecked")) {
				if(!ReadUncheckedKeyword(ref reader, out message, out TokenList @unchecked)) return false;
				else primary_keyword.AddLast(@unchecked);
				return true;
			}

			// |OR|

			// default_keyword
			if(reader.LookAhead("default")) {
				if(!ReadDefaultKeyword(ref reader, out message, out TokenList @default)) return false;
				else primary_keyword.AddLast(@default);
				return true;
			}

			// |OR|

			// nameof_keyword
			if(reader.LookAhead("nameof")) {
				if(!ReadNameofKeyword(ref reader, out message, out TokenList nameof)) return false;
				else primary_keyword.AddLast(nameof);
				return true;
			}

			// |OR|

			// sizeof_keyword
			if(reader.LookAhead("sizeof")) {
				if(!ReadSizeofKeyword(ref reader, out message, out TokenList @sizeof)) return false;
				else primary_keyword.AddLast(@sizeof);
				return true;
			}

			message = Compiler.DefaultNoPath;
			return false;

		}

		/// <summary>
		/// "new" type {"." type} method_arguments
		/// </summary>
		public static bool ReadNewKeyword(ref TokenReader reader, out string message, out TokenList new_keyword) {
			new_keyword = new TokenList();

			// "new"
			if(!reader.ReadNextToken("new", out message, out ScriptToken? new_token)) return false;
			else new_keyword.AddLast(new_token.Value);

			// type
			if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? type0)) return false;
			else new_keyword.AddLast(type0.Value);

			// {"." type}
			while(reader.LookAhead(".")) {

				// "."
				if(!reader.ReadNextToken(".", out message, out ScriptToken? ac)) return false;
				else new_keyword.AddLast(ac.Value);

				// type
				if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? type1)) return false;
				else new_keyword.AddLast(type1.Value);

			}

			// method_arguments
			if(!ReadMethodArguments(ref reader, out message, out TokenList arguments)) return false;
			else new_keyword.AddLast(arguments);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "typeof" "(" expression ")"
		/// </summary>
		public static bool ReadTypeofKeyword(ref TokenReader reader, out string message, out TokenList typeof_keyword) {
			typeof_keyword = new TokenList();

			// "typeof"
			if(!reader.ReadNextToken("typeof", out message, out ScriptToken? typeof_token)) return false;
			else typeof_keyword.AddLast(typeof_token.Value);

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else typeof_keyword.AddLast(op.Value);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList expression)) return false;
			else typeof_keyword.AddLast(expression);

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else typeof_keyword.AddLast(cp.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "checked" "(" expression ")"
		/// </summary>
		public static bool ReadCheckedKeyword(ref TokenReader reader, out string message, out TokenList checked_keyword) {
			checked_keyword = new TokenList();

			// "checked"
			if(!reader.ReadNextToken("checked", out message, out ScriptToken? checked_token)) return false;
			else checked_keyword.AddLast(checked_token.Value);

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else checked_keyword.AddLast(op.Value);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList expression)) return false;
			else checked_keyword.AddLast(expression);

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else checked_keyword.AddLast(cp.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "unchecked" "(" expression ")"
		/// </summary>
		public static bool ReadUncheckedKeyword(ref TokenReader reader, out string message, out TokenList unchecked_keyword) {
			unchecked_keyword = new TokenList();

			// "unchecked"
			if(!reader.ReadNextToken("unchecked", out message, out ScriptToken? unchecked_token)) return false;
			else unchecked_keyword.AddLast(unchecked_token.Value);

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else unchecked_keyword.AddLast(op.Value);

			// expression
			if(!ReadExpression(ref reader, out message, out TokenList expression)) return false;
			else unchecked_keyword.AddLast(expression);

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else unchecked_keyword.AddLast(cp.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "default" ["(" type {"." type} ")"]
		/// </summary>
		public static bool ReadDefaultKeyword(ref TokenReader reader, out string message, out TokenList default_keyword) {
			default_keyword = new TokenList();

			// "default"
			if(!reader.ReadNextToken("default", out message, out ScriptToken? default_token)) return false;
			else default_keyword.AddLast(default_token.Value);

			// ["(" type {"." type} ")"]
			if(reader.LookAhead("(")) {

				// "("
				if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
				else default_keyword.AddLast(op.Value);

				// type
				if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? type)) return false;
				else default_keyword.AddLast(type.Value);

				// {"." type}
				while(reader.LookAhead(".")) {

					// "."
					if(!reader.ReadNextToken(".", out message, out ScriptToken? ac)) return false;
					else default_keyword.AddLast(ac.Value);

					// type
					if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? type1)) return false;
					else default_keyword.AddLast(type1.Value);

				}

				// ")"
				if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
				else default_keyword.AddLast(cp.Value);

			}

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "nameof" "(" identifier|type {"." identifier|type} ")"
		/// </summary>
		public static bool ReadNameofKeyword(ref TokenReader reader, out string message, out TokenList nameof_keyword) {
			nameof_keyword = new TokenList();

			// "nameof"
			if(!reader.ReadNextToken("default", out message, out ScriptToken? nameof_tokem)) return false;
			else nameof_keyword.AddLast(nameof_tokem.Value);

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else nameof_keyword.AddLast(op.Value);

			// identifier|type
			{

				bool or0 = false;

				// identifier
				{
					TokenReader branch = reader.Branch();

					if(!branch.ReadNextToken(NameRegex, "identifier", out _, out ScriptToken? identifier)) or0 = true;
					else nameof_keyword.AddLast(identifier.Value);

					reader = branch;
				}

				// |OR|

				// type
				if(or0) {

					if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? type)) return false;
					else nameof_keyword.AddLast(type.Value);

				}

			}

			// {"." identifier|type}
			while(reader.LookAhead(".")) {

				// "."
				if(!reader.ReadNextToken(".", out message, out ScriptToken? ac)) return false;
				else nameof_keyword.AddLast(ac.Value);

				bool or1 = false;

				// identifier
				{
					TokenReader branch = reader.Branch();

					if(!branch.ReadNextToken(NameRegex, "identifier", out _, out ScriptToken? identifier)) or1 = true;
					else nameof_keyword.AddLast(identifier.Value);

					reader = branch;
				}

				// |OR|

				// type
				if(or1) {

					if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? type)) return false;
					else nameof_keyword.AddLast(type.Value);

				}

			}

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else nameof_keyword.AddLast(cp.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "delegate" [method_arguments] code_block
		/// </summary>
		public static bool ReadDelegateKeyword(ref TokenReader reader, out string message, out TokenList delegate_keyword) {
			delegate_keyword = new TokenList();

			// "delegate"
			if(!reader.ReadNextToken("delegate", out message, out ScriptToken? delegate_token)) return false;
			else delegate_keyword.AddLast(delegate_keyword);

			// [method_arguments]
			{
				TokenReader branch = reader.Branch();

				// method_arguments

				reader = branch;
			}
			Break0:

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "sizeof" "(" identifier|type {"." identifier|type} ")"
		/// </summary>
		public static bool ReadSizeofKeyword(ref TokenReader reader, out string message, out TokenList sizeof_keyword) {
			sizeof_keyword = new TokenList();

			// "sizeof"
			if(!reader.ReadNextToken("sizeof", out message, out ScriptToken? sizeof_token)) return false;
			else sizeof_keyword.AddLast(sizeof_token.Value);

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else sizeof_keyword.AddLast(op.Value);

			// identifier|type
			{

				bool or0 = false;

				// identifier
				{
					TokenReader branch = reader.Branch();

					if(!branch.ReadNextToken(NameRegex, "identifier", out _, out ScriptToken? identifier)) or0 = true;
					else sizeof_keyword.AddLast(identifier.Value);

					reader = branch;
				}

				// |OR|

				// type
				if(or0) {

					if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? type)) return false;
					else sizeof_keyword.AddLast(type.Value);

				}

			}

			// {"." identifier|type}
			while(reader.LookAhead(".")) {

				// "."
				if(!reader.ReadNextToken(".", out message, out ScriptToken? ac)) return false;
				else sizeof_keyword.AddLast(ac.Value);

				bool or1 = false;

				// identifier
				{
					TokenReader branch = reader.Branch();

					if(!branch.ReadNextToken(NameRegex, "identifier", out _, out ScriptToken? identifier)) or1 = true;
					else sizeof_keyword.AddLast(identifier.Value);

					reader = branch;
				}

				// |OR|

				// type
				if(or1) {

					if(!reader.ReadNextToken(ScriptClass.NameRegex, "type", out message, out ScriptToken? type)) return false;
					else sizeof_keyword.AddLast(type.Value);

				}

			}

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else sizeof_keyword.AddLast(cp.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// "(" [expression_list]  ")"
		/// </summary>
		public static bool ReadMethodArguments(ref TokenReader reader, out string message, out TokenList method_arguments) {
			method_arguments = new TokenList();

			// "("
			if(!reader.ReadNextToken("(", out message, out ScriptToken? op)) return false;
			else method_arguments.AddLast(op.Value);

			// [expression {"," expression}]
			if(!reader.LookAhead(")")) {

				// expression
				if(!ReadExpression(ref reader, out message, out TokenList expression0)) return false;
				else method_arguments.AddLast(expression0);

				// {"," expression}
				while(reader.LookAhead(",")) {

					// ","
					if(!reader.ReadNextToken(".", out message, out ScriptToken? comma)) return false;
					else method_arguments.AddLast(comma.Value);

					// expression
					if(!ReadExpression(ref reader, out message, out TokenList expression1)) return false;
					else method_arguments.AddLast(expression1);

				}

			}

			// ")"
			if(!reader.ReadNextToken(")", out message, out ScriptToken? cp)) return false;
			else method_arguments.AddLast(cp.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

		/// <summary>
		/// ["?"] "[" [expression_list] "]"
		/// </summary>
		public static bool ReadIndexerArguments(ref TokenReader reader, out string message, out TokenList indexer_arguments) {
			indexer_arguments = new TokenList();

			// ["?"]
			if(reader.LookAhead("?")) {

				// "?"
				if(!reader.ReadNextToken("?", out message, out ScriptToken? qm)) return false;
				else indexer_arguments.AddLast(qm.Value);

			}

			// "["
			if(!reader.ReadNextToken("[", out message, out ScriptToken? ob)) return false;
			else indexer_arguments.AddLast(ob.Value);

			// [expression {"," expression}]
			if(!reader.LookAhead(")")) {

				// expression
				if(!ReadExpression(ref reader, out message, out TokenList expression0)) return false;
				else indexer_arguments.AddLast(expression0);

				// {"," expression}
				while(reader.LookAhead(",")) {

					// ","
					if(!reader.ReadNextToken(".", out message, out ScriptToken? comma)) return false;
					else indexer_arguments.AddLast(comma.Value);

					// expression
					if(!ReadExpression(ref reader, out message, out TokenList expression1)) return false;
					else indexer_arguments.AddLast(expression1);

				}

			}

			// "]"
			if(!reader.ReadNextToken("]", out message, out ScriptToken? cb)) return false;
			else indexer_arguments.AddLast(cb.Value);

			message = Compiler.DefaultSuccess;
			return true;

		}

	}

}
