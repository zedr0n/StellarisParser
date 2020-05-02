using System;

namespace StellarisParser.Core
{
    public class TierVisitor : SpecVisitor<int>
    {
        public override string SpecId => Specs.TierId;

        public override int GetValue(stellarisParser.IdContext context)
        {
            return int.Parse(context.GetText());
;        }
    }
}