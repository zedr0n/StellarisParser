using System.Collections.Generic;
using Antlr4.Runtime.Tree;
using StellarisParser.Core.Techs;

namespace StellarisParser.Core.Components
{
    public interface IComponentVisitor
    {
        public Component Visit(IParseTree tree);
    }
    
    public abstract class ComponentVisitor<T> : StellarisVisitor<T>, IComponentVisitor
        where T : Component, new()
    {
        private readonly ComponentsList _componentsList;
        private readonly ComponentSets _componentSets;
        
        private readonly KeyVisitor _keyVisitor;
        private readonly PowerVisitor _powerVisitor;
        private readonly PrereqVisitor _prereqVisitor;
        private readonly ComponentSetVisitor _componentSetVisitor;
        private readonly UpgradesToVisitor _upgradesToVisitor;
        private readonly ModifiersVisitor _modifiersVisitor;
        private readonly SizeVisitor _sizeVisitor;
        
        private readonly Parser _parser;
        protected Specs.ComponentType ComponentType => new T().ComponentType;

        public ComponentVisitor(KeyVisitor keyVisitor, PowerVisitor powerVisitor, PrereqVisitor prereqVisitor,
            ComponentSetVisitor componentSetVisitor, UpgradesToVisitor upgradesToVisitor, Parser parser,
            ComponentsList componentsList, ComponentSets componentSets, ModifiersVisitor modifiersVisitor, SizeVisitor sizeVisitor)
        {
            _keyVisitor = keyVisitor;
            _powerVisitor = powerVisitor;
            _prereqVisitor = prereqVisitor;
            _parser = parser;
            _componentsList = componentsList;
            _componentSets = componentSets;
            _modifiersVisitor = modifiersVisitor;
            _sizeVisitor = sizeVisitor;
            _upgradesToVisitor = upgradesToVisitor;
            _componentSetVisitor = componentSetVisitor;
        }

        public override T VisitKeyval(stellarisParser.KeyvalContext context)
        {
            var componentsSet = _componentSetVisitor.Visit(context.val())?.Replace('"'.ToString(),"");
            if (componentsSet == null)
            {
                var str = context.GetText();
            }

            var componentType = _componentSets[componentsSet];
            if (componentType == Specs.ComponentType.UNKNOWN && ( context.GetText().StartsWith(Specs.WEAPON_TEMPLATE) || context.GetText().StartsWith(Specs.STRIKE_CRAFT_TEMPLATE)))
                componentType = Specs.ComponentType.WEAPON;

            var size = _sizeVisitor.Visit(context.val())?.Replace('"'.ToString(), "");
            if (componentType == Specs.ComponentType.UNKNOWN && size == "aux")
                componentType = Specs.ComponentType.AUXILARY;
            
            if (componentType != ComponentType)
                return null;

            var key = _keyVisitor.Visit(context.val());
            var power = _powerVisitor.Visit(context.val());
            var prereqs = _prereqVisitor.Visit(context.val());
            var source = _parser.CurrentSource;
            var upgradesTo = _upgradesToVisitor.Visit(context.val());
            var modifiers = _modifiersVisitor.Visit(context.val());

            var component = new T();
            component.Key = key;
            component.Power = power;
            component.Source = source;
            component.Prerequisites = prereqs?.ToList() ?? new List<Tech>();
            component.UpgradesTo = _componentsList[upgradesTo];
            component.Modifiers = modifiers;

            return component;
        }

        public Component Visit(IParseTree tree)
        {
            return base.Visit(tree);
        }
    }
}