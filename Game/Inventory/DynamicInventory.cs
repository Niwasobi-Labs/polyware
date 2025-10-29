using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace PolyWare.Inventory {
	[Serializable]
	public class DynamicInventory<T> : IInventory<T> {
		[ShowInInspector] public List<T> Items { get; protected set; }

		public int Size => Items.Count;
		[ShowInInspector] public int ActiveIndex { get; private set; } = -1;

		public DynamicInventory() {
			Items = new List<T>();
		}
		
		public DynamicInventory(List<T> items) {
			Items = items;
		}
		
		public DynamicInventory(int startingCapacity) {
			Items = new List<T>(startingCapacity);
		}
		
		public int Add(T item) {
			Items.Add(item);
			return Items.Count - 1;
		}

		public void Clear() {
			Items.Clear();
		}

		public bool SetActiveSlot(int slot) {
			if (slot < 0 || slot >= Items.Count) return false;
			ActiveIndex = slot;
			return true;
		}
		
		public bool CycleRight() {
			int start = ActiveIndex;

			for (int i = 1; i <= Items.Count; i++) {
				int check = (start + i) % Items.Count;
				if (Items[check] == null) continue;
				ActiveIndex = check;
				return true;
			}

			return false;
		}

		public bool CycleLeft() {
			int start = ActiveIndex;

			for (int i = 1; i <= Items.Count; i++) {
				int check = (start - i + Items.Count) % Items.Count;
				if (Items[check] == null) continue;
				ActiveIndex = check;
				return true;
			}

			return false;
		}

		public T ActiveSlot => ActiveIndex >= 0 ? Items[ActiveIndex] : default(T);
	}
}