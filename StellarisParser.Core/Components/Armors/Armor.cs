using System.Linq;

namespace StellarisParser.Core.Components.Armors
{
    public class Armor : Component
    {
        public override Specs.ComponentType ComponentType => Specs.ComponentType.ARMOR;

        public double ArmorAdd => Modifiers.OfType<Modifiers.Armor>().SingleOrDefault()?.Value ?? default;
        public double HullAdd => Modifiers.OfType<Modifiers.Hull>().SingleOrDefault()?.Value ?? default;
        public double ShieldRegen { get; set; }
        public double Shield { get; set; }
    }
}