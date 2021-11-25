using System;
using Farious.Gist.UIComponents.Tree;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Tags.Toggles
{
	public class ArrowToggle : Node, IToggle
	{
		public string BlockName => "arrow-toggle";
		public string ClassName => BaseClassName + BlockName;

		public string FoldedClassPostfix => "__folded";
		public string FoldedClassName => ClassName + FoldedClassPostfix;

		public string UnfoldedClassPostfix => "__unfolded";
		public string UnfoldedClassName => ClassName + UnfoldedClassPostfix;

		public event Action Changing;
		public event Action Changed;

		public bool State { get; private set; }

		public bool IsUnfolded => State;
		public bool IsFolded => !State;

		public ArrowToggle()
		{
			AddToClassList(ClassName);

			RegisterCallback<ClickEvent>(HandleClick);
		}

		private void HandleClick(ClickEvent e) =>
			SwitchState();

		public void SetState(bool state)
		{
			Changing?.Invoke();

			State = state;
			EnableInClassList(UnfoldedClassName, IsUnfolded);
			EnableInClassList(FoldedClassName, IsFolded);

			Changed?.Invoke();
		}

		public void SwitchState() =>
			SetState(!State);

		public void Fold() =>
			SetState(false);

		public void Unfold() =>
			SetState(true);
	}
}