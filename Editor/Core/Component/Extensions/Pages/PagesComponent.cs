using System;
using System.Collections.Generic;

namespace Farious.Gist.UIComponents.Components
{
	public class PagesComponent : NodeComponent
	{
		private List<PageComponent> _pages = new List<PageComponent>();
		public IEnumerable<PageComponent> Pages => _pages;

		PageComponent _currentPage = null;

		public PageComponent CurrentPage
		{
			get => _currentPage;
			set
			{
				var oldPage = CurrentPage;

				if (_currentPage != null)
				{
					_currentPage.SetActiveState(false);
					_currentPage.Node.Deactivate();
					_currentPage.Hide();
				}

				_currentPage = value;
				_currentPage.SetActiveState(true);
				_currentPage.Node.Activate();
				_currentPage.Show();

				if (CurrentPage != oldPage)
					CurrentPageChanged?.Invoke(CurrentPage);
			}
		}

		public event Action<PageComponent> CurrentPageChanged;

		public void AddPage(PageComponent page)
		{
			_pages.Add(page);
			CurrentPage = page;
		}
	}
}