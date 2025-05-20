using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolyWare.AssetManagement.Collections {
	[Serializable]
	public struct StringAssetInfo<T> {
		public string Name;
		public T Asset;
	}
	
	public abstract class StringCollection<T> : Collection {
		[SerializeField] private List<StringAssetInfo<T>> items = new ();

		private readonly Dictionary<string, T> dictionary = new();

		public override void Initialize() {
			foreach (var value in items) {
				try {
					dictionary.Add(value.Name, value.Asset);
				}
				catch (Exception e) {
					Debug.Logger.Error(e.Message);
				}
			}
		}

		public bool Contains(string key) {
			return dictionary.ContainsKey(key);
		}
		
		public T Get(string key) {
			if (dictionary.TryGetValue(key, out T value)) return value;
			Debug.Logger.Error("Key not found: " + key);
			return default;
		}
	}
}