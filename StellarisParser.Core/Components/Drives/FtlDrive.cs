using System.Linq;
using StellarisParser.Core.Modifiers;

namespace StellarisParser.Core.Components.Drives
{
    public class FtlDrive : Component
    {
        public override Specs.ComponentType ComponentType => Specs.ComponentType.FTL_DRIVE;
        public double WindupMultiplier => Modifiers.OfType<ShipWindup>().SingleOrDefault()?.Value ?? default;

        public double JumpDriveRangeMultiplier =>
            Modifiers.OfType<JumpDriveRangeMultiplier>().SingleOrDefault()?.Value ?? default;
        public bool JumpDrive { get; set; }
    }
}