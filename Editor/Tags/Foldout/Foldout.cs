using Farious.Gist.UIComponents.Tags.Headers;
using Farious.Gist.UIComponents.Tags.Toggles;
using Farious.Gist.UIComponents.Tree;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Tags.Foldouts
{
	public class Foldout : Node
	{
		public readonly string BlockName = "foldout";
		public string ClassName => BaseClassName + BlockName;
		public readonly string ContainerName = "foldout-container";
		public string ContainerClassName => BaseClassName + ContainerName;

		private readonly VisualElement _container;
		public override VisualElement contentContainer => _container ?? base.contentContainer;

		public readonly ToggledHeader Header;

		public bool IsFolded { get; private set; }

		public Foldout(IToggle toggle)
		{
			Header = new ToggledHeader(toggle);
			AddChild(Header);

			_container = new VisualElement();
			_container.style.paddingLeft = 4;
			_container.AddToClassList(ContainerClassName);
			hierarchy.Add(_container);

			AddToClassList(ClassName);

			Header.SetState(IsFolded);
			Header.Changed += () => SetFoldState(toggle.State);

			SetFoldState(toggle.State);
		}

		public Foldout() : this(new ArrowToggle())
		{
		}

		public void SetFoldState(bool state)
		{
			IsFolded = !state;

			var visibility = DisplayStyle.None;

			if (!IsFolded)
				visibility = DisplayStyle.Flex;

			contentContainer.style.display = visibility;
		}

		public void ChangeFoldState() =>
			SetFoldState(!IsFolded);
	}
}