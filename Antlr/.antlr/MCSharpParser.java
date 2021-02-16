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
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, T__33=34, T__34=35, T__35=36, T__36=37, T__37=38, 
		T__38=39, T__39=40, T__40=41, T__41=42, T__42=43, T__43=44, T__44=45, 
		T__45=46, T__46=47, T__47=48, T__48=49, T__49=50, T__50=51, T__51=52, 
		T__52=53, T__53=54, T__54=55, T__55=56, T__56=57, T__57=58, T__58=59, 
		T__59=60, T__60=61, T__61=62, T__62=63, T__63=64, T__64=65, T__65=66, 
		T__66=67, T__67=68, T__68=69, T__69=70, T__70=71, T__71=72, T__72=73, 
		WHITESPACE=74, NEWLINE=75, INTEGER=76, DECIMAL=77, STRING=78, TYPE_NAME=79, 
		MEMBER_NAME=80, VARIABLE_NAME=81, ACCESS=82, USAGE=83, MODIFIER=84, PARAMETER_MODIFIER=85;
	public static final int
		RULE_generic_parameter = 0, RULE_generic_parameter_list = 1, RULE_generic_parameters = 2, 
		RULE_method_parameter = 3, RULE_method_parameter_list = 4, RULE_method_parameters = 5, 
		RULE_indexer_parameters = 6, RULE_argument = 7, RULE_argument_list = 8, 
		RULE_generic_arguments = 9, RULE_method_arguments = 10, RULE_indexer_arguments = 11, 
		RULE_member_initializer = 12, RULE_object_initializer = 13, RULE_element_initializer = 14, 
		RULE_collection_initializer = 15, RULE_anonymous_element_initializer = 16, 
		RULE_anonymous_object_initializer = 17, RULE_type_definition = 18, RULE_member_definition = 19, 
		RULE_field_definition = 20, RULE_field_definition_end = 21, RULE_property_definition = 22, 
		RULE_property_definition_end = 23, RULE_property_get_definition = 24, 
		RULE_property_set_definition = 25, RULE_method_definition = 26, RULE_method_definition_end = 27, 
		RULE_literal = 28, RULE_identifier = 29, RULE_statement = 30, RULE_code_block = 31, 
		RULE_language_function = 32, RULE_if_statement = 33, RULE_for_statement = 34, 
		RULE_foreach_statement = 35, RULE_while_statement = 36, RULE_do_statement = 37, 
		RULE_return_statement = 38, RULE_throw_statement = 39, RULE_try_statement = 40, 
		RULE_expression = 41, RULE_initialization_expression = 42, RULE_non_assignment_expression = 43, 
		RULE_lambda_expression = 44, RULE_expression_list = 45, RULE_conditional_expression = 46, 
		RULE_null_coalescing_expression = 47, RULE_conditional_or_expression = 48, 
		RULE_conditional_and_expression = 49, RULE_inclusive_or_expression = 50, 
		RULE_exclusive_or_expression = 51, RULE_and_expression = 52, RULE_equality_expression = 53, 
		RULE_relational_expression = 54, RULE_relation_or_type_check = 55, RULE_shift_expression = 56, 
		RULE_additive_expression = 57, RULE_multiplicative_expression = 58, RULE_with_expression = 59, 
		RULE_range_expression = 60, RULE_unary_expression = 61, RULE_cast_expression = 62, 
		RULE_pointer_indirection_expression = 63, RULE_addressof_expression = 64, 
		RULE_assignment_expression = 65, RULE_primary_expression = 66, RULE_array_creation_expression = 67, 
		RULE_array_rank_specifier = 68, RULE_array_initializer = 69, RULE_variable_initializer = 70, 
		RULE_primary_no_array_creation_expression = 71, RULE_member_access = 72, 
		RULE_invocation_expression = 73, RULE_indexer_expression = 74, RULE_post_step_expression = 75, 
		RULE_keyword_expression = 76, RULE_object_or_collection_initializer = 77, 
		RULE_new_keyword_expression = 78, RULE_typeof_keyword_expression = 79, 
		RULE_checked_expression = 80, RULE_unchecked_expression = 81, RULE_default_keyword_expression = 82, 
		RULE_delegate_keyword_expression = 83, RULE_sizeof_keyword_expression = 84;
	private static String[] makeRuleNames() {
		return new String[] {
			"generic_parameter", "generic_parameter_list", "generic_parameters", 
			"method_parameter", "method_parameter_list", "method_parameters", "indexer_parameters", 
			"argument", "argument_list", "generic_arguments", "method_arguments", 
			"indexer_arguments", "member_initializer", "object_initializer", "element_initializer", 
			"collection_initializer", "anonymous_element_initializer", "anonymous_object_initializer", 
			"type_definition", "member_definition", "field_definition", "field_definition_end", 
			"property_definition", "property_definition_end", "property_get_definition", 
			"property_set_definition", "method_definition", "method_definition_end", 
			"literal", "identifier", "statement", "code_block", "language_function", 
			"if_statement", "for_statement", "foreach_statement", "while_statement", 
			"do_statement", "return_statement", "throw_statement", "try_statement", 
			"expression", "initialization_expression", "non_assignment_expression", 
			"lambda_expression", "expression_list", "conditional_expression", "null_coalescing_expression", 
			"conditional_or_expression", "conditional_and_expression", "inclusive_or_expression", 
			"exclusive_or_expression", "and_expression", "equality_expression", "relational_expression", 
			"relation_or_type_check", "shift_expression", "additive_expression", 
			"multiplicative_expression", "with_expression", "range_expression", "unary_expression", 
			"cast_expression", "pointer_indirection_expression", "addressof_expression", 
			"assignment_expression", "primary_expression", "array_creation_expression", 
			"array_rank_specifier", "array_initializer", "variable_initializer", 
			"primary_no_array_creation_expression", "member_access", "invocation_expression", 
			"indexer_expression", "post_step_expression", "keyword_expression", "object_or_collection_initializer", 
			"new_keyword_expression", "typeof_keyword_expression", "checked_expression", 
			"unchecked_expression", "default_keyword_expression", "delegate_keyword_expression", 
			"sizeof_keyword_expression"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "','", "'<'", "'>'", "'('", "')'", "'['", "']'", "'='", "'{'", 
			"'}'", "';'", "'=>'", "'get'", "'set'", "'@'", "'.'", "'if'", "'for'", 
			"'foreach'", "'in'", "'while'", "'do'", "'return'", "'throw'", "'try'", 
			"'catch'", "'finally'", "'?'", "':'", "'??'", "'||'", "'&&'", "'|'", 
			"'^'", "'&'", "'=='", "'!='", "'<='", "'>='", "'is'", "'as'", "'<<'", 
			"'>>'", "'+'", "'-'", "'*'", "'/'", "'%'", "'with'", "'..'", "'..^'", 
			"'!'", "'~'", "'++'", "'--'", "'+='", "'-='", "'*='", "'/='", "'%='", 
			"'&='", "'|='", "'^='", "'<<='", "'>>='", "'.='", "'new'", "'typeof'", 
			"'checked'", "'unchecked'", "'default'", "'delegate'", "'sizeof'", null, 
			"'\n'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, "WHITESPACE", "NEWLINE", "INTEGER", "DECIMAL", "STRING", 
			"TYPE_NAME", "MEMBER_NAME", "VARIABLE_NAME", "ACCESS", "USAGE", "MODIFIER", 
			"PARAMETER_MODIFIER"
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

	public static class Generic_parameterContext extends ParserRuleContext {
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public Generic_parameterContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_generic_parameter; }
	}

	public final Generic_parameterContext generic_parameter() throws RecognitionException {
		Generic_parameterContext _localctx = new Generic_parameterContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_generic_parameter);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(170);
			match(TYPE_NAME);
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
		public Generic_parameter_listContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_generic_parameter_list; }
	}

	public final Generic_parameter_listContext generic_parameter_list() throws RecognitionException {
		Generic_parameter_listContext _localctx = new Generic_parameter_listContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_generic_parameter_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(172);
			generic_parameter();
			setState(177);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__0) {
				{
				{
				setState(173);
				match(T__0);
				setState(174);
				generic_parameter();
				}
				}
				setState(179);
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
		public Generic_parameter_listContext generic_parameter_list() {
			return getRuleContext(Generic_parameter_listContext.class,0);
		}
		public Generic_parametersContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_generic_parameters; }
	}

	public final Generic_parametersContext generic_parameters() throws RecognitionException {
		Generic_parametersContext _localctx = new Generic_parametersContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_generic_parameters);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(180);
			match(T__1);
			setState(181);
			generic_parameter_list();
			setState(182);
			match(T__2);
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
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public TerminalNode VARIABLE_NAME() { return getToken(MCSharpParser.VARIABLE_NAME, 0); }
		public TerminalNode PARAMETER_MODIFIER() { return getToken(MCSharpParser.PARAMETER_MODIFIER, 0); }
		public Method_parameterContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_method_parameter; }
	}

	public final Method_parameterContext method_parameter() throws RecognitionException {
		Method_parameterContext _localctx = new Method_parameterContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_method_parameter);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(185);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==PARAMETER_MODIFIER) {
				{
				setState(184);
				match(PARAMETER_MODIFIER);
				}
			}

			setState(187);
			match(TYPE_NAME);
			setState(188);
			match(VARIABLE_NAME);
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
		public Method_parameter_listContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_method_parameter_list; }
	}

	public final Method_parameter_listContext method_parameter_list() throws RecognitionException {
		Method_parameter_listContext _localctx = new Method_parameter_listContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_method_parameter_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(190);
			method_parameter();
			setState(195);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__0) {
				{
				{
				setState(191);
				match(T__0);
				setState(192);
				method_parameter();
				}
				}
				setState(197);
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
		enterRule(_localctx, 10, RULE_method_parameters);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(198);
			match(T__3);
			setState(199);
			method_parameter_list();
			setState(200);
			match(T__4);
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
		enterRule(_localctx, 12, RULE_indexer_parameters);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(202);
			match(T__5);
			setState(203);
			method_parameter_list();
			setState(204);
			match(T__6);
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
		public TerminalNode PARAMETER_MODIFIER() { return getToken(MCSharpParser.PARAMETER_MODIFIER, 0); }
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public TerminalNode VARIABLE_NAME() { return getToken(MCSharpParser.VARIABLE_NAME, 0); }
		public ArgumentContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_argument; }
	}

	public final ArgumentContext argument() throws RecognitionException {
		ArgumentContext _localctx = new ArgumentContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_argument);
		try {
			setState(210);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__3:
			case T__14:
			case T__34:
			case T__43:
			case T__44:
			case T__45:
			case T__51:
			case T__52:
			case T__53:
			case T__54:
			case T__66:
			case T__67:
			case T__68:
			case T__69:
			case T__70:
			case T__72:
			case INTEGER:
			case DECIMAL:
			case STRING:
			case TYPE_NAME:
			case MEMBER_NAME:
			case VARIABLE_NAME:
				enterOuterAlt(_localctx, 1);
				{
				setState(206);
				expression();
				}
				break;
			case PARAMETER_MODIFIER:
				enterOuterAlt(_localctx, 2);
				{
				setState(207);
				match(PARAMETER_MODIFIER);
				setState(208);
				match(TYPE_NAME);
				setState(209);
				match(VARIABLE_NAME);
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
		public Argument_listContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_argument_list; }
	}

	public final Argument_listContext argument_list() throws RecognitionException {
		Argument_listContext _localctx = new Argument_listContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_argument_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(212);
			argument();
			setState(217);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__0) {
				{
				{
				setState(213);
				match(T__0);
				setState(214);
				argument();
				}
				}
				setState(219);
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
		enterRule(_localctx, 18, RULE_generic_arguments);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(220);
			match(T__1);
			setState(221);
			generic_parameter_list();
			setState(222);
			match(T__2);
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
		enterRule(_localctx, 20, RULE_method_arguments);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(224);
			match(T__3);
			setState(225);
			argument_list();
			setState(226);
			match(T__4);
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
		enterRule(_localctx, 22, RULE_indexer_arguments);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(228);
			match(T__5);
			setState(229);
			argument_list();
			setState(230);
			match(T__6);
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
		public TerminalNode MEMBER_NAME() { return getToken(MCSharpParser.MEMBER_NAME, 0); }
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
		enterRule(_localctx, 24, RULE_member_initializer);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(232);
			match(MEMBER_NAME);
			setState(234);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__1) {
				{
				setState(233);
				generic_arguments();
				}
			}

			setState(236);
			match(T__7);
			setState(239);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__3:
			case T__14:
			case T__34:
			case T__43:
			case T__44:
			case T__45:
			case T__51:
			case T__52:
			case T__53:
			case T__54:
			case T__66:
			case T__67:
			case T__68:
			case T__69:
			case T__70:
			case T__72:
			case INTEGER:
			case DECIMAL:
			case STRING:
			case TYPE_NAME:
			case MEMBER_NAME:
			case VARIABLE_NAME:
				{
				setState(237);
				expression();
				}
				break;
			case T__8:
				{
				setState(238);
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
		public List<Member_initializerContext> member_initializer() {
			return getRuleContexts(Member_initializerContext.class);
		}
		public Member_initializerContext member_initializer(int i) {
			return getRuleContext(Member_initializerContext.class,i);
		}
		public Object_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_object_initializer; }
	}

	public final Object_initializerContext object_initializer() throws RecognitionException {
		Object_initializerContext _localctx = new Object_initializerContext(_ctx, getState());
		enterRule(_localctx, 26, RULE_object_initializer);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(241);
			match(T__8);
			setState(250);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==MEMBER_NAME) {
				{
				setState(242);
				member_initializer();
				setState(245);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,7,_ctx) ) {
				case 1:
					{
					setState(243);
					match(T__0);
					setState(244);
					member_initializer();
					}
					break;
				}
				setState(248);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(247);
					match(T__0);
					}
				}

				}
			}

			setState(252);
			match(T__9);
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
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Element_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_element_initializer; }
	}

	public final Element_initializerContext element_initializer() throws RecognitionException {
		Element_initializerContext _localctx = new Element_initializerContext(_ctx, getState());
		enterRule(_localctx, 28, RULE_element_initializer);
		try {
			setState(259);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__3:
			case T__14:
			case T__34:
			case T__43:
			case T__44:
			case T__45:
			case T__51:
			case T__52:
			case T__53:
			case T__54:
			case T__66:
			case T__67:
			case T__68:
			case T__69:
			case T__70:
			case T__72:
			case INTEGER:
			case DECIMAL:
			case STRING:
			case TYPE_NAME:
			case MEMBER_NAME:
			case VARIABLE_NAME:
				enterOuterAlt(_localctx, 1);
				{
				setState(254);
				non_assignment_expression();
				}
				break;
			case T__8:
				enterOuterAlt(_localctx, 2);
				{
				setState(255);
				match(T__8);
				setState(256);
				expression();
				setState(257);
				match(T__9);
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
		public List<Element_initializerContext> element_initializer() {
			return getRuleContexts(Element_initializerContext.class);
		}
		public Element_initializerContext element_initializer(int i) {
			return getRuleContext(Element_initializerContext.class,i);
		}
		public Collection_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_collection_initializer; }
	}

	public final Collection_initializerContext collection_initializer() throws RecognitionException {
		Collection_initializerContext _localctx = new Collection_initializerContext(_ctx, getState());
		enterRule(_localctx, 30, RULE_collection_initializer);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(261);
			match(T__8);
			setState(270);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__3) | (1L << T__8) | (1L << T__14) | (1L << T__34) | (1L << T__43) | (1L << T__44) | (1L << T__45) | (1L << T__51) | (1L << T__52) | (1L << T__53) | (1L << T__54))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (T__66 - 67)) | (1L << (T__67 - 67)) | (1L << (T__68 - 67)) | (1L << (T__69 - 67)) | (1L << (T__70 - 67)) | (1L << (T__72 - 67)) | (1L << (INTEGER - 67)) | (1L << (DECIMAL - 67)) | (1L << (STRING - 67)) | (1L << (TYPE_NAME - 67)) | (1L << (MEMBER_NAME - 67)) | (1L << (VARIABLE_NAME - 67)))) != 0)) {
				{
				setState(262);
				element_initializer();
				setState(265);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,11,_ctx) ) {
				case 1:
					{
					setState(263);
					match(T__0);
					setState(264);
					element_initializer();
					}
					break;
				}
				setState(268);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(267);
					match(T__0);
					}
				}

				}
			}

			setState(272);
			match(T__9);
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
		public TerminalNode MEMBER_NAME() { return getToken(MCSharpParser.MEMBER_NAME, 0); }
		public Generic_argumentsContext generic_arguments() {
			return getRuleContext(Generic_argumentsContext.class,0);
		}
		public Member_accessContext member_access() {
			return getRuleContext(Member_accessContext.class,0);
		}
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
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
		enterRule(_localctx, 32, RULE_anonymous_element_initializer);
		try {
			setState(283);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,15,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(274);
				match(MEMBER_NAME);
				setState(276);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,14,_ctx) ) {
				case 1:
					{
					setState(275);
					generic_arguments();
					}
					break;
				}
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(278);
				member_access();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(279);
				identifier();
				setState(280);
				match(T__7);
				setState(281);
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
		public List<Anonymous_element_initializerContext> anonymous_element_initializer() {
			return getRuleContexts(Anonymous_element_initializerContext.class);
		}
		public Anonymous_element_initializerContext anonymous_element_initializer(int i) {
			return getRuleContext(Anonymous_element_initializerContext.class,i);
		}
		public Anonymous_object_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_anonymous_object_initializer; }
	}

	public final Anonymous_object_initializerContext anonymous_object_initializer() throws RecognitionException {
		Anonymous_object_initializerContext _localctx = new Anonymous_object_initializerContext(_ctx, getState());
		enterRule(_localctx, 34, RULE_anonymous_object_initializer);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(285);
			match(T__8);
			setState(294);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__3 || _la==T__14 || ((((_la - 79)) & ~0x3f) == 0 && ((1L << (_la - 79)) & ((1L << (TYPE_NAME - 79)) | (1L << (MEMBER_NAME - 79)) | (1L << (VARIABLE_NAME - 79)))) != 0)) {
				{
				setState(286);
				anonymous_element_initializer();
				setState(289);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,16,_ctx) ) {
				case 1:
					{
					setState(287);
					match(T__0);
					setState(288);
					anonymous_element_initializer();
					}
					break;
				}
				setState(292);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(291);
					match(T__0);
					}
				}

				}
			}

			setState(296);
			match(T__9);
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
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public List<TerminalNode> MODIFIER() { return getTokens(MCSharpParser.MODIFIER); }
		public TerminalNode MODIFIER(int i) {
			return getToken(MCSharpParser.MODIFIER, i);
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
		enterRule(_localctx, 36, RULE_type_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(301);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==MODIFIER) {
				{
				{
				setState(298);
				match(MODIFIER);
				}
				}
				setState(303);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(304);
			match(TYPE_NAME);
			setState(305);
			match(T__8);
			setState(309);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==MEMBER_NAME || _la==MODIFIER) {
				{
				{
				setState(306);
				member_definition();
				}
				}
				setState(311);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(312);
			match(T__9);
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
		public Field_definitionContext field_definition() {
			return getRuleContext(Field_definitionContext.class,0);
		}
		public Property_definitionContext property_definition() {
			return getRuleContext(Property_definitionContext.class,0);
		}
		public Method_definitionContext method_definition() {
			return getRuleContext(Method_definitionContext.class,0);
		}
		public Member_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_member_definition; }
	}

	public final Member_definitionContext member_definition() throws RecognitionException {
		Member_definitionContext _localctx = new Member_definitionContext(_ctx, getState());
		enterRule(_localctx, 38, RULE_member_definition);
		try {
			setState(317);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,21,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(314);
				field_definition();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(315);
				property_definition();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(316);
				method_definition();
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

	public static class Field_definitionContext extends ParserRuleContext {
		public TerminalNode MEMBER_NAME() { return getToken(MCSharpParser.MEMBER_NAME, 0); }
		public Field_definition_endContext field_definition_end() {
			return getRuleContext(Field_definition_endContext.class,0);
		}
		public List<TerminalNode> MODIFIER() { return getTokens(MCSharpParser.MODIFIER); }
		public TerminalNode MODIFIER(int i) {
			return getToken(MCSharpParser.MODIFIER, i);
		}
		public Field_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_field_definition; }
	}

	public final Field_definitionContext field_definition() throws RecognitionException {
		Field_definitionContext _localctx = new Field_definitionContext(_ctx, getState());
		enterRule(_localctx, 40, RULE_field_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(322);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==MODIFIER) {
				{
				{
				setState(319);
				match(MODIFIER);
				}
				}
				setState(324);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(325);
			match(MEMBER_NAME);
			setState(326);
			field_definition_end();
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

	public static class Field_definition_endContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Field_definition_endContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_field_definition_end; }
	}

	public final Field_definition_endContext field_definition_end() throws RecognitionException {
		Field_definition_endContext _localctx = new Field_definition_endContext(_ctx, getState());
		enterRule(_localctx, 42, RULE_field_definition_end);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(330);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__7) {
				{
				setState(328);
				match(T__7);
				setState(329);
				expression();
				}
			}

			setState(332);
			match(T__10);
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
		public TerminalNode MEMBER_NAME() { return getToken(MCSharpParser.MEMBER_NAME, 0); }
		public Property_definition_endContext property_definition_end() {
			return getRuleContext(Property_definition_endContext.class,0);
		}
		public List<TerminalNode> MODIFIER() { return getTokens(MCSharpParser.MODIFIER); }
		public TerminalNode MODIFIER(int i) {
			return getToken(MCSharpParser.MODIFIER, i);
		}
		public Property_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_property_definition; }
	}

	public final Property_definitionContext property_definition() throws RecognitionException {
		Property_definitionContext _localctx = new Property_definitionContext(_ctx, getState());
		enterRule(_localctx, 44, RULE_property_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(337);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==MODIFIER) {
				{
				{
				setState(334);
				match(MODIFIER);
				}
				}
				setState(339);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(340);
			match(MEMBER_NAME);
			setState(341);
			property_definition_end();
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

	public static class Property_definition_endContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Property_get_definitionContext property_get_definition() {
			return getRuleContext(Property_get_definitionContext.class,0);
		}
		public Property_set_definitionContext property_set_definition() {
			return getRuleContext(Property_set_definitionContext.class,0);
		}
		public List<TerminalNode> MODIFIER() { return getTokens(MCSharpParser.MODIFIER); }
		public TerminalNode MODIFIER(int i) {
			return getToken(MCSharpParser.MODIFIER, i);
		}
		public Property_definition_endContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_property_definition_end; }
	}

	public final Property_definition_endContext property_definition_end() throws RecognitionException {
		Property_definition_endContext _localctx = new Property_definition_endContext(_ctx, getState());
		enterRule(_localctx, 46, RULE_property_definition_end);
		int _la;
		try {
			setState(382);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__11:
				enterOuterAlt(_localctx, 1);
				{
				setState(343);
				match(T__11);
				setState(344);
				expression();
				}
				break;
			case T__8:
				enterOuterAlt(_localctx, 2);
				{
				setState(345);
				match(T__8);
				{
				{
				setState(349);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==MODIFIER) {
					{
					{
					setState(346);
					match(MODIFIER);
					}
					}
					setState(351);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(352);
				property_get_definition();
				}
				setState(361);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,27,_ctx) ) {
				case 1:
					{
					setState(357);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==MODIFIER) {
						{
						{
						setState(354);
						match(MODIFIER);
						}
						}
						setState(359);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(360);
					property_set_definition();
					}
					break;
				}
				}
				}
				break;
			case T__13:
			case MODIFIER:
				enterOuterAlt(_localctx, 3);
				{
				{
				{
				setState(366);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==MODIFIER) {
					{
					{
					setState(363);
					match(MODIFIER);
					}
					}
					setState(368);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(369);
				property_set_definition();
				}
				setState(378);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__12 || _la==MODIFIER) {
					{
					setState(374);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==MODIFIER) {
						{
						{
						setState(371);
						match(MODIFIER);
						}
						}
						setState(376);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(377);
					property_get_definition();
					}
				}

				}
				setState(380);
				match(T__9);
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
		enterRule(_localctx, 48, RULE_property_get_definition);
		try {
			setState(393);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,32,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(384);
				match(T__12);
				setState(385);
				match(T__10);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(386);
				match(T__12);
				setState(387);
				match(T__11);
				setState(388);
				expression();
				setState(389);
				match(T__10);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(391);
				match(T__12);
				setState(392);
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
		enterRule(_localctx, 50, RULE_property_set_definition);
		try {
			setState(404);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,33,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(395);
				match(T__13);
				setState(396);
				match(T__10);
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(397);
				match(T__13);
				setState(398);
				match(T__11);
				setState(399);
				expression();
				setState(400);
				match(T__10);
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(402);
				match(T__13);
				setState(403);
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
		public TerminalNode MEMBER_NAME() { return getToken(MCSharpParser.MEMBER_NAME, 0); }
		public Method_parametersContext method_parameters() {
			return getRuleContext(Method_parametersContext.class,0);
		}
		public Method_definition_endContext method_definition_end() {
			return getRuleContext(Method_definition_endContext.class,0);
		}
		public List<TerminalNode> MODIFIER() { return getTokens(MCSharpParser.MODIFIER); }
		public TerminalNode MODIFIER(int i) {
			return getToken(MCSharpParser.MODIFIER, i);
		}
		public Method_definitionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_method_definition; }
	}

	public final Method_definitionContext method_definition() throws RecognitionException {
		Method_definitionContext _localctx = new Method_definitionContext(_ctx, getState());
		enterRule(_localctx, 52, RULE_method_definition);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(409);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==MODIFIER) {
				{
				{
				setState(406);
				match(MODIFIER);
				}
				}
				setState(411);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(412);
			match(MEMBER_NAME);
			setState(413);
			method_parameters();
			setState(414);
			method_definition_end();
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

	public static class Method_definition_endContext extends ParserRuleContext {
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Code_blockContext code_block() {
			return getRuleContext(Code_blockContext.class,0);
		}
		public Method_definition_endContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_method_definition_end; }
	}

	public final Method_definition_endContext method_definition_end() throws RecognitionException {
		Method_definition_endContext _localctx = new Method_definition_endContext(_ctx, getState());
		enterRule(_localctx, 54, RULE_method_definition_end);
		try {
			setState(421);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__11:
				enterOuterAlt(_localctx, 1);
				{
				setState(416);
				match(T__11);
				setState(417);
				expression();
				setState(418);
				match(T__10);
				}
				break;
			case T__8:
				enterOuterAlt(_localctx, 2);
				{
				setState(420);
				code_block();
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
		enterRule(_localctx, 56, RULE_literal);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(423);
			_la = _input.LA(1);
			if ( !(((((_la - 76)) & ~0x3f) == 0 && ((1L << (_la - 76)) & ((1L << (INTEGER - 76)) | (1L << (DECIMAL - 76)) | (1L << (STRING - 76)))) != 0)) ) {
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
		public List<TerminalNode> TYPE_NAME() { return getTokens(MCSharpParser.TYPE_NAME); }
		public TerminalNode TYPE_NAME(int i) {
			return getToken(MCSharpParser.TYPE_NAME, i);
		}
		public List<TerminalNode> MEMBER_NAME() { return getTokens(MCSharpParser.MEMBER_NAME); }
		public TerminalNode MEMBER_NAME(int i) {
			return getToken(MCSharpParser.MEMBER_NAME, i);
		}
		public List<TerminalNode> VARIABLE_NAME() { return getTokens(MCSharpParser.VARIABLE_NAME); }
		public TerminalNode VARIABLE_NAME(int i) {
			return getToken(MCSharpParser.VARIABLE_NAME, i);
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
		enterRule(_localctx, 58, RULE_identifier);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(426);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__14) {
				{
				setState(425);
				match(T__14);
				}
			}

			setState(428);
			_la = _input.LA(1);
			if ( !(((((_la - 79)) & ~0x3f) == 0 && ((1L << (_la - 79)) & ((1L << (TYPE_NAME - 79)) | (1L << (MEMBER_NAME - 79)) | (1L << (VARIABLE_NAME - 79)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(435);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,38,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					setState(433);
					_errHandler.sync(this);
					switch (_input.LA(1)) {
					case T__15:
						{
						{
						setState(429);
						match(T__15);
						}
						setState(430);
						match(TYPE_NAME);
						}
						break;
					case MEMBER_NAME:
						{
						setState(431);
						match(MEMBER_NAME);
						}
						break;
					case VARIABLE_NAME:
						{
						setState(432);
						match(VARIABLE_NAME);
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					} 
				}
				setState(437);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,38,_ctx);
			}
			setState(439);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,39,_ctx) ) {
			case 1:
				{
				setState(438);
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
		enterRule(_localctx, 60, RULE_statement);
		try {
			setState(449);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,40,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(441);
				code_block();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(442);
				language_function();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(443);
				initialization_expression();
				setState(444);
				match(T__10);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(446);
				expression();
				setState(447);
				match(T__10);
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
		enterRule(_localctx, 62, RULE_code_block);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(451);
			match(T__8);
			setState(455);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__3) | (1L << T__8) | (1L << T__14) | (1L << T__16) | (1L << T__17) | (1L << T__18) | (1L << T__20) | (1L << T__21) | (1L << T__22) | (1L << T__23) | (1L << T__24) | (1L << T__34) | (1L << T__43) | (1L << T__44) | (1L << T__45) | (1L << T__51) | (1L << T__52) | (1L << T__53) | (1L << T__54))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (T__66 - 67)) | (1L << (T__67 - 67)) | (1L << (T__68 - 67)) | (1L << (T__69 - 67)) | (1L << (T__70 - 67)) | (1L << (T__72 - 67)) | (1L << (INTEGER - 67)) | (1L << (DECIMAL - 67)) | (1L << (STRING - 67)) | (1L << (TYPE_NAME - 67)) | (1L << (MEMBER_NAME - 67)) | (1L << (VARIABLE_NAME - 67)))) != 0)) {
				{
				{
				setState(452);
				statement();
				}
				}
				setState(457);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(458);
			match(T__9);
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
		enterRule(_localctx, 64, RULE_language_function);
		try {
			setState(468);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__16:
				enterOuterAlt(_localctx, 1);
				{
				setState(460);
				if_statement();
				}
				break;
			case T__17:
				enterOuterAlt(_localctx, 2);
				{
				setState(461);
				for_statement();
				}
				break;
			case T__18:
				enterOuterAlt(_localctx, 3);
				{
				setState(462);
				foreach_statement();
				}
				break;
			case T__20:
				enterOuterAlt(_localctx, 4);
				{
				setState(463);
				while_statement();
				}
				break;
			case T__21:
				enterOuterAlt(_localctx, 5);
				{
				setState(464);
				do_statement();
				}
				break;
			case T__22:
				enterOuterAlt(_localctx, 6);
				{
				setState(465);
				return_statement();
				}
				break;
			case T__23:
				enterOuterAlt(_localctx, 7);
				{
				setState(466);
				throw_statement();
				}
				break;
			case T__24:
				enterOuterAlt(_localctx, 8);
				{
				setState(467);
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
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public If_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_if_statement; }
	}

	public final If_statementContext if_statement() throws RecognitionException {
		If_statementContext _localctx = new If_statementContext(_ctx, getState());
		enterRule(_localctx, 66, RULE_if_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(470);
			match(T__16);
			setState(471);
			match(T__3);
			setState(472);
			expression();
			setState(473);
			match(T__4);
			setState(474);
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

	public static class For_statementContext extends ParserRuleContext {
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
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
		enterRule(_localctx, 68, RULE_for_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(476);
			match(T__17);
			setState(477);
			match(T__3);
			setState(478);
			expression();
			setState(479);
			match(T__10);
			setState(480);
			expression();
			setState(481);
			match(T__10);
			setState(482);
			expression();
			setState(483);
			match(T__4);
			setState(484);
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
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public TerminalNode VARIABLE_NAME() { return getToken(MCSharpParser.VARIABLE_NAME, 0); }
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
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
		enterRule(_localctx, 70, RULE_foreach_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(486);
			match(T__18);
			setState(487);
			match(T__3);
			setState(488);
			match(TYPE_NAME);
			setState(489);
			match(VARIABLE_NAME);
			setState(490);
			match(T__19);
			setState(491);
			expression();
			setState(492);
			match(T__4);
			setState(493);
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
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
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
		enterRule(_localctx, 72, RULE_while_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(495);
			match(T__20);
			setState(496);
			match(T__3);
			setState(497);
			expression();
			setState(498);
			match(T__4);
			setState(499);
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
		public StatementContext statement() {
			return getRuleContext(StatementContext.class,0);
		}
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Do_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_do_statement; }
	}

	public final Do_statementContext do_statement() throws RecognitionException {
		Do_statementContext _localctx = new Do_statementContext(_ctx, getState());
		enterRule(_localctx, 74, RULE_do_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(501);
			match(T__21);
			setState(502);
			statement();
			setState(503);
			match(T__20);
			setState(504);
			match(T__3);
			setState(505);
			expression();
			setState(506);
			match(T__4);
			setState(507);
			match(T__10);
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
		enterRule(_localctx, 76, RULE_return_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(509);
			match(T__22);
			setState(510);
			expression();
			setState(511);
			match(T__10);
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
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Throw_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_throw_statement; }
	}

	public final Throw_statementContext throw_statement() throws RecognitionException {
		Throw_statementContext _localctx = new Throw_statementContext(_ctx, getState());
		enterRule(_localctx, 78, RULE_throw_statement);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(513);
			match(T__23);
			setState(514);
			expression();
			setState(515);
			match(T__10);
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
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public List<TerminalNode> TYPE_NAME() { return getTokens(MCSharpParser.TYPE_NAME); }
		public TerminalNode TYPE_NAME(int i) {
			return getToken(MCSharpParser.TYPE_NAME, i);
		}
		public List<TerminalNode> VARIABLE_NAME() { return getTokens(MCSharpParser.VARIABLE_NAME); }
		public TerminalNode VARIABLE_NAME(int i) {
			return getToken(MCSharpParser.VARIABLE_NAME, i);
		}
		public Try_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_try_statement; }
	}

	public final Try_statementContext try_statement() throws RecognitionException {
		Try_statementContext _localctx = new Try_statementContext(_ctx, getState());
		enterRule(_localctx, 80, RULE_try_statement);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(517);
			match(T__24);
			setState(518);
			statement();
			setState(527);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,43,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(519);
					match(T__25);
					setState(520);
					match(T__3);
					setState(521);
					match(TYPE_NAME);
					setState(522);
					match(VARIABLE_NAME);
					setState(523);
					match(T__4);
					setState(524);
					statement();
					}
					} 
				}
				setState(529);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,43,_ctx);
			}
			setState(532);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,44,_ctx) ) {
			case 1:
				{
				setState(530);
				match(T__26);
				setState(531);
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
		enterRule(_localctx, 82, RULE_expression);
		try {
			setState(536);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,45,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(534);
				non_assignment_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(535);
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
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public TerminalNode VARIABLE_NAME() { return getToken(MCSharpParser.VARIABLE_NAME, 0); }
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
		enterRule(_localctx, 84, RULE_initialization_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(538);
			match(TYPE_NAME);
			setState(539);
			match(VARIABLE_NAME);
			{
			setState(540);
			match(T__7);
			setState(541);
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
		enterRule(_localctx, 86, RULE_non_assignment_expression);
		try {
			setState(545);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,46,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(543);
				conditional_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(544);
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
		enterRule(_localctx, 88, RULE_lambda_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(547);
			method_arguments();
			setState(548);
			match(T__11);
			{
			setState(549);
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
		public Expression_listContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_expression_list; }
	}

	public final Expression_listContext expression_list() throws RecognitionException {
		Expression_listContext _localctx = new Expression_listContext(_ctx, getState());
		enterRule(_localctx, 90, RULE_expression_list);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(551);
			expression();
			setState(556);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__0) {
				{
				{
				setState(552);
				match(T__0);
				setState(553);
				expression();
				}
				}
				setState(558);
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
		public List<ExpressionContext> expression() {
			return getRuleContexts(ExpressionContext.class);
		}
		public ExpressionContext expression(int i) {
			return getRuleContext(ExpressionContext.class,i);
		}
		public Conditional_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_conditional_expression; }
	}

	public final Conditional_expressionContext conditional_expression() throws RecognitionException {
		Conditional_expressionContext _localctx = new Conditional_expressionContext(_ctx, getState());
		enterRule(_localctx, 92, RULE_conditional_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(559);
			null_coalescing_expression();
			setState(565);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,48,_ctx) ) {
			case 1:
				{
				{
				setState(560);
				match(T__27);
				}
				setState(561);
				expression();
				{
				setState(562);
				match(T__28);
				}
				setState(563);
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
		enterRule(_localctx, 94, RULE_null_coalescing_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(567);
			conditional_or_expression();
			setState(570);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,49,_ctx) ) {
			case 1:
				{
				{
				setState(568);
				match(T__29);
				}
				setState(569);
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
		public Conditional_or_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_conditional_or_expression; }
	}

	public final Conditional_or_expressionContext conditional_or_expression() throws RecognitionException {
		Conditional_or_expressionContext _localctx = new Conditional_or_expressionContext(_ctx, getState());
		enterRule(_localctx, 96, RULE_conditional_or_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(572);
			conditional_and_expression();
			setState(577);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,50,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					{
					setState(573);
					match(T__30);
					}
					setState(574);
					conditional_and_expression();
					}
					} 
				}
				setState(579);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,50,_ctx);
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
		public Conditional_and_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_conditional_and_expression; }
	}

	public final Conditional_and_expressionContext conditional_and_expression() throws RecognitionException {
		Conditional_and_expressionContext _localctx = new Conditional_and_expressionContext(_ctx, getState());
		enterRule(_localctx, 98, RULE_conditional_and_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(580);
			inclusive_or_expression();
			setState(585);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,51,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					{
					setState(581);
					match(T__31);
					}
					setState(582);
					inclusive_or_expression();
					}
					} 
				}
				setState(587);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,51,_ctx);
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
		public Inclusive_or_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_inclusive_or_expression; }
	}

	public final Inclusive_or_expressionContext inclusive_or_expression() throws RecognitionException {
		Inclusive_or_expressionContext _localctx = new Inclusive_or_expressionContext(_ctx, getState());
		enterRule(_localctx, 100, RULE_inclusive_or_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(588);
			exclusive_or_expression();
			setState(593);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,52,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					{
					setState(589);
					match(T__32);
					}
					setState(590);
					exclusive_or_expression();
					}
					} 
				}
				setState(595);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,52,_ctx);
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
		public Exclusive_or_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_exclusive_or_expression; }
	}

	public final Exclusive_or_expressionContext exclusive_or_expression() throws RecognitionException {
		Exclusive_or_expressionContext _localctx = new Exclusive_or_expressionContext(_ctx, getState());
		enterRule(_localctx, 102, RULE_exclusive_or_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(596);
			and_expression();
			setState(601);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,53,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					{
					setState(597);
					match(T__33);
					}
					setState(598);
					and_expression();
					}
					} 
				}
				setState(603);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,53,_ctx);
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
		public And_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_and_expression; }
	}

	public final And_expressionContext and_expression() throws RecognitionException {
		And_expressionContext _localctx = new And_expressionContext(_ctx, getState());
		enterRule(_localctx, 104, RULE_and_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(604);
			equality_expression();
			setState(609);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,54,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					{
					setState(605);
					match(T__34);
					}
					setState(606);
					equality_expression();
					}
					} 
				}
				setState(611);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,54,_ctx);
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
		public Equality_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_equality_expression; }
	}

	public final Equality_expressionContext equality_expression() throws RecognitionException {
		Equality_expressionContext _localctx = new Equality_expressionContext(_ctx, getState());
		enterRule(_localctx, 106, RULE_equality_expression);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(612);
			relational_expression();
			setState(617);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,55,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(613);
					_la = _input.LA(1);
					if ( !(_la==T__35 || _la==T__36) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					setState(614);
					relational_expression();
					}
					} 
				}
				setState(619);
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
		enterRule(_localctx, 108, RULE_relational_expression);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(620);
			shift_expression();
			setState(624);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,56,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(621);
					relation_or_type_check();
					}
					} 
				}
				setState(626);
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

	public static class Relation_or_type_checkContext extends ParserRuleContext {
		public Shift_expressionContext shift_expression() {
			return getRuleContext(Shift_expressionContext.class,0);
		}
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public Relation_or_type_checkContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_relation_or_type_check; }
	}

	public final Relation_or_type_checkContext relation_or_type_check() throws RecognitionException {
		Relation_or_type_checkContext _localctx = new Relation_or_type_checkContext(_ctx, getState());
		enterRule(_localctx, 110, RULE_relation_or_type_check);
		int _la;
		try {
			setState(631);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__1:
			case T__2:
			case T__37:
			case T__38:
				enterOuterAlt(_localctx, 1);
				{
				setState(627);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__1) | (1L << T__2) | (1L << T__37) | (1L << T__38))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(628);
				shift_expression();
				}
				break;
			case T__39:
			case T__40:
				enterOuterAlt(_localctx, 2);
				{
				setState(629);
				_la = _input.LA(1);
				if ( !(_la==T__39 || _la==T__40) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(630);
				match(TYPE_NAME);
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
		public Shift_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_shift_expression; }
	}

	public final Shift_expressionContext shift_expression() throws RecognitionException {
		Shift_expressionContext _localctx = new Shift_expressionContext(_ctx, getState());
		enterRule(_localctx, 112, RULE_shift_expression);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(633);
			additive_expression();
			setState(638);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,58,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(634);
					_la = _input.LA(1);
					if ( !(_la==T__41 || _la==T__42) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					setState(635);
					additive_expression();
					}
					} 
				}
				setState(640);
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

	public static class Additive_expressionContext extends ParserRuleContext {
		public List<Multiplicative_expressionContext> multiplicative_expression() {
			return getRuleContexts(Multiplicative_expressionContext.class);
		}
		public Multiplicative_expressionContext multiplicative_expression(int i) {
			return getRuleContext(Multiplicative_expressionContext.class,i);
		}
		public Additive_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_additive_expression; }
	}

	public final Additive_expressionContext additive_expression() throws RecognitionException {
		Additive_expressionContext _localctx = new Additive_expressionContext(_ctx, getState());
		enterRule(_localctx, 114, RULE_additive_expression);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(641);
			multiplicative_expression();
			setState(646);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,59,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(642);
					_la = _input.LA(1);
					if ( !(_la==T__43 || _la==T__44) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					setState(643);
					multiplicative_expression();
					}
					} 
				}
				setState(648);
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

	public static class Multiplicative_expressionContext extends ParserRuleContext {
		public List<With_expressionContext> with_expression() {
			return getRuleContexts(With_expressionContext.class);
		}
		public With_expressionContext with_expression(int i) {
			return getRuleContext(With_expressionContext.class,i);
		}
		public Multiplicative_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_multiplicative_expression; }
	}

	public final Multiplicative_expressionContext multiplicative_expression() throws RecognitionException {
		Multiplicative_expressionContext _localctx = new Multiplicative_expressionContext(_ctx, getState());
		enterRule(_localctx, 116, RULE_multiplicative_expression);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(649);
			with_expression();
			setState(654);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,60,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(650);
					_la = _input.LA(1);
					if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__45) | (1L << T__46) | (1L << T__47))) != 0)) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					setState(651);
					with_expression();
					}
					} 
				}
				setState(656);
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

	public static class With_expressionContext extends ParserRuleContext {
		public Range_expressionContext range_expression() {
			return getRuleContext(Range_expressionContext.class,0);
		}
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
		enterRule(_localctx, 118, RULE_with_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(657);
			range_expression();
			setState(660);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__48) {
				{
				{
				setState(658);
				match(T__48);
				}
				setState(659);
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
		public Range_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_range_expression; }
	}

	public final Range_expressionContext range_expression() throws RecognitionException {
		Range_expressionContext _localctx = new Range_expressionContext(_ctx, getState());
		enterRule(_localctx, 120, RULE_range_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(662);
			unary_expression();
			setState(665);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__49 || _la==T__50) {
				{
				setState(663);
				_la = _input.LA(1);
				if ( !(_la==T__49 || _la==T__50) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(664);
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

	public static class Unary_expressionContext extends ParserRuleContext {
		public Primary_expressionContext primary_expression() {
			return getRuleContext(Primary_expressionContext.class,0);
		}
		public Unary_expressionContext unary_expression() {
			return getRuleContext(Unary_expressionContext.class,0);
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
		enterRule(_localctx, 122, RULE_unary_expression);
		int _la;
		try {
			setState(675);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,63,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(667);
				primary_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(668);
				_la = _input.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__43) | (1L << T__44) | (1L << T__51) | (1L << T__52))) != 0)) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(669);
				unary_expression();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(670);
				_la = _input.LA(1);
				if ( !(_la==T__53 || _la==T__54) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(671);
				unary_expression();
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(672);
				cast_expression();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(673);
				pointer_indirection_expression();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(674);
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
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
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
		enterRule(_localctx, 124, RULE_cast_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(677);
			match(T__3);
			setState(678);
			match(TYPE_NAME);
			setState(679);
			match(T__4);
			setState(680);
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
		enterRule(_localctx, 126, RULE_pointer_indirection_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(682);
			match(T__45);
			setState(683);
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
		enterRule(_localctx, 128, RULE_addressof_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(685);
			match(T__34);
			setState(686);
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
		enterRule(_localctx, 130, RULE_assignment_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(688);
			unary_expression();
			setState(689);
			_la = _input.LA(1);
			if ( !(((((_la - 8)) & ~0x3f) == 0 && ((1L << (_la - 8)) & ((1L << (T__7 - 8)) | (1L << (T__55 - 8)) | (1L << (T__56 - 8)) | (1L << (T__57 - 8)) | (1L << (T__58 - 8)) | (1L << (T__59 - 8)) | (1L << (T__60 - 8)) | (1L << (T__61 - 8)) | (1L << (T__62 - 8)) | (1L << (T__63 - 8)) | (1L << (T__64 - 8)) | (1L << (T__65 - 8)))) != 0)) ) {
			_errHandler.recoverInline(this);
			}
			else {
				if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
				_errHandler.reportMatch(this);
				consume();
			}
			setState(690);
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
		enterRule(_localctx, 132, RULE_primary_expression);
		try {
			setState(694);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,64,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(692);
				array_creation_expression();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(693);
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
		enterRule(_localctx, 134, RULE_array_creation_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(696);
			match(T__66);
			setState(697);
			indexer_arguments();
			setState(699);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__5) {
				{
				setState(698);
				array_rank_specifier();
				}
			}

			setState(702);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__8) {
				{
				setState(701);
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
		public Array_rank_specifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_array_rank_specifier; }
	}

	public final Array_rank_specifierContext array_rank_specifier() throws RecognitionException {
		Array_rank_specifierContext _localctx = new Array_rank_specifierContext(_ctx, getState());
		enterRule(_localctx, 136, RULE_array_rank_specifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(704);
			match(T__5);
			setState(708);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__0) {
				{
				{
				setState(705);
				match(T__0);
				}
				}
				setState(710);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(711);
			match(T__6);
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
		public List<Variable_initializerContext> variable_initializer() {
			return getRuleContexts(Variable_initializerContext.class);
		}
		public Variable_initializerContext variable_initializer(int i) {
			return getRuleContext(Variable_initializerContext.class,i);
		}
		public Array_initializerContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_array_initializer; }
	}

	public final Array_initializerContext array_initializer() throws RecognitionException {
		Array_initializerContext _localctx = new Array_initializerContext(_ctx, getState());
		enterRule(_localctx, 138, RULE_array_initializer);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(713);
			match(T__8);
			setState(725);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__3) | (1L << T__8) | (1L << T__14) | (1L << T__34) | (1L << T__43) | (1L << T__44) | (1L << T__45) | (1L << T__51) | (1L << T__52) | (1L << T__53) | (1L << T__54))) != 0) || ((((_la - 67)) & ~0x3f) == 0 && ((1L << (_la - 67)) & ((1L << (T__66 - 67)) | (1L << (T__67 - 67)) | (1L << (T__68 - 67)) | (1L << (T__69 - 67)) | (1L << (T__70 - 67)) | (1L << (T__72 - 67)) | (1L << (INTEGER - 67)) | (1L << (DECIMAL - 67)) | (1L << (STRING - 67)) | (1L << (TYPE_NAME - 67)) | (1L << (MEMBER_NAME - 67)) | (1L << (VARIABLE_NAME - 67)))) != 0)) {
				{
				setState(714);
				variable_initializer();
				setState(719);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,68,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(715);
						match(T__0);
						setState(716);
						variable_initializer();
						}
						} 
					}
					setState(721);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,68,_ctx);
				}
				setState(723);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(722);
					match(T__0);
					}
				}

				}
			}

			setState(727);
			match(T__9);
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
		enterRule(_localctx, 140, RULE_variable_initializer);
		try {
			setState(731);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__3:
			case T__14:
			case T__34:
			case T__43:
			case T__44:
			case T__45:
			case T__51:
			case T__52:
			case T__53:
			case T__54:
			case T__66:
			case T__67:
			case T__68:
			case T__69:
			case T__70:
			case T__72:
			case INTEGER:
			case DECIMAL:
			case STRING:
			case TYPE_NAME:
			case MEMBER_NAME:
			case VARIABLE_NAME:
				enterOuterAlt(_localctx, 1);
				{
				setState(729);
				expression();
				}
				break;
			case T__8:
				enterOuterAlt(_localctx, 2);
				{
				setState(730);
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
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Member_accessContext member_access() {
			return getRuleContext(Member_accessContext.class,0);
		}
		public Invocation_expressionContext invocation_expression() {
			return getRuleContext(Invocation_expressionContext.class,0);
		}
		public Indexer_expressionContext indexer_expression() {
			return getRuleContext(Indexer_expressionContext.class,0);
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
		enterRule(_localctx, 142, RULE_primary_no_array_creation_expression);
		try {
			setState(744);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,72,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(733);
				literal();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(734);
				identifier();
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(735);
				match(T__3);
				setState(736);
				expression();
				setState(737);
				match(T__4);
				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(739);
				member_access();
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(740);
				invocation_expression();
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(741);
				indexer_expression();
				}
				break;
			case 7:
				enterOuterAlt(_localctx, 7);
				{
				setState(742);
				post_step_expression();
				}
				break;
			case 8:
				enterOuterAlt(_localctx, 8);
				{
				setState(743);
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
		public Primary_expressionContext primary_expression() {
			return getRuleContext(Primary_expressionContext.class,0);
		}
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public Generic_argumentsContext generic_arguments() {
			return getRuleContext(Generic_argumentsContext.class,0);
		}
		public Member_accessContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_member_access; }
	}

	public final Member_accessContext member_access() throws RecognitionException {
		Member_accessContext _localctx = new Member_accessContext(_ctx, getState());
		enterRule(_localctx, 144, RULE_member_access);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(751);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__3:
				{
				setState(746);
				match(T__3);
				setState(747);
				primary_expression();
				setState(748);
				match(T__4);
				}
				break;
			case TYPE_NAME:
				{
				setState(750);
				match(TYPE_NAME);
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(753);
			match(T__15);
			setState(754);
			identifier();
			setState(756);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,74,_ctx) ) {
			case 1:
				{
				setState(755);
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

	public static class Invocation_expressionContext extends ParserRuleContext {
		public Member_accessContext member_access() {
			return getRuleContext(Member_accessContext.class,0);
		}
		public Method_argumentsContext method_arguments() {
			return getRuleContext(Method_argumentsContext.class,0);
		}
		public Invocation_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_invocation_expression; }
	}

	public final Invocation_expressionContext invocation_expression() throws RecognitionException {
		Invocation_expressionContext _localctx = new Invocation_expressionContext(_ctx, getState());
		enterRule(_localctx, 146, RULE_invocation_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(758);
			member_access();
			setState(759);
			method_arguments();
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

	public static class Indexer_expressionContext extends ParserRuleContext {
		public Member_accessContext member_access() {
			return getRuleContext(Member_accessContext.class,0);
		}
		public Indexer_argumentsContext indexer_arguments() {
			return getRuleContext(Indexer_argumentsContext.class,0);
		}
		public Indexer_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_indexer_expression; }
	}

	public final Indexer_expressionContext indexer_expression() throws RecognitionException {
		Indexer_expressionContext _localctx = new Indexer_expressionContext(_ctx, getState());
		enterRule(_localctx, 148, RULE_indexer_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(761);
			member_access();
			setState(762);
			indexer_arguments();
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
		enterRule(_localctx, 150, RULE_post_step_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(766);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case INTEGER:
			case DECIMAL:
			case STRING:
				{
				setState(764);
				literal();
				}
				break;
			case T__14:
			case TYPE_NAME:
			case MEMBER_NAME:
			case VARIABLE_NAME:
				{
				setState(765);
				identifier();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			setState(768);
			_la = _input.LA(1);
			if ( !(_la==T__53 || _la==T__54) ) {
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
		enterRule(_localctx, 152, RULE_keyword_expression);
		try {
			setState(776);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__66:
				enterOuterAlt(_localctx, 1);
				{
				setState(770);
				new_keyword_expression();
				}
				break;
			case T__67:
				enterOuterAlt(_localctx, 2);
				{
				setState(771);
				typeof_keyword_expression();
				}
				break;
			case T__68:
				enterOuterAlt(_localctx, 3);
				{
				setState(772);
				checked_expression();
				}
				break;
			case T__69:
				enterOuterAlt(_localctx, 4);
				{
				setState(773);
				unchecked_expression();
				}
				break;
			case T__70:
				enterOuterAlt(_localctx, 5);
				{
				setState(774);
				default_keyword_expression();
				}
				break;
			case T__72:
				enterOuterAlt(_localctx, 6);
				{
				setState(775);
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
		enterRule(_localctx, 154, RULE_object_or_collection_initializer);
		try {
			setState(780);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,77,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(778);
				object_initializer();
				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(779);
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
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public Method_argumentsContext method_arguments() {
			return getRuleContext(Method_argumentsContext.class,0);
		}
		public Object_or_collection_initializerContext object_or_collection_initializer() {
			return getRuleContext(Object_or_collection_initializerContext.class,0);
		}
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
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
		enterRule(_localctx, 156, RULE_new_keyword_expression);
		int _la;
		try {
			setState(799);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,80,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(782);
				match(T__66);
				setState(783);
				match(TYPE_NAME);
				setState(789);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case T__3:
					{
					{
					setState(784);
					method_arguments();
					setState(786);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (_la==T__8) {
						{
						setState(785);
						object_or_collection_initializer();
						}
					}

					}
					}
					break;
				case T__8:
					{
					{
					setState(788);
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
				setState(791);
				match(T__66);
				setState(792);
				match(TYPE_NAME);
				{
				setState(793);
				match(T__3);
				setState(794);
				expression();
				setState(795);
				match(T__4);
				}
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(797);
				match(T__66);
				setState(798);
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
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public Typeof_keyword_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_typeof_keyword_expression; }
	}

	public final Typeof_keyword_expressionContext typeof_keyword_expression() throws RecognitionException {
		Typeof_keyword_expressionContext _localctx = new Typeof_keyword_expressionContext(_ctx, getState());
		enterRule(_localctx, 158, RULE_typeof_keyword_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(801);
			match(T__67);
			setState(802);
			match(T__3);
			{
			setState(803);
			match(TYPE_NAME);
			}
			setState(804);
			match(T__4);
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
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Checked_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_checked_expression; }
	}

	public final Checked_expressionContext checked_expression() throws RecognitionException {
		Checked_expressionContext _localctx = new Checked_expressionContext(_ctx, getState());
		enterRule(_localctx, 160, RULE_checked_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(806);
			match(T__68);
			setState(807);
			match(T__3);
			setState(808);
			expression();
			setState(809);
			match(T__4);
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
		public ExpressionContext expression() {
			return getRuleContext(ExpressionContext.class,0);
		}
		public Unchecked_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_unchecked_expression; }
	}

	public final Unchecked_expressionContext unchecked_expression() throws RecognitionException {
		Unchecked_expressionContext _localctx = new Unchecked_expressionContext(_ctx, getState());
		enterRule(_localctx, 162, RULE_unchecked_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(811);
			match(T__69);
			setState(812);
			match(T__3);
			setState(813);
			expression();
			setState(814);
			match(T__4);
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
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public Default_keyword_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_default_keyword_expression; }
	}

	public final Default_keyword_expressionContext default_keyword_expression() throws RecognitionException {
		Default_keyword_expressionContext _localctx = new Default_keyword_expressionContext(_ctx, getState());
		enterRule(_localctx, 164, RULE_default_keyword_expression);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(816);
			match(T__70);
			setState(820);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__3) {
				{
				setState(817);
				match(T__3);
				setState(818);
				match(TYPE_NAME);
				setState(819);
				match(T__4);
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
		enterRule(_localctx, 166, RULE_delegate_keyword_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(822);
			match(T__71);
			setState(823);
			method_parameters();
			setState(824);
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
		public TerminalNode TYPE_NAME() { return getToken(MCSharpParser.TYPE_NAME, 0); }
		public Sizeof_keyword_expressionContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_sizeof_keyword_expression; }
	}

	public final Sizeof_keyword_expressionContext sizeof_keyword_expression() throws RecognitionException {
		Sizeof_keyword_expressionContext _localctx = new Sizeof_keyword_expressionContext(_ctx, getState());
		enterRule(_localctx, 168, RULE_sizeof_keyword_expression);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(826);
			match(T__72);
			setState(827);
			match(T__3);
			setState(828);
			match(TYPE_NAME);
			setState(829);
			match(T__4);
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3W\u0342\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\tC\4D\tD\4E\tE\4F\tF\4G\tG\4H\tH\4I"+
		"\tI\4J\tJ\4K\tK\4L\tL\4M\tM\4N\tN\4O\tO\4P\tP\4Q\tQ\4R\tR\4S\tS\4T\tT"+
		"\4U\tU\4V\tV\3\2\3\2\3\3\3\3\3\3\7\3\u00b2\n\3\f\3\16\3\u00b5\13\3\3\4"+
		"\3\4\3\4\3\4\3\5\5\5\u00bc\n\5\3\5\3\5\3\5\3\6\3\6\3\6\7\6\u00c4\n\6\f"+
		"\6\16\6\u00c7\13\6\3\7\3\7\3\7\3\7\3\b\3\b\3\b\3\b\3\t\3\t\3\t\3\t\5\t"+
		"\u00d5\n\t\3\n\3\n\3\n\7\n\u00da\n\n\f\n\16\n\u00dd\13\n\3\13\3\13\3\13"+
		"\3\13\3\f\3\f\3\f\3\f\3\r\3\r\3\r\3\r\3\16\3\16\5\16\u00ed\n\16\3\16\3"+
		"\16\3\16\5\16\u00f2\n\16\3\17\3\17\3\17\3\17\5\17\u00f8\n\17\3\17\5\17"+
		"\u00fb\n\17\5\17\u00fd\n\17\3\17\3\17\3\20\3\20\3\20\3\20\3\20\5\20\u0106"+
		"\n\20\3\21\3\21\3\21\3\21\5\21\u010c\n\21\3\21\5\21\u010f\n\21\5\21\u0111"+
		"\n\21\3\21\3\21\3\22\3\22\5\22\u0117\n\22\3\22\3\22\3\22\3\22\3\22\5\22"+
		"\u011e\n\22\3\23\3\23\3\23\3\23\5\23\u0124\n\23\3\23\5\23\u0127\n\23\5"+
		"\23\u0129\n\23\3\23\3\23\3\24\7\24\u012e\n\24\f\24\16\24\u0131\13\24\3"+
		"\24\3\24\3\24\7\24\u0136\n\24\f\24\16\24\u0139\13\24\3\24\3\24\3\25\3"+
		"\25\3\25\5\25\u0140\n\25\3\26\7\26\u0143\n\26\f\26\16\26\u0146\13\26\3"+
		"\26\3\26\3\26\3\27\3\27\5\27\u014d\n\27\3\27\3\27\3\30\7\30\u0152\n\30"+
		"\f\30\16\30\u0155\13\30\3\30\3\30\3\30\3\31\3\31\3\31\3\31\7\31\u015e"+
		"\n\31\f\31\16\31\u0161\13\31\3\31\3\31\3\31\7\31\u0166\n\31\f\31\16\31"+
		"\u0169\13\31\3\31\5\31\u016c\n\31\3\31\7\31\u016f\n\31\f\31\16\31\u0172"+
		"\13\31\3\31\3\31\3\31\7\31\u0177\n\31\f\31\16\31\u017a\13\31\3\31\5\31"+
		"\u017d\n\31\3\31\3\31\5\31\u0181\n\31\3\32\3\32\3\32\3\32\3\32\3\32\3"+
		"\32\3\32\3\32\5\32\u018c\n\32\3\33\3\33\3\33\3\33\3\33\3\33\3\33\3\33"+
		"\3\33\5\33\u0197\n\33\3\34\7\34\u019a\n\34\f\34\16\34\u019d\13\34\3\34"+
		"\3\34\3\34\3\34\3\35\3\35\3\35\3\35\3\35\5\35\u01a8\n\35\3\36\3\36\3\37"+
		"\5\37\u01ad\n\37\3\37\3\37\3\37\3\37\3\37\7\37\u01b4\n\37\f\37\16\37\u01b7"+
		"\13\37\3\37\5\37\u01ba\n\37\3 \3 \3 \3 \3 \3 \3 \3 \5 \u01c4\n \3!\3!"+
		"\7!\u01c8\n!\f!\16!\u01cb\13!\3!\3!\3\"\3\"\3\"\3\"\3\"\3\"\3\"\3\"\5"+
		"\"\u01d7\n\"\3#\3#\3#\3#\3#\3#\3$\3$\3$\3$\3$\3$\3$\3$\3$\3$\3%\3%\3%"+
		"\3%\3%\3%\3%\3%\3%\3&\3&\3&\3&\3&\3&\3\'\3\'\3\'\3\'\3\'\3\'\3\'\3\'\3"+
		"(\3(\3(\3(\3)\3)\3)\3)\3*\3*\3*\3*\3*\3*\3*\3*\7*\u0210\n*\f*\16*\u0213"+
		"\13*\3*\3*\5*\u0217\n*\3+\3+\5+\u021b\n+\3,\3,\3,\3,\3,\3-\3-\5-\u0224"+
		"\n-\3.\3.\3.\3.\3/\3/\3/\7/\u022d\n/\f/\16/\u0230\13/\3\60\3\60\3\60\3"+
		"\60\3\60\3\60\5\60\u0238\n\60\3\61\3\61\3\61\5\61\u023d\n\61\3\62\3\62"+
		"\3\62\7\62\u0242\n\62\f\62\16\62\u0245\13\62\3\63\3\63\3\63\7\63\u024a"+
		"\n\63\f\63\16\63\u024d\13\63\3\64\3\64\3\64\7\64\u0252\n\64\f\64\16\64"+
		"\u0255\13\64\3\65\3\65\3\65\7\65\u025a\n\65\f\65\16\65\u025d\13\65\3\66"+
		"\3\66\3\66\7\66\u0262\n\66\f\66\16\66\u0265\13\66\3\67\3\67\3\67\7\67"+
		"\u026a\n\67\f\67\16\67\u026d\13\67\38\38\78\u0271\n8\f8\168\u0274\138"+
		"\39\39\39\39\59\u027a\n9\3:\3:\3:\7:\u027f\n:\f:\16:\u0282\13:\3;\3;\3"+
		";\7;\u0287\n;\f;\16;\u028a\13;\3<\3<\3<\7<\u028f\n<\f<\16<\u0292\13<\3"+
		"=\3=\3=\5=\u0297\n=\3>\3>\3>\5>\u029c\n>\3?\3?\3?\3?\3?\3?\3?\3?\5?\u02a6"+
		"\n?\3@\3@\3@\3@\3@\3A\3A\3A\3B\3B\3B\3C\3C\3C\3C\3D\3D\5D\u02b9\nD\3E"+
		"\3E\3E\5E\u02be\nE\3E\5E\u02c1\nE\3F\3F\7F\u02c5\nF\fF\16F\u02c8\13F\3"+
		"F\3F\3G\3G\3G\3G\7G\u02d0\nG\fG\16G\u02d3\13G\3G\5G\u02d6\nG\5G\u02d8"+
		"\nG\3G\3G\3H\3H\5H\u02de\nH\3I\3I\3I\3I\3I\3I\3I\3I\3I\3I\3I\5I\u02eb"+
		"\nI\3J\3J\3J\3J\3J\5J\u02f2\nJ\3J\3J\3J\5J\u02f7\nJ\3K\3K\3K\3L\3L\3L"+
		"\3M\3M\5M\u0301\nM\3M\3M\3N\3N\3N\3N\3N\3N\5N\u030b\nN\3O\3O\5O\u030f"+
		"\nO\3P\3P\3P\3P\5P\u0315\nP\3P\5P\u0318\nP\3P\3P\3P\3P\3P\3P\3P\3P\5P"+
		"\u0322\nP\3Q\3Q\3Q\3Q\3Q\3R\3R\3R\3R\3R\3S\3S\3S\3S\3S\3T\3T\3T\3T\5T"+
		"\u0337\nT\3U\3U\3U\3U\3V\3V\3V\3V\3V\3V\2\2W\2\4\6\b\n\f\16\20\22\24\26"+
		"\30\32\34\36 \"$&(*,.\60\62\64\668:<>@BDFHJLNPRTVXZ\\^`bdfhjlnprtvxz|"+
		"~\u0080\u0082\u0084\u0086\u0088\u008a\u008c\u008e\u0090\u0092\u0094\u0096"+
		"\u0098\u009a\u009c\u009e\u00a0\u00a2\u00a4\u00a6\u00a8\u00aa\2\16\3\2"+
		"NP\3\2QS\3\2&\'\4\2\4\5()\3\2*+\3\2,-\3\2./\3\2\60\62\3\2\64\65\4\2./"+
		"\66\67\3\289\4\2\n\n:D\2\u035b\2\u00ac\3\2\2\2\4\u00ae\3\2\2\2\6\u00b6"+
		"\3\2\2\2\b\u00bb\3\2\2\2\n\u00c0\3\2\2\2\f\u00c8\3\2\2\2\16\u00cc\3\2"+
		"\2\2\20\u00d4\3\2\2\2\22\u00d6\3\2\2\2\24\u00de\3\2\2\2\26\u00e2\3\2\2"+
		"\2\30\u00e6\3\2\2\2\32\u00ea\3\2\2\2\34\u00f3\3\2\2\2\36\u0105\3\2\2\2"+
		" \u0107\3\2\2\2\"\u011d\3\2\2\2$\u011f\3\2\2\2&\u012f\3\2\2\2(\u013f\3"+
		"\2\2\2*\u0144\3\2\2\2,\u014c\3\2\2\2.\u0153\3\2\2\2\60\u0180\3\2\2\2\62"+
		"\u018b\3\2\2\2\64\u0196\3\2\2\2\66\u019b\3\2\2\28\u01a7\3\2\2\2:\u01a9"+
		"\3\2\2\2<\u01ac\3\2\2\2>\u01c3\3\2\2\2@\u01c5\3\2\2\2B\u01d6\3\2\2\2D"+
		"\u01d8\3\2\2\2F\u01de\3\2\2\2H\u01e8\3\2\2\2J\u01f1\3\2\2\2L\u01f7\3\2"+
		"\2\2N\u01ff\3\2\2\2P\u0203\3\2\2\2R\u0207\3\2\2\2T\u021a\3\2\2\2V\u021c"+
		"\3\2\2\2X\u0223\3\2\2\2Z\u0225\3\2\2\2\\\u0229\3\2\2\2^\u0231\3\2\2\2"+
		"`\u0239\3\2\2\2b\u023e\3\2\2\2d\u0246\3\2\2\2f\u024e\3\2\2\2h\u0256\3"+
		"\2\2\2j\u025e\3\2\2\2l\u0266\3\2\2\2n\u026e\3\2\2\2p\u0279\3\2\2\2r\u027b"+
		"\3\2\2\2t\u0283\3\2\2\2v\u028b\3\2\2\2x\u0293\3\2\2\2z\u0298\3\2\2\2|"+
		"\u02a5\3\2\2\2~\u02a7\3\2\2\2\u0080\u02ac\3\2\2\2\u0082\u02af\3\2\2\2"+
		"\u0084\u02b2\3\2\2\2\u0086\u02b8\3\2\2\2\u0088\u02ba\3\2\2\2\u008a\u02c2"+
		"\3\2\2\2\u008c\u02cb\3\2\2\2\u008e\u02dd\3\2\2\2\u0090\u02ea\3\2\2\2\u0092"+
		"\u02f1\3\2\2\2\u0094\u02f8\3\2\2\2\u0096\u02fb\3\2\2\2\u0098\u0300\3\2"+
		"\2\2\u009a\u030a\3\2\2\2\u009c\u030e\3\2\2\2\u009e\u0321\3\2\2\2\u00a0"+
		"\u0323\3\2\2\2\u00a2\u0328\3\2\2\2\u00a4\u032d\3\2\2\2\u00a6\u0332\3\2"+
		"\2\2\u00a8\u0338\3\2\2\2\u00aa\u033c\3\2\2\2\u00ac\u00ad\7Q\2\2\u00ad"+
		"\3\3\2\2\2\u00ae\u00b3\5\2\2\2\u00af\u00b0\7\3\2\2\u00b0\u00b2\5\2\2\2"+
		"\u00b1\u00af\3\2\2\2\u00b2\u00b5\3\2\2\2\u00b3\u00b1\3\2\2\2\u00b3\u00b4"+
		"\3\2\2\2\u00b4\5\3\2\2\2\u00b5\u00b3\3\2\2\2\u00b6\u00b7\7\4\2\2\u00b7"+
		"\u00b8\5\4\3\2\u00b8\u00b9\7\5\2\2\u00b9\7\3\2\2\2\u00ba\u00bc\7W\2\2"+
		"\u00bb\u00ba\3\2\2\2\u00bb\u00bc\3\2\2\2\u00bc\u00bd\3\2\2\2\u00bd\u00be"+
		"\7Q\2\2\u00be\u00bf\7S\2\2\u00bf\t\3\2\2\2\u00c0\u00c5\5\b\5\2\u00c1\u00c2"+
		"\7\3\2\2\u00c2\u00c4\5\b\5\2\u00c3\u00c1\3\2\2\2\u00c4\u00c7\3\2\2\2\u00c5"+
		"\u00c3\3\2\2\2\u00c5\u00c6\3\2\2\2\u00c6\13\3\2\2\2\u00c7\u00c5\3\2\2"+
		"\2\u00c8\u00c9\7\6\2\2\u00c9\u00ca\5\n\6\2\u00ca\u00cb\7\7\2\2\u00cb\r"+
		"\3\2\2\2\u00cc\u00cd\7\b\2\2\u00cd\u00ce\5\n\6\2\u00ce\u00cf\7\t\2\2\u00cf"+
		"\17\3\2\2\2\u00d0\u00d5\5T+\2\u00d1\u00d2\7W\2\2\u00d2\u00d3\7Q\2\2\u00d3"+
		"\u00d5\7S\2\2\u00d4\u00d0\3\2\2\2\u00d4\u00d1\3\2\2\2\u00d5\21\3\2\2\2"+
		"\u00d6\u00db\5\20\t\2\u00d7\u00d8\7\3\2\2\u00d8\u00da\5\20\t\2\u00d9\u00d7"+
		"\3\2\2\2\u00da\u00dd\3\2\2\2\u00db\u00d9\3\2\2\2\u00db\u00dc\3\2\2\2\u00dc"+
		"\23\3\2\2\2\u00dd\u00db\3\2\2\2\u00de\u00df\7\4\2\2\u00df\u00e0\5\4\3"+
		"\2\u00e0\u00e1\7\5\2\2\u00e1\25\3\2\2\2\u00e2\u00e3\7\6\2\2\u00e3\u00e4"+
		"\5\22\n\2\u00e4\u00e5\7\7\2\2\u00e5\27\3\2\2\2\u00e6\u00e7\7\b\2\2\u00e7"+
		"\u00e8\5\22\n\2\u00e8\u00e9\7\t\2\2\u00e9\31\3\2\2\2\u00ea\u00ec\7R\2"+
		"\2\u00eb\u00ed\5\24\13\2\u00ec\u00eb\3\2\2\2\u00ec\u00ed\3\2\2\2\u00ed"+
		"\u00ee\3\2\2\2\u00ee\u00f1\7\n\2\2\u00ef\u00f2\5T+\2\u00f0\u00f2\5\u009c"+
		"O\2\u00f1\u00ef\3\2\2\2\u00f1\u00f0\3\2\2\2\u00f2\33\3\2\2\2\u00f3\u00fc"+
		"\7\13\2\2\u00f4\u00f7\5\32\16\2\u00f5\u00f6\7\3\2\2\u00f6\u00f8\5\32\16"+
		"\2\u00f7\u00f5\3\2\2\2\u00f7\u00f8\3\2\2\2\u00f8\u00fa\3\2\2\2\u00f9\u00fb"+
		"\7\3\2\2\u00fa\u00f9\3\2\2\2\u00fa\u00fb\3\2\2\2\u00fb\u00fd\3\2\2\2\u00fc"+
		"\u00f4\3\2\2\2\u00fc\u00fd\3\2\2\2\u00fd\u00fe\3\2\2\2\u00fe\u00ff\7\f"+
		"\2\2\u00ff\35\3\2\2\2\u0100\u0106\5X-\2\u0101\u0102\7\13\2\2\u0102\u0103"+
		"\5T+\2\u0103\u0104\7\f\2\2\u0104\u0106\3\2\2\2\u0105\u0100\3\2\2\2\u0105"+
		"\u0101\3\2\2\2\u0106\37\3\2\2\2\u0107\u0110\7\13\2\2\u0108\u010b\5\36"+
		"\20\2\u0109\u010a\7\3\2\2\u010a\u010c\5\36\20\2\u010b\u0109\3\2\2\2\u010b"+
		"\u010c\3\2\2\2\u010c\u010e\3\2\2\2\u010d\u010f\7\3\2\2\u010e\u010d\3\2"+
		"\2\2\u010e\u010f\3\2\2\2\u010f\u0111\3\2\2\2\u0110\u0108\3\2\2\2\u0110"+
		"\u0111\3\2\2\2\u0111\u0112\3\2\2\2\u0112\u0113\7\f\2\2\u0113!\3\2\2\2"+
		"\u0114\u0116\7R\2\2\u0115\u0117\5\24\13\2\u0116\u0115\3\2\2\2\u0116\u0117"+
		"\3\2\2\2\u0117\u011e\3\2\2\2\u0118\u011e\5\u0092J\2\u0119\u011a\5<\37"+
		"\2\u011a\u011b\7\n\2\2\u011b\u011c\5T+\2\u011c\u011e\3\2\2\2\u011d\u0114"+
		"\3\2\2\2\u011d\u0118\3\2\2\2\u011d\u0119\3\2\2\2\u011e#\3\2\2\2\u011f"+
		"\u0128\7\13\2\2\u0120\u0123\5\"\22\2\u0121\u0122\7\3\2\2\u0122\u0124\5"+
		"\"\22\2\u0123\u0121\3\2\2\2\u0123\u0124\3\2\2\2\u0124\u0126\3\2\2\2\u0125"+
		"\u0127\7\3\2\2\u0126\u0125\3\2\2\2\u0126\u0127\3\2\2\2\u0127\u0129\3\2"+
		"\2\2\u0128\u0120\3\2\2\2\u0128\u0129\3\2\2\2\u0129\u012a\3\2\2\2\u012a"+
		"\u012b\7\f\2\2\u012b%\3\2\2\2\u012c\u012e\7V\2\2\u012d\u012c\3\2\2\2\u012e"+
		"\u0131\3\2\2\2\u012f\u012d\3\2\2\2\u012f\u0130\3\2\2\2\u0130\u0132\3\2"+
		"\2\2\u0131\u012f\3\2\2\2\u0132\u0133\7Q\2\2\u0133\u0137\7\13\2\2\u0134"+
		"\u0136\5(\25\2\u0135\u0134\3\2\2\2\u0136\u0139\3\2\2\2\u0137\u0135\3\2"+
		"\2\2\u0137\u0138\3\2\2\2\u0138\u013a\3\2\2\2\u0139\u0137\3\2\2\2\u013a"+
		"\u013b\7\f\2\2\u013b\'\3\2\2\2\u013c\u0140\5*\26\2\u013d\u0140\5.\30\2"+
		"\u013e\u0140\5\66\34\2\u013f\u013c\3\2\2\2\u013f\u013d\3\2\2\2\u013f\u013e"+
		"\3\2\2\2\u0140)\3\2\2\2\u0141\u0143\7V\2\2\u0142\u0141\3\2\2\2\u0143\u0146"+
		"\3\2\2\2\u0144\u0142\3\2\2\2\u0144\u0145\3\2\2\2\u0145\u0147\3\2\2\2\u0146"+
		"\u0144\3\2\2\2\u0147\u0148\7R\2\2\u0148\u0149\5,\27\2\u0149+\3\2\2\2\u014a"+
		"\u014b\7\n\2\2\u014b\u014d\5T+\2\u014c\u014a\3\2\2\2\u014c\u014d\3\2\2"+
		"\2\u014d\u014e\3\2\2\2\u014e\u014f\7\r\2\2\u014f-\3\2\2\2\u0150\u0152"+
		"\7V\2\2\u0151\u0150\3\2\2\2\u0152\u0155\3\2\2\2\u0153\u0151\3\2\2\2\u0153"+
		"\u0154\3\2\2\2\u0154\u0156\3\2\2\2\u0155\u0153\3\2\2\2\u0156\u0157\7R"+
		"\2\2\u0157\u0158\5\60\31\2\u0158/\3\2\2\2\u0159\u015a\7\16\2\2\u015a\u0181"+
		"\5T+\2\u015b\u015f\7\13\2\2\u015c\u015e\7V\2\2\u015d\u015c\3\2\2\2\u015e"+
		"\u0161\3\2\2\2\u015f\u015d\3\2\2\2\u015f\u0160\3\2\2\2\u0160\u0162\3\2"+
		"\2\2\u0161\u015f\3\2\2\2\u0162\u0163\5\62\32\2\u0163\u016b\3\2\2\2\u0164"+
		"\u0166\7V\2\2\u0165\u0164\3\2\2\2\u0166\u0169\3\2\2\2\u0167\u0165\3\2"+
		"\2\2\u0167\u0168\3\2\2\2\u0168\u016a\3\2\2\2\u0169\u0167\3\2\2\2\u016a"+
		"\u016c\5\64\33\2\u016b\u0167\3\2\2\2\u016b\u016c\3\2\2\2\u016c\u0181\3"+
		"\2\2\2\u016d\u016f\7V\2\2\u016e\u016d\3\2\2\2\u016f\u0172\3\2\2\2\u0170"+
		"\u016e\3\2\2\2\u0170\u0171\3\2\2\2\u0171\u0173\3\2\2\2\u0172\u0170\3\2"+
		"\2\2\u0173\u0174\5\64\33\2\u0174\u017c\3\2\2\2\u0175\u0177\7V\2\2\u0176"+
		"\u0175\3\2\2\2\u0177\u017a\3\2\2\2\u0178\u0176\3\2\2\2\u0178\u0179\3\2"+
		"\2\2\u0179\u017b\3\2\2\2\u017a\u0178\3\2\2\2\u017b\u017d\5\62\32\2\u017c"+
		"\u0178\3\2\2\2\u017c\u017d\3\2\2\2\u017d\u017e\3\2\2\2\u017e\u017f\7\f"+
		"\2\2\u017f\u0181\3\2\2\2\u0180\u0159\3\2\2\2\u0180\u015b\3\2\2\2\u0180"+
		"\u0170\3\2\2\2\u0181\61\3\2\2\2\u0182\u0183\7\17\2\2\u0183\u018c\7\r\2"+
		"\2\u0184\u0185\7\17\2\2\u0185\u0186\7\16\2\2\u0186\u0187\5T+\2\u0187\u0188"+
		"\7\r\2\2\u0188\u018c\3\2\2\2\u0189\u018a\7\17\2\2\u018a\u018c\5@!\2\u018b"+
		"\u0182\3\2\2\2\u018b\u0184\3\2\2\2\u018b\u0189\3\2\2\2\u018c\63\3\2\2"+
		"\2\u018d\u018e\7\20\2\2\u018e\u0197\7\r\2\2\u018f\u0190\7\20\2\2\u0190"+
		"\u0191\7\16\2\2\u0191\u0192\5T+\2\u0192\u0193\7\r\2\2\u0193\u0197\3\2"+
		"\2\2\u0194\u0195\7\20\2\2\u0195\u0197\5@!\2\u0196\u018d\3\2\2\2\u0196"+
		"\u018f\3\2\2\2\u0196\u0194\3\2\2\2\u0197\65\3\2\2\2\u0198\u019a\7V\2\2"+
		"\u0199\u0198\3\2\2\2\u019a\u019d\3\2\2\2\u019b\u0199\3\2\2\2\u019b\u019c"+
		"\3\2\2\2\u019c\u019e\3\2\2\2\u019d\u019b\3\2\2\2\u019e\u019f\7R\2\2\u019f"+
		"\u01a0\5\f\7\2\u01a0\u01a1\58\35\2\u01a1\67\3\2\2\2\u01a2\u01a3\7\16\2"+
		"\2\u01a3\u01a4\5T+\2\u01a4\u01a5\7\r\2\2\u01a5\u01a8\3\2\2\2\u01a6\u01a8"+
		"\5@!\2\u01a7\u01a2\3\2\2\2\u01a7\u01a6\3\2\2\2\u01a89\3\2\2\2\u01a9\u01aa"+
		"\t\2\2\2\u01aa;\3\2\2\2\u01ab\u01ad\7\21\2\2\u01ac\u01ab\3\2\2\2\u01ac"+
		"\u01ad\3\2\2\2\u01ad\u01ae\3\2\2\2\u01ae\u01b5\t\3\2\2\u01af\u01b0\7\22"+
		"\2\2\u01b0\u01b4\7Q\2\2\u01b1\u01b4\7R\2\2\u01b2\u01b4\7S\2\2\u01b3\u01af"+
		"\3\2\2\2\u01b3\u01b1\3\2\2\2\u01b3\u01b2\3\2\2\2\u01b4\u01b7\3\2\2\2\u01b5"+
		"\u01b3\3\2\2\2\u01b5\u01b6\3\2\2\2\u01b6\u01b9\3\2\2\2\u01b7\u01b5\3\2"+
		"\2\2\u01b8\u01ba\5\24\13\2\u01b9\u01b8\3\2\2\2\u01b9\u01ba\3\2\2\2\u01ba"+
		"=\3\2\2\2\u01bb\u01c4\5@!\2\u01bc\u01c4\5B\"\2\u01bd\u01be\5V,\2\u01be"+
		"\u01bf\7\r\2\2\u01bf\u01c4\3\2\2\2\u01c0\u01c1\5T+\2\u01c1\u01c2\7\r\2"+
		"\2\u01c2\u01c4\3\2\2\2\u01c3\u01bb\3\2\2\2\u01c3\u01bc\3\2\2\2\u01c3\u01bd"+
		"\3\2\2\2\u01c3\u01c0\3\2\2\2\u01c4?\3\2\2\2\u01c5\u01c9\7\13\2\2\u01c6"+
		"\u01c8\5> \2\u01c7\u01c6\3\2\2\2\u01c8\u01cb\3\2\2\2\u01c9\u01c7\3\2\2"+
		"\2\u01c9\u01ca\3\2\2\2\u01ca\u01cc\3\2\2\2\u01cb\u01c9\3\2\2\2\u01cc\u01cd"+
		"\7\f\2\2\u01cdA\3\2\2\2\u01ce\u01d7\5D#\2\u01cf\u01d7\5F$\2\u01d0\u01d7"+
		"\5H%\2\u01d1\u01d7\5J&\2\u01d2\u01d7\5L\'\2\u01d3\u01d7\5N(\2\u01d4\u01d7"+
		"\5P)\2\u01d5\u01d7\5R*\2\u01d6\u01ce\3\2\2\2\u01d6\u01cf\3\2\2\2\u01d6"+
		"\u01d0\3\2\2\2\u01d6\u01d1\3\2\2\2\u01d6\u01d2\3\2\2\2\u01d6\u01d3\3\2"+
		"\2\2\u01d6\u01d4\3\2\2\2\u01d6\u01d5\3\2\2\2\u01d7C\3\2\2\2\u01d8\u01d9"+
		"\7\23\2\2\u01d9\u01da\7\6\2\2\u01da\u01db\5T+\2\u01db\u01dc\7\7\2\2\u01dc"+
		"\u01dd\5> \2\u01ddE\3\2\2\2\u01de\u01df\7\24\2\2\u01df\u01e0\7\6\2\2\u01e0"+
		"\u01e1\5T+\2\u01e1\u01e2\7\r\2\2\u01e2\u01e3\5T+\2\u01e3\u01e4\7\r\2\2"+
		"\u01e4\u01e5\5T+\2\u01e5\u01e6\7\7\2\2\u01e6\u01e7\5> \2\u01e7G\3\2\2"+
		"\2\u01e8\u01e9\7\25\2\2\u01e9\u01ea\7\6\2\2\u01ea\u01eb\7Q\2\2\u01eb\u01ec"+
		"\7S\2\2\u01ec\u01ed\7\26\2\2\u01ed\u01ee\5T+\2\u01ee\u01ef\7\7\2\2\u01ef"+
		"\u01f0\5> \2\u01f0I\3\2\2\2\u01f1\u01f2\7\27\2\2\u01f2\u01f3\7\6\2\2\u01f3"+
		"\u01f4\5T+\2\u01f4\u01f5\7\7\2\2\u01f5\u01f6\5> \2\u01f6K\3\2\2\2\u01f7"+
		"\u01f8\7\30\2\2\u01f8\u01f9\5> \2\u01f9\u01fa\7\27\2\2\u01fa\u01fb\7\6"+
		"\2\2\u01fb\u01fc\5T+\2\u01fc\u01fd\7\7\2\2\u01fd\u01fe\7\r\2\2\u01feM"+
		"\3\2\2\2\u01ff\u0200\7\31\2\2\u0200\u0201\5T+\2\u0201\u0202\7\r\2\2\u0202"+
		"O\3\2\2\2\u0203\u0204\7\32\2\2\u0204\u0205\5T+\2\u0205\u0206\7\r\2\2\u0206"+
		"Q\3\2\2\2\u0207\u0208\7\33\2\2\u0208\u0211\5> \2\u0209\u020a\7\34\2\2"+
		"\u020a\u020b\7\6\2\2\u020b\u020c\7Q\2\2\u020c\u020d\7S\2\2\u020d\u020e"+
		"\7\7\2\2\u020e\u0210\5> \2\u020f\u0209\3\2\2\2\u0210\u0213\3\2\2\2\u0211"+
		"\u020f\3\2\2\2\u0211\u0212\3\2\2\2\u0212\u0216\3\2\2\2\u0213\u0211\3\2"+
		"\2\2\u0214\u0215\7\35\2\2\u0215\u0217\5> \2\u0216\u0214\3\2\2\2\u0216"+
		"\u0217\3\2\2\2\u0217S\3\2\2\2\u0218\u021b\5X-\2\u0219\u021b\5\u0084C\2"+
		"\u021a\u0218\3\2\2\2\u021a\u0219\3\2\2\2\u021bU\3\2\2\2\u021c\u021d\7"+
		"Q\2\2\u021d\u021e\7S\2\2\u021e\u021f\7\n\2\2\u021f\u0220\5T+\2\u0220W"+
		"\3\2\2\2\u0221\u0224\5^\60\2\u0222\u0224\5Z.\2\u0223\u0221\3\2\2\2\u0223"+
		"\u0222\3\2\2\2\u0224Y\3\2\2\2\u0225\u0226\5\26\f\2\u0226\u0227\7\16\2"+
		"\2\u0227\u0228\5@!\2\u0228[\3\2\2\2\u0229\u022e\5T+\2\u022a\u022b\7\3"+
		"\2\2\u022b\u022d\5T+\2\u022c\u022a\3\2\2\2\u022d\u0230\3\2\2\2\u022e\u022c"+
		"\3\2\2\2\u022e\u022f\3\2\2\2\u022f]\3\2\2\2\u0230\u022e\3\2\2\2\u0231"+
		"\u0237\5`\61\2\u0232\u0233\7\36\2\2\u0233\u0234\5T+\2\u0234\u0235\7\37"+
		"\2\2\u0235\u0236\5T+\2\u0236\u0238\3\2\2\2\u0237\u0232\3\2\2\2\u0237\u0238"+
		"\3\2\2\2\u0238_\3\2\2\2\u0239\u023c\5b\62\2\u023a\u023b\7 \2\2\u023b\u023d"+
		"\5`\61\2\u023c\u023a\3\2\2\2\u023c\u023d\3\2\2\2\u023da\3\2\2\2\u023e"+
		"\u0243\5d\63\2\u023f\u0240\7!\2\2\u0240\u0242\5d\63\2\u0241\u023f\3\2"+
		"\2\2\u0242\u0245\3\2\2\2\u0243\u0241\3\2\2\2\u0243\u0244\3\2\2\2\u0244"+
		"c\3\2\2\2\u0245\u0243\3\2\2\2\u0246\u024b\5f\64\2\u0247\u0248\7\"\2\2"+
		"\u0248\u024a\5f\64\2\u0249\u0247\3\2\2\2\u024a\u024d\3\2\2\2\u024b\u0249"+
		"\3\2\2\2\u024b\u024c\3\2\2\2\u024ce\3\2\2\2\u024d\u024b\3\2\2\2\u024e"+
		"\u0253\5h\65\2\u024f\u0250\7#\2\2\u0250\u0252\5h\65\2\u0251\u024f\3\2"+
		"\2\2\u0252\u0255\3\2\2\2\u0253\u0251\3\2\2\2\u0253\u0254\3\2\2\2\u0254"+
		"g\3\2\2\2\u0255\u0253\3\2\2\2\u0256\u025b\5j\66\2\u0257\u0258\7$\2\2\u0258"+
		"\u025a\5j\66\2\u0259\u0257\3\2\2\2\u025a\u025d\3\2\2\2\u025b\u0259\3\2"+
		"\2\2\u025b\u025c\3\2\2\2\u025ci\3\2\2\2\u025d\u025b\3\2\2\2\u025e\u0263"+
		"\5l\67\2\u025f\u0260\7%\2\2\u0260\u0262\5l\67\2\u0261\u025f\3\2\2\2\u0262"+
		"\u0265\3\2\2\2\u0263\u0261\3\2\2\2\u0263\u0264\3\2\2\2\u0264k\3\2\2\2"+
		"\u0265\u0263\3\2\2\2\u0266\u026b\5n8\2\u0267\u0268\t\4\2\2\u0268\u026a"+
		"\5n8\2\u0269\u0267\3\2\2\2\u026a\u026d\3\2\2\2\u026b\u0269\3\2\2\2\u026b"+
		"\u026c\3\2\2\2\u026cm\3\2\2\2\u026d\u026b\3\2\2\2\u026e\u0272\5r:\2\u026f"+
		"\u0271\5p9\2\u0270\u026f\3\2\2\2\u0271\u0274\3\2\2\2\u0272\u0270\3\2\2"+
		"\2\u0272\u0273\3\2\2\2\u0273o\3\2\2\2\u0274\u0272\3\2\2\2\u0275\u0276"+
		"\t\5\2\2\u0276\u027a\5r:\2\u0277\u0278\t\6\2\2\u0278\u027a\7Q\2\2\u0279"+
		"\u0275\3\2\2\2\u0279\u0277\3\2\2\2\u027aq\3\2\2\2\u027b\u0280\5t;\2\u027c"+
		"\u027d\t\7\2\2\u027d\u027f\5t;\2\u027e\u027c\3\2\2\2\u027f\u0282\3\2\2"+
		"\2\u0280\u027e\3\2\2\2\u0280\u0281\3\2\2\2\u0281s\3\2\2\2\u0282\u0280"+
		"\3\2\2\2\u0283\u0288\5v<\2\u0284\u0285\t\b\2\2\u0285\u0287\5v<\2\u0286"+
		"\u0284\3\2\2\2\u0287\u028a\3\2\2\2\u0288\u0286\3\2\2\2\u0288\u0289\3\2"+
		"\2\2\u0289u\3\2\2\2\u028a\u0288\3\2\2\2\u028b\u0290\5x=\2\u028c\u028d"+
		"\t\t\2\2\u028d\u028f\5x=\2\u028e\u028c\3\2\2\2\u028f\u0292\3\2\2\2\u0290"+
		"\u028e\3\2\2\2\u0290\u0291\3\2\2\2\u0291w\3\2\2\2\u0292\u0290\3\2\2\2"+
		"\u0293\u0296\5z>\2\u0294\u0295\7\63\2\2\u0295\u0297\5\"\22\2\u0296\u0294"+
		"\3\2\2\2\u0296\u0297\3\2\2\2\u0297y\3\2\2\2\u0298\u029b\5|?\2\u0299\u029a"+
		"\t\n\2\2\u029a\u029c\5|?\2\u029b\u0299\3\2\2\2\u029b\u029c\3\2\2\2\u029c"+
		"{\3\2\2\2\u029d\u02a6\5\u0086D\2\u029e\u029f\t\13\2\2\u029f\u02a6\5|?"+
		"\2\u02a0\u02a1\t\f\2\2\u02a1\u02a6\5|?\2\u02a2\u02a6\5~@\2\u02a3\u02a6"+
		"\5\u0080A\2\u02a4\u02a6\5\u0082B\2\u02a5\u029d\3\2\2\2\u02a5\u029e\3\2"+
		"\2\2\u02a5\u02a0\3\2\2\2\u02a5\u02a2\3\2\2\2\u02a5\u02a3\3\2\2\2\u02a5"+
		"\u02a4\3\2\2\2\u02a6}\3\2\2\2\u02a7\u02a8\7\6\2\2\u02a8\u02a9\7Q\2\2\u02a9"+
		"\u02aa\7\7\2\2\u02aa\u02ab\5|?\2\u02ab\177\3\2\2\2\u02ac\u02ad\7\60\2"+
		"\2\u02ad\u02ae\5|?\2\u02ae\u0081\3\2\2\2\u02af\u02b0\7%\2\2\u02b0\u02b1"+
		"\5|?\2\u02b1\u0083\3\2\2\2\u02b2\u02b3\5|?\2\u02b3\u02b4\t\r\2\2\u02b4"+
		"\u02b5\5T+\2\u02b5\u0085\3\2\2\2\u02b6\u02b9\5\u0088E\2\u02b7\u02b9\5"+
		"\u0090I\2\u02b8\u02b6\3\2\2\2\u02b8\u02b7\3\2\2\2\u02b9\u0087\3\2\2\2"+
		"\u02ba\u02bb\7E\2\2\u02bb\u02bd\5\30\r\2\u02bc\u02be\5\u008aF\2\u02bd"+
		"\u02bc\3\2\2\2\u02bd\u02be\3\2\2\2\u02be\u02c0\3\2\2\2\u02bf\u02c1\5\u008c"+
		"G\2\u02c0\u02bf\3\2\2\2\u02c0\u02c1\3\2\2\2\u02c1\u0089\3\2\2\2\u02c2"+
		"\u02c6\7\b\2\2\u02c3\u02c5\7\3\2\2\u02c4\u02c3\3\2\2\2\u02c5\u02c8\3\2"+
		"\2\2\u02c6\u02c4\3\2\2\2\u02c6\u02c7\3\2\2\2\u02c7\u02c9\3\2\2\2\u02c8"+
		"\u02c6\3\2\2\2\u02c9\u02ca\7\t\2\2\u02ca\u008b\3\2\2\2\u02cb\u02d7\7\13"+
		"\2\2\u02cc\u02d1\5\u008eH\2\u02cd\u02ce\7\3\2\2\u02ce\u02d0\5\u008eH\2"+
		"\u02cf\u02cd\3\2\2\2\u02d0\u02d3\3\2\2\2\u02d1\u02cf\3\2\2\2\u02d1\u02d2"+
		"\3\2\2\2\u02d2\u02d5\3\2\2\2\u02d3\u02d1\3\2\2\2\u02d4\u02d6\7\3\2\2\u02d5"+
		"\u02d4\3\2\2\2\u02d5\u02d6\3\2\2\2\u02d6\u02d8\3\2\2\2\u02d7\u02cc\3\2"+
		"\2\2\u02d7\u02d8\3\2\2\2\u02d8\u02d9\3\2\2\2\u02d9\u02da\7\f\2\2\u02da"+
		"\u008d\3\2\2\2\u02db\u02de\5T+\2\u02dc\u02de\5\u008cG\2\u02dd\u02db\3"+
		"\2\2\2\u02dd\u02dc\3\2\2\2\u02de\u008f\3\2\2\2\u02df\u02eb\5:\36\2\u02e0"+
		"\u02eb\5<\37\2\u02e1\u02e2\7\6\2\2\u02e2\u02e3\5T+\2\u02e3\u02e4\7\7\2"+
		"\2\u02e4\u02eb\3\2\2\2\u02e5\u02eb\5\u0092J\2\u02e6\u02eb\5\u0094K\2\u02e7"+
		"\u02eb\5\u0096L\2\u02e8\u02eb\5\u0098M\2\u02e9\u02eb\5\u009aN\2\u02ea"+
		"\u02df\3\2\2\2\u02ea\u02e0\3\2\2\2\u02ea\u02e1\3\2\2\2\u02ea\u02e5\3\2"+
		"\2\2\u02ea\u02e6\3\2\2\2\u02ea\u02e7\3\2\2\2\u02ea\u02e8\3\2\2\2\u02ea"+
		"\u02e9\3\2\2\2\u02eb\u0091\3\2\2\2\u02ec\u02ed\7\6\2\2\u02ed\u02ee\5\u0086"+
		"D\2\u02ee\u02ef\7\7\2\2\u02ef\u02f2\3\2\2\2\u02f0\u02f2\7Q\2\2\u02f1\u02ec"+
		"\3\2\2\2\u02f1\u02f0\3\2\2\2\u02f2\u02f3\3\2\2\2\u02f3\u02f4\7\22\2\2"+
		"\u02f4\u02f6\5<\37\2\u02f5\u02f7\5\24\13\2\u02f6\u02f5\3\2\2\2\u02f6\u02f7"+
		"\3\2\2\2\u02f7\u0093\3\2\2\2\u02f8\u02f9\5\u0092J\2\u02f9\u02fa\5\26\f"+
		"\2\u02fa\u0095\3\2\2\2\u02fb\u02fc\5\u0092J\2\u02fc\u02fd\5\30\r\2\u02fd"+
		"\u0097\3\2\2\2\u02fe\u0301\5:\36\2\u02ff\u0301\5<\37\2\u0300\u02fe\3\2"+
		"\2\2\u0300\u02ff\3\2\2\2\u0301\u0302\3\2\2\2\u0302\u0303\t\f\2\2\u0303"+
		"\u0099\3\2\2\2\u0304\u030b\5\u009eP\2\u0305\u030b\5\u00a0Q\2\u0306\u030b"+
		"\5\u00a2R\2\u0307\u030b\5\u00a4S\2\u0308\u030b\5\u00a6T\2\u0309\u030b"+
		"\5\u00aaV\2\u030a\u0304\3\2\2\2\u030a\u0305\3\2\2\2\u030a\u0306\3\2\2"+
		"\2\u030a\u0307\3\2\2\2\u030a\u0308\3\2\2\2\u030a\u0309\3\2\2\2\u030b\u009b"+
		"\3\2\2\2\u030c\u030f\5\34\17\2\u030d\u030f\5 \21\2\u030e\u030c\3\2\2\2"+
		"\u030e\u030d\3\2\2\2\u030f\u009d\3\2\2\2\u0310\u0311\7E\2\2\u0311\u0317"+
		"\7Q\2\2\u0312\u0314\5\26\f\2\u0313\u0315\5\u009cO\2\u0314\u0313\3\2\2"+
		"\2\u0314\u0315\3\2\2\2\u0315\u0318\3\2\2\2\u0316\u0318\5\u009cO\2\u0317"+
		"\u0312\3\2\2\2\u0317\u0316\3\2\2\2\u0318\u0322\3\2\2\2\u0319\u031a\7E"+
		"\2\2\u031a\u031b\7Q\2\2\u031b\u031c\7\6\2\2\u031c\u031d\5T+\2\u031d\u031e"+
		"\7\7\2\2\u031e\u0322\3\2\2\2\u031f\u0320\7E\2\2\u0320\u0322\5$\23\2\u0321"+
		"\u0310\3\2\2\2\u0321\u0319\3\2\2\2\u0321\u031f\3\2\2\2\u0322\u009f\3\2"+
		"\2\2\u0323\u0324\7F\2\2\u0324\u0325\7\6\2\2\u0325\u0326\7Q\2\2\u0326\u0327"+
		"\7\7\2\2\u0327\u00a1\3\2\2\2\u0328\u0329\7G\2\2\u0329\u032a\7\6\2\2\u032a"+
		"\u032b\5T+\2\u032b\u032c\7\7\2\2\u032c\u00a3\3\2\2\2\u032d\u032e\7H\2"+
		"\2\u032e\u032f\7\6\2\2\u032f\u0330\5T+\2\u0330\u0331\7\7\2\2\u0331\u00a5"+
		"\3\2\2\2\u0332\u0336\7I\2\2\u0333\u0334\7\6\2\2\u0334\u0335\7Q\2\2\u0335"+
		"\u0337\7\7\2\2\u0336\u0333\3\2\2\2\u0336\u0337\3\2\2\2\u0337\u00a7\3\2"+
		"\2\2\u0338\u0339\7J\2\2\u0339\u033a\5\f\7\2\u033a\u033b\5@!\2\u033b\u00a9"+
		"\3\2\2\2\u033c\u033d\7K\2\2\u033d\u033e\7\6\2\2\u033e\u033f\7Q\2\2\u033f"+
		"\u0340\7\7\2\2\u0340\u00ab\3\2\2\2T\u00b3\u00bb\u00c5\u00d4\u00db\u00ec"+
		"\u00f1\u00f7\u00fa\u00fc\u0105\u010b\u010e\u0110\u0116\u011d\u0123\u0126"+
		"\u0128\u012f\u0137\u013f\u0144\u014c\u0153\u015f\u0167\u016b\u0170\u0178"+
		"\u017c\u0180\u018b\u0196\u019b\u01a7\u01ac\u01b3\u01b5\u01b9\u01c3\u01c9"+
		"\u01d6\u0211\u0216\u021a\u0223\u022e\u0237\u023c\u0243\u024b\u0253\u025b"+
		"\u0263\u026b\u0272\u0279\u0280\u0288\u0290\u0296\u029b\u02a5\u02b8\u02bd"+
		"\u02c0\u02c6\u02d1\u02d5\u02d7\u02dd\u02ea\u02f1\u02f6\u0300\u030a\u030e"+
		"\u0314\u0317\u0321\u0336";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}