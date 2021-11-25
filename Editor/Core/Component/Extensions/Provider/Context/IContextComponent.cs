namespace Farious.Gist.UIComponents.Components
{
	public interface IContextComponent<TContent> : IProviderComponent<TContent>
	{
		void SetContent(TContent content);
	}
}