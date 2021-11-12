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
		NOT=51, IN=52, OUT=53, LAMBDA=54, NULL_COALESCING=55, IF=56, ELSE=57, 
		FOR=58, FOREACH=59, DO=60, WHILE=61, RETURN=62, THROW=63, TRY=64, CATCH=65, 
		FINALLY=66, NEW=67, TYPEOF=68, CHECKED=69, UNCHECKED=70, DEFAULT=71, DELEGATE=72, 
		SIZEOF=73, WITH=74, GET=75, SET=76, PUBLIC=77, PRIVATE=78, PROTECTED=79, 
		STATIC=80, ABSTRACT=81, VIRTUAL=82, OVERRIDE=83, REF=84, CLASS=85, STRUCT=86, 
		WHITESPACE=87, SINGLELINE_COMMENT=88, NEWLINE=89, MULTILINE_COMMENT=90, 
		STRING=91, DECIMAL=92, INTEGER=93, BOOLEAN=94, NAME=95;
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
			"RANGE_EXCLUSIVE", "IS", "AS", "NOT", "IN", "OUT", "LAMBDA", "NULL_COALESCING", 
			"IF", "ELSE", "FOR", "FOREACH", "DO", "WHILE", "RETURN", "THROW", "TRY", 
			"CATCH", "FINALLY", "NEW", "TYPEOF", "CHECKED", "UNCHECKED", "DEFAULT", 
			"DELEGATE", "SIZEOF", "WITH", "GET", "SET", "PUBLIC", "PRIVATE", "PROTECTED", 
			"STATIC", "ABSTRACT", "VIRTUAL", "OVERRIDE", "REF", "CLASS", "STRUCT", 
			"WHITESPACE", "SINGLELINE_COMMENT", "NEWLINE", "MULTILINE_COMMENT", "ESCAPE", 
			"ESCAPABLE", "STRING", "DECIMAL", "INTEGER", "BOOLEAN", "SIMPLE_NAME_CHARACTER", 
			"COMPLEX_NAME_CHARACTER", "NAME"
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
			"'..'", "'..^'", "'is'", "'as'", "'not'", "'in'", "'out'", "'=>'", "'??'", 
			"'if'", "'else'", "'for'", "'foreach'", "'do'", "'while'", "'return'", 
			"'throw'", "'try'", "'catch'", "'finally'", "'new'", "'typeof'", "'checked'", 
			"'unchecked'", "'default'", "'delegate'", "'sizeof'", "'with'", "'get'", 
			"'set'", "'public'", "'private'", "'protected'", "'static'", "'abstract'", 
			"'virtual'", "'override'", "'ref'", "'class'", "'struct'"
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
			"RANGE_EXCLUSIVE", "IS", "AS", "NOT", "IN", "OUT", "LAMBDA", "NULL_COALESCING", 
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2a\u0285\b\1\4\2\t"+
		"\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13"+
		"\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4\22\t\22"+
		"\4\23\t\23\4\24\t\24\4\25\t\25\4\26\t\26\4\27\t\27\4\30\t\30\4\31\t\31"+
		"\4\32\t\32\4\33\t\33\4\34\t\34\4\35\t\35\4\36\t\36\4\37\t\37\4 \t \4!"+
		"\t!\4\"\t\"\4#\t#\4$\t$\4%\t%\4&\t&\4\'\t\'\4(\t(\4)\t)\4*\t*\4+\t+\4"+
		",\t,\4-\t-\4.\t.\4/\t/\4\60\t\60\4\61\t\61\4\62\t\62\4\63\t\63\4\64\t"+
		"\64\4\65\t\65\4\66\t\66\4\67\t\67\48\t8\49\t9\4:\t:\4;\t;\4<\t<\4=\t="+
		"\4>\t>\4?\t?\4@\t@\4A\tA\4B\tB\4C\tC\4D\tD\4E\tE\4F\tF\4G\tG\4H\tH\4I"+
		"\tI\4J\tJ\4K\tK\4L\tL\4M\tM\4N\tN\4O\tO\4P\tP\4Q\tQ\4R\tR\4S\tS\4T\tT"+
		"\4U\tU\4V\tV\4W\tW\4X\tX\4Y\tY\4Z\tZ\4[\t[\4\\\t\\\4]\t]\4^\t^\4_\t_\4"+
		"`\t`\4a\ta\4b\tb\4c\tc\4d\td\3\2\3\2\3\3\3\3\3\4\3\4\3\5\3\5\3\6\3\6\3"+
		"\7\3\7\3\b\3\b\3\t\3\t\3\n\3\n\3\13\3\13\3\f\3\f\3\r\3\r\3\16\3\16\3\17"+
		"\3\17\3\20\3\20\3\20\3\21\3\21\3\21\3\22\3\22\3\23\3\23\3\24\3\24\3\25"+
		"\3\25\3\26\3\26\3\26\3\27\3\27\3\27\3\30\3\30\3\31\3\31\3\31\3\32\3\32"+
		"\3\32\3\33\3\33\3\33\3\34\3\34\3\34\3\35\3\35\3\36\3\36\3\37\3\37\3\37"+
		"\3 \3 \3 \3!\3!\3\"\3\"\3#\3#\3#\3$\3$\3$\3%\3%\3%\3&\3&\3&\3\'\3\'\3"+
		"\'\3(\3(\3(\3)\3)\3)\3*\3*\3*\3+\3+\3+\3,\3,\3,\3,\3-\3-\3-\3-\3.\3.\3"+
		"/\3/\3\60\3\60\3\60\3\61\3\61\3\61\3\61\3\62\3\62\3\62\3\63\3\63\3\63"+
		"\3\64\3\64\3\64\3\64\3\65\3\65\3\65\3\66\3\66\3\66\3\66\3\67\3\67\3\67"+
		"\38\38\38\39\39\39\3:\3:\3:\3:\3:\3;\3;\3;\3;\3<\3<\3<\3<\3<\3<\3<\3<"+
		"\3=\3=\3=\3>\3>\3>\3>\3>\3>\3?\3?\3?\3?\3?\3?\3?\3@\3@\3@\3@\3@\3@\3A"+
		"\3A\3A\3A\3B\3B\3B\3B\3B\3B\3C\3C\3C\3C\3C\3C\3C\3C\3D\3D\3D\3D\3E\3E"+
		"\3E\3E\3E\3E\3E\3F\3F\3F\3F\3F\3F\3F\3F\3G\3G\3G\3G\3G\3G\3G\3G\3G\3G"+
		"\3H\3H\3H\3H\3H\3H\3H\3H\3I\3I\3I\3I\3I\3I\3I\3I\3I\3J\3J\3J\3J\3J\3J"+
		"\3J\3K\3K\3K\3K\3K\3L\3L\3L\3L\3M\3M\3M\3M\3N\3N\3N\3N\3N\3N\3N\3O\3O"+
		"\3O\3O\3O\3O\3O\3O\3P\3P\3P\3P\3P\3P\3P\3P\3P\3P\3Q\3Q\3Q\3Q\3Q\3Q\3Q"+
		"\3R\3R\3R\3R\3R\3R\3R\3R\3R\3S\3S\3S\3S\3S\3S\3S\3S\3T\3T\3T\3T\3T\3T"+
		"\3T\3T\3T\3U\3U\3U\3U\3V\3V\3V\3V\3V\3V\3W\3W\3W\3W\3W\3W\3W\3X\6X\u0225"+
		"\nX\rX\16X\u0226\3X\3X\3Y\3Y\3Y\3Y\7Y\u022f\nY\fY\16Y\u0232\13Y\3Y\3Y"+
		"\3Z\6Z\u0237\nZ\rZ\16Z\u0238\3Z\3Z\3[\3[\3[\3[\7[\u0241\n[\f[\16[\u0244"+
		"\13[\3[\3[\3[\3[\3[\3\\\3\\\3]\3]\5]\u024f\n]\3^\3^\3^\3^\3^\7^\u0256"+
		"\n^\f^\16^\u0259\13^\3^\3^\3_\6_\u025e\n_\r_\16_\u025f\3_\3_\6_\u0264"+
		"\n_\r_\16_\u0265\3`\6`\u0269\n`\r`\16`\u026a\3a\3a\3a\3a\3a\3a\3a\3a\3"+
		"a\5a\u0276\na\3b\5b\u0279\nb\3c\3c\5c\u027d\nc\3d\3d\7d\u0281\nd\fd\16"+
		"d\u0284\13d\3\u0242\2e\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27"+
		"\r\31\16\33\17\35\20\37\21!\22#\23%\24\'\25)\26+\27-\30/\31\61\32\63\33"+
		"\65\34\67\359\36;\37= ?!A\"C#E$G%I&K\'M(O)Q*S+U,W-Y.[/]\60_\61a\62c\63"+
		"e\64g\65i\66k\67m8o9q:s;u<w=y>{?}@\177A\u0081B\u0083C\u0085D\u0087E\u0089"+
		"F\u008bG\u008dH\u008fI\u0091J\u0093K\u0095L\u0097M\u0099N\u009bO\u009d"+
		"P\u009fQ\u00a1R\u00a3S\u00a5T\u00a7U\u00a9V\u00abW\u00adX\u00afY\u00b1"+
		"Z\u00b3[\u00b5\\\u00b7\2\u00b9\2\u00bb]\u00bd^\u00bf_\u00c1`\u00c3\2\u00c5"+
		"\2\u00c7a\3\2\7\5\2\13\13\17\17\"\"\3\2\f\f\3\2$$\3\2\62;\6\2//C\\aac"+
		"|\2\u028d\2\3\3\2\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2"+
		"\2\r\3\2\2\2\2\17\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27"+
		"\3\2\2\2\2\31\3\2\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2"+
		"\2\2#\3\2\2\2\2%\3\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2"+
		"\2/\3\2\2\2\2\61\3\2\2\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2\29\3\2"+
		"\2\2\2;\3\2\2\2\2=\3\2\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E\3\2\2\2"+
		"\2G\3\2\2\2\2I\3\2\2\2\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2\2\2\2S"+
		"\3\2\2\2\2U\3\2\2\2\2W\3\2\2\2\2Y\3\2\2\2\2[\3\2\2\2\2]\3\2\2\2\2_\3\2"+
		"\2\2\2a\3\2\2\2\2c\3\2\2\2\2e\3\2\2\2\2g\3\2\2\2\2i\3\2\2\2\2k\3\2\2\2"+
		"\2m\3\2\2\2\2o\3\2\2\2\2q\3\2\2\2\2s\3\2\2\2\2u\3\2\2\2\2w\3\2\2\2\2y"+
		"\3\2\2\2\2{\3\2\2\2\2}\3\2\2\2\2\177\3\2\2\2\2\u0081\3\2\2\2\2\u0083\3"+
		"\2\2\2\2\u0085\3\2\2\2\2\u0087\3\2\2\2\2\u0089\3\2\2\2\2\u008b\3\2\2\2"+
		"\2\u008d\3\2\2\2\2\u008f\3\2\2\2\2\u0091\3\2\2\2\2\u0093\3\2\2\2\2\u0095"+
		"\3\2\2\2\2\u0097\3\2\2\2\2\u0099\3\2\2\2\2\u009b\3\2\2\2\2\u009d\3\2\2"+
		"\2\2\u009f\3\2\2\2\2\u00a1\3\2\2\2\2\u00a3\3\2\2\2\2\u00a5\3\2\2\2\2\u00a7"+
		"\3\2\2\2\2\u00a9\3\2\2\2\2\u00ab\3\2\2\2\2\u00ad\3\2\2\2\2\u00af\3\2\2"+
		"\2\2\u00b1\3\2\2\2\2\u00b3\3\2\2\2\2\u00b5\3\2\2\2\2\u00bb\3\2\2\2\2\u00bd"+
		"\3\2\2\2\2\u00bf\3\2\2\2\2\u00c1\3\2\2\2\2\u00c7\3\2\2\2\3\u00c9\3\2\2"+
		"\2\5\u00cb\3\2\2\2\7\u00cd\3\2\2\2\t\u00cf\3\2\2\2\13\u00d1\3\2\2\2\r"+
		"\u00d3\3\2\2\2\17\u00d5\3\2\2\2\21\u00d7\3\2\2\2\23\u00d9\3\2\2\2\25\u00db"+
		"\3\2\2\2\27\u00dd\3\2\2\2\31\u00df\3\2\2\2\33\u00e1\3\2\2\2\35\u00e3\3"+
		"\2\2\2\37\u00e5\3\2\2\2!\u00e8\3\2\2\2#\u00eb\3\2\2\2%\u00ed\3\2\2\2\'"+
		"\u00ef\3\2\2\2)\u00f1\3\2\2\2+\u00f3\3\2\2\2-\u00f6\3\2\2\2/\u00f9\3\2"+
		"\2\2\61\u00fb\3\2\2\2\63\u00fe\3\2\2\2\65\u0101\3\2\2\2\67\u0104\3\2\2"+
		"\29\u0107\3\2\2\2;\u0109\3\2\2\2=\u010b\3\2\2\2?\u010e\3\2\2\2A\u0111"+
		"\3\2\2\2C\u0113\3\2\2\2E\u0115\3\2\2\2G\u0118\3\2\2\2I\u011b\3\2\2\2K"+
		"\u011e\3\2\2\2M\u0121\3\2\2\2O\u0124\3\2\2\2Q\u0127\3\2\2\2S\u012a\3\2"+
		"\2\2U\u012d\3\2\2\2W\u0130\3\2\2\2Y\u0134\3\2\2\2[\u0138\3\2\2\2]\u013a"+
		"\3\2\2\2_\u013c\3\2\2\2a\u013f\3\2\2\2c\u0143\3\2\2\2e\u0146\3\2\2\2g"+
		"\u0149\3\2\2\2i\u014d\3\2\2\2k\u0150\3\2\2\2m\u0154\3\2\2\2o\u0157\3\2"+
		"\2\2q\u015a\3\2\2\2s\u015d\3\2\2\2u\u0162\3\2\2\2w\u0166\3\2\2\2y\u016e"+
		"\3\2\2\2{\u0171\3\2\2\2}\u0177\3\2\2\2\177\u017e\3\2\2\2\u0081\u0184\3"+
		"\2\2\2\u0083\u0188\3\2\2\2\u0085\u018e\3\2\2\2\u0087\u0196\3\2\2\2\u0089"+
		"\u019a\3\2\2\2\u008b\u01a1\3\2\2\2\u008d\u01a9\3\2\2\2\u008f\u01b3\3\2"+
		"\2\2\u0091\u01bb\3\2\2\2\u0093\u01c4\3\2\2\2\u0095\u01cb\3\2\2\2\u0097"+
		"\u01d0\3\2\2\2\u0099\u01d4\3\2\2\2\u009b\u01d8\3\2\2\2\u009d\u01df\3\2"+
		"\2\2\u009f\u01e7\3\2\2\2\u00a1\u01f1\3\2\2\2\u00a3\u01f8\3\2\2\2\u00a5"+
		"\u0201\3\2\2\2\u00a7\u0209\3\2\2\2\u00a9\u0212\3\2\2\2\u00ab\u0216\3\2"+
		"\2\2\u00ad\u021c\3\2\2\2\u00af\u0224\3\2\2\2\u00b1\u022a\3\2\2\2\u00b3"+
		"\u0236\3\2\2\2\u00b5\u023c\3\2\2\2\u00b7\u024a\3\2\2\2\u00b9\u024e\3\2"+
		"\2\2\u00bb\u0250\3\2\2\2\u00bd\u025d\3\2\2\2\u00bf\u0268\3\2\2\2\u00c1"+
		"\u0275\3\2\2\2\u00c3\u0278\3\2\2\2\u00c5\u027c\3\2\2\2\u00c7\u027e\3\2"+
		"\2\2\u00c9\u00ca\7B\2\2\u00ca\4\3\2\2\2\u00cb\u00cc\7=\2\2\u00cc\6\3\2"+
		"\2\2\u00cd\u00ce\7.\2\2\u00ce\b\3\2\2\2\u00cf\u00d0\7*\2\2\u00d0\n\3\2"+
		"\2\2\u00d1\u00d2\7+\2\2\u00d2\f\3\2\2\2\u00d3\u00d4\7]\2\2\u00d4\16\3"+
		"\2\2\2\u00d5\u00d6\7_\2\2\u00d6\20\3\2\2\2\u00d7\u00d8\7}\2\2\u00d8\22"+
		"\3\2\2\2\u00d9\u00da\7\177\2\2\u00da\24\3\2\2\2\u00db\u00dc\7-\2\2\u00dc"+
		"\26\3\2\2\2\u00dd\u00de\7/\2\2\u00de\30\3\2\2\2\u00df\u00e0\7,\2\2\u00e0"+
		"\32\3\2\2\2\u00e1\u00e2\7\61\2\2\u00e2\34\3\2\2\2\u00e3\u00e4\7\'\2\2"+
		"\u00e4\36\3\2\2\2\u00e5\u00e6\7-\2\2\u00e6\u00e7\7-\2\2\u00e7 \3\2\2\2"+
		"\u00e8\u00e9\7/\2\2\u00e9\u00ea\7/\2\2\u00ea\"\3\2\2\2\u00eb\u00ec\7("+
		"\2\2\u00ec$\3\2\2\2\u00ed\u00ee\7~\2\2\u00ee&\3\2\2\2\u00ef\u00f0\7`\2"+
		"\2\u00f0(\3\2\2\2\u00f1\u00f2\7\u0080\2\2\u00f2*\3\2\2\2\u00f3\u00f4\7"+
		"(\2\2\u00f4\u00f5\7(\2\2\u00f5,\3\2\2\2\u00f6\u00f7\7~\2\2\u00f7\u00f8"+
		"\7~\2\2\u00f8.\3\2\2\2\u00f9\u00fa\7#\2\2\u00fa\60\3\2\2\2\u00fb\u00fc"+
		"\7>\2\2\u00fc\u00fd\7>\2\2\u00fd\62\3\2\2\2\u00fe\u00ff\7@\2\2\u00ff\u0100"+
		"\7@\2\2\u0100\64\3\2\2\2\u0101\u0102\7?\2\2\u0102\u0103\7?\2\2\u0103\66"+
		"\3\2\2\2\u0104\u0105\7#\2\2\u0105\u0106\7?\2\2\u01068\3\2\2\2\u0107\u0108"+
		"\7@\2\2\u0108:\3\2\2\2\u0109\u010a\7>\2\2\u010a<\3\2\2\2\u010b\u010c\7"+
		"@\2\2\u010c\u010d\7?\2\2\u010d>\3\2\2\2\u010e\u010f\7>\2\2\u010f\u0110"+
		"\7?\2\2\u0110@\3\2\2\2\u0111\u0112\7\60\2\2\u0112B\3\2\2\2\u0113\u0114"+
		"\7?\2\2\u0114D\3\2\2\2\u0115\u0116\7-\2\2\u0116\u0117\7?\2\2\u0117F\3"+
		"\2\2\2\u0118\u0119\7/\2\2\u0119\u011a\7?\2\2\u011aH\3\2\2\2\u011b\u011c"+
		"\7,\2\2\u011c\u011d\7?\2\2\u011dJ\3\2\2\2\u011e\u011f\7\61\2\2\u011f\u0120"+
		"\7?\2\2\u0120L\3\2\2\2\u0121\u0122\7\'\2\2\u0122\u0123\7?\2\2\u0123N\3"+
		"\2\2\2\u0124\u0125\7\60\2\2\u0125\u0126\7?\2\2\u0126P\3\2\2\2\u0127\u0128"+
		"\7(\2\2\u0128\u0129\7?\2\2\u0129R\3\2\2\2\u012a\u012b\7~\2\2\u012b\u012c"+
		"\7?\2\2\u012cT\3\2\2\2\u012d\u012e\7`\2\2\u012e\u012f\7?\2\2\u012fV\3"+
		"\2\2\2\u0130\u0131\7>\2\2\u0131\u0132\7>\2\2\u0132\u0133\7?\2\2\u0133"+
		"X\3\2\2\2\u0134\u0135\7@\2\2\u0135\u0136\7@\2\2\u0136\u0137\7?\2\2\u0137"+
		"Z\3\2\2\2\u0138\u0139\7A\2\2\u0139\\\3\2\2\2\u013a\u013b\7<\2\2\u013b"+
		"^\3\2\2\2\u013c\u013d\7\60\2\2\u013d\u013e\7\60\2\2\u013e`\3\2\2\2\u013f"+
		"\u0140\7\60\2\2\u0140\u0141\7\60\2\2\u0141\u0142\7`\2\2\u0142b\3\2\2\2"+
		"\u0143\u0144\7k\2\2\u0144\u0145\7u\2\2\u0145d\3\2\2\2\u0146\u0147\7c\2"+
		"\2\u0147\u0148\7u\2\2\u0148f\3\2\2\2\u0149\u014a\7p\2\2\u014a\u014b\7"+
		"q\2\2\u014b\u014c\7v\2\2\u014ch\3\2\2\2\u014d\u014e\7k\2\2\u014e\u014f"+
		"\7p\2\2\u014fj\3\2\2\2\u0150\u0151\7q\2\2\u0151\u0152\7w\2\2\u0152\u0153"+
		"\7v\2\2\u0153l\3\2\2\2\u0154\u0155\7?\2\2\u0155\u0156\7@\2\2\u0156n\3"+
		"\2\2\2\u0157\u0158\7A\2\2\u0158\u0159\7A\2\2\u0159p\3\2\2\2\u015a\u015b"+
		"\7k\2\2\u015b\u015c\7h\2\2\u015cr\3\2\2\2\u015d\u015e\7g\2\2\u015e\u015f"+
		"\7n\2\2\u015f\u0160\7u\2\2\u0160\u0161\7g\2\2\u0161t\3\2\2\2\u0162\u0163"+
		"\7h\2\2\u0163\u0164\7q\2\2\u0164\u0165\7t\2\2\u0165v\3\2\2\2\u0166\u0167"+
		"\7h\2\2\u0167\u0168\7q\2\2\u0168\u0169\7t\2\2\u0169\u016a\7g\2\2\u016a"+
		"\u016b\7c\2\2\u016b\u016c\7e\2\2\u016c\u016d\7j\2\2\u016dx\3\2\2\2\u016e"+
		"\u016f\7f\2\2\u016f\u0170\7q\2\2\u0170z\3\2\2\2\u0171\u0172\7y\2\2\u0172"+
		"\u0173\7j\2\2\u0173\u0174\7k\2\2\u0174\u0175\7n\2\2\u0175\u0176\7g\2\2"+
		"\u0176|\3\2\2\2\u0177\u0178\7t\2\2\u0178\u0179\7g\2\2\u0179\u017a\7v\2"+
		"\2\u017a\u017b\7w\2\2\u017b\u017c\7t\2\2\u017c\u017d\7p\2\2\u017d~\3\2"+
		"\2\2\u017e\u017f\7v\2\2\u017f\u0180\7j\2\2\u0180\u0181\7t\2\2\u0181\u0182"+
		"\7q\2\2\u0182\u0183\7y\2\2\u0183\u0080\3\2\2\2\u0184\u0185\7v\2\2\u0185"+
		"\u0186\7t\2\2\u0186\u0187\7{\2\2\u0187\u0082\3\2\2\2\u0188\u0189\7e\2"+
		"\2\u0189\u018a\7c\2\2\u018a\u018b\7v\2\2\u018b\u018c\7e\2\2\u018c\u018d"+
		"\7j\2\2\u018d\u0084\3\2\2\2\u018e\u018f\7h\2\2\u018f\u0190\7k\2\2\u0190"+
		"\u0191\7p\2\2\u0191\u0192\7c\2\2\u0192\u0193\7n\2\2\u0193\u0194\7n\2\2"+
		"\u0194\u0195\7{\2\2\u0195\u0086\3\2\2\2\u0196\u0197\7p\2\2\u0197\u0198"+
		"\7g\2\2\u0198\u0199\7y\2\2\u0199\u0088\3\2\2\2\u019a\u019b\7v\2\2\u019b"+
		"\u019c\7{\2\2\u019c\u019d\7r\2\2\u019d\u019e\7g\2\2\u019e\u019f\7q\2\2"+
		"\u019f\u01a0\7h\2\2\u01a0\u008a\3\2\2\2\u01a1\u01a2\7e\2\2\u01a2\u01a3"+
		"\7j\2\2\u01a3\u01a4\7g\2\2\u01a4\u01a5\7e\2\2\u01a5\u01a6\7m\2\2\u01a6"+
		"\u01a7\7g\2\2\u01a7\u01a8\7f\2\2\u01a8\u008c\3\2\2\2\u01a9\u01aa\7w\2"+
		"\2\u01aa\u01ab\7p\2\2\u01ab\u01ac\7e\2\2\u01ac\u01ad\7j\2\2\u01ad\u01ae"+
		"\7g\2\2\u01ae\u01af\7e\2\2\u01af\u01b0\7m\2\2\u01b0\u01b1\7g\2\2\u01b1"+
		"\u01b2\7f\2\2\u01b2\u008e\3\2\2\2\u01b3\u01b4\7f\2\2\u01b4\u01b5\7g\2"+
		"\2\u01b5\u01b6\7h\2\2\u01b6\u01b7\7c\2\2\u01b7\u01b8\7w\2\2\u01b8\u01b9"+
		"\7n\2\2\u01b9\u01ba\7v\2\2\u01ba\u0090\3\2\2\2\u01bb\u01bc\7f\2\2\u01bc"+
		"\u01bd\7g\2\2\u01bd\u01be\7n\2\2\u01be\u01bf\7g\2\2\u01bf\u01c0\7i\2\2"+
		"\u01c0\u01c1\7c\2\2\u01c1\u01c2\7v\2\2\u01c2\u01c3\7g\2\2\u01c3\u0092"+
		"\3\2\2\2\u01c4\u01c5\7u\2\2\u01c5\u01c6\7k\2\2\u01c6\u01c7\7|\2\2\u01c7"+
		"\u01c8\7g\2\2\u01c8\u01c9\7q\2\2\u01c9\u01ca\7h\2\2\u01ca\u0094\3\2\2"+
		"\2\u01cb\u01cc\7y\2\2\u01cc\u01cd\7k\2\2\u01cd\u01ce\7v\2\2\u01ce\u01cf"+
		"\7j\2\2\u01cf\u0096\3\2\2\2\u01d0\u01d1\7i\2\2\u01d1\u01d2\7g\2\2\u01d2"+
		"\u01d3\7v\2\2\u01d3\u0098\3\2\2\2\u01d4\u01d5\7u\2\2\u01d5\u01d6\7g\2"+
		"\2\u01d6\u01d7\7v\2\2\u01d7\u009a\3\2\2\2\u01d8\u01d9\7r\2\2\u01d9\u01da"+
		"\7w\2\2\u01da\u01db\7d\2\2\u01db\u01dc\7n\2\2\u01dc\u01dd\7k\2\2\u01dd"+
		"\u01de\7e\2\2\u01de\u009c\3\2\2\2\u01df\u01e0\7r\2\2\u01e0\u01e1\7t\2"+
		"\2\u01e1\u01e2\7k\2\2\u01e2\u01e3\7x\2\2\u01e3\u01e4\7c\2\2\u01e4\u01e5"+
		"\7v\2\2\u01e5\u01e6\7g\2\2\u01e6\u009e\3\2\2\2\u01e7\u01e8\7r\2\2\u01e8"+
		"\u01e9\7t\2\2\u01e9\u01ea\7q\2\2\u01ea\u01eb\7v\2\2\u01eb\u01ec\7g\2\2"+
		"\u01ec\u01ed\7e\2\2\u01ed\u01ee\7v\2\2\u01ee\u01ef\7g\2\2\u01ef\u01f0"+
		"\7f\2\2\u01f0\u00a0\3\2\2\2\u01f1\u01f2\7u\2\2\u01f2\u01f3\7v\2\2\u01f3"+
		"\u01f4\7c\2\2\u01f4\u01f5\7v\2\2\u01f5\u01f6\7k\2\2\u01f6\u01f7\7e\2\2"+
		"\u01f7\u00a2\3\2\2\2\u01f8\u01f9\7c\2\2\u01f9\u01fa\7d\2\2\u01fa\u01fb"+
		"\7u\2\2\u01fb\u01fc\7v\2\2\u01fc\u01fd\7t\2\2\u01fd\u01fe\7c\2\2\u01fe"+
		"\u01ff\7e\2\2\u01ff\u0200\7v\2\2\u0200\u00a4\3\2\2\2\u0201\u0202\7x\2"+
		"\2\u0202\u0203\7k\2\2\u0203\u0204\7t\2\2\u0204\u0205\7v\2\2\u0205\u0206"+
		"\7w\2\2\u0206\u0207\7c\2\2\u0207\u0208\7n\2\2\u0208\u00a6\3\2\2\2\u0209"+
		"\u020a\7q\2\2\u020a\u020b\7x\2\2\u020b\u020c\7g\2\2\u020c\u020d\7t\2\2"+
		"\u020d\u020e\7t\2\2\u020e\u020f\7k\2\2\u020f\u0210\7f\2\2\u0210\u0211"+
		"\7g\2\2\u0211\u00a8\3\2\2\2\u0212\u0213\7t\2\2\u0213\u0214\7g\2\2\u0214"+
		"\u0215\7h\2\2\u0215\u00aa\3\2\2\2\u0216\u0217\7e\2\2\u0217\u0218\7n\2"+
		"\2\u0218\u0219\7c\2\2\u0219\u021a\7u\2\2\u021a\u021b\7u\2\2\u021b\u00ac"+
		"\3\2\2\2\u021c\u021d\7u\2\2\u021d\u021e\7v\2\2\u021e\u021f\7t\2\2\u021f"+
		"\u0220\7w\2\2\u0220\u0221\7e\2\2\u0221\u0222\7v\2\2\u0222\u00ae\3\2\2"+
		"\2\u0223\u0225\t\2\2\2\u0224\u0223\3\2\2\2\u0225\u0226\3\2\2\2\u0226\u0224"+
		"\3\2\2\2\u0226\u0227\3\2\2\2\u0227\u0228\3\2\2\2\u0228\u0229\bX\2\2\u0229"+
		"\u00b0\3\2\2\2\u022a\u022b\7\61\2\2\u022b\u022c\7\61\2\2\u022c\u0230\3"+
		"\2\2\2\u022d\u022f\n\3\2\2\u022e\u022d\3\2\2\2\u022f\u0232\3\2\2\2\u0230"+
		"\u022e\3\2\2\2\u0230\u0231\3\2\2\2\u0231\u0233\3\2\2\2\u0232\u0230\3\2"+
		"\2\2\u0233\u0234\bY\2\2\u0234\u00b2\3\2\2\2\u0235\u0237\t\3\2\2\u0236"+
		"\u0235\3\2\2\2\u0237\u0238\3\2\2\2\u0238\u0236\3\2\2\2\u0238\u0239\3\2"+
		"\2\2\u0239\u023a\3\2\2\2\u023a\u023b\bZ\2\2\u023b\u00b4\3\2\2\2\u023c"+
		"\u023d\7\61\2\2\u023d\u023e\7,\2\2\u023e\u0242\3\2\2\2\u023f\u0241\13"+
		"\2\2\2\u0240\u023f\3\2\2\2\u0241\u0244\3\2\2\2\u0242\u0243\3\2\2\2\u0242"+
		"\u0240\3\2\2\2\u0243\u0245\3\2\2\2\u0244\u0242\3\2\2\2\u0245\u0246\7,"+
		"\2\2\u0246\u0247\7\61\2\2\u0247\u0248\3\2\2\2\u0248\u0249\b[\2\2\u0249"+
		"\u00b6\3\2\2\2\u024a\u024b\7^\2\2\u024b\u00b8\3\2\2\2\u024c\u024f\5\u00b7"+
		"\\\2\u024d\u024f\7$\2\2\u024e\u024c\3\2\2\2\u024e\u024d\3\2\2\2\u024f"+
		"\u00ba\3\2\2\2\u0250\u0257\7$\2\2\u0251\u0252\5\u00b7\\\2\u0252\u0253"+
		"\5\u00b9]\2\u0253\u0256\3\2\2\2\u0254\u0256\n\4\2\2\u0255\u0251\3\2\2"+
		"\2\u0255\u0254\3\2\2\2\u0256\u0259\3\2\2\2\u0257\u0255\3\2\2\2\u0257\u0258"+
		"\3\2\2\2\u0258\u025a\3\2\2\2\u0259\u0257\3\2\2\2\u025a\u025b\7$\2\2\u025b"+
		"\u00bc\3\2\2\2\u025c\u025e\t\5\2\2\u025d\u025c\3\2\2\2\u025e\u025f\3\2"+
		"\2\2\u025f\u025d\3\2\2\2\u025f\u0260\3\2\2\2\u0260\u0261\3\2\2\2\u0261"+
		"\u0263\7\60\2\2\u0262\u0264\t\5\2\2\u0263\u0262\3\2\2\2\u0264\u0265\3"+
		"\2\2\2\u0265\u0263\3\2\2\2\u0265\u0266\3\2\2\2\u0266\u00be\3\2\2\2\u0267"+
		"\u0269\t\5\2\2\u0268\u0267\3\2\2\2\u0269\u026a\3\2\2\2\u026a\u0268\3\2"+
		"\2\2\u026a\u026b\3\2\2\2\u026b\u00c0\3\2\2\2\u026c\u026d\7v\2\2\u026d"+
		"\u026e\7t\2\2\u026e\u026f\7w\2\2\u026f\u0276\7g\2\2\u0270\u0271\7h\2\2"+
		"\u0271\u0272\7c\2\2\u0272\u0273\7n\2\2\u0273\u0274\7u\2\2\u0274\u0276"+
		"\7g\2\2\u0275\u026c\3\2\2\2\u0275\u0270\3\2\2\2\u0276\u00c2\3\2\2\2\u0277"+
		"\u0279\t\6\2\2\u0278\u0277\3\2\2\2\u0279\u00c4\3\2\2\2\u027a\u027d\5\u00c3"+
		"b\2\u027b\u027d\t\5\2\2\u027c\u027a\3\2\2\2\u027c\u027b\3\2\2\2\u027d"+
		"\u00c6\3\2\2\2\u027e\u0282\5\u00c3b\2\u027f\u0281\5\u00c5c\2\u0280\u027f"+
		"\3\2\2\2\u0281\u0284\3\2\2\2\u0282\u0280\3\2\2\2\u0282\u0283\3\2\2\2\u0283"+
		"\u00c8\3\2\2\2\u0284\u0282\3\2\2\2\21\2\u0226\u0230\u0238\u0242\u024e"+
		"\u0255\u0257\u025f\u0265\u026a\u0275\u0278\u027c\u0282\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}