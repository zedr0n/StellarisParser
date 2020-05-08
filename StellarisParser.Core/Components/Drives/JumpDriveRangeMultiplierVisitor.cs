namespace StellarisParser.Core.Components.Drives
{
    public class JumpDriveRangeMultiplierVisitor : SpecVisitorDouble
    {
        public JumpDriveRangeMultiplierVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.JUMPDRIVE_RANGE_ID;
    }
}