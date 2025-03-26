using Godot;
using VContainer.Diagnostics;

namespace VContainer.Godot
{
    public partial class RootLifetimeScope : LifetimeScope
    {
        [Export] private PackedScene _sceneAfter;
        [Export] private VContainerSettings _settings;

        public override void _Ready()
        {
            InternalBuild();
            VContainerSettings.Instance = _settings;
            VContainerSettings.Instance.RootLifetimeScope = this;
            if (_sceneAfter != null)
            {
                GD.Print("SCENE START");
                GetTree().ChangeSceneToPacked(_sceneAfter);
            }
        }

        private void InternalBuild()
        {
            var builder = new ContainerBuilder
            {
                ApplicationOrigin = this,
                Diagnostics = VContainerSettings.DiagnosticsEnabled ? DiagnositcsContext.GetCollector(Name) : null,
            };
            InstallTo(builder);
            Container = builder.Build();
        }

        // void OnApplicationQuit()
        // {
        //     if (Container != null)
        //     {
        //         // Execute Dispose once at the slowest possible time.
        //         // However, the GameObject may be destroyed at that time.
        //         PlayerLoopHelper.Dispatch(PlayerLoopTiming.LateUpdate, new DisposeLoopItem(Container));
        //     }
        // }
    }
}
