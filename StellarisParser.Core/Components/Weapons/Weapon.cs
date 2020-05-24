namespace StellarisParser.Core.Components.Weapons
{
    public class Weapon : Component
    {
        public override Specs.ComponentType ComponentType => Specs.ComponentType.WEAPON;
        
        public double MinDamage { get; set; }
        public double MaxDamage { get; set; }
    }
}