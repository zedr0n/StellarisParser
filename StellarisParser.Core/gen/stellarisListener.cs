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

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="stellarisParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public interface IstellarisListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="stellarisParser.content"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterContent([NotNull] stellarisParser.ContentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="stellarisParser.content"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitContent([NotNull] stellarisParser.ContentContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="stellarisParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpr([NotNull] stellarisParser.ExprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="stellarisParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpr([NotNull] stellarisParser.ExprContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="stellarisParser.keyval"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterKeyval([NotNull] stellarisParser.KeyvalContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="stellarisParser.keyval"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitKeyval([NotNull] stellarisParser.KeyvalContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="stellarisParser.key"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterKey([NotNull] stellarisParser.KeyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="stellarisParser.key"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitKey([NotNull] stellarisParser.KeyContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="stellarisParser.val"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVal([NotNull] stellarisParser.ValContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="stellarisParser.val"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVal([NotNull] stellarisParser.ValContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="stellarisParser.attrib"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAttrib([NotNull] stellarisParser.AttribContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="stellarisParser.attrib"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAttrib([NotNull] stellarisParser.AttribContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="stellarisParser.accessor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAccessor([NotNull] stellarisParser.AccessorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="stellarisParser.accessor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAccessor([NotNull] stellarisParser.AccessorContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="stellarisParser.group"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterGroup([NotNull] stellarisParser.GroupContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="stellarisParser.group"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitGroup([NotNull] stellarisParser.GroupContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="stellarisParser.id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterId([NotNull] stellarisParser.IdContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="stellarisParser.id"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitId([NotNull] stellarisParser.IdContext context);
}