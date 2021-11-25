using System;
using System.Collections.Generic;
using Farious.Gist.UIComponents.Components;

namespace Farious.Gist.UIComponents.Tree
{
	public interface INode : IEntity, IDisposable
	{
		INode ParentInHierarchy { get; }

		event Action Shown;
		event Action Hidden;

		event Action Activated;
		event Action Deactivated;

		event Action AttachedToHierarchy;

		bool HasComponentInParents<TComponent>(bool includesSelf)
			where TComponent : class, INodeComponent;

		bool HasComponentInParents(Type type, bool includesSelf);

		TComponent FindComponentInParents<TComponent>(bool includeSelf)
			where TComponent : class, INodeComponent;

		IEnumerable<TComponent> GetComponentsInParents<TComponent>(bool includeSelf)
			where TComponent : class, INodeComponent;

		void Show();
		void Hide();

		void Activate();
		void Deactivate();

		void AttachToHierarchy(HierarchyComponent component);

		TNode CreateChild<TNode>() where TNode : class, INode, new();
	}
}