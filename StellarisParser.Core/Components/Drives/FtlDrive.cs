namespace StellarisParser.Core.Components.Drives
{
    public class FtlDrive : Component
    {
        public override Specs.ComponentType ComponentType => Specs.ComponentType.FTL_DRIVE;
        public double WindupMultiplier { get; set; }
        public double JumpDriveRangeMultiplier { get; set; }
        public bool JumpDrive { get; set; }
    }
}