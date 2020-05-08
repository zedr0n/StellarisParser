using System.Collections.Generic;
using StellarisParser.Core.Techs;

namespace StellarisParser.Core.Components
{
    public abstract class Component
    {
        public string Key { get; set; }
        public string Type => ComponentType.ToString();
        public abstract Specs.ComponentType ComponentType { get; }
        
        public List<Tech> Prerequisites { get; set; } = new List<Tech>();
        public double Power { get; set; }
        public string Source { get; set; }
        
        public Component UpgradesTo { get; set; }
    }
}