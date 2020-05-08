namespace StellarisParser.Core.Components.Reactors
{
    public class Reactor : Component
    {
        public override Specs.ComponentType ComponentType => Specs.ComponentType.REACTOR;

        public string SizeRestriction { get; set; }
    }
}