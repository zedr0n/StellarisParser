namespace StellarisParser.Core.Components
{
    public class SpeedVisitor : SpecVisitorDouble
    {
        public SpeedVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.SPEED_ID;
    }
}