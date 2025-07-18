namespace PolyWare.Items {
	public interface IUsable {
		bool CanUse { get; }
		void Use();
		void CancelUse();
	}
}