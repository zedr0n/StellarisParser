namespace StellarisParser.Core.Techs
{
    public class AreaVisitor : SpecVisitor<string>
    {
        public override string SpecId => Specs.AREA_ID;

        public override string GetValue(stellarisParser.IdContext context)
        {
            return context.GetText();
        }
    }
}