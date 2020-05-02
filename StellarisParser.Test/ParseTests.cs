using System.Linq;
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

        private const string DestroyersWithTechCosts = @"@tier2cost1 = 4000

            tech_destroyers = {
	        cost = @tier2cost1
	        area = engineering
	        tier = 2
	        category = { voidcraft }
	        prerequisites = { ""tech_corvettes"" }
            weight = @tier2weight1

            gateway = ship
        }";

        private const string Corvettes = @"tech_corvettes = {
	        cost = 0
	        area = engineering
	        start_tech = yes
	        category = { voidcraft }
	        prerequisites = { ""tech_starbase_2"" }
                tier = 0
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
            
            var tech = parser.RunVisitor<Techs>(SolarPanelNetworks).Map.First().Value;
            
            Assert.Equal("tech_solar_panel_network", tech.Name);
            Assert.Equal("engineering", tech.Area);
            Assert.Equal(0, tech.Tier);
        }

        [Fact]
        public void CanParseVariables()
        {
            var parser = CreateParser();

            var vars = parser.RunVisitor<Variables>(TechCosts);
            
            Assert.Equal(6, vars.Count);
            Assert.Equal(2500, vars.Get("tier1cost2"));
        }

        [Fact]
        public void CanParseTechWithVariables()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            var variables = container.GetInstance<Variables>();
            
            var vars = parser.RunVisitor<Variables>(DestroyersWithTechCosts);
            variables.Aggregate(vars);
            
            var tech = parser.RunVisitor<Techs>(DestroyersWithTechCosts).Map.First().Value;

            Assert.Equal(4000, tech.Cost);
        }

        [Fact]
        public void CanParseTechWithSeparateVariables()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            var variables = container.GetInstance<Variables>();
            
            var vars = parser.RunVisitor<Variables>(TechCosts);
            variables.Aggregate(vars);
            
            var techs = parser.RunVisitor<Techs>(Destroyers);

            Assert.Equal(4000, techs["tech_destroyers"].Cost);
        }

        [Fact]
        public void CanParseMultipleTechs()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            var variables = container.GetInstance<Variables>();

            var str = DestroyersWithTechCosts + "\n" + SolarPanelNetworks;
            variables.Aggregate(parser.RunVisitor<Variables>(str));
            var techs = parser.RunVisitor<Techs>(str);
            
            Assert.Equal(2, techs.Count);
            Assert.Equal(4000, techs["tech_destroyers"].Cost);
        }

        [Fact]
        public void CanParseFile()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();

            var techs = parser.ReadFile("../../../../00_eng_tech.txt","../../../../00_scripted_variables.txt");
            
            Assert.Equal(80, techs.Count);
            var tech = techs["tech_destroyers"];
            Assert.Equal("tech_corvettes", tech.Prerequisites.Single().Name);
        }

        [Fact]
        public void CanSerialiseGraph()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();

            var techs = parser.ReadFile("../../../../00_eng_tech.txt","../../../../00_scripted_variables.txt");
            var graph = container.GetInstance<Graph>();
            graph.Populate();
            graph.Serialise(nameof(CanSerialiseGraph));
        }
    }
}