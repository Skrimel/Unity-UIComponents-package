using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Farious.Gist.UIComponents
{
	[CreateAssetMenu(menuName = "Gist/Dev/Settings carrier")]
	public class SettingsCarrier : ScriptableObject
	{
		[SerializeField] private List<StyleSheet> _styleSheets = new List<StyleSheet>();
		public IEnumerable<StyleSheet> StyleSheets => _styleSheets;

		public StyleSheet FindSheetByName(string name)
		{
			var result = _styleSheets.Find(sheet => sheet.name == name);

			if (result == null)
				throw new NullReferenceException($"Sheet with name {name} not found!");

			return result;
		}
	}
}