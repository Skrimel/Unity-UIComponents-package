namespace Farious.Gist.UIComponents.Tree
{
	public class Root : Node
	{
		public Root()
		{
			var settingsFinder = Create<AssetsFinderComponent<GeneralSettingsCarrier>>();
			var settings = settingsFinder.FindFirstAssociatedAsset();
			AddStyleSheets(settings.StyleSheets);
		}
	}
}