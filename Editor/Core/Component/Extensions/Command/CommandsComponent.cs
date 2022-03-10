using System.Collections.Generic;
using Farious.Gist.UIComponents.Commands;

namespace Farious.Gist.UIComponents.Components.Extensions.Command
{
	public class CommandsComponent : NodeComponent
	{
		private Dictionary<CommandLifetime, List<ICommand>> _commands;

		public CommandsComponent()
		{
			AttachedToNode += () =>
			{
				Node.Activated += HandleNodeActivated;
				Node.Deactivated += HandleNodeDeactivated;

				Node.Shown += HandleNodeShown;
				Node.Hidden += HandleNodeHidden;

				Node.Destroying += HandleNodeDestroying;
			};
		}

		public void Add<TCommand>(TCommand command, CommandLifetime lifetime = CommandLifetime.Full)
			where TCommand : ICommand
		{
			if (_commands.ContainsKey(lifetime))
				_commands[lifetime].Add(command);
			else
				_commands.Add(lifetime, new() {command});

			HandleCommandAdding(command, lifetime);
		}

		private void HandleCommandAdding(ICommand command, CommandLifetime lifetime = CommandLifetime.Full)
		{
			if (lifetime == CommandLifetime.Full
			    || Node.IsActive && lifetime == CommandLifetime.Activated
			    || Node.IsVisible && lifetime == CommandLifetime.Visible)
				command.Do();
		}

		private void HandleNodeActivated()
		{
			foreach (var command in _commands[CommandLifetime.Activated])
				command.Do();
		}

		private void HandleNodeDeactivated()
		{
			foreach (var command in _commands[CommandLifetime.Activated])
				command.Undo();
		}

		private void HandleNodeShown()
		{
			foreach (var command in _commands[CommandLifetime.Visible])
				command.Do();
		}

		private void HandleNodeHidden()
		{
			foreach (var command in _commands[CommandLifetime.Visible])
				command.Undo();
		}

		private void HandleNodeDestroying()
		{
			foreach (var command in _commands[CommandLifetime.Full])
				command.Undo();
		}
	}
}
