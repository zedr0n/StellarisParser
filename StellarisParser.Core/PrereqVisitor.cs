using Antlr4.Runtime.Tree;
using StellarisParser.Core.Techs;

namespace StellarisParser.Core
{
    public class PrereqVisitor : StellarisVisitor<TechsList>
    {
        private readonly TechsList _techsList;

        public PrereqVisitor(TechsList techsList)
        {
            _techsList = techsList;
        }
        
        public override TechsList VisitChildren(IRuleNode node)
        {
            var result = DefaultResult;
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                result = AggregateResult(result, nextResult);
                if (result != null && !result.Equals(default(TechsList)))
                    break;
            }
            return result;
        }

        public override TechsList VisitKeyval(stellarisParser.KeyvalContext context)
        {
            if (context.key().id().GetText() != Specs.PREREQ_ID)
                return null;

            var ids = context.val().group().id();
            var techs = new TechsList();
            foreach (var id in ids)
            {
                var techId = id.GetText().Replace('"'.ToString(), string.Empty);
                var tech = _techsList[techId];
                if (tech != null)
                    techs.Add(tech);
            }

            return techs;
        }
    }
}