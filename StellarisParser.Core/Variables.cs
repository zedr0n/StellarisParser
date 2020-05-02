using System.Collections.Generic;
using static System.Double;

namespace StellarisParser.Core
{
    public class Variables
    {
        // public List<Variable<double>> Values { get; set; } = new List<Variable<double>>();
        private Dictionary<string, Dictionary<string, double>> SourceMaps = new Dictionary<string, Dictionary<string, double>>();
        private Dictionary<string, double> Map { get; } = new Dictionary<string, double>();

        public Variables()
        {
            
        }

        public int Count => Map.Count;

        public void Add(Variable<double> var)
        {
            Map[var.Id] = var.Value;
        }
        
        public void Aggregate(Variables other, string source = null)
        {
            if (source != null)
                SourceMaps[source] = new Dictionary<string, double>(other.Map);
            
            foreach (var (key, value) in other.Map)
                Map[key] = value;
        }

        public double Get(string name)
        {
            if (Map.ContainsKey(name))
                return Map[name];
            return NaN;
        }
    }
}