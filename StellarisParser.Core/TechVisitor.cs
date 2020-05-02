using System.Collections;
using System.Collections.Generic;

namespace StellarisParser.Core
{
    public class TechVisitor : StellarisVisitor<Tech>
    {
        private readonly AreaVisitor _areaVisitor;
        private readonly TierVisitor _tierVisitor;

        public TechVisitor(AreaVisitor areaVisitor, TierVisitor tierVisitor)
        {
            _areaVisitor = areaVisitor;
            _tierVisitor = tierVisitor;
        }
        // private readonly IEnumerable<ISpecVisitor> _specs;

        public override Tech VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var lvl = Level(context);
            if (lvl > 1)
                return null;
            
            var id = context.key().id().GetText();
            var area = _areaVisitor.Visit(context.val());
            var tier = _tierVisitor.Visit(context.val());

            return new Tech()
            {
                Name = id,
                Area = area,
                Tier = tier
            };
        }
    }
}