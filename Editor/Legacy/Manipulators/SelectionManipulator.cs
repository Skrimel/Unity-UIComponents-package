using System;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents
{
	public class SelectionManipulator : MouseManipulator
	{
		#region Init

		Action<MouseUpEvent> onSelection;

		public SelectionManipulator(VisualElement target, Action<MouseUpEvent> onSelection)
		{
			this.target = target;

			this.onSelection = onSelection;
			this.activators.Add(new ManipulatorActivationFilter {button = MouseButton.LeftMouse});
		}

		#endregion

		#region Registrations

		protected override void RegisterCallbacksOnTarget()
		{
			target.RegisterCallback<MouseUpEvent>(OnSelection);
			target.RegisterCallback<MouseDownEvent>(DragPlug);
		}

		protected override void UnregisterCallbacksFromTarget()
		{
			target.UnregisterCallback<MouseUpEvent>(OnSelection);
			target.UnregisterCallback<MouseDownEvent>(DragPlug);
		}

		#endregion

		#region Handlers

		void OnSelection(MouseUpEvent e)
		{
			if (CanStartManipulation(e) && (e.bubbles || e.target == target) &&
			    !DragManipulator.WasDragged)
			{
				onSelection(e);
				e.StopPropagation();
			}
		}

		void DragPlug(MouseDownEvent e)
		{
			if (e.bubbles || e.target == target) e.StopPropagation();
		}

		#endregion
	}
}