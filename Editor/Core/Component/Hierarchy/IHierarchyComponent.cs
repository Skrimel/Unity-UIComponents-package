using System;

namespace Farious.Gist.UIComponents.Components
{
	public interface IHierarchyComponent : INodeComponent
	{
		IHierarchyComponent Parent { get; }
		PathToRoot PathToRoot { get; }

		bool IsActive { get; }

		event Action Activated;
		event Action Deactivated;

		void Activate();
		void Deactivate();

		void SetActivity(bool state, bool isForce);
		void MarkAsChild(IHierarchyComponent child);
	}
}