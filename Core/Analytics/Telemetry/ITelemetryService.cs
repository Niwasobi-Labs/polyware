using PolyWare.Core.Services;

namespace PolyWare.Analytics {
	public interface ITelemetryService : IService {
		public void LogEvent<T>(T @event) where T : ITelemetryEvent;
	}
}