namespace StellarisParser.Core.Components.Shields
{
    public class Shield : Component
    {
        public override Specs.ComponentType ComponentType => Specs.ComponentType.SHIELD;
        
        public double ShieldAdd { get; set; }
        public double ShieldRegen { get; set; }
    }
}