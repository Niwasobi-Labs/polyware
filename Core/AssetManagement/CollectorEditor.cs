#if UNITY_EDITOR
using PolyWare.Editor;
using UnityEditor;
using UnityEngine;

namespace PolyWare.AssetManagement {
	[CustomEditor(typeof(Collector))]
	public class CollectorEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			var collector = (Collector)target;
			if (GUILayout.Button("Refresh")) {
				collector.collections = GetPrefabs.GetAllScriptableObjectsOfType<Collection>(false, PrefabSearchMode.Global, AssetDatabase.GetAssetPath(target));
				EditorUtility.SetDirty(target);
			}

			DrawDefaultInspector();
		}
	}
}
#endif