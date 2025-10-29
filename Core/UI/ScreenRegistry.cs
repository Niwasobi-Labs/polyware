using PolyWare.AssetManagement;
using UnityEditor;
using UnityEngine;

namespace PolyWare.UI {
	[CreateAssetMenu(menuName = "PolyWare/Registries/ScreenRegistry", fileName = "New ScreenRegistry")]
	public class ScreenRegistry : Registry<PolyWare.UI.UIScreen> { }
	
#if UNITY_EDITOR
	[CustomEditor(typeof(ScreenRegistry))]
	public class ScreenRegistryEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			if (GUILayout.Button("Refresh")) {
				RegistryEditor.RefreshRegistry<PolyWare.UI.UIScreen>(target);
				EditorUtility.SetDirty(target);
			}
			DrawDefaultInspector();
		}
	}
#endif
}