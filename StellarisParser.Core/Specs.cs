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
        public const string DAMAGE_ID = "damage";

        public const string SENSOR_RANGE_ID = "sensor_range";
        public const string HYPERLANE_RANGE_ID = "hyperlane_range";
        
        public const string EVASION_ID = "ship_evasion_add";
        public const string EVASION_MULT_ID = "ship_evasion_mult";
        public const string BASE_SPEED_MULT_ID = "ship_base_speed_mult";
        public const string SPEED_MULT_ID = "ship_speed_mult";
        public const string WINDUP_ID = "ship_windup_mult";
        public const string JUMPDRIVE_RANGE_ID = "ship_ftl_jumpdrive_range_mult";
        public const string MODIFIER_ID = "modifier";
        public const string SHIP_MODIFIER_ID = "ship_modifier";
        public const string JUMPDRIVE_ID = "jumpdrive";
        public const string FIRERATE_ID = "ship_fire_rate_mult";
        public const string TRACKING_ID = "ship_tracking_add";
        public const string ACCURACY_ID = "ship_accuracy_add";
        public const string WEAPON_RANGE_MULT_ID = "ship_weapon_range_mult";
        public const string ENGAGEMENT_RANGE_MULT_ID = "ship_engagement_range_mult";
        public const string ARMOR_ID = "ship_armor_add";
        public const string HULL_ID = "ship_hull_add";
        public const string SHIELD_REGEN_ID = "ship_shield_regen_add_static";
        public const string SHIELD_ID = "ship_shield_add";
        public const string SHIELD_MULT_ID = "ship_shield_mult";
        
        public const string SET_ID = "component_set";
        public const string SIZE_ID = "size";

        public const string THRUSTER_SET = "thruster_components";
        public const string REACTOR_SET = "power_core";
        public const string FTL_SET = "ftl_components";
        public const string AFTERBURNER_SET = "afterburner";
        public const string SENSOR_SET = "sensor_components";
        public const string COMBAT_COMPUTER_SET = "combat_computers";
        public const string ARMOR_SET = "armor";
        
        public const string SHIELD_SET = "shield";
        public const string DEFLECTOR_SET = "deflector";
        public const string BARRIER_SET = "barrier";
        
        public const string LIGHT_WALL_SET = "light_wall";
        public const string WEAPON_TEMPLATE = "weapon_component_template";
        public const string STRIKE_CRAFT_TEMPLATE = "strike_craft_component_template";

        public const string AURA_SET = "aura";

        public const string BASE_PATH = "f:\\steam\\steamapps\\common\\Stellaris";
        public const string BASE_VARS = BASE_PATH + "\\common\\scripted_variables\\00_scripted_variables.txt";
        public const string BASE_VARS_DIR = BASE_PATH + "\\common\\scripted_variables";
        public const string TECH_PATH = BASE_PATH + "\\common\\technology";
        public const string LOCALISATION_PATH = BASE_PATH + "\\localisation\\english";
        public const string COMPONENT_PATH = BASE_PATH + "\\common\\component_templates";
        public const string COMPONENT_SETS_POSTFIX = "\\common\\component_sets";
        public const string COMPONENT_SETS_PATH = BASE_PATH + COMPONENT_SETS_POSTFIX;
        

        public const string NHSC_PATH = "F:\\steam\\steamapps\\workshop\\content\\281990\\1885775216";

        public enum ComponentType
        {
            THRUSTER,
            REACTOR,
            FTL_DRIVE,
            AFTERBURNER,
            SENSOR,
            COMBAT_COMPUTER,
            ARMOR,
            SHIELD,
            AUXILARY,
            WEAPON,
            AURA,
            UNKNOWN
        }
    }
}