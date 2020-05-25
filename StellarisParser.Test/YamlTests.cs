using StellarisParser.Core;
using StellarisParser.Core.Localisation;
using Xunit;

namespace StellarisParser.Test
{
    public class YamlTests
    {
        [Fact]
        public void CanLoadLocalisation()
        {
            var yamlParser = new YamlParser();
            var path = Specs.BASE_PATH + "\\localisation\\english\\technology_l_english.yml";
            
            var localisation = yamlParser.LoadYaml(path);
            Assert.Equal(488, localisation.Count);
        }
    }
}