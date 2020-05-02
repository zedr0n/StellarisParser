using SimpleInjector;
using StellarisParser.Core;
using Xunit;

namespace StellarisParser.Test
{
    public class ParseTests
    {
        private Parser CreateParser()
        {
            var container = new Container();
            var root = new CompositionRoot();
            root.ComposeApplication(container);

            container.Verify();
            return container.GetInstance<Parser>();
        }
        
        
        [Fact]
        public void CanParseSolarPanelNetworks()
        {
            var parser = CreateParser();

            var input = @"tech_solar_panel_network = {
	                        area = engineering
	                        tier = 0
	                        category = { voidcraft }
	                        prerequisites = { ""tech_starbase_2"" }
                            start_tech = yes

                            potential = {
                                is_gestalt = yes
                            }
                    }";
            var tech = parser.RunVisitor<Tech>(input);
            
            Assert.Equal("tech_solar_panel_network", tech.Name);
            Assert.Equal("engineering", tech.Area);
            Assert.Equal(0, tech.Tier);
        }
    }
}