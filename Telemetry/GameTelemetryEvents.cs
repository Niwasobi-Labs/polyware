using System.Text;
using PolyWare.Telemetry;

namespace PolyWare.Game {
	public readonly struct GameStartTelemetryEvent : ITelemetryEvent {
		public int EventID => (int)PolyWare.Telemetry.EventID.LevelStart;
		public string EventName => eventName;
		private const string eventName = "GameStarted";
		
		public void SerializeTo(StringBuilder stringBuilder) {
			// noop
		}
	}
}