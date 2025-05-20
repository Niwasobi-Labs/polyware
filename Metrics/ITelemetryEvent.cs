using System.IO;

namespace PolyWare.Metrics {
	public interface ITelemetryEvent {
		public void SerializeTo(TextWriter writer);
	}
}