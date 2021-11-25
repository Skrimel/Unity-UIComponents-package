using System;
using Farious.Gist.UIComponents.Tree;

namespace Farious.Gist.UIComponents.Tags.Toggles
{
	public interface IToggle : INode
	{
		event Action Changing;
		event Action Changed;

		bool State { get; }

		void SetState(bool state);
		void SwitchState();
	}
}