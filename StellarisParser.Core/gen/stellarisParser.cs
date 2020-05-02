//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from G:/dev/StellarisParser/StellarisParser.Core/grammar\stellaris.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public partial class stellarisParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, IDENTIFIER=9, 
		INTEGER=10, STRING=11, COMMENT=12, SPACE=13, NL=14;
	public const int
		RULE_content = 0, RULE_expr = 1, RULE_keyval = 2, RULE_key = 3, RULE_val = 4, 
		RULE_attrib = 5, RULE_accessor = 6, RULE_group = 7, RULE_id = 8;
	public static readonly string[] ruleNames = {
		"content", "expr", "keyval", "key", "val", "attrib", "accessor", "group", 
		"id"
	};

	private static readonly string[] _LiteralNames = {
		null, "'='", "'>'", "'<'", "'.'", "'@'", "':'", "'{'", "'}'"
	};
	private static readonly string[] _SymbolicNames = {
		null, null, null, null, null, null, null, null, null, "IDENTIFIER", "INTEGER", 
		"STRING", "COMMENT", "SPACE", "NL"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "stellaris.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static stellarisParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public stellarisParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public stellarisParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class ContentContext : ParserRuleContext {
		public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		public ContentContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_content; } }
		public override void EnterRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.EnterContent(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.ExitContent(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstellarisVisitor<TResult> typedVisitor = visitor as IstellarisVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitContent(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ContentContext content() {
		ContentContext _localctx = new ContentContext(Context, State);
		EnterRule(_localctx, 0, RULE_content);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 19;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 18; expr();
				}
				}
				State = 21;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << IDENTIFIER) | (1L << INTEGER) | (1L << STRING))) != 0) );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ExprContext : ParserRuleContext {
		public KeyvalContext[] keyval() {
			return GetRuleContexts<KeyvalContext>();
		}
		public KeyvalContext keyval(int i) {
			return GetRuleContext<KeyvalContext>(i);
		}
		public ExprContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_expr; } }
		public override void EnterRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.EnterExpr(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.ExitExpr(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstellarisVisitor<TResult> typedVisitor = visitor as IstellarisVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExpr(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ExprContext expr() {
		ExprContext _localctx = new ExprContext(Context, State);
		EnterRule(_localctx, 2, RULE_expr);
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 24;
			ErrorHandler.Sync(this);
			_alt = 1;
			do {
				switch (_alt) {
				case 1:
					{
					{
					State = 23; keyval();
					}
					}
					break;
				default:
					throw new NoViableAltException(this);
				}
				State = 26;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,1,Context);
			} while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class KeyvalContext : ParserRuleContext {
		public KeyContext key() {
			return GetRuleContext<KeyContext>(0);
		}
		public ValContext val() {
			return GetRuleContext<ValContext>(0);
		}
		public KeyvalContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_keyval; } }
		public override void EnterRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.EnterKeyval(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.ExitKeyval(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstellarisVisitor<TResult> typedVisitor = visitor as IstellarisVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitKeyval(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public KeyvalContext keyval() {
		KeyvalContext _localctx = new KeyvalContext(Context, State);
		EnterRule(_localctx, 4, RULE_keyval);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 28; key();
			State = 30;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			do {
				{
				{
				State = 29;
				_la = TokenStream.LA(1);
				if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2))) != 0)) ) {
				ErrorHandler.RecoverInline(this);
				}
				else {
					ErrorHandler.ReportMatch(this);
				    Consume();
				}
				}
				}
				State = 32;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			} while ( (((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__1) | (1L << T__2))) != 0) );
			State = 34; val();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class KeyContext : ParserRuleContext {
		public IdContext id() {
			return GetRuleContext<IdContext>(0);
		}
		public AttribContext attrib() {
			return GetRuleContext<AttribContext>(0);
		}
		public KeyContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_key; } }
		public override void EnterRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.EnterKey(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.ExitKey(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstellarisVisitor<TResult> typedVisitor = visitor as IstellarisVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitKey(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public KeyContext key() {
		KeyContext _localctx = new KeyContext(Context, State);
		EnterRule(_localctx, 6, RULE_key);
		try {
			State = 38;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case IDENTIFIER:
			case INTEGER:
			case STRING:
				EnterOuterAlt(_localctx, 1);
				{
				State = 36; id();
				}
				break;
			case T__3:
			case T__4:
			case T__5:
				EnterOuterAlt(_localctx, 2);
				{
				State = 37; attrib();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ValContext : ParserRuleContext {
		public IdContext id() {
			return GetRuleContext<IdContext>(0);
		}
		public AttribContext attrib() {
			return GetRuleContext<AttribContext>(0);
		}
		public GroupContext group() {
			return GetRuleContext<GroupContext>(0);
		}
		public ValContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_val; } }
		public override void EnterRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.EnterVal(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.ExitVal(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstellarisVisitor<TResult> typedVisitor = visitor as IstellarisVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitVal(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ValContext val() {
		ValContext _localctx = new ValContext(Context, State);
		EnterRule(_localctx, 8, RULE_val);
		try {
			State = 43;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case IDENTIFIER:
			case INTEGER:
			case STRING:
				EnterOuterAlt(_localctx, 1);
				{
				State = 40; id();
				}
				break;
			case T__3:
			case T__4:
			case T__5:
				EnterOuterAlt(_localctx, 2);
				{
				State = 41; attrib();
				}
				break;
			case T__6:
				EnterOuterAlt(_localctx, 3);
				{
				State = 42; group();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AttribContext : ParserRuleContext {
		public AccessorContext accessor() {
			return GetRuleContext<AccessorContext>(0);
		}
		public AttribContext attrib() {
			return GetRuleContext<AttribContext>(0);
		}
		public IdContext id() {
			return GetRuleContext<IdContext>(0);
		}
		public AttribContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_attrib; } }
		public override void EnterRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.EnterAttrib(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.ExitAttrib(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstellarisVisitor<TResult> typedVisitor = visitor as IstellarisVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAttrib(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AttribContext attrib() {
		AttribContext _localctx = new AttribContext(Context, State);
		EnterRule(_localctx, 10, RULE_attrib);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 45; accessor();
			State = 48;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case T__3:
			case T__4:
			case T__5:
				{
				State = 46; attrib();
				}
				break;
			case IDENTIFIER:
			case INTEGER:
			case STRING:
				{
				State = 47; id();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AccessorContext : ParserRuleContext {
		public AccessorContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_accessor; } }
		public override void EnterRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.EnterAccessor(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.ExitAccessor(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstellarisVisitor<TResult> typedVisitor = visitor as IstellarisVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAccessor(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AccessorContext accessor() {
		AccessorContext _localctx = new AccessorContext(Context, State);
		EnterRule(_localctx, 12, RULE_accessor);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 50;
			_la = TokenStream.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__3) | (1L << T__4) | (1L << T__5))) != 0)) ) {
			ErrorHandler.RecoverInline(this);
			}
			else {
				ErrorHandler.ReportMatch(this);
			    Consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class GroupContext : ParserRuleContext {
		public IdContext id() {
			return GetRuleContext<IdContext>(0);
		}
		public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		public GroupContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_group; } }
		public override void EnterRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.EnterGroup(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.ExitGroup(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstellarisVisitor<TResult> typedVisitor = visitor as IstellarisVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitGroup(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public GroupContext group() {
		GroupContext _localctx = new GroupContext(Context, State);
		EnterRule(_localctx, 14, RULE_group);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 52; Match(T__6);
			State = 60;
			ErrorHandler.Sync(this);
			switch ( Interpreter.AdaptivePredict(TokenStream,7,Context) ) {
			case 1:
				{
				State = 56;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__3) | (1L << T__4) | (1L << T__5) | (1L << IDENTIFIER) | (1L << INTEGER) | (1L << STRING))) != 0)) {
					{
					{
					State = 53; expr();
					}
					}
					State = 58;
					ErrorHandler.Sync(this);
					_la = TokenStream.LA(1);
				}
				}
				break;
			case 2:
				{
				State = 59; id();
				}
				break;
			}
			State = 62; Match(T__7);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class IdContext : ParserRuleContext {
		public ITerminalNode IDENTIFIER() { return GetToken(stellarisParser.IDENTIFIER, 0); }
		public ITerminalNode STRING() { return GetToken(stellarisParser.STRING, 0); }
		public ITerminalNode INTEGER() { return GetToken(stellarisParser.INTEGER, 0); }
		public IdContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_id; } }
		public override void EnterRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.EnterId(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			IstellarisListener typedListener = listener as IstellarisListener;
			if (typedListener != null) typedListener.ExitId(this);
		}
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IstellarisVisitor<TResult> typedVisitor = visitor as IstellarisVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitId(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public IdContext id() {
		IdContext _localctx = new IdContext(Context, State);
		EnterRule(_localctx, 16, RULE_id);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 64;
			_la = TokenStream.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << IDENTIFIER) | (1L << INTEGER) | (1L << STRING))) != 0)) ) {
			ErrorHandler.RecoverInline(this);
			}
			else {
				ErrorHandler.ReportMatch(this);
			    Consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x3', '\x10', '\x45', '\x4', '\x2', '\t', '\x2', '\x4', '\x3', 
		'\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', '\x5', '\x4', 
		'\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', '\t', '\b', 
		'\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', '\x3', '\x2', '\x6', 
		'\x2', '\x16', '\n', '\x2', '\r', '\x2', '\xE', '\x2', '\x17', '\x3', 
		'\x3', '\x6', '\x3', '\x1B', '\n', '\x3', '\r', '\x3', '\xE', '\x3', '\x1C', 
		'\x3', '\x4', '\x3', '\x4', '\x6', '\x4', '!', '\n', '\x4', '\r', '\x4', 
		'\xE', '\x4', '\"', '\x3', '\x4', '\x3', '\x4', '\x3', '\x5', '\x3', '\x5', 
		'\x5', '\x5', ')', '\n', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', 
		'\x5', '\x6', '.', '\n', '\x6', '\x3', '\a', '\x3', '\a', '\x3', '\a', 
		'\x5', '\a', '\x33', '\n', '\a', '\x3', '\b', '\x3', '\b', '\x3', '\t', 
		'\x3', '\t', '\a', '\t', '\x39', '\n', '\t', '\f', '\t', '\xE', '\t', 
		'<', '\v', '\t', '\x3', '\t', '\x5', '\t', '?', '\n', '\t', '\x3', '\t', 
		'\x3', '\t', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x2', '\x2', '\v', 
		'\x2', '\x4', '\x6', '\b', '\n', '\f', '\xE', '\x10', '\x12', '\x2', '\x5', 
		'\x3', '\x2', '\x3', '\x5', '\x3', '\x2', '\x6', '\b', '\x3', '\x2', '\v', 
		'\r', '\x2', '\x44', '\x2', '\x15', '\x3', '\x2', '\x2', '\x2', '\x4', 
		'\x1A', '\x3', '\x2', '\x2', '\x2', '\x6', '\x1E', '\x3', '\x2', '\x2', 
		'\x2', '\b', '(', '\x3', '\x2', '\x2', '\x2', '\n', '-', '\x3', '\x2', 
		'\x2', '\x2', '\f', '/', '\x3', '\x2', '\x2', '\x2', '\xE', '\x34', '\x3', 
		'\x2', '\x2', '\x2', '\x10', '\x36', '\x3', '\x2', '\x2', '\x2', '\x12', 
		'\x42', '\x3', '\x2', '\x2', '\x2', '\x14', '\x16', '\x5', '\x4', '\x3', 
		'\x2', '\x15', '\x14', '\x3', '\x2', '\x2', '\x2', '\x16', '\x17', '\x3', 
		'\x2', '\x2', '\x2', '\x17', '\x15', '\x3', '\x2', '\x2', '\x2', '\x17', 
		'\x18', '\x3', '\x2', '\x2', '\x2', '\x18', '\x3', '\x3', '\x2', '\x2', 
		'\x2', '\x19', '\x1B', '\x5', '\x6', '\x4', '\x2', '\x1A', '\x19', '\x3', 
		'\x2', '\x2', '\x2', '\x1B', '\x1C', '\x3', '\x2', '\x2', '\x2', '\x1C', 
		'\x1A', '\x3', '\x2', '\x2', '\x2', '\x1C', '\x1D', '\x3', '\x2', '\x2', 
		'\x2', '\x1D', '\x5', '\x3', '\x2', '\x2', '\x2', '\x1E', ' ', '\x5', 
		'\b', '\x5', '\x2', '\x1F', '!', '\t', '\x2', '\x2', '\x2', ' ', '\x1F', 
		'\x3', '\x2', '\x2', '\x2', '!', '\"', '\x3', '\x2', '\x2', '\x2', '\"', 
		' ', '\x3', '\x2', '\x2', '\x2', '\"', '#', '\x3', '\x2', '\x2', '\x2', 
		'#', '$', '\x3', '\x2', '\x2', '\x2', '$', '%', '\x5', '\n', '\x6', '\x2', 
		'%', '\a', '\x3', '\x2', '\x2', '\x2', '&', ')', '\x5', '\x12', '\n', 
		'\x2', '\'', ')', '\x5', '\f', '\a', '\x2', '(', '&', '\x3', '\x2', '\x2', 
		'\x2', '(', '\'', '\x3', '\x2', '\x2', '\x2', ')', '\t', '\x3', '\x2', 
		'\x2', '\x2', '*', '.', '\x5', '\x12', '\n', '\x2', '+', '.', '\x5', '\f', 
		'\a', '\x2', ',', '.', '\x5', '\x10', '\t', '\x2', '-', '*', '\x3', '\x2', 
		'\x2', '\x2', '-', '+', '\x3', '\x2', '\x2', '\x2', '-', ',', '\x3', '\x2', 
		'\x2', '\x2', '.', '\v', '\x3', '\x2', '\x2', '\x2', '/', '\x32', '\x5', 
		'\xE', '\b', '\x2', '\x30', '\x33', '\x5', '\f', '\a', '\x2', '\x31', 
		'\x33', '\x5', '\x12', '\n', '\x2', '\x32', '\x30', '\x3', '\x2', '\x2', 
		'\x2', '\x32', '\x31', '\x3', '\x2', '\x2', '\x2', '\x33', '\r', '\x3', 
		'\x2', '\x2', '\x2', '\x34', '\x35', '\t', '\x3', '\x2', '\x2', '\x35', 
		'\xF', '\x3', '\x2', '\x2', '\x2', '\x36', '>', '\a', '\t', '\x2', '\x2', 
		'\x37', '\x39', '\x5', '\x4', '\x3', '\x2', '\x38', '\x37', '\x3', '\x2', 
		'\x2', '\x2', '\x39', '<', '\x3', '\x2', '\x2', '\x2', ':', '\x38', '\x3', 
		'\x2', '\x2', '\x2', ':', ';', '\x3', '\x2', '\x2', '\x2', ';', '?', '\x3', 
		'\x2', '\x2', '\x2', '<', ':', '\x3', '\x2', '\x2', '\x2', '=', '?', '\x5', 
		'\x12', '\n', '\x2', '>', ':', '\x3', '\x2', '\x2', '\x2', '>', '=', '\x3', 
		'\x2', '\x2', '\x2', '?', '@', '\x3', '\x2', '\x2', '\x2', '@', '\x41', 
		'\a', '\n', '\x2', '\x2', '\x41', '\x11', '\x3', '\x2', '\x2', '\x2', 
		'\x42', '\x43', '\t', '\x4', '\x2', '\x2', '\x43', '\x13', '\x3', '\x2', 
		'\x2', '\x2', '\n', '\x17', '\x1C', '\"', '(', '-', '\x32', ':', '>',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
