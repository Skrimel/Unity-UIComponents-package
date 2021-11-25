using System;
using Farious.Gist.UIComponents.Components;
using UnityEngine;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents
{
	public class DragComponent : NodeComponent
	{
		public DragManipulator InnerManipulator { get; set; }
		public Func<RectOffset> DragThresholdGetter { get; set; }

		public RectOffset DragThreshold { get; private set; } = new RectOffset();

		public Action<MouseUpEvent> MouseUpHandler { get; set; }
		public Func<Vector2, Vector2> DragHandler { get; set; }
		public event Func<Vector2, Vector2> DragValueProcessor;
		public Action<MouseDownEvent> MouseDownHandler { get; set; }

		private Vector2 UnhandledDrag { get; set; }= Vector2.zero;

		public DragComponent()
		{
			InnerManipulator = new DragManipulator(Node.View,
				e => AddDrag(e.mouseDelta),
				e =>
				{
					MouseUpHandler?.Invoke(e);

					ClearUnhandledDrag();
				},
				e => MouseDownHandler?.Invoke(e));

			DragValueProcessor = drag => drag;
		}

		private void ClearUnhandledDrag()
		{
			UnhandledDrag = Vector2.zero;
		}

		private void AddDrag(Vector2 drag)
		{
			if (DragThreshold == null)
				DragThreshold = DragThresholdGetter();

			if (DragValueProcessor != default)
				UnhandledDrag += DragValueProcessor.Invoke(drag);

			if (DragThreshold.top > -1 || DragThreshold.bottom > -1 || DragThreshold.left > -1 ||
			    DragThreshold.right > -1)
			{
				if (UnhandledDrag.x >= DragThreshold.left || UnhandledDrag.x <= DragThreshold.right &&
					UnhandledDrag.y >= DragThreshold.top || UnhandledDrag.y <= DragThreshold.bottom)
				{
					UnhandledDrag = DragHandler(UnhandledDrag);
					DragThreshold = DragThresholdGetter();
				}
			}
		}

		public void SetDragThresholdGetter(Func<RectOffset> value)
		{
			DragThresholdGetter = value;
			DragThreshold = DragThresholdGetter();
		}
	}
}