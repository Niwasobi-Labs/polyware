using UnityEngine;

namespace PolyWare.ActionGame.AimAssist {
	public interface IAimAssistTarget {
		public Transform AimPoint { get; }
	}
}