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
		WHITESPACE=86, SINGLELINE_COMMENT=87, NEWLINE=88, MULTILINE_COMMENT=89, 
		STRING=90, DECIMAL=91, INTEGER=92, BOOLEAN=93, NAME=94;
	public static final int
		RULE_script = 0, RULE_generic_parameter = 1, RULE_generic_parameter_list = 2, 
		RULE_generic_parameters = 3, RULE_method_parameter = 4, RULE_method_parameter_list = 5, 
		RULE_method_parameters = 6, RULE_indexer_parameters = 7, RULE_argument = 8, 
		RULE_argument_list = 9, RULE_generic_arguments = 10, RULE_method_arguments = 11, 
		RULE_indexer_arguments = 12, RULE_member_initializer = 13, RULE_object_initializer = 14, 
		RULE_element_initializer = 15, RULE_collection_initializer = 16, RULE_anonymous_element_initializer = 17, 
		RULE_anonymous_object_initializer = 18, RULE_modifier = 19, RULE_parameter_modifier = 20, 
		RULE_class_type = 21, RULE_attribute_tag = 22, RULE_type_definition = 23, 
		RULE_member_definition = 24, RULE_constructor_definition = 25, RULE_field_definition = 26, 
		RULE_property_definition = 27, RULE_property_get_definition = 28, RULE_property_set_definition = 29, 
		RULE_method_definition = 30, RULE_literal = 31, RULE_identifier = 32, 
		RULE_short_identifier = 33, RULE_statement = 34, RULE_code_block = 35, 
		RULE_additive_operator = 36, RULE_multiplicative_operator = 37, RULE_step_operator = 38, 
		RULE_bitwise_operator = 39, RULE_boolean_operator = 40, RULE_shift_operator = 41, 
		RULE_equality_operator = 42, RULE_relation_operator = 43, RULE_assignment_operator = 44, 
		RULE_range_operator = 45, RULE_language_function = 46, RULE_if_statement = 47, 
		RULE_for_statement = 48, RULE_foreach_statement = 49, RULE_while_statement = 50, 
		RULE_do_statement = 51, RULE_return_statement = 52, RULE_throw_statement = 53, 
		RULE_try_statement = 54, RULE_expression = 55, RULE_initialization_expression = 56, 
		RULE_non_assignment_expression = 57, RULE_lambda_expression = 58, RULE_expression_list = 59, 
		RULE_conditional_expression = 60, RULE_null_coalescing_expression = 61, 
		RULE_conditional_or_expression = 62, RULE_conditional_and_expression = 63, 
		RULE_inclusive_or_expression = 64, RULE_exclusive_or_expression = 65, 
		RULE_and_expression = 66, RULE_equality_expression = 67, RULE_relational_expression = 68, 
		RULE_relation_or_type_check = 69, RULE_shift_expression = 70, RULE_additive_expression = 71, 
		RULE_multiplicative_expression = 72, RULE_with_expression = 73, RULE_range_expression = 74, 
		RULE_pre_step_expression = 75, RULE_post_step_expression = 76, RULE_unary_expression = 77, 
		RULE_cast_expression = 78, RULE_pointer_indirection_expression = 79, RULE_addressof_expression = 80, 
		RULE_assignment_expression = 81, RULE_primary_expression = 82, RULE_array_creation_expression = 83, 
		RULE_array_rank_specifier = 84, RULE_array_initializer = 85, RULE_variable_initializer = 86, 
		RULE_primary_no_array_creation_expression = 87, RULE_member_access_prefix = 88, 
		RULE_member_access = 89, RULE_keyword_expression = 90, RULE_object_or_collection_initializer = 91, 
		RULE_new_keyword_expression = 92, RULE_typeof_keyword_expression = 93, 
		RULE_checked_expression = 94, RULE_unchecked_expression = 95, RULE_default_keyword_expression = 96, 
		RULE_delegate_keyword_expression = 97, RULE_sizeof_keyword_expression = 98;
	private static String[] makeRuleNames() {
		return new String[] {
			"script", "generic_parameter", "generic_parameter_list", "generic_parameters", 
			"method_parameter", "method_parameter_list", "method_parameters", "indexer_parameters", 
			"argument", "argument_list", "generic_arguments", "method_arguments", 
			"indexer_arguments", "member_initializer", "object_initializer", "element_initializer", 
			"collection_initializer", "anonymous_element_initializer", "anonymous_object_initializer", 
			"modifier", "parameter_modifier", "class_type", "attribute_tag", "type_definition", 
			"member_definition", "constructor_definition", "field_definition", "property_definition", 
			"property_get_definition", "property_set_definition", "method_definition", 
			"literal", "identifier", "short_identifier", "statement", "code_block", 
			"additive_operator", "multiplicative_operator", "step_operator", "bitwise_operator", 
			"boolean_operator", "shift_operator", "equality_operator", "relation_operator", 
			"assignment_operator", "range_operator", "language_function", "if_statement", 
			"for_statement", "foreach_statement", "while_statement", "do_statement", 
			"return_statement", "throw_statement", "try_statement", "expression", 
			"initialization_expression", "non_assignment_expression", "lambda_expression", 
			"expression_list", "conditional_expression", "null_coalescing_expression", 
			"conditional_or_expression", "conditional_and_expression", "inclusive_or_expression", 
			"exclusive_or_expression", "and_expression", "equality_expression", "relational_expression", 
			"relation_or_type_check", "shift_expression", "additive_expression", 
			"multiplicative_expression", "with_expression", "range_expression", "pre_step_expression", 
			"post_step_expression", "unary_expression", "cast_expression", "pointer_indirection_expression", 
			"addressof_expression", "assignment_expression", "primary_expression", 
			"array_creation_expression", "array_rank_specifier", "array_initializer", 
			"variable_initializer", "primary_no_array_creation_expression", "member_access_prefix", 
			"member_access", "keyword_expression", "object_or_collection_initializer", 
			"new_keyword_expression", "typeof_keyword_expression", "checked_expression", 
			"unchecked_expression", "default_keyword_expression", "delegate_keyword_expression", 
			"sizeof_keyword_expression"
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
			"WHITESPACE", "SINGLELINE_COMMENT", "NEWLINE", "MULTILINE_COMMENT", "STRING", 
			"DECIMAL", "INTEGER", "BOOLEAN", "NAME"
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
			setState(201);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==OB || ((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)) | (1L << (CLASS - 76)) | (1L << (STRUCT - 76)))) != 0)) {
				{
				{
				setState(198);
				type_definition();
				}
				}
				setState(203);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(204);
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
			setState(206);
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
			setState(208);
			generic_parameter();
			setState(213);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(209);
				match(COMMA);
				setState(210);
				generic_parameter();
				}
				}
				setState(215);
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
			setState(216);
			match(LESS_THAN);
			setState(217);
			generic_parameter_list();
			setState(218);
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
			setState(221);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 51)) & ~0x3f) == 0 && ((1L << (_la - 51)) & ((1L << (IN - 51)) | (1L << (OUT - 51)) | (1L << (REF - 51)))) != 0)) {
				{
				setState(220);
				parameter_modifier();
				}
			}

			setState(223);
			match(NAME);
			setState(224);
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
			setState(226);
			method_parameter();
			setState(231);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(227);
				match(COMMA);
				setState(228);
				method_parameter();
				}
				}
				setState(233);
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
			setState(234);
			match(OP);
			setState(236);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 51)) & ~0x3f) == 0 && ((1L << (_la - 51)) & ((1L << (IN - 51)) | (1L << (OUT - 51)) | (1L << (REF - 51)) | (1L << (NAME - 51)))) != 0)) {
				{
				setState(235);
				method_parameter_list();
				}
			}

			setState(238);
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
			setState(240);
			match(OB);
			setState(242);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (((((_la - 51)) & ~0x3f) == 0 && ((1L << (_la - 51)) & ((1L << (IN - 51)) | (1L << (OUT - 51)) | (1L << (REF - 51)) | (1L << (NAME - 51)))) != 0)) {
				{
				setState(241);
				method_parameter_list();
				}
			}

			setState(244);
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
			setState(251);
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
			case BOOLEAN:
			case NAME:
				enterOuterAlt(_localctx, 1);
				{
				setState(246);
				expression();
				}
				break;
			case IN:
			case OUT:
			case REF:
				enterOuterAlt(_localctx, 2);
				{
				setState(247);
				parameter_modifier();
				setState(248);
				match(NAME);
				setState(249);
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
			setState(253);
			argument();
			setState(258);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(254);
				match(COMMA);
				setState(255);
				argument();
				}
				}
				setState(260);
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
			setState(261);
			match(LESS_THAN);
			setState(263);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==NAME) {
				{
				setState(262);
				generic_parameter_list();
				}
			}

			setState(265);
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
			setState(267);
			match(OP);
			setState(269);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << OP) | (1L << PLUS) | (1L << MINUS) | (1L << MULTIPLY) | (1L << INCREMENT) | (1L << DECREMENT) | (1L << BITWISE_AND) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT) | (1L << IN) | (1L << OUT))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (REF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (BOOLEAN - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				setState(268);
				argument_list();
				}
			}

			setState(271);
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
			setState(273);
			match(OB);
			setState(275);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << OP) | (1L << PLUS) | (1L << MINUS) | (1L << MULTIPLY) | (1L << INCREMENT) | (1L << DECREMENT) | (1L << BITWISE_AND) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT) | (1L << IN) | (1L << OUT))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (REF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (BOOLEAN - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				setState(274);
				argument_list();
				}
			}

			setState(277);
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
			setState(279);
			match(NAME);
			setState(281);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LESS_THAN) {
				{
				setState(280);
				generic_arguments();
				}
			}

			setState(283);
			match(ASSIGN);
			setState(286);
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
			case BOOLEAN:
			case NAME:
				{
				setState(284);
				expression();
				}
				break;
			case OC:
				{
				setState(285);
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
			setState(288);
			match(OC);
			setState(297);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==NAME) {
				{
				setState(289);
				member_initializer();
				setState(292);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,13,_ctx) ) {
				case 1:
					{
					setState(290);
					match(COMMA);
					setState(291);
					member_initializer();
					}
					break;
				}
				setState(295);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==COMMA) {
					{
					setState(294);
					match(COMMA);
					}
				}

				}
			}

			setState(299);
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
			setState(306);
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
			case BOOLEAN:
			case NAME:
				enterOuterAlt(_localctx, 1);
				{
				setState(301);
				non_assignment_expression();
				}
				break;
			case OC:
				enterOuterAlt(_localctx, 2);
				{
				setState(302);
				match(OC);
				setState(303);
				expression();
				setState(304);
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
			setState(308);
			match(OC);
			setState(317);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << OP) | (1L << OC) | (1L << PLUS) | (1L << MINUS) | (1L << MULTIPLY) | (1L << INCREMENT) | (1L << DECREMENT) | (1L << BITWISE_AND) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (BOOLEAN - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				setState(309);
				element_initializer();
				setState(312);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,17,_ctx) ) {
				case 1:
					{
					setState(310);
					match(COMMA);
					setState(311);
					element_initializer();
					}
					break;
				}
				setState(315);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==COMMA) {
					{
					setState(314);
					match(COMMA);
					}
				}

				}
			}

			setState(319);
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
			setState(330);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,21,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(321);
				match(NAME);
				setState(323);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,20,_ctx) ) {
				case 1:
					{
					setState(322);
					generic_arguments();
					}
					break;
				}
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(325);
				member_access();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(326);
				identifier();
				setState(327);
				match(ASSIGN);
				setState(328);
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
			setState(332);
			match(OC);
			setState(341);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__0 || _la==OP || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (BOOLEAN - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				setState(333);
				anonymous_element_initializer();
				setState(336);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,22,_ctx) ) {
				case 1:
					{
					setState(334);
					match(COMMA);
					setState(335);
					anonymous_element_initializer();
					}
					break;
				}
				setState(339);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==COMMA) {
					{
					setState(338);
					match(COMMA);
					}
				}

				}
			}

			setState(343);
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
			setState(345);
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
			setState(347);
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
			setState(349);
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

	public static class Attribute_tagContext extends ParserRuleContext {
		public TerminalNode OB() { return getToken(MCSharpParser.OB, 0); }
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public TerminalNode CB() { return getToken(MCSharpParser.CB, 0); }
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public Method_parametersContext method_parameters() {
			return getRuleContext(Method_parametersContext.class,0);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public Attribute_tagContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_attribute_tag; }
	}

	public final Attribute_tagContext attribute_tag() throws RecognitionException {
		Attribute_tagContext _localctx = new Attribute_tagContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_attribute_tag);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(351);
			match(OB);
			setState(352);
			match(NAME);
			setState(357);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==OP) {
				{
				setState(353);
				match(OP);
				setState(354);
				method_parameters();
				setState(355);
				match(CP);
				}
			}

			setState(359);
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

	public static class Type_definitionContext extends ParserRuleContext {
		public Class_typeContext class_type() {
			return getRuleContext(Class_typeContext.class,0);
		}
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public TerminalNode OC() { return getToken(MCSharpParser.OC, 0); }
		public TerminalNode CC() { return getToken(MCSharpParser.CC, 0); }
		public List<Attribute_tagContext> attribute_tag() {
			return getRuleContexts(Attribute_tagContext.class);
		}
		public Attribute_tagContext attribute_tag(int i) {
			return getRuleContext(Attribute_tagContext.class,i);
		}
		public List<ModifierContext> modifier() {
			return getRuleContexts(ModifierContext.class);
		}
		public ModifierContext modifier(int i) {
			return getRuleContext(ModifierContext.class,i);
		}
		public List<Constructor_definitionContext> constructor_definition() {
			return getRuleContexts(Constructor_definitionContext.class);
		}
		public Constructor_definitionContext constructor_definition(int i) {
			return getRuleContext(Constructor_definitionContext.class,i);
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
		enterRule(_localctx, 46, RULE_type_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(364);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==OB) {
				{
				{
				setState(361);
				attribute_tag();
				}
				}
				setState(366);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(370);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
				{
				{
				setState(367);
				modifier();
				}
				}
				setState(372);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(373);
			class_type();
			setState(374);
			match(NAME);
			setState(375);
			match(OC);
			setState(380);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==OB || ((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)) | (1L << (NAME - 76)))) != 0)) {
				{
				setState(378);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,28,_ctx) ) {
				case 1:
					{
					setState(376);
					constructor_definition();
					}
					break;
				case 2:
					{
					setState(377);
					member_definition();
					}
					break;
				}
				}
				setState(382);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(383);
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
		public List<Attribute_tagContext> attribute_tag() {
			return getRuleContexts(Attribute_tagContext.class);
		}
		public Attribute_tagContext attribute_tag(int i) {
			return getRuleContext(Attribute_tagContext.class,i);
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
		enterRule(_localctx, 48, RULE_member_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(388);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==OB) {
				{
				{
				setState(385);
				attribute_tag();
				}
				}
				setState(390);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(394);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
				{
				{
				setState(391);
				modifier();
				}
				}
				setState(396);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(397);
			match(NAME);
			setState(398);
			match(NAME);
			setState(402);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case END:
			case ASSIGN:
				{
				setState(399);
				field_definition();
				}
				break;
			case OB:
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
				setState(400);
				property_definition();
				}
				break;
			case OP:
			case LESS_THAN:
				{
				setState(401);
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

	public static class Constructor_definitionContext extends ParserRuleContext {
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public Method_parametersContext method_parameters() {
			return getRuleContext(Method_parametersContext.class,0);
		}
		public Code_blockContext code_block() {
			return getRuleContext(Code_blockContext.class,0);
		}
		public TerminalNode LAMBDA() { return getToken(MCSharpParser.LAMBDA, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public List<Attribute_tagContext> attribute_tag() {
			return getRuleContexts(Attribute_tagContext.class);
		}
		public Attribute_tagContext attribute_tag(int i) {
			return getRuleContext(Attribute_tagContext.class,i);
		}
		public List<ModifierContext> modifier() {
			return getRuleContexts(ModifierContext.class);
		}
		public ModifierContext modifier(int i) {
			return getRuleContext(ModifierContext.class,i);
		}
		public Constructor_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_constructor_definition; }
	}

	public final Constructor_definitionContext constructor_definition() throws RecognitionException {
		Constructor_definitionContext _localctx = new Constructor_definitionContext(_ctx, getState());
		enterRule(_localctx, 50, RULE_constructor_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(407);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==OB) {
				{
				{
				setState(404);
				attribute_tag();
				}
				}
				setState(409);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(413);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
				{
				{
				setState(410);
				modifier();
				}
				}
				setState(415);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(416);
			match(NAME);
			setState(417);
			method_parameters();
			setState(421);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case OC:
				{
				setState(418);
				code_block();
				}
				break;
			case LAMBDA:
				{
				setState(419);
				match(LAMBDA);
				setState(420);
				expression();
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
		enterRule(_localctx, 52, RULE_field_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(425);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==ASSIGN) {
				{
				setState(423);
				match(ASSIGN);
				setState(424);
				expression();
				}
			}

			setState(427);
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
		public List<Attribute_tagContext> attribute_tag() {
			return getRuleContexts(Attribute_tagContext.class);
		}
		public Attribute_tagContext attribute_tag(int i) {
			return getRuleContext(Attribute_tagContext.class,i);
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
		enterRule(_localctx, 54, RULE_property_definition);
		int _la;
		try {
			setState(492);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LAMBDA:
				enterOuterAlt(_localctx, 1);
				{
				setState(429);
				match(LAMBDA);
				setState(430);
				expression();
				}
				break;
			case OC:
				enterOuterAlt(_localctx, 2);
				{
				setState(431);
				match(OC);
				{
				{
				setState(435);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==OB) {
					{
					{
					setState(432);
					attribute_tag();
					}
					}
					setState(437);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(441);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
					{
					{
					setState(438);
					modifier();
					}
					}
					setState(443);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(444);
				property_get_definition();
				}
				setState(459);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,41,_ctx) ) {
				case 1:
					{
					setState(449);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==OB) {
						{
						{
						setState(446);
						attribute_tag();
						}
						}
						setState(451);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(455);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
						{
						{
						setState(452);
						modifier();
						}
						}
						setState(457);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(458);
					property_set_definition();
					}
					break;
				}
				}
				}
				break;
			case OB:
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
				setState(464);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==OB) {
					{
					{
					setState(461);
					attribute_tag();
					}
					}
					setState(466);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(470);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
					{
					{
					setState(467);
					modifier();
					}
					}
					setState(472);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(473);
				property_set_definition();
				}
				setState(488);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==OB || ((((_la - 74)) & ~0x3f) == 0 && ((1L << (_la - 74)) & ((1L << (GET - 74)) | (1L << (PUBLIC - 74)) | (1L << (PRIVATE - 74)) | (1L << (PROTECTED - 74)) | (1L << (STATIC - 74)) | (1L << (ABSTRACT - 74)) | (1L << (VIRTUAL - 74)))) != 0)) {
					{
					setState(478);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==OB) {
						{
						{
						setState(475);
						attribute_tag();
						}
						}
						setState(480);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(484);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (PUBLIC - 76)) | (1L << (PRIVATE - 76)) | (1L << (PROTECTED - 76)) | (1L << (STATIC - 76)) | (1L << (ABSTRACT - 76)) | (1L << (VIRTUAL - 76)))) != 0)) {
						{
						{
						setState(481);
						modifier();
						}
						}
						setState(486);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(487);
					property_get_definition();
					}
				}

				}
				setState(490);
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
		enterRule(_localctx, 56, RULE_property_get_definition);
		try {
			setState(503);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,48,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(494);
				match(GET);
				setState(495);
				match(END);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(496);
				match(GET);
				setState(497);
				match(LAMBDA);
				setState(498);
				expression();
				setState(499);
				match(END);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(501);
				match(GET);
				setState(502);
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
		enterRule(_localctx, 58, RULE_property_set_definition);
		try {
			setState(514);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,49,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(505);
				match(SET);
				setState(506);
				match(END);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(507);
				match(SET);
				setState(508);
				match(LAMBDA);
				setState(509);
				expression();
				setState(510);
				match(END);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(512);
				match(SET);
				setState(513);
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
		enterRule(_localctx, 60, RULE_method_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(517);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==LESS_THAN) {
				{
				setState(516);
				generic_parameters();
				}
			}

			setState(519);
			method_parameters();
			setState(525);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LAMBDA:
				{
				setState(520);
				match(LAMBDA);
				setState(521);
				expression();
				setState(522);
				match(END);
				}
				break;
			case OC:
				{
				setState(524);
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
		public TerminalNode BOOLEAN() { return getToken(MCSharpParser.BOOLEAN, 0); }
		public TerminalNode DECIMAL() { return getToken(MCSharpParser.DECIMAL, 0); }
		public TerminalNode STRING() { return getToken(MCSharpParser.STRING, 0); }
		public LiteralContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_literal; }
	}

	public final LiteralContext literal() throws RecognitionException {
		LiteralContext _localctx = new LiteralContext(_ctx, getState());
		enterRule(_localctx, 62, RULE_literal);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(527);
			_la = _input.LA(1);
			if ( !(((((_la - 90)) & ~0x3f) == 0 && ((1L << (_la - 90)) & ((1L << (STRING - 90)) | (1L << (DECIMAL - 90)) | (1L << (INTEGER - 90)) | (1L << (BOOLEAN - 90)))) != 0)) ) {
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
		public IdentifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_identifier; }
	}

	public final IdentifierContext identifier() throws RecognitionException {
		IdentifierContext _localctx = new IdentifierContext(_ctx, getState());
		enterRule(_localctx, 64, RULE_identifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(530);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__0) {
				{
				setState(529);
				match(T__0);
				}
			}

			setState(532);
			match(NAME);
			setState(537);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==DOT) {
				{
				{
				setState(533);
				match(DOT);
				setState(534);
				match(NAME);
				}
				}
				setState(539);
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

	public static class Short_identifierContext extends ParserRuleContext {
		public TerminalNode NAME() { return getToken(MCSharpParser.NAME, 0); }
		public Short_identifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_short_identifier; }
	}

	public final Short_identifierContext short_identifier() throws RecognitionException {
		Short_identifierContext _localctx = new Short_identifierContext(_ctx, getState());
		enterRule(_localctx, 66, RULE_short_identifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(541);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__0) {
				{
				setState(540);
				match(T__0);
				}
			}

			setState(543);
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
		enterRule(_localctx, 68, RULE_statement);
		try {
			setState(553);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,55,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(545);
				code_block();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(546);
				language_function();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(547);
				initialization_expression();
				setState(548);
				match(END);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(550);
				expression();
				setState(551);
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
		enterRule(_localctx, 70, RULE_code_block);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(555);
			match(OC);
			setState(559);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << OP) | (1L << OC) | (1L << PLUS) | (1L << MINUS) | (1L << MULTIPLY) | (1L << INCREMENT) | (1L << DECREMENT) | (1L << BITWISE_AND) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT) | (1L << IF) | (1L << FOR) | (1L << FOREACH) | (1L << DO) | (1L << WHILE) | (1L << RETURN) | (1L << THROW) | (1L << TRY))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (BOOLEAN - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				{
				setState(556);
				statement();
				}
				}
				setState(561);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(562);
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
		enterRule(_localctx, 72, RULE_additive_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(564);
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
		enterRule(_localctx, 74, RULE_multiplicative_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(566);
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
		enterRule(_localctx, 76, RULE_step_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(568);
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
		enterRule(_localctx, 78, RULE_bitwise_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(570);
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
		enterRule(_localctx, 80, RULE_boolean_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(572);
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
		enterRule(_localctx, 82, RULE_shift_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(574);
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
		enterRule(_localctx, 84, RULE_equality_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(576);
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
		enterRule(_localctx, 86, RULE_relation_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(578);
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
		enterRule(_localctx, 88, RULE_assignment_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(580);
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
		enterRule(_localctx, 90, RULE_range_operator);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(582);
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
		enterRule(_localctx, 92, RULE_language_function);
		try {
			setState(592);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case IF:
				enterOuterAlt(_localctx, 1);
				{
				setState(584);
				if_statement();
				}
				break;
			case FOR:
				enterOuterAlt(_localctx, 2);
				{
				setState(585);
				for_statement();
				}
				break;
			case FOREACH:
				enterOuterAlt(_localctx, 3);
				{
				setState(586);
				foreach_statement();
				}
				break;
			case WHILE:
				enterOuterAlt(_localctx, 4);
				{
				setState(587);
				while_statement();
				}
				break;
			case DO:
				enterOuterAlt(_localctx, 5);
				{
				setState(588);
				do_statement();
				}
				break;
			case RETURN:
				enterOuterAlt(_localctx, 6);
				{
				setState(589);
				return_statement();
				}
				break;
			case THROW:
				enterOuterAlt(_localctx, 7);
				{
				setState(590);
				throw_statement();
				}
				break;
			case TRY:
				enterOuterAlt(_localctx, 8);
				{
				setState(591);
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
		enterRule(_localctx, 94, RULE_if_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(594);
			match(IF);
			setState(595);
			match(OP);
			setState(596);
			expression();
			setState(597);
			match(CP);
			setState(598);
			statement();
			setState(601);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,58,_ctx) ) {
			case 1:
				{
				setState(599);
				match(ELSE);
				setState(600);
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
		enterRule(_localctx, 96, RULE_for_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(603);
			match(FOR);
			setState(604);
			match(OP);
			setState(605);
			initialization_expression();
			setState(606);
			match(END);
			setState(607);
			expression();
			setState(608);
			match(END);
			setState(609);
			expression();
			setState(610);
			match(CP);
			setState(611);
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
		enterRule(_localctx, 98, RULE_foreach_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(613);
			match(FOREACH);
			setState(614);
			match(OP);
			setState(615);
			match(NAME);
			setState(616);
			match(NAME);
			setState(617);
			match(IN);
			setState(618);
			expression();
			setState(619);
			match(CP);
			setState(620);
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
		enterRule(_localctx, 100, RULE_while_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(622);
			match(WHILE);
			setState(623);
			match(OP);
			setState(624);
			expression();
			setState(625);
			match(CP);
			setState(626);
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
		enterRule(_localctx, 102, RULE_do_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(628);
			match(DO);
			setState(629);
			statement();
			setState(630);
			match(WHILE);
			setState(631);
			match(OP);
			setState(632);
			expression();
			setState(633);
			match(CP);
			setState(634);
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
		public TerminalNode END() { return getToken(MCSharpParser.END, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Return_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_return_statement; }
	}

	public final Return_statementContext return_statement() throws RecognitionException {
		Return_statementContext _localctx = new Return_statementContext(_ctx, getState());
		enterRule(_localctx, 104, RULE_return_statement);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(636);
			match(RETURN);
			setState(638);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << OP) | (1L << PLUS) | (1L << MINUS) | (1L << MULTIPLY) | (1L << INCREMENT) | (1L << DECREMENT) | (1L << BITWISE_AND) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (BOOLEAN - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				setState(637);
				expression();
				}
			}

			setState(640);
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
		enterRule(_localctx, 106, RULE_throw_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(642);
			match(THROW);
			setState(643);
			expression();
			setState(644);
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
		enterRule(_localctx, 108, RULE_try_statement);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(646);
			match(TRY);
			setState(647);
			statement();
			setState(656);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,60,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(648);
					match(CATCH);
					setState(649);
					match(OP);
					setState(650);
					match(NAME);
					setState(651);
					match(NAME);
					setState(652);
					match(CP);
					setState(653);
					statement();
					}
					} 
				}
				setState(658);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,60,_ctx);
			}
			setState(661);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,61,_ctx) ) {
			case 1:
				{
				setState(659);
				match(FINALLY);
				setState(660);
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
		enterRule(_localctx, 110, RULE_expression);
		try {
			setState(665);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,62,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(663);
				non_assignment_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(664);
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
		enterRule(_localctx, 112, RULE_initialization_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(667);
			match(NAME);
			setState(668);
			match(NAME);
			setState(671);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==ASSIGN) {
				{
				setState(669);
				match(ASSIGN);
				setState(670);
				expression();
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
		enterRule(_localctx, 114, RULE_non_assignment_expression);
		try {
			setState(675);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,64,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(673);
				conditional_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(674);
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
		enterRule(_localctx, 116, RULE_lambda_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(677);
			method_arguments();
			setState(678);
			match(LAMBDA);
			{
			setState(679);
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
		enterRule(_localctx, 118, RULE_expression_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(681);
			expression();
			setState(686);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(682);
				match(COMMA);
				setState(683);
				expression();
				}
				}
				setState(688);
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
		enterRule(_localctx, 120, RULE_conditional_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(689);
			null_coalescing_expression();
			setState(695);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,66,_ctx) ) {
			case 1:
				{
				setState(690);
				match(CONDITION_IF);
				setState(691);
				expression();
				setState(692);
				match(CONDITION_ELSE);
				setState(693);
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
		enterRule(_localctx, 122, RULE_null_coalescing_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(697);
			conditional_or_expression();
			setState(700);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,67,_ctx) ) {
			case 1:
				{
				setState(698);
				match(NULL_COALESCING);
				setState(699);
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
		enterRule(_localctx, 124, RULE_conditional_or_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(702);
			conditional_and_expression();
			setState(707);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,68,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(703);
					match(BOOLEAN_OR);
					setState(704);
					conditional_and_expression();
					}
					} 
				}
				setState(709);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,68,_ctx);
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
		enterRule(_localctx, 126, RULE_conditional_and_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(710);
			inclusive_or_expression();
			setState(715);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,69,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(711);
					match(BOOLEAN_AND);
					setState(712);
					inclusive_or_expression();
					}
					} 
				}
				setState(717);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,69,_ctx);
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
		enterRule(_localctx, 128, RULE_inclusive_or_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(718);
			exclusive_or_expression();
			setState(723);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,70,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(719);
					match(BITWISE_OR);
					setState(720);
					exclusive_or_expression();
					}
					} 
				}
				setState(725);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,70,_ctx);
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
		enterRule(_localctx, 130, RULE_exclusive_or_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(726);
			and_expression();
			setState(731);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,71,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(727);
					match(BITWISE_XOR);
					setState(728);
					and_expression();
					}
					} 
				}
				setState(733);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,71,_ctx);
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
		enterRule(_localctx, 132, RULE_and_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(734);
			equality_expression();
			setState(739);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,72,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(735);
					match(BITWISE_AND);
					setState(736);
					equality_expression();
					}
					} 
				}
				setState(741);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,72,_ctx);
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
		enterRule(_localctx, 134, RULE_equality_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(742);
			relational_expression();
			setState(748);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,73,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(743);
					equality_operator();
					setState(744);
					relational_expression();
					}
					} 
				}
				setState(750);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,73,_ctx);
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
		enterRule(_localctx, 136, RULE_relational_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(751);
			shift_expression();
			setState(755);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,74,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(752);
					relation_or_type_check();
					}
					} 
				}
				setState(757);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,74,_ctx);
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
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public TerminalNode IS() { return getToken(MCSharpParser.IS, 0); }
		public TerminalNode AS() { return getToken(MCSharpParser.AS, 0); }
		public Relation_or_type_checkContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_relation_or_type_check; }
	}

	public final Relation_or_type_checkContext relation_or_type_check() throws RecognitionException {
		Relation_or_type_checkContext _localctx = new Relation_or_type_checkContext(_ctx, getState());
		enterRule(_localctx, 138, RULE_relation_or_type_check);
		int _la;
		try {
			setState(763);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case LESS_THAN:
			case GREATER_THAN:
			case LESS_THAN_EQUAL:
			case GREATER_THAN_EQUAL:
				enterOuterAlt(_localctx, 1);
				{
				setState(758);
				relation_operator();
				setState(759);
				shift_expression();
				}
				break;
			case IS:
			case AS:
				enterOuterAlt(_localctx, 2);
				{
				setState(761);
				_la = _input.LA(1);
				if ( !(_la==IS || _la==AS) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(762);
				identifier();
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
		enterRule(_localctx, 140, RULE_shift_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(765);
			additive_expression();
			setState(771);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,76,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(766);
					shift_operator();
					setState(767);
					additive_expression();
					}
					} 
				}
				setState(773);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,76,_ctx);
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
		enterRule(_localctx, 142, RULE_additive_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(774);
			multiplicative_expression();
			setState(780);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,77,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(775);
					additive_operator();
					setState(776);
					multiplicative_expression();
					}
					} 
				}
				setState(782);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,77,_ctx);
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
		enterRule(_localctx, 144, RULE_multiplicative_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(783);
			with_expression();
			setState(789);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,78,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(784);
					multiplicative_operator();
					setState(785);
					with_expression();
					}
					} 
				}
				setState(791);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,78,_ctx);
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
		enterRule(_localctx, 146, RULE_with_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(792);
			range_expression();
			setState(795);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==WITH) {
				{
				setState(793);
				match(WITH);
				setState(794);
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
		enterRule(_localctx, 148, RULE_range_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(797);
			unary_expression();
			setState(801);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==RANGE_INCLUSIVE || _la==RANGE_EXCLUSIVE) {
				{
				setState(798);
				range_operator();
				setState(799);
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
		enterRule(_localctx, 150, RULE_pre_step_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(803);
			step_operator();
			setState(804);
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
		enterRule(_localctx, 152, RULE_post_step_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(808);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case STRING:
			case DECIMAL:
			case INTEGER:
			case BOOLEAN:
				{
				setState(806);
				literal();
				}
				break;
			case T__0:
			case NAME:
				{
				setState(807);
				identifier();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(810);
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
		public Pre_step_expressionContext pre_step_expression() {
			return getRuleContext(Pre_step_expressionContext.class,0);
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
		enterRule(_localctx, 154, RULE_unary_expression);
		int _la;
		try {
			setState(819);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,82,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(812);
				primary_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(813);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << PLUS) | (1L << MINUS) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(814);
				unary_expression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(815);
				pre_step_expression();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(816);
				cast_expression();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(817);
				pointer_indirection_expression();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(818);
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
		enterRule(_localctx, 156, RULE_cast_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(821);
			match(OP);
			setState(822);
			match(NAME);
			setState(823);
			match(CP);
			setState(824);
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
		enterRule(_localctx, 158, RULE_pointer_indirection_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(826);
			match(MULTIPLY);
			setState(827);
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
		enterRule(_localctx, 160, RULE_addressof_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(829);
			match(BITWISE_AND);
			setState(830);
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
		enterRule(_localctx, 162, RULE_assignment_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(832);
			unary_expression();
			setState(833);
			assignment_operator();
			setState(834);
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
		enterRule(_localctx, 164, RULE_primary_expression);
		try {
			setState(838);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,83,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(836);
				array_creation_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(837);
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
		enterRule(_localctx, 166, RULE_array_creation_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(840);
			match(NEW);
			setState(841);
			indexer_arguments();
			setState(843);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,84,_ctx) ) {
			case 1:
				{
				setState(842);
				array_rank_specifier();
				}
				break;
			}
			setState(846);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==OC) {
				{
				setState(845);
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
		enterRule(_localctx, 168, RULE_array_rank_specifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(848);
			match(OB);
			setState(852);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==COMMA) {
				{
				{
				setState(849);
				match(COMMA);
				}
				}
				setState(854);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(855);
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
		enterRule(_localctx, 170, RULE_array_initializer);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(857);
			match(OC);
			setState(869);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << OP) | (1L << OC) | (1L << PLUS) | (1L << MINUS) | (1L << MULTIPLY) | (1L << INCREMENT) | (1L << DECREMENT) | (1L << BITWISE_AND) | (1L << BITWISE_NOT) | (1L << BOOLEAN_NOT))) != 0) || ((((_la - 66)) & ~0x3f) == 0 && ((1L << (_la - 66)) & ((1L << (NEW - 66)) | (1L << (TYPEOF - 66)) | (1L << (CHECKED - 66)) | (1L << (UNCHECKED - 66)) | (1L << (DEFAULT - 66)) | (1L << (SIZEOF - 66)) | (1L << (STRING - 66)) | (1L << (DECIMAL - 66)) | (1L << (INTEGER - 66)) | (1L << (BOOLEAN - 66)) | (1L << (NAME - 66)))) != 0)) {
				{
				setState(858);
				variable_initializer();
				setState(863);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,87,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(859);
						match(COMMA);
						setState(860);
						variable_initializer();
						}
						} 
					}
					setState(865);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,87,_ctx);
				}
				setState(867);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==COMMA) {
					{
					setState(866);
					match(COMMA);
					}
				}

				}
			}

			setState(871);
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
		enterRule(_localctx, 172, RULE_variable_initializer);
		try {
			setState(875);
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
			case BOOLEAN:
			case NAME:
				enterOuterAlt(_localctx, 1);
				{
				setState(873);
				expression();
				}
				break;
			case OC:
				enterOuterAlt(_localctx, 2);
				{
				setState(874);
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
		public Short_identifierContext short_identifier() {
			return getRuleContext(Short_identifierContext.class,0);
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
		enterRule(_localctx, 174, RULE_primary_no_array_creation_expression);
		try {
			setState(886);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,91,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(877);
				literal();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(878);
				short_identifier();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(879);
				match(OP);
				setState(880);
				expression();
				setState(881);
				match(CP);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(883);
				member_access();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(884);
				post_step_expression();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(885);
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

	public static class Member_access_prefixContext extends ParserRuleContext {
		public Array_creation_expressionContext array_creation_expression() {
			return getRuleContext(Array_creation_expressionContext.class,0);
		}
		public TerminalNode DOT() { return getToken(MCSharpParser.DOT, 0); }
		public LiteralContext literal() {
			return getRuleContext(LiteralContext.class,0);
		}
		public Short_identifierContext short_identifier() {
			return getRuleContext(Short_identifierContext.class,0);
		}
		public Generic_argumentsContext generic_arguments() {
			return getRuleContext(Generic_argumentsContext.class,0);
		}
		public Method_argumentsContext method_arguments() {
			return getRuleContext(Method_argumentsContext.class,0);
		}
		public Indexer_argumentsContext indexer_arguments() {
			return getRuleContext(Indexer_argumentsContext.class,0);
		}
		public TerminalNode OP() { return getToken(MCSharpParser.OP, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public TerminalNode CP() { return getToken(MCSharpParser.CP, 0); }
		public Post_step_expressionContext post_step_expression() {
			return getRuleContext(Post_step_expressionContext.class,0);
		}
		public Keyword_expressionContext keyword_expression() {
			return getRuleContext(Keyword_expressionContext.class,0);
		}
		public Member_access_prefixContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_member_access_prefix; }
	}

	public final Member_access_prefixContext member_access_prefix() throws RecognitionException {
		Member_access_prefixContext _localctx = new Member_access_prefixContext(_ctx, getState());
		enterRule(_localctx, 176, RULE_member_access_prefix);
		int _la;
		try {
			setState(915);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,94,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(888);
				array_creation_expression();
				setState(889);
				match(DOT);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(891);
				literal();
				setState(892);
				match(DOT);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(894);
				short_identifier();
				setState(896);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==LESS_THAN) {
					{
					setState(895);
					generic_arguments();
					}
				}

				setState(900);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case OP:
					{
					setState(898);
					method_arguments();
					}
					break;
				case OB:
					{
					setState(899);
					indexer_arguments();
					}
					break;
				case DOT:
					break;
				default:
					break;
				}
				setState(902);
				match(DOT);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(904);
				match(OP);
				setState(905);
				expression();
				setState(906);
				match(CP);
				setState(907);
				match(DOT);
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(909);
				post_step_expression();
				setState(910);
				match(DOT);
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(912);
				keyword_expression();
				setState(913);
				match(DOT);
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
		public Short_identifierContext short_identifier() {
			return getRuleContext(Short_identifierContext.class,0);
		}
		public List<Member_access_prefixContext> member_access_prefix() {
			return getRuleContexts(Member_access_prefixContext.class);
		}
		public Member_access_prefixContext member_access_prefix(int i) {
			return getRuleContext(Member_access_prefixContext.class,i);
		}
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
		enterRule(_localctx, 178, RULE_member_access);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(920);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,95,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(917);
					member_access_prefix();
					}
					} 
				}
				setState(922);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,95,_ctx);
			}
			setState(923);
			short_identifier();
			setState(925);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,96,_ctx) ) {
			case 1:
				{
				setState(924);
				generic_arguments();
				}
				break;
			}
			setState(929);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,97,_ctx) ) {
			case 1:
				{
				setState(927);
				method_arguments();
				}
				break;
			case 2:
				{
				setState(928);
				indexer_arguments();
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
		enterRule(_localctx, 180, RULE_keyword_expression);
		try {
			setState(937);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case NEW:
				enterOuterAlt(_localctx, 1);
				{
				setState(931);
				new_keyword_expression();
				}
				break;
			case TYPEOF:
				enterOuterAlt(_localctx, 2);
				{
				setState(932);
				typeof_keyword_expression();
				}
				break;
			case CHECKED:
				enterOuterAlt(_localctx, 3);
				{
				setState(933);
				checked_expression();
				}
				break;
			case UNCHECKED:
				enterOuterAlt(_localctx, 4);
				{
				setState(934);
				unchecked_expression();
				}
				break;
			case DEFAULT:
				enterOuterAlt(_localctx, 5);
				{
				setState(935);
				default_keyword_expression();
				}
				break;
			case SIZEOF:
				enterOuterAlt(_localctx, 6);
				{
				setState(936);
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
		enterRule(_localctx, 182, RULE_object_or_collection_initializer);
		try {
			setState(941);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,99,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(939);
				object_initializer();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(940);
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
		enterRule(_localctx, 184, RULE_new_keyword_expression);
		int _la;
		try {
			setState(954);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,102,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(943);
				match(NEW);
				setState(944);
				match(NAME);
				setState(950);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case OP:
					{
					{
					setState(945);
					method_arguments();
					setState(947);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (_la==OC) {
						{
						setState(946);
						object_or_collection_initializer();
						}
					}

					}
					}
					break;
				case OC:
					{
					{
					setState(949);
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
				setState(952);
				match(NEW);
				setState(953);
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
		enterRule(_localctx, 186, RULE_typeof_keyword_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(956);
			match(TYPEOF);
			setState(957);
			match(OP);
			{
			setState(958);
			match(NAME);
			}
			setState(959);
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
		enterRule(_localctx, 188, RULE_checked_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(961);
			match(CHECKED);
			setState(962);
			match(OP);
			setState(963);
			expression();
			setState(964);
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
		enterRule(_localctx, 190, RULE_unchecked_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(966);
			match(UNCHECKED);
			setState(967);
			match(OP);
			setState(968);
			expression();
			setState(969);
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
		enterRule(_localctx, 192, RULE_default_keyword_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(971);
			match(DEFAULT);
			setState(975);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==OP) {
				{
				setState(972);
				match(OP);
				setState(973);
				match(NAME);
				setState(974);
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
		enterRule(_localctx, 194, RULE_delegate_keyword_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(977);
			match(DELEGATE);
			setState(978);
			method_parameters();
			setState(979);
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
		enterRule(_localctx, 196, RULE_sizeof_keyword_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(981);
			match(SIZEOF);
			setState(982);
			match(OP);
			setState(983);
			match(NAME);
			setState(984);
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3`\u03dd\4\2\t\2\4"+
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
		"`\t`\4a\ta\4b\tb\4c\tc\4d\td\3\2\7\2\u00ca\n\2\f\2\16\2\u00cd\13\2\3\2"+
		"\3\2\3\3\3\3\3\4\3\4\3\4\7\4\u00d6\n\4\f\4\16\4\u00d9\13\4\3\5\3\5\3\5"+
		"\3\5\3\6\5\6\u00e0\n\6\3\6\3\6\3\6\3\7\3\7\3\7\7\7\u00e8\n\7\f\7\16\7"+
		"\u00eb\13\7\3\b\3\b\5\b\u00ef\n\b\3\b\3\b\3\t\3\t\5\t\u00f5\n\t\3\t\3"+
		"\t\3\n\3\n\3\n\3\n\3\n\5\n\u00fe\n\n\3\13\3\13\3\13\7\13\u0103\n\13\f"+
		"\13\16\13\u0106\13\13\3\f\3\f\5\f\u010a\n\f\3\f\3\f\3\r\3\r\5\r\u0110"+
		"\n\r\3\r\3\r\3\16\3\16\5\16\u0116\n\16\3\16\3\16\3\17\3\17\5\17\u011c"+
		"\n\17\3\17\3\17\3\17\5\17\u0121\n\17\3\20\3\20\3\20\3\20\5\20\u0127\n"+
		"\20\3\20\5\20\u012a\n\20\5\20\u012c\n\20\3\20\3\20\3\21\3\21\3\21\3\21"+
		"\3\21\5\21\u0135\n\21\3\22\3\22\3\22\3\22\5\22\u013b\n\22\3\22\5\22\u013e"+
		"\n\22\5\22\u0140\n\22\3\22\3\22\3\23\3\23\5\23\u0146\n\23\3\23\3\23\3"+
		"\23\3\23\3\23\5\23\u014d\n\23\3\24\3\24\3\24\3\24\5\24\u0153\n\24\3\24"+
		"\5\24\u0156\n\24\5\24\u0158\n\24\3\24\3\24\3\25\3\25\3\26\3\26\3\27\3"+
		"\27\3\30\3\30\3\30\3\30\3\30\3\30\5\30\u0168\n\30\3\30\3\30\3\31\7\31"+
		"\u016d\n\31\f\31\16\31\u0170\13\31\3\31\7\31\u0173\n\31\f\31\16\31\u0176"+
		"\13\31\3\31\3\31\3\31\3\31\3\31\7\31\u017d\n\31\f\31\16\31\u0180\13\31"+
		"\3\31\3\31\3\32\7\32\u0185\n\32\f\32\16\32\u0188\13\32\3\32\7\32\u018b"+
		"\n\32\f\32\16\32\u018e\13\32\3\32\3\32\3\32\3\32\3\32\5\32\u0195\n\32"+
		"\3\33\7\33\u0198\n\33\f\33\16\33\u019b\13\33\3\33\7\33\u019e\n\33\f\33"+
		"\16\33\u01a1\13\33\3\33\3\33\3\33\3\33\3\33\5\33\u01a8\n\33\3\34\3\34"+
		"\5\34\u01ac\n\34\3\34\3\34\3\35\3\35\3\35\3\35\7\35\u01b4\n\35\f\35\16"+
		"\35\u01b7\13\35\3\35\7\35\u01ba\n\35\f\35\16\35\u01bd\13\35\3\35\3\35"+
		"\3\35\7\35\u01c2\n\35\f\35\16\35\u01c5\13\35\3\35\7\35\u01c8\n\35\f\35"+
		"\16\35\u01cb\13\35\3\35\5\35\u01ce\n\35\3\35\7\35\u01d1\n\35\f\35\16\35"+
		"\u01d4\13\35\3\35\7\35\u01d7\n\35\f\35\16\35\u01da\13\35\3\35\3\35\3\35"+
		"\7\35\u01df\n\35\f\35\16\35\u01e2\13\35\3\35\7\35\u01e5\n\35\f\35\16\35"+
		"\u01e8\13\35\3\35\5\35\u01eb\n\35\3\35\3\35\5\35\u01ef\n\35\3\36\3\36"+
		"\3\36\3\36\3\36\3\36\3\36\3\36\3\36\5\36\u01fa\n\36\3\37\3\37\3\37\3\37"+
		"\3\37\3\37\3\37\3\37\3\37\5\37\u0205\n\37\3 \5 \u0208\n \3 \3 \3 \3 \3"+
		" \3 \5 \u0210\n \3!\3!\3\"\5\"\u0215\n\"\3\"\3\"\3\"\7\"\u021a\n\"\f\""+
		"\16\"\u021d\13\"\3#\5#\u0220\n#\3#\3#\3$\3$\3$\3$\3$\3$\3$\3$\5$\u022c"+
		"\n$\3%\3%\7%\u0230\n%\f%\16%\u0233\13%\3%\3%\3&\3&\3\'\3\'\3(\3(\3)\3"+
		")\3*\3*\3+\3+\3,\3,\3-\3-\3.\3.\3/\3/\3\60\3\60\3\60\3\60\3\60\3\60\3"+
		"\60\3\60\5\60\u0253\n\60\3\61\3\61\3\61\3\61\3\61\3\61\3\61\5\61\u025c"+
		"\n\61\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\62\3\63\3\63\3\63"+
		"\3\63\3\63\3\63\3\63\3\63\3\63\3\64\3\64\3\64\3\64\3\64\3\64\3\65\3\65"+
		"\3\65\3\65\3\65\3\65\3\65\3\65\3\66\3\66\5\66\u0281\n\66\3\66\3\66\3\67"+
		"\3\67\3\67\3\67\38\38\38\38\38\38\38\38\78\u0291\n8\f8\168\u0294\138\3"+
		"8\38\58\u0298\n8\39\39\59\u029c\n9\3:\3:\3:\3:\5:\u02a2\n:\3;\3;\5;\u02a6"+
		"\n;\3<\3<\3<\3<\3=\3=\3=\7=\u02af\n=\f=\16=\u02b2\13=\3>\3>\3>\3>\3>\3"+
		">\5>\u02ba\n>\3?\3?\3?\5?\u02bf\n?\3@\3@\3@\7@\u02c4\n@\f@\16@\u02c7\13"+
		"@\3A\3A\3A\7A\u02cc\nA\fA\16A\u02cf\13A\3B\3B\3B\7B\u02d4\nB\fB\16B\u02d7"+
		"\13B\3C\3C\3C\7C\u02dc\nC\fC\16C\u02df\13C\3D\3D\3D\7D\u02e4\nD\fD\16"+
		"D\u02e7\13D\3E\3E\3E\3E\7E\u02ed\nE\fE\16E\u02f0\13E\3F\3F\7F\u02f4\n"+
		"F\fF\16F\u02f7\13F\3G\3G\3G\3G\3G\5G\u02fe\nG\3H\3H\3H\3H\7H\u0304\nH"+
		"\fH\16H\u0307\13H\3I\3I\3I\3I\7I\u030d\nI\fI\16I\u0310\13I\3J\3J\3J\3"+
		"J\7J\u0316\nJ\fJ\16J\u0319\13J\3K\3K\3K\5K\u031e\nK\3L\3L\3L\3L\5L\u0324"+
		"\nL\3M\3M\3M\3N\3N\5N\u032b\nN\3N\3N\3O\3O\3O\3O\3O\3O\3O\5O\u0336\nO"+
		"\3P\3P\3P\3P\3P\3Q\3Q\3Q\3R\3R\3R\3S\3S\3S\3S\3T\3T\5T\u0349\nT\3U\3U"+
		"\3U\5U\u034e\nU\3U\5U\u0351\nU\3V\3V\7V\u0355\nV\fV\16V\u0358\13V\3V\3"+
		"V\3W\3W\3W\3W\7W\u0360\nW\fW\16W\u0363\13W\3W\5W\u0366\nW\5W\u0368\nW"+
		"\3W\3W\3X\3X\5X\u036e\nX\3Y\3Y\3Y\3Y\3Y\3Y\3Y\3Y\3Y\5Y\u0379\nY\3Z\3Z"+
		"\3Z\3Z\3Z\3Z\3Z\3Z\5Z\u0383\nZ\3Z\3Z\5Z\u0387\nZ\3Z\3Z\3Z\3Z\3Z\3Z\3Z"+
		"\3Z\3Z\3Z\3Z\3Z\3Z\5Z\u0396\nZ\3[\7[\u0399\n[\f[\16[\u039c\13[\3[\3[\5"+
		"[\u03a0\n[\3[\3[\5[\u03a4\n[\3\\\3\\\3\\\3\\\3\\\3\\\5\\\u03ac\n\\\3]"+
		"\3]\5]\u03b0\n]\3^\3^\3^\3^\5^\u03b6\n^\3^\5^\u03b9\n^\3^\3^\5^\u03bd"+
		"\n^\3_\3_\3_\3_\3_\3`\3`\3`\3`\3`\3a\3a\3a\3a\3a\3b\3b\3b\3b\5b\u03d2"+
		"\nb\3c\3c\3c\3c\3d\3d\3d\3d\3d\3d\2\2e\2\4\6\b\n\f\16\20\22\24\26\30\32"+
		"\34\36 \"$&(*,.\60\62\64\668:<>@BDFHJLNPRTVXZ\\^`bdfhjlnprtvxz|~\u0080"+
		"\u0082\u0084\u0086\u0088\u008a\u008c\u008e\u0090\u0092\u0094\u0096\u0098"+
		"\u009a\u009c\u009e\u00a0\u00a2\u00a4\u00a6\u00a8\u00aa\u00ac\u00ae\u00b0"+
		"\u00b2\u00b4\u00b6\u00b8\u00ba\u00bc\u00be\u00c0\u00c2\u00c4\u00c6\2\22"+
		"\3\2NS\4\2\65\66UU\3\2VW\3\2\\_\3\2\f\r\3\2\16\20\3\2\21\22\3\2\23\25"+
		"\3\2\27\30\3\2\32\33\3\2\34\35\3\2\36!\3\2#.\3\2\61\62\3\2\63\64\5\2\f"+
		"\r\26\26\31\31\2\u0400\2\u00cb\3\2\2\2\4\u00d0\3\2\2\2\6\u00d2\3\2\2\2"+
		"\b\u00da\3\2\2\2\n\u00df\3\2\2\2\f\u00e4\3\2\2\2\16\u00ec\3\2\2\2\20\u00f2"+
		"\3\2\2\2\22\u00fd\3\2\2\2\24\u00ff\3\2\2\2\26\u0107\3\2\2\2\30\u010d\3"+
		"\2\2\2\32\u0113\3\2\2\2\34\u0119\3\2\2\2\36\u0122\3\2\2\2 \u0134\3\2\2"+
		"\2\"\u0136\3\2\2\2$\u014c\3\2\2\2&\u014e\3\2\2\2(\u015b\3\2\2\2*\u015d"+
		"\3\2\2\2,\u015f\3\2\2\2.\u0161\3\2\2\2\60\u016e\3\2\2\2\62\u0186\3\2\2"+
		"\2\64\u0199\3\2\2\2\66\u01ab\3\2\2\28\u01ee\3\2\2\2:\u01f9\3\2\2\2<\u0204"+
		"\3\2\2\2>\u0207\3\2\2\2@\u0211\3\2\2\2B\u0214\3\2\2\2D\u021f\3\2\2\2F"+
		"\u022b\3\2\2\2H\u022d\3\2\2\2J\u0236\3\2\2\2L\u0238\3\2\2\2N\u023a\3\2"+
		"\2\2P\u023c\3\2\2\2R\u023e\3\2\2\2T\u0240\3\2\2\2V\u0242\3\2\2\2X\u0244"+
		"\3\2\2\2Z\u0246\3\2\2\2\\\u0248\3\2\2\2^\u0252\3\2\2\2`\u0254\3\2\2\2"+
		"b\u025d\3\2\2\2d\u0267\3\2\2\2f\u0270\3\2\2\2h\u0276\3\2\2\2j\u027e\3"+
		"\2\2\2l\u0284\3\2\2\2n\u0288\3\2\2\2p\u029b\3\2\2\2r\u029d\3\2\2\2t\u02a5"+
		"\3\2\2\2v\u02a7\3\2\2\2x\u02ab\3\2\2\2z\u02b3\3\2\2\2|\u02bb\3\2\2\2~"+
		"\u02c0\3\2\2\2\u0080\u02c8\3\2\2\2\u0082\u02d0\3\2\2\2\u0084\u02d8\3\2"+
		"\2\2\u0086\u02e0\3\2\2\2\u0088\u02e8\3\2\2\2\u008a\u02f1\3\2\2\2\u008c"+
		"\u02fd\3\2\2\2\u008e\u02ff\3\2\2\2\u0090\u0308\3\2\2\2\u0092\u0311\3\2"+
		"\2\2\u0094\u031a\3\2\2\2\u0096\u031f\3\2\2\2\u0098\u0325\3\2\2\2\u009a"+
		"\u032a\3\2\2\2\u009c\u0335\3\2\2\2\u009e\u0337\3\2\2\2\u00a0\u033c\3\2"+
		"\2\2\u00a2\u033f\3\2\2\2\u00a4\u0342\3\2\2\2\u00a6\u0348\3\2\2\2\u00a8"+
		"\u034a\3\2\2\2\u00aa\u0352\3\2\2\2\u00ac\u035b\3\2\2\2\u00ae\u036d\3\2"+
		"\2\2\u00b0\u0378\3\2\2\2\u00b2\u0395\3\2\2\2\u00b4\u039a\3\2\2\2\u00b6"+
		"\u03ab\3\2\2\2\u00b8\u03af\3\2\2\2\u00ba\u03bc\3\2\2\2\u00bc\u03be\3\2"+
		"\2\2\u00be\u03c3\3\2\2\2\u00c0\u03c8\3\2\2\2\u00c2\u03cd\3\2\2\2\u00c4"+
		"\u03d3\3\2\2\2\u00c6\u03d7\3\2\2\2\u00c8\u00ca\5\60\31\2\u00c9\u00c8\3"+
		"\2\2\2\u00ca\u00cd\3\2\2\2\u00cb\u00c9\3\2\2\2\u00cb\u00cc\3\2\2\2\u00cc"+
		"\u00ce\3\2\2\2\u00cd\u00cb\3\2\2\2\u00ce\u00cf\7\2\2\3\u00cf\3\3\2\2\2"+
		"\u00d0\u00d1\7`\2\2\u00d1\5\3\2\2\2\u00d2\u00d7\5\4\3\2\u00d3\u00d4\7"+
		"\5\2\2\u00d4\u00d6\5\4\3\2\u00d5\u00d3\3\2\2\2\u00d6\u00d9\3\2\2\2\u00d7"+
		"\u00d5\3\2\2\2\u00d7\u00d8\3\2\2\2\u00d8\7\3\2\2\2\u00d9\u00d7\3\2\2\2"+
		"\u00da\u00db\7\36\2\2\u00db\u00dc\5\6\4\2\u00dc\u00dd\7\37\2\2\u00dd\t"+
		"\3\2\2\2\u00de\u00e0\5*\26\2\u00df\u00de\3\2\2\2\u00df\u00e0\3\2\2\2\u00e0"+
		"\u00e1\3\2\2\2\u00e1\u00e2\7`\2\2\u00e2\u00e3\7`\2\2\u00e3\13\3\2\2\2"+
		"\u00e4\u00e9\5\n\6\2\u00e5\u00e6\7\5\2\2\u00e6\u00e8\5\n\6\2\u00e7\u00e5"+
		"\3\2\2\2\u00e8\u00eb\3\2\2\2\u00e9\u00e7\3\2\2\2\u00e9\u00ea\3\2\2\2\u00ea"+
		"\r\3\2\2\2\u00eb\u00e9\3\2\2\2\u00ec\u00ee\7\6\2\2\u00ed\u00ef\5\f\7\2"+
		"\u00ee\u00ed\3\2\2\2\u00ee\u00ef\3\2\2\2\u00ef\u00f0\3\2\2\2\u00f0\u00f1"+
		"\7\7\2\2\u00f1\17\3\2\2\2\u00f2\u00f4\7\b\2\2\u00f3\u00f5\5\f\7\2\u00f4"+
		"\u00f3\3\2\2\2\u00f4\u00f5\3\2\2\2\u00f5\u00f6\3\2\2\2\u00f6\u00f7\7\t"+
		"\2\2\u00f7\21\3\2\2\2\u00f8\u00fe\5p9\2\u00f9\u00fa\5*\26\2\u00fa\u00fb"+
		"\7`\2\2\u00fb\u00fc\7`\2\2\u00fc\u00fe\3\2\2\2\u00fd\u00f8\3\2\2\2\u00fd"+
		"\u00f9\3\2\2\2\u00fe\23\3\2\2\2\u00ff\u0104\5\22\n\2\u0100\u0101\7\5\2"+
		"\2\u0101\u0103\5\22\n\2\u0102\u0100\3\2\2\2\u0103\u0106\3\2\2\2\u0104"+
		"\u0102\3\2\2\2\u0104\u0105\3\2\2\2\u0105\25\3\2\2\2\u0106\u0104\3\2\2"+
		"\2\u0107\u0109\7\36\2\2\u0108\u010a\5\6\4\2\u0109\u0108\3\2\2\2\u0109"+
		"\u010a\3\2\2\2\u010a\u010b\3\2\2\2\u010b\u010c\7\37\2\2\u010c\27\3\2\2"+
		"\2\u010d\u010f\7\6\2\2\u010e\u0110\5\24\13\2\u010f\u010e\3\2\2\2\u010f"+
		"\u0110\3\2\2\2\u0110\u0111\3\2\2\2\u0111\u0112\7\7\2\2\u0112\31\3\2\2"+
		"\2\u0113\u0115\7\b\2\2\u0114\u0116\5\24\13\2\u0115\u0114\3\2\2\2\u0115"+
		"\u0116\3\2\2\2\u0116\u0117\3\2\2\2\u0117\u0118\7\t\2\2\u0118\33\3\2\2"+
		"\2\u0119\u011b\7`\2\2\u011a\u011c\5\26\f\2\u011b\u011a\3\2\2\2\u011b\u011c"+
		"\3\2\2\2\u011c\u011d\3\2\2\2\u011d\u0120\7#\2\2\u011e\u0121\5p9\2\u011f"+
		"\u0121\5\u00b8]\2\u0120\u011e\3\2\2\2\u0120\u011f\3\2\2\2\u0121\35\3\2"+
		"\2\2\u0122\u012b\7\n\2\2\u0123\u0126\5\34\17\2\u0124\u0125\7\5\2\2\u0125"+
		"\u0127\5\34\17\2\u0126\u0124\3\2\2\2\u0126\u0127\3\2\2\2\u0127\u0129\3"+
		"\2\2\2\u0128\u012a\7\5\2\2\u0129\u0128\3\2\2\2\u0129\u012a\3\2\2\2\u012a"+
		"\u012c\3\2\2\2\u012b\u0123\3\2\2\2\u012b\u012c\3\2\2\2\u012c\u012d\3\2"+
		"\2\2\u012d\u012e\7\13\2\2\u012e\37\3\2\2\2\u012f\u0135\5t;\2\u0130\u0131"+
		"\7\n\2\2\u0131\u0132\5p9\2\u0132\u0133\7\13\2\2\u0133\u0135\3\2\2\2\u0134"+
		"\u012f\3\2\2\2\u0134\u0130\3\2\2\2\u0135!\3\2\2\2\u0136\u013f\7\n\2\2"+
		"\u0137\u013a\5 \21\2\u0138\u0139\7\5\2\2\u0139\u013b\5 \21\2\u013a\u0138"+
		"\3\2\2\2\u013a\u013b\3\2\2\2\u013b\u013d\3\2\2\2\u013c\u013e\7\5\2\2\u013d"+
		"\u013c\3\2\2\2\u013d\u013e\3\2\2\2\u013e\u0140\3\2\2\2\u013f\u0137\3\2"+
		"\2\2\u013f\u0140\3\2\2\2\u0140\u0141\3\2\2\2\u0141\u0142\7\13\2\2\u0142"+
		"#\3\2\2\2\u0143\u0145\7`\2\2\u0144\u0146\5\26\f\2\u0145\u0144\3\2\2\2"+
		"\u0145\u0146\3\2\2\2\u0146\u014d\3\2\2\2\u0147\u014d\5\u00b4[\2\u0148"+
		"\u0149\5B\"\2\u0149\u014a\7#\2\2\u014a\u014b\5p9\2\u014b\u014d\3\2\2\2"+
		"\u014c\u0143\3\2\2\2\u014c\u0147\3\2\2\2\u014c\u0148\3\2\2\2\u014d%\3"+
		"\2\2\2\u014e\u0157\7\n\2\2\u014f\u0152\5$\23\2\u0150\u0151\7\5\2\2\u0151"+
		"\u0153\5$\23\2\u0152\u0150\3\2\2\2\u0152\u0153\3\2\2\2\u0153\u0155\3\2"+
		"\2\2\u0154\u0156\7\5\2\2\u0155\u0154\3\2\2\2\u0155\u0156\3\2\2\2\u0156"+
		"\u0158\3\2\2\2\u0157\u014f\3\2\2\2\u0157\u0158\3\2\2\2\u0158\u0159\3\2"+
		"\2\2\u0159\u015a\7\13\2\2\u015a\'\3\2\2\2\u015b\u015c\t\2\2\2\u015c)\3"+
		"\2\2\2\u015d\u015e\t\3\2\2\u015e+\3\2\2\2\u015f\u0160\t\4\2\2\u0160-\3"+
		"\2\2\2\u0161\u0162\7\b\2\2\u0162\u0167\7`\2\2\u0163\u0164\7\6\2\2\u0164"+
		"\u0165\5\16\b\2\u0165\u0166\7\7\2\2\u0166\u0168\3\2\2\2\u0167\u0163\3"+
		"\2\2\2\u0167\u0168\3\2\2\2\u0168\u0169\3\2\2\2\u0169\u016a\7\t\2\2\u016a"+
		"/\3\2\2\2\u016b\u016d\5.\30\2\u016c\u016b\3\2\2\2\u016d\u0170\3\2\2\2"+
		"\u016e\u016c\3\2\2\2\u016e\u016f\3\2\2\2\u016f\u0174\3\2\2\2\u0170\u016e"+
		"\3\2\2\2\u0171\u0173\5(\25\2\u0172\u0171\3\2\2\2\u0173\u0176\3\2\2\2\u0174"+
		"\u0172\3\2\2\2\u0174\u0175\3\2\2\2\u0175\u0177\3\2\2\2\u0176\u0174\3\2"+
		"\2\2\u0177\u0178\5,\27\2\u0178\u0179\7`\2\2\u0179\u017e\7\n\2\2\u017a"+
		"\u017d\5\64\33\2\u017b\u017d\5\62\32\2\u017c\u017a\3\2\2\2\u017c\u017b"+
		"\3\2\2\2\u017d\u0180\3\2\2\2\u017e\u017c\3\2\2\2\u017e\u017f\3\2\2\2\u017f"+
		"\u0181\3\2\2\2\u0180\u017e\3\2\2\2\u0181\u0182\7\13\2\2\u0182\61\3\2\2"+
		"\2\u0183\u0185\5.\30\2\u0184\u0183\3\2\2\2\u0185\u0188\3\2\2\2\u0186\u0184"+
		"\3\2\2\2\u0186\u0187\3\2\2\2\u0187\u018c\3\2\2\2\u0188\u0186\3\2\2\2\u0189"+
		"\u018b\5(\25\2\u018a\u0189\3\2\2\2\u018b\u018e\3\2\2\2\u018c\u018a\3\2"+
		"\2\2\u018c\u018d\3\2\2\2\u018d\u018f\3\2\2\2\u018e\u018c\3\2\2\2\u018f"+
		"\u0190\7`\2\2\u0190\u0194\7`\2\2\u0191\u0195\5\66\34\2\u0192\u0195\58"+
		"\35\2\u0193\u0195\5> \2\u0194\u0191\3\2\2\2\u0194\u0192\3\2\2\2\u0194"+
		"\u0193\3\2\2\2\u0195\63\3\2\2\2\u0196\u0198\5.\30\2\u0197\u0196\3\2\2"+
		"\2\u0198\u019b\3\2\2\2\u0199\u0197\3\2\2\2\u0199\u019a\3\2\2\2\u019a\u019f"+
		"\3\2\2\2\u019b\u0199\3\2\2\2\u019c\u019e\5(\25\2\u019d\u019c\3\2\2\2\u019e"+
		"\u01a1\3\2\2\2\u019f\u019d\3\2\2\2\u019f\u01a0\3\2\2\2\u01a0\u01a2\3\2"+
		"\2\2\u01a1\u019f\3\2\2\2\u01a2\u01a3\7`\2\2\u01a3\u01a7\5\16\b\2\u01a4"+
		"\u01a8\5H%\2\u01a5\u01a6\7\67\2\2\u01a6\u01a8\5p9\2\u01a7\u01a4\3\2\2"+
		"\2\u01a7\u01a5\3\2\2\2\u01a8\65\3\2\2\2\u01a9\u01aa\7#\2\2\u01aa\u01ac"+
		"\5p9\2\u01ab\u01a9\3\2\2\2\u01ab\u01ac\3\2\2\2\u01ac\u01ad\3\2\2\2\u01ad"+
		"\u01ae\7\4\2\2\u01ae\67\3\2\2\2\u01af\u01b0\7\67\2\2\u01b0\u01ef\5p9\2"+
		"\u01b1\u01b5\7\n\2\2\u01b2\u01b4\5.\30\2\u01b3\u01b2\3\2\2\2\u01b4\u01b7"+
		"\3\2\2\2\u01b5\u01b3\3\2\2\2\u01b5\u01b6\3\2\2\2\u01b6\u01bb\3\2\2\2\u01b7"+
		"\u01b5\3\2\2\2\u01b8\u01ba\5(\25\2\u01b9\u01b8\3\2\2\2\u01ba\u01bd\3\2"+
		"\2\2\u01bb\u01b9\3\2\2\2\u01bb\u01bc\3\2\2\2\u01bc\u01be\3\2\2\2\u01bd"+
		"\u01bb\3\2\2\2\u01be\u01bf\5:\36\2\u01bf\u01cd\3\2\2\2\u01c0\u01c2\5."+
		"\30\2\u01c1\u01c0\3\2\2\2\u01c2\u01c5\3\2\2\2\u01c3\u01c1\3\2\2\2\u01c3"+
		"\u01c4\3\2\2\2\u01c4\u01c9\3\2\2\2\u01c5\u01c3\3\2\2\2\u01c6\u01c8\5("+
		"\25\2\u01c7\u01c6\3\2\2\2\u01c8\u01cb\3\2\2\2\u01c9\u01c7\3\2\2\2\u01c9"+
		"\u01ca\3\2\2\2\u01ca\u01cc\3\2\2\2\u01cb\u01c9\3\2\2\2\u01cc\u01ce\5<"+
		"\37\2\u01cd\u01c3\3\2\2\2\u01cd\u01ce\3\2\2\2\u01ce\u01ef\3\2\2\2\u01cf"+
		"\u01d1\5.\30\2\u01d0\u01cf\3\2\2\2\u01d1\u01d4\3\2\2\2\u01d2\u01d0\3\2"+
		"\2\2\u01d2\u01d3\3\2\2\2\u01d3\u01d8\3\2\2\2\u01d4\u01d2\3\2\2\2\u01d5"+
		"\u01d7\5(\25\2\u01d6\u01d5\3\2\2\2\u01d7\u01da\3\2\2\2\u01d8\u01d6\3\2"+
		"\2\2\u01d8\u01d9\3\2\2\2\u01d9\u01db\3\2\2\2\u01da\u01d8\3\2\2\2\u01db"+
		"\u01dc\5<\37\2\u01dc\u01ea\3\2\2\2\u01dd\u01df\5.\30\2\u01de\u01dd\3\2"+
		"\2\2\u01df\u01e2\3\2\2\2\u01e0\u01de\3\2\2\2\u01e0\u01e1\3\2\2\2\u01e1"+
		"\u01e6\3\2\2\2\u01e2\u01e0\3\2\2\2\u01e3\u01e5\5(\25\2\u01e4\u01e3\3\2"+
		"\2\2\u01e5\u01e8\3\2\2\2\u01e6\u01e4\3\2\2\2\u01e6\u01e7\3\2\2\2\u01e7"+
		"\u01e9\3\2\2\2\u01e8\u01e6\3\2\2\2\u01e9\u01eb\5:\36\2\u01ea\u01e0\3\2"+
		"\2\2\u01ea\u01eb\3\2\2\2\u01eb\u01ec\3\2\2\2\u01ec\u01ed\7\13\2\2\u01ed"+
		"\u01ef\3\2\2\2\u01ee\u01af\3\2\2\2\u01ee\u01b1\3\2\2\2\u01ee\u01d2\3\2"+
		"\2\2\u01ef9\3\2\2\2\u01f0\u01f1\7L\2\2\u01f1\u01fa\7\4\2\2\u01f2\u01f3"+
		"\7L\2\2\u01f3\u01f4\7\67\2\2\u01f4\u01f5\5p9\2\u01f5\u01f6\7\4\2\2\u01f6"+
		"\u01fa\3\2\2\2\u01f7\u01f8\7L\2\2\u01f8\u01fa\5H%\2\u01f9\u01f0\3\2\2"+
		"\2\u01f9\u01f2\3\2\2\2\u01f9\u01f7\3\2\2\2\u01fa;\3\2\2\2\u01fb\u01fc"+
		"\7M\2\2\u01fc\u0205\7\4\2\2\u01fd\u01fe\7M\2\2\u01fe\u01ff\7\67\2\2\u01ff"+
		"\u0200\5p9\2\u0200\u0201\7\4\2\2\u0201\u0205\3\2\2\2\u0202\u0203\7M\2"+
		"\2\u0203\u0205\5H%\2\u0204\u01fb\3\2\2\2\u0204\u01fd\3\2\2\2\u0204\u0202"+
		"\3\2\2\2\u0205=\3\2\2\2\u0206\u0208\5\b\5\2\u0207\u0206\3\2\2\2\u0207"+
		"\u0208\3\2\2\2\u0208\u0209\3\2\2\2\u0209\u020f\5\16\b\2\u020a\u020b\7"+
		"\67\2\2\u020b\u020c\5p9\2\u020c\u020d\7\4\2\2\u020d\u0210\3\2\2\2\u020e"+
		"\u0210\5H%\2\u020f\u020a\3\2\2\2\u020f\u020e\3\2\2\2\u0210?\3\2\2\2\u0211"+
		"\u0212\t\5\2\2\u0212A\3\2\2\2\u0213\u0215\7\3\2\2\u0214\u0213\3\2\2\2"+
		"\u0214\u0215\3\2\2\2\u0215\u0216\3\2\2\2\u0216\u021b\7`\2\2\u0217\u0218"+
		"\7\"\2\2\u0218\u021a\7`\2\2\u0219\u0217\3\2\2\2\u021a\u021d\3\2\2\2\u021b"+
		"\u0219\3\2\2\2\u021b\u021c\3\2\2\2\u021cC\3\2\2\2\u021d\u021b\3\2\2\2"+
		"\u021e\u0220\7\3\2\2\u021f\u021e\3\2\2\2\u021f\u0220\3\2\2\2\u0220\u0221"+
		"\3\2\2\2\u0221\u0222\7`\2\2\u0222E\3\2\2\2\u0223\u022c\5H%\2\u0224\u022c"+
		"\5^\60\2\u0225\u0226\5r:\2\u0226\u0227\7\4\2\2\u0227\u022c\3\2\2\2\u0228"+
		"\u0229\5p9\2\u0229\u022a\7\4\2\2\u022a\u022c\3\2\2\2\u022b\u0223\3\2\2"+
		"\2\u022b\u0224\3\2\2\2\u022b\u0225\3\2\2\2\u022b\u0228\3\2\2\2\u022cG"+
		"\3\2\2\2\u022d\u0231\7\n\2\2\u022e\u0230\5F$\2\u022f\u022e\3\2\2\2\u0230"+
		"\u0233\3\2\2\2\u0231\u022f\3\2\2\2\u0231\u0232\3\2\2\2\u0232\u0234\3\2"+
		"\2\2\u0233\u0231\3\2\2\2\u0234\u0235\7\13\2\2\u0235I\3\2\2\2\u0236\u0237"+
		"\t\6\2\2\u0237K\3\2\2\2\u0238\u0239\t\7\2\2\u0239M\3\2\2\2\u023a\u023b"+
		"\t\b\2\2\u023bO\3\2\2\2\u023c\u023d\t\t\2\2\u023dQ\3\2\2\2\u023e\u023f"+
		"\t\n\2\2\u023fS\3\2\2\2\u0240\u0241\t\13\2\2\u0241U\3\2\2\2\u0242\u0243"+
		"\t\f\2\2\u0243W\3\2\2\2\u0244\u0245\t\r\2\2\u0245Y\3\2\2\2\u0246\u0247"+
		"\t\16\2\2\u0247[\3\2\2\2\u0248\u0249\t\17\2\2\u0249]\3\2\2\2\u024a\u0253"+
		"\5`\61\2\u024b\u0253\5b\62\2\u024c\u0253\5d\63\2\u024d\u0253\5f\64\2\u024e"+
		"\u0253\5h\65\2\u024f\u0253\5j\66\2\u0250\u0253\5l\67\2\u0251\u0253\5n"+
		"8\2\u0252\u024a\3\2\2\2\u0252\u024b\3\2\2\2\u0252\u024c\3\2\2\2\u0252"+
		"\u024d\3\2\2\2\u0252\u024e\3\2\2\2\u0252\u024f\3\2\2\2\u0252\u0250\3\2"+
		"\2\2\u0252\u0251\3\2\2\2\u0253_\3\2\2\2\u0254\u0255\79\2\2\u0255\u0256"+
		"\7\6\2\2\u0256\u0257\5p9\2\u0257\u0258\7\7\2\2\u0258\u025b\5F$\2\u0259"+
		"\u025a\7:\2\2\u025a\u025c\5F$\2\u025b\u0259\3\2\2\2\u025b\u025c\3\2\2"+
		"\2\u025ca\3\2\2\2\u025d\u025e\7;\2\2\u025e\u025f\7\6\2\2\u025f\u0260\5"+
		"r:\2\u0260\u0261\7\4\2\2\u0261\u0262\5p9\2\u0262\u0263\7\4\2\2\u0263\u0264"+
		"\5p9\2\u0264\u0265\7\7\2\2\u0265\u0266\5F$\2\u0266c\3\2\2\2\u0267\u0268"+
		"\7<\2\2\u0268\u0269\7\6\2\2\u0269\u026a\7`\2\2\u026a\u026b\7`\2\2\u026b"+
		"\u026c\7\65\2\2\u026c\u026d\5p9\2\u026d\u026e\7\7\2\2\u026e\u026f\5F$"+
		"\2\u026fe\3\2\2\2\u0270\u0271\7>\2\2\u0271\u0272\7\6\2\2\u0272\u0273\5"+
		"p9\2\u0273\u0274\7\7\2\2\u0274\u0275\5F$\2\u0275g\3\2\2\2\u0276\u0277"+
		"\7=\2\2\u0277\u0278\5F$\2\u0278\u0279\7>\2\2\u0279\u027a\7\6\2\2\u027a"+
		"\u027b\5p9\2\u027b\u027c\7\7\2\2\u027c\u027d\7\4\2\2\u027di\3\2\2\2\u027e"+
		"\u0280\7?\2\2\u027f\u0281\5p9\2\u0280\u027f\3\2\2\2\u0280\u0281\3\2\2"+
		"\2\u0281\u0282\3\2\2\2\u0282\u0283\7\4\2\2\u0283k\3\2\2\2\u0284\u0285"+
		"\7@\2\2\u0285\u0286\5p9\2\u0286\u0287\7\4\2\2\u0287m\3\2\2\2\u0288\u0289"+
		"\7A\2\2\u0289\u0292\5F$\2\u028a\u028b\7B\2\2\u028b\u028c\7\6\2\2\u028c"+
		"\u028d\7`\2\2\u028d\u028e\7`\2\2\u028e\u028f\7\7\2\2\u028f\u0291\5F$\2"+
		"\u0290\u028a\3\2\2\2\u0291\u0294\3\2\2\2\u0292\u0290\3\2\2\2\u0292\u0293"+
		"\3\2\2\2\u0293\u0297\3\2\2\2\u0294\u0292\3\2\2\2\u0295\u0296\7C\2\2\u0296"+
		"\u0298\5F$\2\u0297\u0295\3\2\2\2\u0297\u0298\3\2\2\2\u0298o\3\2\2\2\u0299"+
		"\u029c\5t;\2\u029a\u029c\5\u00a4S\2\u029b\u0299\3\2\2\2\u029b\u029a\3"+
		"\2\2\2\u029cq\3\2\2\2\u029d\u029e\7`\2\2\u029e\u02a1\7`\2\2\u029f\u02a0"+
		"\7#\2\2\u02a0\u02a2\5p9\2\u02a1\u029f\3\2\2\2\u02a1\u02a2\3\2\2\2\u02a2"+
		"s\3\2\2\2\u02a3\u02a6\5z>\2\u02a4\u02a6\5v<\2\u02a5\u02a3\3\2\2\2\u02a5"+
		"\u02a4\3\2\2\2\u02a6u\3\2\2\2\u02a7\u02a8\5\30\r\2\u02a8\u02a9\7\67\2"+
		"\2\u02a9\u02aa\5H%\2\u02aaw\3\2\2\2\u02ab\u02b0\5p9\2\u02ac\u02ad\7\5"+
		"\2\2\u02ad\u02af\5p9\2\u02ae\u02ac\3\2\2\2\u02af\u02b2\3\2\2\2\u02b0\u02ae"+
		"\3\2\2\2\u02b0\u02b1\3\2\2\2\u02b1y\3\2\2\2\u02b2\u02b0\3\2\2\2\u02b3"+
		"\u02b9\5|?\2\u02b4\u02b5\7/\2\2\u02b5\u02b6\5p9\2\u02b6\u02b7\7\60\2\2"+
		"\u02b7\u02b8\5p9\2\u02b8\u02ba\3\2\2\2\u02b9\u02b4\3\2\2\2\u02b9\u02ba"+
		"\3\2\2\2\u02ba{\3\2\2\2\u02bb\u02be\5~@\2\u02bc\u02bd\78\2\2\u02bd\u02bf"+
		"\5|?\2\u02be\u02bc\3\2\2\2\u02be\u02bf\3\2\2\2\u02bf}\3\2\2\2\u02c0\u02c5"+
		"\5\u0080A\2\u02c1\u02c2\7\30\2\2\u02c2\u02c4\5\u0080A\2\u02c3\u02c1\3"+
		"\2\2\2\u02c4\u02c7\3\2\2\2\u02c5\u02c3\3\2\2\2\u02c5\u02c6\3\2\2\2\u02c6"+
		"\177\3\2\2\2\u02c7\u02c5\3\2\2\2\u02c8\u02cd\5\u0082B\2\u02c9\u02ca\7"+
		"\27\2\2\u02ca\u02cc\5\u0082B\2\u02cb\u02c9\3\2\2\2\u02cc\u02cf\3\2\2\2"+
		"\u02cd\u02cb\3\2\2\2\u02cd\u02ce\3\2\2\2\u02ce\u0081\3\2\2\2\u02cf\u02cd"+
		"\3\2\2\2\u02d0\u02d5\5\u0084C\2\u02d1\u02d2\7\24\2\2\u02d2\u02d4\5\u0084"+
		"C\2\u02d3\u02d1\3\2\2\2\u02d4\u02d7\3\2\2\2\u02d5\u02d3\3\2\2\2\u02d5"+
		"\u02d6\3\2\2\2\u02d6\u0083\3\2\2\2\u02d7\u02d5\3\2\2\2\u02d8\u02dd\5\u0086"+
		"D\2\u02d9\u02da\7\25\2\2\u02da\u02dc\5\u0086D\2\u02db\u02d9\3\2\2\2\u02dc"+
		"\u02df\3\2\2\2\u02dd\u02db\3\2\2\2\u02dd\u02de\3\2\2\2\u02de\u0085\3\2"+
		"\2\2\u02df\u02dd\3\2\2\2\u02e0\u02e5\5\u0088E\2\u02e1\u02e2\7\23\2\2\u02e2"+
		"\u02e4\5\u0088E\2\u02e3\u02e1\3\2\2\2\u02e4\u02e7\3\2\2\2\u02e5\u02e3"+
		"\3\2\2\2\u02e5\u02e6\3\2\2\2\u02e6\u0087\3\2\2\2\u02e7\u02e5\3\2\2\2\u02e8"+
		"\u02ee\5\u008aF\2\u02e9\u02ea\5V,\2\u02ea\u02eb\5\u008aF\2\u02eb\u02ed"+
		"\3\2\2\2\u02ec\u02e9\3\2\2\2\u02ed\u02f0\3\2\2\2\u02ee\u02ec\3\2\2\2\u02ee"+
		"\u02ef\3\2\2\2\u02ef\u0089\3\2\2\2\u02f0\u02ee\3\2\2\2\u02f1\u02f5\5\u008e"+
		"H\2\u02f2\u02f4\5\u008cG\2\u02f3\u02f2\3\2\2\2\u02f4\u02f7\3\2\2\2\u02f5"+
		"\u02f3\3\2\2\2\u02f5\u02f6\3\2\2\2\u02f6\u008b\3\2\2\2\u02f7\u02f5\3\2"+
		"\2\2\u02f8\u02f9\5X-\2\u02f9\u02fa\5\u008eH\2\u02fa\u02fe\3\2\2\2\u02fb"+
		"\u02fc\t\20\2\2\u02fc\u02fe\5B\"\2\u02fd\u02f8\3\2\2\2\u02fd\u02fb\3\2"+
		"\2\2\u02fe\u008d\3\2\2\2\u02ff\u0305\5\u0090I\2\u0300\u0301\5T+\2\u0301"+
		"\u0302\5\u0090I\2\u0302\u0304\3\2\2\2\u0303\u0300\3\2\2\2\u0304\u0307"+
		"\3\2\2\2\u0305\u0303\3\2\2\2\u0305\u0306\3\2\2\2\u0306\u008f\3\2\2\2\u0307"+
		"\u0305\3\2\2\2\u0308\u030e\5\u0092J\2\u0309\u030a\5J&\2\u030a\u030b\5"+
		"\u0092J\2\u030b\u030d\3\2\2\2\u030c\u0309\3\2\2\2\u030d\u0310\3\2\2\2"+
		"\u030e\u030c\3\2\2\2\u030e\u030f\3\2\2\2\u030f\u0091\3\2\2\2\u0310\u030e"+
		"\3\2\2\2\u0311\u0317\5\u0094K\2\u0312\u0313\5L\'\2\u0313\u0314\5\u0094"+
		"K\2\u0314\u0316\3\2\2\2\u0315\u0312\3\2\2\2\u0316\u0319\3\2\2\2\u0317"+
		"\u0315\3\2\2\2\u0317\u0318\3\2\2\2\u0318\u0093\3\2\2\2\u0319\u0317\3\2"+
		"\2\2\u031a\u031d\5\u0096L\2\u031b\u031c\7K\2\2\u031c\u031e\5$\23\2\u031d"+
		"\u031b\3\2\2\2\u031d\u031e\3\2\2\2\u031e\u0095\3\2\2\2\u031f\u0323\5\u009c"+
		"O\2\u0320\u0321\5\\/\2\u0321\u0322\5\u009cO\2\u0322\u0324\3\2\2\2\u0323"+
		"\u0320\3\2\2\2\u0323\u0324\3\2\2\2\u0324\u0097\3\2\2\2\u0325\u0326\5N"+
		"(\2\u0326\u0327\5\u009cO\2\u0327\u0099\3\2\2\2\u0328\u032b\5@!\2\u0329"+
		"\u032b\5B\"\2\u032a\u0328\3\2\2\2\u032a\u0329\3\2\2\2\u032b\u032c\3\2"+
		"\2\2\u032c\u032d\5N(\2\u032d\u009b\3\2\2\2\u032e\u0336\5\u00a6T\2\u032f"+
		"\u0330\t\21\2\2\u0330\u0336\5\u009cO\2\u0331\u0336\5\u0098M\2\u0332\u0336"+
		"\5\u009eP\2\u0333\u0336\5\u00a0Q\2\u0334\u0336\5\u00a2R\2\u0335\u032e"+
		"\3\2\2\2\u0335\u032f\3\2\2\2\u0335\u0331\3\2\2\2\u0335\u0332\3\2\2\2\u0335"+
		"\u0333\3\2\2\2\u0335\u0334\3\2\2\2\u0336\u009d\3\2\2\2\u0337\u0338\7\6"+
		"\2\2\u0338\u0339\7`\2\2\u0339\u033a\7\7\2\2\u033a\u033b\5\u009cO\2\u033b"+
		"\u009f\3\2\2\2\u033c\u033d\7\16\2\2\u033d\u033e\5\u009cO\2\u033e\u00a1"+
		"\3\2\2\2\u033f\u0340\7\23\2\2\u0340\u0341\5\u009cO\2\u0341\u00a3\3\2\2"+
		"\2\u0342\u0343\5\u009cO\2\u0343\u0344\5Z.\2\u0344\u0345\5p9\2\u0345\u00a5"+
		"\3\2\2\2\u0346\u0349\5\u00a8U\2\u0347\u0349\5\u00b0Y\2\u0348\u0346\3\2"+
		"\2\2\u0348\u0347\3\2\2\2\u0349\u00a7\3\2\2\2\u034a\u034b\7D\2\2\u034b"+
		"\u034d\5\32\16\2\u034c\u034e\5\u00aaV\2\u034d\u034c\3\2\2\2\u034d\u034e"+
		"\3\2\2\2\u034e\u0350\3\2\2\2\u034f\u0351\5\u00acW\2\u0350\u034f\3\2\2"+
		"\2\u0350\u0351\3\2\2\2\u0351\u00a9\3\2\2\2\u0352\u0356\7\b\2\2\u0353\u0355"+
		"\7\5\2\2\u0354\u0353\3\2\2\2\u0355\u0358\3\2\2\2\u0356\u0354\3\2\2\2\u0356"+
		"\u0357\3\2\2\2\u0357\u0359\3\2\2\2\u0358\u0356\3\2\2\2\u0359\u035a\7\t"+
		"\2\2\u035a\u00ab\3\2\2\2\u035b\u0367\7\n\2\2\u035c\u0361\5\u00aeX\2\u035d"+
		"\u035e\7\5\2\2\u035e\u0360\5\u00aeX\2\u035f\u035d\3\2\2\2\u0360\u0363"+
		"\3\2\2\2\u0361\u035f\3\2\2\2\u0361\u0362\3\2\2\2\u0362\u0365\3\2\2\2\u0363"+
		"\u0361\3\2\2\2\u0364\u0366\7\5\2\2\u0365\u0364\3\2\2\2\u0365\u0366\3\2"+
		"\2\2\u0366\u0368\3\2\2\2\u0367\u035c\3\2\2\2\u0367\u0368\3\2\2\2\u0368"+
		"\u0369\3\2\2\2\u0369\u036a\7\13\2\2\u036a\u00ad\3\2\2\2\u036b\u036e\5"+
		"p9\2\u036c\u036e\5\u00acW\2\u036d\u036b\3\2\2\2\u036d\u036c\3\2\2\2\u036e"+
		"\u00af\3\2\2\2\u036f\u0379\5@!\2\u0370\u0379\5D#\2\u0371\u0372\7\6\2\2"+
		"\u0372\u0373\5p9\2\u0373\u0374\7\7\2\2\u0374\u0379\3\2\2\2\u0375\u0379"+
		"\5\u00b4[\2\u0376\u0379\5\u009aN\2\u0377\u0379\5\u00b6\\\2\u0378\u036f"+
		"\3\2\2\2\u0378\u0370\3\2\2\2\u0378\u0371\3\2\2\2\u0378\u0375\3\2\2\2\u0378"+
		"\u0376\3\2\2\2\u0378\u0377\3\2\2\2\u0379\u00b1\3\2\2\2\u037a\u037b\5\u00a8"+
		"U\2\u037b\u037c\7\"\2\2\u037c\u0396\3\2\2\2\u037d\u037e\5@!\2\u037e\u037f"+
		"\7\"\2\2\u037f\u0396\3\2\2\2\u0380\u0382\5D#\2\u0381\u0383\5\26\f\2\u0382"+
		"\u0381\3\2\2\2\u0382\u0383\3\2\2\2\u0383\u0386\3\2\2\2\u0384\u0387\5\30"+
		"\r\2\u0385\u0387\5\32\16\2\u0386\u0384\3\2\2\2\u0386\u0385\3\2\2\2\u0386"+
		"\u0387\3\2\2\2\u0387\u0388\3\2\2\2\u0388\u0389\7\"\2\2\u0389\u0396\3\2"+
		"\2\2\u038a\u038b\7\6\2\2\u038b\u038c\5p9\2\u038c\u038d\7\7\2\2\u038d\u038e"+
		"\7\"\2\2\u038e\u0396\3\2\2\2\u038f\u0390\5\u009aN\2\u0390\u0391\7\"\2"+
		"\2\u0391\u0396\3\2\2\2\u0392\u0393\5\u00b6\\\2\u0393\u0394\7\"\2\2\u0394"+
		"\u0396\3\2\2\2\u0395\u037a\3\2\2\2\u0395\u037d\3\2\2\2\u0395\u0380\3\2"+
		"\2\2\u0395\u038a\3\2\2\2\u0395\u038f\3\2\2\2\u0395\u0392\3\2\2\2\u0396"+
		"\u00b3\3\2\2\2\u0397\u0399\5\u00b2Z\2\u0398\u0397\3\2\2\2\u0399\u039c"+
		"\3\2\2\2\u039a\u0398\3\2\2\2\u039a\u039b\3\2\2\2\u039b\u039d\3\2\2\2\u039c"+
		"\u039a\3\2\2\2\u039d\u039f\5D#\2\u039e\u03a0\5\26\f\2\u039f\u039e\3\2"+
		"\2\2\u039f\u03a0\3\2\2\2\u03a0\u03a3\3\2\2\2\u03a1\u03a4\5\30\r\2\u03a2"+
		"\u03a4\5\32\16\2\u03a3\u03a1\3\2\2\2\u03a3\u03a2\3\2\2\2\u03a3\u03a4\3"+
		"\2\2\2\u03a4\u00b5\3\2\2\2\u03a5\u03ac\5\u00ba^\2\u03a6\u03ac\5\u00bc"+
		"_\2\u03a7\u03ac\5\u00be`\2\u03a8\u03ac\5\u00c0a\2\u03a9\u03ac\5\u00c2"+
		"b\2\u03aa\u03ac\5\u00c6d\2\u03ab\u03a5\3\2\2\2\u03ab\u03a6\3\2\2\2\u03ab"+
		"\u03a7\3\2\2\2\u03ab\u03a8\3\2\2\2\u03ab\u03a9\3\2\2\2\u03ab\u03aa\3\2"+
		"\2\2\u03ac\u00b7\3\2\2\2\u03ad\u03b0\5\36\20\2\u03ae\u03b0\5\"\22\2\u03af"+
		"\u03ad\3\2\2\2\u03af\u03ae\3\2\2\2\u03b0\u00b9\3\2\2\2\u03b1\u03b2\7D"+
		"\2\2\u03b2\u03b8\7`\2\2\u03b3\u03b5\5\30\r\2\u03b4\u03b6\5\u00b8]\2\u03b5"+
		"\u03b4\3\2\2\2\u03b5\u03b6\3\2\2\2\u03b6\u03b9\3\2\2\2\u03b7\u03b9\5\u00b8"+
		"]\2\u03b8\u03b3\3\2\2\2\u03b8\u03b7\3\2\2\2\u03b9\u03bd\3\2\2\2\u03ba"+
		"\u03bb\7D\2\2\u03bb\u03bd\5&\24\2\u03bc\u03b1\3\2\2\2\u03bc\u03ba\3\2"+
		"\2\2\u03bd\u00bb\3\2\2\2\u03be\u03bf\7E\2\2\u03bf\u03c0\7\6\2\2\u03c0"+
		"\u03c1\7`\2\2\u03c1\u03c2\7\7\2\2\u03c2\u00bd\3\2\2\2\u03c3\u03c4\7F\2"+
		"\2\u03c4\u03c5\7\6\2\2\u03c5\u03c6\5p9\2\u03c6\u03c7\7\7\2\2\u03c7\u00bf"+
		"\3\2\2\2\u03c8\u03c9\7G\2\2\u03c9\u03ca\7\6\2\2\u03ca\u03cb\5p9\2\u03cb"+
		"\u03cc\7\7\2\2\u03cc\u00c1\3\2\2\2\u03cd\u03d1\7H\2\2\u03ce\u03cf\7\6"+
		"\2\2\u03cf\u03d0\7`\2\2\u03d0\u03d2\7\7\2\2\u03d1\u03ce\3\2\2\2\u03d1"+
		"\u03d2\3\2\2\2\u03d2\u00c3\3\2\2\2\u03d3\u03d4\7I\2\2\u03d4\u03d5\5\16"+
		"\b\2\u03d5\u03d6\5H%\2\u03d6\u00c5\3\2\2\2\u03d7\u03d8\7J\2\2\u03d8\u03d9"+
		"\7\6\2\2\u03d9\u03da\7`\2\2\u03da\u03db\7\7\2\2\u03db\u00c7\3\2\2\2j\u00cb"+
		"\u00d7\u00df\u00e9\u00ee\u00f4\u00fd\u0104\u0109\u010f\u0115\u011b\u0120"+
		"\u0126\u0129\u012b\u0134\u013a\u013d\u013f\u0145\u014c\u0152\u0155\u0157"+
		"\u0167\u016e\u0174\u017c\u017e\u0186\u018c\u0194\u0199\u019f\u01a7\u01ab"+
		"\u01b5\u01bb\u01c3\u01c9\u01cd\u01d2\u01d8\u01e0\u01e6\u01ea\u01ee\u01f9"+
		"\u0204\u0207\u020f\u0214\u021b\u021f\u022b\u0231\u0252\u025b\u0280\u0292"+
		"\u0297\u029b\u02a1\u02a5\u02b0\u02b9\u02be\u02c5\u02cd\u02d5\u02dd\u02e5"+
		"\u02ee\u02f5\u02fd\u0305\u030e\u0317\u031d\u0323\u032a\u0335\u0348\u034d"+
		"\u0350\u0356\u0361\u0365\u0367\u036d\u0378\u0382\u0386\u0395\u039a\u039f"+
		"\u03a3\u03ab\u03af\u03b5\u03b8\u03bc\u03d1";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}