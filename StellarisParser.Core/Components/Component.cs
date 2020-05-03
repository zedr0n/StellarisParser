using System.Collections.Generic;

namespace StellarisParser.Core.Components
{
    public class Component
    {
        public string Key { get; set; }
        public List<Tech> Prerequisites { get; set; } = new List<Tech>();
        public double Power { get; set; }
        
    }
}