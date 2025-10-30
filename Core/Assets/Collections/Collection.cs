using System.Collections.Generic;
using UnityEngine;

namespace PolyWare.Core {
	public abstract class Collection<TKey, TValue> : ScriptableObject {
		protected Dictionary<TKey, TValue> dictionary;
		
		public abstract void Initialize();
		
		public bool Contains(TKey key) {
			return dictionary.ContainsKey(key);
		}
		
		public TValue Get(TKey key) {
			if (dictionary.TryGetValue(key, out TValue value)) return value;
			Log.Error("Key not found: " + key);
			return default;
		}

		public bool TryGet(TKey key, out TValue value) {
			return dictionary.TryGetValue(key, out value);
		}
	}
}