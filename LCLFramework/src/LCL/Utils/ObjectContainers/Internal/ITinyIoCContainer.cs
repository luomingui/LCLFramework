using System;
using System.Collections.Generic;
using System.Reflection;
using TinyIoC;
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
        void BuildUp(object input,  ResolveOptions resolveOptions);
        bool CanResolve(Type resolveType);
        bool CanResolve(Type resolveType, string name,  NamedParameterOverloads parameters);
        bool CanResolve(Type resolveType, string name,  NamedParameterOverloads parameters,  ResolveOptions options);
        bool CanResolve(Type resolveType, string name,  ResolveOptions options);
        bool CanResolve(Type resolveType,  NamedParameterOverloads parameters);
        bool CanResolve(Type resolveType,  NamedParameterOverloads parameters,  ResolveOptions options);
        bool CanResolve(Type resolveType,  ResolveOptions options);
        bool CanResolve<ResolveType>() where ResolveType : class;
        bool CanResolve<ResolveType>(string name) where ResolveType : class;
        bool CanResolve<ResolveType>(string name,  NamedParameterOverloads parameters) where ResolveType : class;
        bool CanResolve<ResolveType>(string name,  NamedParameterOverloads parameters,  ResolveOptions options) where ResolveType : class;
        bool CanResolve<ResolveType>(string name,  ResolveOptions options) where ResolveType : class;
        bool CanResolve<ResolveType>( NamedParameterOverloads parameters) where ResolveType : class;
        bool CanResolve<ResolveType>( NamedParameterOverloads parameters,  ResolveOptions options) where ResolveType : class;
        bool CanResolve<ResolveType>( ResolveOptions options) where ResolveType : class;
        void Dispose();
         TinyIoCContainer GetChildContainer();
         TinyIoCContainer.RegisterOptions Register(Type registerImplementation);
         TinyIoCContainer.RegisterOptions Register(Type registerImplementation, object instance);
         TinyIoCContainer.RegisterOptions Register(Type registerImplementation, object instance, string name);
         TinyIoCContainer.RegisterOptions Register(Type registerImplementation, string name);
         TinyIoCContainer.RegisterOptions Register(Type registerType, Type implementationType, object instance);
         TinyIoCContainer.RegisterOptions Register(Type registerType, Type implementationType, object instance, string name);
         TinyIoCContainer.RegisterOptions Register(Type registerType, Type registerImplementation);
         TinyIoCContainer.RegisterOptions Register(Type registerType, Type registerImplementation, string name);
         TinyIoCContainer.RegisterOptions Register<RegisterType, RegisterImplementation>()
            where RegisterType : class
            where RegisterImplementation : class, RegisterType;
         TinyIoCContainer.RegisterOptions Register<RegisterType, RegisterImplementation>(RegisterImplementation instance)
            where RegisterType : class
            where RegisterImplementation : class, RegisterType;
         TinyIoCContainer.RegisterOptions Register<RegisterType, RegisterImplementation>(RegisterImplementation instance, string name)
            where RegisterType : class
            where RegisterImplementation : class, RegisterType;
         TinyIoCContainer.RegisterOptions Register<RegisterType, RegisterImplementation>(string name)
            where RegisterType : class
            where RegisterImplementation : class, RegisterType;
         TinyIoCContainer.RegisterOptions Register<RegisterType>() where RegisterType : class;
         TinyIoCContainer.RegisterOptions Register<RegisterType>(RegisterType instance) where RegisterType : class;
         TinyIoCContainer.RegisterOptions Register<RegisterType>(RegisterType instance, string name) where RegisterType : class;
         TinyIoCContainer.RegisterOptions Register<RegisterType>(Func< TinyIoCContainer,  NamedParameterOverloads, RegisterType> factory) where RegisterType : class;
         TinyIoCContainer.RegisterOptions Register<RegisterType>(Func< TinyIoCContainer,  NamedParameterOverloads, RegisterType> factory, string name) where RegisterType : class;
         TinyIoCContainer.RegisterOptions Register<RegisterType>(string name) where RegisterType : class;
         TinyIoCContainer.MultiRegisterOptions RegisterMultiple(Type registrationType, IEnumerable<Type> implementationTypes);
         TinyIoCContainer.MultiRegisterOptions RegisterMultiple<RegisterType>(IEnumerable<Type> implementationTypes);
        object Resolve(Type resolveType);
        object Resolve(Type resolveType, string name);
        object Resolve(Type resolveType, string name,  NamedParameterOverloads parameters);
        object Resolve(Type resolveType, string name,  NamedParameterOverloads parameters,  ResolveOptions options);
        object Resolve(Type resolveType, string name,  ResolveOptions options);
        object Resolve(Type resolveType,  NamedParameterOverloads parameters);
        object Resolve(Type resolveType,  NamedParameterOverloads parameters,  ResolveOptions options);
        object Resolve(Type resolveType,  ResolveOptions options);
        ResolveType Resolve<ResolveType>() where ResolveType : class;
        ResolveType Resolve<ResolveType>(string name) where ResolveType : class;
        ResolveType Resolve<ResolveType>(string name,  NamedParameterOverloads parameters) where ResolveType : class;
        ResolveType Resolve<ResolveType>(string name,  NamedParameterOverloads parameters,  ResolveOptions options) where ResolveType : class;
        ResolveType Resolve<ResolveType>(string name,  ResolveOptions options) where ResolveType : class;
        ResolveType Resolve<ResolveType>( NamedParameterOverloads parameters) where ResolveType : class;
        ResolveType Resolve<ResolveType>( NamedParameterOverloads parameters,  ResolveOptions options) where ResolveType : class;
        ResolveType Resolve<ResolveType>( ResolveOptions options) where ResolveType : class;
        IEnumerable<object> ResolveAll(Type resolveType);
        IEnumerable<object> ResolveAll(Type resolveType, bool includeUnnamed);
        IEnumerable<ResolveType> ResolveAll<ResolveType>() where ResolveType : class;
        IEnumerable<ResolveType> ResolveAll<ResolveType>(bool includeUnnamed) where ResolveType : class;
        bool TryResolve(Type resolveType, out object resolvedType);
        bool TryResolve(Type resolveType, string name, out object resolvedType);
        bool TryResolve(Type resolveType, string name,  NamedParameterOverloads parameters, out object resolvedType);
        bool TryResolve(Type resolveType, string name,  NamedParameterOverloads parameters,  ResolveOptions options, out object resolvedType);
        bool TryResolve(Type resolveType, string name,  ResolveOptions options, out object resolvedType);
        bool TryResolve(Type resolveType,  NamedParameterOverloads parameters, out object resolvedType);
        bool TryResolve(Type resolveType,  NamedParameterOverloads parameters,  ResolveOptions options, out object resolvedType);
        bool TryResolve(Type resolveType,  ResolveOptions options, out object resolvedType);
        bool TryResolve<ResolveType>(out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>(string name, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>(string name,  NamedParameterOverloads parameters, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>(string name,  NamedParameterOverloads parameters,  ResolveOptions options, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>(string name,  ResolveOptions options, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>( NamedParameterOverloads parameters, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>( NamedParameterOverloads parameters,  ResolveOptions options, out ResolveType resolvedType) where ResolveType : class;
        bool TryResolve<ResolveType>( ResolveOptions options, out ResolveType resolvedType) where ResolveType : class;
    }
}
