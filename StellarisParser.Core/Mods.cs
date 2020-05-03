using System.Collections.Generic;

namespace StellarisParser.Core
{
    public class Mods
    {
        // path -> ModDescriptor
        public Dictionary<string, ModDescriptor> Map { get; set; } = new Dictionary<string, ModDescriptor>();
    }
}