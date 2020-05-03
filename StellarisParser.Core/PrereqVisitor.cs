using Antlr4.Runtime.Tree;

namespace StellarisParser.Core
{
    public class PrereqVisitor : StellarisVisitor<Techs>
    {
        private readonly Techs _techs;

        public PrereqVisitor(Techs techs)
        {
            _techs = techs;
        }
        
        public override Techs VisitChildren(IRuleNode node)
        {
            var result = DefaultResult;
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                result = AggregateResult(result, nextResult);
                if (result != null && !result.Equals(default(Techs)))
                    break;
            }
            return result;
        }

        public override Techs VisitKeyval(stellarisParser.KeyvalContext context)
        {
            if (context.key().id().GetText() != Specs.PREREQ_ID)
                return null;

            var ids = context.val().group().id();
            var techs = new Techs();
            foreach (var id in ids)
            {
                var techId = id.GetText().Replace('"'.ToString(), string.Empty);
                var tech = _techs[techId];
                if (tech != null)
                    techs.Add(tech);
            }

            return techs;
        }
    }
}