using Farious.Gist.UIComponents.Components;
using Farious.Gist.UIComponents.Components.Extensions.Command;

namespace Farious.Gist.UIComponents.Tree
{
	public partial class Node
	{
		public new HierarchyComponent Hierarchy => Get<HierarchyComponent>();
		public CommandsComponent Commands => Get<CommandsComponent>();

		public INode ParentInHierarchy => Hierarchy.Parent.Node;
		public bool IsActive => Hierarchy.IsActive;
	}
}
