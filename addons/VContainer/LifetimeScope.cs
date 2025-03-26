using System;
using System.Collections.Generic;
using Godot;
using Godot.Collections;
using VContainer.Diagnostics;

namespace VContainer.Godot
{
    public partial class LifetimeScope : Node
    {
        [Export]
        private Array<ScriptableObjectInstaller> _scriptableObjectInstallers;
        [Export]
        private Array<MonoInstaller> _monoInstallers;

        public readonly struct ExtraInstallationScope : IDisposable
        {
            public ExtraInstallationScope(IInstaller installer) => EnqueueExtra(installer);
            void IDisposable.Dispose() => RemoveExtra();
        }

        [Export]
        bool autoRun = true;

        [Export]
        protected Array<Node> autoInjectGameObjects;

        static ExtraInstaller extraInstaller;
        static readonly object SyncRoot = new object();

        static LifetimeScope Create(IInstaller installer = null)
        {
            var gameObject = new LifetimeScope() { Name = "LifetimeScope" };
            if (installer != null)
            {
                gameObject.extraInstallers.Add(installer);
            }
            return gameObject;
        }

        public static LifetimeScope Create(Action<IContainerBuilder> configuration)
            => Create(new ActionInstaller(configuration));

        public static ExtraInstallationScope Enqueue(Action<IContainerBuilder> installing)
            => new ExtraInstallationScope(new ActionInstaller(installing));

        public static ExtraInstallationScope Enqueue(IInstaller installer)
            => new ExtraInstallationScope(installer);

        static void EnqueueExtra(IInstaller installer)
        {
            lock (SyncRoot)
            {
                if (extraInstaller != null)
                    extraInstaller.Add(installer);
                else
                    extraInstaller = new ExtraInstaller { installer };
            }
        }

        static void RemoveExtra()
        {
            lock (SyncRoot)
                extraInstaller = null;
        }

        public IObjectResolver? Container { get; protected set; }
        public LifetimeScope? Parent { get; private set; }
        public bool IsRoot { get; set; }

        readonly List<IInstaller> extraInstallers = new List<IInstaller>();

        public override void _Ready()
        {
            Parent = GetRuntimeParent();
            if (autoRun)
            {
                Build();
            }
        }

        protected virtual void OnDestroy()
        {
            DisposeCore();
        }

        protected virtual void Configure(IContainerBuilder builder)
        {
            foreach (var installer in _scriptableObjectInstallers)
            {
                Parent?.Container?.Inject(installer);
                installer.Install(builder);
            }

            foreach (var installer in _monoInstallers)
            {
                Parent?.Container?.Inject(installer);
                installer.Install(builder);
            }
        }

        public override void _ExitTree()
        {
            DisposeCore();
        }

        public void DisposeCore()
        {
            Container?.Dispose();
            Container = null;
        }

        public void Build()
        {
            if (Parent == null)
                Parent = GetRuntimeParent();

            if (Parent != null)
            {
                Container = Parent.Container?.CreateScope(builder =>
                {
                    builder.ApplicationOrigin = this;
                    builder.Diagnostics = VContainerSettings.DiagnosticsEnabled ? DiagnositcsContext.GetCollector(Name) : null;
                    InstallTo(builder);
                });
            }
            else
            {
                var builder = new ContainerBuilder
                {
                    ApplicationOrigin = this,
                    Diagnostics = VContainerSettings.DiagnosticsEnabled ? DiagnositcsContext.GetCollector(Name) : null,
                };
                InstallTo(builder);
                Container = builder.Build();
            }

            extraInstallers.Clear();

            AutoInjectAll();
        }

        protected virtual void InstallTo(IContainerBuilder builder)
        {
            Configure(builder);

            foreach (var installer in extraInstallers)
            {
                installer.Install(builder);
            }

            ExtraInstaller extraInstallerStatic;
            lock (SyncRoot)
            {
                extraInstallerStatic = extraInstaller;
            }
            extraInstallerStatic?.Install(builder);

            builder.RegisterInstance<LifetimeScope>(this).AsSelf();
        }

        LifetimeScope? GetRuntimeParent()
        {
            if (IsRoot)
                return null;

            // Find root from settings
            if (VContainerSettings.Instance != null)
            {
                var rootLifetimeScope = VContainerSettings.Instance.RootLifetimeScope;
                if (rootLifetimeScope != null)
                {
                    if (rootLifetimeScope.Container == null)
                    {
                        rootLifetimeScope.Build();
                    }
                    return rootLifetimeScope;
                }
            }
            return null;
        }

        void AutoInjectAll()
        {
            foreach (var target in autoInjectGameObjects)
            {
                if (target != null) // Check missing reference
                {
                    Container.InjectGameObject(target);
                    if (target is IInitializable initializable)
                        initializable.Initialize();
                }
            }
        }
    }

    public sealed class VContainerParentTypeReferenceNotFound : Exception
    {
        public readonly Type ParentType;

        public VContainerParentTypeReferenceNotFound(Type parentType, string message)
            : base(message)
        {
            ParentType = parentType;
        }
    }
}
