namespace StellarisParser.Core.Components.Shields
{
    public class ShieldMultiplierVisitor : SpecVisitorDouble
    {
        public ShieldMultiplierVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.SHIELD_MULT_ID;
    }
}