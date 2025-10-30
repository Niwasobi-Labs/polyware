using UnityEditor;
using UnityEngine;

namespace PolyWare.Core {
	[CreateAssetMenu(menuName = "PolyWare/Registries/ScreenRegistry", fileName = "New ScreenRegistry")]
	public class ScreenRegistry : Registry<UIScreen> { }
	
#if UNITY_EDITOR
	[CustomEditor(typeof(ScreenRegistry))]
	public class ScreenRegistryEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			if (GUILayout.Button("Refresh")) {
				RegistryEditor.RefreshRegistry<UIScreen>(target);
				EditorUtility.SetDirty(target);
			}
			DrawDefaultInspector();
		}
	}
#endif
}