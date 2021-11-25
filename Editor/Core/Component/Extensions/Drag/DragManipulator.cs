using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents
{
	public class DragManipulator : MouseManipulator
	{
		#region Init

		protected bool isDragging = false;

		public event Action<MouseUpEvent> OnMouseUpHandler;
		public event Action<MouseMoveEvent> DragHandler;
		public event Action<MouseDownEvent> OnMouseDownHandler;

		public static bool WasDragged { get; private set; }
		public static bool DragCaught { get; set; }

		public DragManipulator(VisualElement target, Action<MouseMoveEvent> dragHandler = null,
			Action<MouseUpEvent> onMouseUpHandler = null, Action<MouseDownEvent> onMouseDownHandler = null)
		{
			activators.Add(new ManipulatorActivationFilter {button = MouseButton.LeftMouse});

			this.target = target;

			DragHandler = dragHandler;
			OnMouseUpHandler = onMouseUpHandler;
			OnMouseDownHandler = onMouseDownHandler;
		}

		#endregion

		#region Registrations

		protected override void RegisterCallbacksOnTarget()
		{
			target.RegisterCallback<MouseDownEvent>(OnMouseDown);
			target.RegisterCallback<MouseUpEvent>(OnMouseUp);
			target.RegisterCallback<MouseMoveEvent>(OnMouseMove);
		}

		protected override void UnregisterCallbacksFromTarget()
		{
			target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
			target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
			target.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
		}

		#endregion

		#region Handlers

		#region OnMouseDown

		protected void OnMouseDown(MouseDownEvent e)
		{
			if (isDragging)
				return;

			if (CanStartManipulation(e) && !DragCaught)
			{
				OnMouseDownHandler?.Invoke(e);

				isDragging = true;
				WasDragged = false;
				target.CaptureMouse();
				DragCaught = true;
			}
		}

		#endregion

		#region OnMouseMove

		protected void OnMouseMove(MouseMoveEvent e)
		{
			if (!isDragging && !target.HasMouseCapture())
				return;

			DragHandler?.Invoke(e);
			WasDragged = true;

			e.StopImmediatePropagation();
		}

		#endregion

		#region OnMouseUp

		protected void OnMouseUp(MouseUpEvent e)
		{
			if (!isDragging || !target.HasMouseCapture() || !CanStartManipulation(e) || !DragCaught)
				return;

			isDragging = false;
			target.ReleaseMouse();

			OnMouseUpHandler?.Invoke(e);

			e.StopPropagation();

			WasDragged = false;
			DragCaught = false;
		}

		#endregion

		#endregion
	}
}