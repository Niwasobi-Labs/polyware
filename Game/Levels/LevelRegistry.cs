using PolyWare.Core;
using UnityEditor;
using UnityEngine;

namespace PolyWare.Game {
	[CreateAssetMenu(menuName = "PolyWare/Registries/LevelRegistry", fileName = "New LevelRegistry")]
	public class LevelRegistry : Registry<Level> { }
	
#if UNITY_EDITOR
	[CustomEditor(typeof(LevelRegistry))]
	public class LevelRegistryEditor : Editor {
		public override void OnInspectorGUI() {
			if (GUILayout.Button("Refresh")) {
				RegistryEditor.RefreshRegistry<Level>(target);
				EditorUtility.SetDirty(target);
			}
			DrawDefaultInspector();
		}
	}
#endif
}