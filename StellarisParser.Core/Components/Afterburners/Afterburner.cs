using System.Linq;
using StellarisParser.Core.Modifiers;

namespace StellarisParser.Core.Components.Afterburners
{
    public class Afterburner : Component
    {
        public double Speed => Modifiers.OfType<SpeedMultiplier>().SingleOrDefault()?.Value ?? default; 
        public double Evasion => Modifiers.OfType<ShipEvasionMultiplier>().SingleOrDefault()?.Value ?? default;
        public override Specs.ComponentType ComponentType => Specs.ComponentType.AFTERBURNER;
    }
}