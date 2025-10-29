using Sirenix.OdinInspector;

namespace PolyWare.Inventory {
	public class SlotInventory<T> : IInventory<T> {
		[ShowInInspector] public T[] Slots { get; private set; }

		public int Size => Slots.Length;
		[ShowInInspector] public int ActiveIndex { get; private set; } = -1;

		public SlotInventory(int capacity) {
			Slots = new T[capacity];
		}

		public int Add(T item) {
			for (int i = 0; i < Size; i++) {
				if (Slots[i] != null) continue;
				Slots[i] = item;
				return i;
			}

			return (int)InventoryError.InventoryFull; // inventory full
		}

		public void Clear() {
			for (int i = 0; i < Slots.Length; i++) {
				Slots[i] = default;
			}
		}

		public bool SetSlot(int index, T item) {
			if (index < 0 || index >= Size) return false;
			Slots[index] = item;
			return true;
		}

		public bool SetActiveSlot(int index) {
			if (index < 0 || index >= Size || Slots[index] == null) return false;
			ActiveIndex = index;
			return true;
		}

		public void ClearActiveSlot() {
			ClearSlot(ActiveIndex);
		}
		
		public void ClearSlot(int index) {
			Slots[index] = default;
		}

		public bool CycleRight() {
			if (Size == 0) return false;

			int start = ActiveIndex >= 0 ? ActiveIndex : -1;

			for (int i = 1; i <= Size; i++) {
				int check = (start + i) % Size;
				if (Slots[check] == null) continue;
				ActiveIndex = check;
				return true;
			}

			return false;
		}

		public bool CycleLeft() {
			if (Size == 0) return false;

			int start = ActiveIndex >= 0 ? ActiveIndex : Size;

			for (int i = 1; i <= Size; i++) {
				int check = (start - i + Size) % Size;
				if (Slots[check] == null) continue;
				ActiveIndex = check;
				return true;
			}

			return false;
		}

		public T ActiveItem => (ActiveIndex >= 0 && ActiveIndex < Size) ? Slots[ActiveIndex] : default(T);
	}
}