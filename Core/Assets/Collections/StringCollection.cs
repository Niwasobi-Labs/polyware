using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PolyWare.Core {
	[Serializable]
	public struct StringAssetInfo<T> {
		public string Name;
		public T Asset;
	}
	
	public abstract class StringCollection<T> : Collection<string, T> {
		[SerializeField] private List<StringAssetInfo<T>> items = new ();

		public string RandomItem(int startingIndex) => items[Random.Range(startingIndex, items.Count - 1)].Name;
		
		public override void Initialize() {
			dictionary = new Dictionary<string, T>();
			
			foreach (var value in items) {
				try {
					dictionary.Add(value.Name, value.Asset);
				}
				catch (Exception e) {
					Log.Error(e.Message);
				}
			}
		}
	}
}