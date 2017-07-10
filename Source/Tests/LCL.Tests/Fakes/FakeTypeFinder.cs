using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LCL.Infrastructure;

namespace LCL.Tests
{
    public class FakeTypeFinder : ITypeFinder
    {
        public Assembly[] Assemblies { get; set; }
        public Type[] Types { get; set; }

        public FakeTypeFinder(Assembly assembly, params Type[] types)
        {
            Assemblies = new[] { assembly };
            this.Types = types;
        }
        public FakeTypeFinder(params Type[] types)
        {
            Assemblies = new Assembly[0];
            this.Types = types;
        }
        public FakeTypeFinder(params Assembly[] assemblies)
        {
            this.Assemblies = assemblies;
        }

        public IList<Assembly> GetAssemblies()
        {
            return Assemblies.ToList();
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true)
        {
            return (from t in Types
                    where !t.IsInterface && assignTypeFrom.IsAssignableFrom(t) && (onlyConcreteClasses ? (t.IsClass && !t.IsAbstract) : true)
                    select t).ToList();
        }

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(assignTypeFrom, onlyConcreteClasses);
        }

        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            return FindClassesOfType(typeof(T), onlyConcreteClasses);
        }

        public Type[] Find(Func<Type, bool> predicate)
        {
            var result = new List<Type>();
            var assemblies = GetAssemblies();
            foreach (var a in assemblies)
            {
                Type[] types = null;
                try
                {
                    types = a.GetTypes();
                }
                catch
                {
                }
                if (types != null)
                {
                    result.AddRange(types.Where(predicate));
                }
            }

            return result.ToArray();

        }
    }
}
