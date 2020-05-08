using System.Collections.Generic;
using System.Linq;
using static StellarisParser.Core.Specs;

namespace StellarisParser.Core.Components
{
    public class ComponentSets
    {
        private readonly Dictionary<string, ComponentType> _componentTypes  = new Dictionary<string, ComponentType>();
        private readonly Dictionary<string, ComponentType> _guesses = new Dictionary<string, ComponentType>
        {
            { THRUSTER_SET, ComponentType.THRUSTER },
            { REACTOR_SET, ComponentType.REACTOR},
            { FTL_SET, ComponentType.FTL_DRIVE },
            { AFTERBURNER_SET, ComponentType.AFTERBURNER }
        };

        public void Aggregate(ComponentSets other)
        {
            var copy = new Dictionary<string, ComponentType>(other._componentTypes);
            foreach (var (name, type) in copy)
            {
                _componentTypes[name] = type;
            }
        }
        
        public ComponentType this[string id] => _componentTypes.ContainsKey(id) ? _componentTypes[id] : ComponentType.UNKNOWN;
        
        public void Add(string componentSet, ComponentType type = ComponentType.UNKNOWN)
        {
            if (type == ComponentType.UNKNOWN)
                GuessComponentType(componentSet);
            else
                _componentTypes[componentSet] = type;
        }
        
        private void GuessComponentType(string componentSet)
        {
            if (!_guesses.Any(g => componentSet.ToUpper().Contains(g.Key.ToUpper())))
                return;
            
            var guess = _guesses.SingleOrDefault(g => componentSet.ToUpper().Contains(g.Key.ToUpper()));
            _componentTypes[componentSet] = guess.Value;
        }
    }
    
}