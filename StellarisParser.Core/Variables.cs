using System.Collections.Generic;

namespace StellarisParser.Core
{
    public class Variables
    {
        // public List<Variable<double>> Values { get; set; } = new List<Variable<double>>();
        public Dictionary<string, double> Map { get; } = new Dictionary<string, double>();

        public Variables()
        {
            
        }

        public Variables(Variable<double> var)
        {
            Map[var.Id] = var.Value;
        }

        public void Aggregate(Variables other)
        {
            foreach (var (key, value) in other.Map)
                Map[key] = value;
        }
    }
}