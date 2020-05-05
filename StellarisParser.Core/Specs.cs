namespace StellarisParser.Core
{
    public static class Specs
    {
        public const string POTENTIAL_ID = "potential";
        public const string AREA_ID = "area";
        public const string TIER_ID = "tier";
        public const string COST_ID = "cost";
        public const string PREREQ_ID = "prerequisites";
        public const string KEY_ID = "key";
        public const string POWER_ID = "power";
        public const string UPGRADES_TO_ID = "upgrades_to";
        public const string EVASION_ID = "ship_evasion_add";
        public const string SPEED_ID = "ship_base_speed_mult";
        public const string MODIFIER_ID = "modifier";
        public const string SET_ID = "component_set";

        public const string THRUSTER_SET = "thruster_components";
        public const string REACTOR_SET = "power_core";
        
        public const string BASE_PATH = "f:\\steam\\steamapps\\common\\Stellaris";
        public const string BASE_VARS = BASE_PATH + "\\common\\scripted_variables\\00_scripted_variables.txt";
        public const string TECH_PATH = BASE_PATH + "\\common\\technology";
        public const string COMPONENT_PATH = BASE_PATH + "\\common\\component_templates";
        
        public enum ComponentType
        {
            THRUSTER,
            REACTOR
        }
    }
}