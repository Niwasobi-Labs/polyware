using System.Linq;
using PolyWare.Debug;

namespace PolyWare.Items {
	public class SlottedInventory<T> : IInventory where T : Item {
		
		protected readonly T[] slots;
		protected int activeSlot = 0;
		public T ActiveItem => slots[activeSlot];

		protected SlottedInventory(T[] startingSlots) {
			slots = startingSlots;
		}

		public void Clear() {
			foreach (T item in slots.Where(item => item)) {
				item.RemoveFromWorld();
			}
		}

		/// <summary>
		/// this will drop an item at the given slot if there is an item there
		/// </summary>
		/// <param name="item"></param>
		/// <param name="slot"></param>
		/// <returns></returns>
		public bool AddItemAtSlot(Item item, int slot) {
			if (slots[slot] && !slots[slot].Drop()) {
				return false;
			}
			
			slots[slot] = (T)item;
			
			return true; // later, we may need to block dropping equipment
		} 
		
		public virtual int AddItem(Item item) {
			if (item is not T) {
				Logger.Error($"Inventory doesnt support {item.GetType()}");
				return (int)IInventory.InventoryError.InvalidItem;
			}

			if (!CheckForOpenSlot(out int newSlot)) newSlot = activeSlot;

			if (!AddItemAtSlot(item, newSlot)) return (int)IInventory.InventoryError.InventoryFull;

			return newSlot;
		}

		public bool DropActiveItem() {
			return DropItemAtSlot(activeSlot);
		}
		
		public bool DropItemAtSlot(int slot) {
			if (!slots[slot]) return true;
			if (!slots[slot].Drop()) return false;
			
			slots[slot] = null;
			return true;
		}
		
		private bool CheckForOpenSlot(out int openSlot) {
			for (int i = 0; i < slots.Length; ++i) {
				if (slots[i]) continue;
				
				openSlot = i;
				return true;
			}	
					
			openSlot = -1;
			return false;
		}
		
		public bool CycleActiveItem(bool right = true) {
			return right ? CycleActiveItemRight() : CycleActiveItemLeft();
		}
		
		private bool CycleActiveItemRight() {
			int newSlot = activeSlot;

			for (int i = 0; i < slots.Length; i++) {
				newSlot = (newSlot + 1) % slots.Length;
				if (!slots[newSlot]) continue;
				activeSlot = newSlot;
				return true;
			}

			return false;
		}

		private bool CycleActiveItemLeft() {
			int newSlot = activeSlot;

			for (int i = 0; i < slots.Length; i++) {
				newSlot = (newSlot - 1 + slots.Length) % slots.Length;
				if (!slots[newSlot]) continue;
				activeSlot = newSlot;
				return true;
			}

			return false;
		}
	}
}