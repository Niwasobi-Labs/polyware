using UnityEngine;

namespace PolyWare.Shooter.AimAssist {
	public interface IAimAssistTarget {
		public Transform AimPoint { get; }
	}
}