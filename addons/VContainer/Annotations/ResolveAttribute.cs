using System;

namespace VContainer
{
#if UNITY_2018_4_OR_NEWER
    [JetBrains.Annotations.MeansImplicitUse(JetBrains.Annotations.ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
#endif
    [AttributeUsage( AttributeTargets.Property | AttributeTargets.Field)]
    public class ResolveAttribute : PreserveAttribute
    {
    }
}