using System.ComponentModel;
using System.Linq;
using StellarisParser.Core;
using StellarisParser.Core.Components;
using StellarisParser.Core.Techs;
using Xunit;
using Component = StellarisParser.Core.Components.Component;
using Container = SimpleInjector.Container;

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
        
        private const string Thruster = @"utility_component_template = {
	key = ""DESTROYER_SHIP_THRUSTER_1""
        size = small
            icon = ""GFX_ship_part_thruster_1""
        icon_frame = 1
        power = @destroyer_power_1
            resources = {
                category = ship_components
                cost = {
                alloys = @destroyer_cost1
                }
            }
	
        modifier = {
            		ship_base_speed_mult = 0.25
		            ship_evasion_add = 3
        }
		
        prerequisites = { ""tech_thrusters_1"" }
        component_set = ""thruster_components""
        size_restriction = { destroyer }
        upgrades_to = ""DESTROYER_SHIP_THRUSTER_2""
	
        ai_weight = {
            weight = 1
        }
    }
    ";

        [Fact]
        public void CanParseSolarPanelNetworks()
        {
            var parser = CreateParser();
            
            var tech = parser.RunVisitor<TechsList>(SolarPanelNetworks).Map.First().Value;
            
            Assert.Equal("tech_solar_panel_network", tech.Name);
            Assert.Equal("engineering", tech.Area);
            Assert.Equal(0, tech.Tier);
        }

        [Fact]
        public void CanDisableTech()
        {
            var parser = CreateParser();
            
            var str = parser.ApplyModifications(DestroyersWithTechCosts + "\n" + SolarPanelNetworks);
            var target = @"

tech_destroyers   = { cost   = @tier2cost1     area   = engineering    tier   = 2    category   = { voidcraft  }    prerequisites   = { ""tech_corvettes""  }    weight   = @tier2weight1     gateway   = ship    potential   = { }     }   
tech_solar_panel_network   = { area   = engineering    tier   = 0    category   = { voidcraft  }    prerequisites   = { ""tech_starbase_2""  }    start_tech   = yes    potential   = { }     }   ";
            target = target.Replace("\r", "");
                
            Assert.Equal(target, str);
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
            
            var tech = parser.RunVisitor<TechsList>(DestroyersWithTechCosts).Map.First().Value;

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
            
            var techs = parser.RunVisitor<TechsList>(Destroyers);

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
            var techs = parser.RunVisitor<TechsList>(str);
            
            Assert.Equal(2, techs.Count);
            Assert.Equal(4000, techs["tech_destroyers"].Cost);
        }

        [Fact]
        public void CanParseFile()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();

            var techs = parser.ReadFile(Specs.TECH_PATH + "\\00_eng_tech.txt",Specs.BASE_VARS);
            
            Assert.Equal(80, techs.Count);
            var tech = techs["tech_destroyers"];
            Assert.Equal("tech_corvettes", tech.Prerequisites.Single().Name);
        }

        [Fact]
        public void CanSerialiseGraph()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();

            var techs = parser.ReadFile(Specs.TECH_PATH + "\\00_eng_tech.txt",Specs.BASE_VARS);
            var graph = container.GetInstance<Graph>();
            graph.Populate();
            graph.Serialise(nameof(CanSerialiseGraph));
        }

        [Fact]
        public void CanParseComponent()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadVars(Specs.COMPONENT_PATH + "\\00_utilities_thrusters.txt");
            //parser.ReadVars("../../../../00_scripted_variables.txt");

            var component = parser.RunVisitor<Component>(Thruster);
            Assert.Equal(-20, component.Power);
            Assert.Single(component.Prerequisites);
            
            var thruster = parser.RunVisitor<Thruster>(Thruster);
            Assert.Equal(3, thruster.Evasion);
        }
        
        [Fact]
        public void CanParseThruster()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadComponents(Specs.COMPONENT_PATH + "\\00_utilities_thrusters.txt");

            var components = container.GetInstance<ComponentsList>();
            Assert.Equal(30, components.Count);
        }
        
        [Fact]
        public void CanParseReactor()
        {
            var container = CreateContainer();
            var parser = container.GetInstance<Parser>();
            parser.ReadTechs(Specs.TECH_PATH + "\\00_eng_tech.txt");
            parser.ReadComponents(Specs.COMPONENT_PATH + "\\00_utilities_reactors.txt");

            var components = container.GetInstance<ComponentsList>();
            Assert.Equal(46, components.Count);
        }
    }
}