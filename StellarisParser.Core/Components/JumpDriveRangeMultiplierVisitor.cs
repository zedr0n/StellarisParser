namespace StellarisParser.Core.Components
{
    public class JumpDriveRangeMultiplierVisitor : SpecVisitorDouble
    {
        public JumpDriveRangeMultiplierVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.JUMPDRIVE_RANGE_ID;
    }
}