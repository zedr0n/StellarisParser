using System.Linq;
using StellarisParser.Core.Modifiers;

namespace StellarisParser.Core.Components.Shields
{
    public class Shield : Component
    {
        public override Specs.ComponentType ComponentType => Specs.ComponentType.SHIELD;

        public double ShieldAdd => Modifiers.OfType<Modifiers.Shield>().SingleOrDefault()?.Value ?? default;

        public double ShieldRegen => Modifiers.OfType<ShieldRegen>().SingleOrDefault()?.Value ?? default;
        // for shield boosters
        public double ShieldMultiplier => Modifiers.OfType<ShieldMultiplier>().SingleOrDefault()?.Value ?? default;
    }
}