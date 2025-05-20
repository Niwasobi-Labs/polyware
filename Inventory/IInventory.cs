namespace PolyWare.Items {
	public interface IInventory {
		public enum InventoryError {
			InvalidItem = -1,
			InventoryFull = -2,
		}
		
		/// <summary>
		/// returns a InventoryError if failed, other numbers are up to the implementer
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int AddItem(Item item);
		public void Clear();
	}
}