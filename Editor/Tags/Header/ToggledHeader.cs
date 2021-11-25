using System;
using Farious.Gist.UIComponents.Tags.Toggles;
using UnityEngine;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents.Tags.Headers
{
	public class ToggledHeader : Header, IToggle
	{
		public event Action Changing;
		public event Action Changed;

		public readonly string BlockName = "toggled-header";
		public string ClassName => BaseClassName + BlockName;

		public readonly string ToggleName = "toggled-header__toggle";
		public string ToggleClassName => BaseClassName + ToggleName;

		public IToggle InnerToggle { get; }

		public bool State => InnerToggle.State;

		public ToggledHeader(IToggle toggle, Sprite icon = default) : base(icon)
		{
			AddToClassList(ClassName);

			InnerToggle = toggle;
			InnerToggle.View.AddToClassList(ToggleClassName);
			AddChild(InnerToggle);
			InnerToggle.View.SendToBack();

			InnerToggle.Changed += PropagateChanged;
			InnerToggle.Changing += PropagateChanging;

			RegisterCallback<ClickEvent>(HandleClick);
		}

		private void HandleClick(ClickEvent e)
		{
			if (e.target != InnerToggle.View)
				SwitchState();
		}

		private void PropagateChanged() =>
			Changed?.Invoke();

		private void PropagateChanging() =>
			Changing?.Invoke();

		public void SwitchState() => InnerToggle.SwitchState();

		public void SetState(bool state) => InnerToggle.SetState(state);
	}
}