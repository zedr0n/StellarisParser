using System.Collections.Generic;
using StellarisParser.Core.Techs;

namespace StellarisParser.Core.Components
{
    public class ComponentVisitor : StellarisVisitor<Component>
    {
        private readonly KeyVisitor _keyVisitor;
        private readonly PowerVisitor _powerVisitor;
        private readonly PrereqVisitor _prereqVisitor;
        private readonly ComponentSetVisitor _componentSetVisitor;

        private readonly Parser _parser;
        protected virtual string ComponentSet { get; } = null;

        public ComponentVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor,
            ComponentSetVisitor componentSetVisitor, Parser parser)
        {
            _keyVisitor = keyVisitor;
            _powerVisitor = powerVisitor;
            _prereqVisitor = prereqVisitor;
            _parser = parser;
            _componentSetVisitor = componentSetVisitor;
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var componentsSet = _componentSetVisitor.Visit(context.val()).Replace('"'.ToString(),"");
            if (componentsSet != ComponentSet && GetType() != typeof(ComponentVisitor))
                return null;

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