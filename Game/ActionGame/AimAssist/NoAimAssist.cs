using UnityEngine;

namespace PolyWare.ActionGame.AimAssist {
	public class NoAimAssist : AimAssistStrategy {
		public NoAimAssist(AimAssistInfo aimAssistData, Transform spawnPoint) : base(aimAssistData, spawnPoint) { }
		public override void RunAimAssist() {
			// noop
		}
	}
}