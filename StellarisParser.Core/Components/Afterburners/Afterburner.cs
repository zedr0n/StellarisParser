namespace StellarisParser.Core.Components.Afterburners
{
    public class Afterburner : Component
    {
        public double Speed { get; set; }
        public double Evasion { get; set; }
        public override Specs.ComponentType ComponentType => Specs.ComponentType.AFTERBURNER;
    }
}