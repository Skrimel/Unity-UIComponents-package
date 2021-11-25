using System;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents
{
	public class FocusManipulator : MouseManipulator
	{
		public Action<FocusEvent> onFocusHandler;
		public Action<BlurEvent> onBlurHandler;

		public FocusManipulator(VisualElement target,
			Action<FocusEvent> onFocusHandler = null,
			Action<BlurEvent> onBlurHandler = null)
		{
			base.target = target;
			this.onFocusHandler = onFocusHandler;
			this.onBlurHandler = onBlurHandler;
		}

		protected override void RegisterCallbacksOnTarget()
		{
			target.RegisterCallback<FocusEvent>(OnFocus);
			target.RegisterCallback<BlurEvent>(OnBlur);
		}

		protected override void UnregisterCallbacksFromTarget()
		{
			target.UnregisterCallback<FocusEvent>(OnFocus);
			target.UnregisterCallback<BlurEvent>(OnBlur);
		}

		void OnFocus(FocusEvent e)
		{
			onFocusHandler(e);
		}

		void OnBlur(BlurEvent e)
		{
			onBlurHandler(e);
		}
	}
}