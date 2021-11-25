using System.Linq;
using UnityEditor;
using UnityEditor.ShortcutManagement;

namespace Farious.Gist.UIComponents
{
	[InitializeOnLoad]
	public static class StandardShortcutDisabler
	{
		static string _emptyProfileName = "StorylineEmptyProfile";
		static bool _isEnabled = true;
		static string _previousProfileName = "";

		static void CreateEmptyShortcutProfile()
		{
			ShortcutManager.instance.CreateProfile(_emptyProfileName);

			_previousProfileName = ShortcutManager.instance.activeProfileId;
			ShortcutManager.instance.activeProfileId = _emptyProfileName;

			foreach (string pid in ShortcutManager.instance.GetAvailableShortcutIds())
				ShortcutManager.instance.RebindShortcut(pid, ShortcutBinding.empty);

			ShortcutManager.instance.activeProfileId = _previousProfileName;
		}

		public static void EnableStandardShortcuts()
		{
			if (_isEnabled)
				return;

			if (ShortcutManager.instance.activeProfileId != _emptyProfileName)
			{
				_isEnabled = true;
				return;
			}

			if (_previousProfileName != "")
				ShortcutManager.instance.activeProfileId = _previousProfileName;
			else if (ShortcutManager.instance.activeProfileId != ShortcutManager.defaultProfileId)
				ShortcutManager.instance.activeProfileId = ShortcutManager.defaultProfileId;
			_isEnabled = true;
		}

		public static void DisableStandardShortcuts()
		{
			if (!_isEnabled)
				return;

			if (ShortcutManager.instance.activeProfileId == _emptyProfileName)
			{
				_isEnabled = false;
				return;
			}

			if (!ShortcutManager.instance.GetAvailableProfileIds().ToList().Contains(_emptyProfileName))
				CreateEmptyShortcutProfile();

			_previousProfileName = ShortcutManager.instance.activeProfileId;
			ShortcutManager.instance.activeProfileId = _emptyProfileName;
			_isEnabled = false;
		}

		static StandardShortcutDisabler()
		{
			EnableStandardShortcuts();
		}
	}
}