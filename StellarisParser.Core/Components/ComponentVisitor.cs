using System.Collections.Generic;
using StellarisParser.Core.Techs;

namespace StellarisParser.Core.Components
{
    public abstract class ComponentVisitor : StellarisVisitor<Component>
    {
        private readonly ComponentsList _componentsList;
        
        private readonly KeyVisitor _keyVisitor;
        private readonly PowerVisitor _powerVisitor;
        private readonly PrereqVisitor _prereqVisitor;
        private readonly ComponentSetVisitor _componentSetVisitor;
        private readonly UpgradesToVisitor _upgradesToVisitor;
        
        private readonly Parser _parser;
        protected abstract string ComponentSet { get; }
        
        public abstract Component Create();

        public ComponentVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor,
            ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser,
            ComponentsList componentsList)
        {
            _keyVisitor = keyVisitor;
            _powerVisitor = powerVisitor;
            _prereqVisitor = prereqVisitor;
            _parser = parser;
            _componentsList = componentsList;
            _upgradesToVisitor = upgradesToVisitor;
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
            var upgradesTo = _upgradesToVisitor.Visit(context.val());

            var component = Create();
            component.Key = key;
            component.Power = power;
            component.Source = source;
            component.Prerequisites = prereqs?.ToList() ?? new List<Tech>();
            component.UpgradesTo = _componentsList[upgradesTo];

            return component;
        }
    }
}