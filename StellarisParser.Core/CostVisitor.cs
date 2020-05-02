using static System.Double;

namespace StellarisParser.Core
{
    public class CostVisitor : SpecVisitor<double> 
    {
        private readonly Variables _variables;

        public CostVisitor(Variables variables)
        {
            _variables = variables;
        }

        public override string SpecId => Specs.CostId;
        public override double GetValue(stellarisParser.IdContext context)
        {
            return Parse(context.GetText());
        }

        public override double GetValue(stellarisParser.AttribContext context)
        {
            var id = context.id().GetText();
            if (!IsNaN(_variables.Get(id)))
                return _variables.Get(id);
            return NaN;
        }
    }
}