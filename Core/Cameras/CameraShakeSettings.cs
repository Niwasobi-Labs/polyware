using System;

namespace PolyWare.Core {
	[Serializable]
	public struct CameraShakeSettings {
		public float Strength;
		public float Duration;
		public float Frequency;
		public float StartDelay;
		public float EndDelay;
	}
}