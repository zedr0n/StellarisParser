using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;
using StellarisParser.Core.Components;
using StellarisParser.Core.Components.Afterburners;
using StellarisParser.Core.Components.Armors;
using StellarisParser.Core.Components.Auras;
using StellarisParser.Core.Components.Auxilaries;
using StellarisParser.Core.Components.CombatComputers;
using StellarisParser.Core.Components.Drives;
using StellarisParser.Core.Components.Reactors;
using StellarisParser.Core.Components.Sensors;
using StellarisParser.Core.Components.Shields;
using StellarisParser.Core.Components.Thrusters;
using StellarisParser.Core.Components.Weapons;
using StellarisParser.Core.Modifiers;
using StellarisParser.Core.Techs;
using Armor = StellarisParser.Core.Modifiers.Armor;
using Shield = StellarisParser.Core.Modifiers.Shield;

namespace StellarisParser.Core
{

    public static class AssemblyExtensions
    {
        public static IEnumerable<Type> GetTypesFromInterface(this Assembly assembly, Type t)
        {
            var types = assembly.GetTypes()
                .Where(p => p.GetInterfaces().Contains(t));
            return types;
        }
    }
    public class CompositionRoot
    {
        private void RegisterModifiers(Container container)
        {
            container.Collection.Append<ModifierVisitor, ModifierVisitor<ShipEvasionMultiplier>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<SpeedMultiplier>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<Accuracy>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<EngagementRangeMultiplier>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<FireRateMultiplier>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<Tracking>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<WeaponRangeMultiplier>>(Lifestyle.Singleton);
            
            container.Collection.Append<ModifierVisitor, ModifierVisitor<Armor>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<Hull>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<Shield>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<ShieldRegen>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<ShieldMultiplier>>(Lifestyle.Singleton);

            container.Collection.Append<ModifierVisitor, ModifierVisitor<ShipWindup>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<JumpDriveRangeMultiplier>>(Lifestyle.Singleton);
            
            container.Collection.Append<ModifierVisitor, ModifierVisitor<ShipEvasion>>(Lifestyle.Singleton);
            container.Collection.Append<ModifierVisitor, ModifierVisitor<BaseSpeedMultiplier>>(Lifestyle.Singleton);

            container.Register<ModifiersVisitor>(Lifestyle.Singleton);
        }
        
        public virtual void ComposeApplication(Container container)
        {
            container.Register<IStellarisVisitor<TechsList>, TechsListVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<Variables>, VariableVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<ModDescriptor>, DescriptorVisitor>(Lifestyle.Singleton);
            //container.Register<IStellarisVisitor<Component>, ComponentVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<ComponentSets>, ComponentSetsVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<Thruster>, ThrusterVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<Reactor>, ReactorVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<FtlDrive>, FtlDriveVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<ComponentsList>, ComponentsListVisitor>(Lifestyle.Singleton);
            
            container.Register<ComponentSets>(Lifestyle.Singleton);
            container.Register<ComponentSetsVisitor>(Lifestyle.Singleton);

            RegisterModifiers(container);

            container.Register<AreaVisitor>(Lifestyle.Singleton);
            container.Register<TierVisitor>(Lifestyle.Singleton);
            container.Register<CostVisitor>(Lifestyle.Singleton);
            container.Register<PrereqVisitor>(Lifestyle.Singleton);
            container.Register<KeyVisitor>(Lifestyle.Singleton);
            container.Register<PowerVisitor>(Lifestyle.Singleton);
            container.Register<UpgradesToVisitor>(Lifestyle.Singleton);
            container.Register<ComponentSetVisitor>(Lifestyle.Singleton);
            container.Register<SizeVisitor>(Lifestyle.Singleton);
            
            // components
            container.Register<ThrusterVisitor>(Lifestyle.Singleton);
            container.Register<ReactorVisitor>(Lifestyle.Singleton);
            
            container.Register<JumpDriveVisitor>(Lifestyle.Singleton);
            container.Register<FtlDriveVisitor>(Lifestyle.Singleton);
            
            container.Register<AfterburnerVisitor>(Lifestyle.Singleton);
            
            container.Register<SensorRangeVisitor>(Lifestyle.Singleton);
            container.Register<HyperlaneRangeVisitor>(Lifestyle.Singleton);
            container.Register<SensorVisitor>(Lifestyle.Singleton);

            container.Register<CombatComputerVisitor>(Lifestyle.Singleton);
            container.Register<ArmorVisitor>(Lifestyle.Singleton);
            container.Register<ShieldVisitor>(Lifestyle.Singleton);
            
            container.Register<AuxilaryVisitor>(Lifestyle.Singleton);

            container.Register<DamageVisitor>(Lifestyle.Singleton);
            container.Register<WeaponVisitor>(Lifestyle.Singleton);
            
            container.Register<AuraVisitor>(Lifestyle.Singleton);
            
            container.Register<UnknownVisitor>(Lifestyle.Singleton);
            
            // techs
            
            container.Register<TechModifier>(Lifestyle.Singleton);
            container.Register<TechsModifier>(Lifestyle.Singleton);
            
            container.Register<Variables>(Lifestyle.Singleton);
            container.Register<TechsList>(Lifestyle.Singleton);
            container.Register<Graph>(Lifestyle.Singleton);
            container.Register<ComponentsList>(Lifestyle.Singleton);
            
            container.Register<Parser>(Lifestyle.Singleton);
            container.Register<Mods>(Lifestyle.Singleton);
            
            container.Collection.Register<IComponentVisitor>(new List<Type>
            {
                typeof(ThrusterVisitor),
                typeof(AfterburnerVisitor),
                typeof(ReactorVisitor),
                typeof(FtlDriveVisitor),
                typeof(SensorVisitor),
                typeof(CombatComputerVisitor),
                typeof(ArmorVisitor),
                typeof(ShieldVisitor),
                typeof(WeaponVisitor),
                typeof(AuxilaryVisitor),
                typeof(UnknownVisitor),
                typeof(AuraVisitor)
            });
            
            container.Register<Localisation.Localisation>(Lifestyle.Singleton);
        }
    }
}

