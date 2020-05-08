namespace StellarisParser.Core.Components.CombatComputers
{
    public class WeaponRangeVisitor : SpecVisitorDouble
    {
        public WeaponRangeVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.WEAPON_RANGE_MULT_ID;
    }
}