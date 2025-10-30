namespace PolyWare.Game {
	public interface IInteractable {
		public void Interact(IProximityUser user);
		public string InteractionPrompt { get; }
	}
}