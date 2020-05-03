using System;

namespace StellarisParser.Core
{
    public class TierVisitor : SpecVisitor<int>
    {
        public override string SpecId => Specs.TIER_ID;

        public override int GetValue(stellarisParser.IdContext context)
        {
            return int.Parse(context.GetText());
;        }
    }
}