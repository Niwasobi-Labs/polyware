using UnityEngine;

namespace PolyWare.Shooter.AimAssist {
	public enum AimAssistMode {
		None,
		SphereCast,
		ConeCast
	}
	
	public interface IAimAssistStrategy {
		public void RunAimAssist();
		public Collider GetTarget { get; }
	}
}