using System;
using System.Collections.Generic;
using Farious.Gist.UIComponents.Components;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Tree
{
	public interface IEntity
	{
		IEnumerable<INodeComponent> Components { get; }

		VisualElement View { get; }

		TComponent AddComponent<TComponent>() where TComponent : class, INodeComponent, new();
		void AddComponent<TComponent>(TComponent component) where TComponent : class, INodeComponent;
		TComponent RemoveComponent<TComponent>() where TComponent : class, INodeComponent;

		bool HasComponent<TComponent>() where TComponent : class, INodeComponent;
		bool HasComponent(Type type);
		bool ContainsComponent(INodeComponent component);

		TComponent FindComponent<TComponent>() where TComponent : class, INodeComponent;
		INodeComponent FindComponent(Predicate<INodeComponent> finder);
		TComponent FindComponent<TComponent>(Predicate<TComponent> finder) where TComponent : class, INodeComponent;

		IEnumerable<TComponent> GetComponents<TComponent>() where TComponent : class, INodeComponent;
	}
}