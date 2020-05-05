using System.Collections.Generic;
using StellarisParser.Core.Techs;

namespace StellarisParser.Core.Components
{
    public class Component
    {
        public string Key { get; set; }
        public List<Tech> Prerequisites { get; set; } = new List<Tech>();
        public double Power { get; set; }
        public string Source { get; set; }
        
        public virtual string Type { get; }
    }
}