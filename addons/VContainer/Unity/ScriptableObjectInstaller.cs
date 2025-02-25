using System;
using Godot;

namespace VContainer.Unity
{
	public partial class ScriptableObjectInstaller : Resource, IInstaller
	{
		public virtual void Install(IContainerBuilder builder)
		{
			throw new NotImplementedException();
		}
	}
}