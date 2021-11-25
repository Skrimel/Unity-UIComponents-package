namespace Gist.Core.UIComponents.Runtime.Core.Commands
{
	public interface ICommand
	{
		void Do();
		void Undo();
	}
}
