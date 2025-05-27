namespace PolyWare.Events {
	public interface IEvent { }
	
	public struct PlayerInteractionEvent : IEvent {
		public bool HasEntered;
		public string Prompt;

		public PlayerInteractionEvent(bool hasEntered, string prompt) {
			HasEntered = hasEntered;
			Prompt = prompt;
		}
	}
}



