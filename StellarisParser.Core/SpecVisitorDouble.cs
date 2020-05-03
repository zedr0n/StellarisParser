namespace StellarisParser.Core
{
    public abstract class SpecVisitorDouble : SpecVisitor<double>
    {
        private readonly Variables _variables;

        public SpecVisitorDouble(Variables variables)
        {
            _variables = variables;
        }

        public override double GetValue(stellarisParser.IdContext context)
        {
            return double.Parse(context.GetText());
        }

        public override double GetValue(stellarisParser.AttribContext context)
        {
            var id = context.id().GetText();
            if (!double.IsNaN(_variables.Get(id)))
                return _variables.Get(id);
            return double.NaN;
        }
    }
}