using System;
using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Core {
	[Serializable]
	public class AssetData<TKey, TValue> {
		public string Name;
		public TKey AssetID;
		public TValue Asset;
	}

	public class AssetCollection<TKey, TAsset, TValue> : Collection<TKey, TValue>
		where TKey : notnull
		where TAsset : AssetData<TKey, TValue> {
		[SerializeField] private List<TAsset> items = new();

		public override void Initialize() {
			foreach (TAsset value in items)
				try {
					dictionary.Add(value.AssetID, value.Asset);
				}
				catch (Exception e) {
					Log.Error(e.Message);
					return;
				}
		}
	}
}