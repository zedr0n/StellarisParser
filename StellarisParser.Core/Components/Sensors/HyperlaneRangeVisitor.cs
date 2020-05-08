namespace StellarisParser.Core.Components.Sensors
{
    public class HyperlaneRangeVisitor : SpecVisitorDouble
    {
        public HyperlaneRangeVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.HYPERLANE_RANGE_ID;
    }
}