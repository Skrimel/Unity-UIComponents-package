using System;
using Farious.Gist.UIComponents.Components;
using Farious.Gist.UIComponents.Components.Extensions.Command;

namespace Farious.Gist.UIComponents.Tree
{
	public partial interface INode
	{
		HierarchyComponent Hierarchy { get; }
		CommandsComponent Commands { get; }

		INode ParentInHierarchy { get; }
		bool IsActive { get; }

		event Action AttachedToHierarchy;
	}
}
