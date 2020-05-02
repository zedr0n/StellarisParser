namespace StellarisParser.Core
{
    public class AreaVisitor : SpecVisitor<string>
    {
        public override string SpecId => Specs.AreaId;

        public override string GetValue(stellarisParser.IdContext context)
        {
            return context.GetText();
        }
    }
}