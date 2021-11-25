using System.Collections.Generic;
using Farious.Gist.UIComponents.Tree;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Tags.StructViews
{
	public class ListView<TItem, TCarrier> : Node
		where TCarrier : ListItemCarrier<TItem>
		where TItem : VisualElement
	{
		private List<TCarrier> Items { get; } = new List<TCarrier>();
		
		public void AddItem(TCarrier item)
		{
			
		}

		public void Clear()
		{
			foreach (var item in Items)
			{
				item.Dispose();
			}
			
			Items.Clear();
		}
	}
}