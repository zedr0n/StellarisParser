namespace StellarisParser.Core.Components.Reactors
{
    public class ReactorVisitor : ComponentVisitor<Reactor>, IStellarisVisitor<Reactor>
    {
        public ReactorVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList, ComponentSets componentSets, ModifiersVisitor modifiersVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets, modifiersVisitor)
        {
        }

        public new Reactor VisitContent(stellarisParser.ContentContext context)
        {
            return (Reactor) base.VisitContent(context);
        }
    }
}