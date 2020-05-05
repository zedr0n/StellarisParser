using static System.Double;

namespace StellarisParser.Core.Techs
{
    public class CostVisitor : SpecVisitorDouble 
    {
        public override string SpecId => Specs.COST_ID;

        public CostVisitor(Variables variables) : base(variables)
        {
        }
    }
}