using Farious.Gist.UIComponents.Components;
using Farious.Gist.UIComponents.Tree;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Examples.PagesFeature
{
	public class PagesExample : Node
	{
		PagesComponent Pages { get; }
		TabsComponent Tabs { get; }

		public PagesExample() : base()
		{
			var tabsContainer = new VisualElement();
			tabsContainer.style.flexDirection = FlexDirection.Row;
			Add(tabsContainer);

			Pages = AddComponent<PagesComponent>();
			Tabs = AddComponent<TabsComponent>();
			Tabs.SetViewData(tabsContainer, Pages);

			CreateAndAddChild<Page1>();
			CreateAndAddChild<Page2>();
			CreateAndAddChild<Page3>();

			Activated += Tabs.CreateTabs;
		}
	}
}