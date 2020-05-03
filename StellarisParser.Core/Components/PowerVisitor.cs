namespace StellarisParser.Core.Components
{
    public class PowerVisitor : SpecVisitorDouble
    {
        public override string SpecId => Specs.POWER_ID;

        public PowerVisitor(Variables variables) 
            : base(variables)
        {
        }
    }
}