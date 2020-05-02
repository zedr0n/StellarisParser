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

        private Container CreateContainer()
        {
            var container = new Container();
            var root = new CompositionRoot();
            root.ComposeApplication(container);

            container.Verify();
            return container;
        }


        private const string TechCosts = @"# TECH COSTS
                                       @tier1cost1 = 2000
                                       @tier1cost2 = 2500
                                       @tier1cost3 = 3000

                                       @tier2cost1 = 4000
                                       @tier2cost2 = 5000
                                       @tier2cost3 = 6000";

        private const string SolarPanelNetworks = @"tech_solar_panel_network = {
	                        area = engineering
	                        tier = 0
	                        category = { voidcraft }
	                        prerequisites = { ""tech_starbase_2"" }
                            start_tech = yes

                            potential = {
                                is_gestalt = yes
                            }
                            }";

        private const string Destroyers = @"tech_destroyers = {
	        cost = @tier2cost1
	        area = engineering
	        tier = 2
	        category = { voidcraft }
	        prerequisites = { ""tech_corvettes"" }
            weight = @tier2weight1

            gateway = ship
        }";
        
        [Fact]
        public void CanParseSolarPanelNetworks()
        {
            var parser = CreateParser();
            
            var tech = parser.RunVisitor<Tech>(SolarPanelNetworks);
            
            Assert.Equal("tech_solar_panel_network", tech.Name);
            Assert.Equal("engineering", tech.Area);
            Assert.Equal(0, tech.Tier);
        }

        [Fact]
        public void CanParseVariables()
        {
            var parser = CreateParser();

            var vars = parser.RunVisitor<Variables>(TechCosts);
            
            Assert.Equal(3, vars.Count);
            Assert.Equal(2500, vars.Get("tier1cost2"));
        }

        [Fact]
        public void CanParseTechWithVariables()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            var variables = container.GetInstance<Variables>();
            
            var vars = parser.RunVisitor<Variables>(TechCosts);
            variables.Aggregate(vars);
            
            var tech = parser.RunVisitor<Tech>(Destroyers);

            Assert.Equal(4000, tech.Cost);
        }
    }
}