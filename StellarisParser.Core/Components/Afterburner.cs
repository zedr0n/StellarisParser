namespace StellarisParser.Core.Components
{
    public class Afterburner : Component
    {
        public double Speed { get; set; }
        public double Evasion { get; set; }
        public override string Type => Specs.ComponentType.AFTERBURNER.ToString();
    }
}