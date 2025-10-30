using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PolyWare.Core {
	public abstract class Registry<T> : ScriptableObject where T : Component {
		
		[SerializeField] internal PrefabSearchMode searchMode = PrefabSearchMode.Global;
		[SerializeField] internal List<T> allPrefabs;
		protected Dictionary<Type, T> prefabDictionary;

		protected IReadOnlyList<T> GetPrefabs() {
			return allPrefabs.AsReadOnly();
		}

		public void Initialize() {
			prefabDictionary = new Dictionary<Type, T>();

			foreach (T prefab in allPrefabs.Where(prefab => !prefabDictionary.TryAdd(prefab.GetType(), prefab)))
				Log.Error("Duplicate prefab type found: " + prefab.name);
		}

		public T GetPrefab(Type prefabType, Transform parent = null) {
			if (prefabDictionary.TryGetValue(prefabType, out T prefab)) return Instantiate(prefab, parent);

			Log.Error($"Could not find prefab of type: {prefabType}");
			return null;
		}
	}
}