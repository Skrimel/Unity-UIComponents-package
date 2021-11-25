namespace Farious.Gist.UIComponents.Components
{
	public class ContextComponent<TContent> : ProviderComponent<TContent>, IContextComponent<TContent>
	{
		private TContent _content;
		public override TContent Content => _content;

		public void SetContent(TContent content)
		{
			_content = content;
			MarkContentAsChanged();
		}
	}
}