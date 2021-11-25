using System;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Components
{
	public class ContextualMenuComponent : NodeComponent
	{
		public ContextualMenuManipulator ContextualManipulator { get; }
		public event Action<DropdownMenu> PopulatingMenu;

		public ContextualMenuComponent()
		{
			AttachedToNode += SubscribeOnNode;

			ContextualManipulator = new ContextualMenuManipulator(evt =>
			{
				PopulatingMenu?.Invoke(evt.menu);
				evt.StopPropagation();
			});
		}

		public void SubscribeOnNode()
		{
			ContextualManipulator.target = Node.View;
		}
	}
}