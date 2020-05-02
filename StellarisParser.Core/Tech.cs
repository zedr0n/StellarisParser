using System.Collections.Generic;

namespace StellarisParser.Core
{
    public class Tech
    {
        public string Name { get; set; }

        public string Area { get; set; }
        public int Tier { get; set; }
        public List<string> Category { get; } = new List<string>();
        public List<Tech> Prerequisites { get; } = new List<Tech>();
    }
}