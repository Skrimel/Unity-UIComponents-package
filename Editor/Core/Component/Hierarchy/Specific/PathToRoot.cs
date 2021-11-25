using System.Collections;
using System.Collections.Generic;

namespace Farious.Gist.UIComponents.Components
{
	public struct PathToRoot : IEnumerable<IHierarchyComponent>
	{
		IHierarchyComponent _pathStart;

		public IEnumerator<IHierarchyComponent> GetEnumerator() => new BranchWalker(_pathStart);
		IEnumerator IEnumerable.GetEnumerator() => new BranchWalker(_pathStart);

		public PathToRoot(IHierarchyComponent pathStart)
		{
			_pathStart = pathStart;
		}
	}
}