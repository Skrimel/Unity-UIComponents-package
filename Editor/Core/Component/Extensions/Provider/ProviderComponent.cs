using System;

namespace Farious.Gist.UIComponents.Components
{
	public abstract class ProviderComponent<TContent> : NodeComponent, IProviderComponent<TContent>
	{
		public abstract TContent Content { get; }
		public event Action ContentChanged;
		public event Action ContentModified;

		protected void MarkContentAsChanged() =>
			ContentChanged?.Invoke();

		public void MarkAsModified() =>
			ContentModified?.Invoke();
	}
}