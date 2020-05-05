namespace StellarisParser.Core.Components
{
    public class Thruster : Component
    {
        public double SpeedMultipler { get; set; }
        public double Evasion { get; set; }

        public override string Type => Specs.ComponentType.THRUSTER.ToString();
    }
}