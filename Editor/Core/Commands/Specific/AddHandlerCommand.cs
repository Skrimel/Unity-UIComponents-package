using System;
using System.Reflection;

namespace Farious.Gist.UIComponents.Commands
{
	public class AddHandlerCommand<TDelegate> : Command<object>
		where TDelegate : Delegate
	{
		private EventInfo _targetEvent;

		private TDelegate _eventDelegate;
		private TDelegate _handler;

		private object _target;

		public override void AssignTarget(object target)
		{
			_target = target;

			var targetType = target.GetType();
			var eventsInfo = targetType.GetEvents();

			foreach (var eventInfo in eventsInfo)
			{
				var field = targetType.GetField(eventInfo.Name);
				var eventValue = field.GetValue(target);

				if (eventValue == _eventDelegate)
				{
					_targetEvent = eventInfo;
					break;
				}
			}
		}

		public override void Do()
		{
			_targetEvent.AddMethod.Invoke(_target, new object[] {_handler});
		}

		public override void Undo()
		{
			_targetEvent.RemoveMethod.Invoke(_target, new object[] {_handler});
		}

		public void AssignEvent(TDelegate eventDelegate)
		{
			_eventDelegate = eventDelegate;
		}

		public void AssignHandler(TDelegate handler)
		{
			_handler = handler;
		}

		public void AssignData(TDelegate eventDelegate, TDelegate handler)
		{
			AssignEvent(eventDelegate);
			AssignHandler(handler);
		}
	}
}
