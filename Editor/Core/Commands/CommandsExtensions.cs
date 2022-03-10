using System;
using Farious.Gist.UIComponents.Tree;
using Farious.Gist.UIComponents.Components;

namespace Farious.Gist.UIComponents.Commands
{
	public static class CommandsExtensions
	{
		public static AddHandlerCommand<TAction> AddHandler<TAction>(this Node node, TAction targetEvent,
			TAction handler, object target, CommandLifetime lifetime = CommandLifetime.Full)
			where TAction : Delegate
		{
			var result = new AddHandlerCommand<TAction>();
			result.AssignData(targetEvent, handler);
			result.AssignTarget(target);

			node.Add(result, lifetime);

			return result;
		}
	}
}
