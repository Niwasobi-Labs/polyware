using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace PolyWare.Entities {
	[Serializable]
	public struct OverrideValue<T> {
		public bool Enabled;
		public T Value;
	}

	public class OverrideableEntityDataDrawer<T> : OdinValueDrawer<T> where T : IEntityData {
		protected override void DrawPropertyLayout(GUIContent label) {
			if (ValueEntry.SmartValue == null) {
				CallNextDrawer(label);
				return;
			}

			var data = ValueEntry.SmartValue;
			var fields = data.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			foreach (var field in fields) {
				if (!Attribute.IsDefined(field, typeof(OverrideableAttribute)))
					continue;

				if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(OverrideValue<>)) {
					GUILayout.BeginVertical(GUI.skin.box);
					GUILayout.Label(field.Name, EditorStyles.boldLabel);

					object boxedFieldValue = field.GetValue(data);
					var enabledField = field.FieldType.GetField("Enabled");
					var valueField = field.FieldType.GetField("Value");

					bool enabled = (bool)enabledField.GetValue(boxedFieldValue);
					enabled = EditorGUILayout.ToggleLeft("Override", enabled);
					enabledField.SetValue(boxedFieldValue, enabled);

					if (enabled) {
						Type valueType = field.FieldType.GetGenericArguments()[0];
						object value = valueField.GetValue(boxedFieldValue);
						object newValue = null;

						if (valueType == typeof(int)) {
							newValue = EditorGUILayout.IntField((int)value);
						} else if (valueType == typeof(float)) {
							newValue = EditorGUILayout.FloatField((float)value);
						} else if (typeof(UnityEngine.Object).IsAssignableFrom(valueType)) {
							newValue = EditorGUILayout.ObjectField((UnityEngine.Object)value, valueType, true);
						}

						if (newValue != null && !Equals(newValue, value)) {
							valueField.SetValue(boxedFieldValue, newValue);
						}
					}

					field.SetValue(data, boxedFieldValue);
					GUILayout.EndVertical();
				}
			}
		}
	}
}
