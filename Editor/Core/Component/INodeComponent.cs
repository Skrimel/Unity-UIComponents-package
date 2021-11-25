using System;
using System.Collections.Generic;
using Farious.Gist.UIComponents.Tree;

namespace Farious.Gist.UIComponents.Components
{
	public interface INodeComponent
	{
		IEnumerable<Type> RequiredComponents { get; }
		INode Node { get; }

		void Attach(INode node);
	}
}