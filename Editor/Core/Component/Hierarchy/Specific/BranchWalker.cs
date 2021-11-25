using System;
using System.Collections;
using System.Collections.Generic;

namespace Farious.Gist.UIComponents.Components
{
	public struct BranchWalker : IEnumerator<IHierarchyComponent>
	{
		IHierarchyComponent _walkStart;

		IHierarchyComponent _current;
		public IHierarchyComponent Current => _current;
		Object IEnumerator.Current => _current;

		public BranchWalker(IHierarchyComponent walkStart)
		{
			_walkStart = walkStart;
			_current = null;
		}

		public bool MoveNext()
		{
			if (_current == null)
				_current = _walkStart;
			else
				_current = _current.Parent;

			return _current != null;
		}

		public void Reset()
		{
			_current = _walkStart;
		}

		public void Dispose()
		{
			_walkStart = null;
			_current = null;
		}
	}
}