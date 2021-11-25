using System;
using System.Collections.Generic;
using Farious.Gist.UIComponents.Components;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Tree
{
	public abstract class Entity : VisualElement, IEntity
	{
		public string BaseClassName => "gist-";

		protected HashSet<INodeComponent> _components = new HashSet<INodeComponent>();
		public IEnumerable<INodeComponent> Components => _components;

		public VisualElement View => this;

		public abstract TComponent AddComponent<TComponent>()
			where TComponent : class, INodeComponent, new();

		public abstract void AddComponent<TComponent>(TComponent component)
			where TComponent : class, INodeComponent;

		public abstract TComponent RemoveComponent<TComponent>()
			where TComponent : class, INodeComponent;

		public abstract void RemoveComponent(NodeComponent component);

		public bool HasComponent<TComponent>() where TComponent : class, INodeComponent
		{
			bool result = false;

			foreach (var comp in Components)
				result |= (comp as TComponent) != null;

			return result;
		}

		public bool HasComponent(Type type)
		{
			bool result = false;

			foreach (var comp in Components)
				result |= type.IsAssignableFrom(comp.GetType());

			return result;
		}

		public bool ContainsComponent(INodeComponent component) =>
			_components.Contains(component);

		public TComponent FindComponent<TComponent>()
			where TComponent : class, INodeComponent
		{
			TComponent result = default;

			foreach (var component in Components)
				if (component is TComponent)
				{
					result = component as TComponent;
					break;
				}

			if (result == null)
				throw new NullReferenceException("Unable to find component");

			return result;
		}

		public INodeComponent FindComponent(Predicate<INodeComponent> finder)
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

		public TComponent FindComponent<TComponent>(Predicate<TComponent> finder)
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

		public IEnumerable<TComponent> GetComponents<TComponent>()
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