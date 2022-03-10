using Farious.Gist.UIComponents.Tree;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Components
{
	public class Tab : Node
	{
		private readonly Image _faviconContainer = new();
		private readonly Label _label = new();
		private PageComponent _page;
		private PagesComponent _pagesComponent;

		public Tab() : base()
		{
			((VisualElement)this).Add(_faviconContainer);
			((VisualElement)this).Add(_label);

			AddToClassList("tab");

			RegisterCallback<MouseUpEvent>(e => _pagesComponent.CurrentPage = _page);
		}

		public void SetPage(PagesComponent pagesController, PageComponent page)
		{
			_pagesComponent = pagesController;
			_page = page;
			_label.text = page.TabName;
			_faviconContainer.image = page.TabFavicon;

			page.OnActivenessChanged += SetActiveClass;

			if (page == pagesController.CurrentPage)
				SetActiveClass(true);
		}

		private void SetActiveClass(bool activate) =>
			EnableInClassList("activeTab", activate);
	}
}
