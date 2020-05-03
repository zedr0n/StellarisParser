using System.Collections.Generic;
using static System.Double;

namespace StellarisParser.Core
{
    public class Variables
    {
        // public List<Variable<double>> Values { get; set; } = new List<Variable<double>>();
        private Dictionary<string, Dictionary<string, string>> SourceMaps = new Dictionary<string, Dictionary<string, string>>();
        private Dictionary<string, string> Map { get; } = new Dictionary<string, string>();

        public Variables()
        {
            
        }

        public int Count => Map.Count;

        public void Add(Variable<string> var)
        {
            Map[var.Id] = var.Value;
        }
        
        public void Aggregate(Variables other, string source = null)
        {
            if (source != null)
                SourceMaps[source] = new Dictionary<string, string>(other.Map);
            
            foreach (var (key, value) in other.Map)
                Map[key] = value;
        }

        public double Get(string name)
        {
            if (Map.ContainsKey(name))
                return Parse(Map[name]);
            return NaN;
        }

        public string GetString(string name)
        {
            if (Map.ContainsKey(name))
                return Map[name];
            return string.Empty;
        }
    }
}