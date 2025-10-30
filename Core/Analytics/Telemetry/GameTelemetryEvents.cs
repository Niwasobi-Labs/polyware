using System.Text;

namespace PolyWare.Core {
	public readonly struct GameStartTelemetryEvent : ITelemetryEvent {
		public int EventID => (int)TelemetryEventId.LevelStart;
		public string EventName => eventName;
		private const string eventName = "GameStarted";
		
		public void SerializeTo(StringBuilder stringBuilder) {
			// noop
		}
	}
}