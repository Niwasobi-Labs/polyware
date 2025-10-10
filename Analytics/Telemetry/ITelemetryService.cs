using PolyWare.Core.Services;

namespace PolyWare.Analytics.Telemetry {
	public interface ITelemetryService : IService {
		public void LogEvent<T>(T @event) where T : ITelemetryEvent;
	}
}