using PolyWare.Core;

namespace PolyWare.Game {
	public class DebugTelegrapher : Telegrapher {
		public override void StartTelegraphing(float duration) {
			Log.Message($"Starting telegraph for {duration}");
		}
		public override void StopTelegraphing() {
			Log.Message("Stopping telegraph");
		}
	}
}