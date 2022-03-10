using System;
using System.Collections.Generic;
using Farious.Gist.UIComponents.Tree;

namespace Farious.Gist.UIComponents.Components
{
	public abstract class NodeComponent : INodeComponent
	{
		public virtual IEnumerable<Type> RequiredComponents => new List<Type>();

		public INode Node { get; private set; }

		public event Action AttachedToNode;

		public void Attach(INode node)
		{
			if (!CheckNode(node))
				throw new ArgumentException("Entity is missing required components");

			Node = node;

			AttachedToNode?.Invoke();
		}

		bool CheckNode(INode node)
		{
			var result = true;

			foreach (var component in RequiredComponents)
			{
				result &= node.Has(component.GetType());

				if (!result)
					break;
			}

			return result;
		}
	}
}
