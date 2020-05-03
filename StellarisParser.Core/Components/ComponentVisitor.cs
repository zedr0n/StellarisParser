namespace StellarisParser.Core.Components
{
    public class ComponentVisitor : StellarisVisitor<Component>
    {
        private readonly KeyVisitor _keyVisitor;
        private readonly PowerVisitor _powerVisitor;
        private readonly PrereqVisitor _prereqVisitor;

        public ComponentVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor)
        {
            _keyVisitor = keyVisitor;
            _powerVisitor = powerVisitor;
            _prereqVisitor = prereqVisitor;
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var key = _keyVisitor.Visit(context.val());
            var power = _powerVisitor.Visit(context.val());
            var prereqs = _prereqVisitor.Visit(context.val());

            return new Component
            {
                Key = key,
                Power = power,
                Prerequisites = prereqs.ToList()
            };
        }
    }
}