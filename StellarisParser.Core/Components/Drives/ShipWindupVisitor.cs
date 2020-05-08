namespace StellarisParser.Core.Components.Drives
{
    public class ShipWindupVisitor : SpecVisitorDouble
    {
        public ShipWindupVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.WINDUP_ID;
    }
}