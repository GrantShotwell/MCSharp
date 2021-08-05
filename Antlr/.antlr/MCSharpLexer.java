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
		WHITESPACE=86, SINGLELINE_COMMENT=87, NEWLINE=88, MULTILINE_COMMENT=89, 
		STRING=90, DECIMAL=91, INTEGER=92, BOOLEAN=93, NAME=94;
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
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2`\u027f\b\1\4\2\t"+
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
		"`\t`\4a\ta\4b\tb\4c\tc\3\2\3\2\3\3\3\3\3\4\3\4\3\5\3\5\3\6\3\6\3\7\3\7"+
		"\3\b\3\b\3\t\3\t\3\n\3\n\3\13\3\13\3\f\3\f\3\r\3\r\3\16\3\16\3\17\3\17"+
		"\3\20\3\20\3\20\3\21\3\21\3\21\3\22\3\22\3\23\3\23\3\24\3\24\3\25\3\25"+
		"\3\26\3\26\3\26\3\27\3\27\3\27\3\30\3\30\3\31\3\31\3\31\3\32\3\32\3\32"+
		"\3\33\3\33\3\33\3\34\3\34\3\34\3\35\3\35\3\36\3\36\3\37\3\37\3\37\3 \3"+
		" \3 \3!\3!\3\"\3\"\3#\3#\3#\3$\3$\3$\3%\3%\3%\3&\3&\3&\3\'\3\'\3\'\3("+
		"\3(\3(\3)\3)\3)\3*\3*\3*\3+\3+\3+\3,\3,\3,\3,\3-\3-\3-\3-\3.\3.\3/\3/"+
		"\3\60\3\60\3\60\3\61\3\61\3\61\3\61\3\62\3\62\3\62\3\63\3\63\3\63\3\64"+
		"\3\64\3\64\3\65\3\65\3\65\3\65\3\66\3\66\3\66\3\67\3\67\3\67\38\38\38"+
		"\39\39\39\39\39\3:\3:\3:\3:\3;\3;\3;\3;\3;\3;\3;\3;\3<\3<\3<\3=\3=\3="+
		"\3=\3=\3=\3>\3>\3>\3>\3>\3>\3>\3?\3?\3?\3?\3?\3?\3@\3@\3@\3@\3A\3A\3A"+
		"\3A\3A\3A\3B\3B\3B\3B\3B\3B\3B\3B\3C\3C\3C\3C\3D\3D\3D\3D\3D\3D\3D\3E"+
		"\3E\3E\3E\3E\3E\3E\3E\3F\3F\3F\3F\3F\3F\3F\3F\3F\3F\3G\3G\3G\3G\3G\3G"+
		"\3G\3G\3H\3H\3H\3H\3H\3H\3H\3H\3H\3I\3I\3I\3I\3I\3I\3I\3J\3J\3J\3J\3J"+
		"\3K\3K\3K\3K\3L\3L\3L\3L\3M\3M\3M\3M\3M\3M\3M\3N\3N\3N\3N\3N\3N\3N\3N"+
		"\3O\3O\3O\3O\3O\3O\3O\3O\3O\3O\3P\3P\3P\3P\3P\3P\3P\3Q\3Q\3Q\3Q\3Q\3Q"+
		"\3Q\3Q\3Q\3R\3R\3R\3R\3R\3R\3R\3R\3S\3S\3S\3S\3S\3S\3S\3S\3S\3T\3T\3T"+
		"\3T\3U\3U\3U\3U\3U\3U\3V\3V\3V\3V\3V\3V\3V\3W\6W\u021f\nW\rW\16W\u0220"+
		"\3W\3W\3X\3X\3X\3X\7X\u0229\nX\fX\16X\u022c\13X\3X\3X\3Y\6Y\u0231\nY\r"+
		"Y\16Y\u0232\3Y\3Y\3Z\3Z\3Z\3Z\7Z\u023b\nZ\fZ\16Z\u023e\13Z\3Z\3Z\3Z\3"+
		"Z\3Z\3[\3[\3\\\3\\\5\\\u0249\n\\\3]\3]\3]\3]\3]\7]\u0250\n]\f]\16]\u0253"+
		"\13]\3]\3]\3^\6^\u0258\n^\r^\16^\u0259\3^\3^\6^\u025e\n^\r^\16^\u025f"+
		"\3_\6_\u0263\n_\r_\16_\u0264\3`\3`\3`\3`\3`\3`\3`\3`\3`\5`\u0270\n`\3"+
		"a\5a\u0273\na\3b\3b\5b\u0277\nb\3c\3c\7c\u027b\nc\fc\16c\u027e\13c\3\u023c"+
		"\2d\3\3\5\4\7\5\t\6\13\7\r\b\17\t\21\n\23\13\25\f\27\r\31\16\33\17\35"+
		"\20\37\21!\22#\23%\24\'\25)\26+\27-\30/\31\61\32\63\33\65\34\67\359\36"+
		";\37= ?!A\"C#E$G%I&K\'M(O)Q*S+U,W-Y.[/]\60_\61a\62c\63e\64g\65i\66k\67"+
		"m8o9q:s;u<w=y>{?}@\177A\u0081B\u0083C\u0085D\u0087E\u0089F\u008bG\u008d"+
		"H\u008fI\u0091J\u0093K\u0095L\u0097M\u0099N\u009bO\u009dP\u009fQ\u00a1"+
		"R\u00a3S\u00a5T\u00a7U\u00a9V\u00abW\u00adX\u00afY\u00b1Z\u00b3[\u00b5"+
		"\2\u00b7\2\u00b9\\\u00bb]\u00bd^\u00bf_\u00c1\2\u00c3\2\u00c5`\3\2\7\5"+
		"\2\13\13\17\17\"\"\3\2\f\f\3\2$$\3\2\62;\6\2//C\\aac|\2\u0287\2\3\3\2"+
		"\2\2\2\5\3\2\2\2\2\7\3\2\2\2\2\t\3\2\2\2\2\13\3\2\2\2\2\r\3\2\2\2\2\17"+
		"\3\2\2\2\2\21\3\2\2\2\2\23\3\2\2\2\2\25\3\2\2\2\2\27\3\2\2\2\2\31\3\2"+
		"\2\2\2\33\3\2\2\2\2\35\3\2\2\2\2\37\3\2\2\2\2!\3\2\2\2\2#\3\2\2\2\2%\3"+
		"\2\2\2\2\'\3\2\2\2\2)\3\2\2\2\2+\3\2\2\2\2-\3\2\2\2\2/\3\2\2\2\2\61\3"+
		"\2\2\2\2\63\3\2\2\2\2\65\3\2\2\2\2\67\3\2\2\2\29\3\2\2\2\2;\3\2\2\2\2"+
		"=\3\2\2\2\2?\3\2\2\2\2A\3\2\2\2\2C\3\2\2\2\2E\3\2\2\2\2G\3\2\2\2\2I\3"+
		"\2\2\2\2K\3\2\2\2\2M\3\2\2\2\2O\3\2\2\2\2Q\3\2\2\2\2S\3\2\2\2\2U\3\2\2"+
		"\2\2W\3\2\2\2\2Y\3\2\2\2\2[\3\2\2\2\2]\3\2\2\2\2_\3\2\2\2\2a\3\2\2\2\2"+
		"c\3\2\2\2\2e\3\2\2\2\2g\3\2\2\2\2i\3\2\2\2\2k\3\2\2\2\2m\3\2\2\2\2o\3"+
		"\2\2\2\2q\3\2\2\2\2s\3\2\2\2\2u\3\2\2\2\2w\3\2\2\2\2y\3\2\2\2\2{\3\2\2"+
		"\2\2}\3\2\2\2\2\177\3\2\2\2\2\u0081\3\2\2\2\2\u0083\3\2\2\2\2\u0085\3"+
		"\2\2\2\2\u0087\3\2\2\2\2\u0089\3\2\2\2\2\u008b\3\2\2\2\2\u008d\3\2\2\2"+
		"\2\u008f\3\2\2\2\2\u0091\3\2\2\2\2\u0093\3\2\2\2\2\u0095\3\2\2\2\2\u0097"+
		"\3\2\2\2\2\u0099\3\2\2\2\2\u009b\3\2\2\2\2\u009d\3\2\2\2\2\u009f\3\2\2"+
		"\2\2\u00a1\3\2\2\2\2\u00a3\3\2\2\2\2\u00a5\3\2\2\2\2\u00a7\3\2\2\2\2\u00a9"+
		"\3\2\2\2\2\u00ab\3\2\2\2\2\u00ad\3\2\2\2\2\u00af\3\2\2\2\2\u00b1\3\2\2"+
		"\2\2\u00b3\3\2\2\2\2\u00b9\3\2\2\2\2\u00bb\3\2\2\2\2\u00bd\3\2\2\2\2\u00bf"+
		"\3\2\2\2\2\u00c5\3\2\2\2\3\u00c7\3\2\2\2\5\u00c9\3\2\2\2\7\u00cb\3\2\2"+
		"\2\t\u00cd\3\2\2\2\13\u00cf\3\2\2\2\r\u00d1\3\2\2\2\17\u00d3\3\2\2\2\21"+
		"\u00d5\3\2\2\2\23\u00d7\3\2\2\2\25\u00d9\3\2\2\2\27\u00db\3\2\2\2\31\u00dd"+
		"\3\2\2\2\33\u00df\3\2\2\2\35\u00e1\3\2\2\2\37\u00e3\3\2\2\2!\u00e6\3\2"+
		"\2\2#\u00e9\3\2\2\2%\u00eb\3\2\2\2\'\u00ed\3\2\2\2)\u00ef\3\2\2\2+\u00f1"+
		"\3\2\2\2-\u00f4\3\2\2\2/\u00f7\3\2\2\2\61\u00f9\3\2\2\2\63\u00fc\3\2\2"+
		"\2\65\u00ff\3\2\2\2\67\u0102\3\2\2\29\u0105\3\2\2\2;\u0107\3\2\2\2=\u0109"+
		"\3\2\2\2?\u010c\3\2\2\2A\u010f\3\2\2\2C\u0111\3\2\2\2E\u0113\3\2\2\2G"+
		"\u0116\3\2\2\2I\u0119\3\2\2\2K\u011c\3\2\2\2M\u011f\3\2\2\2O\u0122\3\2"+
		"\2\2Q\u0125\3\2\2\2S\u0128\3\2\2\2U\u012b\3\2\2\2W\u012e\3\2\2\2Y\u0132"+
		"\3\2\2\2[\u0136\3\2\2\2]\u0138\3\2\2\2_\u013a\3\2\2\2a\u013d\3\2\2\2c"+
		"\u0141\3\2\2\2e\u0144\3\2\2\2g\u0147\3\2\2\2i\u014a\3\2\2\2k\u014e\3\2"+
		"\2\2m\u0151\3\2\2\2o\u0154\3\2\2\2q\u0157\3\2\2\2s\u015c\3\2\2\2u\u0160"+
		"\3\2\2\2w\u0168\3\2\2\2y\u016b\3\2\2\2{\u0171\3\2\2\2}\u0178\3\2\2\2\177"+
		"\u017e\3\2\2\2\u0081\u0182\3\2\2\2\u0083\u0188\3\2\2\2\u0085\u0190\3\2"+
		"\2\2\u0087\u0194\3\2\2\2\u0089\u019b\3\2\2\2\u008b\u01a3\3\2\2\2\u008d"+
		"\u01ad\3\2\2\2\u008f\u01b5\3\2\2\2\u0091\u01be\3\2\2\2\u0093\u01c5\3\2"+
		"\2\2\u0095\u01ca\3\2\2\2\u0097\u01ce\3\2\2\2\u0099\u01d2\3\2\2\2\u009b"+
		"\u01d9\3\2\2\2\u009d\u01e1\3\2\2\2\u009f\u01eb\3\2\2\2\u00a1\u01f2\3\2"+
		"\2\2\u00a3\u01fb\3\2\2\2\u00a5\u0203\3\2\2\2\u00a7\u020c\3\2\2\2\u00a9"+
		"\u0210\3\2\2\2\u00ab\u0216\3\2\2\2\u00ad\u021e\3\2\2\2\u00af\u0224\3\2"+
		"\2\2\u00b1\u0230\3\2\2\2\u00b3\u0236\3\2\2\2\u00b5\u0244\3\2\2\2\u00b7"+
		"\u0248\3\2\2\2\u00b9\u024a\3\2\2\2\u00bb\u0257\3\2\2\2\u00bd\u0262\3\2"+
		"\2\2\u00bf\u026f\3\2\2\2\u00c1\u0272\3\2\2\2\u00c3\u0276\3\2\2\2\u00c5"+
		"\u0278\3\2\2\2\u00c7\u00c8\7B\2\2\u00c8\4\3\2\2\2\u00c9\u00ca\7=\2\2\u00ca"+
		"\6\3\2\2\2\u00cb\u00cc\7.\2\2\u00cc\b\3\2\2\2\u00cd\u00ce\7*\2\2\u00ce"+
		"\n\3\2\2\2\u00cf\u00d0\7+\2\2\u00d0\f\3\2\2\2\u00d1\u00d2\7]\2\2\u00d2"+
		"\16\3\2\2\2\u00d3\u00d4\7_\2\2\u00d4\20\3\2\2\2\u00d5\u00d6\7}\2\2\u00d6"+
		"\22\3\2\2\2\u00d7\u00d8\7\177\2\2\u00d8\24\3\2\2\2\u00d9\u00da\7-\2\2"+
		"\u00da\26\3\2\2\2\u00db\u00dc\7/\2\2\u00dc\30\3\2\2\2\u00dd\u00de\7,\2"+
		"\2\u00de\32\3\2\2\2\u00df\u00e0\7\61\2\2\u00e0\34\3\2\2\2\u00e1\u00e2"+
		"\7\'\2\2\u00e2\36\3\2\2\2\u00e3\u00e4\7-\2\2\u00e4\u00e5\7-\2\2\u00e5"+
		" \3\2\2\2\u00e6\u00e7\7/\2\2\u00e7\u00e8\7/\2\2\u00e8\"\3\2\2\2\u00e9"+
		"\u00ea\7(\2\2\u00ea$\3\2\2\2\u00eb\u00ec\7~\2\2\u00ec&\3\2\2\2\u00ed\u00ee"+
		"\7`\2\2\u00ee(\3\2\2\2\u00ef\u00f0\7\u0080\2\2\u00f0*\3\2\2\2\u00f1\u00f2"+
		"\7(\2\2\u00f2\u00f3\7(\2\2\u00f3,\3\2\2\2\u00f4\u00f5\7~\2\2\u00f5\u00f6"+
		"\7~\2\2\u00f6.\3\2\2\2\u00f7\u00f8\7#\2\2\u00f8\60\3\2\2\2\u00f9\u00fa"+
		"\7>\2\2\u00fa\u00fb\7>\2\2\u00fb\62\3\2\2\2\u00fc\u00fd\7@\2\2\u00fd\u00fe"+
		"\7@\2\2\u00fe\64\3\2\2\2\u00ff\u0100\7?\2\2\u0100\u0101\7?\2\2\u0101\66"+
		"\3\2\2\2\u0102\u0103\7#\2\2\u0103\u0104\7?\2\2\u01048\3\2\2\2\u0105\u0106"+
		"\7@\2\2\u0106:\3\2\2\2\u0107\u0108\7>\2\2\u0108<\3\2\2\2\u0109\u010a\7"+
		"@\2\2\u010a\u010b\7?\2\2\u010b>\3\2\2\2\u010c\u010d\7>\2\2\u010d\u010e"+
		"\7?\2\2\u010e@\3\2\2\2\u010f\u0110\7\60\2\2\u0110B\3\2\2\2\u0111\u0112"+
		"\7?\2\2\u0112D\3\2\2\2\u0113\u0114\7-\2\2\u0114\u0115\7?\2\2\u0115F\3"+
		"\2\2\2\u0116\u0117\7/\2\2\u0117\u0118\7?\2\2\u0118H\3\2\2\2\u0119\u011a"+
		"\7,\2\2\u011a\u011b\7?\2\2\u011bJ\3\2\2\2\u011c\u011d\7\61\2\2\u011d\u011e"+
		"\7?\2\2\u011eL\3\2\2\2\u011f\u0120\7\'\2\2\u0120\u0121\7?\2\2\u0121N\3"+
		"\2\2\2\u0122\u0123\7\60\2\2\u0123\u0124\7?\2\2\u0124P\3\2\2\2\u0125\u0126"+
		"\7(\2\2\u0126\u0127\7?\2\2\u0127R\3\2\2\2\u0128\u0129\7~\2\2\u0129\u012a"+
		"\7?\2\2\u012aT\3\2\2\2\u012b\u012c\7`\2\2\u012c\u012d\7?\2\2\u012dV\3"+
		"\2\2\2\u012e\u012f\7>\2\2\u012f\u0130\7>\2\2\u0130\u0131\7?\2\2\u0131"+
		"X\3\2\2\2\u0132\u0133\7@\2\2\u0133\u0134\7@\2\2\u0134\u0135\7?\2\2\u0135"+
		"Z\3\2\2\2\u0136\u0137\7A\2\2\u0137\\\3\2\2\2\u0138\u0139\7<\2\2\u0139"+
		"^\3\2\2\2\u013a\u013b\7\60\2\2\u013b\u013c\7\60\2\2\u013c`\3\2\2\2\u013d"+
		"\u013e\7\60\2\2\u013e\u013f\7\60\2\2\u013f\u0140\7`\2\2\u0140b\3\2\2\2"+
		"\u0141\u0142\7k\2\2\u0142\u0143\7u\2\2\u0143d\3\2\2\2\u0144\u0145\7c\2"+
		"\2\u0145\u0146\7u\2\2\u0146f\3\2\2\2\u0147\u0148\7k\2\2\u0148\u0149\7"+
		"p\2\2\u0149h\3\2\2\2\u014a\u014b\7q\2\2\u014b\u014c\7w\2\2\u014c\u014d"+
		"\7v\2\2\u014dj\3\2\2\2\u014e\u014f\7?\2\2\u014f\u0150\7@\2\2\u0150l\3"+
		"\2\2\2\u0151\u0152\7A\2\2\u0152\u0153\7A\2\2\u0153n\3\2\2\2\u0154\u0155"+
		"\7k\2\2\u0155\u0156\7h\2\2\u0156p\3\2\2\2\u0157\u0158\7g\2\2\u0158\u0159"+
		"\7n\2\2\u0159\u015a\7u\2\2\u015a\u015b\7g\2\2\u015br\3\2\2\2\u015c\u015d"+
		"\7h\2\2\u015d\u015e\7q\2\2\u015e\u015f\7t\2\2\u015ft\3\2\2\2\u0160\u0161"+
		"\7h\2\2\u0161\u0162\7q\2\2\u0162\u0163\7t\2\2\u0163\u0164\7g\2\2\u0164"+
		"\u0165\7c\2\2\u0165\u0166\7e\2\2\u0166\u0167\7j\2\2\u0167v\3\2\2\2\u0168"+
		"\u0169\7f\2\2\u0169\u016a\7q\2\2\u016ax\3\2\2\2\u016b\u016c\7y\2\2\u016c"+
		"\u016d\7j\2\2\u016d\u016e\7k\2\2\u016e\u016f\7n\2\2\u016f\u0170\7g\2\2"+
		"\u0170z\3\2\2\2\u0171\u0172\7t\2\2\u0172\u0173\7g\2\2\u0173\u0174\7v\2"+
		"\2\u0174\u0175\7w\2\2\u0175\u0176\7t\2\2\u0176\u0177\7p\2\2\u0177|\3\2"+
		"\2\2\u0178\u0179\7v\2\2\u0179\u017a\7j\2\2\u017a\u017b\7t\2\2\u017b\u017c"+
		"\7q\2\2\u017c\u017d\7y\2\2\u017d~\3\2\2\2\u017e\u017f\7v\2\2\u017f\u0180"+
		"\7t\2\2\u0180\u0181\7{\2\2\u0181\u0080\3\2\2\2\u0182\u0183\7e\2\2\u0183"+
		"\u0184\7c\2\2\u0184\u0185\7v\2\2\u0185\u0186\7e\2\2\u0186\u0187\7j\2\2"+
		"\u0187\u0082\3\2\2\2\u0188\u0189\7h\2\2\u0189\u018a\7k\2\2\u018a\u018b"+
		"\7p\2\2\u018b\u018c\7c\2\2\u018c\u018d\7n\2\2\u018d\u018e\7n\2\2\u018e"+
		"\u018f\7{\2\2\u018f\u0084\3\2\2\2\u0190\u0191\7p\2\2\u0191\u0192\7g\2"+
		"\2\u0192\u0193\7y\2\2\u0193\u0086\3\2\2\2\u0194\u0195\7v\2\2\u0195\u0196"+
		"\7{\2\2\u0196\u0197\7r\2\2\u0197\u0198\7g\2\2\u0198\u0199\7q\2\2\u0199"+
		"\u019a\7h\2\2\u019a\u0088\3\2\2\2\u019b\u019c\7e\2\2\u019c\u019d\7j\2"+
		"\2\u019d\u019e\7g\2\2\u019e\u019f\7e\2\2\u019f\u01a0\7m\2\2\u01a0\u01a1"+
		"\7g\2\2\u01a1\u01a2\7f\2\2\u01a2\u008a\3\2\2\2\u01a3\u01a4\7w\2\2\u01a4"+
		"\u01a5\7p\2\2\u01a5\u01a6\7e\2\2\u01a6\u01a7\7j\2\2\u01a7\u01a8\7g\2\2"+
		"\u01a8\u01a9\7e\2\2\u01a9\u01aa\7m\2\2\u01aa\u01ab\7g\2\2\u01ab\u01ac"+
		"\7f\2\2\u01ac\u008c\3\2\2\2\u01ad\u01ae\7f\2\2\u01ae\u01af\7g\2\2\u01af"+
		"\u01b0\7h\2\2\u01b0\u01b1\7c\2\2\u01b1\u01b2\7w\2\2\u01b2\u01b3\7n\2\2"+
		"\u01b3\u01b4\7v\2\2\u01b4\u008e\3\2\2\2\u01b5\u01b6\7f\2\2\u01b6\u01b7"+
		"\7g\2\2\u01b7\u01b8\7n\2\2\u01b8\u01b9\7g\2\2\u01b9\u01ba\7i\2\2\u01ba"+
		"\u01bb\7c\2\2\u01bb\u01bc\7v\2\2\u01bc\u01bd\7g\2\2\u01bd\u0090\3\2\2"+
		"\2\u01be\u01bf\7u\2\2\u01bf\u01c0\7k\2\2\u01c0\u01c1\7|\2\2\u01c1\u01c2"+
		"\7g\2\2\u01c2\u01c3\7q\2\2\u01c3\u01c4\7h\2\2\u01c4\u0092\3\2\2\2\u01c5"+
		"\u01c6\7y\2\2\u01c6\u01c7\7k\2\2\u01c7\u01c8\7v\2\2\u01c8\u01c9\7j\2\2"+
		"\u01c9\u0094\3\2\2\2\u01ca\u01cb\7i\2\2\u01cb\u01cc\7g\2\2\u01cc\u01cd"+
		"\7v\2\2\u01cd\u0096\3\2\2\2\u01ce\u01cf\7u\2\2\u01cf\u01d0\7g\2\2\u01d0"+
		"\u01d1\7v\2\2\u01d1\u0098\3\2\2\2\u01d2\u01d3\7r\2\2\u01d3\u01d4\7w\2"+
		"\2\u01d4\u01d5\7d\2\2\u01d5\u01d6\7n\2\2\u01d6\u01d7\7k\2\2\u01d7\u01d8"+
		"\7e\2\2\u01d8\u009a\3\2\2\2\u01d9\u01da\7r\2\2\u01da\u01db\7t\2\2\u01db"+
		"\u01dc\7k\2\2\u01dc\u01dd\7x\2\2\u01dd\u01de\7c\2\2\u01de\u01df\7v\2\2"+
		"\u01df\u01e0\7g\2\2\u01e0\u009c\3\2\2\2\u01e1\u01e2\7r\2\2\u01e2\u01e3"+
		"\7t\2\2\u01e3\u01e4\7q\2\2\u01e4\u01e5\7v\2\2\u01e5\u01e6\7g\2\2\u01e6"+
		"\u01e7\7e\2\2\u01e7\u01e8\7v\2\2\u01e8\u01e9\7g\2\2\u01e9\u01ea\7f\2\2"+
		"\u01ea\u009e\3\2\2\2\u01eb\u01ec\7u\2\2\u01ec\u01ed\7v\2\2\u01ed\u01ee"+
		"\7c\2\2\u01ee\u01ef\7v\2\2\u01ef\u01f0\7k\2\2\u01f0\u01f1\7e\2\2\u01f1"+
		"\u00a0\3\2\2\2\u01f2\u01f3\7c\2\2\u01f3\u01f4\7d\2\2\u01f4\u01f5\7u\2"+
		"\2\u01f5\u01f6\7v\2\2\u01f6\u01f7\7t\2\2\u01f7\u01f8\7c\2\2\u01f8\u01f9"+
		"\7e\2\2\u01f9\u01fa\7v\2\2\u01fa\u00a2\3\2\2\2\u01fb\u01fc\7x\2\2\u01fc"+
		"\u01fd\7k\2\2\u01fd\u01fe\7t\2\2\u01fe\u01ff\7v\2\2\u01ff\u0200\7w\2\2"+
		"\u0200\u0201\7c\2\2\u0201\u0202\7n\2\2\u0202\u00a4\3\2\2\2\u0203\u0204"+
		"\7q\2\2\u0204\u0205\7x\2\2\u0205\u0206\7g\2\2\u0206\u0207\7t\2\2\u0207"+
		"\u0208\7t\2\2\u0208\u0209\7k\2\2\u0209\u020a\7f\2\2\u020a\u020b\7g\2\2"+
		"\u020b\u00a6\3\2\2\2\u020c\u020d\7t\2\2\u020d\u020e\7g\2\2\u020e\u020f"+
		"\7h\2\2\u020f\u00a8\3\2\2\2\u0210\u0211\7e\2\2\u0211\u0212\7n\2\2\u0212"+
		"\u0213\7c\2\2\u0213\u0214\7u\2\2\u0214\u0215\7u\2\2\u0215\u00aa\3\2\2"+
		"\2\u0216\u0217\7u\2\2\u0217\u0218\7v\2\2\u0218\u0219\7t\2\2\u0219\u021a"+
		"\7w\2\2\u021a\u021b\7e\2\2\u021b\u021c\7v\2\2\u021c\u00ac\3\2\2\2\u021d"+
		"\u021f\t\2\2\2\u021e\u021d\3\2\2\2\u021f\u0220\3\2\2\2\u0220\u021e\3\2"+
		"\2\2\u0220\u0221\3\2\2\2\u0221\u0222\3\2\2\2\u0222\u0223\bW\2\2\u0223"+
		"\u00ae\3\2\2\2\u0224\u0225\7\61\2\2\u0225\u0226\7\61\2\2\u0226\u022a\3"+
		"\2\2\2\u0227\u0229\n\3\2\2\u0228\u0227\3\2\2\2\u0229\u022c\3\2\2\2\u022a"+
		"\u0228\3\2\2\2\u022a\u022b\3\2\2\2\u022b\u022d\3\2\2\2\u022c\u022a\3\2"+
		"\2\2\u022d\u022e\bX\2\2\u022e\u00b0\3\2\2\2\u022f\u0231\t\3\2\2\u0230"+
		"\u022f\3\2\2\2\u0231\u0232\3\2\2\2\u0232\u0230\3\2\2\2\u0232\u0233\3\2"+
		"\2\2\u0233\u0234\3\2\2\2\u0234\u0235\bY\2\2\u0235\u00b2\3\2\2\2\u0236"+
		"\u0237\7\61\2\2\u0237\u0238\7,\2\2\u0238\u023c\3\2\2\2\u0239\u023b\13"+
		"\2\2\2\u023a\u0239\3\2\2\2\u023b\u023e\3\2\2\2\u023c\u023d\3\2\2\2\u023c"+
		"\u023a\3\2\2\2\u023d\u023f\3\2\2\2\u023e\u023c\3\2\2\2\u023f\u0240\7,"+
		"\2\2\u0240\u0241\7\61\2\2\u0241\u0242\3\2\2\2\u0242\u0243\bZ\2\2\u0243"+
		"\u00b4\3\2\2\2\u0244\u0245\7^\2\2\u0245\u00b6\3\2\2\2\u0246\u0249\5\u00b5"+
		"[\2\u0247\u0249\7$\2\2\u0248\u0246\3\2\2\2\u0248\u0247\3\2\2\2\u0249\u00b8"+
		"\3\2\2\2\u024a\u0251\7$\2\2\u024b\u024c\5\u00b5[\2\u024c\u024d\5\u00b7"+
		"\\\2\u024d\u0250\3\2\2\2\u024e\u0250\n\4\2\2\u024f\u024b\3\2\2\2\u024f"+
		"\u024e\3\2\2\2\u0250\u0253\3\2\2\2\u0251\u024f\3\2\2\2\u0251\u0252\3\2"+
		"\2\2\u0252\u0254\3\2\2\2\u0253\u0251\3\2\2\2\u0254\u0255\7$\2\2\u0255"+
		"\u00ba\3\2\2\2\u0256\u0258\t\5\2\2\u0257\u0256\3\2\2\2\u0258\u0259\3\2"+
		"\2\2\u0259\u0257\3\2\2\2\u0259\u025a\3\2\2\2\u025a\u025b\3\2\2\2\u025b"+
		"\u025d\7\60\2\2\u025c\u025e\t\5\2\2\u025d\u025c\3\2\2\2\u025e\u025f\3"+
		"\2\2\2\u025f\u025d\3\2\2\2\u025f\u0260\3\2\2\2\u0260\u00bc\3\2\2\2\u0261"+
		"\u0263\t\5\2\2\u0262\u0261\3\2\2\2\u0263\u0264\3\2\2\2\u0264\u0262\3\2"+
		"\2\2\u0264\u0265\3\2\2\2\u0265\u00be\3\2\2\2\u0266\u0267\7v\2\2\u0267"+
		"\u0268\7t\2\2\u0268\u0269\7w\2\2\u0269\u0270\7g\2\2\u026a\u026b\7h\2\2"+
		"\u026b\u026c\7c\2\2\u026c\u026d\7n\2\2\u026d\u026e\7u\2\2\u026e\u0270"+
		"\7g\2\2\u026f\u0266\3\2\2\2\u026f\u026a\3\2\2\2\u0270\u00c0\3\2\2\2\u0271"+
		"\u0273\t\6\2\2\u0272\u0271\3\2\2\2\u0273\u00c2\3\2\2\2\u0274\u0277\5\u00c1"+
		"a\2\u0275\u0277\t\5\2\2\u0276\u0274\3\2\2\2\u0276\u0275\3\2\2\2\u0277"+
		"\u00c4\3\2\2\2\u0278\u027c\5\u00c1a\2\u0279\u027b\5\u00c3b\2\u027a\u0279"+
		"\3\2\2\2\u027b\u027e\3\2\2\2\u027c\u027a\3\2\2\2\u027c\u027d\3\2\2\2\u027d"+
		"\u00c6\3\2\2\2\u027e\u027c\3\2\2\2\21\2\u0220\u022a\u0232\u023c\u0248"+
		"\u024f\u0251\u0259\u025f\u0264\u026f\u0272\u0276\u027c\3\b\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}