using Farious.Gist.UIComponents.Tree;

namespace Farious.Gist.UIComponents.Examples.Tags.Foldouts
{
	public class ExampleRoot : Root
	{
		public readonly FoldoutCarrier Example;

		public ExampleRoot()
		{
			Example = CreateAndAddChild<FoldoutCarrier>();
		}
	}
}