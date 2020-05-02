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
            /*container.Collection.Register<IstellarisListener>(new []
            {
                typeof(TechListener)
            });*/
            
            /*container.Collection.Register(typeof(StellarisVisitor<>), new Type[]
            {
                typeof(AreaVisitor)
            });*/
            
            //container.Register<TechVisitor>(Lifestyle.Singleton);

            /*foreach (var t in Assembly.GetExecutingAssembly().GetTypesFromInterface(typeof(ISpecVisitor)))
            {
                var reg= Lifestyle.Singleton.CreateRegistration(t, container);
                container.Collection.Append(typeof(ISpecVisitor), reg);
            }*/
            
            // var reg= Lifestyle.Singleton.CreateRegistration(t, container);
            // container.AddRegistration(reg);
            
            container.Register<StellarisVisitor<Tech>, TechVisitor>(Lifestyle.Singleton);
            container.Register<AreaVisitor>(Lifestyle.Singleton);
            container.Register<TierVisitor>(Lifestyle.Singleton);
            // container.Register<StellarisVisitor<string>, AreaVisitor>(Lifestyle.Singleton);
            // container.Register<AreaVisitor>(Lifestyle.Singleton);
            
            /*container.Collection.Register<IstellarisVisitor<Tech>>(new []
            {
                typeof(TechListener)
            });*/

            
            container.Register<Parser>();
        }
    }
}
