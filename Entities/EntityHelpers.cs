using System;
using System.Reflection;

namespace PolyWare.Entities {
	public static class EntityHelpers {
		public static void ApplyOverrides<T>(T target, T overrides) {
			Type type = target.GetType();
			const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

			FieldInfo[] fields = type.GetFields(flags);
			foreach (FieldInfo field in fields) {
				if (!Attribute.IsDefined(field, typeof(OverrideableAttribute))) continue;
				object overrideValue = field.GetValue(overrides);
				field.SetValue(target, overrideValue);
			}
		}
	}
}