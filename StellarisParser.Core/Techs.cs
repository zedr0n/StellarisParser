using System.Collections.Generic;

namespace StellarisParser.Core
{
    public class Techs
    {
        private readonly Dictionary<string, Tech> _techs = new Dictionary<string, Tech>();

        public Dictionary<string, Tech> Map => _techs;

        public void Aggregate(Techs other)
        {
            foreach (var (key, value) in other.Map)
                Map[key] = value;
        }

        public void Add(Tech tech)
        {
            Map[tech.Name] = tech;
        }

        public int Count => Map.Count;

        public Tech this[string key] => Map[key];
    }
}