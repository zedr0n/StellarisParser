namespace StellarisParser.Core.Components.CombatComputers
{
    public class FireRateMultiplierVisitor : SpecVisitorDouble
    {
        public FireRateMultiplierVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.FIRERATE_ID;
    }
}