using System.Text;

namespace PolyWare.Analytics.Telemetry {
	public interface ITelemetryEvent {
		/// <summary>
		/// EventIDs must be unique and have a limit of 10000 for game specific events
		/// </summary>
		public int EventID { get; }
		public string EventName { get; }
		public void SerializeTo(StringBuilder stringBuilder);
	}
}