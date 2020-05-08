namespace StellarisParser.Core.Components.Thrusters
{
    public class Thruster : Component
    {
        public double SpeedMultiplier { get; set; }
        public double Evasion { get; set; }

        public override Specs.ComponentType ComponentType => Specs.ComponentType.THRUSTER;
    }
}