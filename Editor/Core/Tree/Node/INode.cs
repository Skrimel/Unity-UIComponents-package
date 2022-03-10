using System;
using System.Collections.Generic;
using Farious.Gist.UIComponents.Components;

namespace Farious.Gist.UIComponents.Tree
{
	public partial interface INode : IEntity, IDisposable
	{
		bool IsVisible { get; }

		event Action Destroying;

		event Action Shown;
		event Action Hidden;

		event Action Activated;
		event Action Deactivated;

		bool HasInParents<TComponent>(bool includesSelf)
			where TComponent : class, INodeComponent;

		bool HasInParents(Type type, bool includesSelf);

		TComponent FindInParents<TComponent>(bool includeSelf)
			where TComponent : class, INodeComponent;

		IEnumerable<TComponent> GetAllInParents<TComponent>(bool includeSelf)
			where TComponent : class, INodeComponent;

		void Show();
		void Hide();

		void Activate();
		void Deactivate();

		void AttachToHierarchy(HierarchyComponent component);

		TNode CreateChild<TNode>() where TNode : class, INode, new();
	}
}
