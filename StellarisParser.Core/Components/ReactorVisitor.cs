namespace StellarisParser.Core.Components
{
    public class ReactorVisitor : ComponentVisitor, IStellarisVisitor<Reactor>
    {
        public ReactorVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList)
        {
        }

        protected override string ComponentSet => Specs.REACTOR_SET;

        public override Component Create()
        {
            return new Reactor();
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var reactor = base.VisitKeyval(context) as Reactor;
            return reactor;
        }

        public new Reactor VisitContent(stellarisParser.ContentContext context)
        {
            return (Reactor) base.VisitContent(context);
        }
    }
}