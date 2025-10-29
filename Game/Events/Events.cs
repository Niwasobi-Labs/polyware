namespace PolyWare.Core.Events {
	public interface IEvent { }
	
	public struct PlayerNearbyInteractionEvent : IEvent {
		public bool HasEntered;
		public string Prompt;

		public PlayerNearbyInteractionEvent(bool hasEntered, string prompt) {
			HasEntered = hasEntered;
			Prompt = prompt;
		}
	}
}



