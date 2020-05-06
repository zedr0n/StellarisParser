using Antlr4.Runtime.Tree;

namespace StellarisParser.Core.Components
{
    public class ModifierVisitor<T> : StellarisVisitor<double>
        where T : SpecVisitorDouble
    {
        private readonly T _specVisitor;

        public ModifierVisitor(T specVisitor)
        {
            _specVisitor = specVisitor;
        }

        public override double VisitChildren(IRuleNode node)
        {
            var result = DefaultResult;
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                result = AggregateResult(result, nextResult);
                if (result != default && !double.IsNaN(result))
                    break;
            }
            return result;
        }

        public override double VisitKeyval(stellarisParser.KeyvalContext context)
        {
            if (context.key().id().GetText() != Specs.MODIFIER_ID && context.key().id().GetText() != Specs.SHIP_MODIFIER_ID)
                return double.NaN;

            var evasion = _specVisitor.Visit(context.val());
            return evasion;
        }
    }
}