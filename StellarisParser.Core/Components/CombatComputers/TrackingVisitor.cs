namespace StellarisParser.Core.Components.CombatComputers
{
    public class TrackingVisitor : SpecVisitorDouble
    {
        public TrackingVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.TRACKING_ID;
    }
}