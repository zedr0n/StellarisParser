namespace StellarisParser.Core.Components
{
    public class ShipWindupVisitor : SpecVisitorDouble
    {
        public ShipWindupVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.WINDUP_ID;
    }
}