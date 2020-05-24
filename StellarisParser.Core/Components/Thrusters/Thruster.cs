using System.Linq;
using StellarisParser.Core.Modifiers;

namespace StellarisParser.Core.Components.Thrusters
{
    public class Thruster : Component
    {
        public double SpeedMultiplier => Modifiers.OfType<BaseSpeedMultiplier>().SingleOrDefault()?.Value ?? default;
        public double Evasion => Modifiers.OfType<ShipEvasion>().SingleOrDefault()?.Value ?? default;

        public override Specs.ComponentType ComponentType => Specs.ComponentType.THRUSTER;
    }
}