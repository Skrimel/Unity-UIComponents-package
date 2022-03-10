using Farious.Gist.UIComponents.Tree;
using UnityEngine.UIElements;
using Foldout = Farious.Gist.UIComponents.Tags.Foldouts.Foldout;

namespace Farious.Gist.UIComponents.Examples.Tags.Foldouts
{
	public class FoldoutCarrier : Node
	{
		public readonly Foldout Foldout;

		public FoldoutCarrier()
		{
			Foldout = CreateChild<Foldout>();
			Foldout.Header.Text.text = "Foldout";

			for (int i = 0; i < 5; i++)
			{
				var element = new TextElement();
				element.text = "Text " + i;

				((VisualElement)this).Add(element);
			}
		}
	}
}
