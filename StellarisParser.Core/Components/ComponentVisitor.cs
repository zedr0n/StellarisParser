using System;
using System.Collections.Generic;
using System.Diagnostics;
using Antlr4.Runtime.Tree;
using StellarisParser.Core.Techs;

namespace StellarisParser.Core.Components
{
    public interface IComponentVisitor
    {
        public Component Visit(IParseTree tree);
    }
    
    public abstract class ComponentVisitor : StellarisVisitor<Component>, IComponentVisitor
    {
        private readonly ComponentsList _componentsList;
        private readonly ComponentSets _componentSets;
        
        private readonly KeyVisitor _keyVisitor;
        private readonly PowerVisitor _powerVisitor;
        private readonly PrereqVisitor _prereqVisitor;
        private readonly ComponentSetVisitor _componentSetVisitor;
        private readonly UpgradesToVisitor _upgradesToVisitor;
        
        private readonly Parser _parser;
        protected Specs.ComponentType ComponentType => Create().ComponentType;
        
        public abstract Component Create();

        public ComponentVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor,
            ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser,
            ComponentsList componentsList, ComponentSets componentSets)
        {
            _keyVisitor = keyVisitor;
            _powerVisitor = powerVisitor;
            _prereqVisitor = prereqVisitor;
            _parser = parser;
            _componentsList = componentsList;
            _componentSets = componentSets;
            _upgradesToVisitor = upgradesToVisitor;
            _componentSetVisitor = componentSetVisitor;
        }

        public override Component VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var componentsSet = _componentSetVisitor.Visit(context.val())?.Replace('"'.ToString(),"");
            if (componentsSet == null)
            {
                var str = context.GetText();
            }

            var componentType = _componentSets[componentsSet];
            
            if (componentType != ComponentType)
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