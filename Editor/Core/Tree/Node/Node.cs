using System;
using System.Collections.Generic;
using Farious.Gist.UIComponents.Components;
using UnityEngine;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Tree
{
	public class Node : Entity, INode
	{
		public new HierarchyComponent Hierarchy { get; private set; }
		public INode ParentInHierarchy => Hierarchy.Parent.Node;

		public event Action Shown;
		public event Action Hidden;

		public event Action Activated;
		public event Action Deactivated;

		public event Action Destroying;

		public event Action AttachedToHierarchy;

		public Node()
		{
			Hierarchy = AddComponent<HierarchyComponent>();

			Hierarchy.Activated += () => Activated?.Invoke();
			Hierarchy.Deactivated += () => Deactivated?.Invoke();
		}

		public sealed override TComponent AddComponent<TComponent>()
		{
			var component = new TComponent();
			_components.Add(component);
			component.Attach(this);
			return component;
		}

		public sealed override void AddComponent<TComponent>(TComponent component)
		{
			_components.Add(component);
			component.Attach(this);
		}

		public sealed override TComponent RemoveComponent<TComponent>()
		{
			if (!HasComponent<TComponent>())
				throw new NullReferenceException($"Unable to remove component of type {typeof(TComponent)}.");

			var comp = FindComponent<TComponent>();
			_components.Remove(comp);
			return comp;
		}

		public sealed override void RemoveComponent(Components.NodeComponent component)
		{
			if (!ContainsComponent(component))
				throw new NullReferenceException($"Unable to remove component of type {component.GetType()}.");

			_components.Remove(component);
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
			style.display = DisplayStyle.Flex;

			Shown?.Invoke();
		}

		public void Hide()
		{
			style.display = DisplayStyle.None;

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
			var result = new TNode();
			result.AttachToHierarchy(Hierarchy);
			return result;
		}

		public TNode CreateAndAddChild<TNode>()
			where TNode : class, INode, new()
		{
			return CreateAndAddChild<TNode>(this);
		}

		public TNode CreateAndAddChild<TNode>(VisualElement container)
			where TNode : class, INode, new()
		{
			var result = CreateChild<TNode>();
			container.Add(result.View);
			return result;
		}

		public void AddChild(INode node)
		{
			node.AttachToHierarchy(Hierarchy);
			Add(node.View);
		}

		public bool HasComponentInParents(Type type, bool includeSelf)
		{
			foreach (var element in Hierarchy.PathToRoot)
			{
				var node = element.Node;

				if (node == this && !includeSelf)
					continue;

				if (node.HasComponent(type))
					return true;
			}

			return false;
		}

		public bool HasComponentInParents<TComponent>(bool includeSelf)
			where TComponent : class, INodeComponent
		{
			foreach (var element in Hierarchy.PathToRoot)
			{
				var node = element.Node;

				if (node == this && !includeSelf)
					continue;

				if (node.HasComponent<TComponent>())
					return true;
			}

			return false;
		}

		public TComponent FindComponentInParents<TComponent>(bool includeSelf)
			where TComponent : class, INodeComponent
		{
			TComponent result = default;

			foreach (var element in Hierarchy.PathToRoot)
			{
				var node = element.Node;

				if (node == this && !includeSelf)
					continue;

				if (node.HasComponent<TComponent>())
				{
					result = node.FindComponent<TComponent>();
					break;
				}
			}

			if (result == null)
				throw new NullReferenceException("Component not found");

			return result;
		}

		public IContextComponent<TContent> FindContext<TContent>() =>
			FindComponentInParents<IContextComponent<TContent>>(false);

		public IProviderComponent<TContent> FindProvider<TContent>() =>
			FindComponentInParents<IProviderComponent<TContent>>(false);

		public IEnumerable<TComponent> GetComponentsInParents<TComponent>(bool includeSelf)
			where TComponent : class, INodeComponent
		{
			HashSet<TComponent> result = null;

			foreach (var element in Hierarchy.PathToRoot)
			{
				var node = element.Node;

				if (node == this && !includeSelf)
					continue;

				if (node.HasComponent<TComponent>())
					foreach (var comp in node.GetComponents<TComponent>())
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