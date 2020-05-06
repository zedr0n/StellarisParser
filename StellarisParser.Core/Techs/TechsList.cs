using System.Collections.Generic;
using System.Linq;

namespace StellarisParser.Core.Techs
{
    public class TechsList
    {
        private readonly Dictionary<string, Tech> _techs = new Dictionary<string, Tech>();

        public Dictionary<string, Tech> Map => _techs;

        public void Aggregate(TechsList other)
        {
            if (other == null)
                return;
            
            var copy = new Dictionary<string, Tech>(other.Map);
            foreach (var (key, value) in copy)
                Map[key] = value;
        }

        public void Add(Tech tech)
        {
            Map[tech.Name] = tech;
        }

        public int Count => Map.Count;

        public Tech this[string key] => Map.ContainsKey(key) ? Map[key] : null;

        public List<Tech> ToList() => Map.Values.ToList();
    }
}