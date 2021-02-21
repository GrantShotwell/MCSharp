// Generated from c:\Projects\MCSharp\MCSharp\Antlr\MCSharp.g4 by ANTLR 4.8
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class MCSharpParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.8", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, END=2, COMMA=3, OP=4, CP=5, OB=6, CB=7, OC=8, CC=9, PLUS=10, MINUS=11, 
		MULTIPLY=12, DIVIDE=13, MODULUS=14, INCREMENT=15, DECREMENT=16, BITWISE_AND=17, 
		BITWISE_OR=18, BITWISE_XOR=19, BITWISE_NOT=20, BOOLEAN_AND=21, BOOLEAN_OR=22, 
		BOOLEAN_NOT=23, SHIFT_LEFT=24, SHIFT_RIGHT=25, EQUIVALENT=26, NOT_EQUIVALENT=27, 
		LESS_THAN=28, GREATER_THAN=29, LESS_THAN_EQUAL=30, GREATER_THAN_EQUAL=31, 
		DOT=32, ASSIGN=33, ASSIGN_PLUS=34, ASSIGN_MINUS=35, ASSIGN_MULTIPLY=36, 
		ASSIGN_DIVIDE=37, ASSIGN_MODULUS=38, ASSIGN_ACCESS=39, ASSIGN_AND=40, 
		ASSIGN_OR=41, ASSIGN_XOR=42, ASSIGN_LEFT=43, ASSIGN_RIGHT=44, CONDITION_IF=45, 
		CONDITION_ELSE=46, RANGE_INCLUSIVE=47, RANGE_EXCLUSIVE=48, IS=49, AS=50, 
		IN=51, OUT=52, LAMBDA=53, NULL_COALESCING=54, IF=55, ELSE=56, FOR=57, 
		FOREACH=58, DO=59, WHILE=60, RETURN=61, THROW=62, TRY=63, CATCH=64, FINALLY=65, 
		NEW=66, TYPEOF=67, CHECKED=68, UNCHECKED=69, DEFAULT=70, DELEGATE=71, 
		SIZEOF=72, WITH=73, GET=74, SET=75, PUBLIC=76, PRIVATE=77, PROTECTED=78, 
		STATIC=79, ABSTRACT=80, VIRTUAL=81, OVERRIDE=82, REF=83, CLASS=84, STRUCT=85, 
		STRING=86, DECIMAL=87, INTEGER=88, NAME=89, WHITESPACE=90, NEWLINE=91;
	public static final int
		RULE_script = 0, RULE_generic_parameter = 1, RULE_generic_parameter_list = 2, 
		RULE_generic_parameters = 3, RULE_method_parameter = 4, RULE_method_parameter_list = 5, 
		RULE_method_parameters = 6, RULE_indexer_parameters = 7, RULE_argument = 8, 
		RULE_argument_list = 9, RULE_generic_arguments = 10, RULE_method_arguments = 11, 
		RULE_indexer_arguments = 12, RULE_member_initializer = 13, RULE_object_initializer = 14, 
		RULE_element_initializer = 15, RULE_collection_initializer = 16, RULE_anonymous_element_initializer = 17, 
		RULE_anonymous_object_initializer = 18, RULE_modifier = 19, RULE_parameter_modifier = 20, 
		RULE_class_type = 21, RULE_type_definition = 22, RULE_member_definition = 23, 
		RULE_field_definition = 24, RULE_property_definition = 25, RULE_property_get_definition = 26, 
		RULE_property_set_definition = 27, RULE_method_definition = 28, RULE_literal = 29, 
		RULE_identifier = 30, RULE_statement = 31, RULE_code_block = 32, RULE_additive_operator = 33, 
		RULE_multiplicative_operator = 34, RULE_step_operator = 35, RULE_bitwise_operator = 36, 
		RULE_boolean_operator = 37, RULE_shift_operator = 38, RULE_equality_operator = 39, 
		RULE_relation_operator = 40, RULE_assignment_operator = 41, RULE_range_operator = 42, 
		RULE_language_function = 43, RULE_if_statement = 44, RULE_for_statement = 45, 
		RULE_foreach_statement = 46, RULE_while_statement = 47, RULE_do_statement = 48, 
		RULE_return_statement = 49, RULE_throw_statement = 50, RULE_try_statement = 51, 
		RULE_expression = 52, RULE_initialization_expression = 53, RULE_non_assignment_expression = 54, 
		RULE_lambda_expression = 55, RULE_expression_list = 56, RULE_conditional_expression = 57, 
		RULE_null_coalescing_expression = 58, RULE_conditional_or_expression = 59, 
		RULE_conditional_and_expression = 60, RULE_inclusive_or_expression = 61, 
		RULE_exclusive_or_expression = 62, RULE_and_expression = 63, RULE_equality_expression = 64, 
		RULE_relational_expression = 65, RULE_relation_or_type_check = 66, RULE_shift_expression = 67, 
		RULE_additive_expression = 68, RULE_multiplicative_expression = 69, RULE_with_expression = 70, 
		RULE_range_expression = 71, RULE_pre_step_expression = 72, RULE_post_step_expression = 73, 
		RULE_unary_expression = 74, RULE_cast_expression = 75, RULE_pointer_indirection_expression = 76, 
		RULE_addressof_expression = 77, RULE_assignment_expression = 78, RULE_primary_expression = 79, 
		RULE_array_creation_expression = 80, RULE_array_rank_specifier = 81, RULE_array_initializer = 82, 
		RULE_variable_initializer = 83, RULE_primary_no_array_creation_expression = 84, 
		RULE_member_access = 85, RULE_keyword_expression = 86, RULE_object_or_collection_initializer = 87, 
		RULE_new_keyword_expression = 88, RULE_typeof_keyword_expression = 89, 
		RULE_checked_expression = 90, RULE_unchecked_expression = 91, RULE_default_keyword_expression = 92, 
		RULE_delegate_keyword_expression = 93, RULE_sizeof_keyword_expression = 94;
	private static String[] makeRuleNames() {
		return new String[] {
			"script", "generic_parameter", "generic_parameter_list", "generic_parameters", 
			"method_parameter", "method_parameter_list", "method_parameters", "indexer_parameters", 
			"argument", "argument_list", "generic_arguments", "method_arguments", 
			"indexer_arguments", "member_initializer", "object_initializer", "element_initializer", 
			"collection_initializer", "anonymous_element_initializer", "anonymous_object_initializer", 
			"modifier", "parameter_modifier", "class_type", "type_definition", "member_definition", 
			"field_definition", "property_definition", "property_get_definition", 
			"property_set_definition", "method_definition", "literal", "identifier", 
			"statement", "code_block", "additive_operator", "multiplicative_operator", 
			"step_operator", "bitwise_operator", "boolean_operator", "shift_operator", 
			"equality_operator", "relation_operator", "assignment_operator", "range_operator", 
			"language_function", "if_statement", "for_statement", "foreach_statement", 
			"while_statement", "do_statement", "return_statement", "throw_statement", 
			"try_statement", "expression", "initialization_expression", "non_assignment_expression", 
			"lambda_expression", "expression_list", "conditional_expression", "null_coalescing_expression", 
			"conditional_or_expression", "conditional_and_expression", "inclusive_or_expression", 
			"exclusive_or_expression", "and_expression", "equality_expression", "relational_expression", 
			"relation_or_type_check", "shift_expression", "additive_expression", 
			"multiplicative_expression", "with_expression", "range_expression", "pre_step_expression", 
			"post_step_expression", "unary_expression", "cast_expression", "pointer_indirection_expression", 
			"addressof_expression", "assignment_expression", "primary_expression", 
			"array_creation_expression", "array_rank_specifier", "array_initializer", 
			"variable_initializer", "primary_no_array_creation_expression", "member_access", 
			"keyword_expression", "object_or_collection_initializer", "new_keyword_expression", 
			"typeof_keyword_expression", "checked_expression", "unchecked_expression", 
			"default_keyword_expression", "delegate_keyword_expression", "sizeof_keyword_expression"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'@'", "';'", "','", "'('", "')'", "'['", "']'", "'{'", "'}'", 
			"'+'", "'-'", "'*'", "'/'", "'%'", "'++'", "'--'", "'&'", "'|'", "'^'", 
			"'~'", "'&&'", "'||'", "'!'", "'<<'", "'>>'", "'=='", "'!='", "'>'", 
			"'<'", "'>='", "'<='", "'.'", "'='", "'+='", "'-='", "'*='", "'/='", 
			"'%='", "'.='", "'&='", "'|='", "'^='", "'<<='", "'>>='", "'?'", "':'", 
			"'..'", "'..^'", "'is'", "'as'", "'in'", "'out'", "'=>'", "'??'", "'if'", 
			"'else'", "'for'", "'foreach'", "'do'", "'while'", "'return'", "'throw'", 
			"'try'", "'catch'", "'finally'", "'new'", "'typeof'", "'checked'", "'unchecked'", 
			"'default'", "'delegate'", "'sizeof'", "'with'", "'get'", "'set'", "'public'", 
			"'private'", "'protected'", "'static'", "'abstract'", "'virtual'", "'override'", 
			"'ref'", "'class'", "'struct'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, "END", "COMMA", "OP", "CP", "OB", "CB", "OC", "CC", "PLUS", 
			"MINUS", "MULTIPLY", "DIVIDE", "MODULUS", "INCREMENT", "DECREMENT", "BITWISE_AND", 
			"BITWISE_OR", "BITWISE_XOR", "BITWISE_NOT", "BOOLEAN_AND", "BOOLEAN_OR", 
			"BOOLEAN_NOT", "SHIFT_LEFT", "SHIFT_RIGHT", "EQUIVALENT", "NOT_EQUIVALENT", 
			"LESS_THAN", "GREATER_THAN", "LESS_THAN_EQUAL", "GREATER_THAN_EQUAL", 
			"DOT", "ASSIGN", "ASSIGN_PLUS", "ASSIGN_MINUS", "ASSIGN_MULTIPLY", "ASSIGN_DIVIDE", 
			"ASSIGN_MODULUS", "ASSIGN_ACCESS", "ASSIGN_AND", "ASSIGN_OR", "ASSIGN_XOR", 
			"ASSIGN_LEFT", "ASSIGN_RIGHT", "CONDITION_IF", "CONDITION_ELSE", "RANGE_INCLUSIVE", 
			"RANGE_EXCLUSIVE", "IS", "AS", "IN", "OUT", "LAMBDA", "NULL_COALESCING", 
			"IF", "ELSE", "FOR", "FOREACH", "DO", "WHILE", "RETURN", "THROW", "TRY", 
			"CATCH", "FINALLY", "NEW", "TYPEOF", "CHECKED", "UNCHECKED", "DEFAULT", 
			"DELEGATE", "SIZEOF", "WITH", "GET", "SET", "PUBLIC", "PRIVATE", "PROTECTED", 
			"STATIC", "ABSTRACT", "VIRTUAL", "OVERRIDE", "REF", "CLASS", "STRUCT", 
			"STRING", "DECIMAL", "INTEGER", "NAME", "WHITESPACE", "NEWLINE"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "MCSharp.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public MCSharpParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ScriptContext extends ParserRuleContext {
		public TerminalNode EOF() { return getToken(MCSharpParser.EOF, 0); }
		public List<Type_definitionContext> type_definition() {
			return getRuleContexts(Type_definitionContext.class);
		}
		public Type_definitionContext type_definition(int i) {
			return getRuleContext(Type_definitionContext.class,i);
		}
		public ScriptContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_script; }
	}

	public final ScriptContext script() throws RecognitionException {
		ScriptContext _localctx = new ScriptContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_script);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(193);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)) | (1L << (CLASS - 76)) | (1L << (STRUCT - 76)))) != 0)) {
				{
				{
				setState(190);
				type_definition();
				}
				}
				setState(195);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(196);
			match(EOF);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Generic_parameterContext extends ParserRuleContext {
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public Generic_parameterContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_generic_parameter; }
	}

	public final Generic_parameterContext generic_parameter() throws RecognitionException {
		Generic_parameterContext _localctx = new Generic_parameterContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_generic_parameter);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(198);
			match(NAME);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Generic_parameter_listContext extends ParserRuleContext {
		public List<Generic_parameterContext> generic_parameter() {
			return getRuleContexts(Generic_parameterContext.class);
		}
		public Generic_parameterContext generic_parameter(int i) {
			return getRuleContext(Generic_parameterContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(MCSharpParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(MCSharpParser.COMMA, i);
		}
		public Generic_parameter_listContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_generic_parameter_list; }
	}

	public final Generic_parameter_listContext generic_parameter_list() throws RecognitionException {
		Generic_parameter_listContext _localctx = new Generic_parameter_listContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_generic_parameter_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(200);
			generic_parameter();
			setState(205);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(201);
				match(COMMA);
				setState(202);
				generic_parameter();
				}
				}
				setState(207);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Generic_parametersContext extends ParserRuleContext {
		public TerminalNode LESS_THAN() { return getToken(MCSharpParser.LESS_THAN, 0); }
		public Generic_parameter_listContext generic_parameter_list() {
			return getRuleContext(Generic_parameter_listContext.class,0);
		}
		public TerminalNode GREATER_THAN() { return getToken(MCSharpParser.GREATER_THAN, 0); }
		public Generic_parametersContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_generic_parameters; }
	}

	public final Generic_parametersContext generic_parameters() throws RecognitionException {
		Generic_parametersContext _localctx = new Generic_parametersContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_generic_parameters);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(208);
			match(LESS_THAN);
			setState(209);
			generic_parameter_list();
			setState(210);
			match(GREATER_THAN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Method_parameterContext extends ParserRuleContext {
		public List<TerminalNode> NAME() { return getTokens(MCSharpParser.NAME); }
		public TerminalNode NAME(int i) {
			return getToken(MCSharpParser.NAME, i);
		}
		public Parameter_modifierContext parameter_modifier() {
			return getRuleContext(Parameter_modifierContext.class,0);
		}
		public Method_parameterContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_method_parameter; }
	}

	public final Method_parameterContext method_parameter() throws RecognitionException {
		Method_parameterContext _localctx = new Method_parameterContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_method_parameter);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(213);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 51)) & ~0x3f) == 0 && ((1L << (_la - 51)) & ((1L << (IN - 51)) | (1L << (OUT - 51)) | (1L << (REF - 51)))) != 0)) {
				{
				setState(212);
				parameter_modifier();
				}
			}

			setState(215);
			match(NAME);
			setState(216);
			match(NAME);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Method_parameter_listContext extends ParserRuleContext {
		public List<Method_parameterContext> method_parameter() {
			return getRuleContexts(Method_parameterContext.class);
		}
		public Method_parameterContext method_parameter(int i) {
			return getRuleContext(Method_parameterContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(MCSharpParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(MCSharpParser.COMMA, i);
		}
		public Method_parameter_listContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_method_parameter_list; }
	}

	public final Method_parameter_listContext method_parameter_list() throws RecognitionException {
		Method_parameter_listContext _localctx = new Method_parameter_listContext(_ctx, getState());
		enterRule(_localctx, 10, RULE_method_parameter_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(218);
			method_parameter();
			setState(223);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(219);
				match(COMMA);
				setState(220);
				method_parameter();
				}
				}
				setState(225);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Method_parametersContext extends ParserRuleContext {
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public Method_parameter_listContext method_parameter_list() {
			return getRuleContext(Method_parameter_listContext.class,0);
		}
		public Method_parametersContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_method_parameters; }
	}

	public final Method_parametersContext method_parameters() throws RecognitionException {
		Method_parametersContext _localctx = new Method_parametersContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_method_parameters);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(226);
			match(OP);
			setState(228);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 51)) & ~0x3f) == 0 && ((1L << (_la - 51)) & ((1L << (IN - 51)) | (1L << (OUT - 51)) | (1L << (REF - 51)) | (1L << (NAME - 51)))) != 0)) {
				{
				setState(227);
				method_parameter_list();
				}
			}

			setState(230);
			match(CP);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Indexer_parametersContext extends ParserRuleContext {
		public TerminalNode OB() { return getToken(MCSharpParser.OB, 0); }
		public TerminalNode CB() { return getToken(MCSharpParser.CB, 0); }
		public Method_parameter_listContext method_parameter_list() {
			return getRuleContext(Method_parameter_listContext.class,0);
		}
		public Indexer_parametersContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_indexer_parameters; }
	}

	public final Indexer_parametersContext indexer_parameters() throws RecognitionException {
		Indexer_parametersContext _localctx = new Indexer_parametersContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_indexer_parameters);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(232);
			match(OB);
			setState(234);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 51)) & ~0x3f) == 0 && ((1L << (_la - 51)) & ((1L << (IN - 51)) | (1L << (OUT - 51)) | (1L << (REF - 51)) | (1L << (NAME - 51)))) != 0)) {
				{
				setState(233);
				method_parameter_list();
				}
			}

			setState(236);
			match(CB);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ArgumentContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Parameter_modifierContext parameter_modifier() {
			return getRuleContext(Parameter_modifierContext.class,0);
		}
		public List<TerminalNode> NAME() { return getTokens(MCSharpParser.NAME); }
		public TerminalNode NAME(int i) {
			return getToken(MCSharpParser.NAME, i);
		}
		public ArgumentContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_argument; }
	}

	public final ArgumentContext argument() throws RecognitionException {
		ArgumentContext _localctx = new ArgumentContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_argument);
		try {
			setState(243);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
			case OP:
			case PLUS:
			case MINUS:
			case MULTIPLY:
			case INCREMENT:
			case DECREMENT:
			case BITWISE_AND:
			case BITWISE_NOT:
			case BOOLEAN_NOT:
			case NEW:
			case TYPEOF:
			case CHECKED:
			case UNCHECKED:
			case DEFAULT:
			case SIZEOF:
			case STRING:
			case DECIMAL:
			case INTEGER:
			case NAME:
				enterOuterAlt(_localctx, 1);
				{
				setState(238);
				expression();
				}
				break;
			case IN:
			case OUT:
			case REF:
				enterOuterAlt(_localctx, 2);
				{
				setState(239);
				parameter_modifier();
				setState(240);
				match(NAME);
				setState(241);
				match(NAME);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Argument_listContext extends ParserRuleContext {
		public List<ArgumentContext> argument() {
			return getRuleContexts(ArgumentContext.class);
		}
		public ArgumentContext argument(int i) {
			return getRuleContext(ArgumentContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(MCSharpParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(MCSharpParser.COMMA, i);
		}
		public Argument_listContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_argument_list; }
	}

	public final Argument_listContext argument_list() throws RecognitionException {
		Argument_listContext _localctx = new Argument_listContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_argument_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(245);
			argument();
			setState(250);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(246);
				match(COMMA);
				setState(247);
				argument();
				}
				}
				setState(252);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Generic_argumentsContext extends ParserRuleContext {
		public TerminalNode LESS_THAN() { return getToken(MCSharpParser.LESS_THAN, 0); }
		public TerminalNode GREATER_THAN() { return getToken(MCSharpParser.GREATER_THAN, 0); }
		public Generic_parameter_listContext generic_parameter_list() {
			return getRuleContext(Generic_parameter_listContext.class,0);
		}
		public Generic_argumentsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_generic_arguments; }
	}

	public final Generic_argumentsContext generic_arguments() throws RecognitionException {
		Generic_argumentsContext _localctx = new Generic_argumentsContext(_ctx, getState());
		enterRule(_localctx, 20, RULE_generic_arguments);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(253);
			match(LESS_THAN);
			setState(255);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==NAME) {
				{
				setState(254);
				generic_parameter_list();
				}
			}

			setState(257);
			match(GREATER_THAN);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Method_argumentsContext extends ParserRuleContext {
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public Argument_listContext argument_list() {
			return getRuleContext(Argument_listContext.class,0);
		}
		public Method_argumentsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_method_arguments; }
	}

	public final Method_argumentsContext method_arguments() throws RecognitionException {
		Method_argumentsContext _localctx = new Method_argumentsContext(_ctx, getState());
		enterRule(_localctx, 22, RULE_method_arguments);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(259);
			match(OP);
			setState(261);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << OP) | (1L << PLUS) | (1L << MINUS) | (1L << MULTIPLY) | (1L << INCREMENT) | (1L << DECREMENT) | (1L << BITWISE_AND) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT) | (1L << IN) | (1L << OUT))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (REF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				setState(260);
				argument_list();
				}
			}

			setState(263);
			match(CP);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Indexer_argumentsContext extends ParserRuleContext {
		public TerminalNode OB() { return getToken(MCSharpParser.OB, 0); }
		public TerminalNode CB() { return getToken(MCSharpParser.CB, 0); }
		public Argument_listContext argument_list() {
			return getRuleContext(Argument_listContext.class,0);
		}
		public Indexer_argumentsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_indexer_arguments; }
	}

	public final Indexer_argumentsContext indexer_arguments() throws RecognitionException {
		Indexer_argumentsContext _localctx = new Indexer_argumentsContext(_ctx, getState());
		enterRule(_localctx, 24, RULE_indexer_arguments);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(265);
			match(OB);
			setState(267);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << OP) | (1L << PLUS) | (1L << MINUS) | (1L << MULTIPLY) | (1L << INCREMENT) | (1L << DECREMENT) | (1L << BITWISE_AND) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT) | (1L << IN) | (1L << OUT))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (REF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				setState(266);
				argument_list();
				}
			}

			setState(269);
			match(CB);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Member_initializerContext extends ParserRuleContext {
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public TerminalNode ASSIGN() { return getToken(MCSharpParser.ASSIGN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Object_or_collection_initializerContext object_or_collection_initializer() {
			return getRuleContext(Object_or_collection_initializerContext.class,0);
		}
		public Generic_argumentsContext generic_arguments() {
			return getRuleContext(Generic_argumentsContext.class,0);
		}
		public Member_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_member_initializer; }
	}

	public final Member_initializerContext member_initializer() throws RecognitionException {
		Member_initializerContext _localctx = new Member_initializerContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_member_initializer);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(271);
			match(NAME);
			setState(273);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LESS_THAN) {
				{
				setState(272);
				generic_arguments();
				}
			}

			setState(275);
			match(ASSIGN);
			setState(278);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
			case OP:
			case PLUS:
			case MINUS:
			case MULTIPLY:
			case INCREMENT:
			case DECREMENT:
			case BITWISE_AND:
			case BITWISE_NOT:
			case BOOLEAN_NOT:
			case NEW:
			case TYPEOF:
			case CHECKED:
			case UNCHECKED:
			case DEFAULT:
			case SIZEOF:
			case STRING:
			case DECIMAL:
			case INTEGER:
			case NAME:
				{
				setState(276);
				expression();
				}
				break;
			case OC:
				{
				setState(277);
				object_or_collection_initializer();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Object_initializerContext extends ParserRuleContext {
		public TerminalNode OC() { return getToken(MCSharpParser.OC, 0); }
		public TerminalNode CC() { return getToken(MCSharpParser.CC, 0); }
		public List<Member_initializerContext> member_initializer() {
			return getRuleContexts(Member_initializerContext.class);
		}
		public Member_initializerContext member_initializer(int i) {
			return getRuleContext(Member_initializerContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(MCSharpParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(MCSharpParser.COMMA, i);
		}
		public Object_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_object_initializer; }
	}

	public final Object_initializerContext object_initializer() throws RecognitionException {
		Object_initializerContext _localctx = new Object_initializerContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_object_initializer);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(280);
			match(OC);
			setState(289);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==NAME) {
				{
				setState(281);
				member_initializer();
				setState(284);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,13,_ctx) ) {
				case 1:
					{
					setState(282);
					match(COMMA);
					setState(283);
					member_initializer();
					}
					break;
				}
				setState(287);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==COMMA) {
					{
					setState(286);
					match(COMMA);
					}
				}

				}
			}

			setState(291);
			match(CC);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Element_initializerContext extends ParserRuleContext {
		public Non_assignment_expressionContext non_assignment_expression() {
			return getRuleContext(Non_assignment_expressionContext.class,0);
		}
		public TerminalNode OC() { return getToken(MCSharpParser.OC, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode CC() { return getToken(MCSharpParser.CC, 0); }
		public Element_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_element_initializer; }
	}

	public final Element_initializerContext element_initializer() throws RecognitionException {
		Element_initializerContext _localctx = new Element_initializerContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_element_initializer);
		try {
			setState(298);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
			case OP:
			case PLUS:
			case MINUS:
			case MULTIPLY:
			case INCREMENT:
			case DECREMENT:
			case BITWISE_AND:
			case BITWISE_NOT:
			case BOOLEAN_NOT:
			case NEW:
			case TYPEOF:
			case CHECKED:
			case UNCHECKED:
			case DEFAULT:
			case SIZEOF:
			case STRING:
			case DECIMAL:
			case INTEGER:
			case NAME:
				enterOuterAlt(_localctx, 1);
				{
				setState(293);
				non_assignment_expression();
				}
				break;
			case OC:
				enterOuterAlt(_localctx, 2);
				{
				setState(294);
				match(OC);
				setState(295);
				expression();
				setState(296);
				match(CC);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Collection_initializerContext extends ParserRuleContext {
		public TerminalNode OC() { return getToken(MCSharpParser.OC, 0); }
		public TerminalNode CC() { return getToken(MCSharpParser.CC, 0); }
		public List<Element_initializerContext> element_initializer() {
			return getRuleContexts(Element_initializerContext.class);
		}
		public Element_initializerContext element_initializer(int i) {
			return getRuleContext(Element_initializerContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(MCSharpParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(MCSharpParser.COMMA, i);
		}
		public Collection_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_collection_initializer; }
	}

	public final Collection_initializerContext collection_initializer() throws RecognitionException {
		Collection_initializerContext _localctx = new Collection_initializerContext(_ctx, getState());
		enterRule(_localctx, 32, RULE_collection_initializer);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(300);
			match(OC);
			setState(309);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << OP) | (1L << OC) | (1L << PLUS) | (1L << MINUS) | (1L << MULTIPLY) | (1L << INCREMENT) | (1L << DECREMENT) | (1L << BITWISE_AND) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				setState(301);
				element_initializer();
				setState(304);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,17,_ctx) ) {
				case 1:
					{
					setState(302);
					match(COMMA);
					setState(303);
					element_initializer();
					}
					break;
				}
				setState(307);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==COMMA) {
					{
					setState(306);
					match(COMMA);
					}
				}

				}
			}

			setState(311);
			match(CC);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Anonymous_element_initializerContext extends ParserRuleContext {
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public Generic_argumentsContext generic_arguments() {
			return getRuleContext(Generic_argumentsContext.class,0);
		}
		public Member_accessContext member_access() {
			return getRuleContext(Member_accessContext.class,0);
		}
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public TerminalNode ASSIGN() { return getToken(MCSharpParser.ASSIGN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Anonymous_element_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_anonymous_element_initializer; }
	}

	public final Anonymous_element_initializerContext anonymous_element_initializer() throws RecognitionException {
		Anonymous_element_initializerContext _localctx = new Anonymous_element_initializerContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_anonymous_element_initializer);
		try {
			setState(322);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,21,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(313);
				match(NAME);
				setState(315);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,20,_ctx) ) {
				case 1:
					{
					setState(314);
					generic_arguments();
					}
					break;
				}
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(317);
				member_access();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(318);
				identifier();
				setState(319);
				match(ASSIGN);
				setState(320);
				expression();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Anonymous_object_initializerContext extends ParserRuleContext {
		public TerminalNode OC() { return getToken(MCSharpParser.OC, 0); }
		public TerminalNode CC() { return getToken(MCSharpParser.CC, 0); }
		public List<Anonymous_element_initializerContext> anonymous_element_initializer() {
			return getRuleContexts(Anonymous_element_initializerContext.class);
		}
		public Anonymous_element_initializerContext anonymous_element_initializer(int i) {
			return getRuleContext(Anonymous_element_initializerContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(MCSharpParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(MCSharpParser.COMMA, i);
		}
		public Anonymous_object_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_anonymous_object_initializer; }
	}

	public final Anonymous_object_initializerContext anonymous_object_initializer() throws RecognitionException {
		Anonymous_object_initializerContext _localctx = new Anonymous_object_initializerContext(_ctx, getState());
		enterRule(_localctx, 36, RULE_anonymous_object_initializer);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(324);
			match(OC);
			setState(333);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__0 || _la==OP || _la==NAME) {
				{
				setState(325);
				anonymous_element_initializer();
				setState(328);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,22,_ctx) ) {
				case 1:
					{
					setState(326);
					match(COMMA);
					setState(327);
					anonymous_element_initializer();
					}
					break;
				}
				setState(331);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==COMMA) {
					{
					setState(330);
					match(COMMA);
					}
				}

				}
			}

			setState(335);
			match(CC);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ModifierContext extends ParserRuleContext {
		public TerminalNode PUBLIC() { return getToken(MCSharpParser.PUBLIC, 0); }
		public TerminalNode PRIVATE() { return getToken(MCSharpParser.PRIVATE, 0); }
		public TerminalNode PROTECTED() { return getToken(MCSharpParser.PROTECTED, 0); }
		public TerminalNode STATIC() { return getToken(MCSharpParser.STATIC, 0); }
		public TerminalNode ABSTRACT() { return getToken(MCSharpParser.ABSTRACT, 0); }
		public TerminalNode VIRTUAL() { return getToken(MCSharpParser.VIRTUAL, 0); }
		public ModifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_modifier; }
	}

	public final ModifierContext modifier() throws RecognitionException {
		ModifierContext _localctx = new ModifierContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_modifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(337);
			_la = _input.LA(1);
			if ( !(((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Parameter_modifierContext extends ParserRuleContext {
		public TerminalNode IN() { return getToken(MCSharpParser.IN, 0); }
		public TerminalNode OUT() { return getToken(MCSharpParser.OUT, 0); }
		public TerminalNode REF() { return getToken(MCSharpParser.REF, 0); }
		public Parameter_modifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_parameter_modifier; }
	}

	public final Parameter_modifierContext parameter_modifier() throws RecognitionException {
		Parameter_modifierContext _localctx = new Parameter_modifierContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_parameter_modifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(339);
			_la = _input.LA(1);
			if ( !(((((_la - 51)) & ~0x3f) == 0 && ((1L << (_la - 51)) & ((1L << (IN - 51)) | (1L << (OUT - 51)) | (1L << (REF - 51)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Class_typeContext extends ParserRuleContext {
		public TerminalNode CLASS() { return getToken(MCSharpParser.CLASS, 0); }
		public TerminalNode STRUCT() { return getToken(MCSharpParser.STRUCT, 0); }
		public Class_typeContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_class_type; }
	}

	public final Class_typeContext class_type() throws RecognitionException {
		Class_typeContext _localctx = new Class_typeContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_class_type);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(341);
			_la = _input.LA(1);
			if ( !(_la==CLASS || _la==STRUCT) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Type_definitionContext extends ParserRuleContext {
		public Class_typeContext class_type() {
			return getRuleContext(Class_typeContext.class,0);
		}
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public TerminalNode OC() { return getToken(MCSharpParser.OC, 0); }
		public TerminalNode CC() { return getToken(MCSharpParser.CC, 0); }
		public List<ModifierContext> modifier() {
			return getRuleContexts(ModifierContext.class);
		}
		public ModifierContext modifier(int i) {
			return getRuleContext(ModifierContext.class,i);
		}
		public List<Member_definitionContext> member_definition() {
			return getRuleContexts(Member_definitionContext.class);
		}
		public Member_definitionContext member_definition(int i) {
			return getRuleContext(Member_definitionContext.class,i);
		}
		public Type_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type_definition; }
	}

	public final Type_definitionContext type_definition() throws RecognitionException {
		Type_definitionContext _localctx = new Type_definitionContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_type_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(346);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
				{
				{
				setState(343);
				modifier();
				}
				}
				setState(348);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(349);
			class_type();
			setState(350);
			match(NAME);
			setState(351);
			match(OC);
			setState(355);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)) | (1L << (NAME - 76)))) != 0)) {
				{
				{
				setState(352);
				member_definition();
				}
				}
				setState(357);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(358);
			match(CC);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Member_definitionContext extends ParserRuleContext {
		public List<TerminalNode> NAME() { return getTokens(MCSharpParser.NAME); }
		public TerminalNode NAME(int i) {
			return getToken(MCSharpParser.NAME, i);
		}
		public Field_definitionContext field_definition() {
			return getRuleContext(Field_definitionContext.class,0);
		}
		public Property_definitionContext property_definition() {
			return getRuleContext(Property_definitionContext.class,0);
		}
		public Method_definitionContext method_definition() {
			return getRuleContext(Method_definitionContext.class,0);
		}
		public List<ModifierContext> modifier() {
			return getRuleContexts(ModifierContext.class);
		}
		public ModifierContext modifier(int i) {
			return getRuleContext(ModifierContext.class,i);
		}
		public Member_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_member_definition; }
	}

	public final Member_definitionContext member_definition() throws RecognitionException {
		Member_definitionContext _localctx = new Member_definitionContext(_ctx, getState());
		enterRule(_localctx, 46, RULE_member_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(363);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
				{
				{
				setState(360);
				modifier();
				}
				}
				setState(365);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(366);
			match(NAME);
			setState(367);
			match(NAME);
			setState(371);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case END:
			case ASSIGN:
				{
				setState(368);
				field_definition();
				}
				break;
			case OC:
			case LAMBDA:
			case SET:
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case STATIC:
			case ABSTRACT:
			case VIRTUAL:
				{
				setState(369);
				property_definition();
				}
				break;
			case OP:
			case LESS_THAN:
				{
				setState(370);
				method_definition();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Field_definitionContext extends ParserRuleContext {
		public TerminalNode END() { return getToken(MCSharpParser.END, 0); }
		public TerminalNode ASSIGN() { return getToken(MCSharpParser.ASSIGN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Field_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_field_definition; }
	}

	public final Field_definitionContext field_definition() throws RecognitionException {
		Field_definitionContext _localctx = new Field_definitionContext(_ctx, getState());
		enterRule(_localctx, 48, RULE_field_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(375);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==ASSIGN) {
				{
				setState(373);
				match(ASSIGN);
				setState(374);
				expression();
				}
			}

			setState(377);
			match(END);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Property_definitionContext extends ParserRuleContext {
		public TerminalNode LAMBDA() { return getToken(MCSharpParser.LAMBDA, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode OC() { return getToken(MCSharpParser.OC, 0); }
		public Property_get_definitionContext property_get_definition() {
			return getRuleContext(Property_get_definitionContext.class,0);
		}
		public Property_set_definitionContext property_set_definition() {
			return getRuleContext(Property_set_definitionContext.class,0);
		}
		public List<ModifierContext> modifier() {
			return getRuleContexts(ModifierContext.class);
		}
		public ModifierContext modifier(int i) {
			return getRuleContext(ModifierContext.class,i);
		}
		public TerminalNode CC() { return getToken(MCSharpParser.CC, 0); }
		public Property_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_property_definition; }
	}

	public final Property_definitionContext property_definition() throws RecognitionException {
		Property_definitionContext _localctx = new Property_definitionContext(_ctx, getState());
		enterRule(_localctx, 50, RULE_property_definition);
		int _la;
		try {
			setState(418);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LAMBDA:
				enterOuterAlt(_localctx, 1);
				{
				setState(379);
				match(LAMBDA);
				setState(380);
				expression();
				}
				break;
			case OC:
				enterOuterAlt(_localctx, 2);
				{
				setState(381);
				match(OC);
				{
				{
				setState(385);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
					{
					{
					setState(382);
					modifier();
					}
					}
					setState(387);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(388);
				property_get_definition();
				}
				setState(397);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,32,_ctx) ) {
				case 1:
					{
					setState(393);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
						{
						{
						setState(390);
						modifier();
						}
						}
						setState(395);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(396);
					property_set_definition();
					}
					break;
				}
				}
				}
				break;
			case SET:
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case STATIC:
			case ABSTRACT:
			case VIRTUAL:
				enterOuterAlt(_localctx, 3);
				{
				{
				{
				setState(402);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
					{
					{
					setState(399);
					modifier();
					}
					}
					setState(404);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(405);
				property_set_definition();
				}
				setState(414);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (((((_la - 74)) & ~0x3f) == 0 && ((1L << (_la - 74)) & ((1L << (GET - 74)) | (1L << (PUBLIC - 74)) | (1L << (PRIVATE - 74)) | (1L << (PROTECTED - 74)) | (1L << (STATIC - 74)) | (1L << (ABSTRACT - 74)) | (1L << (VIRTUAL - 74)))) != 0)) {
					{
					setState(410);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
						{
						{
						setState(407);
						modifier();
						}
						}
						setState(412);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(413);
					property_get_definition();
					}
				}

				}
				setState(416);
				match(CC);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Property_get_definitionContext extends ParserRuleContext {
		public TerminalNode GET() { return getToken(MCSharpParser.GET, 0); }
		public TerminalNode END() { return getToken(MCSharpParser.END, 0); }
		public TerminalNode LAMBDA() { return getToken(MCSharpParser.LAMBDA, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Code_blockContext code_block() {
			return getRuleContext(Code_blockContext.class,0);
		}
		public Property_get_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_property_get_definition; }
	}

	public final Property_get_definitionContext property_get_definition() throws RecognitionException {
		Property_get_definitionContext _localctx = new Property_get_definitionContext(_ctx, getState());
		enterRule(_localctx, 52, RULE_property_get_definition);
		try {
			setState(429);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,37,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(420);
				match(GET);
				setState(421);
				match(END);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(422);
				match(GET);
				setState(423);
				match(LAMBDA);
				setState(424);
				expression();
				setState(425);
				match(END);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(427);
				match(GET);
				setState(428);
				code_block();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Property_set_definitionContext extends ParserRuleContext {
		public TerminalNode SET() { return getToken(MCSharpParser.SET, 0); }
		public TerminalNode END() { return getToken(MCSharpParser.END, 0); }
		public TerminalNode LAMBDA() { return getToken(MCSharpParser.LAMBDA, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Code_blockContext code_block() {
			return getRuleContext(Code_blockContext.class,0);
		}
		public Property_set_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_property_set_definition; }
	}

	public final Property_set_definitionContext property_set_definition() throws RecognitionException {
		Property_set_definitionContext _localctx = new Property_set_definitionContext(_ctx, getState());
		enterRule(_localctx, 54, RULE_property_set_definition);
		try {
			setState(440);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,38,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(431);
				match(SET);
				setState(432);
				match(END);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(433);
				match(SET);
				setState(434);
				match(LAMBDA);
				setState(435);
				expression();
				setState(436);
				match(END);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(438);
				match(SET);
				setState(439);
				code_block();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Method_definitionContext extends ParserRuleContext {
		public Method_parametersContext method_parameters() {
			return getRuleContext(Method_parametersContext.class,0);
		}
		public TerminalNode LAMBDA() { return getToken(MCSharpParser.LAMBDA, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode END() { return getToken(MCSharpParser.END, 0); }
		public Code_blockContext code_block() {
			return getRuleContext(Code_blockContext.class,0);
		}
		public Generic_parametersContext generic_parameters() {
			return getRuleContext(Generic_parametersContext.class,0);
		}
		public Method_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_method_definition; }
	}

	public final Method_definitionContext method_definition() throws RecognitionException {
		Method_definitionContext _localctx = new Method_definitionContext(_ctx, getState());
		enterRule(_localctx, 56, RULE_method_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(443);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LESS_THAN) {
				{
				setState(442);
				generic_parameters();
				}
			}

			setState(445);
			method_parameters();
			setState(451);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LAMBDA:
				{
				setState(446);
				match(LAMBDA);
				setState(447);
				expression();
				setState(448);
				match(END);
				}
				break;
			case OC:
				{
				setState(450);
				code_block();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class LiteralContext extends ParserRuleContext {
		public TerminalNode INTEGER() { return getToken(MCSharpParser.INTEGER, 0); }
		public TerminalNode DECIMAL() { return getToken(MCSharpParser.DECIMAL, 0); }
		public TerminalNode STRING() { return getToken(MCSharpParser.STRING, 0); }
		public LiteralContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_literal; }
	}

	public final LiteralContext literal() throws RecognitionException {
		LiteralContext _localctx = new LiteralContext(_ctx, getState());
		enterRule(_localctx, 58, RULE_literal);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(453);
			_la = _input.LA(1);
			if ( !(((((_la - 86)) & ~0x3f) == 0 && ((1L << (_la - 86)) & ((1L << (STRING - 86)) | (1L << (DECIMAL - 86)) | (1L << (INTEGER - 86)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IdentifierContext extends ParserRuleContext {
		public List<TerminalNode> NAME() { return getTokens(MCSharpParser.NAME); }
		public TerminalNode NAME(int i) {
			return getToken(MCSharpParser.NAME, i);
		}
		public List<TerminalNode> DOT() { return getTokens(MCSharpParser.DOT); }
		public TerminalNode DOT(int i) {
			return getToken(MCSharpParser.DOT, i);
		}
		public Generic_argumentsContext generic_arguments() {
			return getRuleContext(Generic_argumentsContext.class,0);
		}
		public IdentifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_identifier; }
	}

	public final IdentifierContext identifier() throws RecognitionException {
		IdentifierContext _localctx = new IdentifierContext(_ctx, getState());
		enterRule(_localctx, 60, RULE_identifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(456);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__0) {
				{
				setState(455);
				match(T__0);
				}
			}

			{
			setState(458);
			match(NAME);
			}
			setState(463);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==DOT) {
				{
				{
				setState(459);
				match(DOT);
				setState(460);
				match(NAME);
				}
				}
				setState(465);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(467);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,43,_ctx) ) {
			case 1:
				{
				setState(466);
				generic_arguments();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StatementContext extends ParserRuleContext {
		public Code_blockContext code_block() {
			return getRuleContext(Code_blockContext.class,0);
		}
		public Language_functionContext language_function() {
			return getRuleContext(Language_functionContext.class,0);
		}
		public Initialization_expressionContext initialization_expression() {
			return getRuleContext(Initialization_expressionContext.class,0);
		}
		public TerminalNode END() { return getToken(MCSharpParser.END, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 62, RULE_statement);
		try {
			setState(477);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,44,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(469);
				code_block();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(470);
				language_function();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(471);
				initialization_expression();
				setState(472);
				match(END);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(474);
				expression();
				setState(475);
				match(END);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Code_blockContext extends ParserRuleContext {
		public TerminalNode OC() { return getToken(MCSharpParser.OC, 0); }
		public TerminalNode CC() { return getToken(MCSharpParser.CC, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public Code_blockContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_code_block; }
	}

	public final Code_blockContext code_block() throws RecognitionException {
		Code_blockContext _localctx = new Code_blockContext(_ctx, getState());
		enterRule(_localctx, 64, RULE_code_block);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(479);
			match(OC);
			setState(483);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << OP) | (1L << OC) | (1L << PLUS) | (1L << MINUS) | (1L << MULTIPLY) | (1L << INCREMENT) | (1L << DECREMENT) | (1L << BITWISE_AND) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT) | (1L << IF) | (1L << FOR) | (1L << FOREACH) | (1L << DO) | (1L << WHILE) | (1L << RETURN) | (1L << THROW) | (1L << TRY))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				{
				setState(480);
				statement();
				}
				}
				setState(485);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(486);
			match(CC);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Additive_operatorContext extends ParserRuleContext {
		public TerminalNode PLUS() { return getToken(MCSharpParser.PLUS, 0); }
		public TerminalNode MINUS() { return getToken(MCSharpParser.MINUS, 0); }
		public Additive_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_additive_operator; }
	}

	public final Additive_operatorContext additive_operator() throws RecognitionException {
		Additive_operatorContext _localctx = new Additive_operatorContext(_ctx, getState());
		enterRule(_localctx, 66, RULE_additive_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(488);
			_la = _input.LA(1);
			if ( !(_la==PLUS || _la==MINUS) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Multiplicative_operatorContext extends ParserRuleContext {
		public TerminalNode MULTIPLY() { return getToken(MCSharpParser.MULTIPLY, 0); }
		public TerminalNode DIVIDE() { return getToken(MCSharpParser.DIVIDE, 0); }
		public TerminalNode MODULUS() { return getToken(MCSharpParser.MODULUS, 0); }
		public Multiplicative_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_multiplicative_operator; }
	}

	public final Multiplicative_operatorContext multiplicative_operator() throws RecognitionException {
		Multiplicative_operatorContext _localctx = new Multiplicative_operatorContext(_ctx, getState());
		enterRule(_localctx, 68, RULE_multiplicative_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(490);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << MULTIPLY) | (1L << DIVIDE) | (1L << MODULUS))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Step_operatorContext extends ParserRuleContext {
		public TerminalNode INCREMENT() { return getToken(MCSharpParser.INCREMENT, 0); }
		public TerminalNode DECREMENT() { return getToken(MCSharpParser.DECREMENT, 0); }
		public Step_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_step_operator; }
	}

	public final Step_operatorContext step_operator() throws RecognitionException {
		Step_operatorContext _localctx = new Step_operatorContext(_ctx, getState());
		enterRule(_localctx, 70, RULE_step_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(492);
			_la = _input.LA(1);
			if ( !(_la==INCREMENT || _la==DECREMENT) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Bitwise_operatorContext extends ParserRuleContext {
		public TerminalNode BITWISE_AND() { return getToken(MCSharpParser.BITWISE_AND, 0); }
		public TerminalNode BITWISE_OR() { return getToken(MCSharpParser.BITWISE_OR, 0); }
		public TerminalNode BITWISE_XOR() { return getToken(MCSharpParser.BITWISE_XOR, 0); }
		public Bitwise_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_bitwise_operator; }
	}

	public final Bitwise_operatorContext bitwise_operator() throws RecognitionException {
		Bitwise_operatorContext _localctx = new Bitwise_operatorContext(_ctx, getState());
		enterRule(_localctx, 72, RULE_bitwise_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(494);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << BITWISE_AND) | (1L << BITWISE_OR) | (1L << BITWISE_XOR))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Boolean_operatorContext extends ParserRuleContext {
		public TerminalNode BOOLEAN_AND() { return getToken(MCSharpParser.BOOLEAN_AND, 0); }
		public TerminalNode BOOLEAN_OR() { return getToken(MCSharpParser.BOOLEAN_OR, 0); }
		public Boolean_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_boolean_operator; }
	}

	public final Boolean_operatorContext boolean_operator() throws RecognitionException {
		Boolean_operatorContext _localctx = new Boolean_operatorContext(_ctx, getState());
		enterRule(_localctx, 74, RULE_boolean_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(496);
			_la = _input.LA(1);
			if ( !(_la==BOOLEAN_AND || _la==BOOLEAN_OR) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Shift_operatorContext extends ParserRuleContext {
		public TerminalNode SHIFT_LEFT() { return getToken(MCSharpParser.SHIFT_LEFT, 0); }
		public TerminalNode SHIFT_RIGHT() { return getToken(MCSharpParser.SHIFT_RIGHT, 0); }
		public Shift_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_shift_operator; }
	}

	public final Shift_operatorContext shift_operator() throws RecognitionException {
		Shift_operatorContext _localctx = new Shift_operatorContext(_ctx, getState());
		enterRule(_localctx, 76, RULE_shift_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(498);
			_la = _input.LA(1);
			if ( !(_la==SHIFT_LEFT || _la==SHIFT_RIGHT) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Equality_operatorContext extends ParserRuleContext {
		public TerminalNode EQUIVALENT() { return getToken(MCSharpParser.EQUIVALENT, 0); }
		public TerminalNode NOT_EQUIVALENT() { return getToken(MCSharpParser.NOT_EQUIVALENT, 0); }
		public Equality_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_equality_operator; }
	}

	public final Equality_operatorContext equality_operator() throws RecognitionException {
		Equality_operatorContext _localctx = new Equality_operatorContext(_ctx, getState());
		enterRule(_localctx, 78, RULE_equality_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(500);
			_la = _input.LA(1);
			if ( !(_la==EQUIVALENT || _la==NOT_EQUIVALENT) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Relation_operatorContext extends ParserRuleContext {
		public TerminalNode LESS_THAN() { return getToken(MCSharpParser.LESS_THAN, 0); }
		public TerminalNode GREATER_THAN() { return getToken(MCSharpParser.GREATER_THAN, 0); }
		public TerminalNode LESS_THAN_EQUAL() { return getToken(MCSharpParser.LESS_THAN_EQUAL, 0); }
		public TerminalNode GREATER_THAN_EQUAL() { return getToken(MCSharpParser.GREATER_THAN_EQUAL, 0); }
		public Relation_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_relation_operator; }
	}

	public final Relation_operatorContext relation_operator() throws RecognitionException {
		Relation_operatorContext _localctx = new Relation_operatorContext(_ctx, getState());
		enterRule(_localctx, 80, RULE_relation_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(502);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << LESS_THAN) | (1L << GREATER_THAN) | (1L << LESS_THAN_EQUAL) | (1L << GREATER_THAN_EQUAL))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Assignment_operatorContext extends ParserRuleContext {
		public TerminalNode ASSIGN() { return getToken(MCSharpParser.ASSIGN, 0); }
		public TerminalNode ASSIGN_PLUS() { return getToken(MCSharpParser.ASSIGN_PLUS, 0); }
		public TerminalNode ASSIGN_MINUS() { return getToken(MCSharpParser.ASSIGN_MINUS, 0); }
		public TerminalNode ASSIGN_MULTIPLY() { return getToken(MCSharpParser.ASSIGN_MULTIPLY, 0); }
		public TerminalNode ASSIGN_DIVIDE() { return getToken(MCSharpParser.ASSIGN_DIVIDE, 0); }
		public TerminalNode ASSIGN_MODULUS() { return getToken(MCSharpParser.ASSIGN_MODULUS, 0); }
		public TerminalNode ASSIGN_ACCESS() { return getToken(MCSharpParser.ASSIGN_ACCESS, 0); }
		public TerminalNode ASSIGN_AND() { return getToken(MCSharpParser.ASSIGN_AND, 0); }
		public TerminalNode ASSIGN_OR() { return getToken(MCSharpParser.ASSIGN_OR, 0); }
		public TerminalNode ASSIGN_XOR() { return getToken(MCSharpParser.ASSIGN_XOR, 0); }
		public TerminalNode ASSIGN_LEFT() { return getToken(MCSharpParser.ASSIGN_LEFT, 0); }
		public TerminalNode ASSIGN_RIGHT() { return getToken(MCSharpParser.ASSIGN_RIGHT, 0); }
		public Assignment_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignment_operator; }
	}

	public final Assignment_operatorContext assignment_operator() throws RecognitionException {
		Assignment_operatorContext _localctx = new Assignment_operatorContext(_ctx, getState());
		enterRule(_localctx, 82, RULE_assignment_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(504);
			_la = _input.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << ASSIGN) | (1L << ASSIGN_PLUS) | (1L << ASSIGN_MINUS) | (1L << ASSIGN_MULTIPLY) | (1L << ASSIGN_DIVIDE) | (1L << ASSIGN_MODULUS) | (1L << ASSIGN_ACCESS) | (1L << ASSIGN_AND) | (1L << ASSIGN_OR) | (1L << ASSIGN_XOR) | (1L << ASSIGN_LEFT) | (1L << ASSIGN_RIGHT))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Range_operatorContext extends ParserRuleContext {
		public TerminalNode RANGE_INCLUSIVE() { return getToken(MCSharpParser.RANGE_INCLUSIVE, 0); }
		public TerminalNode RANGE_EXCLUSIVE() { return getToken(MCSharpParser.RANGE_EXCLUSIVE, 0); }
		public Range_operatorContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_range_operator; }
	}

	public final Range_operatorContext range_operator() throws RecognitionException {
		Range_operatorContext _localctx = new Range_operatorContext(_ctx, getState());
		enterRule(_localctx, 84, RULE_range_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(506);
			_la = _input.LA(1);
			if ( !(_la==RANGE_INCLUSIVE || _la==RANGE_EXCLUSIVE) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Language_functionContext extends ParserRuleContext {
		public If_statementContext if_statement() {
			return getRuleContext(If_statementContext.class,0);
		}
		public For_statementContext for_statement() {
			return getRuleContext(For_statementContext.class,0);
		}
		public Foreach_statementContext foreach_statement() {
			return getRuleContext(Foreach_statementContext.class,0);
		}
		public While_statementContext while_statement() {
			return getRuleContext(While_statementContext.class,0);
		}
		public Do_statementContext do_statement() {
			return getRuleContext(Do_statementContext.class,0);
		}
		public Return_statementContext return_statement() {
			return getRuleContext(Return_statementContext.class,0);
		}
		public Throw_statementContext throw_statement() {
			return getRuleContext(Throw_statementContext.class,0);
		}
		public Try_statementContext try_statement() {
			return getRuleContext(Try_statementContext.class,0);
		}
		public Language_functionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_language_function; }
	}

	public final Language_functionContext language_function() throws RecognitionException {
		Language_functionContext _localctx = new Language_functionContext(_ctx, getState());
		enterRule(_localctx, 86, RULE_language_function);
		try {
			setState(516);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case IF:
				enterOuterAlt(_localctx, 1);
				{
				setState(508);
				if_statement();
				}
				break;
			case FOR:
				enterOuterAlt(_localctx, 2);
				{
				setState(509);
				for_statement();
				}
				break;
			case FOREACH:
				enterOuterAlt(_localctx, 3);
				{
				setState(510);
				foreach_statement();
				}
				break;
			case WHILE:
				enterOuterAlt(_localctx, 4);
				{
				setState(511);
				while_statement();
				}
				break;
			case DO:
				enterOuterAlt(_localctx, 5);
				{
				setState(512);
				do_statement();
				}
				break;
			case RETURN:
				enterOuterAlt(_localctx, 6);
				{
				setState(513);
				return_statement();
				}
				break;
			case THROW:
				enterOuterAlt(_localctx, 7);
				{
				setState(514);
				throw_statement();
				}
				break;
			case TRY:
				enterOuterAlt(_localctx, 8);
				{
				setState(515);
				try_statement();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class If_statementContext extends ParserRuleContext {
		public TerminalNode IF() { return getToken(MCSharpParser.IF, 0); }
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public TerminalNode ELSE() { return getToken(MCSharpParser.ELSE, 0); }
		public If_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_if_statement; }
	}

	public final If_statementContext if_statement() throws RecognitionException {
		If_statementContext _localctx = new If_statementContext(_ctx, getState());
		enterRule(_localctx, 88, RULE_if_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(518);
			match(IF);
			setState(519);
			match(OP);
			setState(520);
			expression();
			setState(521);
			match(CP);
			setState(522);
			statement();
			setState(525);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,47,_ctx) ) {
			case 1:
				{
				setState(523);
				match(ELSE);
				setState(524);
				statement();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class For_statementContext extends ParserRuleContext {
		public TerminalNode FOR() { return getToken(MCSharpParser.FOR, 0); }
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public Initialization_expressionContext initialization_expression() {
			return getRuleContext(Initialization_expressionContext.class,0);
		}
		public List<TerminalNode> END() { return getTokens(MCSharpParser.END); }
		public TerminalNode END(int i) {
			return getToken(MCSharpParser.END, i);
		}
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public For_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_for_statement; }
	}

	public final For_statementContext for_statement() throws RecognitionException {
		For_statementContext _localctx = new For_statementContext(_ctx, getState());
		enterRule(_localctx, 90, RULE_for_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(527);
			match(FOR);
			setState(528);
			match(OP);
			setState(529);
			initialization_expression();
			setState(530);
			match(END);
			setState(531);
			expression();
			setState(532);
			match(END);
			setState(533);
			expression();
			setState(534);
			match(CP);
			setState(535);
			statement();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Foreach_statementContext extends ParserRuleContext {
		public TerminalNode FOREACH() { return getToken(MCSharpParser.FOREACH, 0); }
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public List<TerminalNode> NAME() { return getTokens(MCSharpParser.NAME); }
		public TerminalNode NAME(int i) {
			return getToken(MCSharpParser.NAME, i);
		}
		public TerminalNode IN() { return getToken(MCSharpParser.IN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public Foreach_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_foreach_statement; }
	}

	public final Foreach_statementContext foreach_statement() throws RecognitionException {
		Foreach_statementContext _localctx = new Foreach_statementContext(_ctx, getState());
		enterRule(_localctx, 92, RULE_foreach_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(537);
			match(FOREACH);
			setState(538);
			match(OP);
			setState(539);
			match(NAME);
			setState(540);
			match(NAME);
			setState(541);
			match(IN);
			setState(542);
			expression();
			setState(543);
			match(CP);
			setState(544);
			statement();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class While_statementContext extends ParserRuleContext {
		public TerminalNode WHILE() { return getToken(MCSharpParser.WHILE, 0); }
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public While_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_while_statement; }
	}

	public final While_statementContext while_statement() throws RecognitionException {
		While_statementContext _localctx = new While_statementContext(_ctx, getState());
		enterRule(_localctx, 94, RULE_while_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(546);
			match(WHILE);
			setState(547);
			match(OP);
			setState(548);
			expression();
			setState(549);
			match(CP);
			setState(550);
			statement();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Do_statementContext extends ParserRuleContext {
		public TerminalNode DO() { return getToken(MCSharpParser.DO, 0); }
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public TerminalNode WHILE() { return getToken(MCSharpParser.WHILE, 0); }
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public TerminalNode END() { return getToken(MCSharpParser.END, 0); }
		public Do_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_do_statement; }
	}

	public final Do_statementContext do_statement() throws RecognitionException {
		Do_statementContext _localctx = new Do_statementContext(_ctx, getState());
		enterRule(_localctx, 96, RULE_do_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(552);
			match(DO);
			setState(553);
			statement();
			setState(554);
			match(WHILE);
			setState(555);
			match(OP);
			setState(556);
			expression();
			setState(557);
			match(CP);
			setState(558);
			match(END);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Return_statementContext extends ParserRuleContext {
		public TerminalNode RETURN() { return getToken(MCSharpParser.RETURN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode END() { return getToken(MCSharpParser.END, 0); }
		public Return_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_return_statement; }
	}

	public final Return_statementContext return_statement() throws RecognitionException {
		Return_statementContext _localctx = new Return_statementContext(_ctx, getState());
		enterRule(_localctx, 98, RULE_return_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(560);
			match(RETURN);
			setState(561);
			expression();
			setState(562);
			match(END);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Throw_statementContext extends ParserRuleContext {
		public TerminalNode THROW() { return getToken(MCSharpParser.THROW, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode END() { return getToken(MCSharpParser.END, 0); }
		public Throw_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_throw_statement; }
	}

	public final Throw_statementContext throw_statement() throws RecognitionException {
		Throw_statementContext _localctx = new Throw_statementContext(_ctx, getState());
		enterRule(_localctx, 100, RULE_throw_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(564);
			match(THROW);
			setState(565);
			expression();
			setState(566);
			match(END);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Try_statementContext extends ParserRuleContext {
		public TerminalNode TRY() { return getToken(MCSharpParser.TRY, 0); }
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public List<TerminalNode> CATCH() { return getTokens(MCSharpParser.CATCH); }
		public TerminalNode CATCH(int i) {
			return getToken(MCSharpParser.CATCH, i);
		}
		public List<TerminalNode> OP() { return getTokens(MCSharpParser.OP); }
		public TerminalNode OP(int i) {
			return getToken(MCSharpParser.OP, i);
		}
		public List<TerminalNode> NAME() { return getTokens(MCSharpParser.NAME); }
		public TerminalNode NAME(int i) {
			return getToken(MCSharpParser.NAME, i);
		}
		public List<TerminalNode> CP() { return getTokens(MCSharpParser.CP); }
		public TerminalNode CP(int i) {
			return getToken(MCSharpParser.CP, i);
		}
		public TerminalNode FINALLY() { return getToken(MCSharpParser.FINALLY, 0); }
		public Try_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_try_statement; }
	}

	public final Try_statementContext try_statement() throws RecognitionException {
		Try_statementContext _localctx = new Try_statementContext(_ctx, getState());
		enterRule(_localctx, 102, RULE_try_statement);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(568);
			match(TRY);
			setState(569);
			statement();
			setState(578);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,48,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(570);
					match(CATCH);
					setState(571);
					match(OP);
					setState(572);
					match(NAME);
					setState(573);
					match(NAME);
					setState(574);
					match(CP);
					setState(575);
					statement();
					}
					} 
				}
				setState(580);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,48,_ctx);
			}
			setState(583);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,49,_ctx) ) {
			case 1:
				{
				setState(581);
				match(FINALLY);
				setState(582);
				statement();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpressionContext extends ParserRuleContext {
		public Non_assignment_expressionContext non_assignment_expression() {
			return getRuleContext(Non_assignment_expressionContext.class,0);
		}
		public Assignment_expressionContext assignment_expression() {
			return getRuleContext(Assignment_expressionContext.class,0);
		}
		public ExpressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression; }
	}

	public final ExpressionContext expression() throws RecognitionException {
		ExpressionContext _localctx = new ExpressionContext(_ctx, getState());
		enterRule(_localctx, 104, RULE_expression);
		try {
			setState(587);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,50,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(585);
				non_assignment_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(586);
				assignment_expression();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Initialization_expressionContext extends ParserRuleContext {
		public List<TerminalNode> NAME() { return getTokens(MCSharpParser.NAME); }
		public TerminalNode NAME(int i) {
			return getToken(MCSharpParser.NAME, i);
		}
		public TerminalNode ASSIGN() { return getToken(MCSharpParser.ASSIGN, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Initialization_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_initialization_expression; }
	}

	public final Initialization_expressionContext initialization_expression() throws RecognitionException {
		Initialization_expressionContext _localctx = new Initialization_expressionContext(_ctx, getState());
		enterRule(_localctx, 106, RULE_initialization_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(589);
			match(NAME);
			setState(590);
			match(NAME);
			{
			setState(591);
			match(ASSIGN);
			setState(592);
			expression();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Non_assignment_expressionContext extends ParserRuleContext {
		public Conditional_expressionContext conditional_expression() {
			return getRuleContext(Conditional_expressionContext.class,0);
		}
		public Lambda_expressionContext lambda_expression() {
			return getRuleContext(Lambda_expressionContext.class,0);
		}
		public Non_assignment_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_non_assignment_expression; }
	}

	public final Non_assignment_expressionContext non_assignment_expression() throws RecognitionException {
		Non_assignment_expressionContext _localctx = new Non_assignment_expressionContext(_ctx, getState());
		enterRule(_localctx, 108, RULE_non_assignment_expression);
		try {
			setState(596);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,51,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(594);
				conditional_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(595);
				lambda_expression();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Lambda_expressionContext extends ParserRuleContext {
		public Method_argumentsContext method_arguments() {
			return getRuleContext(Method_argumentsContext.class,0);
		}
		public TerminalNode LAMBDA() { return getToken(MCSharpParser.LAMBDA, 0); }
		public Code_blockContext code_block() {
			return getRuleContext(Code_blockContext.class,0);
		}
		public Lambda_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_lambda_expression; }
	}

	public final Lambda_expressionContext lambda_expression() throws RecognitionException {
		Lambda_expressionContext _localctx = new Lambda_expressionContext(_ctx, getState());
		enterRule(_localctx, 110, RULE_lambda_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(598);
			method_arguments();
			setState(599);
			match(LAMBDA);
			{
			setState(600);
			code_block();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Expression_listContext extends ParserRuleContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(MCSharpParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(MCSharpParser.COMMA, i);
		}
		public Expression_listContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression_list; }
	}

	public final Expression_listContext expression_list() throws RecognitionException {
		Expression_listContext _localctx = new Expression_listContext(_ctx, getState());
		enterRule(_localctx, 112, RULE_expression_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(602);
			expression();
			setState(607);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(603);
				match(COMMA);
				setState(604);
				expression();
				}
				}
				setState(609);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Conditional_expressionContext extends ParserRuleContext {
		public Null_coalescing_expressionContext null_coalescing_expression() {
			return getRuleContext(Null_coalescing_expressionContext.class,0);
		}
		public TerminalNode CONDITION_IF() { return getToken(MCSharpParser.CONDITION_IF, 0); }
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public TerminalNode CONDITION_ELSE() { return getToken(MCSharpParser.CONDITION_ELSE, 0); }
		public Conditional_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_conditional_expression; }
	}

	public final Conditional_expressionContext conditional_expression() throws RecognitionException {
		Conditional_expressionContext _localctx = new Conditional_expressionContext(_ctx, getState());
		enterRule(_localctx, 114, RULE_conditional_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(610);
			null_coalescing_expression();
			setState(616);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,53,_ctx) ) {
			case 1:
				{
				setState(611);
				match(CONDITION_IF);
				setState(612);
				expression();
				setState(613);
				match(CONDITION_ELSE);
				setState(614);
				expression();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Null_coalescing_expressionContext extends ParserRuleContext {
		public Conditional_or_expressionContext conditional_or_expression() {
			return getRuleContext(Conditional_or_expressionContext.class,0);
		}
		public TerminalNode NULL_COALESCING() { return getToken(MCSharpParser.NULL_COALESCING, 0); }
		public Null_coalescing_expressionContext null_coalescing_expression() {
			return getRuleContext(Null_coalescing_expressionContext.class,0);
		}
		public Null_coalescing_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_null_coalescing_expression; }
	}

	public final Null_coalescing_expressionContext null_coalescing_expression() throws RecognitionException {
		Null_coalescing_expressionContext _localctx = new Null_coalescing_expressionContext(_ctx, getState());
		enterRule(_localctx, 116, RULE_null_coalescing_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(618);
			conditional_or_expression();
			setState(621);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,54,_ctx) ) {
			case 1:
				{
				setState(619);
				match(NULL_COALESCING);
				setState(620);
				null_coalescing_expression();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Conditional_or_expressionContext extends ParserRuleContext {
		public List<Conditional_and_expressionContext> conditional_and_expression() {
			return getRuleContexts(Conditional_and_expressionContext.class);
		}
		public Conditional_and_expressionContext conditional_and_expression(int i) {
			return getRuleContext(Conditional_and_expressionContext.class,i);
		}
		public List<TerminalNode> BOOLEAN_OR() { return getTokens(MCSharpParser.BOOLEAN_OR); }
		public TerminalNode BOOLEAN_OR(int i) {
			return getToken(MCSharpParser.BOOLEAN_OR, i);
		}
		public Conditional_or_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_conditional_or_expression; }
	}

	public final Conditional_or_expressionContext conditional_or_expression() throws RecognitionException {
		Conditional_or_expressionContext _localctx = new Conditional_or_expressionContext(_ctx, getState());
		enterRule(_localctx, 118, RULE_conditional_or_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(623);
			conditional_and_expression();
			setState(628);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,55,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(624);
					match(BOOLEAN_OR);
					setState(625);
					conditional_and_expression();
					}
					} 
				}
				setState(630);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,55,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Conditional_and_expressionContext extends ParserRuleContext {
		public List<Inclusive_or_expressionContext> inclusive_or_expression() {
			return getRuleContexts(Inclusive_or_expressionContext.class);
		}
		public Inclusive_or_expressionContext inclusive_or_expression(int i) {
			return getRuleContext(Inclusive_or_expressionContext.class,i);
		}
		public List<TerminalNode> BOOLEAN_AND() { return getTokens(MCSharpParser.BOOLEAN_AND); }
		public TerminalNode BOOLEAN_AND(int i) {
			return getToken(MCSharpParser.BOOLEAN_AND, i);
		}
		public Conditional_and_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_conditional_and_expression; }
	}

	public final Conditional_and_expressionContext conditional_and_expression() throws RecognitionException {
		Conditional_and_expressionContext _localctx = new Conditional_and_expressionContext(_ctx, getState());
		enterRule(_localctx, 120, RULE_conditional_and_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(631);
			inclusive_or_expression();
			setState(636);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,56,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(632);
					match(BOOLEAN_AND);
					setState(633);
					inclusive_or_expression();
					}
					} 
				}
				setState(638);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,56,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Inclusive_or_expressionContext extends ParserRuleContext {
		public List<Exclusive_or_expressionContext> exclusive_or_expression() {
			return getRuleContexts(Exclusive_or_expressionContext.class);
		}
		public Exclusive_or_expressionContext exclusive_or_expression(int i) {
			return getRuleContext(Exclusive_or_expressionContext.class,i);
		}
		public List<TerminalNode> BITWISE_OR() { return getTokens(MCSharpParser.BITWISE_OR); }
		public TerminalNode BITWISE_OR(int i) {
			return getToken(MCSharpParser.BITWISE_OR, i);
		}
		public Inclusive_or_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_inclusive_or_expression; }
	}

	public final Inclusive_or_expressionContext inclusive_or_expression() throws RecognitionException {
		Inclusive_or_expressionContext _localctx = new Inclusive_or_expressionContext(_ctx, getState());
		enterRule(_localctx, 122, RULE_inclusive_or_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(639);
			exclusive_or_expression();
			setState(644);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,57,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(640);
					match(BITWISE_OR);
					setState(641);
					exclusive_or_expression();
					}
					} 
				}
				setState(646);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,57,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Exclusive_or_expressionContext extends ParserRuleContext {
		public List<And_expressionContext> and_expression() {
			return getRuleContexts(And_expressionContext.class);
		}
		public And_expressionContext and_expression(int i) {
			return getRuleContext(And_expressionContext.class,i);
		}
		public List<TerminalNode> BITWISE_XOR() { return getTokens(MCSharpParser.BITWISE_XOR); }
		public TerminalNode BITWISE_XOR(int i) {
			return getToken(MCSharpParser.BITWISE_XOR, i);
		}
		public Exclusive_or_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_exclusive_or_expression; }
	}

	public final Exclusive_or_expressionContext exclusive_or_expression() throws RecognitionException {
		Exclusive_or_expressionContext _localctx = new Exclusive_or_expressionContext(_ctx, getState());
		enterRule(_localctx, 124, RULE_exclusive_or_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(647);
			and_expression();
			setState(652);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,58,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(648);
					match(BITWISE_XOR);
					setState(649);
					and_expression();
					}
					} 
				}
				setState(654);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,58,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class And_expressionContext extends ParserRuleContext {
		public List<Equality_expressionContext> equality_expression() {
			return getRuleContexts(Equality_expressionContext.class);
		}
		public Equality_expressionContext equality_expression(int i) {
			return getRuleContext(Equality_expressionContext.class,i);
		}
		public List<TerminalNode> BITWISE_AND() { return getTokens(MCSharpParser.BITWISE_AND); }
		public TerminalNode BITWISE_AND(int i) {
			return getToken(MCSharpParser.BITWISE_AND, i);
		}
		public And_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_and_expression; }
	}

	public final And_expressionContext and_expression() throws RecognitionException {
		And_expressionContext _localctx = new And_expressionContext(_ctx, getState());
		enterRule(_localctx, 126, RULE_and_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(655);
			equality_expression();
			setState(660);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,59,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(656);
					match(BITWISE_AND);
					setState(657);
					equality_expression();
					}
					} 
				}
				setState(662);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,59,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Equality_expressionContext extends ParserRuleContext {
		public List<Relational_expressionContext> relational_expression() {
			return getRuleContexts(Relational_expressionContext.class);
		}
		public Relational_expressionContext relational_expression(int i) {
			return getRuleContext(Relational_expressionContext.class,i);
		}
		public List<Equality_operatorContext> equality_operator() {
			return getRuleContexts(Equality_operatorContext.class);
		}
		public Equality_operatorContext equality_operator(int i) {
			return getRuleContext(Equality_operatorContext.class,i);
		}
		public Equality_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_equality_expression; }
	}

	public final Equality_expressionContext equality_expression() throws RecognitionException {
		Equality_expressionContext _localctx = new Equality_expressionContext(_ctx, getState());
		enterRule(_localctx, 128, RULE_equality_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(663);
			relational_expression();
			setState(669);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,60,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(664);
					equality_operator();
					setState(665);
					relational_expression();
					}
					} 
				}
				setState(671);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,60,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Relational_expressionContext extends ParserRuleContext {
		public Shift_expressionContext shift_expression() {
			return getRuleContext(Shift_expressionContext.class,0);
		}
		public List<Relation_or_type_checkContext> relation_or_type_check() {
			return getRuleContexts(Relation_or_type_checkContext.class);
		}
		public Relation_or_type_checkContext relation_or_type_check(int i) {
			return getRuleContext(Relation_or_type_checkContext.class,i);
		}
		public Relational_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_relational_expression; }
	}

	public final Relational_expressionContext relational_expression() throws RecognitionException {
		Relational_expressionContext _localctx = new Relational_expressionContext(_ctx, getState());
		enterRule(_localctx, 130, RULE_relational_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(672);
			shift_expression();
			setState(676);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,61,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(673);
					relation_or_type_check();
					}
					} 
				}
				setState(678);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,61,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Relation_or_type_checkContext extends ParserRuleContext {
		public Relation_operatorContext relation_operator() {
			return getRuleContext(Relation_operatorContext.class,0);
		}
		public Shift_expressionContext shift_expression() {
			return getRuleContext(Shift_expressionContext.class,0);
		}
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public TerminalNode IS() { return getToken(MCSharpParser.IS, 0); }
		public TerminalNode AS() { return getToken(MCSharpParser.AS, 0); }
		public Relation_or_type_checkContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_relation_or_type_check; }
	}

	public final Relation_or_type_checkContext relation_or_type_check() throws RecognitionException {
		Relation_or_type_checkContext _localctx = new Relation_or_type_checkContext(_ctx, getState());
		enterRule(_localctx, 132, RULE_relation_or_type_check);
		int _la;
		try {
			setState(684);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LESS_THAN:
			case GREATER_THAN:
			case LESS_THAN_EQUAL:
			case GREATER_THAN_EQUAL:
				enterOuterAlt(_localctx, 1);
				{
				setState(679);
				relation_operator();
				setState(680);
				shift_expression();
				}
				break;
			case IS:
			case AS:
				enterOuterAlt(_localctx, 2);
				{
				setState(682);
				_la = _input.LA(1);
				if ( !(_la==IS || _la==AS) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(683);
				match(NAME);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Shift_expressionContext extends ParserRuleContext {
		public List<Additive_expressionContext> additive_expression() {
			return getRuleContexts(Additive_expressionContext.class);
		}
		public Additive_expressionContext additive_expression(int i) {
			return getRuleContext(Additive_expressionContext.class,i);
		}
		public List<Shift_operatorContext> shift_operator() {
			return getRuleContexts(Shift_operatorContext.class);
		}
		public Shift_operatorContext shift_operator(int i) {
			return getRuleContext(Shift_operatorContext.class,i);
		}
		public Shift_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_shift_expression; }
	}

	public final Shift_expressionContext shift_expression() throws RecognitionException {
		Shift_expressionContext _localctx = new Shift_expressionContext(_ctx, getState());
		enterRule(_localctx, 134, RULE_shift_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(686);
			additive_expression();
			setState(692);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,63,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(687);
					shift_operator();
					setState(688);
					additive_expression();
					}
					} 
				}
				setState(694);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,63,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Additive_expressionContext extends ParserRuleContext {
		public List<Multiplicative_expressionContext> multiplicative_expression() {
			return getRuleContexts(Multiplicative_expressionContext.class);
		}
		public Multiplicative_expressionContext multiplicative_expression(int i) {
			return getRuleContext(Multiplicative_expressionContext.class,i);
		}
		public List<Additive_operatorContext> additive_operator() {
			return getRuleContexts(Additive_operatorContext.class);
		}
		public Additive_operatorContext additive_operator(int i) {
			return getRuleContext(Additive_operatorContext.class,i);
		}
		public Additive_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_additive_expression; }
	}

	public final Additive_expressionContext additive_expression() throws RecognitionException {
		Additive_expressionContext _localctx = new Additive_expressionContext(_ctx, getState());
		enterRule(_localctx, 136, RULE_additive_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(695);
			multiplicative_expression();
			setState(701);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,64,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(696);
					additive_operator();
					setState(697);
					multiplicative_expression();
					}
					} 
				}
				setState(703);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,64,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Multiplicative_expressionContext extends ParserRuleContext {
		public List<With_expressionContext> with_expression() {
			return getRuleContexts(With_expressionContext.class);
		}
		public With_expressionContext with_expression(int i) {
			return getRuleContext(With_expressionContext.class,i);
		}
		public List<Multiplicative_operatorContext> multiplicative_operator() {
			return getRuleContexts(Multiplicative_operatorContext.class);
		}
		public Multiplicative_operatorContext multiplicative_operator(int i) {
			return getRuleContext(Multiplicative_operatorContext.class,i);
		}
		public Multiplicative_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_multiplicative_expression; }
	}

	public final Multiplicative_expressionContext multiplicative_expression() throws RecognitionException {
		Multiplicative_expressionContext _localctx = new Multiplicative_expressionContext(_ctx, getState());
		enterRule(_localctx, 138, RULE_multiplicative_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(704);
			with_expression();
			setState(710);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,65,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(705);
					multiplicative_operator();
					setState(706);
					with_expression();
					}
					} 
				}
				setState(712);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,65,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class With_expressionContext extends ParserRuleContext {
		public Range_expressionContext range_expression() {
			return getRuleContext(Range_expressionContext.class,0);
		}
		public TerminalNode WITH() { return getToken(MCSharpParser.WITH, 0); }
		public Anonymous_element_initializerContext anonymous_element_initializer() {
			return getRuleContext(Anonymous_element_initializerContext.class,0);
		}
		public With_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_with_expression; }
	}

	public final With_expressionContext with_expression() throws RecognitionException {
		With_expressionContext _localctx = new With_expressionContext(_ctx, getState());
		enterRule(_localctx, 140, RULE_with_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(713);
			range_expression();
			setState(716);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==WITH) {
				{
				setState(714);
				match(WITH);
				setState(715);
				anonymous_element_initializer();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Range_expressionContext extends ParserRuleContext {
		public List<Unary_expressionContext> unary_expression() {
			return getRuleContexts(Unary_expressionContext.class);
		}
		public Unary_expressionContext unary_expression(int i) {
			return getRuleContext(Unary_expressionContext.class,i);
		}
		public Range_operatorContext range_operator() {
			return getRuleContext(Range_operatorContext.class,0);
		}
		public Range_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_range_expression; }
	}

	public final Range_expressionContext range_expression() throws RecognitionException {
		Range_expressionContext _localctx = new Range_expressionContext(_ctx, getState());
		enterRule(_localctx, 142, RULE_range_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(718);
			unary_expression();
			setState(722);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==RANGE_INCLUSIVE || _la==RANGE_EXCLUSIVE) {
				{
				setState(719);
				range_operator();
				setState(720);
				unary_expression();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Pre_step_expressionContext extends ParserRuleContext {
		public Step_operatorContext step_operator() {
			return getRuleContext(Step_operatorContext.class,0);
		}
		public Unary_expressionContext unary_expression() {
			return getRuleContext(Unary_expressionContext.class,0);
		}
		public Pre_step_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pre_step_expression; }
	}

	public final Pre_step_expressionContext pre_step_expression() throws RecognitionException {
		Pre_step_expressionContext _localctx = new Pre_step_expressionContext(_ctx, getState());
		enterRule(_localctx, 144, RULE_pre_step_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(724);
			step_operator();
			{
			setState(725);
			unary_expression();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Post_step_expressionContext extends ParserRuleContext {
		public Step_operatorContext step_operator() {
			return getRuleContext(Step_operatorContext.class,0);
		}
		public LiteralContext literal() {
			return getRuleContext(LiteralContext.class,0);
		}
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public Post_step_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_post_step_expression; }
	}

	public final Post_step_expressionContext post_step_expression() throws RecognitionException {
		Post_step_expressionContext _localctx = new Post_step_expressionContext(_ctx, getState());
		enterRule(_localctx, 146, RULE_post_step_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(729);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case STRING:
			case DECIMAL:
			case INTEGER:
				{
				setState(727);
				literal();
				}
				break;
			case T__0:
			case NAME:
				{
				setState(728);
				identifier();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(731);
			step_operator();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Unary_expressionContext extends ParserRuleContext {
		public Primary_expressionContext primary_expression() {
			return getRuleContext(Primary_expressionContext.class,0);
		}
		public Unary_expressionContext unary_expression() {
			return getRuleContext(Unary_expressionContext.class,0);
		}
		public TerminalNode PLUS() { return getToken(MCSharpParser.PLUS, 0); }
		public TerminalNode MINUS() { return getToken(MCSharpParser.MINUS, 0); }
		public TerminalNode BOOLEAN_NOT() { return getToken(MCSharpParser.BOOLEAN_NOT, 0); }
		public TerminalNode BITWISE_NOT() { return getToken(MCSharpParser.BITWISE_NOT, 0); }
		public Step_operatorContext step_operator() {
			return getRuleContext(Step_operatorContext.class,0);
		}
		public Cast_expressionContext cast_expression() {
			return getRuleContext(Cast_expressionContext.class,0);
		}
		public Pointer_indirection_expressionContext pointer_indirection_expression() {
			return getRuleContext(Pointer_indirection_expressionContext.class,0);
		}
		public Addressof_expressionContext addressof_expression() {
			return getRuleContext(Addressof_expressionContext.class,0);
		}
		public Unary_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_unary_expression; }
	}

	public final Unary_expressionContext unary_expression() throws RecognitionException {
		Unary_expressionContext _localctx = new Unary_expressionContext(_ctx, getState());
		enterRule(_localctx, 148, RULE_unary_expression);
		int _la;
		try {
			setState(742);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,69,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(733);
				primary_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(734);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << PLUS) | (1L << MINUS) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(735);
				unary_expression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(736);
				step_operator();
				setState(737);
				unary_expression();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(739);
				cast_expression();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(740);
				pointer_indirection_expression();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(741);
				addressof_expression();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Cast_expressionContext extends ParserRuleContext {
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public Unary_expressionContext unary_expression() {
			return getRuleContext(Unary_expressionContext.class,0);
		}
		public Cast_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_cast_expression; }
	}

	public final Cast_expressionContext cast_expression() throws RecognitionException {
		Cast_expressionContext _localctx = new Cast_expressionContext(_ctx, getState());
		enterRule(_localctx, 150, RULE_cast_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(744);
			match(OP);
			setState(745);
			match(NAME);
			setState(746);
			match(CP);
			setState(747);
			unary_expression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Pointer_indirection_expressionContext extends ParserRuleContext {
		public TerminalNode MULTIPLY() { return getToken(MCSharpParser.MULTIPLY, 0); }
		public Unary_expressionContext unary_expression() {
			return getRuleContext(Unary_expressionContext.class,0);
		}
		public Pointer_indirection_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_pointer_indirection_expression; }
	}

	public final Pointer_indirection_expressionContext pointer_indirection_expression() throws RecognitionException {
		Pointer_indirection_expressionContext _localctx = new Pointer_indirection_expressionContext(_ctx, getState());
		enterRule(_localctx, 152, RULE_pointer_indirection_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(749);
			match(MULTIPLY);
			setState(750);
			unary_expression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Addressof_expressionContext extends ParserRuleContext {
		public TerminalNode BITWISE_AND() { return getToken(MCSharpParser.BITWISE_AND, 0); }
		public Unary_expressionContext unary_expression() {
			return getRuleContext(Unary_expressionContext.class,0);
		}
		public Addressof_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_addressof_expression; }
	}

	public final Addressof_expressionContext addressof_expression() throws RecognitionException {
		Addressof_expressionContext _localctx = new Addressof_expressionContext(_ctx, getState());
		enterRule(_localctx, 154, RULE_addressof_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(752);
			match(BITWISE_AND);
			setState(753);
			unary_expression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Assignment_expressionContext extends ParserRuleContext {
		public Unary_expressionContext unary_expression() {
			return getRuleContext(Unary_expressionContext.class,0);
		}
		public Assignment_operatorContext assignment_operator() {
			return getRuleContext(Assignment_operatorContext.class,0);
		}
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Assignment_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_assignment_expression; }
	}

	public final Assignment_expressionContext assignment_expression() throws RecognitionException {
		Assignment_expressionContext _localctx = new Assignment_expressionContext(_ctx, getState());
		enterRule(_localctx, 156, RULE_assignment_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(755);
			unary_expression();
			setState(756);
			assignment_operator();
			setState(757);
			expression();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Primary_expressionContext extends ParserRuleContext {
		public Array_creation_expressionContext array_creation_expression() {
			return getRuleContext(Array_creation_expressionContext.class,0);
		}
		public Primary_no_array_creation_expressionContext primary_no_array_creation_expression() {
			return getRuleContext(Primary_no_array_creation_expressionContext.class,0);
		}
		public Primary_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_primary_expression; }
	}

	public final Primary_expressionContext primary_expression() throws RecognitionException {
		Primary_expressionContext _localctx = new Primary_expressionContext(_ctx, getState());
		enterRule(_localctx, 158, RULE_primary_expression);
		try {
			setState(761);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,70,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(759);
				array_creation_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(760);
				primary_no_array_creation_expression();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Array_creation_expressionContext extends ParserRuleContext {
		public TerminalNode NEW() { return getToken(MCSharpParser.NEW, 0); }
		public Indexer_argumentsContext indexer_arguments() {
			return getRuleContext(Indexer_argumentsContext.class,0);
		}
		public Array_rank_specifierContext array_rank_specifier() {
			return getRuleContext(Array_rank_specifierContext.class,0);
		}
		public Array_initializerContext array_initializer() {
			return getRuleContext(Array_initializerContext.class,0);
		}
		public Array_creation_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_array_creation_expression; }
	}

	public final Array_creation_expressionContext array_creation_expression() throws RecognitionException {
		Array_creation_expressionContext _localctx = new Array_creation_expressionContext(_ctx, getState());
		enterRule(_localctx, 160, RULE_array_creation_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(763);
			match(NEW);
			setState(764);
			indexer_arguments();
			setState(766);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==OB) {
				{
				setState(765);
				array_rank_specifier();
				}
			}

			setState(769);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==OC) {
				{
				setState(768);
				array_initializer();
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Array_rank_specifierContext extends ParserRuleContext {
		public TerminalNode OB() { return getToken(MCSharpParser.OB, 0); }
		public TerminalNode CB() { return getToken(MCSharpParser.CB, 0); }
		public List<TerminalNode> COMMA() { return getTokens(MCSharpParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(MCSharpParser.COMMA, i);
		}
		public Array_rank_specifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_array_rank_specifier; }
	}

	public final Array_rank_specifierContext array_rank_specifier() throws RecognitionException {
		Array_rank_specifierContext _localctx = new Array_rank_specifierContext(_ctx, getState());
		enterRule(_localctx, 162, RULE_array_rank_specifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(771);
			match(OB);
			setState(775);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(772);
				match(COMMA);
				}
				}
				setState(777);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(778);
			match(CB);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Array_initializerContext extends ParserRuleContext {
		public TerminalNode OC() { return getToken(MCSharpParser.OC, 0); }
		public TerminalNode CC() { return getToken(MCSharpParser.CC, 0); }
		public List<Variable_initializerContext> variable_initializer() {
			return getRuleContexts(Variable_initializerContext.class);
		}
		public Variable_initializerContext variable_initializer(int i) {
			return getRuleContext(Variable_initializerContext.class,i);
		}
		public List<TerminalNode> COMMA() { return getTokens(MCSharpParser.COMMA); }
		public TerminalNode COMMA(int i) {
			return getToken(MCSharpParser.COMMA, i);
		}
		public Array_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_array_initializer; }
	}

	public final Array_initializerContext array_initializer() throws RecognitionException {
		Array_initializerContext _localctx = new Array_initializerContext(_ctx, getState());
		enterRule(_localctx, 164, RULE_array_initializer);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(780);
			match(OC);
			setState(792);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << OP) | (1L << OC) | (1L << PLUS) | (1L << MINUS) | (1L << MULTIPLY) | (1L << INCREMENT) | (1L << DECREMENT) | (1L << BITWISE_AND) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				setState(781);
				variable_initializer();
				setState(786);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,74,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(782);
						match(COMMA);
						setState(783);
						variable_initializer();
						}
						} 
					}
					setState(788);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,74,_ctx);
				}
				setState(790);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==COMMA) {
					{
					setState(789);
					match(COMMA);
					}
				}

				}
			}

			setState(794);
			match(CC);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Variable_initializerContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Array_initializerContext array_initializer() {
			return getRuleContext(Array_initializerContext.class,0);
		}
		public Variable_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_variable_initializer; }
	}

	public final Variable_initializerContext variable_initializer() throws RecognitionException {
		Variable_initializerContext _localctx = new Variable_initializerContext(_ctx, getState());
		enterRule(_localctx, 166, RULE_variable_initializer);
		try {
			setState(798);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__0:
			case OP:
			case PLUS:
			case MINUS:
			case MULTIPLY:
			case INCREMENT:
			case DECREMENT:
			case BITWISE_AND:
			case BITWISE_NOT:
			case BOOLEAN_NOT:
			case NEW:
			case TYPEOF:
			case CHECKED:
			case UNCHECKED:
			case DEFAULT:
			case SIZEOF:
			case STRING:
			case DECIMAL:
			case INTEGER:
			case NAME:
				enterOuterAlt(_localctx, 1);
				{
				setState(796);
				expression();
				}
				break;
			case OC:
				enterOuterAlt(_localctx, 2);
				{
				setState(797);
				array_initializer();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Primary_no_array_creation_expressionContext extends ParserRuleContext {
		public LiteralContext literal() {
			return getRuleContext(LiteralContext.class,0);
		}
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public Member_accessContext member_access() {
			return getRuleContext(Member_accessContext.class,0);
		}
		public Post_step_expressionContext post_step_expression() {
			return getRuleContext(Post_step_expressionContext.class,0);
		}
		public Keyword_expressionContext keyword_expression() {
			return getRuleContext(Keyword_expressionContext.class,0);
		}
		public Primary_no_array_creation_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_primary_no_array_creation_expression; }
	}

	public final Primary_no_array_creation_expressionContext primary_no_array_creation_expression() throws RecognitionException {
		Primary_no_array_creation_expressionContext _localctx = new Primary_no_array_creation_expressionContext(_ctx, getState());
		enterRule(_localctx, 168, RULE_primary_no_array_creation_expression);
		try {
			setState(809);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,78,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(800);
				literal();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(801);
				identifier();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(802);
				match(OP);
				setState(803);
				expression();
				setState(804);
				match(CP);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(806);
				member_access();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(807);
				post_step_expression();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(808);
				keyword_expression();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Member_accessContext extends ParserRuleContext {
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public Primary_expressionContext primary_expression() {
			return getRuleContext(Primary_expressionContext.class,0);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public TerminalNode DOT() { return getToken(MCSharpParser.DOT, 0); }
		public Generic_argumentsContext generic_arguments() {
			return getRuleContext(Generic_argumentsContext.class,0);
		}
		public Method_argumentsContext method_arguments() {
			return getRuleContext(Method_argumentsContext.class,0);
		}
		public Indexer_argumentsContext indexer_arguments() {
			return getRuleContext(Indexer_argumentsContext.class,0);
		}
		public Member_accessContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_member_access; }
	}

	public final Member_accessContext member_access() throws RecognitionException {
		Member_accessContext _localctx = new Member_accessContext(_ctx, getState());
		enterRule(_localctx, 170, RULE_member_access);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(816);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==OP) {
				{
				setState(811);
				match(OP);
				setState(812);
				primary_expression();
				setState(813);
				match(CP);
				setState(814);
				match(DOT);
				}
			}

			setState(818);
			identifier();
			setState(820);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,80,_ctx) ) {
			case 1:
				{
				setState(819);
				generic_arguments();
				}
				break;
			}
			setState(824);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case OP:
				{
				setState(822);
				method_arguments();
				}
				break;
			case OB:
				{
				setState(823);
				indexer_arguments();
				}
				break;
			case EOF:
			case END:
			case COMMA:
			case CP:
			case CB:
			case CC:
			case PLUS:
			case MINUS:
			case MULTIPLY:
			case DIVIDE:
			case MODULUS:
			case BITWISE_AND:
			case BITWISE_OR:
			case BITWISE_XOR:
			case BOOLEAN_AND:
			case BOOLEAN_OR:
			case SHIFT_LEFT:
			case SHIFT_RIGHT:
			case EQUIVALENT:
			case NOT_EQUIVALENT:
			case LESS_THAN:
			case GREATER_THAN:
			case LESS_THAN_EQUAL:
			case GREATER_THAN_EQUAL:
			case ASSIGN:
			case ASSIGN_PLUS:
			case ASSIGN_MINUS:
			case ASSIGN_MULTIPLY:
			case ASSIGN_DIVIDE:
			case ASSIGN_MODULUS:
			case ASSIGN_ACCESS:
			case ASSIGN_AND:
			case ASSIGN_OR:
			case ASSIGN_XOR:
			case ASSIGN_LEFT:
			case ASSIGN_RIGHT:
			case CONDITION_IF:
			case CONDITION_ELSE:
			case RANGE_INCLUSIVE:
			case RANGE_EXCLUSIVE:
			case IS:
			case AS:
			case NULL_COALESCING:
			case WITH:
			case PUBLIC:
			case PRIVATE:
			case PROTECTED:
			case STATIC:
			case ABSTRACT:
			case VIRTUAL:
			case NAME:
				break;
			default:
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Keyword_expressionContext extends ParserRuleContext {
		public New_keyword_expressionContext new_keyword_expression() {
			return getRuleContext(New_keyword_expressionContext.class,0);
		}
		public Typeof_keyword_expressionContext typeof_keyword_expression() {
			return getRuleContext(Typeof_keyword_expressionContext.class,0);
		}
		public Checked_expressionContext checked_expression() {
			return getRuleContext(Checked_expressionContext.class,0);
		}
		public Unchecked_expressionContext unchecked_expression() {
			return getRuleContext(Unchecked_expressionContext.class,0);
		}
		public Default_keyword_expressionContext default_keyword_expression() {
			return getRuleContext(Default_keyword_expressionContext.class,0);
		}
		public Sizeof_keyword_expressionContext sizeof_keyword_expression() {
			return getRuleContext(Sizeof_keyword_expressionContext.class,0);
		}
		public Keyword_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_keyword_expression; }
	}

	public final Keyword_expressionContext keyword_expression() throws RecognitionException {
		Keyword_expressionContext _localctx = new Keyword_expressionContext(_ctx, getState());
		enterRule(_localctx, 172, RULE_keyword_expression);
		try {
			setState(832);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case NEW:
				enterOuterAlt(_localctx, 1);
				{
				setState(826);
				new_keyword_expression();
				}
				break;
			case TYPEOF:
				enterOuterAlt(_localctx, 2);
				{
				setState(827);
				typeof_keyword_expression();
				}
				break;
			case CHECKED:
				enterOuterAlt(_localctx, 3);
				{
				setState(828);
				checked_expression();
				}
				break;
			case UNCHECKED:
				enterOuterAlt(_localctx, 4);
				{
				setState(829);
				unchecked_expression();
				}
				break;
			case DEFAULT:
				enterOuterAlt(_localctx, 5);
				{
				setState(830);
				default_keyword_expression();
				}
				break;
			case SIZEOF:
				enterOuterAlt(_localctx, 6);
				{
				setState(831);
				sizeof_keyword_expression();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Object_or_collection_initializerContext extends ParserRuleContext {
		public Object_initializerContext object_initializer() {
			return getRuleContext(Object_initializerContext.class,0);
		}
		public Collection_initializerContext collection_initializer() {
			return getRuleContext(Collection_initializerContext.class,0);
		}
		public Object_or_collection_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_object_or_collection_initializer; }
	}

	public final Object_or_collection_initializerContext object_or_collection_initializer() throws RecognitionException {
		Object_or_collection_initializerContext _localctx = new Object_or_collection_initializerContext(_ctx, getState());
		enterRule(_localctx, 174, RULE_object_or_collection_initializer);
		try {
			setState(836);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,83,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(834);
				object_initializer();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(835);
				collection_initializer();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class New_keyword_expressionContext extends ParserRuleContext {
		public TerminalNode NEW() { return getToken(MCSharpParser.NEW, 0); }
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public Method_argumentsContext method_arguments() {
			return getRuleContext(Method_argumentsContext.class,0);
		}
		public Object_or_collection_initializerContext object_or_collection_initializer() {
			return getRuleContext(Object_or_collection_initializerContext.class,0);
		}
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public Anonymous_object_initializerContext anonymous_object_initializer() {
			return getRuleContext(Anonymous_object_initializerContext.class,0);
		}
		public New_keyword_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_new_keyword_expression; }
	}

	public final New_keyword_expressionContext new_keyword_expression() throws RecognitionException {
		New_keyword_expressionContext _localctx = new New_keyword_expressionContext(_ctx, getState());
		enterRule(_localctx, 176, RULE_new_keyword_expression);
		int _la;
		try {
			setState(855);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,86,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(838);
				match(NEW);
				setState(839);
				match(NAME);
				setState(845);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case OP:
					{
					{
					setState(840);
					method_arguments();
					setState(842);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (_la==OC) {
						{
						setState(841);
						object_or_collection_initializer();
						}
					}

					}
					}
					break;
				case OC:
					{
					{
					setState(844);
					object_or_collection_initializer();
					}
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(847);
				match(NEW);
				setState(848);
				match(NAME);
				{
				setState(849);
				match(OP);
				setState(850);
				expression();
				setState(851);
				match(CP);
				}
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(853);
				match(NEW);
				setState(854);
				anonymous_object_initializer();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Typeof_keyword_expressionContext extends ParserRuleContext {
		public TerminalNode TYPEOF() { return getToken(MCSharpParser.TYPEOF, 0); }
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public Typeof_keyword_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_typeof_keyword_expression; }
	}

	public final Typeof_keyword_expressionContext typeof_keyword_expression() throws RecognitionException {
		Typeof_keyword_expressionContext _localctx = new Typeof_keyword_expressionContext(_ctx, getState());
		enterRule(_localctx, 178, RULE_typeof_keyword_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(857);
			match(TYPEOF);
			setState(858);
			match(OP);
			{
			setState(859);
			match(NAME);
			}
			setState(860);
			match(CP);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Checked_expressionContext extends ParserRuleContext {
		public TerminalNode CHECKED() { return getToken(MCSharpParser.CHECKED, 0); }
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public Checked_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_checked_expression; }
	}

	public final Checked_expressionContext checked_expression() throws RecognitionException {
		Checked_expressionContext _localctx = new Checked_expressionContext(_ctx, getState());
		enterRule(_localctx, 180, RULE_checked_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(862);
			match(CHECKED);
			setState(863);
			match(OP);
			setState(864);
			expression();
			setState(865);
			match(CP);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Unchecked_expressionContext extends ParserRuleContext {
		public TerminalNode UNCHECKED() { return getToken(MCSharpParser.UNCHECKED, 0); }
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public Unchecked_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_unchecked_expression; }
	}

	public final Unchecked_expressionContext unchecked_expression() throws RecognitionException {
		Unchecked_expressionContext _localctx = new Unchecked_expressionContext(_ctx, getState());
		enterRule(_localctx, 182, RULE_unchecked_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(867);
			match(UNCHECKED);
			setState(868);
			match(OP);
			setState(869);
			expression();
			setState(870);
			match(CP);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Default_keyword_expressionContext extends ParserRuleContext {
		public TerminalNode DEFAULT() { return getToken(MCSharpParser.DEFAULT, 0); }
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public Default_keyword_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_default_keyword_expression; }
	}

	public final Default_keyword_expressionContext default_keyword_expression() throws RecognitionException {
		Default_keyword_expressionContext _localctx = new Default_keyword_expressionContext(_ctx, getState());
		enterRule(_localctx, 184, RULE_default_keyword_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(872);
			match(DEFAULT);
			setState(876);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==OP) {
				{
				setState(873);
				match(OP);
				setState(874);
				match(NAME);
				setState(875);
				match(CP);
				}
			}

			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Delegate_keyword_expressionContext extends ParserRuleContext {
		public TerminalNode DELEGATE() { return getToken(MCSharpParser.DELEGATE, 0); }
		public Method_parametersContext method_parameters() {
			return getRuleContext(Method_parametersContext.class,0);
		}
		public Code_blockContext code_block() {
			return getRuleContext(Code_blockContext.class,0);
		}
		public Delegate_keyword_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_delegate_keyword_expression; }
	}

	public final Delegate_keyword_expressionContext delegate_keyword_expression() throws RecognitionException {
		Delegate_keyword_expressionContext _localctx = new Delegate_keyword_expressionContext(_ctx, getState());
		enterRule(_localctx, 186, RULE_delegate_keyword_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(878);
			match(DELEGATE);
			setState(879);
			method_parameters();
			setState(880);
			code_block();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Sizeof_keyword_expressionContext extends ParserRuleContext {
		public TerminalNode SIZEOF() { return getToken(MCSharpParser.SIZEOF, 0); }
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public Sizeof_keyword_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_sizeof_keyword_expression; }
	}

	public final Sizeof_keyword_expressionContext sizeof_keyword_expression() throws RecognitionException {
		Sizeof_keyword_expressionContext _localctx = new Sizeof_keyword_expressionContext(_ctx, getState());
		enterRule(_localctx, 188, RULE_sizeof_keyword_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(882);
			match(SIZEOF);
			setState(883);
			match(OP);
			setState(884);
			match(NAME);
			setState(885);
			match(CP);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3]\u037a\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\tC\4D\tD\4E\tE\4F\tF\4G\tG\4H\tH\4I"+
		"\tI\4J\tJ\4K\tK\4L\tL\4M\tM\4N\tN\4O\tO\4P\tP\4Q\tQ\4R\tR\4S\tS\4T\tT"+
		"\4U\tU\4V\tV\4W\tW\4X\tX\4Y\tY\4Z\tZ\4[\t[\4\\\t\\\4]\t]\4^\t^\4_\t_\4"+
		"`\t`\3\2\7\2\u00c2\n\2\f\2\16\2\u00c5\13\2\3\2\3\2\3\3\3\3\3\4\3\4\3\4"+
		"\7\4\u00ce\n\4\f\4\16\4\u00d1\13\4\3\5\3\5\3\5\3\5\3\6\5\6\u00d8\n\6\3"+
		"\6\3\6\3\6\3\7\3\7\3\7\7\7\u00e0\n\7\f\7\16\7\u00e3\13\7\3\b\3\b\5\b\u00e7"+
		"\n\b\3\b\3\b\3\t\3\t\5\t\u00ed\n\t\3\t\3\t\3\n\3\n\3\n\3\n\3\n\5\n\u00f6"+
		"\n\n\3\13\3\13\3\13\7\13\u00fb\n\13\f\13\16\13\u00fe\13\13\3\f\3\f\5\f"+
		"\u0102\n\f\3\f\3\f\3\r\3\r\5\r\u0108\n\r\3\r\3\r\3\16\3\16\5\16\u010e"+
		"\n\16\3\16\3\16\3\17\3\17\5\17\u0114\n\17\3\17\3\17\3\17\5\17\u0119\n"+
		"\17\3\20\3\20\3\20\3\20\5\20\u011f\n\20\3\20\5\20\u0122\n\20\5\20\u0124"+
		"\n\20\3\20\3\20\3\21\3\21\3\21\3\21\3\21\5\21\u012d\n\21\3\22\3\22\3\22"+
		"\3\22\5\22\u0133\n\22\3\22\5\22\u0136\n\22\5\22\u0138\n\22\3\22\3\22\3"+
		"\23\3\23\5\23\u013e\n\23\3\23\3\23\3\23\3\23\3\23\5\23\u0145\n\23\3\24"+
		"\3\24\3\24\3\24\5\24\u014b\n\24\3\24\5\24\u014e\n\24\5\24\u0150\n\24\3"+
		"\24\3\24\3\25\3\25\3\26\3\26\3\27\3\27\3\30\7\30\u015b\n\30\f\30\16\30"+
		"\u015e\13\30\3\30\3\30\3\30\3\30\7\30\u0164\n\30\f\30\16\30\u0167\13\30"+
		"\3\30\3\30\3\31\7\31\u016c\n\31\f\31\16\31\u016f\13\31\3\31\3\31\3\31"+
		"\3\31\3\31\5\31\u0176\n\31\3\32\3\32\5\32\u017a\n\32\3\32\3\32\3\33\3"+
		"\33\3\33\3\33\7\33\u0182\n\33\f\33\16\33\u0185\13\33\3\33\3\33\3\33\7"+
		"\33\u018a\n\33\f\33\16\33\u018d\13\33\3\33\5\33\u0190\n\33\3\33\7\33\u0193"+
		"\n\33\f\33\16\33\u0196\13\33\3\33\3\33\3\33\7\33\u019b\n\33\f\33\16\33"+
		"\u019e\13\33\3\33\5\33\u01a1\n\33\3\33\3\33\5\33\u01a5\n\33\3\34\3\34"+
		"\3\34\3\34\3\34\3\34\3\34\3\34\3\34\5\34\u01b0\n\34\3\35\3\35\3\35\3\35"+
		"\3\35\3\35\3\35\3\35\3\35\5\35\u01bb\n\35\3\36\5\36\u01be\n\36\3\36\3"+
		"\36\3\36\3\36\3\36\3\36\5\36\u01c6\n\36\3\37\3\37\3 \5 \u01cb\n \3 \3"+
		" \3 \7 \u01d0\n \f \16 \u01d3\13 \3 \5 \u01d6\n \3!\3!\3!\3!\3!\3!\3!"+
		"\3!\5!\u01e0\n!\3\"\3\"\7\"\u01e4\n\"\f\"\16\"\u01e7\13\"\3\"\3\"\3#\3"+
		"#\3$\3$\3%\3%\3&\3&\3\'\3\'\3(\3(\3)\3)\3*\3*\3+\3+\3,\3,\3-\3-\3-\3-"+
		"\3-\3-\3-\3-\5-\u0207\n-\3.\3.\3.\3.\3.\3.\3.\5.\u0210\n.\3/\3/\3/\3/"+
		"\3/\3/\3/\3/\3/\3/\3\60\3\60\3\60\3\60\3\60\3\60\3\60\3\60\3\60\3\61\3"+
		"\61\3\61\3\61\3\61\3\61\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\63\3"+
		"\63\3\63\3\63\3\64\3\64\3\64\3\64\3\65\3\65\3\65\3\65\3\65\3\65\3\65\3"+
		"\65\7\65\u0243\n\65\f\65\16\65\u0246\13\65\3\65\3\65\5\65\u024a\n\65\3"+
		"\66\3\66\5\66\u024e\n\66\3\67\3\67\3\67\3\67\3\67\38\38\58\u0257\n8\3"+
		"9\39\39\39\3:\3:\3:\7:\u0260\n:\f:\16:\u0263\13:\3;\3;\3;\3;\3;\3;\5;"+
		"\u026b\n;\3<\3<\3<\5<\u0270\n<\3=\3=\3=\7=\u0275\n=\f=\16=\u0278\13=\3"+
		">\3>\3>\7>\u027d\n>\f>\16>\u0280\13>\3?\3?\3?\7?\u0285\n?\f?\16?\u0288"+
		"\13?\3@\3@\3@\7@\u028d\n@\f@\16@\u0290\13@\3A\3A\3A\7A\u0295\nA\fA\16"+
		"A\u0298\13A\3B\3B\3B\3B\7B\u029e\nB\fB\16B\u02a1\13B\3C\3C\7C\u02a5\n"+
		"C\fC\16C\u02a8\13C\3D\3D\3D\3D\3D\5D\u02af\nD\3E\3E\3E\3E\7E\u02b5\nE"+
		"\fE\16E\u02b8\13E\3F\3F\3F\3F\7F\u02be\nF\fF\16F\u02c1\13F\3G\3G\3G\3"+
		"G\7G\u02c7\nG\fG\16G\u02ca\13G\3H\3H\3H\5H\u02cf\nH\3I\3I\3I\3I\5I\u02d5"+
		"\nI\3J\3J\3J\3K\3K\5K\u02dc\nK\3K\3K\3L\3L\3L\3L\3L\3L\3L\3L\3L\5L\u02e9"+
		"\nL\3M\3M\3M\3M\3M\3N\3N\3N\3O\3O\3O\3P\3P\3P\3P\3Q\3Q\5Q\u02fc\nQ\3R"+
		"\3R\3R\5R\u0301\nR\3R\5R\u0304\nR\3S\3S\7S\u0308\nS\fS\16S\u030b\13S\3"+
		"S\3S\3T\3T\3T\3T\7T\u0313\nT\fT\16T\u0316\13T\3T\5T\u0319\nT\5T\u031b"+
		"\nT\3T\3T\3U\3U\5U\u0321\nU\3V\3V\3V\3V\3V\3V\3V\3V\3V\5V\u032c\nV\3W"+
		"\3W\3W\3W\3W\5W\u0333\nW\3W\3W\5W\u0337\nW\3W\3W\5W\u033b\nW\3X\3X\3X"+
		"\3X\3X\3X\5X\u0343\nX\3Y\3Y\5Y\u0347\nY\3Z\3Z\3Z\3Z\5Z\u034d\nZ\3Z\5Z"+
		"\u0350\nZ\3Z\3Z\3Z\3Z\3Z\3Z\3Z\3Z\5Z\u035a\nZ\3[\3[\3[\3[\3[\3\\\3\\\3"+
		"\\\3\\\3\\\3]\3]\3]\3]\3]\3^\3^\3^\3^\5^\u036f\n^\3_\3_\3_\3_\3`\3`\3"+
		"`\3`\3`\3`\2\2a\2\4\6\b\n\f\16\20\22\24\26\30\32\34\36 \"$&(*,.\60\62"+
		"\64\668:<>@BDFHJLNPRTVXZ\\^`bdfhjlnprtvxz|~\u0080\u0082\u0084\u0086\u0088"+
		"\u008a\u008c\u008e\u0090\u0092\u0094\u0096\u0098\u009a\u009c\u009e\u00a0"+
		"\u00a2\u00a4\u00a6\u00a8\u00aa\u00ac\u00ae\u00b0\u00b2\u00b4\u00b6\u00b8"+
		"\u00ba\u00bc\u00be\2\22\3\2NS\4\2\65\66UU\3\2VW\3\2XZ\3\2\f\r\3\2\16\20"+
		"\3\2\21\22\3\2\23\25\3\2\27\30\3\2\32\33\3\2\34\35\3\2\36!\3\2#.\3\2\61"+
		"\62\3\2\63\64\5\2\f\r\26\26\31\31\2\u038d\2\u00c3\3\2\2\2\4\u00c8\3\2"+
		"\2\2\6\u00ca\3\2\2\2\b\u00d2\3\2\2\2\n\u00d7\3\2\2\2\f\u00dc\3\2\2\2\16"+
		"\u00e4\3\2\2\2\20\u00ea\3\2\2\2\22\u00f5\3\2\2\2\24\u00f7\3\2\2\2\26\u00ff"+
		"\3\2\2\2\30\u0105\3\2\2\2\32\u010b\3\2\2\2\34\u0111\3\2\2\2\36\u011a\3"+
		"\2\2\2 \u012c\3\2\2\2\"\u012e\3\2\2\2$\u0144\3\2\2\2&\u0146\3\2\2\2(\u0153"+
		"\3\2\2\2*\u0155\3\2\2\2,\u0157\3\2\2\2.\u015c\3\2\2\2\60\u016d\3\2\2\2"+
		"\62\u0179\3\2\2\2\64\u01a4\3\2\2\2\66\u01af\3\2\2\28\u01ba\3\2\2\2:\u01bd"+
		"\3\2\2\2<\u01c7\3\2\2\2>\u01ca\3\2\2\2@\u01df\3\2\2\2B\u01e1\3\2\2\2D"+
		"\u01ea\3\2\2\2F\u01ec\3\2\2\2H\u01ee\3\2\2\2J\u01f0\3\2\2\2L\u01f2\3\2"+
		"\2\2N\u01f4\3\2\2\2P\u01f6\3\2\2\2R\u01f8\3\2\2\2T\u01fa\3\2\2\2V\u01fc"+
		"\3\2\2\2X\u0206\3\2\2\2Z\u0208\3\2\2\2\\\u0211\3\2\2\2^\u021b\3\2\2\2"+
		"`\u0224\3\2\2\2b\u022a\3\2\2\2d\u0232\3\2\2\2f\u0236\3\2\2\2h\u023a\3"+
		"\2\2\2j\u024d\3\2\2\2l\u024f\3\2\2\2n\u0256\3\2\2\2p\u0258\3\2\2\2r\u025c"+
		"\3\2\2\2t\u0264\3\2\2\2v\u026c\3\2\2\2x\u0271\3\2\2\2z\u0279\3\2\2\2|"+
		"\u0281\3\2\2\2~\u0289\3\2\2\2\u0080\u0291\3\2\2\2\u0082\u0299\3\2\2\2"+
		"\u0084\u02a2\3\2\2\2\u0086\u02ae\3\2\2\2\u0088\u02b0\3\2\2\2\u008a\u02b9"+
		"\3\2\2\2\u008c\u02c2\3\2\2\2\u008e\u02cb\3\2\2\2\u0090\u02d0\3\2\2\2\u0092"+
		"\u02d6\3\2\2\2\u0094\u02db\3\2\2\2\u0096\u02e8\3\2\2\2\u0098\u02ea\3\2"+
		"\2\2\u009a\u02ef\3\2\2\2\u009c\u02f2\3\2\2\2\u009e\u02f5\3\2\2\2\u00a0"+
		"\u02fb\3\2\2\2\u00a2\u02fd\3\2\2\2\u00a4\u0305\3\2\2\2\u00a6\u030e\3\2"+
		"\2\2\u00a8\u0320\3\2\2\2\u00aa\u032b\3\2\2\2\u00ac\u0332\3\2\2\2\u00ae"+
		"\u0342\3\2\2\2\u00b0\u0346\3\2\2\2\u00b2\u0359\3\2\2\2\u00b4\u035b\3\2"+
		"\2\2\u00b6\u0360\3\2\2\2\u00b8\u0365\3\2\2\2\u00ba\u036a\3\2\2\2\u00bc"+
		"\u0370\3\2\2\2\u00be\u0374\3\2\2\2\u00c0\u00c2\5.\30\2\u00c1\u00c0\3\2"+
		"\2\2\u00c2\u00c5\3\2\2\2\u00c3\u00c1\3\2\2\2\u00c3\u00c4\3\2\2\2\u00c4"+
		"\u00c6\3\2\2\2\u00c5\u00c3\3\2\2\2\u00c6\u00c7\7\2\2\3\u00c7\3\3\2\2\2"+
		"\u00c8\u00c9\7[\2\2\u00c9\5\3\2\2\2\u00ca\u00cf\5\4\3\2\u00cb\u00cc\7"+
		"\5\2\2\u00cc\u00ce\5\4\3\2\u00cd\u00cb\3\2\2\2\u00ce\u00d1\3\2\2\2\u00cf"+
		"\u00cd\3\2\2\2\u00cf\u00d0\3\2\2\2\u00d0\7\3\2\2\2\u00d1\u00cf\3\2\2\2"+
		"\u00d2\u00d3\7\36\2\2\u00d3\u00d4\5\6\4\2\u00d4\u00d5\7\37\2\2\u00d5\t"+
		"\3\2\2\2\u00d6\u00d8\5*\26\2\u00d7\u00d6\3\2\2\2\u00d7\u00d8\3\2\2\2\u00d8"+
		"\u00d9\3\2\2\2\u00d9\u00da\7[\2\2\u00da\u00db\7[\2\2\u00db\13\3\2\2\2"+
		"\u00dc\u00e1\5\n\6\2\u00dd\u00de\7\5\2\2\u00de\u00e0\5\n\6\2\u00df\u00dd"+
		"\3\2\2\2\u00e0\u00e3\3\2\2\2\u00e1\u00df\3\2\2\2\u00e1\u00e2\3\2\2\2\u00e2"+
		"\r\3\2\2\2\u00e3\u00e1\3\2\2\2\u00e4\u00e6\7\6\2\2\u00e5\u00e7\5\f\7\2"+
		"\u00e6\u00e5\3\2\2\2\u00e6\u00e7\3\2\2\2\u00e7\u00e8\3\2\2\2\u00e8\u00e9"+
		"\7\7\2\2\u00e9\17\3\2\2\2\u00ea\u00ec\7\b\2\2\u00eb\u00ed\5\f\7\2\u00ec"+
		"\u00eb\3\2\2\2\u00ec\u00ed\3\2\2\2\u00ed\u00ee\3\2\2\2\u00ee\u00ef\7\t"+
		"\2\2\u00ef\21\3\2\2\2\u00f0\u00f6\5j\66\2\u00f1\u00f2\5*\26\2\u00f2\u00f3"+
		"\7[\2\2\u00f3\u00f4\7[\2\2\u00f4\u00f6\3\2\2\2\u00f5\u00f0\3\2\2\2\u00f5"+
		"\u00f1\3\2\2\2\u00f6\23\3\2\2\2\u00f7\u00fc\5\22\n\2\u00f8\u00f9\7\5\2"+
		"\2\u00f9\u00fb\5\22\n\2\u00fa\u00f8\3\2\2\2\u00fb\u00fe\3\2\2\2\u00fc"+
		"\u00fa\3\2\2\2\u00fc\u00fd\3\2\2\2\u00fd\25\3\2\2\2\u00fe\u00fc\3\2\2"+
		"\2\u00ff\u0101\7\36\2\2\u0100\u0102\5\6\4\2\u0101\u0100\3\2\2\2\u0101"+
		"\u0102\3\2\2\2\u0102\u0103\3\2\2\2\u0103\u0104\7\37\2\2\u0104\27\3\2\2"+
		"\2\u0105\u0107\7\6\2\2\u0106\u0108\5\24\13\2\u0107\u0106\3\2\2\2\u0107"+
		"\u0108\3\2\2\2\u0108\u0109\3\2\2\2\u0109\u010a\7\7\2\2\u010a\31\3\2\2"+
		"\2\u010b\u010d\7\b\2\2\u010c\u010e\5\24\13\2\u010d\u010c\3\2\2\2\u010d"+
		"\u010e\3\2\2\2\u010e\u010f\3\2\2\2\u010f\u0110\7\t\2\2\u0110\33\3\2\2"+
		"\2\u0111\u0113\7[\2\2\u0112\u0114\5\26\f\2\u0113\u0112\3\2\2\2\u0113\u0114"+
		"\3\2\2\2\u0114\u0115\3\2\2\2\u0115\u0118\7#\2\2\u0116\u0119\5j\66\2\u0117"+
		"\u0119\5\u00b0Y\2\u0118\u0116\3\2\2\2\u0118\u0117\3\2\2\2\u0119\35\3\2"+
		"\2\2\u011a\u0123\7\n\2\2\u011b\u011e\5\34\17\2\u011c\u011d\7\5\2\2\u011d"+
		"\u011f\5\34\17\2\u011e\u011c\3\2\2\2\u011e\u011f\3\2\2\2\u011f\u0121\3"+
		"\2\2\2\u0120\u0122\7\5\2\2\u0121\u0120\3\2\2\2\u0121\u0122\3\2\2\2\u0122"+
		"\u0124\3\2\2\2\u0123\u011b\3\2\2\2\u0123\u0124\3\2\2\2\u0124\u0125\3\2"+
		"\2\2\u0125\u0126\7\13\2\2\u0126\37\3\2\2\2\u0127\u012d\5n8\2\u0128\u0129"+
		"\7\n\2\2\u0129\u012a\5j\66\2\u012a\u012b\7\13\2\2\u012b\u012d\3\2\2\2"+
		"\u012c\u0127\3\2\2\2\u012c\u0128\3\2\2\2\u012d!\3\2\2\2\u012e\u0137\7"+
		"\n\2\2\u012f\u0132\5 \21\2\u0130\u0131\7\5\2\2\u0131\u0133\5 \21\2\u0132"+
		"\u0130\3\2\2\2\u0132\u0133\3\2\2\2\u0133\u0135\3\2\2\2\u0134\u0136\7\5"+
		"\2\2\u0135\u0134\3\2\2\2\u0135\u0136\3\2\2\2\u0136\u0138\3\2\2\2\u0137"+
		"\u012f\3\2\2\2\u0137\u0138\3\2\2\2\u0138\u0139\3\2\2\2\u0139\u013a\7\13"+
		"\2\2\u013a#\3\2\2\2\u013b\u013d\7[\2\2\u013c\u013e\5\26\f\2\u013d\u013c"+
		"\3\2\2\2\u013d\u013e\3\2\2\2\u013e\u0145\3\2\2\2\u013f\u0145\5\u00acW"+
		"\2\u0140\u0141\5> \2\u0141\u0142\7#\2\2\u0142\u0143\5j\66\2\u0143\u0145"+
		"\3\2\2\2\u0144\u013b\3\2\2\2\u0144\u013f\3\2\2\2\u0144\u0140\3\2\2\2\u0145"+
		"%\3\2\2\2\u0146\u014f\7\n\2\2\u0147\u014a\5$\23\2\u0148\u0149\7\5\2\2"+
		"\u0149\u014b\5$\23\2\u014a\u0148\3\2\2\2\u014a\u014b\3\2\2\2\u014b\u014d"+
		"\3\2\2\2\u014c\u014e\7\5\2\2\u014d\u014c\3\2\2\2\u014d\u014e\3\2\2\2\u014e"+
		"\u0150\3\2\2\2\u014f\u0147\3\2\2\2\u014f\u0150\3\2\2\2\u0150\u0151\3\2"+
		"\2\2\u0151\u0152\7\13\2\2\u0152\'\3\2\2\2\u0153\u0154\t\2\2\2\u0154)\3"+
		"\2\2\2\u0155\u0156\t\3\2\2\u0156+\3\2\2\2\u0157\u0158\t\4\2\2\u0158-\3"+
		"\2\2\2\u0159\u015b\5(\25\2\u015a\u0159\3\2\2\2\u015b\u015e\3\2\2\2\u015c"+
		"\u015a\3\2\2\2\u015c\u015d\3\2\2\2\u015d\u015f\3\2\2\2\u015e\u015c\3\2"+
		"\2\2\u015f\u0160\5,\27\2\u0160\u0161\7[\2\2\u0161\u0165\7\n\2\2\u0162"+
		"\u0164\5\60\31\2\u0163\u0162\3\2\2\2\u0164\u0167\3\2\2\2\u0165\u0163\3"+
		"\2\2\2\u0165\u0166\3\2\2\2\u0166\u0168\3\2\2\2\u0167\u0165\3\2\2\2\u0168"+
		"\u0169\7\13\2\2\u0169/\3\2\2\2\u016a\u016c\5(\25\2\u016b\u016a\3\2\2\2"+
		"\u016c\u016f\3\2\2\2\u016d\u016b\3\2\2\2\u016d\u016e\3\2\2\2\u016e\u0170"+
		"\3\2\2\2\u016f\u016d\3\2\2\2\u0170\u0171\7[\2\2\u0171\u0175\7[\2\2\u0172"+
		"\u0176\5\62\32\2\u0173\u0176\5\64\33\2\u0174\u0176\5:\36\2\u0175\u0172"+
		"\3\2\2\2\u0175\u0173\3\2\2\2\u0175\u0174\3\2\2\2\u0176\61\3\2\2\2\u0177"+
		"\u0178\7#\2\2\u0178\u017a\5j\66\2\u0179\u0177\3\2\2\2\u0179\u017a\3\2"+
		"\2\2\u017a\u017b\3\2\2\2\u017b\u017c\7\4\2\2\u017c\63\3\2\2\2\u017d\u017e"+
		"\7\67\2\2\u017e\u01a5\5j\66\2\u017f\u0183\7\n\2\2\u0180\u0182\5(\25\2"+
		"\u0181\u0180\3\2\2\2\u0182\u0185\3\2\2\2\u0183\u0181\3\2\2\2\u0183\u0184"+
		"\3\2\2\2\u0184\u0186\3\2\2\2\u0185\u0183\3\2\2\2\u0186\u0187\5\66\34\2"+
		"\u0187\u018f\3\2\2\2\u0188\u018a\5(\25\2\u0189\u0188\3\2\2\2\u018a\u018d"+
		"\3\2\2\2\u018b\u0189\3\2\2\2\u018b\u018c\3\2\2\2\u018c\u018e\3\2\2\2\u018d"+
		"\u018b\3\2\2\2\u018e\u0190\58\35\2\u018f\u018b\3\2\2\2\u018f\u0190\3\2"+
		"\2\2\u0190\u01a5\3\2\2\2\u0191\u0193\5(\25\2\u0192\u0191\3\2\2\2\u0193"+
		"\u0196\3\2\2\2\u0194\u0192\3\2\2\2\u0194\u0195\3\2\2\2\u0195\u0197\3\2"+
		"\2\2\u0196\u0194\3\2\2\2\u0197\u0198\58\35\2\u0198\u01a0\3\2\2\2\u0199"+
		"\u019b\5(\25\2\u019a\u0199\3\2\2\2\u019b\u019e\3\2\2\2\u019c\u019a\3\2"+
		"\2\2\u019c\u019d\3\2\2\2\u019d\u019f\3\2\2\2\u019e\u019c\3\2\2\2\u019f"+
		"\u01a1\5\66\34\2\u01a0\u019c\3\2\2\2\u01a0\u01a1\3\2\2\2\u01a1\u01a2\3"+
		"\2\2\2\u01a2\u01a3\7\13\2\2\u01a3\u01a5\3\2\2\2\u01a4\u017d\3\2\2\2\u01a4"+
		"\u017f\3\2\2\2\u01a4\u0194\3\2\2\2\u01a5\65\3\2\2\2\u01a6\u01a7\7L\2\2"+
		"\u01a7\u01b0\7\4\2\2\u01a8\u01a9\7L\2\2\u01a9\u01aa\7\67\2\2\u01aa\u01ab"+
		"\5j\66\2\u01ab\u01ac\7\4\2\2\u01ac\u01b0\3\2\2\2\u01ad\u01ae\7L\2\2\u01ae"+
		"\u01b0\5B\"\2\u01af\u01a6\3\2\2\2\u01af\u01a8\3\2\2\2\u01af\u01ad\3\2"+
		"\2\2\u01b0\67\3\2\2\2\u01b1\u01b2\7M\2\2\u01b2\u01bb\7\4\2\2\u01b3\u01b4"+
		"\7M\2\2\u01b4\u01b5\7\67\2\2\u01b5\u01b6\5j\66\2\u01b6\u01b7\7\4\2\2\u01b7"+
		"\u01bb\3\2\2\2\u01b8\u01b9\7M\2\2\u01b9\u01bb\5B\"\2\u01ba\u01b1\3\2\2"+
		"\2\u01ba\u01b3\3\2\2\2\u01ba\u01b8\3\2\2\2\u01bb9\3\2\2\2\u01bc\u01be"+
		"\5\b\5\2\u01bd\u01bc\3\2\2\2\u01bd\u01be\3\2\2\2\u01be\u01bf\3\2\2\2\u01bf"+
		"\u01c5\5\16\b\2\u01c0\u01c1\7\67\2\2\u01c1\u01c2\5j\66\2\u01c2\u01c3\7"+
		"\4\2\2\u01c3\u01c6\3\2\2\2\u01c4\u01c6\5B\"\2\u01c5\u01c0\3\2\2\2\u01c5"+
		"\u01c4\3\2\2\2\u01c6;\3\2\2\2\u01c7\u01c8\t\5\2\2\u01c8=\3\2\2\2\u01c9"+
		"\u01cb\7\3\2\2\u01ca\u01c9\3\2\2\2\u01ca\u01cb\3\2\2\2\u01cb\u01cc\3\2"+
		"\2\2\u01cc\u01d1\7[\2\2\u01cd\u01ce\7\"\2\2\u01ce\u01d0\7[\2\2\u01cf\u01cd"+
		"\3\2\2\2\u01d0\u01d3\3\2\2\2\u01d1\u01cf\3\2\2\2\u01d1\u01d2\3\2\2\2\u01d2"+
		"\u01d5\3\2\2\2\u01d3\u01d1\3\2\2\2\u01d4\u01d6\5\26\f\2\u01d5\u01d4\3"+
		"\2\2\2\u01d5\u01d6\3\2\2\2\u01d6?\3\2\2\2\u01d7\u01e0\5B\"\2\u01d8\u01e0"+
		"\5X-\2\u01d9\u01da\5l\67\2\u01da\u01db\7\4\2\2\u01db\u01e0\3\2\2\2\u01dc"+
		"\u01dd\5j\66\2\u01dd\u01de\7\4\2\2\u01de\u01e0\3\2\2\2\u01df\u01d7\3\2"+
		"\2\2\u01df\u01d8\3\2\2\2\u01df\u01d9\3\2\2\2\u01df\u01dc\3\2\2\2\u01e0"+
		"A\3\2\2\2\u01e1\u01e5\7\n\2\2\u01e2\u01e4\5@!\2\u01e3\u01e2\3\2\2\2\u01e4"+
		"\u01e7\3\2\2\2\u01e5\u01e3\3\2\2\2\u01e5\u01e6\3\2\2\2\u01e6\u01e8\3\2"+
		"\2\2\u01e7\u01e5\3\2\2\2\u01e8\u01e9\7\13\2\2\u01e9C\3\2\2\2\u01ea\u01eb"+
		"\t\6\2\2\u01ebE\3\2\2\2\u01ec\u01ed\t\7\2\2\u01edG\3\2\2\2\u01ee\u01ef"+
		"\t\b\2\2\u01efI\3\2\2\2\u01f0\u01f1\t\t\2\2\u01f1K\3\2\2\2\u01f2\u01f3"+
		"\t\n\2\2\u01f3M\3\2\2\2\u01f4\u01f5\t\13\2\2\u01f5O\3\2\2\2\u01f6\u01f7"+
		"\t\f\2\2\u01f7Q\3\2\2\2\u01f8\u01f9\t\r\2\2\u01f9S\3\2\2\2\u01fa\u01fb"+
		"\t\16\2\2\u01fbU\3\2\2\2\u01fc\u01fd\t\17\2\2\u01fdW\3\2\2\2\u01fe\u0207"+
		"\5Z.\2\u01ff\u0207\5\\/\2\u0200\u0207\5^\60\2\u0201\u0207\5`\61\2\u0202"+
		"\u0207\5b\62\2\u0203\u0207\5d\63\2\u0204\u0207\5f\64\2\u0205\u0207\5h"+
		"\65\2\u0206\u01fe\3\2\2\2\u0206\u01ff\3\2\2\2\u0206\u0200\3\2\2\2\u0206"+
		"\u0201\3\2\2\2\u0206\u0202\3\2\2\2\u0206\u0203\3\2\2\2\u0206\u0204\3\2"+
		"\2\2\u0206\u0205\3\2\2\2\u0207Y\3\2\2\2\u0208\u0209\79\2\2\u0209\u020a"+
		"\7\6\2\2\u020a\u020b\5j\66\2\u020b\u020c\7\7\2\2\u020c\u020f\5@!\2\u020d"+
		"\u020e\7:\2\2\u020e\u0210\5@!\2\u020f\u020d\3\2\2\2\u020f\u0210\3\2\2"+
		"\2\u0210[\3\2\2\2\u0211\u0212\7;\2\2\u0212\u0213\7\6\2\2\u0213\u0214\5"+
		"l\67\2\u0214\u0215\7\4\2\2\u0215\u0216\5j\66\2\u0216\u0217\7\4\2\2\u0217"+
		"\u0218\5j\66\2\u0218\u0219\7\7\2\2\u0219\u021a\5@!\2\u021a]\3\2\2\2\u021b"+
		"\u021c\7<\2\2\u021c\u021d\7\6\2\2\u021d\u021e\7[\2\2\u021e\u021f\7[\2"+
		"\2\u021f\u0220\7\65\2\2\u0220\u0221\5j\66\2\u0221\u0222\7\7\2\2\u0222"+
		"\u0223\5@!\2\u0223_\3\2\2\2\u0224\u0225\7>\2\2\u0225\u0226\7\6\2\2\u0226"+
		"\u0227\5j\66\2\u0227\u0228\7\7\2\2\u0228\u0229\5@!\2\u0229a\3\2\2\2\u022a"+
		"\u022b\7=\2\2\u022b\u022c\5@!\2\u022c\u022d\7>\2\2\u022d\u022e\7\6\2\2"+
		"\u022e\u022f\5j\66\2\u022f\u0230\7\7\2\2\u0230\u0231\7\4\2\2\u0231c\3"+
		"\2\2\2\u0232\u0233\7?\2\2\u0233\u0234\5j\66\2\u0234\u0235\7\4\2\2\u0235"+
		"e\3\2\2\2\u0236\u0237\7@\2\2\u0237\u0238\5j\66\2\u0238\u0239\7\4\2\2\u0239"+
		"g\3\2\2\2\u023a\u023b\7A\2\2\u023b\u0244\5@!\2\u023c\u023d\7B\2\2\u023d"+
		"\u023e\7\6\2\2\u023e\u023f\7[\2\2\u023f\u0240\7[\2\2\u0240\u0241\7\7\2"+
		"\2\u0241\u0243\5@!\2\u0242\u023c\3\2\2\2\u0243\u0246\3\2\2\2\u0244\u0242"+
		"\3\2\2\2\u0244\u0245\3\2\2\2\u0245\u0249\3\2\2\2\u0246\u0244\3\2\2\2\u0247"+
		"\u0248\7C\2\2\u0248\u024a\5@!\2\u0249\u0247\3\2\2\2\u0249\u024a\3\2\2"+
		"\2\u024ai\3\2\2\2\u024b\u024e\5n8\2\u024c\u024e\5\u009eP\2\u024d\u024b"+
		"\3\2\2\2\u024d\u024c\3\2\2\2\u024ek\3\2\2\2\u024f\u0250\7[\2\2\u0250\u0251"+
		"\7[\2\2\u0251\u0252\7#\2\2\u0252\u0253\5j\66\2\u0253m\3\2\2\2\u0254\u0257"+
		"\5t;\2\u0255\u0257\5p9\2\u0256\u0254\3\2\2\2\u0256\u0255\3\2\2\2\u0257"+
		"o\3\2\2\2\u0258\u0259\5\30\r\2\u0259\u025a\7\67\2\2\u025a\u025b\5B\"\2"+
		"\u025bq\3\2\2\2\u025c\u0261\5j\66\2\u025d\u025e\7\5\2\2\u025e\u0260\5"+
		"j\66\2\u025f\u025d\3\2\2\2\u0260\u0263\3\2\2\2\u0261\u025f\3\2\2\2\u0261"+
		"\u0262\3\2\2\2\u0262s\3\2\2\2\u0263\u0261\3\2\2\2\u0264\u026a\5v<\2\u0265"+
		"\u0266\7/\2\2\u0266\u0267\5j\66\2\u0267\u0268\7\60\2\2\u0268\u0269\5j"+
		"\66\2\u0269\u026b\3\2\2\2\u026a\u0265\3\2\2\2\u026a\u026b\3\2\2\2\u026b"+
		"u\3\2\2\2\u026c\u026f\5x=\2\u026d\u026e\78\2\2\u026e\u0270\5v<\2\u026f"+
		"\u026d\3\2\2\2\u026f\u0270\3\2\2\2\u0270w\3\2\2\2\u0271\u0276\5z>\2\u0272"+
		"\u0273\7\30\2\2\u0273\u0275\5z>\2\u0274\u0272\3\2\2\2\u0275\u0278\3\2"+
		"\2\2\u0276\u0274\3\2\2\2\u0276\u0277\3\2\2\2\u0277y\3\2\2\2\u0278\u0276"+
		"\3\2\2\2\u0279\u027e\5|?\2\u027a\u027b\7\27\2\2\u027b\u027d\5|?\2\u027c"+
		"\u027a\3\2\2\2\u027d\u0280\3\2\2\2\u027e\u027c\3\2\2\2\u027e\u027f\3\2"+
		"\2\2\u027f{\3\2\2\2\u0280\u027e\3\2\2\2\u0281\u0286\5~@\2\u0282\u0283"+
		"\7\24\2\2\u0283\u0285\5~@\2\u0284\u0282\3\2\2\2\u0285\u0288\3\2\2\2\u0286"+
		"\u0284\3\2\2\2\u0286\u0287\3\2\2\2\u0287}\3\2\2\2\u0288\u0286\3\2\2\2"+
		"\u0289\u028e\5\u0080A\2\u028a\u028b\7\25\2\2\u028b\u028d\5\u0080A\2\u028c"+
		"\u028a\3\2\2\2\u028d\u0290\3\2\2\2\u028e\u028c\3\2\2\2\u028e\u028f\3\2"+
		"\2\2\u028f\177\3\2\2\2\u0290\u028e\3\2\2\2\u0291\u0296\5\u0082B\2\u0292"+
		"\u0293\7\23\2\2\u0293\u0295\5\u0082B\2\u0294\u0292\3\2\2\2\u0295\u0298"+
		"\3\2\2\2\u0296\u0294\3\2\2\2\u0296\u0297\3\2\2\2\u0297\u0081\3\2\2\2\u0298"+
		"\u0296\3\2\2\2\u0299\u029f\5\u0084C\2\u029a\u029b\5P)\2\u029b\u029c\5"+
		"\u0084C\2\u029c\u029e\3\2\2\2\u029d\u029a\3\2\2\2\u029e\u02a1\3\2\2\2"+
		"\u029f\u029d\3\2\2\2\u029f\u02a0\3\2\2\2\u02a0\u0083\3\2\2\2\u02a1\u029f"+
		"\3\2\2\2\u02a2\u02a6\5\u0088E\2\u02a3\u02a5\5\u0086D\2\u02a4\u02a3\3\2"+
		"\2\2\u02a5\u02a8\3\2\2\2\u02a6\u02a4\3\2\2\2\u02a6\u02a7\3\2\2\2\u02a7"+
		"\u0085\3\2\2\2\u02a8\u02a6\3\2\2\2\u02a9\u02aa\5R*\2\u02aa\u02ab\5\u0088"+
		"E\2\u02ab\u02af\3\2\2\2\u02ac\u02ad\t\20\2\2\u02ad\u02af\7[\2\2\u02ae"+
		"\u02a9\3\2\2\2\u02ae\u02ac\3\2\2\2\u02af\u0087\3\2\2\2\u02b0\u02b6\5\u008a"+
		"F\2\u02b1\u02b2\5N(\2\u02b2\u02b3\5\u008aF\2\u02b3\u02b5\3\2\2\2\u02b4"+
		"\u02b1\3\2\2\2\u02b5\u02b8\3\2\2\2\u02b6\u02b4\3\2\2\2\u02b6\u02b7\3\2"+
		"\2\2\u02b7\u0089\3\2\2\2\u02b8\u02b6\3\2\2\2\u02b9\u02bf\5\u008cG\2\u02ba"+
		"\u02bb\5D#\2\u02bb\u02bc\5\u008cG\2\u02bc\u02be\3\2\2\2\u02bd\u02ba\3"+
		"\2\2\2\u02be\u02c1\3\2\2\2\u02bf\u02bd\3\2\2\2\u02bf\u02c0\3\2\2\2\u02c0"+
		"\u008b\3\2\2\2\u02c1\u02bf\3\2\2\2\u02c2\u02c8\5\u008eH\2\u02c3\u02c4"+
		"\5F$\2\u02c4\u02c5\5\u008eH\2\u02c5\u02c7\3\2\2\2\u02c6\u02c3\3\2\2\2"+
		"\u02c7\u02ca\3\2\2\2\u02c8\u02c6\3\2\2\2\u02c8\u02c9\3\2\2\2\u02c9\u008d"+
		"\3\2\2\2\u02ca\u02c8\3\2\2\2\u02cb\u02ce\5\u0090I\2\u02cc\u02cd\7K\2\2"+
		"\u02cd\u02cf\5$\23\2\u02ce\u02cc\3\2\2\2\u02ce\u02cf\3\2\2\2\u02cf\u008f"+
		"\3\2\2\2\u02d0\u02d4\5\u0096L\2\u02d1\u02d2\5V,\2\u02d2\u02d3\5\u0096"+
		"L\2\u02d3\u02d5\3\2\2\2\u02d4\u02d1\3\2\2\2\u02d4\u02d5\3\2\2\2\u02d5"+
		"\u0091\3\2\2\2\u02d6\u02d7\5H%\2\u02d7\u02d8\5\u0096L\2\u02d8\u0093\3"+
		"\2\2\2\u02d9\u02dc\5<\37\2\u02da\u02dc\5> \2\u02db\u02d9\3\2\2\2\u02db"+
		"\u02da\3\2\2\2\u02dc\u02dd\3\2\2\2\u02dd\u02de\5H%\2\u02de\u0095\3\2\2"+
		"\2\u02df\u02e9\5\u00a0Q\2\u02e0\u02e1\t\21\2\2\u02e1\u02e9\5\u0096L\2"+
		"\u02e2\u02e3\5H%\2\u02e3\u02e4\5\u0096L\2\u02e4\u02e9\3\2\2\2\u02e5\u02e9"+
		"\5\u0098M\2\u02e6\u02e9\5\u009aN\2\u02e7\u02e9\5\u009cO\2\u02e8\u02df"+
		"\3\2\2\2\u02e8\u02e0\3\2\2\2\u02e8\u02e2\3\2\2\2\u02e8\u02e5\3\2\2\2\u02e8"+
		"\u02e6\3\2\2\2\u02e8\u02e7\3\2\2\2\u02e9\u0097\3\2\2\2\u02ea\u02eb\7\6"+
		"\2\2\u02eb\u02ec\7[\2\2\u02ec\u02ed\7\7\2\2\u02ed\u02ee\5\u0096L\2\u02ee"+
		"\u0099\3\2\2\2\u02ef\u02f0\7\16\2\2\u02f0\u02f1\5\u0096L\2\u02f1\u009b"+
		"\3\2\2\2\u02f2\u02f3\7\23\2\2\u02f3\u02f4\5\u0096L\2\u02f4\u009d\3\2\2"+
		"\2\u02f5\u02f6\5\u0096L\2\u02f6\u02f7\5T+\2\u02f7\u02f8\5j\66\2\u02f8"+
		"\u009f\3\2\2\2\u02f9\u02fc\5\u00a2R\2\u02fa\u02fc\5\u00aaV\2\u02fb\u02f9"+
		"\3\2\2\2\u02fb\u02fa\3\2\2\2\u02fc\u00a1\3\2\2\2\u02fd\u02fe\7D\2\2\u02fe"+
		"\u0300\5\32\16\2\u02ff\u0301\5\u00a4S\2\u0300\u02ff\3\2\2\2\u0300\u0301"+
		"\3\2\2\2\u0301\u0303\3\2\2\2\u0302\u0304\5\u00a6T\2\u0303\u0302\3\2\2"+
		"\2\u0303\u0304\3\2\2\2\u0304\u00a3\3\2\2\2\u0305\u0309\7\b\2\2\u0306\u0308"+
		"\7\5\2\2\u0307\u0306\3\2\2\2\u0308\u030b\3\2\2\2\u0309\u0307\3\2\2\2\u0309"+
		"\u030a\3\2\2\2\u030a\u030c\3\2\2\2\u030b\u0309\3\2\2\2\u030c\u030d\7\t"+
		"\2\2\u030d\u00a5\3\2\2\2\u030e\u031a\7\n\2\2\u030f\u0314\5\u00a8U\2\u0310"+
		"\u0311\7\5\2\2\u0311\u0313\5\u00a8U\2\u0312\u0310\3\2\2\2\u0313\u0316"+
		"\3\2\2\2\u0314\u0312\3\2\2\2\u0314\u0315\3\2\2\2\u0315\u0318\3\2\2\2\u0316"+
		"\u0314\3\2\2\2\u0317\u0319\7\5\2\2\u0318\u0317\3\2\2\2\u0318\u0319\3\2"+
		"\2\2\u0319\u031b\3\2\2\2\u031a\u030f\3\2\2\2\u031a\u031b\3\2\2\2\u031b"+
		"\u031c\3\2\2\2\u031c\u031d\7\13\2\2\u031d\u00a7\3\2\2\2\u031e\u0321\5"+
		"j\66\2\u031f\u0321\5\u00a6T\2\u0320\u031e\3\2\2\2\u0320\u031f\3\2\2\2"+
		"\u0321\u00a9\3\2\2\2\u0322\u032c\5<\37\2\u0323\u032c\5> \2\u0324\u0325"+
		"\7\6\2\2\u0325\u0326\5j\66\2\u0326\u0327\7\7\2\2\u0327\u032c\3\2\2\2\u0328"+
		"\u032c\5\u00acW\2\u0329\u032c\5\u0094K\2\u032a\u032c\5\u00aeX\2\u032b"+
		"\u0322\3\2\2\2\u032b\u0323\3\2\2\2\u032b\u0324\3\2\2\2\u032b\u0328\3\2"+
		"\2\2\u032b\u0329\3\2\2\2\u032b\u032a\3\2\2\2\u032c\u00ab\3\2\2\2\u032d"+
		"\u032e\7\6\2\2\u032e\u032f\5\u00a0Q\2\u032f\u0330\7\7\2\2\u0330\u0331"+
		"\7\"\2\2\u0331\u0333\3\2\2\2\u0332\u032d\3\2\2\2\u0332\u0333\3\2\2\2\u0333"+
		"\u0334\3\2\2\2\u0334\u0336\5> \2\u0335\u0337\5\26\f\2\u0336\u0335\3\2"+
		"\2\2\u0336\u0337\3\2\2\2\u0337\u033a\3\2\2\2\u0338\u033b\5\30\r\2\u0339"+
		"\u033b\5\32\16\2\u033a\u0338\3\2\2\2\u033a\u0339\3\2\2\2\u033a\u033b\3"+
		"\2\2\2\u033b\u00ad\3\2\2\2\u033c\u0343\5\u00b2Z\2\u033d\u0343\5\u00b4"+
		"[\2\u033e\u0343\5\u00b6\\\2\u033f\u0343\5\u00b8]\2\u0340\u0343\5\u00ba"+
		"^\2\u0341\u0343\5\u00be`\2\u0342\u033c\3\2\2\2\u0342\u033d\3\2\2\2\u0342"+
		"\u033e\3\2\2\2\u0342\u033f\3\2\2\2\u0342\u0340\3\2\2\2\u0342\u0341\3\2"+
		"\2\2\u0343\u00af\3\2\2\2\u0344\u0347\5\36\20\2\u0345\u0347\5\"\22\2\u0346"+
		"\u0344\3\2\2\2\u0346\u0345\3\2\2\2\u0347\u00b1\3\2\2\2\u0348\u0349\7D"+
		"\2\2\u0349\u034f\7[\2\2\u034a\u034c\5\30\r\2\u034b\u034d\5\u00b0Y\2\u034c"+
		"\u034b\3\2\2\2\u034c\u034d\3\2\2\2\u034d\u0350\3\2\2\2\u034e\u0350\5\u00b0"+
		"Y\2\u034f\u034a\3\2\2\2\u034f\u034e\3\2\2\2\u0350\u035a\3\2\2\2\u0351"+
		"\u0352\7D\2\2\u0352\u0353\7[\2\2\u0353\u0354\7\6\2\2\u0354\u0355\5j\66"+
		"\2\u0355\u0356\7\7\2\2\u0356\u035a\3\2\2\2\u0357\u0358\7D\2\2\u0358\u035a"+
		"\5&\24\2\u0359\u0348\3\2\2\2\u0359\u0351\3\2\2\2\u0359\u0357\3\2\2\2\u035a"+
		"\u00b3\3\2\2\2\u035b\u035c\7E\2\2\u035c\u035d\7\6\2\2\u035d\u035e\7[\2"+
		"\2\u035e\u035f\7\7\2\2\u035f\u00b5\3\2\2\2\u0360\u0361\7F\2\2\u0361\u0362"+
		"\7\6\2\2\u0362\u0363\5j\66\2\u0363\u0364\7\7\2\2\u0364\u00b7\3\2\2\2\u0365"+
		"\u0366\7G\2\2\u0366\u0367\7\6\2\2\u0367\u0368\5j\66\2\u0368\u0369\7\7"+
		"\2\2\u0369\u00b9\3\2\2\2\u036a\u036e\7H\2\2\u036b\u036c\7\6\2\2\u036c"+
		"\u036d\7[\2\2\u036d\u036f\7\7\2\2\u036e\u036b\3\2\2\2\u036e\u036f\3\2"+
		"\2\2\u036f\u00bb\3\2\2\2\u0370\u0371\7I\2\2\u0371\u0372\5\16\b\2\u0372"+
		"\u0373\5B\"\2\u0373\u00bd\3\2\2\2\u0374\u0375\7J\2\2\u0375\u0376\7\6\2"+
		"\2\u0376\u0377\7[\2\2\u0377\u0378\7\7\2\2\u0378\u00bf\3\2\2\2Z\u00c3\u00cf"+
		"\u00d7\u00e1\u00e6\u00ec\u00f5\u00fc\u0101\u0107\u010d\u0113\u0118\u011e"+
		"\u0121\u0123\u012c\u0132\u0135\u0137\u013d\u0144\u014a\u014d\u014f\u015c"+
		"\u0165\u016d\u0175\u0179\u0183\u018b\u018f\u0194\u019c\u01a0\u01a4\u01af"+
		"\u01ba\u01bd\u01c5\u01ca\u01d1\u01d5\u01df\u01e5\u0206\u020f\u0244\u0249"+
		"\u024d\u0256\u0261\u026a\u026f\u0276\u027e\u0286\u028e\u0296\u029f\u02a6"+
		"\u02ae\u02b6\u02bf\u02c8\u02ce\u02d4\u02db\u02e8\u02fb\u0300\u0303\u0309"+
		"\u0314\u0318\u031a\u0320\u032b\u0332\u0336\u033a\u0342\u0346\u034c\u034f"+
		"\u0359\u036e";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}