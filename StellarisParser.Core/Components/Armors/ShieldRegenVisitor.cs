namespace StellarisParser.Core.Components.Armors
{
    public class ShieldRegenVisitor : SpecVisitorDouble
    {
        public ShieldRegenVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.SHIELD_REGEN_ID;
    }
}