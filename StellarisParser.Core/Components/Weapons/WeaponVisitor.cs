namespace StellarisParser.Core.Components.Weapons
{
    public class WeaponVisitor : ComponentVisitor<Weapon>
    {
        private readonly DamageVisitor _damageVisitor;
        
        public WeaponVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList, ComponentSets componentSets, ModifiersVisitor modifiersVisitor, DamageVisitor damageVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets, modifiersVisitor)
        {
            _damageVisitor = damageVisitor;
        }

        public override Weapon VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var weapon = base.VisitKeyval(context);
            if (weapon == null)
                return null;

            var damage = _damageVisitor.Visit(context.val()); 
            weapon.MinDamage = damage?.Min ?? double.NaN;
            weapon.MaxDamage = damage?.Max ?? double.NaN;

            return weapon;
        }
    }
}