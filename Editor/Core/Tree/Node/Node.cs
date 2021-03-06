using System;
using System.Collections.Generic;
using Farious.Gist.UIComponents.Components;
using Farious.Gist.UIComponents.Components.Extensions.Command;
using UnityEngine;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Tree
{
	public partial class Node : Entity, INode
	{
		public bool IsVisible { get; private set; }

		public event Action Destroying;

		public event Action Shown;
		public event Action Hidden;

		public event Action Activated;
		public event Action Deactivated;

		public event Action AttachedToHierarchy;

		public Node()
		{
			Create<HierarchyComponent>();
			Create<CommandsComponent>();

			Hierarchy.Activated += () => Activated?.Invoke();
			Hierarchy.Deactivated += () => Deactivated?.Invoke();
		}

		public sealed override TComponent Create<TComponent>()
		{
			var component = new TComponent();
			_components.Add(typeof(TComponent), component);
			component.Attach(this);
			return component;
		}

		public sealed override void Attach<TComponent>(TComponent component)
		{
			_components.Add(typeof(TComponent), component);
			component.Attach(this);
		}

		public sealed override TComponent Remove<TComponent>()
		{
			if (!Has<TComponent>())
				throw new NullReferenceException($"Unable to remove component of type {typeof(TComponent)}.");

			var type = typeof(TComponent);
			var target = (TComponent) _components[type];
			_components.Remove(type);
			return target;
		}

		public sealed override void Remove(NodeComponent component)
		{
			if (!Contains(component))
				throw new NullReferenceException($"Unable to remove component of type {component.GetType()}.");

			_components.Remove(component.GetType());
		}

		public void Dispose()
		{
			try
			{
				Destroying?.Invoke();
			}
			catch (Exception e)
			{
				Debug.LogError(e);
			}

			Deactivate();

			RemoveFromHierarchy();
		}

		public void Show()
		{
			if (IsVisible)
				throw new InvalidOperationException("Already shown");

			style.display = DisplayStyle.Flex;
			IsVisible = true;

			Shown?.Invoke();
		}

		public void Hide()
		{
			if (!IsVisible)
				throw new InvalidOperationException("Already hidden");

			style.display = DisplayStyle.None;
			IsVisible = false;

			Hidden?.Invoke();
		}

		public void Activate() =>
			Hierarchy.Activate();

		public void Deactivate() =>
			Hierarchy.Deactivate();

		public void AttachToHierarchy(HierarchyComponent parent)
		{
			if (Hierarchy.Parent != null)
				throw new Exception("Element is already attached to hierarchy");

			Hierarchy.SetParent(parent);
			AttachedToHierarchy?.Invoke();
		}

		public TNode CreateChild<TNode>()
			where TNode : class, INode, new()
		{
			return CreateChild<TNode>(this);
		}

		public TNode CreateChild<TNode>(VisualElement container)
			where TNode : class, INode, new()
		{
			var result = new TNode();
			result.AttachToHierarchy(Hierarchy);
			container.Add(result.View);
			return result;
		}

		public void Create(INode node)
		{
			node.AttachToHierarchy(Hierarchy);
			Add(node.View);
		}

		public bool HasInParents(Type type, bool includeSelf = false)
		{
			foreach (var element in Hierarchy.PathToRoot)
			{
				var node = element.Node;

				if (node == this && !includeSelf)
					continue;

				if (node.Has(type))
					return true;
			}

			return false;
		}

		public bool HasInParents<TComponent>(bool includeSelf = false)
			where TComponent : class, INodeComponent
		{
			foreach (var element in Hierarchy.PathToRoot)
			{
				var node = element.Node;

				if (node == this && !includeSelf)
					continue;

				if (node.Has<TComponent>())
					return true;
			}

			return false;
		}

		public TComponent FindInParents<TComponent>(bool includeSelf = false)
			where TComponent : class, INodeComponent
		{
			TComponent result = default;

			foreach (var element in Hierarchy.PathToRoot)
			{
				var node = element.Node;

				if (node == this && !includeSelf)
					continue;

				if (node.Has<TComponent>())
				{
					result = node.Get<TComponent>();
					break;
				}
			}

			if (result == null)
				throw new NullReferenceException("Component not found");

			return result;
		}

		public IEnumerable<TComponent> GetAllInParents<TComponent>(bool includeSelf)
			where TComponent : class, INodeComponent
		{
			HashSet<TComponent> result = null;

			foreach (var element in Hierarchy.PathToRoot)
			{
				var node = element.Node;

				if (node == this && !includeSelf)
					continue;

				if (node.Has<TComponent>())
					foreach (var comp in node.GetAll<TComponent>())
						result.Add(comp);
			}

			return result;
		}

		public void AddStyleSheets(IEnumerable<StyleSheet> sheets)
		{
			foreach (var styleSheet in sheets)
				styleSheets.Add(styleSheet);
		}
	}
}
