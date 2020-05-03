namespace StellarisParser.Core.Components
{
    public class ThrusterVisitor : ComponentVisitor, IStellarisVisitor<Thruster>
    {
        private readonly ComponentSetVisitor _componentSetVisitor;
        private readonly ModifierVisitor<EvasionVisitor> _evasionVisitor;
        private readonly ModifierVisitor<SpeedVisitor> _speedVisitor;

        public ThrusterVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, Parser parser, ComponentSetVisitor componentSetVisitor, ModifierVisitor<EvasionVisitor> evasionVisitor, ModifierVisitor<SpeedVisitor> speedVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, parser)
        {
            _componentSetVisitor = componentSetVisitor;
            _evasionVisitor = evasionVisitor;
            _speedVisitor = speedVisitor;
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var component = base.VisitKeyval(context);
            if (component == null)
                return null;

            var componentsSet = _componentSetVisitor.Visit(context.val()).Replace('"'.ToString(),"");
            if (componentsSet != Specs.THRUSTER_SET)
                return null;
            
            var evasion = _evasionVisitor.Visit(context.val());
            var speed = _speedVisitor.Visit(context.val());
            
            return new Thruster
            {
                Key = component.Key,
                Evasion = evasion,
                Power = component.Power,
                Prerequisites = component.Prerequisites,
                SpeedMultipler = speed,
                Source = component.Source
            };
        }

        public new Thruster VisitContent(stellarisParser.ContentContext context)
        {
            return (Thruster) base.VisitContent(context);
        }

    }
}