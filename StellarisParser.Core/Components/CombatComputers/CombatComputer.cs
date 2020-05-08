namespace StellarisParser.Core.Components.CombatComputers
{
    public class CombatComputer : Component
    {
        public override Specs.ComponentType ComponentType => Specs.ComponentType.COMBAT_COMPUTER;
        
        public double EvasionMultiplier { get; set; }
        public double FireRateMultiplier { get; set; }
        public double Tracking { get; set; }
        public double Accuracy { get; set; }
        public double EngagementRangeMultiplier { get; set; }
        public double WeaponRange { get; set; }
    }
}