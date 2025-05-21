namespace PolyWare.Telemetry {
	public enum EventID {
		// game specific events (0-9,999)
		// polyware specific events (10,000 - 19,999)
		GameStart = 10000,
		LevelStart = 10001,
		LevelReset = 10002,
		LevelComplete = 10003
	}
}