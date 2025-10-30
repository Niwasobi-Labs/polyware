namespace PolyWare.Game {
	public abstract class ItemDefinition : EntityDefinition {
		public string ItemID; // todo: make an enum of sorts
		public string Name;
		public string Description;
		public int MaxStack = 1;
	}
}