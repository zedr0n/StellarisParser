using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;
using StellarisParser.Core.Components;
using StellarisParser.Core.Components.Afterburners;
using StellarisParser.Core.Components.Drives;
using StellarisParser.Core.Components.Reactors;
using StellarisParser.Core.Components.Thrusters;
using StellarisParser.Core.Techs;

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
            
            container.Register<AreaVisitor>(Lifestyle.Singleton);
            container.Register<TierVisitor>(Lifestyle.Singleton);
            container.Register<CostVisitor>(Lifestyle.Singleton);
            container.Register<PrereqVisitor>(Lifestyle.Singleton);
            container.Register<KeyVisitor>(Lifestyle.Singleton);
            container.Register<PowerVisitor>(Lifestyle.Singleton);
            container.Register<UpgradesToVisitor>(Lifestyle.Singleton);
            container.Register<ModifierVisitor<EvasionVisitor>>(Lifestyle.Singleton);
            container.Register<ModifierVisitor<BaseSpeedMultiplierVisitor>>(Lifestyle.Singleton);
            container.Register<EvasionVisitor>(Lifestyle.Singleton);
            container.Register<BaseSpeedMultiplierVisitor>(Lifestyle.Singleton);
            container.Register<ComponentSetVisitor>(Lifestyle.Singleton);
            container.Register<ThrusterVisitor>(Lifestyle.Singleton);
            container.Register<ReactorVisitor>(Lifestyle.Singleton);
            
            container.Register<ShipWindupVisitor>(Lifestyle.Singleton);
            container.Register<JumpDriveRangeMultiplierVisitor>(Lifestyle.Singleton);
            container.Register<ModifierVisitor<JumpDriveRangeMultiplierVisitor>>(Lifestyle.Singleton);
            container.Register<ModifierVisitor<ShipWindupVisitor>>(Lifestyle.Singleton);
            container.Register<JumpDriveVisitor>(Lifestyle.Singleton);
            container.Register<FtlDriveVisitor>(Lifestyle.Singleton);
            
            container.Register<EvasionMultiplierVisitor>(Lifestyle.Singleton);
            container.Register<ModifierVisitor<EvasionMultiplierVisitor>>(Lifestyle.Singleton);
            container.Register<SpeedMultiplierVisitor>(Lifestyle.Singleton);
            container.Register<ModifierVisitor<SpeedMultiplierVisitor>>(Lifestyle.Singleton);
            container.Register<AfterburnerVisitor>(Lifestyle.Singleton);
            
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
                typeof(FtlDriveVisitor)
            });
        }
    }
}

