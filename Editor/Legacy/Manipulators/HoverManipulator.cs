using System;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents
{
	public class HoverManipulator : MouseManipulator
	{
		#region Init

		protected static bool isHovered = false;
		protected static VisualElement hoverTarget;

		Action onHover;

		public void SetHoverHandler(Action value)
		{
			if (value == null)
				throw new Exception("Can not set null value!");

			onHover = value;
		}

		Action onDehover;

		public void SetDehoverHandler(Action value)
		{
			if (value == null)
				throw new Exception("Can not set null value!");

			onDehover = value;
		}

		public HoverManipulator(VisualElement target, Action onHover, Action onDehover)
		{
			this.target = target;

			this.onHover = onHover;
			this.onDehover = onDehover;
		}

		#endregion

		#region Registrations

		protected override void RegisterCallbacksOnTarget()
		{
			target.RegisterCallback<MouseOverEvent>(OnMouseOver);
			target.RegisterCallback<MouseOutEvent>(OnMouseOut);
		}

		protected override void UnregisterCallbacksFromTarget()
		{
			target.UnregisterCallback<MouseOverEvent>(OnMouseOver);
			target.UnregisterCallback<MouseOutEvent>(OnMouseOut);
		}

		#endregion

		#region Handlers

		#region OnMouseOver

		void OnMouseOver(MouseOverEvent e)
		{
			onHover();
			hoverTarget = target;
			e.StopPropagation();
		}

		#endregion

		#region OnMouseOut

		void OnMouseOut(MouseOutEvent e)
		{
			onDehover();
			e.StopPropagation();
		}

		#endregion

		#endregion
	}
}