using PolyWare.Interaction;

namespace PolyWare.Items {
	public interface IItem {
		public ItemData Data { get; }
		public void Pickup(IProximityUser user);
		public bool Drop();
		public void Use();
		public void CancelUse();
		public bool CanUse { get; }
		public void RemoveFromWorld();
	}
}