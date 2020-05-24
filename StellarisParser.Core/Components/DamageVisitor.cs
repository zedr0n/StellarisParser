namespace StellarisParser.Core.Components
{
    public class DamageVisitor : MinMaxVisitor
    {
        
        public DamageVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.DAMAGE_ID;
    }
}