namespace StellarisParser.Core.Components
{
    public class FtlDrive : Component
    {
        public override string Type => Specs.ComponentType.FTL_DRIVE.ToString();
        public double WindupMultiplier { get; set; }
        public double JumpDriveRangeMultiplier { get; set; }
        public bool JumpDrive { get; set; }
    }
}