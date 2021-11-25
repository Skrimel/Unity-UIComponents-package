using System;
using UnityEngine;

namespace Farious.Gist.UIComponents.Components
{
	public class PageComponent : NodeComponent
	{
		public string TabName { get; set; }
		public Texture2D TabFavicon { get; set; }
		public bool IsActive { get; private set; }
		public event Action<bool> OnActivenessChanged;

		public PageComponent()
		{
			AttachedToNode += FindController;
		}

		public void SetActiveState(bool state)
		{
			var old = IsActive;
			IsActive = state;

			if (IsActive != old)
				OnActivenessChanged?.Invoke(IsActive);
		}

		public void Show() =>
			Node.Show();

		public void Hide() =>
			Node.Hide();

		private void FindController() =>
			Node.FindComponentInParents<PagesComponent>(false).AddPage(this);
	}
}