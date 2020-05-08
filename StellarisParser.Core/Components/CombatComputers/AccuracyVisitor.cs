namespace StellarisParser.Core.Components.CombatComputers
{
    public class AccuracyVisitor : SpecVisitorDouble
    {
        public AccuracyVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.ACCURACY_ID;
    }
}