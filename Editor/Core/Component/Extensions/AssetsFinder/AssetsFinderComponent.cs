using System;
using System.Collections.Generic;
using System.Linq;
using Farious.Gist.Editor.AssetFlow.Database;
using Farious.Gist.UIComponents.Components;

namespace Farious.Gist.UIComponents
{
	public class AssetsFinderComponent<TAsset> : NodeComponent
		where TAsset : UnityEngine.Object
	{
		public IEnumerable<TAsset> GetAssociatedAssets() =>
			AssetDatabaseUtility.FindObjects<TAsset>();

		public TAsset FindFirstAssociatedAsset()
		{
			var result = GetAssociatedAssets();

			if (!result.Any())
				throw new NullReferenceException("Asset not found!");

			return result.ElementAt(0);
		}
	}
}