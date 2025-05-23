using UnityEngine;

namespace PolyWare.Shooter.AimAssist {
	public class NoAimAssist : AimAssistStrategy {
		public NoAimAssist(AimAssistInfo aimAssistData, Transform spawnPoint) : base(aimAssistData, spawnPoint) { }
		public override void RunAimAssist() {
			// noop
		}
	}
}