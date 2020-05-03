using System.Collections.Generic;
using System.Linq;

namespace StellarisParser.Core.Components
{
    public class Components
    {
        private readonly Dictionary<string, Component> _components = new Dictionary<string, Component>();

        public Dictionary<string, Component> Map => _components;

        public void Aggregate(Components other)
        {
            var copy = new Dictionary<string, Component>(other.Map);
            foreach (var (key, value) in copy)
                Map[key] = value;
        }

        public void Add(Component component)
        {
            Map[component.Key.Replace('"'.ToString(),"")] = component;
        }

        public int Count => Map.Count;

        public Component this[string key] => Map.ContainsKey(key) ? Map[key] : null;

        public List<Component> ToList() => Map.Values.ToList();
    }
}