#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PolyWare.Editor {
	public class GetPrefabs : MonoBehaviour {
		// ReSharper disable Unity.PerformanceAnalysis
		public static List<T> GetAllPrefabsWithComponent<T>(bool allowDuplicates) where T : Component {
			string[] searchFolders = { "Assets/_Project/Prefabs" };

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
					Debug.Logger.Error($"Duplicate prefab with component '{typeof(T)}' found: {name}, skipping.");
					continue;
				}

				result.Add(component);
				foundNames.Add(name);
			}

			Debug.Logger.Message($"Collected {result.Count} prefabs with component {typeof(T).Name} from selected folders.");
			return result;
		}

		// ReSharper disable Unity.PerformanceAnalysis
		public static List<T> GetAllScriptableObjectsOfType<T>(bool allowDuplicates) where T : ScriptableObject {
			string[] searchFolders = { "Assets/_Project/Prefabs" };

			string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}", searchFolders);

			List<T> result = new();
			HashSet<string> foundNames = new();

			foreach (string guid in guids) {
				string path = AssetDatabase.GUIDToAssetPath(guid);
				var asset = AssetDatabase.LoadAssetAtPath<T>(path);
				if (!asset) continue;

				string name = asset.name;

				if (!allowDuplicates && foundNames.Contains(name)) {
					Debug.Logger.Error($"Duplicate ScriptableObject of type '{typeof(T)}' found: {name}, skipping.");
					continue;
				}

				result.Add(asset);
				foundNames.Add(name);
			}

			Debug.Logger.Message($"Collected {result.Count} ScriptableObjects of type {typeof(T).Name} from selected folders.");
			return result;
		}
	}
}

#endif