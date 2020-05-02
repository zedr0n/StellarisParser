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

        [Fact]
        public void CanParseVariables()
        {
            var parser = CreateParser();
            
            var input = @"# TECH COSTS
                          @tier1cost1 = 2000
                          @tier1cost2 = 2500
                          @tier1cost3 = 3000";

            var vars = parser.RunVisitor<Variables>(input);
            
            Assert.Equal(3, vars.Map.Count);
            Assert.Equal(2500, vars.Map["tier1cost2"]);
        }
    }
}