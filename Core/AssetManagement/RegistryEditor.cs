#if UNITY_EDITOR
using PolyWare.Editor;
using UnityEditor;
using UnityEngine;

namespace PolyWare.AssetManagement {
	public static class RegistryEditor {
		public static void RefreshRegistry<T>(Object target) where T : Component {
			var registry = (Registry<T>)target;
			registry.allPrefabs = GetPrefabs.GetAllPrefabsWithComponent<T>(false, registry.searchMode, AssetDatabase.GetAssetPath(target));
		} 
	}
}
#endif