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

            if (context.val().id() != null)
                return GetValue(context.val().id());
            if (context.val().attrib() != null)
                return GetValue(context.val().attrib());
            return default;
        }

        public virtual T GetValue(stellarisParser.IdContext context) => default;
        public virtual T GetValue(stellarisParser.AttribContext context) => default;
    }
}