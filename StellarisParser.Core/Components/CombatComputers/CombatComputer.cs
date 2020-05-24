using System.Linq;
using StellarisParser.Core.Modifiers;

namespace StellarisParser.Core.Components.CombatComputers
{
    public class CombatComputer : Component
    {
        public override Specs.ComponentType ComponentType => Specs.ComponentType.COMBAT_COMPUTER;

        public double EvasionMultiplier =>
            Modifiers.OfType<ShipEvasionMultiplier>().SingleOrDefault()?.Value ?? default;
        public double FireRateMultiplier =>
            Modifiers.OfType<FireRateMultiplier>().SingleOrDefault()?.Value ?? default;
        public double Tracking =>
            Modifiers.OfType<Tracking>().SingleOrDefault()?.Value ?? default;
        public double Accuracy =>
            Modifiers.OfType<Accuracy>().SingleOrDefault()?.Value ?? default;
        public double EngagementRangeMultiplier =>
            Modifiers.OfType<EngagementRangeMultiplier>().SingleOrDefault()?.Value ?? default;
        public double WeaponRange =>
            Modifiers.OfType<WeaponRangeMultiplier>().SingleOrDefault()?.Value ?? default;
    }
}