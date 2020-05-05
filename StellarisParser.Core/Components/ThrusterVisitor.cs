namespace StellarisParser.Core.Components
{
    public class ThrusterVisitor : ComponentVisitor, IStellarisVisitor<Thruster>
    {
        private readonly ModifierVisitor<EvasionVisitor> _evasionVisitor;
        private readonly ModifierVisitor<SpeedVisitor> _speedVisitor;

        protected override string ComponentSet => Specs.THRUSTER_SET;
        public override Component Create() => new Thruster(); 

        public ThrusterVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor,
            ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser, ComponentsList componentsList, ModifierVisitor<EvasionVisitor> evasionVisitor,
            ModifierVisitor<SpeedVisitor> speedVisitor) : base(keyVisitor, powerVisitor, prereqVisitor, componentSetVisitor, upgradesToVisitor, parser, componentsList)
        {
            _evasionVisitor = evasionVisitor;
            _speedVisitor = speedVisitor;
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var thruster = base.VisitKeyval(context) as Thruster;
            if (thruster == null)
                return null;
            
            var evasion = _evasionVisitor.Visit(context.val());
            var speed = _speedVisitor.Visit(context.val());

            thruster.Evasion = evasion;
            thruster.SpeedMultipler = speed;

            return thruster;
        }

        public new Thruster VisitContent(stellarisParser.ContentContext context)
        {
            return (Thruster) base.VisitContent(context);
        }
    }
}