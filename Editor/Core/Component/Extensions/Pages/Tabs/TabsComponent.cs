using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Components
{
	public class TabsComponent : NodeComponent
	{
		private VisualElement _tabsContainer = default;
		private PagesComponent _pages = default;

		private readonly List<Tab> _currentTabs = new List<Tab>();

		public void SetViewData(VisualElement container, PagesComponent pages)
		{
			_tabsContainer = container;
			_pages = pages;
		}

		public void CreateTabs()
		{
			foreach (var tab in _currentTabs)
				tab.Dispose();

			_currentTabs.Clear();

			foreach (var page in _pages.Pages)
			{
				var tab = Node.CreateChild<Tab>();
				tab.SetPage(_pages, page);

				_tabsContainer.Add(tab);
				_currentTabs.Add(tab);
			}
		}
	}
}