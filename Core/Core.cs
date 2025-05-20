using PolyWare.AssetManagement;
using PolyWare.Audio;
using PolyWare.Input;
using PolyWare.Metrics;
using PolyWare.UI;
using UnityEngine.EventSystems;

namespace PolyWare {
	public static class Core {
		public static Constants Constants { get; private set; }
		public static Collector Collector { get; private set; }
		public static EventSystem EventSystem { get; private set; }
		public static InputManager Input { get; private set; }
		public static SfxManager SfxManager { get; private set; }
		public static UIManager UI { get; private set; }
		public static Game Game { get; private set; }
		public static TelemetryManager Telemetry { get; private set; }

		public static void Setup(Bootstrapper bootstrapper) {
			Constants = bootstrapper.Constants;
			Collector = bootstrapper.Collector;
			EventSystem = bootstrapper.EventSystem;
			Input = bootstrapper.Input;
			SfxManager = bootstrapper.SfxManager;
			UI = bootstrapper.UI;
			Game = bootstrapper.Game;
			Telemetry = new TelemetryManager();
		}
		
		public static void Initialize() {
			Collector.Initialize();
			Input.Initialize();
			UI.Initialize();
			Game.Initialize();
		}
	}
}