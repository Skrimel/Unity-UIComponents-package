using Farious.Gist.UIComponents.Tree;

namespace Farious.Gist.UIComponents.Examples.PagesFeature
{
	public class ExampleRoot : Root
	{
		public readonly PagesExample _pages;

		public ExampleRoot() : base()
		{
			_pages = CreateAndAddChild<PagesExample>();
		}
	}
}