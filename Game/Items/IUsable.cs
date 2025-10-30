namespace PolyWare.Game {
	public interface IUsable {
		bool CanUse { get; }
		void Use();
		void StopUsing();
	}
}