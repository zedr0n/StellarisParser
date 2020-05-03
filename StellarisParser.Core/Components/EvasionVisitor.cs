namespace StellarisParser.Core.Components
{
    public class EvasionVisitor : SpecVisitorDouble
    {
        public override string SpecId => Specs.EVASION_ID;

        public EvasionVisitor(Variables variables) : base(variables)
        {
        }
    }
}