using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;

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
            container.Register<StellarisVisitor<Techs>, TechsVisitor>(Lifestyle.Singleton);
            container.Register<StellarisVisitor<Variables>, VariableVisitor>(Lifestyle.Singleton);
            container.Register<AreaVisitor>(Lifestyle.Singleton);
            container.Register<TierVisitor>(Lifestyle.Singleton);
            container.Register<CostVisitor>(Lifestyle.Singleton);
            container.Register<PrereqVisitor>(Lifestyle.Singleton);
            container.Register<Variables>(Lifestyle.Singleton);
            container.Register<Techs>(Lifestyle.Singleton);
            container.Register<Graph>(Lifestyle.Singleton);

            container.Register<Parser>();
        }
    }
}
