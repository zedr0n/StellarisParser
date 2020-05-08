namespace StellarisParser.Core.Components.Armors
{
    public class Armor : Component
    {
        public override Specs.ComponentType ComponentType => Specs.ComponentType.ARMOR;
        
        public double ArmorAdd { get; set; }
        public double HullAdd { get; set; }
        public double ShieldRegen { get; set; }
        public double Shield { get; set; }
    }
}