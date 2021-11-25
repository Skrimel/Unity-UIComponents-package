using System;
using UnityEngine;

namespace Gist.Core.UIComponents.Runtime.Core.Commands
{
	public class AddHandlerCommand<TDelegate> : ICommand
		where TDelegate : Delegate
	{
		private TDelegate _target;
		private TDelegate _handler;
		
		public void Do()
		{
		}

		public void Undo()
		{
			
		}

		public void AssignEvent(ref TDelegate e)
		{
			if (!e.GetType().IsSubclassOf(typeof(Delegate)))
				throw new ArgumentException("Given argument is not delegate");

			_target = e;
		}

		public void AssignHandler(ref TDelegate handler)
		{
			if (!handler.GetType().IsSubclassOf(typeof(Delegate)))
				throw new ArgumentException("Given argument is not delegate");

			_handler = handler;
		}
	}
}