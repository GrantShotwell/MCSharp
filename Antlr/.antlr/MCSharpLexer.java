// Generated from c:\Projects\MCSharp\MCSharp\Antlr\MCSharp.g4 by ANTLR 4.8
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class MCSharpLexer extends Lexer {
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
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE"
	};

	private static String[] makeRuleNames() {
		return new String[] {
			"T__0", "END", "COMMA", "OP", "CP", "OB", "CB", "OC", "CC", "PLUS", "MINUS", 
			"MULTIPLY", "DIVIDE", "MODULUS", "INCREMENT", "DECREMENT", "BITWISE_AND", 
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
			"STRING", "DECIMAL", "INTEGER", "SIMPLE_NAME_CHARACTER", "COMPLEX_NAME_CHARACTER", 
			"NAME", "WHITESPACE", "NEWLINE"
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


	public MCSharpLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "MCSharp.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2]\u0249\b\1\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\tC\4D\tD\4E\tE\4F\tF\4G\tG\4H\tH\4I"+
		"\tI\4J\tJ\4K\tK\4L\tL\4M\tM\4N\tN\4O\tO\4P\tP\4Q\tQ\4R\tR\4S\tS\4T\tT"+
		"\4U\tU\4V\tV\4W\tW\4X\tX\4Y\tY\4Z\tZ\4[\t[\4\\\t\\\4]\t]\4^\t^\3\2\3\2"+
		"\3\3\3\3\3\4\3\4\3\5\3\5\3\6\3\6\3\7\3\7\3\b\3\b\3\t\3\t\3\n\3\n\3\13"+
		"\3\13\3\f\3\f\3\r\3\r\3\16\3\16\3\17\3\17\3\20\3\20\3\20\3\21\3\21\3\21"+
		"\3\22\3\22\3\23\3\23\3\24\3\24\3\25\3\25\3\26\3\26\3\26\3\27\3\27\3\27"+
		"\3\30\3\30\3\31\3\31\3\31\3\32\3\32\3\32\3\33\3\33\3\33\3\34\3\34\3\34"+
		"\3\35\3\35\3\36\3\36\3\37\3\37\3\37\3 \3 \3 \3!\3!\3\"\3\"\3#\3#\3#\3"+
		"$\3$\3$\3%\3%\3%\3&\3&\3&\3\'\3\'\3\'\3(\3(\3(\3)\3)\3)\3*\3*\3*\3+\3"+
		"+\3+\3,\3,\3,\3,\3-\3-\3-\3-\3.\3.\3/\3/\3\60\3\60\3\60\3\61\3\61\3\61"+
		"\3\61\3\62\3\62\3\62\3\63\3\63\3\63\3\64\3\64\3\64\3\65\3\65\3\65\3\65"+
		"\3\66\3\66\3\66\3\67\3\67\3\67\38\38\38\39\39\39\39\39\3:\3:\3:\3:\3;"+
		"\3;\3;\3;\3;\3;\3;\3;\3<\3<\3<\3=\3=\3=\3=\3=\3=\3>\3>\3>\3>\3>\3>\3>"+
		"\3?\3?\3?\3?\3?\3?\3@\3@\3@\3@\3A\3A\3A\3A\3A\3A\3B\3B\3B\3B\3B\3B\3B"+
		"\3B\3C\3C\3C\3C\3D\3D\3D\3D\3D\3D\3D\3E\3E\3E\3E\3E\3E\3E\3E\3F\3F\3F"+
		"\3F\3F\3F\3F\3F\3F\3F\3G\3G\3G\3G\3G\3G\3G\3G\3H\3H\3H\3H\3H\3H\3H\3H"+
		"\3H\3I\3I\3I\3I\3I\3I\3I\3J\3J\3J\3J\3J\3K\3K\3K\3K\3L\3L\3L\3L\3M\3M"+
		"\3M\3M\3M\3M\3M\3N\3N\3N\3N\3N\3N\3N\3N\3O\3O\3O\3O\3O\3O\3O\3O\3O\3O"+
		"\3P\3P\3P\3P\3P\3P\3P\3Q\3Q\3Q\3Q\3Q\3Q\3Q\3Q\3Q\3R\3R\3R\3R\3R\3R\3R"+
		"\3R\3S\3S\3S\3S\3S\3S\3S\3S\3S\3T\3T\3T\3T\3U\3U\3U\3U\3U\3U\3V\3V\3V"+
		"\3V\3V\3V\3V\3W\3W\3W\3W\3W\3W\5W\u021a\nW\3W\3W\3X\6X\u021f\nX\rX\16"+
		"X\u0220\3X\3X\6X\u0225\nX\rX\16X\u0226\3Y\6Y\u022a\nY\rY\16Y\u022b\3Z"+
		"\5Z\u022f\nZ\3[\3[\5[\u0233\n[\3\\\3\\\7\\\u0237\n\\\f\\\16\\\u023a\13"+
		"\\\3]\6]\u023d\n]\r]\16]\u023e\3]\3]\3^\6^\u0244\n^\r^\16^\u0245\3^\3"+
		"^\2\2_\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17"+
		"\35\20\37\21!\22#\23%\24\'\25)\26+\27-\30/\31\61\32\63\33\65\34\67\35"+
		"9\36;\37= ?!A\"C#E$G%I&K\'M(O)Q*S+U,W-Y.[/]\60_\61a\62c\63e\64g\65i\66"+
		"k\67m8o9q:s;u<w=y>{?}@\177A\u0081B\u0083C\u0085D\u0087E\u0089F\u008bG"+
		"\u008dH\u008fI\u0091J\u0093K\u0095L\u0097M\u0099N\u009bO\u009dP\u009f"+
		"Q\u00a1R\u00a3S\u00a5T\u00a7U\u00a9V\u00abW\u00adX\u00afY\u00b1Z\u00b3"+
		"\2\u00b5\2\u00b7[\u00b9\\\u00bb]\3\2\7\3\2$$\3\2\62;\6\2//C\\aac|\5\2"+
		"\13\13\17\17\"\"\3\2\f\f\2\u024f\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2"+
		"\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2"+
		"\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2"+
		"\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2"+
		"\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2\2\2\61\3\2\2\2\2\63\3\2\2\2\2\65\3\2\2"+
		"\2\2\67\3\2\2\2\29\3\2\2\2\2;\3\2\2\2\2=\3\2\2\2\2?\3\2\2\2\2A\3\2\2\2"+
		"\2C\3\2\2\2\2E\3\2\2\2\2G\3\2\2\2\2I\3\2\2\2\2K\3\2\2\2\2M\3\2\2\2\2O"+
		"\3\2\2\2\2Q\3\2\2\2\2S\3\2\2\2\2U\3\2\2\2\2W\3\2\2\2\2Y\3\2\2\2\2[\3\2"+
		"\2\2\2]\3\2\2\2\2_\3\2\2\2\2a\3\2\2\2\2c\3\2\2\2\2e\3\2\2\2\2g\3\2\2\2"+
		"\2i\3\2\2\2\2k\3\2\2\2\2m\3\2\2\2\2o\3\2\2\2\2q\3\2\2\2\2s\3\2\2\2\2u"+
		"\3\2\2\2\2w\3\2\2\2\2y\3\2\2\2\2{\3\2\2\2\2}\3\2\2\2\2\177\3\2\2\2\2\u0081"+
		"\3\2\2\2\2\u0083\3\2\2\2\2\u0085\3\2\2\2\2\u0087\3\2\2\2\2\u0089\3\2\2"+
		"\2\2\u008b\3\2\2\2\2\u008d\3\2\2\2\2\u008f\3\2\2\2\2\u0091\3\2\2\2\2\u0093"+
		"\3\2\2\2\2\u0095\3\2\2\2\2\u0097\3\2\2\2\2\u0099\3\2\2\2\2\u009b\3\2\2"+
		"\2\2\u009d\3\2\2\2\2\u009f\3\2\2\2\2\u00a1\3\2\2\2\2\u00a3\3\2\2\2\2\u00a5"+
		"\3\2\2\2\2\u00a7\3\2\2\2\2\u00a9\3\2\2\2\2\u00ab\3\2\2\2\2\u00ad\3\2\2"+
		"\2\2\u00af\3\2\2\2\2\u00b1\3\2\2\2\2\u00b7\3\2\2\2\2\u00b9\3\2\2\2\2\u00bb"+
		"\3\2\2\2\3\u00bd\3\2\2\2\5\u00bf\3\2\2\2\7\u00c1\3\2\2\2\t\u00c3\3\2\2"+
		"\2\13\u00c5\3\2\2\2\r\u00c7\3\2\2\2\17\u00c9\3\2\2\2\21\u00cb\3\2\2\2"+
		"\23\u00cd\3\2\2\2\25\u00cf\3\2\2\2\27\u00d1\3\2\2\2\31\u00d3\3\2\2\2\33"+
		"\u00d5\3\2\2\2\35\u00d7\3\2\2\2\37\u00d9\3\2\2\2!\u00dc\3\2\2\2#\u00df"+
		"\3\2\2\2%\u00e1\3\2\2\2\'\u00e3\3\2\2\2)\u00e5\3\2\2\2+\u00e7\3\2\2\2"+
		"-\u00ea\3\2\2\2/\u00ed\3\2\2\2\61\u00ef\3\2\2\2\63\u00f2\3\2\2\2\65\u00f5"+
		"\3\2\2\2\67\u00f8\3\2\2\29\u00fb\3\2\2\2;\u00fd\3\2\2\2=\u00ff\3\2\2\2"+
		"?\u0102\3\2\2\2A\u0105\3\2\2\2C\u0107\3\2\2\2E\u0109\3\2\2\2G\u010c\3"+
		"\2\2\2I\u010f\3\2\2\2K\u0112\3\2\2\2M\u0115\3\2\2\2O\u0118\3\2\2\2Q\u011b"+
		"\3\2\2\2S\u011e\3\2\2\2U\u0121\3\2\2\2W\u0124\3\2\2\2Y\u0128\3\2\2\2["+
		"\u012c\3\2\2\2]\u012e\3\2\2\2_\u0130\3\2\2\2a\u0133\3\2\2\2c\u0137\3\2"+
		"\2\2e\u013a\3\2\2\2g\u013d\3\2\2\2i\u0140\3\2\2\2k\u0144\3\2\2\2m\u0147"+
		"\3\2\2\2o\u014a\3\2\2\2q\u014d\3\2\2\2s\u0152\3\2\2\2u\u0156\3\2\2\2w"+
		"\u015e\3\2\2\2y\u0161\3\2\2\2{\u0167\3\2\2\2}\u016e\3\2\2\2\177\u0174"+
		"\3\2\2\2\u0081\u0178\3\2\2\2\u0083\u017e\3\2\2\2\u0085\u0186\3\2\2\2\u0087"+
		"\u018a\3\2\2\2\u0089\u0191\3\2\2\2\u008b\u0199\3\2\2\2\u008d\u01a3\3\2"+
		"\2\2\u008f\u01ab\3\2\2\2\u0091\u01b4\3\2\2\2\u0093\u01bb\3\2\2\2\u0095"+
		"\u01c0\3\2\2\2\u0097\u01c4\3\2\2\2\u0099\u01c8\3\2\2\2\u009b\u01cf\3\2"+
		"\2\2\u009d\u01d7\3\2\2\2\u009f\u01e1\3\2\2\2\u00a1\u01e8\3\2\2\2\u00a3"+
		"\u01f1\3\2\2\2\u00a5\u01f9\3\2\2\2\u00a7\u0202\3\2\2\2\u00a9\u0206\3\2"+
		"\2\2\u00ab\u020c\3\2\2\2\u00ad\u0213\3\2\2\2\u00af\u021e\3\2\2\2\u00b1"+
		"\u0229\3\2\2\2\u00b3\u022e\3\2\2\2\u00b5\u0232\3\2\2\2\u00b7\u0234\3\2"+
		"\2\2\u00b9\u023c\3\2\2\2\u00bb\u0243\3\2\2\2\u00bd\u00be\7B\2\2\u00be"+
		"\4\3\2\2\2\u00bf\u00c0\7=\2\2\u00c0\6\3\2\2\2\u00c1\u00c2\7.\2\2\u00c2"+
		"\b\3\2\2\2\u00c3\u00c4\7*\2\2\u00c4\n\3\2\2\2\u00c5\u00c6\7+\2\2\u00c6"+
		"\f\3\2\2\2\u00c7\u00c8\7]\2\2\u00c8\16\3\2\2\2\u00c9\u00ca\7_\2\2\u00ca"+
		"\20\3\2\2\2\u00cb\u00cc\7}\2\2\u00cc\22\3\2\2\2\u00cd\u00ce\7\177\2\2"+
		"\u00ce\24\3\2\2\2\u00cf\u00d0\7-\2\2\u00d0\26\3\2\2\2\u00d1\u00d2\7/\2"+
		"\2\u00d2\30\3\2\2\2\u00d3\u00d4\7,\2\2\u00d4\32\3\2\2\2\u00d5\u00d6\7"+
		"\61\2\2\u00d6\34\3\2\2\2\u00d7\u00d8\7\'\2\2\u00d8\36\3\2\2\2\u00d9\u00da"+
		"\7-\2\2\u00da\u00db\7-\2\2\u00db \3\2\2\2\u00dc\u00dd\7/\2\2\u00dd\u00de"+
		"\7/\2\2\u00de\"\3\2\2\2\u00df\u00e0\7(\2\2\u00e0$\3\2\2\2\u00e1\u00e2"+
		"\7~\2\2\u00e2&\3\2\2\2\u00e3\u00e4\7`\2\2\u00e4(\3\2\2\2\u00e5\u00e6\7"+
		"\u0080\2\2\u00e6*\3\2\2\2\u00e7\u00e8\7(\2\2\u00e8\u00e9\7(\2\2\u00e9"+
		",\3\2\2\2\u00ea\u00eb\7~\2\2\u00eb\u00ec\7~\2\2\u00ec.\3\2\2\2\u00ed\u00ee"+
		"\7#\2\2\u00ee\60\3\2\2\2\u00ef\u00f0\7>\2\2\u00f0\u00f1\7>\2\2\u00f1\62"+
		"\3\2\2\2\u00f2\u00f3\7@\2\2\u00f3\u00f4\7@\2\2\u00f4\64\3\2\2\2\u00f5"+
		"\u00f6\7?\2\2\u00f6\u00f7\7?\2\2\u00f7\66\3\2\2\2\u00f8\u00f9\7#\2\2\u00f9"+
		"\u00fa\7?\2\2\u00fa8\3\2\2\2\u00fb\u00fc\7@\2\2\u00fc:\3\2\2\2\u00fd\u00fe"+
		"\7>\2\2\u00fe<\3\2\2\2\u00ff\u0100\7@\2\2\u0100\u0101\7?\2\2\u0101>\3"+
		"\2\2\2\u0102\u0103\7>\2\2\u0103\u0104\7?\2\2\u0104@\3\2\2\2\u0105\u0106"+
		"\7\60\2\2\u0106B\3\2\2\2\u0107\u0108\7?\2\2\u0108D\3\2\2\2\u0109\u010a"+
		"\7-\2\2\u010a\u010b\7?\2\2\u010bF\3\2\2\2\u010c\u010d\7/\2\2\u010d\u010e"+
		"\7?\2\2\u010eH\3\2\2\2\u010f\u0110\7,\2\2\u0110\u0111\7?\2\2\u0111J\3"+
		"\2\2\2\u0112\u0113\7\61\2\2\u0113\u0114\7?\2\2\u0114L\3\2\2\2\u0115\u0116"+
		"\7\'\2\2\u0116\u0117\7?\2\2\u0117N\3\2\2\2\u0118\u0119\7\60\2\2\u0119"+
		"\u011a\7?\2\2\u011aP\3\2\2\2\u011b\u011c\7(\2\2\u011c\u011d\7?\2\2\u011d"+
		"R\3\2\2\2\u011e\u011f\7~\2\2\u011f\u0120\7?\2\2\u0120T\3\2\2\2\u0121\u0122"+
		"\7`\2\2\u0122\u0123\7?\2\2\u0123V\3\2\2\2\u0124\u0125\7>\2\2\u0125\u0126"+
		"\7>\2\2\u0126\u0127\7?\2\2\u0127X\3\2\2\2\u0128\u0129\7@\2\2\u0129\u012a"+
		"\7@\2\2\u012a\u012b\7?\2\2\u012bZ\3\2\2\2\u012c\u012d\7A\2\2\u012d\\\3"+
		"\2\2\2\u012e\u012f\7<\2\2\u012f^\3\2\2\2\u0130\u0131\7\60\2\2\u0131\u0132"+
		"\7\60\2\2\u0132`\3\2\2\2\u0133\u0134\7\60\2\2\u0134\u0135\7\60\2\2\u0135"+
		"\u0136\7`\2\2\u0136b\3\2\2\2\u0137\u0138\7k\2\2\u0138\u0139\7u\2\2\u0139"+
		"d\3\2\2\2\u013a\u013b\7c\2\2\u013b\u013c\7u\2\2\u013cf\3\2\2\2\u013d\u013e"+
		"\7k\2\2\u013e\u013f\7p\2\2\u013fh\3\2\2\2\u0140\u0141\7q\2\2\u0141\u0142"+
		"\7w\2\2\u0142\u0143\7v\2\2\u0143j\3\2\2\2\u0144\u0145\7?\2\2\u0145\u0146"+
		"\7@\2\2\u0146l\3\2\2\2\u0147\u0148\7A\2\2\u0148\u0149\7A\2\2\u0149n\3"+
		"\2\2\2\u014a\u014b\7k\2\2\u014b\u014c\7h\2\2\u014cp\3\2\2\2\u014d\u014e"+
		"\7g\2\2\u014e\u014f\7n\2\2\u014f\u0150\7u\2\2\u0150\u0151\7g\2\2\u0151"+
		"r\3\2\2\2\u0152\u0153\7h\2\2\u0153\u0154\7q\2\2\u0154\u0155\7t\2\2\u0155"+
		"t\3\2\2\2\u0156\u0157\7h\2\2\u0157\u0158\7q\2\2\u0158\u0159\7t\2\2\u0159"+
		"\u015a\7g\2\2\u015a\u015b\7c\2\2\u015b\u015c\7e\2\2\u015c\u015d\7j\2\2"+
		"\u015dv\3\2\2\2\u015e\u015f\7f\2\2\u015f\u0160\7q\2\2\u0160x\3\2\2\2\u0161"+
		"\u0162\7y\2\2\u0162\u0163\7j\2\2\u0163\u0164\7k\2\2\u0164\u0165\7n\2\2"+
		"\u0165\u0166\7g\2\2\u0166z\3\2\2\2\u0167\u0168\7t\2\2\u0168\u0169\7g\2"+
		"\2\u0169\u016a\7v\2\2\u016a\u016b\7w\2\2\u016b\u016c\7t\2\2\u016c\u016d"+
		"\7p\2\2\u016d|\3\2\2\2\u016e\u016f\7v\2\2\u016f\u0170\7j\2\2\u0170\u0171"+
		"\7t\2\2\u0171\u0172\7q\2\2\u0172\u0173\7y\2\2\u0173~\3\2\2\2\u0174\u0175"+
		"\7v\2\2\u0175\u0176\7t\2\2\u0176\u0177\7{\2\2\u0177\u0080\3\2\2\2\u0178"+
		"\u0179\7e\2\2\u0179\u017a\7c\2\2\u017a\u017b\7v\2\2\u017b\u017c\7e\2\2"+
		"\u017c\u017d\7j\2\2\u017d\u0082\3\2\2\2\u017e\u017f\7h\2\2\u017f\u0180"+
		"\7k\2\2\u0180\u0181\7p\2\2\u0181\u0182\7c\2\2\u0182\u0183\7n\2\2\u0183"+
		"\u0184\7n\2\2\u0184\u0185\7{\2\2\u0185\u0084\3\2\2\2\u0186\u0187\7p\2"+
		"\2\u0187\u0188\7g\2\2\u0188\u0189\7y\2\2\u0189\u0086\3\2\2\2\u018a\u018b"+
		"\7v\2\2\u018b\u018c\7{\2\2\u018c\u018d\7r\2\2\u018d\u018e\7g\2\2\u018e"+
		"\u018f\7q\2\2\u018f\u0190\7h\2\2\u0190\u0088\3\2\2\2\u0191\u0192\7e\2"+
		"\2\u0192\u0193\7j\2\2\u0193\u0194\7g\2\2\u0194\u0195\7e\2\2\u0195\u0196"+
		"\7m\2\2\u0196\u0197\7g\2\2\u0197\u0198\7f\2\2\u0198\u008a\3\2\2\2\u0199"+
		"\u019a\7w\2\2\u019a\u019b\7p\2\2\u019b\u019c\7e\2\2\u019c\u019d\7j\2\2"+
		"\u019d\u019e\7g\2\2\u019e\u019f\7e\2\2\u019f\u01a0\7m\2\2\u01a0\u01a1"+
		"\7g\2\2\u01a1\u01a2\7f\2\2\u01a2\u008c\3\2\2\2\u01a3\u01a4\7f\2\2\u01a4"+
		"\u01a5\7g\2\2\u01a5\u01a6\7h\2\2\u01a6\u01a7\7c\2\2\u01a7\u01a8\7w\2\2"+
		"\u01a8\u01a9\7n\2\2\u01a9\u01aa\7v\2\2\u01aa\u008e\3\2\2\2\u01ab\u01ac"+
		"\7f\2\2\u01ac\u01ad\7g\2\2\u01ad\u01ae\7n\2\2\u01ae\u01af\7g\2\2\u01af"+
		"\u01b0\7i\2\2\u01b0\u01b1\7c\2\2\u01b1\u01b2\7v\2\2\u01b2\u01b3\7g\2\2"+
		"\u01b3\u0090\3\2\2\2\u01b4\u01b5\7u\2\2\u01b5\u01b6\7k\2\2\u01b6\u01b7"+
		"\7|\2\2\u01b7\u01b8\7g\2\2\u01b8\u01b9\7q\2\2\u01b9\u01ba\7h\2\2\u01ba"+
		"\u0092\3\2\2\2\u01bb\u01bc\7y\2\2\u01bc\u01bd\7k\2\2\u01bd\u01be\7v\2"+
		"\2\u01be\u01bf\7j\2\2\u01bf\u0094\3\2\2\2\u01c0\u01c1\7i\2\2\u01c1\u01c2"+
		"\7g\2\2\u01c2\u01c3\7v\2\2\u01c3\u0096\3\2\2\2\u01c4\u01c5\7u\2\2\u01c5"+
		"\u01c6\7g\2\2\u01c6\u01c7\7v\2\2\u01c7\u0098\3\2\2\2\u01c8\u01c9\7r\2"+
		"\2\u01c9\u01ca\7w\2\2\u01ca\u01cb\7d\2\2\u01cb\u01cc\7n\2\2\u01cc\u01cd"+
		"\7k\2\2\u01cd\u01ce\7e\2\2\u01ce\u009a\3\2\2\2\u01cf\u01d0\7r\2\2\u01d0"+
		"\u01d1\7t\2\2\u01d1\u01d2\7k\2\2\u01d2\u01d3\7x\2\2\u01d3\u01d4\7c\2\2"+
		"\u01d4\u01d5\7v\2\2\u01d5\u01d6\7g\2\2\u01d6\u009c\3\2\2\2\u01d7\u01d8"+
		"\7r\2\2\u01d8\u01d9\7t\2\2\u01d9\u01da\7q\2\2\u01da\u01db\7v\2\2\u01db"+
		"\u01dc\7g\2\2\u01dc\u01dd\7e\2\2\u01dd\u01de\7v\2\2\u01de\u01df\7g\2\2"+
		"\u01df\u01e0\7f\2\2\u01e0\u009e\3\2\2\2\u01e1\u01e2\7u\2\2\u01e2\u01e3"+
		"\7v\2\2\u01e3\u01e4\7c\2\2\u01e4\u01e5\7v\2\2\u01e5\u01e6\7k\2\2\u01e6"+
		"\u01e7\7e\2\2\u01e7\u00a0\3\2\2\2\u01e8\u01e9\7c\2\2\u01e9\u01ea\7d\2"+
		"\2\u01ea\u01eb\7u\2\2\u01eb\u01ec\7v\2\2\u01ec\u01ed\7t\2\2\u01ed\u01ee"+
		"\7c\2\2\u01ee\u01ef\7e\2\2\u01ef\u01f0\7v\2\2\u01f0\u00a2\3\2\2\2\u01f1"+
		"\u01f2\7x\2\2\u01f2\u01f3\7k\2\2\u01f3\u01f4\7t\2\2\u01f4\u01f5\7v\2\2"+
		"\u01f5\u01f6\7w\2\2\u01f6\u01f7\7c\2\2\u01f7\u01f8\7n\2\2\u01f8\u00a4"+
		"\3\2\2\2\u01f9\u01fa\7q\2\2\u01fa\u01fb\7x\2\2\u01fb\u01fc\7g\2\2\u01fc"+
		"\u01fd\7t\2\2\u01fd\u01fe\7t\2\2\u01fe\u01ff\7k\2\2\u01ff\u0200\7f\2\2"+
		"\u0200\u0201\7g\2\2\u0201\u00a6\3\2\2\2\u0202\u0203\7t\2\2\u0203\u0204"+
		"\7g\2\2\u0204\u0205\7h\2\2\u0205\u00a8\3\2\2\2\u0206\u0207\7e\2\2\u0207"+
		"\u0208\7n\2\2\u0208\u0209\7c\2\2\u0209\u020a\7u\2\2\u020a\u020b\7u\2\2"+
		"\u020b\u00aa\3\2\2\2\u020c\u020d\7u\2\2\u020d\u020e\7v\2\2\u020e\u020f"+
		"\7t\2\2\u020f\u0210\7w\2\2\u0210\u0211\7e\2\2\u0211\u0212\7v\2\2\u0212"+
		"\u00ac\3\2\2\2\u0213\u0219\7$\2\2\u0214\u0215\7^\2\2\u0215\u021a\7^\2"+
		"\2\u0216\u0217\7^\2\2\u0217\u021a\7$\2\2\u0218\u021a\n\2\2\2\u0219\u0214"+
		"\3\2\2\2\u0219\u0216\3\2\2\2\u0219\u0218\3\2\2\2\u021a\u021b\3\2\2\2\u021b"+
		"\u021c\7$\2\2\u021c\u00ae\3\2\2\2\u021d\u021f\t\3\2\2\u021e\u021d\3\2"+
		"\2\2\u021f\u0220\3\2\2\2\u0220\u021e\3\2\2\2\u0220\u0221\3\2\2\2\u0221"+
		"\u0222\3\2\2\2\u0222\u0224\7\60\2\2\u0223\u0225\t\3\2\2\u0224\u0223\3"+
		"\2\2\2\u0225\u0226\3\2\2\2\u0226\u0224\3\2\2\2\u0226\u0227\3\2\2\2\u0227"+
		"\u00b0\3\2\2\2\u0228\u022a\t\3\2\2\u0229\u0228\3\2\2\2\u022a\u022b\3\2"+
		"\2\2\u022b\u0229\3\2\2\2\u022b\u022c\3\2\2\2\u022c\u00b2\3\2\2\2\u022d"+
		"\u022f\t\4\2\2\u022e\u022d\3\2\2\2\u022f\u00b4\3\2\2\2\u0230\u0233\5\u00b3"+
		"Z\2\u0231\u0233\t\3\2\2\u0232\u0230\3\2\2\2\u0232\u0231\3\2\2\2\u0233"+
		"\u00b6\3\2\2\2\u0234\u0238\5\u00b3Z\2\u0235\u0237\5\u00b5[\2\u0236\u0235"+
		"\3\2\2\2\u0237\u023a\3\2\2\2\u0238\u0236\3\2\2\2\u0238\u0239\3\2\2\2\u0239"+
		"\u00b8\3\2\2\2\u023a\u0238\3\2\2\2\u023b\u023d\t\5\2\2\u023c\u023b\3\2"+
		"\2\2\u023d\u023e\3\2\2\2\u023e\u023c\3\2\2\2\u023e\u023f\3\2\2\2\u023f"+
		"\u0240\3\2\2\2\u0240\u0241\b]\2\2\u0241\u00ba\3\2\2\2\u0242\u0244\t\6"+
		"\2\2\u0243\u0242\3\2\2\2\u0244\u0245\3\2\2\2\u0245\u0243\3\2\2\2\u0245"+
		"\u0246\3\2\2\2\u0246\u0247\3\2\2\2\u0247\u0248\b^\2\2\u0248\u00bc\3\2"+
		"\2\2\f\2\u0219\u0220\u0226\u022b\u022e\u0232\u0238\u023e\u0245\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}