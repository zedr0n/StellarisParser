using System.Collections.Generic;
using Antlr4.Runtime.Tree;

namespace StellarisParser.Core.Techs
{
    public class TechsListVisitor : StellarisVisitor<TechsList>
    {
        private readonly Parser _parser;
        private readonly AreaVisitor _areaVisitor;
        private readonly TierVisitor _tierVisitor;
        private readonly CostVisitor _costVisitor;
        private readonly PrereqVisitor _prereqVisitor;
        private readonly Localisation.Localisation _localisation;

        public TechsListVisitor(Parser parser, AreaVisitor areaVisitor, TierVisitor tierVisitor, CostVisitor costVisitor, PrereqVisitor prereqVisitor, Localisation.Localisation localisation)
        {
            _areaVisitor = areaVisitor;
            _tierVisitor = tierVisitor;
            _costVisitor = costVisitor;
            _prereqVisitor = prereqVisitor;
            _localisation = localisation;
            _parser = parser;
        }
        
        public override TechsList VisitChildren(IRuleNode node)
        {
            var result = new TechsList();
            var childCount = node.ChildCount;
            for (var i = 0; i < childCount && ShouldVisitNextChild(node, result); ++i)
            {
                var nextResult = node.GetChild(i).Accept(this);
                if (nextResult != null)
                    result.Aggregate(nextResult);

            }
            return result;
        }

        public override TechsList VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var lvl = Level(context);
            if (lvl > 1)
                return null;

            if (context.key().id() == null)
                return null;
            
            var id = context.key().id().GetText();
            var area = _areaVisitor.Visit(context.val());
            var tier = _tierVisitor.Visit(context.val());
            var cost = _costVisitor.Visit(context.val());
            var prereqs = _prereqVisitor.Visit(context.val());
            var source = _parser.CurrentSource;
            
            var tech = new Tech
            {
                Key = id,
                Area = area,
                Tier = tier,
                Cost = (int) cost,
                Prerequisites = prereqs?.ToList() ?? new List<Tech>(),
                Source = source,
                Name = _localisation[id].Name,
                Description = _localisation[id].Description
            };

            var techs = new TechsList();
            techs.Add(tech);
            return techs;
        }
    }
}