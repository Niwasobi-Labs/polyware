using UnityEditor;
using UnityEngine;

namespace PolyWare.AssetManagement.Registries {
	[CreateAssetMenu(fileName = "LevelRegistry", menuName = "PolyWare/Registries/Level Registry")]
	public class LevelRegistry : Registry<Levels.Level> { }
	
#if UNITY_EDITOR
	[CustomEditor(typeof(LevelRegistry))]
	public class LevelRegistryEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			if (GUILayout.Button("Refresh")) {
				RegistryEditor.RefreshRegistry<Levels.Level>(target);
				EditorUtility.SetDirty(target);
			}
			DrawDefaultInspector();
		}
	}
#endif
}