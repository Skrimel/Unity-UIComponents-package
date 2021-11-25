using Farious.Gist.UIComponents.Components;
using Farious.Gist.UIComponents.Tree;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Examples.PagesFeature
{
	public class Page1 : Node
	{
		Label _label;

		PageComponent Component { get; set; }

		public Page1() : base()
		{
			_label = new Label();
			_label.text = "Page 1 content";
			Add(_label);

			AttachedToHierarchy += () =>
			{
				Component = AddComponent<PageComponent>();
				Component.TabName = "Page 1";
			};
		}
	}
}