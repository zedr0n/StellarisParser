namespace StellarisParser.Core.Components.Afterburners
{
    public class AfterburnerVisitor : ComponentVisitor, IStellarisVisitor<Afterburner>
    {
        private readonly ModifierVisitor<EvasionMultiplierVisitor> _evasionVisitor;
        private readonly ModifierVisitor<SpeedMultiplierVisitor> _speedVisitor;

        
        public AfterburnerVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, 
            ComponentsList componentsList, ComponentSets componentSets, 
            ModifierVisitor<EvasionMultiplierVisitor> evasionVisitor, ModifierVisitor<SpeedMultiplierVisitor> speedVisitor) 
            : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList, componentSets)
        {
            _evasionVisitor = evasionVisitor;
            _speedVisitor = speedVisitor;
        }

        public override Component Create()
        {
            return new Afterburner();
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var afterburner = base.VisitKeyval(context) as Afterburner;
            if (afterburner == null)
                return null;

            afterburner.Speed = _speedVisitor.Visit(context.val());
            afterburner.Evasion = _evasionVisitor.Visit(context.val());

            return afterburner;
        }

        public Afterburner VisitContent(stellarisParser.ContentContext context)
        {
            return (Afterburner) base.VisitContent(context);
        }
    }
}