using System;

namespace PolyWare.Game {
	[Serializable]
	public class CharacterMoveSettings {
		public float Mass = 1f;
		public float LinearDampening = 1f;
		public float AngularDampening = 1f;
		public float Acceleration = 30f;
		public float MaxMoveSpeed = 5f;
		public float TurnSpeed = 12f;
	}
}