using System;
using UnityEngine;

namespace PolyWare.Shooter.AimAssist {
	[Serializable]
	public class AimAssistInfo {
		[field: SerializeField] public AimAssistMode Mode { get; private set; }
		[field: SerializeField] public float Radius { get; private set; } = 1.25f;
		[field: SerializeField] public float Range { get; private set; } = 1.25f;
		[field: SerializeField] public AnimationCurve Curve { get; private set; } = AnimationCurve.Linear(0, 0, 1, 1);
	}
}