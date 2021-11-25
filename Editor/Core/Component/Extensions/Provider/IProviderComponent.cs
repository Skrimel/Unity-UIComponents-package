using System;

namespace Farious.Gist.UIComponents.Components
{
	public interface IProviderComponent<out TContent> : INodeComponent
	{
		TContent Content { get; }
		event Action ContentChanged;
		event Action ContentModified;
	}
}