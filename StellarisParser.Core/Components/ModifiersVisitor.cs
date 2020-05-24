using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using StellarisParser.Core.Modifiers;

namespace StellarisParser.Core.Components
{
    public class ModifiersVisitor : StellarisVisitor<List<Modifier>>
    {
        private readonly IEnumerable<ModifierVisitor> _singleModifierVisitors;

        public ModifiersVisitor(IEnumerable<ModifierVisitor> singleModifierVisitors)
        {
            _singleModifierVisitors = singleModifierVisitors;
        }

        public override List<Modifier> VisitChildren(IRuleNode node)
        {
            var result = new List<Modifier>();
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                if (nextResult != default)
                {
                    result.AddRange(nextResult);
                    break;
                }
            }
            return result;
        }

        public override List<Modifier> VisitKeyval(stellarisParser.KeyvalContext context)
        {
            if (context.key().id().GetText() != Specs.MODIFIER_ID && context.key().id().GetText() != Specs.SHIP_MODIFIER_ID)
                return null;

            var modifiers = new List<Modifier>();

            foreach (var visitor in _singleModifierVisitors)
            {
                var val = visitor.Visit(context.val());
                if (val != default && !double.IsNaN(val))
                {
                    var modifier = visitor.Modifier;
                    modifier.Value = val;
                    modifiers.Add(modifier);
                }
            }

            return modifiers;
        }
    }
}