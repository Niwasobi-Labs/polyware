namespace PolyWare.Game {
	public enum InventoryError {
		InvalidItem = -1,
		InventoryFull = -2,
	}
	
	public class InventorySlot<T> where T : ItemDefinition {
		public InventorySlot(T item, int startingStack) {
			Item = item;
			Stack = startingStack;
		}
		
		public T Item;
		public int Stack;
	}

	public interface IInventory<in T> {
		public int Size { get; }
		public int Add(T item);
		public void Clear();
	}
}