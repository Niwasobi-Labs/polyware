using PolyWare.Core.Entities;

namespace PolyWare.Items {
	public abstract class ItemDefinition : EntityDefinition {
		public abstract int ItemID { get; }
		public string Name;
		public string Description;
		public int MaxStack = 1;
	}
}