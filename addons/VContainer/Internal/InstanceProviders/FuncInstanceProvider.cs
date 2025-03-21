using System;
using System.Runtime.CompilerServices;

namespace VContainer.Internal
{
    internal readonly struct FuncInstanceProvider : IInstanceProvider
    {
        readonly Func<IObjectResolver, object> implementationProvider;

        public FuncInstanceProvider(Func<IObjectResolver, object> implementationProvider)
        {
            this.implementationProvider = implementationProvider;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object SpawnInstance(IObjectResolver resolver) => implementationProvider(resolver);
    }
}
