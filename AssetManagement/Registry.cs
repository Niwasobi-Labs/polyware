using System;
using System.Collections.Generic;
using System.Linq;
using PolyWare.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PolyWare.AssetManagement {
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
				Debug.Log.Error("Duplicate prefab type found: " + prefab.name);
		}

		public T GetPrefab(Type prefabType, Transform parent = null) {
			if (prefabDictionary.TryGetValue(prefabType, out T prefab)) return Instantiate(prefab, parent);

			Debug.Log.Error($"Could not find prefab of type: {prefabType}");
			return null;
		}
	}
	
	// unity doesn't support generic custom editors, here's a helper class to refresh the registry
#if UNITY_EDITOR
	public static class RegistryEditor {
		public static void RefreshRegistry<T>(Object target) where T : Component {
			var registry = (Registry<T>)target;
			registry.allPrefabs = GetPrefabs.GetAllPrefabsWithComponent<T>(false, registry.searchMode, AssetDatabase.GetAssetPath(target));
		} 
	}
#endif
}