using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolyWare.AssetManagement.Collections {
	[Serializable]
	public class AssetData<TKey, TValue> {
		public string Name;
		public TKey AssetID;
		public TValue Asset;
	}

	public class AssetCollection<TKey, TAsset, TValue> : Collection
		where TKey : notnull
		where TAsset : AssetData<TKey, TValue> {
		[SerializeField] private List<TAsset> items = new();

		private readonly Dictionary<TKey, TValue> dictionary = new();

		public override void Initialize() {
			foreach (TAsset value in items)
				try {
					dictionary.Add(value.AssetID, value.Asset);
				}
				catch (Exception e) {
					Debug.Logger.Error(e.Message);
					return;
				}
		}

		public bool Contains(TKey key) {
			return dictionary.ContainsKey(key);
		}
		
		public TValue Get(TKey key) {
			if (dictionary.TryGetValue(key, out TValue value)) return value;
			Debug.Logger.Error("Key not found: " + key);
			return default;
		}
	}
}