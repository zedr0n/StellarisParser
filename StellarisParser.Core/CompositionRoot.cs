using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;
using StellarisParser.Core.Components;

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
            container.Register<IStellarisVisitor<Techs>, TechsVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<Variables>, VariableVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<ModDescriptor>, DescriptorVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<Component>, ComponentVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<Thruster>, ThrusterVisitor>(Lifestyle.Singleton);
            container.Register<IStellarisVisitor<Components.Components>, ComponentsVisitor>(Lifestyle.Singleton);
            
            container.Register<AreaVisitor>(Lifestyle.Singleton);
            container.Register<TierVisitor>(Lifestyle.Singleton);
            container.Register<CostVisitor>(Lifestyle.Singleton);
            container.Register<PrereqVisitor>(Lifestyle.Singleton);
            container.Register<KeyVisitor>(Lifestyle.Singleton);
            container.Register<PowerVisitor>(Lifestyle.Singleton);
            container.Register<ModifierVisitor<EvasionVisitor>>(Lifestyle.Singleton);
            container.Register<ModifierVisitor<SpeedVisitor>>(Lifestyle.Singleton);
            container.Register<EvasionVisitor>(Lifestyle.Singleton);
            container.Register<SpeedVisitor>(Lifestyle.Singleton);
            container.Register<ComponentSetVisitor>(Lifestyle.Singleton);
            container.Register<ThrusterVisitor>(Lifestyle.Singleton);
            
            container.Register<Variables>(Lifestyle.Singleton);
            container.Register<Techs>(Lifestyle.Singleton);
            container.Register<Graph>(Lifestyle.Singleton);
            container.Register<Components.Components>(Lifestyle.Singleton);
            
            container.Register<Parser>(Lifestyle.Singleton);
            container.Register<Mods>(Lifestyle.Singleton);
        }
    }
}

