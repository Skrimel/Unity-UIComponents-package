using Farious.Gist.UIComponents.Tree;
using Farious.Gist.UIComponents.Commands;

namespace Farious.Gist.UIComponents.Components
{
	public static class NodeExtensions
	{
		public static IContextComponent<TContent> FindContext<TContent>(this Node node) =>
			node.FindInParents<IContextComponent<TContent>>(false);

		public static IProviderComponent<TContent> FindProvider<TContent>(this Node node) =>
			node.FindInParents<IProviderComponent<TContent>>(false);

		public static void Add<TCommand>(this Node node, TCommand command,
			CommandLifetime lifetime = CommandLifetime.Full)
			where TCommand : ICommand
		{
			node.Commands.Add(command, lifetime);
		}
	}
}
