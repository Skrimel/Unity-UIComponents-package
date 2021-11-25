using Farious.Gist.UIComponents.Tree;
using UnityEngine;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Tags.Headers
{
	public class Header : Node
	{
		public readonly string BlockName = "header";
		public string ClassName => BaseClassName + BlockName;
		public readonly string IconName = "header-icon";
		public string IconClassName => BaseClassName + IconName;

		public readonly TextElement Text;
		public readonly Image Icon;

		public Header(Sprite sprite = default)
		{
			AddToClassList(ClassName);

			Icon = new Image();
			Icon.AddToClassList(IconClassName);
			Add(Icon);

			SetIcon(sprite);

			Text = new TextElement();
			Add(Text);
		}

		public void SetIcon(Sprite icon)
		{
			SetIconVisibility(icon != default);

			Icon.sprite = icon;
		}

		public void HideIcon() =>
			SetIconVisibility(false);

		public void ShowIcon() =>
			SetIconVisibility(true);

		public void SetIconVisibility(bool visibility) =>
			Icon.style.display = visibility ? DisplayStyle.Flex : DisplayStyle.None;
	}
}