namespace StellarisParser.Core
{
    public abstract class SpecVisitorString : SpecVisitor<string>
    {
        public override string GetValue(stellarisParser.IdContext context)
        {
            return context.GetText().Replace('"'.ToString(),"");
        }
    }
}