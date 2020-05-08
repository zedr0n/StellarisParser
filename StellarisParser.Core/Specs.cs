using System.Collections.Generic;

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
        public const string EVASION_MULT_ID = "ship_evasion_mult";
        public const string BASE_SPEED_MULT_ID = "ship_base_speed_mult";
        public const string SPEED_MULT_ID = "ship_speed_mult";
        public const string WINDUP_ID = "ship_windup_mult";
        public const string JUMPDRIVE_RANGE_ID = "ship_ftl_jumpdrive_range_mult";
        public const string MODIFIER_ID = "modifier";
        public const string SHIP_MODIFIER_ID = "ship_modifier";
        public const string JUMPDRIVE_ID = "jumpdrive";
        
        public const string SET_ID = "component_set";

        public const string THRUSTER_SET = "thruster_components";
        public const string REACTOR_SET = "power_core";
        public const string FTL_SET = "ftl_components";
        public const string AFTERBURNER_SET = "AFTERBURNER";
        
        public const string BASE_PATH = "f:\\steam\\steamapps\\common\\Stellaris";
        public const string BASE_VARS = BASE_PATH + "\\common\\scripted_variables\\00_scripted_variables.txt";
        public const string TECH_PATH = BASE_PATH + "\\common\\technology";
        public const string COMPONENT_PATH = BASE_PATH + "\\common\\component_templates";
        public const string COMPONENT_SETS_POSTFIX = "\\common\\component_sets";

        public const string NHSC_PATH = "F:\\steam\\steamapps\\workshop\\content\\281990\\1885775216";

        public enum ComponentType
        {
            THRUSTER,
            REACTOR,
            FTL_DRIVE,
            AFTERBURNER,
            UNKNOWN
        }
    }
}