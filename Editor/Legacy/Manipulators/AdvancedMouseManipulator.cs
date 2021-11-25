using System;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents
{
	public class AdvancedMouseManipulator : MouseManipulator
	{
		public event Action<MouseDownEvent> MouseButtonPressed;
		public event Action<MouseMoveEvent> MouseMoved;
		public event Action<MouseUpEvent> MouseButtonReleased;
		public event Action<WheelEvent> WheelUsed;

		public AdvancedMouseManipulator(VisualElement target,
			Action<MouseDownEvent> mouseButtonPressed = null, Action<MouseMoveEvent> mouseMoved = null,
			Action<MouseUpEvent> mouseButtonReleased = null, Action<WheelEvent> wheelUsed = null)
		{
			base.target = target;

			MouseButtonPressed = mouseButtonPressed;
			MouseMoved = mouseMoved;
			MouseButtonReleased = mouseButtonReleased;
			WheelUsed = wheelUsed;
		}

		protected override void RegisterCallbacksOnTarget()
		{
			target.RegisterCallback<MouseDownEvent>(OnMouseDownHandler);
			target.RegisterCallback<MouseMoveEvent>(OnMouseMoveHandler);
			target.RegisterCallback<MouseUpEvent>(OnMouseUpHandler);
			target.RegisterCallback<WheelEvent>(OnWheelHandler);
		}

		protected override void UnregisterCallbacksFromTarget()
		{
			target.UnregisterCallback<MouseDownEvent>(OnMouseDownHandler);
			target.UnregisterCallback<MouseMoveEvent>(OnMouseMoveHandler);
			target.UnregisterCallback<MouseUpEvent>(OnMouseUpHandler);
			target.UnregisterCallback<WheelEvent>(OnWheelHandler);
		}

		void OnMouseDownHandler(MouseDownEvent e)
		{
			MouseButtonPressed?.Invoke(e);
		}

		void OnMouseMoveHandler(MouseMoveEvent e)
		{
			MouseMoved?.Invoke(e);
		}

		void OnMouseUpHandler(MouseUpEvent e)
		{
			 MouseButtonReleased?.Invoke(e);
		}

		void OnWheelHandler(WheelEvent e)
		{
			WheelUsed?.Invoke(e);
		}
	}
}