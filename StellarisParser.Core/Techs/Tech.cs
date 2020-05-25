using System.Collections.Generic;

namespace StellarisParser.Core.Techs
{
    public class Tech
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Area { get; set; }
        public int Tier { get; set; }
        public int Cost { get; set; }
        public string Source { get; set; }
        public List<string> Category { get; } = new List<string>();
        public List<Tech> Prerequisites { get; set; } = new List<Tech>();
        public bool Disable { get; set; } = false;
    }
}