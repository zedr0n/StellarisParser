using Antlr4.Runtime.Tree;

namespace StellarisParser.Core
{
    public abstract class SpecVisitor<T> : StellarisVisitor<T>
    {
        public abstract string SpecId { get; }
        
        public override T VisitChildren(IRuleNode node)
        {
            var result = DefaultResult;
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                result = AggregateResult(result, nextResult);
                if (result != null && !result.Equals(default(T)))
                    break;
            }
            return result;
        }

        public override T VisitKeyval(stellarisParser.KeyvalContext context)
        {
            if (context.key().id().GetText() != SpecId)
                return default;

            return GetValue(context.val().id());
        }

        public abstract T GetValue(stellarisParser.IdContext context);
    }
}