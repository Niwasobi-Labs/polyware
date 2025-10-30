using UnityEditor;
using UnityEngine;

namespace PolyWare.Core {
	[CreateAssetMenu(menuName = "PolyWare/Registries/WidgetRegistry", fileName = "New WidgetRegistry")]
	public class WidgetRegistry : Registry<Widget> { }
	
#if UNITY_EDITOR
	[CustomEditor(typeof(WidgetRegistry))]
	public class WidgetRegistryEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			if (GUILayout.Button("Refresh")) {
				RegistryEditor.RefreshRegistry<Widget>(target);
				EditorUtility.SetDirty(target);
			}
			DrawDefaultInspector();
		}
	}
#endif
}