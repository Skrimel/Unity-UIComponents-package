using Farious.Gist.UIComponents.Components;
using Farious.Gist.UIComponents.Tree;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Examples.PagesFeature
{
	public class PagesExample : Node
	{
		PagesComponent Pages { get; }
		TabsComponent Tabs { get; }

		public PagesExample()
		{
			var tabsContainer = new VisualElement();
			tabsContainer.style.flexDirection = FlexDirection.Row;
			Add(tabsContainer);

			Pages = Create<PagesComponent>();
			Tabs = Create<TabsComponent>();
			Tabs.SetViewData(tabsContainer, Pages);

			CreateChild<Page1>();
			CreateChild<Page2>();
			CreateChild<Page3>();

			Activated += Tabs.CreateTabs;
		}
	}
}
