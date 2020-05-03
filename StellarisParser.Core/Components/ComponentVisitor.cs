using System.Collections.Generic;

namespace StellarisParser.Core.Components
{
    public class ComponentVisitor : StellarisVisitor<Component>
    {
        private readonly KeyVisitor _keyVisitor;
        private readonly PowerVisitor _powerVisitor;
        private readonly PrereqVisitor _prereqVisitor;

        private readonly Parser _parser;

        public ComponentVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor, Parser parser)
        {
            _keyVisitor = keyVisitor;
            _powerVisitor = powerVisitor;
            _prereqVisitor = prereqVisitor;
            _parser = parser;
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var key = _keyVisitor.Visit(context.val());
            var power = _powerVisitor.Visit(context.val());
            var prereqs = _prereqVisitor.Visit(context.val());
            var source = _parser.CurrentSource;
            
            return new Component
            {
                Key = key,
                Power = power,
                Prerequisites = prereqs?.ToList() ?? new List<Tech>(),
                Source = source
            };
        }
    }
}