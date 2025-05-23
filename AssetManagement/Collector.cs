using System;
using System.Collections.Generic;
using PolyWare.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace PolyWare.AssetManagement {
	[CreateAssetMenu(fileName = "Collector", menuName = "PolyWare/Collector")]
	public class Collector : ScriptableObject {
		[SerializeField] [FormerlySerializedAs("collections")] internal List<Collection> collections = new List<Collection>();

		private readonly Dictionary<Type, Collection> collectionDictionary = new();

		public void Initialize() {
			foreach (Collection collection in collections)
				try {
					collectionDictionary.Add(collection.GetType(), collection);
					collection.Initialize();
				}
				catch (Exception e) {
					Debug.Log.Error(e.Message);
					throw;
				}
		}

		public bool Contains<T>() where T : Collection {
			return collectionDictionary.ContainsKey(typeof(T));
		}

		public T Get<T>() where T : Collection {
			if (collectionDictionary.TryGetValue(typeof(T), out Collection value)) return value as T;
			return null;
		}
	}
	
#if UNITY_EDITOR
	[CustomEditor(typeof(Collector))]
	public class CollectorEditor : UnityEditor.Editor {
		public override void OnInspectorGUI() {
			var collector = (Collector)target;
			if (GUILayout.Button("Refresh")) {
				collector.collections = GetPrefabs.GetAllScriptableObjectsOfType<Collection>(false);
				EditorUtility.SetDirty(target);
			}

			DrawDefaultInspector();
		}
	}
#endif
}