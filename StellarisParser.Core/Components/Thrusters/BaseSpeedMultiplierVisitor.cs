namespace StellarisParser.Core.Components.Thrusters
{
    public class BaseSpeedMultiplierVisitor : SpecVisitorDouble
    {
        public BaseSpeedMultiplierVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.BASE_SPEED_MULT_ID;
    }
}