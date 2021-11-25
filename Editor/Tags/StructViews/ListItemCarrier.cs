using Farious.Gist.UIComponents.Tree;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Tags.StructViews
{
	public class ListItemCarrier<TItem> : Node
		where TItem : VisualElement
	{
		public ListItemCarrier()
		{
			var draggerContainer = CreateAndAddChild<Node>();
			var icon = new Image();
			draggerContainer.Add(icon);
		}
	}
}