using System;
using System.Linq;
using System.Reflection;
using SimpleInjector.Advanced;

namespace Pg.LetsMeet.Dataverse.Shared.Injections
{
    public class DependencyInjectionPropertySelectionBehavior : IPropertySelectionBehavior
    {
        public bool SelectProperty(Type type, PropertyInfo prop)
        {
            return prop.GetCustomAttributes(typeof(DependencyInjectionAttribute)).Any();
        }
    }
}
