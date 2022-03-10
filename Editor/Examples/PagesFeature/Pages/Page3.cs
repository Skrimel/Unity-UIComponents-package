using Farious.Gist.UIComponents.Components;
using Farious.Gist.UIComponents.Tree;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Examples.PagesFeature
{
	public class Page3 : Node
	{
		Label _label;

		PageComponent Component { get; set; }

		public Page3()
		{
			_label = new Label();
			_label.text = "Page 3 content";
			Add(_label);

			AttachedToHierarchy += () =>
			{
				Component = Create<PageComponent>();
				Component.TabName = "Page 3";
			};
		}
	}
}
