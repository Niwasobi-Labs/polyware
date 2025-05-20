using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace PolyWare.Editor {
	public class LockInspector {
		[MenuItem("Tools/Lock Inspector %l")]
		public static void Lock() {
			ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;

			foreach (var activeEditor in ActiveEditorTracker.sharedTracker.activeEditors) {
				if (activeEditor.target is not Transform transform) continue;

				PropertyInfo propInfo = transform.GetType().GetProperty("constrainProportionsScale", BindingFlags.NonPublic | BindingFlags.Instance);

				if (propInfo == null) continue;
				
				bool value = (bool) propInfo.GetValue(transform, null);
				propInfo.SetValue(transform, !value, null);
			}
			
			ActiveEditorTracker.sharedTracker.ForceRebuild();
		}

		public static bool Valid() {
			return ActiveEditorTracker.sharedTracker.activeEditors.Length != 0;
		}
	}
}
