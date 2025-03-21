using System;
using System.Runtime.CompilerServices;
using Godot;
using VContainer.Internal;

namespace VContainer
{
    public static class ContainerBuilderExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterFromComponent<T>(
            this IContainerBuilder builder,
            Node gameObject,
            Lifetime lifetime)
            => builder.Register(new FromComponentOnRegistrationBuilder(gameObject, typeof(T), lifetime));
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder Register(
            this IContainerBuilder builder,
            Type type,
            Lifetime lifetime)
            => builder.Register(new RegistrationBuilder(type, lifetime));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder Register<T>(
            this IContainerBuilder builder,
            Lifetime lifetime)
            => builder.Register(typeof(T), lifetime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder Register<TInterface, TImplement>(
            this IContainerBuilder builder,
            Lifetime lifetime)
            where TImplement : TInterface
            => builder.Register<TImplement>(lifetime).As<TInterface>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder Register<TInterface1, TInterface2, TImplement>(
            this IContainerBuilder builder,
            Lifetime lifetime)
            where TImplement : TInterface1, TInterface2
            => builder.Register<TImplement>(lifetime).As(typeof(TInterface1), typeof(TInterface2));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder Register<TInterface1, TInterface2, TInterface3, TImplement>(
            this IContainerBuilder builder,
            Lifetime lifetime)
            where TImplement : TInterface1, TInterface2, TInterface3
            => builder.Register<TImplement>(lifetime).As(typeof(TInterface1), typeof(TInterface2), typeof(TInterface3));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder Register<TInterface>(
            this IContainerBuilder builder,
            Func<IObjectResolver, TInterface> implementationConfiguration,
            Lifetime lifetime)
            where TInterface : class
            => builder.Register(new FuncRegistrationBuilder(implementationConfiguration, typeof(TInterface), lifetime));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterInstance<TInterface>(
            this IContainerBuilder builder,
            TInterface instance)
            => builder.Register(new InstanceRegistrationBuilder(instance)).As(typeof(TInterface));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterInstance<TInterface1, TInterface2>(
            this IContainerBuilder builder,
            TInterface1 instance)
            => builder.RegisterInstance(instance).As(typeof(TInterface1), typeof(TInterface2));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterInstance<TInterface1, TInterface2, TInterface3>(
            this IContainerBuilder builder,
            TInterface1 instance)
            => builder.RegisterInstance(instance).As(typeof(TInterface1), typeof(TInterface2), typeof(TInterface3));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterFactory<T>(
            this IContainerBuilder builder,
            Func<T> factory)
            => builder.RegisterInstance(factory);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterFactory<TParam1, T>(
            this IContainerBuilder builder,
            Func<TParam1, T> factory)
            => builder.RegisterInstance(factory);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterFactory<TParam1, TParam2, T>(
            this IContainerBuilder builder,
            Func<TParam1, TParam2, T> factory)
            => builder.RegisterInstance(factory);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterFactory<TParam1, TParam2, TParam3, T>(
            this IContainerBuilder builder,
            Func<TParam1, TParam2, TParam3, T> factory)
            => builder.RegisterInstance(factory);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterFactory<TParam1, TParam2, TParam3, TParam4, T>(
            this IContainerBuilder builder,
            Func<TParam1, TParam2, TParam3, TParam4, T> factory)
            => builder.RegisterInstance(factory);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterFactory<T>(
            this IContainerBuilder builder,
            Func<IObjectResolver, Func<T>> factoryFactory,
            Lifetime lifetime)
            => builder.Register(new FuncRegistrationBuilder(factoryFactory, typeof(Func<T>), lifetime));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterFactory<TParam1, T>(
            this IContainerBuilder builder,
            Func<IObjectResolver, Func<TParam1, T>> factoryFactory,
            Lifetime lifetime)
            => builder.Register(new FuncRegistrationBuilder(factoryFactory, typeof(Func<TParam1, T>), lifetime));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterFactory<TParam1, TParam2, T>(
            this IContainerBuilder builder,
            Func<IObjectResolver, Func<TParam1, TParam2, T>> factoryFactory,
            Lifetime lifetime)
            => builder.Register(new FuncRegistrationBuilder(factoryFactory, typeof(Func<TParam1, TParam2, T>), lifetime));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterFactory<TParam1, TParam2, TParam3, T>(
            this IContainerBuilder builder,
            Func<IObjectResolver, Func<TParam1, TParam2, TParam3, T>> factoryFactory,
            Lifetime lifetime)
            => builder.Register(new FuncRegistrationBuilder(factoryFactory, typeof(Func<TParam1, TParam2, TParam3, T>), lifetime));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IRegistrationBuilder RegisterFactory<TParam1, TParam2, TParam3, TParam4, T>(
            this IContainerBuilder builder,
            Func<IObjectResolver, Func<TParam1, TParam2, TParam3, TParam4, T>> factoryFactory,
            Lifetime lifetime)
            => builder.Register(new FuncRegistrationBuilder(factoryFactory, typeof(Func<TParam1, TParam2, TParam3, TParam4, T>), lifetime));

        [Obsolete("IObjectResolver is registered by default. This method does nothing.")]
        public static void RegisterContainer(this IContainerBuilder builder)
        {
        }
    }
}
