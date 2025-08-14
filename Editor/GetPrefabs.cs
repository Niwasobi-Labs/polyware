#if UNITY_EDITOR
using System.Collections.Generic;
using Log = PolyWare.Debug.Log;
using UnityEngine;
using UnityEditor;

namespace PolyWare.Editor {
	public static class GetPrefabs {
		// ReSharper disable Unity.PerformanceAnalysis
		public static List<T> GetAllPrefabsWithComponent<T>(bool allowDuplicates, PrefabSearchMode searchMode, string startingPath) where T : Component {
			string[] searchFolders = searchMode == PrefabSearchMode.Global ? new[] { "Assets/_Project/Prefabs" } : new[] { startingPath };

			string[] prefabGUIDs = AssetDatabase.FindAssets("t:Prefab", searchFolders);

			List<T> result = new();
			HashSet<string> foundNames = new();

			foreach (string guid in prefabGUIDs) {
				string path = AssetDatabase.GUIDToAssetPath(guid);
				var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
				
				if (!prefab) continue;

				if (!prefab.TryGetComponent(out T component)) continue;
				
				string name = component.name;

				if (!allowDuplicates && foundNames.Contains(name)) {
					Log.Error($"Duplicate prefab with component '{typeof(T)}' found: {name}, skipping.");
					continue;
				}

				result.Add(component);
				foundNames.Add(name);
			}

			Log.Message($"Collected {result.Count} prefabs with component {typeof(T).Name} from selected folders.");
			return result;
		}

		// ReSharper disable Unity.PerformanceAnalysis
		public static List<T> GetAllScriptableObjectsOfType<T>(bool allowDuplicates, PrefabSearchMode searchMode, string startingPath) where T : ScriptableObject {
			string[] searchFolders = searchMode == PrefabSearchMode.Global ? new[] { "Assets/_Project/Prefabs" } : new[] { startingPath };

			string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", searchFolders);

			List<T> result = new();
			HashSet<string> foundNames = new();

			foreach (string guid in guids) {
				string path = AssetDatabase.GUIDToAssetPath(guid);
				var asset = AssetDatabase.LoadAssetAtPath<T>(path);
				if (!asset) continue;

				string name = asset.name;

				if (!allowDuplicates && foundNames.Contains(name)) {
					Log.Error($"Duplicate ScriptableObject of type '{typeof(T)}' found: {name}, skipping.");
					continue;
				}

				result.Add(asset);
				foundNames.Add(name);
			}

			Log.Message($"Collected {result.Count} ScriptableObjects of type {typeof(T).Name} from selected folders.");
			return result;
		}
	}
}

#endif