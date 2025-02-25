using Godot;

namespace VContainer.Unity
{
    [GlobalClass]
    [Tool]
    public sealed partial class VContainerSettings : Resource
    {
        public static VContainerSettings? Instance { get; internal set; }

        public static bool DiagnosticsEnabled => Instance != null && Instance.EnableDiagnostics;

        public LifetimeScope? RootLifetimeScope { set; get; }

        [Export]
        public bool EnableDiagnostics;

        [Export]
        public bool DisableScriptModifier;
    }
}
