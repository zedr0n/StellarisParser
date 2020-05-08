namespace StellarisParser.Core.Components.CombatComputers
{
    public class EngagementRangeMultiplierVisitor : SpecVisitorDouble
    {
        public EngagementRangeMultiplierVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.ENGAGEMENT_RANGE_MULT_ID;
    }
}