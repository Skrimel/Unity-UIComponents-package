using System;
using System.Collections.Generic;

namespace Farious.Gist.UIComponents.Components
{
	public class HierarchyComponent : NodeComponent, IHierarchyComponent
	{
		private List<IHierarchyComponent> _children = new List<IHierarchyComponent>();
		public IEnumerable<IHierarchyComponent> Children => _children;

		public IHierarchyComponent Parent { get; private set; } = null;

		public PathToRoot PathToRoot => new PathToRoot(this);

		public bool IsActive { get; private set; } = false;
		public event Action Activated;
		public event Action Deactivated;

		public void SetParent(IHierarchyComponent parent)
		{
			Parent = parent;
			Parent.MarkAsChild(this);
		}

		public void MarkAsChild(IHierarchyComponent hierarchyComponent)
		{
			_children.Add(hierarchyComponent);
		}

		public void SetActivity(bool state, bool isForce)
		{
			if (IsActive == state && !isForce)
				throw new ArgumentException($"State is already set to {state}");

			IsActive = state;

			foreach (var child in _children)
				if (child.IsActive != IsActive)
					child.SetActivity(state, false);

			if (state)
				Activated?.Invoke();
			else
				Deactivated?.Invoke();
		}

		public void Activate() =>
			SetActivity(true, true);

		public void Deactivate() =>
			SetActivity(false, true);
	}
}