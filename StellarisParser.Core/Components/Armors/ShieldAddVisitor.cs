namespace StellarisParser.Core.Components.Armors
{
    public class ShieldAddVisitor : SpecVisitorDouble
    {
        public ShieldAddVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.SHIELD_ID;
    }
}