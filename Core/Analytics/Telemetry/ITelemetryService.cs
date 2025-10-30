namespace PolyWare.Core {
	public interface ITelemetryService : IService {
		public void LogEvent<T>(T @event) where T : ITelemetryEvent;
	}
}