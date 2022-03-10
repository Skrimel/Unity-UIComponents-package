namespace Farious.Gist.UIComponents.Commands
{
	public abstract class Command<TTarget> : ICommand
	{
		public abstract void AssignTarget(TTarget target);
		public abstract void Do();
		public abstract void Undo();
	}
}
