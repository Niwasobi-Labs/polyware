using UnityEditor;
using UnityEngine;

namespace PolyWare.AssetManagement.Registries {
	[CreateAssetMenu(fileName = "ScreenRegistry", menuName = "PolyWare/Registries/Screen Registry")]
	public class ScreenRegistry : Registry<PolyWare.UI.Screen> { }
	
#if UNITY_EDITOR
	[CustomEditor(typeof(ScreenRegistry))]
	public class ScreenRegistryEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			if (GUILayout.Button("Refresh")) {
				RegistryEditor.RefreshRegistry<PolyWare.UI.Screen>(target);
				EditorUtility.SetDirty(target);
			}
			DrawDefaultInspector();
		}
	}
#endif
}