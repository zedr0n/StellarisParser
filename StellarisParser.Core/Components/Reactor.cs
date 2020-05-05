namespace StellarisParser.Core.Components
{
    public class Reactor : Component
    {
        public override string Type => Specs.ComponentType.REACTOR.ToString();
        
        public string SizeRestriction { get; set; }
    }
}