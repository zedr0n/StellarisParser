namespace StellarisParser.Core.Components
{
    public class SpeedMultiplierVisitor : SpecVisitorDouble
    {
        public SpeedMultiplierVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.SPEED_MULT_ID;
    }
}