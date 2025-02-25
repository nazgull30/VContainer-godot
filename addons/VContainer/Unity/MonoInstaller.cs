using System;
using Godot;

namespace VContainer.Unity
{
	public partial class MonoInstaller : Node, IInstaller
	{
		public virtual void Install(IContainerBuilder builder)
		{
			throw new NotImplementedException();
		}
	}
}