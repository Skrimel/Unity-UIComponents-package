using System.Collections.Generic;
using UnityEngine;

namespace Farious.Gist.UIComponents
{
	public class KeyboardController<TRoot>
	{
		float shortcutRepeatStartThreshold
		{
			get => 1f;
		}

		bool isThresholdCounts;
		float timeSinceThresholdStarted;

		List<KeyCode> keysDown = new List<KeyCode>();

		public List<KeyCode> KeysDown
		{
			get => keysDown;
		}

		public void ClearKeysDown()
		{
			keysDown.Clear();
		}

		public void Update()
		{
			KeyboardShortcuts.ForEach((ShortcutHandlerBase<TRoot> handler) => handler.UpdateTimers());
		}

		public void HandleEvent(Event e)
		{
			if (e.isKey && e.keyCode != KeyCode.None)
			{
				if (e.type == EventType.KeyDown)
				{
					if (!keysDown.Contains(e.keyCode))
						keysDown.Add(e.keyCode);
					e.Use();
				}
				else if (e.type == EventType.KeyUp)
				{
					if (keysDown.Contains(e.keyCode))
						keysDown.Remove(e.keyCode);
					e.Use();
				}
				//DebugOutput();

				KeyboardShortcuts.ForEach((ShortcutHandlerBase<TRoot> shortcut) => shortcut.TryHandle());
			}
		}

		void DebugOutput()
		{
			string result = "Current key combination is: ";

			for (int i = 0; i < keysDown.Count; i++)
			{
				result += keysDown[i].ToString();
				if (i != keysDown.Count - 1) result += "+";
			}

			Debug.Log(result);
		}

		List<ShortcutHandlerBase<TRoot>> keyboardShortcuts;

		List<ShortcutHandlerBase<TRoot>> KeyboardShortcuts
		{
			get => keyboardShortcuts;
		}

		public KeyboardController()
		{
			keyboardShortcuts = new List<ShortcutHandlerBase<TRoot>>()
			{
				//new CreateNewNodeShortcutHandler(_mainController),
				//new CreateNewRegularNodeElementShortcut(_mainController),
				//new ChangeSelectedElementToNextShortcut(_mainController),
				//new ChangeSelectedElementToPreviousShortcut(_mainController),
				//new DeleteSelectedElementShortcutHandler(_mainController),
				//new CreateNewPhraseAndStartEditShortcutHandler(_mainController)
			};
		}
	}
}