using System;
using System.Collections.Generic;
using Farious.Gist.UIComponents.Components;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Tree
{
	public abstract class Entity : VisualElement, IEntity
	{
		public string BaseClassName => "gist-";

		protected Dictionary<Type, INodeComponent> _components = new();
		public IEnumerable<INodeComponent> Components => _components.Values;

		public VisualElement View => this;

		public abstract TComponent Create<TComponent>()
			where TComponent : class, INodeComponent, new();

		public abstract void Attach<TComponent>(TComponent component)
			where TComponent : class, INodeComponent;

		public abstract TComponent Remove<TComponent>()
			where TComponent : class, INodeComponent;

		public abstract void Remove(NodeComponent component);

		public bool Has<TComponent>() where TComponent : class, INodeComponent =>
			_components.ContainsKey(typeof(TComponent));

		public bool Has(Type type) =>
			_components.ContainsKey(type);

		public bool Contains(INodeComponent component) =>
			_components.ContainsValue(component);

		public TComponent Get<TComponent>()
			where TComponent : class, INodeComponent
		{
			return (TComponent) _components[typeof(TComponent)];
		}

		public INodeComponent Find(Predicate<INodeComponent> finder)
		{
			INodeComponent result = default;

			foreach (var component in Components)
				if (finder(component))
				{
					result = component;
					break;
				}

			if (result == null)
				throw new NullReferenceException("Unable to find component");

			return result;
		}

		public TComponent Find<TComponent>(Predicate<TComponent> finder)
			where TComponent : class, INodeComponent
		{
			TComponent result = default;

			foreach (var component in Components)
				if (component is TComponent)
					if (finder(component as TComponent))
					{
						result = component as TComponent;
						break;
					}

			if (result == null)
				throw new NullReferenceException("Unable to find component");

			return result;
		}

		public IEnumerable<TComponent> GetAll<TComponent>()
			where TComponent : class, INodeComponent
		{
			List<TComponent> result = new List<TComponent>();

			foreach (var component in Components)
				if (component is TComponent)
				{
					result.Add(component as TComponent);
					break;
				}

			return result;
		}
	}
}
