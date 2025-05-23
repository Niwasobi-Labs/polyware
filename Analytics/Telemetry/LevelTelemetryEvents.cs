using System.Text;

namespace PolyWare.Analytics.Telemetry {
	public class LevelTelemetryEvents {
		public const string LevelPrefix = "level: ";
		public const string LevelTimePrefix = "level time: ";
		
		public readonly struct LevelStart : ITelemetryEvent {
			public int EventID => (int)Telemetry.EventID.LevelStart;
			public string EventName => eventName;
			private const string eventName = "LevelStarted";
			private readonly string levelName;

			public LevelStart(string name) {
				levelName = name;
			}

			public void SerializeTo(StringBuilder stringBuilder) {
				stringBuilder.Append(LevelPrefix);
				stringBuilder.Append(levelName);
			}
		}
		
		public readonly struct LevelComplete : ITelemetryEvent {
			public int EventID => (int)Telemetry.EventID.LevelComplete;
			public string EventName => eventName;
			private const string eventName = "LevelCompleted";
			
			private readonly string levelName;
			private readonly string levelTime;

			public LevelComplete(string name, string time) {
				levelName = name;
				levelTime = time;
			}

			public void SerializeTo(StringBuilder stringBuilder) {
				stringBuilder.Append(LevelPrefix);
				stringBuilder.Append(levelName);
				
				stringBuilder.Append(TelemetryManager.EventSerializationSeparator);
				
				stringBuilder.Append(LevelTimePrefix);
				stringBuilder.Append(levelTime);
			}
		}

		public readonly struct LevelReset : ITelemetryEvent {
			public int EventID => (int)Telemetry.EventID.LevelReset;
			public string EventName => eventName;
			private const string eventName = "LevelReset";
			
			private readonly string levelName;
			private readonly string levelTime;
			
			public LevelReset(string name, string time) {
				levelName = name;
				levelTime = time;
			}

			public void SerializeTo(StringBuilder stringBuilder) {
				stringBuilder.Append(LevelPrefix);
				stringBuilder.Append(levelName);

				stringBuilder.Append(TelemetryManager.EventSerializationSeparator);
				
				stringBuilder.Append(LevelTimePrefix);
				stringBuilder.Append(levelTime);
			}
		}
	}
}