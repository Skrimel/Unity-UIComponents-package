using UnityEditor;

namespace Farious.Gist.UIComponents.Examples.Tags.Foldouts
{
	public class ExampleWindow : EditorWindow
	{
		ExampleRoot Root { get; set; }

		public void OnEnable()
		{
			Root = new ExampleRoot();
			Root.Activate();
			rootVisualElement.Add(Root);
		}

		[MenuItem("Tools/Gist/EditorKit Examples/Tags/Foldout")]
		public static void Initialize()
		{
			GetWindow<ExampleWindow>("Pages Example Window", true);
		}

		public void OnDisable()
		{
			Root.Dispose();
		}
	}
}
