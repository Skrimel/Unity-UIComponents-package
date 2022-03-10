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

		TComponent Create<TComponent>() where TComponent : class, INodeComponent, new();
		void Attach<TComponent>(TComponent component) where TComponent : class, INodeComponent;
		TComponent Remove<TComponent>() where TComponent : class, INodeComponent;

		bool Has<TComponent>() where TComponent : class, INodeComponent;
		bool Has(Type type);
		bool Contains(INodeComponent component);

		TComponent Get<TComponent>() where TComponent : class, INodeComponent;
		INodeComponent Find(Predicate<INodeComponent> finder);
		TComponent Find<TComponent>(Predicate<TComponent> finder) where TComponent : class, INodeComponent;

		IEnumerable<TComponent> GetAll<TComponent>() where TComponent : class, INodeComponent;
	}
}
