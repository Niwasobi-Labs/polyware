using System;
using UnityEngine;

namespace PolyWare.Game {
	[Serializable]
	public class AimAssistInfo {
		public const float DefaultMaxVerticalAngle = 18f;
		[field: SerializeField] public AimAssistMode Mode { get; private set; }
		[field: SerializeField] public float Radius { get; private set; } = 1.25f;
		[field: SerializeField] public float Range { get; private set; } = 1.25f;
		[field: SerializeField] public AnimationCurve Curve { get; private set; } = AnimationCurve.Linear(0, 0, 1, 1);
		[field: SerializeField] public LayerMask LayerMask { get; private set; }
	}
}