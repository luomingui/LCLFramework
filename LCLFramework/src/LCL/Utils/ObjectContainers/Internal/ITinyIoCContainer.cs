using System;
using System.Collections.Generic;
using System.Reflection;
namespace LCL.ObjectContainers.TinyIoC
{
    public interface ITinyIoCContainer
    {
        void AutoRegister();
        void AutoRegister(bool ignoreDuplicateImplementations);
        void AutoRegister(bool ignoreDuplicateImplementations, Func<Type, bool> registrationPredicate);
        void AutoRegister(IEnumerable<Assembly> assemblies);
        void AutoRegister(IEnumerable<Assembly> assemblies, bool ignoreDuplicateImplementations);
        void AutoRegister(IEnumerable<Assembly> assemblies, bool ignoreDuplicateImplementations, Func<Type, bool> registrationPredicate);
        void AutoRegister(IEnumerable<Assembly> assemblies, Func<Type, bool> registrationPredicate);
        void AutoRegister(Func<Type, bool> registrationPredicate);
        void BuildUp(object input);
        void BuildUp(object input, global::TinyIoC.ResolveOptions resolveOptions);
        bool CanResolve(Type resolveType);
        bool CanResolve(Type resolveType, string name, global::TinyIoC.NamedParameterOverloads parameters);
        bool CanResolve(Type resolveType, string name, global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options);
        bool CanResolve(Type resolveType, string name, global::TinyIoC.ResolveOptions options);
        bool CanResolve(Type resolveType, global::TinyIoC.NamedParameterOverloads parameters);
        bool CanResolve(Type resolveType, global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options);
        bool CanResolve(Type resolveType, global::TinyIoC.ResolveOptions options);
        bool CanResolve<ResolveType>() where ResolveType : class;
        bool CanResolve<ResolveType>(string name) where ResolveType : class;
        bool CanResolve<ResolveType>(string name, global::TinyIoC.NamedParameterOverloads parameters) where ResolveType : class;
        bool CanResolve<ResolveType>(string name, global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options) where ResolveType : class;
        bool CanResolve<ResolveType>(string name, global::TinyIoC.ResolveOptions options) where ResolveType : class;
        bool CanResolve<ResolveType>(global::TinyIoC.NamedParameterOverloads parameters) where ResolveType : class;
        bool CanResolve<ResolveType>(global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options) where ResolveType : class;
        bool CanResolve<ResolveType>(global::TinyIoC.ResolveOptions options) where ResolveType : class;
        void Dispose();
        global::TinyIoC.TinyIoCContainer GetChildContainer();
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register(Type registerImplementation);
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register(Type registerImplementation, object instance);
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register(Type registerImplementation, object instance, string name);
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register(Type registerImplementation, string name);
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register(Type registerType, Type implementationType, object instance);
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register(Type registerType, Type implementationType, object instance, string name);
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register(Type registerType, Type registerImplementation);
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register(Type registerType, Type registerImplementation, string name);
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register<RegisterType, RegisterImplementation>()
            where RegisterType : class
            where RegisterImplementation : class, RegisterType;
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register<RegisterType, RegisterImplementation>(RegisterImplementation instance)
            where RegisterType : class
            where RegisterImplementation : class, RegisterType;
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register<RegisterType, RegisterImplementation>(RegisterImplementation instance, string name)
            where RegisterType : class
            where RegisterImplementation : class, RegisterType;
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register<RegisterType, RegisterImplementation>(string name)
            where RegisterType : class
            where RegisterImplementation : class, RegisterType;
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register<RegisterType>() where RegisterType : class;
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register<RegisterType>(RegisterType instance) where RegisterType : class;
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register<RegisterType>(RegisterType instance, string name) where RegisterType : class;
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register<RegisterType>(Func<global::TinyIoC.TinyIoCContainer, global::TinyIoC.NamedParameterOverloads, RegisterType> factory) where RegisterType : class;
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register<RegisterType>(Func<global::TinyIoC.TinyIoCContainer, global::TinyIoC.NamedParameterOverloads, RegisterType> factory, string name) where RegisterType : class;
        global::TinyIoC.TinyIoCContainer.RegisterOptions Register<RegisterType>(string name) where RegisterType : class;
        global::TinyIoC.TinyIoCContainer.MultiRegisterOptions RegisterMultiple(Type registrationType, IEnumerable<Type> implementationTypes);
        global::TinyIoC.TinyIoCContainer.MultiRegisterOptions RegisterMultiple<RegisterType>(IEnumerable<Type> implementationTypes);
        object Resolve(Type resolveType);
        object Resolve(Type resolveType, string name);
        object Resolve(Type resolveType, string name, global::TinyIoC.NamedParameterOverloads parameters);
        object Resolve(Type resolveType, string name, global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options);
        object Resolve(Type resolveType, string name, global::TinyIoC.ResolveOptions options);
        object Resolve(Type resolveType, global::TinyIoC.NamedParameterOverloads parameters);
        object Resolve(Type resolveType, global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options);
        object Resolve(Type resolveType, global::TinyIoC.ResolveOptions options);
        ResolveType Resolve<ResolveType>() where ResolveType : class;
        ResolveType Resolve<ResolveType>(string name) where ResolveType : class;
        ResolveType Resolve<ResolveType>(string name, global::TinyIoC.NamedParameterOverloads parameters) where ResolveType : class;
        ResolveType Resolve<ResolveType>(string name, global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options) where ResolveType : class;
        ResolveType Resolve<ResolveType>(string name, global::TinyIoC.ResolveOptions options) where ResolveType : class;
        ResolveType Resolve<ResolveType>(global::TinyIoC.NamedParameterOverloads parameters) where ResolveType : class;
        ResolveType Resolve<ResolveType>(global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options) where ResolveType : class;
        ResolveType Resolve<ResolveType>(global::TinyIoC.ResolveOptions options) where ResolveType : class;
        IEnumerable<object> ResolveAll(Type resolveType);
        IEnumerable<object> ResolveAll(Type resolveType, bool includeUnnamed);
        IEnumerable<ResolveType> ResolveAll<ResolveType>() where ResolveType : class;
        IEnumerable<ResolveType> ResolveAll<ResolveType>(bool includeUnnamed) where ResolveType : class;
        bool TryResolve(Type resolveType, out object resolvedType);
        bool TryResolve(Type resolveType, string name, out object resolvedType);
        bool TryResolve(Type resolveType, string name, global::TinyIoC.NamedParameterOverloads parameters, out object resolvedType);
        bool TryResolve(Type resolveType, string name, global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options, out object resolvedType);
        bool TryResolve(Type resolveType, string name, global::TinyIoC.ResolveOptions options, out object resolvedType);
        bool TryResolve(Type resolveType, global::TinyIoC.NamedParameterOverloads parameters, out object resolvedType);
        bool TryResolve(Type resolveType, global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options, out object resolvedType);
        bool TryResolve(Type resolveType, global::TinyIoC.ResolveOptions options, out object resolvedType);
        bool TryResolve<ResolveType>(out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>(string name, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>(string name, global::TinyIoC.NamedParameterOverloads parameters, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>(string name, global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>(string name, global::TinyIoC.ResolveOptions options, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>(global::TinyIoC.NamedParameterOverloads parameters, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>(global::TinyIoC.NamedParameterOverloads parameters, global::TinyIoC.ResolveOptions options, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>(global::TinyIoC.ResolveOptions options, out ResolveType resolvedType) where ResolveType : class;
    }
}
