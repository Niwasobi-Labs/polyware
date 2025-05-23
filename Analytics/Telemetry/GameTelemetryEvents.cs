using System.Text;

namespace PolyWare.Analytics.Telemetry {
	public readonly struct GameStartTelemetryEvent : ITelemetryEvent {
		public int EventID => (int)PolyWare.Analytics.Telemetry.EventID.LevelStart;
		public string EventName => eventName;
		private const string eventName = "GameStarted";
		
		public void SerializeTo(StringBuilder stringBuilder) {
			// noop
		}
	}
}