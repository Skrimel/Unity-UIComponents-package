using Farious.Gist.UIComponents.Components;
using Farious.Gist.UIComponents.Tree;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Examples.PagesFeature
{
	public class Page2 : Node
	{
		Label _label;

		PageComponent Component { get; set; }

		public Page2() : base()
		{
			_label = new Label();
			_label.text = "Page 2 content";
			Add(_label);

			AttachedToHierarchy += () =>
			{
				Component = AddComponent<PageComponent>();
				Component.TabName = "Page 2";
			};
		}
	}
}