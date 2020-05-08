namespace StellarisParser.Core.Components.Afterburners
{
    public class EvasionMultiplierVisitor : SpecVisitorDouble
    {
        public EvasionMultiplierVisitor(Variables variables) : base(variables)
        {
        }

        public override string SpecId => Specs.EVASION_MULT_ID;
    }
}